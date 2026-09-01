using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using CloudinaryDotNet.Core;

namespace CloudinaryDotNet
{
	public class Url : CloudinaryDotNet.Core.ICloneable
	{
		public static readonly VideoSource[] DefaultVideoSources = new VideoSource[4]
		{
			new VideoSource
			{
				Type = "mp4",
				Codecs = new string[1] { "hev1" },
				Transformation = new Transformation().VideoCodec("h265")
			},
			new VideoSource
			{
				Type = "webm",
				Codecs = new string[1] { "vp9" },
				Transformation = new Transformation().VideoCodec("vp9")
			},
			new VideoSource
			{
				Type = "mp4",
				Transformation = new Transformation().VideoCodec("auto")
			},
			new VideoSource
			{
				Type = "webm",
				Transformation = new Transformation().VideoCodec("auto")
			}
		};

		protected const string CL_BLANK = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7";

		protected static readonly string[] DEFAULT_VIDEO_SOURCE_TYPES = new string[3] { "webm", "mp4", "ogv" };

		protected static readonly Regex VIDEO_EXTENSION_RE = new Regex("\\.(" + string.Join("|", DEFAULT_VIDEO_SOURCE_TYPES) + ")$", RegexOptions.Compiled);

		protected ISignProvider m_signProvider;

		protected AuthToken m_AuthToken;

		protected string m_cloudName;

		protected string m_cloudinaryAddr = "res.cloudinary.com";

		protected string m_apiVersion;

		protected bool m_shorten;

		protected bool m_secure;

		protected bool m_usePrivateCdn;

		protected bool m_signed;

		protected bool m_longUrlSignature;

		protected bool m_useRootPath;

		protected string m_suffix;

		protected string m_privateCdn;

		protected string m_version;

		protected bool m_forceVersion;

		protected string m_cName;

		protected string m_source;

		protected string m_fallbackContent;

		protected bool m_useSubDomain;

		protected Dictionary<string, Transformation> m_sourceTransforms;

		protected List<string> m_customParts = new List<string>();

		protected VideoSource[] m_videoSources;

		protected Transformation m_posterTransformation;

		protected string m_posterSource;

		protected Url m_posterUrl;

		protected string[] m_sourceTypes;

		protected string m_action = string.Empty;

		protected string m_resourceType = string.Empty;

		protected Transformation m_transformation;

		public string FormatValue { get; set; }

		public Transformation Transformation
		{
			get
			{
				if (m_transformation == null)
				{
					m_transformation = new Transformation();
				}
				return m_transformation;
			}
		}

		public Url(string cloudName)
		{
			m_cloudName = cloudName;
			m_longUrlSignature = CloudinaryConfiguration.LongUrlSignature;
		}

		public Url(string cloudName, ISignProvider signProvider)
			: this(cloudName)
		{
			m_signProvider = signProvider;
		}

		public Url Shorten(bool shorten)
		{
			m_shorten = shorten;
			return this;
		}

		public Url CloudinaryAddr(string cloudinaryAddr)
		{
			m_cloudinaryAddr = cloudinaryAddr;
			return this;
		}

		public Url CloudName(string cloudName)
		{
			m_cloudName = cloudName;
			return this;
		}

		public Url Add(string part)
		{
			if (!string.IsNullOrEmpty(part))
			{
				m_customParts.Add(Uri.EscapeUriString(part));
			}
			return this;
		}

		public Url VideoSources(params VideoSource[] videoSources)
		{
			if (videoSources != null && videoSources.Length != 0)
			{
				m_videoSources = videoSources;
			}
			return this;
		}

		public Url Action(string action)
		{
			m_action = action;
			return this;
		}

		public Url ApiVersion(string apiVersion)
		{
			m_apiVersion = apiVersion;
			return this;
		}

		public Url Version(string version)
		{
			m_version = version;
			return this;
		}

		public Url ForceVersion(bool forceVersion = true)
		{
			m_forceVersion = forceVersion;
			return this;
		}

		public Url AuthToken(AuthToken authToken)
		{
			if (m_AuthToken == null)
			{
				m_AuthToken = authToken;
			}
			return this;
		}

		public Url Source(string source)
		{
			m_source = source;
			return this;
		}

		public Url SourceTypes(params string[] sourceTypes)
		{
			m_sourceTypes = sourceTypes;
			return this;
		}

		public Url Signed(bool signed)
		{
			m_signed = signed;
			return this;
		}

		public Url LongUrlSignature(bool isLong)
		{
			m_longUrlSignature = isLong;
			return this;
		}

		public Url ResourceType(string resourceType)
		{
			m_resourceType = resourceType;
			return this;
		}

		public Url Format(string format)
		{
			FormatValue = format;
			return this;
		}

		public Url SecureDistribution(string privateCdn)
		{
			m_privateCdn = privateCdn;
			return this;
		}

		public Url CName(string cName)
		{
			m_cName = cName;
			return this;
		}

		public Url Transform(Transformation transformation)
		{
			m_transformation = transformation;
			return this;
		}

		public Url Secure(bool secure = true)
		{
			m_secure = secure;
			return this;
		}

		public Url PrivateCdn(bool usePrivateCdn)
		{
			m_usePrivateCdn = usePrivateCdn;
			return this;
		}

		public Url CSubDomain(bool useSubDomain)
		{
			m_useSubDomain = useSubDomain;
			return this;
		}

		public Url UseRootPath(bool useRootPath)
		{
			m_useRootPath = useRootPath;
			return this;
		}

		public Url FallbackContent(string fallbackContent)
		{
			m_fallbackContent = fallbackContent;
			return this;
		}

		public Url Suffix(string suffix)
		{
			m_suffix = suffix;
			return this;
		}

		public Url SourceTransformationFor(string source, Transformation transform)
		{
			if (m_sourceTransforms == null)
			{
				m_sourceTransforms = new Dictionary<string, Transformation>();
			}
			m_sourceTransforms.Add(source, transform);
			return this;
		}

		public Url PosterTransform(Transformation transformation)
		{
			m_posterTransformation = transformation;
			return this;
		}

		public Url PosterSource(string source)
		{
			m_posterSource = source;
			return this;
		}

		public Url PosterUrl(Url url)
		{
			m_posterUrl = url;
			return this;
		}

		public Url Poster(object poster)
		{
			if (poster is string)
			{
				return PosterSource((string)poster);
			}
			if (poster is Url)
			{
				return PosterUrl((Url)poster);
			}
			if (poster is Transformation)
			{
				return PosterTransform((Transformation)poster);
			}
			if (poster == null || (poster is bool && !(bool)poster))
			{
				PosterSource(string.Empty);
				PosterUrl(null);
				PosterTransform(null);
			}
			return this;
		}

		public string BuildSpriteCss(string source)
		{
			m_action = "sprite";
			if (!source.EndsWith(".css", StringComparison.Ordinal))
			{
				FormatValue = "css";
			}
			return BuildUrl(source);
		}

		public string BuildImageTag(string source, params string[] keyValuePairs)
		{
			return BuildImageTag(source, new StringDictionary(keyValuePairs));
		}

		public string BuildImageTag(string source, StringDictionary dict = null)
		{
			if (dict == null)
			{
				dict = new StringDictionary();
			}
			string value = BuildUrl(source);
			if (!string.IsNullOrEmpty(Transformation.HtmlWidth))
			{
				dict.Add("width", Transformation.HtmlWidth);
			}
			if (!string.IsNullOrEmpty(Transformation.HtmlHeight))
			{
				dict.Add("height", Transformation.HtmlHeight);
			}
			if (Transformation.HiDpi || Transformation.IsResponsive)
			{
				string text = (Transformation.IsResponsive ? "cld-responsive" : "cld-hidpi");
				string text2 = dict["class"];
				dict["class"] = ((text2 == null) ? text : (text2 + " " + text));
				dict.Add("data-src", value);
				string text3 = dict.Remove("responsive_placeholder");
				if (text3 == "blank")
				{
					text3 = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7";
				}
				value = text3;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<img");
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append(" src=\"").Append(value).Append('"');
			}
			foreach (KeyValuePair<string, string> item in dict)
			{
				stringBuilder.Append(' ').Append(item.Key).Append("=\"")
					.Append(WebUtility.HtmlEncode(item.Value))
					.Append('"');
			}
			stringBuilder.Append("/>");
			return stringBuilder.ToString();
		}

		public string BuildVideoTag(string source, params string[] keyValuePairs)
		{
			return BuildVideoTag(source, new StringDictionary(keyValuePairs));
		}

		public string BuildVideoTag(string source, StringDictionary dict = null)
		{
			if (dict == null)
			{
				dict = new StringDictionary();
			}
			source = VIDEO_EXTENSION_RE.Replace(source, string.Empty, 1);
			if (string.IsNullOrEmpty(m_resourceType))
			{
				m_resourceType = "video";
			}
			string value = FinalizePosterUrl(source);
			if (!string.IsNullOrEmpty(value))
			{
				dict.Add("poster", value);
			}
			List<string> videoSourceTags = GetVideoSourceTags(source);
			StringBuilder sb = new StringBuilder("<video");
			bool flag = videoSourceTags.Count > 1;
			if (flag)
			{
				BuildUrl(source);
			}
			else
			{
				string[] sourceTypes = GetSourceTypes();
				string value2 = BuildUrl(source + "." + sourceTypes[0]);
				dict.Add("src", value2);
			}
			if (dict.ContainsKey("html_height"))
			{
				dict["height"] = dict.Remove("html_height");
			}
			else if (Transformation.HtmlHeight != null)
			{
				dict["height"] = Transformation.HtmlHeight;
			}
			if (dict.ContainsKey("html_width"))
			{
				dict["width"] = dict.Remove("html_width");
			}
			else if (Transformation.HtmlWidth != null)
			{
				dict["width"] = Transformation.HtmlWidth;
			}
			bool sort = dict.Sort;
			dict.Sort = true;
			foreach (KeyValuePair<string, string> item in dict)
			{
				sb.Append(' ').Append(item.Key);
				if (item.Value != null)
				{
					sb.Append("='").Append(item.Value).Append('\'');
				}
			}
			dict.Sort = sort;
			sb.Append('>');
			if (flag)
			{
				videoSourceTags.ForEach(delegate(string t)
				{
					sb.Append(t);
				});
			}
			if (!string.IsNullOrEmpty(m_fallbackContent))
			{
				sb.Append(m_fallbackContent);
			}
			sb.Append("</video>");
			return sb.ToString();
		}

		public string BuildUrl()
		{
			return BuildUrl(null);
		}

		public string BuildUrl(string source)
		{
			if (string.IsNullOrEmpty(m_cloudName))
			{
				throw new ArgumentException("cloudName must be specified!");
			}
			if (source == null)
			{
				source = m_source;
			}
			if (source == null)
			{
				source = string.Empty;
			}
			if (Regex.IsMatch(source.ToLowerInvariant(), "^https?:/.*") && (m_action == "upload" || m_action == "asset"))
			{
				return source;
			}
			if (m_action == "fetch" && !string.IsNullOrEmpty(FormatValue))
			{
				Transformation.FetchFormat(FormatValue);
				FormatValue = null;
			}
			string text = Transformation.Generate();
			CSource cSource = UpdateSource(source);
			bool sharedDomain;
			string prefix = GetPrefix(cSource.Source, out sharedDomain);
			List<string> list = new List<string>(new string[1] { prefix });
			if (!string.IsNullOrEmpty(m_apiVersion))
			{
				list.Add(m_apiVersion);
				list.Add(m_cloudName);
			}
			else if (sharedDomain)
			{
				list.Add(m_cloudName);
			}
			UpdateAction();
			list.Add(m_resourceType);
			list.Add(m_action);
			list.AddRange(m_customParts);
			if (m_forceVersion && cSource.SourceToSign.Contains("/") && !Regex.IsMatch(cSource.SourceToSign, "^v[0-9]+/") && !Regex.IsMatch(cSource.SourceToSign, "https?:/.*") && string.IsNullOrEmpty(m_version))
			{
				m_version = "1";
			}
			string item = (string.IsNullOrEmpty(m_version) ? string.Empty : ("v" + m_version));
			if (m_signed && m_AuthToken == null && CloudinaryConfiguration.AuthToken == null)
			{
				if (m_signProvider == null)
				{
					throw new NullReferenceException("Reference to ISignProvider-compatible object must be provided in order to sign URI!");
				}
				string input = string.Join("/", text, cSource.SourceToSign);
				input = Regex.Replace(input, "^/+", string.Empty);
				input = Regex.Replace(input, "([^:])/{2,}", "$1/");
				input = Regex.Replace(input, "/$", string.Empty);
				input = m_signProvider.SignUriPart(input, m_longUrlSignature);
				list.Add(input);
			}
			list.Add(text);
			list.Add(item);
			list.Add(cSource.Source);
			string input2 = string.Join("/", list.ToArray());
			input2 = Regex.Replace(input2, "([^:])/{2,}", "$1/");
			input2 = Regex.Replace(input2, "/$", string.Empty);
			if (m_signed && (m_AuthToken != null || CloudinaryConfiguration.AuthToken != null))
			{
				AuthToken authToken = ((m_AuthToken != null) ? m_AuthToken : ((CloudinaryConfiguration.AuthToken != null) ? CloudinaryConfiguration.AuthToken : null));
				if (authToken != null && !object.Equals(authToken, CloudinaryDotNet.AuthToken.NULL_AUTH_TOKEN))
				{
					string absolutePath = new Uri(input2).AbsolutePath;
					string text2 = authToken.Generate(absolutePath);
					input2 = input2 + "?" + text2;
				}
			}
			return input2;
		}

		public Url Clone()
		{
			Url url = (Url)MemberwiseClone();
			if (m_transformation != null)
			{
				url.m_transformation = m_transformation.Clone();
			}
			if (m_posterTransformation != null)
			{
				url.m_posterTransformation = m_posterTransformation.Clone();
			}
			if (m_posterUrl != null)
			{
				url.m_posterUrl = m_posterUrl.Clone();
			}
			if (m_sourceTypes != null)
			{
				url.m_sourceTypes = new string[m_sourceTypes.Length];
				Array.Copy(m_sourceTypes, url.m_sourceTypes, m_sourceTypes.Length);
			}
			if (m_sourceTransforms != null)
			{
				url.m_sourceTransforms = new Dictionary<string, Transformation>();
				foreach (KeyValuePair<string, Transformation> sourceTransform in m_sourceTransforms)
				{
					url.m_sourceTransforms.Add(sourceTransform.Key, sourceTransform.Value.Clone());
				}
			}
			url.m_customParts = new List<string>(m_customParts);
			return url;
		}

		object CloudinaryDotNet.Core.ICloneable.Clone()
		{
			return Clone();
		}

		private static string VideoMimeType(string sourceType, params string[] codecs)
		{
			sourceType = ((sourceType == "ogv") ? "ogg" : sourceType);
			if (string.IsNullOrEmpty(sourceType))
			{
				return string.Empty;
			}
			if (codecs == null || codecs.Length == 0)
			{
				return "video/" + sourceType;
			}
			string text = string.Join(", ", codecs.Where((string c) => !string.IsNullOrEmpty(c)));
			string text2 = ((!string.IsNullOrEmpty(text)) ? ("; codecs=" + text) : string.Empty);
			return "video/" + sourceType + text2;
		}

		private static void AppendTransformation(Url url, Transformation transform)
		{
			if (url.m_transformation == null)
			{
				url.Transform(transform);
				return;
			}
			url.m_transformation.Chain();
			transform.NestedTransforms.AddRange(url.m_transformation.NestedTransforms);
			url.Transform(transform);
		}

		private static void MergeUrlTransformation(Url url, Transformation transformationSrc)
		{
			if (transformationSrc == null)
			{
				return;
			}
			if (url.m_transformation == null)
			{
				url.Transform(transformationSrc);
				return;
			}
			foreach (KeyValuePair<string, object> item in transformationSrc.Params)
			{
				url.m_transformation.Add(item.Key, item.Value);
			}
		}

		private static string Shard(string input)
		{
			return ((Crc32.ComputeChecksum(Encoding.UTF8.GetBytes(input)) % 5 + 5) % 5 + 1).ToString(CultureInfo.InvariantCulture);
		}

		private static string Decode(string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			int num = 0;
			while (num < input.Length)
			{
				int num2 = input.IndexOf('%', num);
				if (num2 == -1)
				{
					stringBuilder.Append(input.Substring(num));
					num = input.Length;
					continue;
				}
				stringBuilder.Append(input.Substring(num, num2 - num));
				char value = (char)short.Parse(input.Substring(num2 + 1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				stringBuilder.Append(value);
				num = num2 + 3;
			}
			return stringBuilder.ToString();
		}

		private static string Encode(string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			foreach (char c in input)
			{
				if (!IsSafe(c))
				{
					stringBuilder.Append('%');
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", (short)c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		private static bool IsSafe(char ch)
		{
			if (ch >= '0' && ch <= '9')
			{
				return true;
			}
			if (ch >= 'A' && ch <= 'Z')
			{
				return true;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return true;
			}
			return "/:-_.*".IndexOf(ch) >= 0;
		}

		private string[] GetSourceTypes()
		{
			if (m_sourceTypes != null && m_sourceTypes.Length != 0)
			{
				return m_sourceTypes;
			}
			return DEFAULT_VIDEO_SOURCE_TYPES;
		}

		private List<string> GetVideoSourceTags(string source)
		{
			if (m_videoSources != null && m_videoSources.Length != 0)
			{
				return m_videoSources.Select((VideoSource x) => GetSourceTag(source, x.Type, x.Codecs, x.Transformation)).ToList();
			}
			return (from x in GetSourceTypes()
				select GetSourceTag(source, x)).ToList();
		}

		private string GetSourceTag(string source, string sourceType, string[] codecs = null, Transformation transformation = null)
		{
			Url url = Clone();
			MergeUrlTransformation(url, transformation);
			if (m_sourceTransforms != null && m_sourceTransforms.TryGetValue(sourceType, out var value) && value != null)
			{
				AppendTransformation(url, value.Clone());
			}
			string text = url.Format(sourceType).BuildUrl(source);
			return "<source src='" + text + "' type='" + VideoMimeType(sourceType, codecs) + "'>";
		}

		private string FinalizePosterUrl(string source)
		{
			string result = null;
			if (m_posterUrl != null)
			{
				result = m_posterUrl.BuildUrl();
			}
			else if (m_posterTransformation != null)
			{
				result = Clone().Format("jpg").Transform(m_posterTransformation.Clone()).BuildUrl(source);
			}
			else if (m_posterSource != null)
			{
				if (!string.IsNullOrEmpty(m_posterSource))
				{
					result = Clone().Format("jpg").BuildUrl(m_posterSource);
				}
			}
			else
			{
				result = Clone().Format("jpg").BuildUrl(source);
			}
			return result;
		}

		private CSource UpdateSource(string source)
		{
			CSource cSource = null;
			if (Regex.IsMatch(source.ToLowerInvariant(), "^https?:/.*"))
			{
				cSource = new CSource(Encode(source));
			}
			else
			{
				cSource = new CSource(Encode(Decode(source)));
				if (!string.IsNullOrEmpty(m_suffix))
				{
					if (Regex.IsMatch(m_suffix, "[\\./]"))
					{
						throw new ArgumentException("Suffix should not include . or /!");
					}
					CSource cSource2 = cSource;
					cSource2.Source = cSource2.Source + "/" + m_suffix;
				}
				if (!string.IsNullOrEmpty(FormatValue))
				{
					cSource += "." + FormatValue;
				}
			}
			return cSource;
		}

		private string GetPrefix(string source, out bool sharedDomain)
		{
			sharedDomain = !m_usePrivateCdn;
			if (Regex.IsMatch(m_cloudinaryAddr.ToLowerInvariant(), "^https?:/.*"))
			{
				return m_cloudinaryAddr;
			}
			string text = m_privateCdn;
			if (m_secure)
			{
				if (string.IsNullOrEmpty(text) || text == "cloudinary-a.akamaihd.net")
				{
					text = (m_usePrivateCdn ? (m_cloudName + "-res.cloudinary.com") : "res.cloudinary.com");
				}
				sharedDomain |= text == "res.cloudinary.com";
				if (sharedDomain && m_useSubDomain)
				{
					text = text.Replace("res.cloudinary.com", "res-" + Shard(source) + ".cloudinary.com");
				}
				return string.Format(CultureInfo.InvariantCulture, "https://{0}", text);
			}
			if (m_cName != null)
			{
				string text2 = (m_useSubDomain ? ("a" + Shard(source) + ".") : string.Empty);
				return "http://" + text2 + m_cName;
			}
			string text3 = (m_useSubDomain ? ("-" + Shard(source)) : string.Empty);
			string text4 = (m_usePrivateCdn ? (m_cloudName + "-") : string.Empty) + "res" + text3 + ".cloudinary.com";
			return "http://" + text4;
		}

		private void UpdateAction()
		{
			if (!string.IsNullOrEmpty(m_suffix))
			{
				if (m_resourceType == "image" && m_action == "upload")
				{
					m_resourceType = "images";
					m_action = null;
				}
				else if (m_resourceType == "image" && m_action == "private")
				{
					m_resourceType = "private_images";
					m_action = null;
				}
				else if (m_resourceType == "image" && m_action == "authenticated")
				{
					m_resourceType = "authenticated_images";
					m_action = null;
				}
				else if (m_resourceType == "video" && m_action == "upload")
				{
					m_resourceType = "videos";
					m_action = null;
				}
				else
				{
					if (!(m_resourceType == "raw") || !(m_action == "upload"))
					{
						throw new NotSupportedException("URL Suffix only supported for image/upload, image/private, image/authenticated, video/upload and raw/upload");
					}
					m_resourceType = "files";
					m_action = null;
				}
			}
			if (m_useRootPath)
			{
				if ((!(m_resourceType == "image") || !(m_action == "upload")) && (!(m_resourceType == "images") || !string.IsNullOrEmpty(m_action)))
				{
					throw new NotSupportedException("Root path only supported for image/upload!");
				}
				m_resourceType = string.Empty;
				m_action = string.Empty;
			}
			if (m_shorten && m_resourceType == "image" && m_action == "upload")
			{
				m_resourceType = string.Empty;
				m_action = "iu";
			}
		}
	}
}
