using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet
{
	public class ApiShared : ISignProvider
	{
		public const string ADDR_API = "api.cloudinary.com";

		public const string ADDR_RES = "res.cloudinary.com";

		public const string API_VERSION = "v1_1";

		public const string HTTP_BOUNDARY = "notrandomsequencetouseasboundary";

		public static string USER_AGENT = BuildUserAgent();

		public bool CSubDomain;

		public bool ShortenUrl;

		public bool UseRootPath;

		public bool UsePrivateCdn;

		public bool Secure;

		public string PrivateCdn;

		public string Suffix;

		public string UserPlatform;

		public int Timeout;

		public bool ForceVersion = true;

		public bool UseChunkedEncoding = true;

		public int ChunkSize = 65000;

		public SignatureAlgorithm SignatureAlgorithm;

		protected string m_apiAddr = "https://api.cloudinary.com";

		private readonly Func<string, HttpRequestMessage> requestBuilder = (string url) => new HttpRequestMessage
		{
			RequestUri = new Uri(url)
		};

		public HttpClient Client = new HttpClient();

		public Account Account { get; private set; }

		public string ApiBaseAddress
		{
			get
			{
				return m_apiAddr;
			}
			set
			{
				m_apiAddr = value;
			}
		}

		public Url Url => new Url(Account.Cloud, this).CSubDomain(CSubDomain).Shorten(ShortenUrl).PrivateCdn(UsePrivateCdn)
			.Secure(Secure)
			.ForceVersion(ForceVersion)
			.SecureDistribution(PrivateCdn);

		public Url UrlImgUp => Url.ResourceType("image").Action("upload").UseRootPath(UseRootPath)
			.Suffix(Suffix);

		public Url UrlImgFetch => Url.ResourceType("image").Action("fetch").UseRootPath(UseRootPath)
			.Suffix(Suffix);

		public Url UrlVideoUp => Url.ResourceType("video").Action("upload").UseRootPath(UseRootPath)
			.Suffix(Suffix);

		public Url ApiUrl => Url.CloudinaryAddr(m_apiAddr);

		public Url ApiUrlImgUp => ApiUrl.Action("upload").ResourceType("image");

		public Url ApiUrlV => ApiUrl.ApiVersion("v1_1");

		public Url ApiUrlStreamingProfileV => ApiUrlV.Add("streaming_profiles");

		public Url ApiUrlMetadataFieldV => ApiUrlV.Add("metadata_fields");

		public Url ApiUrlImgUpV => ApiUrlV.Action("upload").ResourceType("image");

		public Url ApiUrlVideoUpV => ApiUrlV.Action("upload").ResourceType("video");

		public ApiShared()
		{
			Account = new Account();
		}

		public ApiShared(string cloudinaryUrl)
		{
			if (string.IsNullOrEmpty(cloudinaryUrl) || !cloudinaryUrl.StartsWith("cloudinary://", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException("Invalid CLOUDINARY_URL scheme. Expecting to start with 'cloudinary://'");
			}
			Uri uri = new Uri(cloudinaryUrl);
			if (string.IsNullOrEmpty(uri.Host))
			{
				throw new ArgumentException("Cloud name must be specified as host name in URL!");
			}
			string[] array = uri.UserInfo.Split(':');
			Account = new Account(uri.Host, array[0], array[1]);
			UsePrivateCdn = !string.IsNullOrEmpty(uri.AbsolutePath) && uri.AbsolutePath != "/";
			PrivateCdn = string.Empty;
			if (UsePrivateCdn)
			{
				PrivateCdn = uri.AbsolutePath;
				Secure = true;
			}
		}

		public ApiShared(Account account, bool usePrivateCdn, string privateCdn, bool shortenUrl, bool cSubDomain)
			: this(account)
		{
			UsePrivateCdn = usePrivateCdn;
			Secure = usePrivateCdn;
			PrivateCdn = privateCdn;
			ShortenUrl = shortenUrl;
			CSubDomain = cSubDomain;
		}

		public ApiShared(Account account)
		{
			if (account == null)
			{
				throw new ArgumentException("Account can't be null!");
			}
			if (string.IsNullOrEmpty(account.Cloud))
			{
				throw new ArgumentException("Cloud name must be specified in Account!");
			}
			UsePrivateCdn = false;
			Account = account;
		}

		public static string GetCloudinaryParam<T>(T e)
		{
			EnumMemberAttribute[] obj = (EnumMemberAttribute[])typeof(T).GetField(e.ToString()).GetCustomAttributes(typeof(EnumMemberAttribute), inherit: false);
			if (obj.Length == 0)
			{
				throw new ArgumentException("Enum fields must be decorated with EnumMemberAttribute!");
			}
			return obj[0].Value;
		}

		public static T ParseCloudinaryParam<T>(string s)
		{
			FieldInfo[] fields = typeof(T).GetFields();
			foreach (FieldInfo fieldInfo in fields)
			{
				EnumMemberAttribute[] array = (EnumMemberAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), inherit: false);
				if (array.Length != 0 && s == array[0].Value)
				{
					return (T)fieldInfo.GetValue(null);
				}
			}
			return default(T);
		}

		public async Task<T> CallAndParseAsync<T>(HttpMethod method, string url, SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null, CancellationToken? cancellationToken = null) where T : BaseResult, new()
		{
			using HttpResponseMessage response = await CallAsync(method, url, parameters, file, extraHeaders, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return await ParseAsync<T>(response).ConfigureAwait(continueOnCapturedContext: false);
		}

		public T CallAndParse<T>(HttpMethod method, string url, SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null) where T : BaseResult, new()
		{
			using HttpResponseMessage response = Call(method, url, parameters, file, extraHeaders);
			return Parse<T>(response);
		}

		public async Task<HttpResponseMessage> CallAsync(HttpMethod method, string url, SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null, CancellationToken? cancellationToken = null)
		{
			using HttpRequestMessage request = await PrepareRequestBodyAsync(requestBuilder(url), method, parameters, file, extraHeaders, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			CancellationToken cancellationToken2 = cancellationToken ?? GetDefaultCancellationToken();
			return await Client.SendAsync(request, cancellationToken2).ConfigureAwait(continueOnCapturedContext: false);
		}

		public HttpResponseMessage Call(HttpMethod method, string url, SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null)
		{
			using HttpRequestMessage request = requestBuilder(url);
			PrepareRequestBody(request, method, parameters, file, extraHeaders);
			CancellationToken defaultCancellationToken = GetDefaultCancellationToken();
			return Client.SendAsync(request, defaultCancellationToken).GetAwaiter().GetResult();
		}

		public string GetUploadUrl(string resourceType = "auto")
		{
			return ApiUrlV.Action("upload").ResourceType(resourceType).BuildUrl();
		}

		public string PrepareUploadParams(IDictionary<string, object> parameters)
		{
			if (parameters == null)
			{
				parameters = new SortedDictionary<string, object>();
			}
			if (!(parameters is SortedDictionary<string, object>))
			{
				parameters = new SortedDictionary<string, object>(parameters);
			}
			foreach (string item in parameters.Keys.ToList())
			{
				object obj = parameters[item];
				if (obj is IEnumerable<string> items)
				{
					parameters[item] = Utils.SafeJoin("|", items);
				}
				else if (obj is Transformation transformation)
				{
					parameters[item] = transformation.Generate();
				}
			}
			string path = string.Empty;
			if (parameters.ContainsKey("callback") && parameters["callback"] != null)
			{
				path = parameters["callback"].ToString();
			}
			try
			{
				parameters["callback"] = BuildCallbackUrl(path);
			}
			catch (ArgumentException)
			{
			}
			if (!parameters.ContainsKey("unsigned") || parameters["unsigned"].ToString() == "false")
			{
				FinalizeUploadParameters(parameters);
			}
			return JsonConvert.SerializeObject(parameters);
		}

		public string SignParameters(IDictionary<string, object> parameters)
		{
			List<string> excludedSignatureKeys = new List<string>(new string[3] { "resource_type", "file", "api_key" });
			StringBuilder stringBuilder = new StringBuilder(string.Join("&", parameters.Where((KeyValuePair<string, object> pair) => pair.Value != null && !excludedSignatureKeys.Any((string s) => pair.Key.Equals(s, StringComparison.Ordinal))).Select(delegate(KeyValuePair<string, object> pair)
			{
				string arg = ((pair.Value is IEnumerable<string>) ? string.Join(",", ((IEnumerable<string>)pair.Value).ToArray()) : pair.Value.ToString());
				return string.Format(CultureInfo.InvariantCulture, "{0}={1}", pair.Key, arg);
			}).ToArray()));
			stringBuilder.Append(Account.ApiSecret);
			byte[] array = Utils.ComputeHash(stringBuilder.ToString(), SignatureAlgorithm);
			StringBuilder stringBuilder2 = new StringBuilder();
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder2.Append(b.ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder2.ToString();
		}

		public string SignUriPart(string uriPart, bool isLong = true)
		{
			string s = uriPart + Account.ApiSecret;
			SignatureAlgorithm signatureAlgorithm = (isLong ? SignatureAlgorithm.SHA256 : SignatureAlgorithm);
			byte[] bytes = Utils.ComputeHash(s, signatureAlgorithm);
			int length = (isLong ? 32 : 8);
			return "s--" + Utils.EncodeUrlSafe(bytes).Substring(0, length) + "--/";
		}

		public bool VerifyApiResponseSignature(string publicId, string version, string signature)
		{
			SortedDictionary<string, object> parameters = new SortedDictionary<string, object>
			{
				{ "public_id", publicId },
				{ "version", version }
			};
			string value = SignParameters(parameters);
			return signature.Equals(value, StringComparison.Ordinal);
		}

		public bool VerifyNotificationSignature(string body, long timestamp, string signature, int validFor = 7200)
		{
			long num = Utils.UnixTimeNowSeconds();
			if (timestamp <= num - validFor)
			{
				return false;
			}
			string value = Utils.ComputeHexHash($"{body}{timestamp}{Account.ApiSecret}", SignatureAlgorithm);
			return signature.Equals(value, StringComparison.Ordinal);
		}

		public virtual string BuildCallbackUrl(string path = "")
		{
			return string.Empty;
		}

		public string BuildUnsignedUploadForm(string field, string preset, string resourceType, SortedDictionary<string, object> parameters = null, Dictionary<string, string> htmlOptions = null)
		{
			return BuildUploadForm(field, resourceType, BuildUnsignedUploadParams(preset, parameters), htmlOptions);
		}

		public string BuildUploadForm(string field, string resourceType, SortedDictionary<string, object> parameters = null, Dictionary<string, string> htmlOptions = null)
		{
			return BuildUploadFormShared(field, resourceType, parameters, htmlOptions);
		}

		public string BuildUploadFormShared(string field, string resourceType, SortedDictionary<string, object> parameters = null, Dictionary<string, string> htmlOptions = null)
		{
			if (htmlOptions == null)
			{
				htmlOptions = new Dictionary<string, string>();
			}
			if (string.IsNullOrEmpty(resourceType))
			{
				resourceType = "auto";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<input type='file' name='file' data-url='").Append(GetUploadUrl(resourceType)).Append("' data-form-data='")
				.Append(PrepareUploadParams(parameters))
				.Append("' data-cloudinary-field='")
				.Append(field)
				.Append("' class='cloudinary-fileupload");
			if (htmlOptions.ContainsKey("class"))
			{
				stringBuilder.Append(' ').Append(htmlOptions["class"]);
			}
			foreach (KeyValuePair<string, string> htmlOption in htmlOptions)
			{
				if (!(htmlOption.Key == "class"))
				{
					stringBuilder.Append("' ").Append(htmlOption.Key).Append("='")
						.Append(EncodeApiUrl(htmlOption.Value));
				}
			}
			stringBuilder.Append("'/>");
			return stringBuilder.ToString();
		}

		private static string BuildUserAgent()
		{
			return "CloudinaryDotNet/" + CloudinaryVersion.Full;
		}

		internal static async Task<T> ParseAsync<T>(HttpResponseMessage response) where T : BaseResult
		{
			using Stream s = await response.Content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false);
			return CreateResult<T>(response, s);
		}

		internal static T Parse<T>(HttpResponseMessage response) where T : BaseResult
		{
			using Stream s = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
			return CreateResult<T>(response, s);
		}

		internal virtual Task<T> CallApiAsync<T>(HttpMethod method, string url, BaseParams parameters, FileDescription file, Dictionary<string, string> extraHeaders = null, CancellationToken? cancellationToken = null) where T : BaseResult, new()
		{
			SortedDictionary<string, object> callParams = GetCallParams(method, parameters);
			return CallAndParseAsync<T>(method, url, callParams, file, extraHeaders, cancellationToken);
		}

		internal virtual T CallApi<T>(HttpMethod method, string url, BaseParams parameters, FileDescription file, Dictionary<string, string> extraHeaders = null) where T : BaseResult, new()
		{
			SortedDictionary<string, object> callParams = GetCallParams(method, parameters);
			return CallAndParse<T>(method, url, callParams, file, extraHeaders);
		}

		internal async Task<HttpRequestMessage> PrepareRequestBodyAsync(HttpRequestMessage request, HttpMethod method, SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null, CancellationToken? cancellationToken = null)
		{
			PrePrepareRequestBody(request, method, extraHeaders);
			if (ShouldPrepareContent(method, parameters))
			{
				SetChunkedEncoding(request);
				await PrepareRequestContentAsync(request, parameters, file, extraHeaders, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			return request;
		}

		internal HttpRequestMessage PrepareRequestBody(HttpRequestMessage request, HttpMethod method, SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null)
		{
			PrePrepareRequestBody(request, method, extraHeaders);
			if (ShouldPrepareContent(method, parameters))
			{
				SetChunkedEncoding(request);
				PrepareRequestContent(request, parameters, file, extraHeaders);
			}
			return request;
		}

		internal void FinalizeUploadParameters(IDictionary<string, object> parameters)
		{
			parameters.Add("timestamp", Utils.UnixTimeNowSeconds());
			parameters.Add("signature", SignParameters(parameters));
			parameters.Add("api_key", Account.ApiKey);
		}

		protected static string EncodeApiUrl(string value)
		{
			return WebUtility.UrlEncode(value);
		}

		protected static string ParamsToJson(SortedDictionary<string, object> parameters)
		{
			JsonSerializer jsonSerializer = new JsonSerializer();
			jsonSerializer.Converters.Add(new JavaScriptDateTimeConverter());
			jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
			StringBuilder stringBuilder = new StringBuilder();
			using (JsonTextWriter jsonWriter = new JsonTextWriter(new StringWriter(stringBuilder)))
			{
				jsonSerializer.Serialize(jsonWriter, parameters);
			}
			return stringBuilder.ToString();
		}

		protected static SortedDictionary<string, object> BuildUnsignedUploadParams(string preset, SortedDictionary<string, object> parameters = null)
		{
			if (parameters == null)
			{
				parameters = new SortedDictionary<string, object>();
			}
			parameters.Add("upload_preset", preset);
			parameters.Add("unsigned", true);
			return parameters;
		}

		protected virtual string GetApiCredentials()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", Account.ApiKey, Account.ApiSecret);
		}

		protected void HandleUnsignedParameters(IDictionary<string, object> parameters)
		{
			if (!parameters.ContainsKey("unsigned") || parameters["unsigned"].ToString() == "false")
			{
				FinalizeUploadParameters(parameters);
			}
			else if (parameters.ContainsKey("removeUnsignedParam"))
			{
				parameters.Remove("unsigned");
				parameters.Remove("removeUnsignedParam");
			}
		}

		private static SortedDictionary<string, object> GetCallParams(HttpMethod method, BaseParams parameters)
		{
			parameters?.Check();
			if (method != HttpMethod.PUT && method != HttpMethod.POST)
			{
				return null;
			}
			return parameters?.ToParamsDictionary();
		}

		private static T CreateResult<T>(HttpResponseMessage response, Stream s) where T : BaseResult
		{
			T result = CreateResultFromStream<T>(s, response.StatusCode);
			UpdateResultFromResponse(response, result);
			return result;
		}

		private static T CreateResultFromStream<T>(Stream s, HttpStatusCode statusCode) where T : BaseResult
		{
			try
			{
				using StreamReader reader = new StreamReader(s);
				using JsonTextReader reader2 = new JsonTextReader(reader);
				JToken jToken = JToken.Load(reader2);
				T? val = jToken.ToObject<T>();
				val.JsonObj = jToken;
				return val;
			}
			catch (JsonException innerException)
			{
				throw new Exception($"Failed to deserialize response with status code: {statusCode}", innerException);
			}
		}

		private static void UpdateResultFromResponse<T>(HttpResponseMessage response, T result) where T : BaseResult
		{
			if (response == null)
			{
				return;
			}
			response?.Headers.Where((KeyValuePair<string, IEnumerable<string>> _) => _.Key.StartsWith("X-FeatureRateLimit", StringComparison.Ordinal)).ToList().ForEach(delegate(KeyValuePair<string, IEnumerable<string>> header)
			{
				string s = header.Value.First();
				string key = header.Key;
				if (key.EndsWith("Limit", StringComparison.Ordinal) && long.TryParse(s, out var result2))
				{
					result.Limit = result2;
				}
				if (key.EndsWith("Remaining", StringComparison.Ordinal) && long.TryParse(s, out result2))
				{
					result.Remaining = result2;
				}
				if (key.EndsWith("Reset", StringComparison.Ordinal) && DateTime.TryParse(s, out var result3))
				{
					result.Reset = result3;
				}
			});
			result.StatusCode = response.StatusCode;
		}

		private static bool ShouldPrepareContent(HttpMethod method, object parameters)
		{
			if (method == HttpMethod.POST || method == HttpMethod.PUT)
			{
				return parameters != null;
			}
			return false;
		}

		private static bool IsContentRange(Dictionary<string, string> extraHeaders)
		{
			return extraHeaders?.ContainsKey("Content-Range") ?? false;
		}

		private static Stream GetFileStream(FileDescription file)
		{
			return file.Stream ?? File.OpenRead(file.FilePath);
		}

		private static void SetStreamContent(FileDescription file, Stream stream, MultipartFormDataContent content)
		{
			StreamContent streamContent = new StreamContent(stream);
			streamContent.Headers.Add("Content-Type", "application/octet-stream");
			streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
			{
				Name = "file",
				FileNameStar = file.FileName
			};
			content.Add(streamContent, "file", file.FileName);
		}

		private static void SetContentForRemoteFile(FileDescription file, MultipartFormDataContent content)
		{
			StringContent stringContent = new StringContent(file.FilePath);
			stringContent.Headers.Add("Content-Disposition", string.Format(CultureInfo.InvariantCulture, "form-data; name=\"{0}\"", "file"));
			content.Add(stringContent);
		}

		private static MultipartFormDataContent CreateMultipartContent(SortedDictionary<string, object> parameters)
		{
			MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent("notrandomsequencetouseasboundary");
			foreach (KeyValuePair<string, object> parameter in parameters)
			{
				if (parameter.Value == null)
				{
					continue;
				}
				if (parameter.Value is IEnumerable<string>)
				{
					foreach (string item in (IEnumerable<string>)parameter.Value)
					{
						multipartFormDataContent.Add(new StringContent(item), string.Format(CultureInfo.InvariantCulture, "\"{0}\"", parameter.Key + "[]"));
					}
				}
				else
				{
					multipartFormDataContent.Add(new StringContent(parameter.Value.ToString()), string.Format(CultureInfo.InvariantCulture, "\"{0}\"", parameter.Key));
				}
			}
			return multipartFormDataContent;
		}

		private static StringContent CreateStringContent(SortedDictionary<string, object> parameters)
		{
			return new StringContent(ParamsToJson(parameters), Encoding.UTF8, "application/json");
		}

		private static bool IsStringContent(Dictionary<string, string> extraHeaders)
		{
			if (extraHeaders != null && extraHeaders.TryGetValue("Content-Type", out var value))
			{
				return value == "application/json";
			}
			return false;
		}

		private static Stream WriterStreamFromBegin(StreamWriter writer)
		{
			Stream baseStream = writer.BaseStream;
			baseStream.Seek(0L, SeekOrigin.Begin);
			return baseStream;
		}

		private static StreamWriter SetStreamToStartAndCreateWriter(FileDescription file, Stream stream)
		{
			StreamWriter result = new StreamWriter(new MemoryStream())
			{
				AutoFlush = true
			};
			stream.Seek(file.BytesSent, SeekOrigin.Begin);
			return result;
		}

		private static void SetHeadersAndContent(HttpRequestMessage request, Dictionary<string, string> extraHeaders, HttpContent content)
		{
			if (extraHeaders != null)
			{
				foreach (KeyValuePair<string, string> extraHeader in extraHeaders)
				{
					content.Headers.TryAddWithoutValidation(extraHeader.Key, extraHeader.Value);
				}
			}
			request.Content = content;
		}

		private static void SetHttpMethod(HttpMethod method, HttpRequestMessage req)
		{
			switch (method)
			{
			case HttpMethod.DELETE:
				req.Method = System.Net.Http.HttpMethod.Delete;
				break;
			case HttpMethod.GET:
				req.Method = System.Net.Http.HttpMethod.Get;
				break;
			case HttpMethod.POST:
				req.Method = System.Net.Http.HttpMethod.Post;
				break;
			case HttpMethod.PUT:
				req.Method = System.Net.Http.HttpMethod.Put;
				break;
			default:
				req.Method = System.Net.Http.HttpMethod.Get;
				break;
			}
		}

		private CancellationToken GetDefaultCancellationToken()
		{
			if (Timeout <= 0)
			{
				return CancellationToken.None;
			}
			return new CancellationTokenSource(Timeout).Token;
		}

		private void SetChunkedEncoding(HttpRequestMessage request)
		{
			if (UseChunkedEncoding)
			{
				request.Headers.Add("Transfer-Encoding", "chunked");
			}
		}

		private void PrePrepareRequestBody(HttpRequestMessage request, HttpMethod method, Dictionary<string, string> extraHeaders)
		{
			SetHttpMethod(method, request);
			string value = (string.IsNullOrEmpty(UserPlatform) ? USER_AGENT : string.Format(CultureInfo.InvariantCulture, "{0} {1}", UserPlatform, USER_AGENT));
			request.Headers.Add("User-Agent", value);
			byte[] bytes = Encoding.ASCII.GetBytes(GetApiCredentials());
			request.Headers.Add("Authorization", string.Format(CultureInfo.InvariantCulture, "Basic {0}", Convert.ToBase64String(bytes)));
			if (extraHeaders == null)
			{
				return;
			}
			if (extraHeaders.ContainsKey("Accept"))
			{
				request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(extraHeaders["Accept"]));
				extraHeaders.Remove("Accept");
			}
			foreach (KeyValuePair<string, string> extraHeader in extraHeaders)
			{
				request.Headers.TryAddWithoutValidation(extraHeader.Key, extraHeader.Value);
			}
		}

		private async Task PrepareRequestContentAsync(HttpRequestMessage request, SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null, CancellationToken? cancellationToken = null)
		{
			HandleUnsignedParameters(parameters);
			HttpContent httpContent = ((!IsStringContent(extraHeaders)) ? (await PrepareMultipartFormDataContentAsync(parameters, file, extraHeaders, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)) : CreateStringContent(parameters));
			HttpContent content = httpContent;
			SetHeadersAndContent(request, extraHeaders, content);
		}

		private void PrepareRequestContent(HttpRequestMessage request, SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null)
		{
			HandleUnsignedParameters(parameters);
			HttpContent content = (IsStringContent(extraHeaders) ? CreateStringContent(parameters) : PrepareMultipartFormDataContent(parameters, file, extraHeaders));
			SetHeadersAndContent(request, extraHeaders, content);
		}

		private async Task<HttpContent> PrepareMultipartFormDataContentAsync(SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null, CancellationToken? cancellationToken = null)
		{
			MultipartFormDataContent content = CreateMultipartContent(parameters);
			if (file != null)
			{
				if (file.IsRemote)
				{
					SetContentForRemoteFile(file, content);
				}
				else
				{
					Stream stream = GetFileStream(file);
					if (IsContentRange(extraHeaders))
					{
						stream = await GetRangeFromFileAsync(file, stream, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					SetStreamContent(file, stream, content);
				}
			}
			return content;
		}

		private HttpContent PrepareMultipartFormDataContent(SortedDictionary<string, object> parameters, FileDescription file, Dictionary<string, string> extraHeaders = null)
		{
			MultipartFormDataContent multipartFormDataContent = CreateMultipartContent(parameters);
			if (file != null)
			{
				if (file.IsRemote)
				{
					SetContentForRemoteFile(file, multipartFormDataContent);
				}
				else
				{
					Stream stream = GetFileStream(file);
					if (IsContentRange(extraHeaders))
					{
						stream = GetRangeFromFile(file, stream);
					}
					SetStreamContent(file, stream, multipartFormDataContent);
				}
			}
			return multipartFormDataContent;
		}

		private async Task<Stream> GetRangeFromFileAsync(FileDescription file, Stream stream, CancellationToken? cancellationToken = null)
		{
			StreamWriter writer = SetStreamToStartAndCreateWriter(file, stream);
			long bytesSent = file.BytesSent;
			file.BytesSent = bytesSent + await ReadBytesAsync(writer, stream, file.BufferLength, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return WriterStreamFromBegin(writer);
		}

		private Stream GetRangeFromFile(FileDescription file, Stream stream)
		{
			StreamWriter writer = SetStreamToStartAndCreateWriter(file, stream);
			file.BytesSent += ReadBytes(writer, stream, file.BufferLength);
			return WriterStreamFromBegin(writer);
		}

		private async Task<int> ReadBytesAsync(StreamWriter writer, Stream stream, int length, CancellationToken? cancellationToken = null)
		{
			int bytesSent = 0;
			byte[] buf = new byte[ChunkSize];
			CancellationToken token = cancellationToken ?? CancellationToken.None;
			int cnt = default(int);
			while (true)
			{
				int num;
				bool flag = (num = length - bytesSent) > 0;
				if (flag)
				{
					int num2;
					cnt = (num2 = await stream.ReadAsync(buf, 0, (num > buf.Length) ? buf.Length : num, token).ConfigureAwait(continueOnCapturedContext: false));
					flag = num2 > 0;
				}
				if (!flag)
				{
					break;
				}
				await writer.BaseStream.WriteAsync(buf, 0, cnt, token).ConfigureAwait(continueOnCapturedContext: false);
				bytesSent += cnt;
			}
			return bytesSent;
		}

		private int ReadBytes(StreamWriter writer, Stream stream, int length)
		{
			int num = 0;
			byte[] array = new byte[ChunkSize];
			int num2;
			int num3;
			while ((num2 = length - num) > 0 && (num3 = stream.Read(array, 0, (num2 > array.Length) ? array.Length : num2)) > 0)
			{
				writer.BaseStream.Write(array, 0, num3);
				num += num3;
			}
			return num;
		}
	}
}
