using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CloudinaryDotNet
{
	public class AuthToken
	{
		public static string AUTH_TOKEN_NAME = "__cld_token__";

		public static string UNSAFE_RE = "[ \"#%&\\'\\/:;<=>?@\\[\\\\\\]^`{\\|}~]";

		public static AuthToken NULL_AUTH_TOKEN = new AuthToken().SetNull();

		public string tokenName = AUTH_TOKEN_NAME;

		public string key;

		public long startTime;

		public long expiration;

		public string ip;

		public string acl;

		public long duration;

		internal const string ERROR_ACL_AND_URL_MISSING = "Must provide either acl or url property";

		private bool isNullToken;

		public AuthToken()
		{
		}

		public AuthToken(string key)
		{
			this.key = key;
		}

		public static byte[] HexStringToByteArray(string s)
		{
			int length = s.Length;
			byte[] array = new byte[length / 2];
			for (int i = 0; i < length; i += 2)
			{
				array[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
			}
			return array;
		}

		public AuthToken StartTime(long startTime)
		{
			this.startTime = startTime;
			return this;
		}

		public AuthToken Expiration(long expiration)
		{
			this.expiration = expiration;
			return this;
		}

		public AuthToken Ip(string ip)
		{
			this.ip = ip;
			return this;
		}

		public AuthToken Acl(string acl)
		{
			this.acl = acl;
			return this;
		}

		public AuthToken Duration(long duration)
		{
			this.duration = duration;
			return this;
		}

		public string Generate()
		{
			return Generate(null);
		}

		public string Generate(string url)
		{
			long num = expiration;
			if (num == 0L)
			{
				if (duration <= 0)
				{
					throw new ArgumentException("Must provide either expiration or duration");
				}
				num = ((startTime > 0) ? startTime : Utils.UnixTimeNowSeconds()) + duration;
			}
			List<string> list = new List<string>();
			if (!string.IsNullOrWhiteSpace(ip))
			{
				list.Add(string.Format(CultureInfo.InvariantCulture, "ip={0}", ip));
			}
			if (startTime > 0)
			{
				list.Add(string.Format(CultureInfo.InvariantCulture, "st={0}", startTime.ToString(CultureInfo.InvariantCulture)));
			}
			list.Add(string.Format(CultureInfo.InvariantCulture, "exp={0}", num.ToString(CultureInfo.InvariantCulture)));
			if (string.IsNullOrWhiteSpace(url) && string.IsNullOrWhiteSpace(acl))
			{
				throw new InvalidOperationException("Must provide either acl or url property");
			}
			if (!string.IsNullOrWhiteSpace(acl))
			{
				list.Add(string.Format(CultureInfo.InvariantCulture, "acl={0}", EscapeUrlToLower(acl)));
			}
			List<string> list2 = new List<string>(list);
			if (!string.IsNullOrWhiteSpace(url) && string.IsNullOrWhiteSpace(acl))
			{
				list2.Add(string.Format(CultureInfo.InvariantCulture, "url={0}", EscapeUrlToLower(url)));
			}
			string arg = Digest(string.Join("~", list2));
			list.Add(string.Format(CultureInfo.InvariantCulture, "hmac={0}", arg));
			return tokenName + "=" + string.Join("~", list);
		}

		public AuthToken Copy()
		{
			return new AuthToken(key)
			{
				tokenName = tokenName,
				startTime = startTime,
				expiration = expiration,
				ip = ip,
				acl = acl,
				duration = duration
			};
		}

		public override bool Equals(object o)
		{
			if (o is AuthToken)
			{
				AuthToken authToken = (AuthToken)o;
				if ((!isNullToken || !authToken.isNullToken) && key != null)
				{
					if (key == authToken.key && tokenName == authToken.tokenName && startTime == authToken.startTime && expiration == authToken.expiration && duration == authToken.duration && ((ip == null) ? (authToken.ip == null) : (ip == authToken.ip)))
					{
						if (acl != null)
						{
							return acl == authToken.acl;
						}
						return authToken.acl == null;
					}
					return false;
				}
				return authToken.key == null;
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (isNullToken)
			{
				return 0;
			}
			return new List<string>
			{
				tokenName,
				startTime.ToString(CultureInfo.InvariantCulture),
				expiration.ToString(CultureInfo.InvariantCulture),
				duration.ToString(CultureInfo.InvariantCulture),
				ip,
				acl
			}.GetHashCode();
		}

		protected static string EscapeUrlToLower(string url)
		{
			return new Regex(UNSAFE_RE, RegexOptions.Compiled | RegexOptions.RightToLeft).Replace(url, (Match m) => string.Join(string.Empty, m.Value.Select((char c) => "%" + Convert.ToByte(c).ToString("x2", CultureInfo.InvariantCulture))).ToLowerInvariant());
		}

		private string Digest(string message)
		{
			byte[] array = HexStringToByteArray(key);
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			using HMACSHA256 hMACSHA = new HMACSHA256(array);
			hMACSHA.Initialize();
			return BitConverter.ToString(hMACSHA.ComputeHash(bytes)).Replace("-", string.Empty).ToLowerInvariant();
		}

		private AuthToken SetNull()
		{
			isNullToken = true;
			return this;
		}
	}
}
