namespace LibcurlWrapper
{
	public class LibcurlOptions
	{
		public int HTTP_VERSION;
		public int SSL_VERSION;

		public LibcurlOptions()
		{
			HTTP_VERSION = (int)LibcurlConstants.CURL_HTTP_VERSION.VERSION_3;
			SSL_VERSION = (int)LibcurlConstants.CURL_TLS_VERSION.SSLVERSION_DEFAULT;
		}

		public LibcurlOptions(LibcurlConstants.CURL_HTTP_VERSION httpVersion, LibcurlConstants.CURL_TLS_VERSION sslVersion)
		{
			HTTP_VERSION = (int)httpVersion;
			SSL_VERSION = (int)sslVersion;
		}
	}
}
