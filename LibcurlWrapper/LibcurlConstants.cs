﻿namespace LibcurlWrapper
{
	public class LibcurlConstants
	{
		public enum CURLOPT
		{
			WRITEDATA = 10001,
			URL = 10002,
			PORT = 3,
			PROXY = 10004,
			USERPWD = 10005,
			PROXYUSERPWD = 10006,
			RANGE = 10007,
			READDATA = 10009,
			ERRORBUFFER = 10010,
			WRITEFUNCTION = 20011,
			READFUNCTION = 20012,
			TIMEOUT = 13,
			INFILESIZE = 14,
			POSTFIELDS = 10015,
			REFERER = 10016,
			FTPPORT = 10017,
			USERAGENT = 10018,
			LOW_SPEED_LIMIT = 19,
			LOW_SPEED_TIME = 20,
			RESUME_FROM = 21,
			COOKIE = 10022,
			HTTPHEADER = 10023,
			SSLCERT = 10025,
			KEYPASSWD = 10026,
			CRLF = 27,
			QUOTE = 10028,
			HEADERDATA = 10029,
			COOKIEFILE = 10031,
			SSLVERSION = 32,
			TIMECONDITION = 33,
			TIMEVALUE = 34,
			CUSTOMREQUEST = 10036,
			STDERR = 10037,
			POSTQUOTE = 10039,
			OBSOLETE40 = 10040,
			VERBOSE = 41,
			HEADER = 42,
			NOPROGRESS = 43,
			NOBODY = 44,
			FAILONERROR = 45,
			UPLOAD = 46,
			POST = 47,
			DIRLISTONLY = 48,
			APPEND = 50,
			NETRC = 51,
			FOLLOWLOCATION = 52,
			TRANSFERTEXT = 53,
			XFERINFODATA = 10057,
			AUTOREFERER = 58,
			PROXYPORT = 59,
			POSTFIELDSIZE = 60,
			HTTPPROXYTUNNEL = 61,
			INTERFACE = 10062,
			KRBLEVEL = 10063,
			SSL_VERIFYPEER = 64,
			CAINFO = 10065,
			MAXREDIRS = 68,
			FILETIME = 69,
			TELNETOPTIONS = 10070,
			MAXCONNECTS = 71,
			OBSOLETE72 = 72,
			FRESH_CONNECT = 74,
			FORBID_REUSE = 75,
			CONNECTTIMEOUT = 78,
			HEADERFUNCTION = 20079,
			HTTPGET = 80,
			SSL_VERIFYHOST = 81,
			COOKIEJAR = 10082,
			SSL_CIPHER_LIST = 10083,
			HTTP_VERSION = 84,
			FTP_USE_EPSV = 85,
			SSLCERTTYPE = 10086,
			SSLKEY = 10087,
			SSLKEYTYPE = 10088,
			SSLENGINE = 10089,
			SSLENGINE_DEFAULT = 90,
			DNS_CACHE_TIMEOUT = 92,
			PREQUOTE = 10093,
			DEBUGFUNCTION = 20094,
			DEBUGDATA = 10095,
			COOKIESESSION = 96,
			CAPATH = 10097,
			BUFFERSIZE = 98,
			NOSIGNAL = 99,
			SHARE = 10100,
			PROXYTYPE = 101,
			ACCEPT_ENCODING = 10102,
			PRIVATE = 10103,
			HTTP200ALIASES = 10104,
			UNRESTRICTED_AUTH = 105,
			FTP_USE_EPRT = 106,
			HTTPAUTH = 107,
			SSL_CTX_FUNCTION = 20108,
			SSL_CTX_DATA = 10109,
			FTP_CREATE_MISSING_DIRS = 110,
			PROXYAUTH = 111,
			SERVER_RESPONSE_TIMEOUT = 112,
			IPRESOLVE = 113,
			MAXFILESIZE = 114,
			INFILESIZE_LARGE = 30115,
			RESUME_FROM_LARGE = 30116,
			MAXFILESIZE_LARGE = 30117,
			NETRC_FILE = 10118,
			USE_SSL = 119,
			POSTFIELDSIZE_LARGE = 30120,
			TCP_NODELAY = 121,
			FTPSSLAUTH = 129,
			FTP_ACCOUNT = 10134,
			COOKIELIST = 10135,
			IGNORE_CONTENT_LENGTH = 136,
			FTP_SKIP_PASV_IP = 137,
			FTP_FILEMETHOD = 138,
			LOCALPORT = 139,
			LOCALPORTRANGE = 140,
			CONNECT_ONLY = 141,
			MAX_SEND_SPEED_LARGE = 30145,
			MAX_RECV_SPEED_LARGE = 30146,
			FTP_ALTERNATIVE_TO_USER = 10147,
			SOCKOPTFUNCTION = 20148,
			SOCKOPTDATA = 10149,
			SSL_SESSIONID_CACHE = 150,
			SSH_AUTH_TYPES = 151,
			SSH_PUBLIC_KEYFILE = 10152,
			SSH_PRIVATE_KEYFILE = 10153,
			FTP_SSL_CCC = 154,
			TIMEOUT_MS = 155,
			CONNECTTIMEOUT_MS = 156,
			HTTP_TRANSFER_DECODING = 157,
			HTTP_CONTENT_DECODING = 158,
			NEW_FILE_PERMS = 159,
			NEW_DIRECTORY_PERMS = 160,
			POSTREDIR = 161,
			SSH_HOST_PUBLIC_KEY_MD5 = 10162,
			OPENSOCKETFUNCTION = 20163,
			OPENSOCKETDATA = 10164,
			COPYPOSTFIELDS = 10165,
			PROXY_TRANSFER_MODE = 166,
			SEEKFUNCTION = 20167,
			SEEKDATA = 10168,
			CRLFILE = 10169,
			ISSUERCERT = 10170,
			ADDRESS_SCOPE = 171,
			CERTINFO = 172,
			USERNAME = 10173,
			PASSWORD = 10174,
			PROXYUSERNAME = 10175,
			PROXYPASSWORD = 10176,
			NOPROXY = 10177,
			TFTP_BLKSIZE = 178,
			SOCKS5_GSSAPI_NEC = 180,
			SSH_KNOWNHOSTS = 10183,
			SSH_KEYFUNCTION = 20184,
			SSH_KEYDATA = 10185,
			MAIL_FROM = 10186,
			MAIL_RCPT = 10187,
			FTP_USE_PRET = 188,
			RTSP_REQUEST = 189,
			RTSP_SESSION_ID = 10190,
			RTSP_STREAM_URI = 10191,
			RTSP_TRANSPORT = 10192,
			RTSP_CLIENT_CSEQ = 193,
			RTSP_SERVER_CSEQ = 194,
			INTERLEAVEDATA = 10195,
			INTERLEAVEFUNCTION = 20196,
			WILDCARDMATCH = 197,
			CHUNK_BGN_FUNCTION = 20198,
			CHUNK_END_FUNCTION = 20199,
			FNMATCH_FUNCTION = 20200,
			CHUNK_DATA = 10201,
			FNMATCH_DATA = 10202,
			RESOLVE = 10203,
			TLSAUTH_USERNAME = 10204,
			TLSAUTH_PASSWORD = 10205,
			TLSAUTH_TYPE = 10206,
			TRANSFER_ENCODING = 207,
			CLOSESOCKETFUNCTION = 20208,
			CLOSESOCKETDATA = 10209,
			GSSAPI_DELEGATION = 210,
			DNS_SERVERS = 10211,
			ACCEPTTIMEOUT_MS = 212,
			TCP_KEEPALIVE = 213,
			TCP_KEEPIDLE = 214,
			TCP_KEEPINTVL = 215,
			SSL_OPTIONS = 216,
			MAIL_AUTH = 10217,
			SASL_IR = 218,
			XFERINFOFUNCTION = 20219,
			XOAUTH2_BEARER = 10220,
			DNS_INTERFACE = 10221,
			DNS_LOCAL_IP4 = 10222,
			DNS_LOCAL_IP6 = 10223,
			LOGIN_OPTIONS = 10224,
			SSL_ENABLE_ALPN = 226,
			EXPECT_100_TIMEOUT_MS = 227,
			PROXYHEADER = 10228,
			HEADEROPT = 229,
			PINNEDPUBLICKEY = 10230,
			UNIX_SOCKET_PATH = 10231,
			SSL_VERIFYSTATUS = 232,
			SSL_FALSESTART = 233,
			PATH_AS_IS = 234,
			PROXY_SERVICE_NAME = 10235,
			SERVICE_NAME = 10236,
			PIPEWAIT = 237,
			DEFAULT_PROTOCOL = 10238,
			STREAM_WEIGHT = 239,
			STREAM_DEPENDS = 10240,
			STREAM_DEPENDS_E = 10241,
			TFTP_NO_OPTIONS = 242,
			CONNECT_TO = 10243,
			TCP_FASTOPEN = 244,
			KEEP_SENDING_ON_ERROR = 245,
			PROXY_CAINFO = 10246,
			PROXY_CAPATH = 10247,
			PROXY_SSL_VERIFYPEER = 248,
			PROXY_SSL_VERIFYHOST = 249,
			PROXY_SSLVERSION = 250,
			PROXY_TLSAUTH_USERNAME = 10251,
			PROXY_TLSAUTH_PASSWORD = 10252,
			PROXY_TLSAUTH_TYPE = 10253,
			PROXY_SSLCERT = 10254,
			PROXY_SSLCERTTYPE = 10255,
			PROXY_SSLKEY = 10256,
			PROXY_SSLKEYTYPE = 10257,
			PROXY_KEYPASSWD = 10258,
			PROXY_SSL_CIPHER_LIST = 10259,
			PROXY_CRLFILE = 10260,
			PROXY_SSL_OPTIONS = 261,
			PRE_PROXY = 10262,
			PROXY_PINNEDPUBLICKEY = 10263,
			ABSTRACT_UNIX_SOCKET = 10264,
			SUPPRESS_CONNECT_HEADERS = 265,
			REQUEST_TARGET = 10266,
			SOCKS5_AUTH = 267,
			SSH_COMPRESSION = 268,
			MIMEPOST = 10269,
			TIMEVALUE_LARGE = 30270,
			HAPPY_EYEBALLS_TIMEOUT_MS = 271,
			RESOLVER_START_FUNCTION = 20272,
			RESOLVER_START_DATA = 10273,
			HAPROXYPROTOCOL = 274,
			DNS_SHUFFLE_ADDRESSES = 275,
			TLS13_CIPHERS = 10276,
			PROXY_TLS13_CIPHERS = 10277,
			DISALLOW_USERNAME_IN_URL = 278,
			DOH_URL = 10279,
			UPLOAD_BUFFERSIZE = 280,
			UPKEEP_INTERVAL_MS = 281,
			CURLU = 10282,
			TRAILERFUNCTION = 20283,
			TRAILERDATA = 10284,
			HTTP09_ALLOWED = 285,
			ALTSVC_CTRL = 286,
			ALTSVC = 10287,
			MAXAGE_CONN = 288,
			SASL_AUTHZID = 10289,
			MAIL_RCPT_ALLOWFAILS = 290,
			SSLCERT_BLOB = 40291,
			SSLKEY_BLOB = 40292,
			PROXY_SSLCERT_BLOB = 40293,
			PROXY_SSLKEY_BLOB = 40294,
			ISSUERCERT_BLOB = 40295,
			PROXY_ISSUERCERT = 10296,
			PROXY_ISSUERCERT_BLOB = 40297,
			SSL_EC_CURVES = 10298,
			HSTS_CTRL = 299,
			HSTS = 10300,
			HSTSREADFUNCTION = 20301,
			HSTSREADDATA = 10302,
			HSTSWRITEFUNCTION = 20303,
			HSTSWRITEDATA = 10304,
			AWS_SIGV4 = 10305,
			DOH_SSL_VERIFYPEER = 306,
			DOH_SSL_VERIFYHOST = 307,
			DOH_SSL_VERIFYSTATUS = 308,
			CAINFO_BLOB = 40309,
			PROXY_CAINFO_BLOB = 40310,
			SSH_HOST_PUBLIC_KEY_SHA256 = 10311,
			PREREQFUNCTION = 20312,
			PREREQDATA = 10313,
			MAXLIFETIME_CONN = 314,
			MIME_OPTIONS = 315,
			SSH_HOSTKEYFUNCTION = 20316,
			SSH_HOSTKEYDATA = 10317,
			PROTOCOLS_STR = 10318,
			REDIR_PROTOCOLS_STR = 10319,
			WS_OPTIONS = 320,
			CA_CACHE_TIMEOUT = 321,
			QUICK_EXIT = 322,
			HAPROXY_CLIENT_IP = 10323,
			SERVER_RESPONSE_TIMEOUT_MS = 324,
		}

		public enum CURL_HTTP_VERSION
		{
			NONE,
			VERSION_1_0,
			VERSION_1_1,
			VERSION_2_0,
			VERSION_2TLS,
			VERSION_2_PRIOR_KNOWLEDGE,
			VERSION_3 = 30,
			VERSION_3ONLY = 31
		}

		public enum CURL_TLS_VERSION
		{
			SSLVERSION_DEFAULT,
			SSLVERSION_TLSv1,
			SSLVERSION_SSLv2,
			SSLVERSION_SSLv3,
			SSLVERSION_TLSv1_0,
			SSLVERSION_TLSv1_1,
			SSLVERSION_TLSv1_2,
			SSLVERSION_TLSv1_3
		}

		public enum CURLINFO
		{
			EFFECTIVE_URL = 0x100000 + 1,
			RESPONSE_CODE = 0x200000 + 2,
			TOTAL_TIME = 0x300000 + 3,
			NAMELOOKUP_TIME = 0x300000 + 4,
			CONNECT_TIME = 0x300000 + 5,
			PRETRANSFER_TIME = 0x300000 + 6,
			SIZE_UPLOAD_T = 0x600000 + 7,
			SIZE_DOWNLOAD_T = 0x600000 + 8,
			SPEED_DOWNLOAD_T = 0x600000 + 9,
			SPEED_UPLOAD_T = 0x600000 + 10,
			HEADER_SIZE = 0x200000 + 11,
			REQUEST_SIZE = 0x200000 + 12,
			SSL_VERIFYRESULT = 0x200000 + 13,
			FILETIME = 0x200000 + 14,
			FILETIME_T = 0x600000 + 14,
			CONTENT_LENGTH_DOWNLOAD_T = 0x600000 + 15,
			CONTENT_LENGTH_UPLOAD_T = 0x600000 + 16,
			STARTTRANSFER_TIME = 0x300000 + 17,
			CONTENT_TYPE = 0x100000 + 18,
			REDIRECT_TIME = 0x300000 + 19,
			REDIRECT_COUNT = 0x200000 + 20,
			PRIVATE = 0x100000 + 21,
			HTTP_CONNECTCODE = 0x200000 + 22,
			HTTPAUTH_AVAIL = 0x200000 + 23,
			PROXYAUTH_AVAIL = 0x200000 + 24,
			OS_ERRNO = 0x200000 + 25,
			NUM_CONNECTS = 0x200000 + 26,
			SSL_ENGINES = 0x400000 + 27,
			COOKIELIST = 0x400000 + 28,
			FTP_ENTRY_PATH = 0x100000 + 30,
			REDIRECT_URL = 0x100000 + 31,
			PRIMARY_IP = 0x100000 + 32,
			APPCONNECT_TIME = 0x300000 + 33,
			CERTINFO = 0x400000 + 34,
			CONDITION_UNMET = 0x200000 + 35,
			RTSP_SESSION_ID = 0x100000 + 36,
			RTSP_CLIENT_CSEQ = 0x200000 + 37,
			RTSP_SERVER_CSEQ = 0x200000 + 38,
			RTSP_CSEQ_RECV = 0x200000 + 39,
			PRIMARY_PORT = 0x200000 + 40,
			LOCAL_IP = 0x100000 + 41,
			LOCAL_PORT = 0x200000 + 42,
			ACTIVESOCKET = 0x500000 + 44,
			TLS_SSL_PTR = 0x400000 + 45,
			HTTP_VERSION = 0x200000 + 46,
			PROXY_SSL_VERIFYRESULT = 0x200000 + 47,
			SCHEME = 0x100000 + 49,
			TOTAL_TIME_T = 0x600000 + 50,
			NAMELOOKUP_TIME_T = 0x600000 + 51,
			CONNECT_TIME_T = 0x600000 + 52,
			PRETRANSFER_TIME_T = 0x600000 + 53,
			STARTTRANSFER_TIME_T = 0x600000 + 54,
			REDIRECT_TIME_T = 0x600000 + 55,
			APPCONNECT_TIME_T = 0x600000 + 56,
			RETRY_AFTER = 0x600000 + 57,
			EFFECTIVE_METHOD = 0x100000 + 58,
			PROXY_ERROR = 0x200000 + 59,
			REFERER = 0x100000 + 60,
			CAINFO = 0x100000 + 61,
			CAPATH = 0x100000 + 62,
			XFER_ID = 0x600000 + 63,
			CONN_ID = 0x600000 + 64,
			QUEUE_TIME_T = 0x600000 + 65,
			USED_PROXY = 0x200000 + 66,
		}
	}
}