namespace LibcurlWrapper
{
	public class LibcurlResponse(string body, int statusCode, LibcurlConstants.CURL_HTTP_VERSION httpVersion)
    {
		public string Body = body;
		public int StatusCode = statusCode;
		public LibcurlConstants.CURL_HTTP_VERSION HttpVersion = httpVersion;
    }
}
