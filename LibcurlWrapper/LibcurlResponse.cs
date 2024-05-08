namespace LibcurlWrapper
{
	public class LibcurlResponse
	{
		public string Body;
		public int StatusCode;
		public LibcurlConstants.CURL_HTTP_VERSION HttpVersion;

		public LibcurlResponse(string body, int statusCode, LibcurlConstants.CURL_HTTP_VERSION httpVersion)
		{
			Body = body;
			StatusCode = statusCode;
			HttpVersion = httpVersion;
		}
	}
}
