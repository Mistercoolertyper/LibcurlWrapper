using LibcurlWrapper;
using static LibcurlWrapper.LibcurlConstants;

Libcurl.Initialize();

var curl = new Libcurl(new(
	CURL_HTTP_VERSION.VERSION_3ONLY,
	CURL_TLS_VERSION.SSLVERSION_TLSv1_3
));

var getResponse = curl.Get("http://localhost/", [
	"SomeHeader: SomeValue"
]);

if (getResponse.ErrorMessage == null)
{
	Console.WriteLine($"GET Request: Http Version used: {getResponse.HttpVersion}; StatusCode: {getResponse.StatusCode}");
} else
{
	throw new Exception($"Failed to make GET request! {getResponse.ErrorMessage}");
}

var postResponse = curl.Post("https://cloudflare-quic.com/", "{\"Some\": \"Data\"}", [
	"Content-Type: application/json"
]);

if (postResponse.ErrorMessage == null)
{
	Console.WriteLine($"POST Request: Http Version used: {postResponse.HttpVersion}; StatusCode: {postResponse.StatusCode}");
} else
{
	throw new Exception($"Failed to make POST Request! {postResponse.ErrorMessage}");
}
