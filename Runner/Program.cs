using LibcurlWrapper;

Libcurl.Initialize();

var curl = new Libcurl(new(
	LibcurlConstants.CURL_HTTP_VERSION.VERSION_3ONLY,
	LibcurlConstants.CURL_TLS_VERSION.SSLVERSION_TLSv1_3
));

var getResponse = curl.Get("https://cloudflare-quic.com/")!;

Console.WriteLine($"GET Request: Http Version used: {getResponse.HttpVersion}; StatusCode: {getResponse.StatusCode}");

var postResponse = curl.Post("https://cloudflare-quic.com/", "{\"Some\": \"Data\"}", [
	"Content-Type: application/json"
]);

Console.WriteLine($"POST Request: Http Version used: {postResponse.HttpVersion}; StatusCode: {postResponse.StatusCode}");
