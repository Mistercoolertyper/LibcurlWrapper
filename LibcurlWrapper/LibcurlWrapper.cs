using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace LibcurlWrapper
{
	public class Libcurl
	{
		private delegate int WriteCallbackDelegate(IntPtr contents, int size, int nmemb, IntPtr userdata);
		private static readonly WriteCallbackDelegate writeCallbackDelegate = new(WriteCallback);
		private LibcurlOptions options;

		private void ExtractFileFromZip(byte[] zipFileContent, string destinationFolderPath, string fileNameInArchive)
		{
			using (MemoryStream memoryStream = new MemoryStream(zipFileContent))
			{
				using (ZipArchive archive = new ZipArchive(memoryStream))
				{
					foreach (ZipArchiveEntry entry in archive.Entries)
					{
						if (entry.Name == fileNameInArchive)
						{
							string destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(fileNameInArchive));
							entry.ExtractToFile(destinationFilePath);
							Console.WriteLine($"File extracted to: {destinationFilePath}");
							return;
						}
					}

					Console.WriteLine($"File '{fileNameInArchive}' not found in the zip archive.");
				}
			}
		}

		private async Task<byte[]> DownloadZipFileContent(string url)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				HttpResponseMessage response = await httpClient.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsByteArrayAsync();
				}
				else
				{
					throw new Exception($"Failed to download zip file. Status code: {response.StatusCode}");
				}
			}
		}

		private static byte[] LoadDLLResource()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();

			byte[] dllBytes;
			using Stream resourceStream = assembly.GetManifestResourceStream("LibcurlWrapper.libcurl-x64.dll")!;
			using MemoryStream memoryStream = new();
			resourceStream.CopyTo(memoryStream);
			dllBytes = memoryStream.ToArray();
			return dllBytes;
		}

		public static void Initialize()
		{
			Consts.dll = new(LoadDLLResource());
			if (!CertWriter.CheckCert())
			{
				try
				{
					CertWriter.WriteCert();
				}
				catch (Exception) {};
			}
			Consts.Initialized = true;
		}

		public Libcurl(LibcurlOptions? options = null)
		{
			if (!Consts.Initialized) throw new Exception("Libcurl has not been Initialized! Please call Libcurl.Initialize() before attempting to use the Library.");
			if (options != null)
			{
				this.options = options;
			} else
			{
				this.options = new();
			}
		}

		private static int WriteCallback(IntPtr contents, int size, int nmemb, IntPtr userp)
		{
			int totalSize = size * nmemb;
			byte[] buffer = new byte[totalSize];
			Marshal.Copy(contents, buffer, 0, totalSize);

			string responseBody = Encoding.UTF8.GetString(buffer);

			var responseBodyBuilder = GCHandle.FromIntPtr(userp).Target as StringBuilder;
			if (responseBodyBuilder != null)
			{
				responseBodyBuilder.Append(responseBody);
			}

			return totalSize;
		}

		public LibcurlResponse? Post(string url, string? data = null, List<string>? headers = null, bool verbose = false)
		{
			if (data == null) data = "";
			IntPtr curlInst = curl_easy_init();

			StringBuilder responseBodyBuilder = new StringBuilder();

			SetupRequest(curlInst, url, responseBodyBuilder, verbose);
			curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.POSTFIELDS, Marshal.StringToHGlobalAnsi(data));
			curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.POST, 1);

			if (headers != null)
			{
				curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.HTTPHEADER, CurlSlistArray([.. headers]));
			}

			int response = curl_easy_perform(curlInst);
			if (response != 0)
			{
				return null;
			}

			IntPtr statusCodePtr = Marshal.AllocHGlobal(sizeof(long));
			curl_easy_getinfo(curlInst, (int)LibcurlConstants.CURLINFO.RESPONSE_CODE, statusCodePtr);

			IntPtr httpVersionPtr = Marshal.AllocHGlobal(sizeof(long));
			curl_easy_getinfo(curlInst, (int)LibcurlConstants.CURLINFO.HTTP_VERSION, httpVersionPtr);

			var httpVersion = (LibcurlConstants.CURL_HTTP_VERSION)Enum.Parse(typeof(LibcurlConstants.CURL_HTTP_VERSION), ((int)Marshal.ReadInt64(statusCodePtr)).ToString());

			curl_easy_cleanup(curlInst);
			return new(responseBodyBuilder.ToString(), (int)Marshal.ReadInt64(statusCodePtr), httpVersion);
		}

		public LibcurlResponse? Get(string url, List<string>? headers = null, bool verbose = false)
		{
			IntPtr curlInst = curl_easy_init();

			StringBuilder responseBodyBuilder = new StringBuilder();

			SetupRequest(curlInst, url, responseBodyBuilder, verbose);

			if (headers != null)
			{
				curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.HTTPHEADER, CurlSlistArray([..headers]));
			}

			int response = curl_easy_perform(curlInst);
			if (response != 0)
			{
				return null;
			}

			IntPtr statusCodePtr = Marshal.AllocHGlobal(sizeof(long));
			curl_easy_getinfo(curlInst, (int)LibcurlConstants.CURLINFO.RESPONSE_CODE, statusCodePtr);

			IntPtr httpVersionPtr = Marshal.AllocHGlobal(sizeof(long));
			curl_easy_getinfo(curlInst, (int)LibcurlConstants.CURLINFO.HTTP_VERSION, httpVersionPtr);

			var httpVersion = (LibcurlConstants.CURL_HTTP_VERSION)Enum.Parse(typeof(LibcurlConstants.CURL_HTTP_VERSION), ((int)Marshal.ReadInt64(httpVersionPtr)).ToString());

			curl_easy_cleanup(curlInst);
			return new(responseBodyBuilder.ToString(), (int)Marshal.ReadInt64(statusCodePtr), httpVersion);
		}

		private IntPtr CurlSlistArray(string[] strings)
		{
			IntPtr slist = IntPtr.Zero;
			foreach (string str in strings)
			{
				slist = curl_slist_append(slist, str);
			}
			return slist;
		}

		private void SetupRequest(IntPtr curlInst, string url, StringBuilder responseData, bool verbose)
		{
			curl_easy_setopt(curlInst, ((int)LibcurlConstants.CURLOPT.URL), Marshal.StringToHGlobalAnsi(url));

			if (verbose)
			{
				curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.VERBOSE, 1);
			}

			curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.SSLVERSION, options.SSL_VERSION);
			curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.HTTP_VERSION, options.HTTP_VERSION);
			curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.CAINFO, Marshal.StringToHGlobalAnsi(".\\cert.crt"));

			curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.WRITEFUNCTION, Marshal.GetFunctionPointerForDelegate(writeCallbackDelegate));
			curl_easy_setopt(curlInst, (int)LibcurlConstants.CURLOPT.WRITEDATA, GCHandle.ToIntPtr(GCHandle.Alloc(responseData)));
		}

		private IntPtr curl_easy_init()
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_easy_init_delegate>("curl_easy_init")();
		}

		private int curl_easy_setopt(IntPtr curl, int option, IntPtr parameter)
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_easy_setopt_delegate>("curl_easy_setopt")(curl, option, parameter);
		}

		private int curl_easy_perform(IntPtr curl)
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_easy_perform_delegate>("curl_easy_perform")(curl);
		}

		private void curl_easy_cleanup(IntPtr curl)
		{
			Consts.dll!.GetDelegateFromFuncName<curl_easy_cleanup_delegate>("curl_easy_cleanup")(curl);
		}

		private int curl_easy_getinfo(IntPtr curl, int info, IntPtr param)
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_easy_getinfo_delegate>("curl_easy_getinfo")(curl, info, param);
		}

		private IntPtr curl_slist_append(IntPtr slist, string str)
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_slist_append_delegate>("curl_slist_append")(slist, str);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr curl_easy_init_delegate();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int curl_easy_setopt_delegate(IntPtr curl, int option, IntPtr parameter);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int curl_easy_perform_delegate(IntPtr curl);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void curl_easy_cleanup_delegate(IntPtr curl);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int curl_easy_getinfo_delegate(IntPtr curl, int info, IntPtr param);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr curl_slist_append_delegate(IntPtr slist, string str);
	}
}
