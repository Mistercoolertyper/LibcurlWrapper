﻿using System.Runtime.InteropServices;
using LibcurlWrapper.Exceptions;
using System.Reflection;
using System.Text;
using static LibcurlWrapper.LibcurlConstants;

namespace LibcurlWrapper
{
	public class Libcurl
	{
		private delegate int WriteCallbackDelegate(IntPtr contents, int size, int nmemb, IntPtr userdata);
		private static readonly WriteCallbackDelegate writeCallbackDelegate = new(WriteCallback);
		private readonly LibcurlOptions options;

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
			if (Consts.Initialized)
			{
				throw new AlreadyInitializedException("LibcurlWrapper has already been Initialized. Make sure to only call Libcurl.Initialize() once.");
			}
			Consts.dll = new(LoadDLLResource());
			if (!CertWriter.CheckCert())
			{
				try
				{
					CertWriter.WriteCert();
				}
				catch (Exception e)
				{
					throw new Exception("Failed to write Certificate.", e);
				}
			}
			Consts.Initialized = true;
		}

		public Libcurl(LibcurlOptions? options = null)
		{
			if (!Consts.Initialized)
			{
				throw new NotInitializedException("LibcurlWrapper has not been Initialized. Call Libcurl.Initialize() to initialize LibcurlWrapper.");
			}
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
			responseBodyBuilder?.Append(responseBody);

			return totalSize;
		}

		private static LibcurlResponse GetResponse(IntPtr curlInst, StringBuilder responseBodyBuilder)
		{
			IntPtr statusCodePtr = Marshal.AllocHGlobal(sizeof(long));
			curl_easy_getinfo(curlInst, CURLINFO.RESPONSE_CODE, statusCodePtr);

			IntPtr httpVersionPtr = Marshal.AllocHGlobal(sizeof(long));
			curl_easy_getinfo(curlInst, CURLINFO.HTTP_VERSION, httpVersionPtr);

			var httpVersion = (CURL_HTTP_VERSION)Enum.Parse(typeof(CURL_HTTP_VERSION), ((int)Marshal.ReadInt64(statusCodePtr)).ToString());
			var statusCode = (int)Marshal.ReadInt64(statusCodePtr);

			Marshal.FreeHGlobal(statusCodePtr);
			Marshal.FreeHGlobal(httpVersionPtr);

			return new(responseBodyBuilder.ToString(), statusCode, httpVersion, null);
		}

		public LibcurlResponse Post(string url, string? data = null, List<string>? headers = null, bool verbose = false)
		{
			data ??= "";
			IntPtr curlInst = curl_easy_init();

			StringBuilder responseBodyBuilder = new();

			SetupRequest(curlInst, url, responseBodyBuilder, verbose);
			curl_easy_setopt(curlInst, CURLOPT.POSTFIELDS, Marshal.StringToHGlobalAnsi(data));
			curl_easy_setopt(curlInst, CURLOPT.POST, 1);

			if (headers != null)
			{
				curl_easy_setopt(curlInst, CURLOPT.HTTPHEADER, CurlSlistArray([.. headers]));
			}

			CURLCode response = (CURLCode)curl_easy_perform(curlInst);
			if (response != CURLCode.CURLE_OK)
			{
				return new(null, null, null, curl_easy_stderr(response));
			}

			curl_easy_cleanup(curlInst);
			return GetResponse(curlInst, responseBodyBuilder);
		}

		public LibcurlResponse Get(string url, List<string>? headers = null, bool verbose = false)
		{
			IntPtr curlInst = curl_easy_init();

			StringBuilder responseBodyBuilder = new();

			SetupRequest(curlInst, url, responseBodyBuilder, verbose);

			if (headers != null)
			{
				curl_easy_setopt(curlInst, CURLOPT.HTTPHEADER, CurlSlistArray([..headers]));
			}

			CURLCode response = (CURLCode)curl_easy_perform(curlInst);
			if (response != CURLCode.CURLE_OK)
			{
				return new(null, null, null, curl_easy_stderr(response));
			}

			return GetResponse(curlInst, responseBodyBuilder);
		}

		private static IntPtr CurlSlistArray(string[] strings)
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
			curl_easy_setopt(curlInst, CURLOPT.URL, Marshal.StringToHGlobalAnsi(url));

			if (verbose)
			{
				curl_easy_setopt(curlInst, CURLOPT.VERBOSE, 1);
			}

			curl_easy_setopt(curlInst, CURLOPT.SSLVERSION, options.SSL_VERSION);
			curl_easy_setopt(curlInst, CURLOPT.HTTP_VERSION, options.HTTP_VERSION);
			curl_easy_setopt(curlInst, CURLOPT.CAINFO, Marshal.StringToHGlobalAnsi(".\\cert.crt"));

			curl_easy_setopt(curlInst, CURLOPT.WRITEFUNCTION, Marshal.GetFunctionPointerForDelegate(writeCallbackDelegate));
			curl_easy_setopt(curlInst, CURLOPT.WRITEDATA, GCHandle.ToIntPtr(GCHandle.Alloc(responseData)));
		}

		private static IntPtr curl_easy_init()
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_easy_init_delegate>("curl_easy_init")();
		}

		private static int curl_easy_setopt(IntPtr curl, CURLOPT option, IntPtr parameter)
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_easy_setopt_delegate>("curl_easy_setopt")(curl, option, parameter);
		}

		private static int curl_easy_perform(IntPtr curl)
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_easy_perform_delegate>("curl_easy_perform")(curl);
		}

		private static string curl_easy_stderr(CURLCode errorCode)
		{
			IntPtr errorMessagePtr = Consts.dll!.GetDelegateFromFuncName<curl_easy_strerror_delegate>("curl_easy_strerror")(errorCode);
			string errorMessage = Marshal.PtrToStringAnsi(errorMessagePtr)!;
			return errorMessage;
		}

		private static void curl_easy_cleanup(IntPtr curl)
		{
			Consts.dll!.GetDelegateFromFuncName<curl_easy_cleanup_delegate>("curl_easy_cleanup")(curl);
		}

		private static int curl_easy_getinfo(IntPtr curl, CURLINFO info, IntPtr param)
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_easy_getinfo_delegate>("curl_easy_getinfo")(curl, info, param);
		}

		private static IntPtr curl_slist_append(IntPtr slist, string str)
		{
			return Consts.dll!.GetDelegateFromFuncName<curl_slist_append_delegate>("curl_slist_append")(slist, str);
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr curl_easy_init_delegate();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int curl_easy_setopt_delegate(IntPtr curl, CURLOPT option, IntPtr parameter);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int curl_easy_perform_delegate(IntPtr curl);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void curl_easy_cleanup_delegate(IntPtr curl);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int curl_easy_getinfo_delegate(IntPtr curl, CURLINFO info, IntPtr param);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr curl_slist_append_delegate(IntPtr slist, string str);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr curl_easy_strerror_delegate(CURLCode errornum);
	}
}
