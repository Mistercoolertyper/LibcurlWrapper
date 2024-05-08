using static LibcurlWrapper.LibcurlConstants;

namespace LibcurlWrapper
{
	public class LibcurlResponse(string? body, int? statusCode, CURL_HTTP_VERSION? httpVersion, string? errorMessage)
	{
		public string? Body = body;
		public int? StatusCode = statusCode;
		public CURL_HTTP_VERSION? HttpVersion = httpVersion;
		public readonly string? ErrorMessage = errorMessage;
	}
}
