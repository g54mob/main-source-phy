using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CloudinaryDotNet
{
	internal static class Utils
	{
		internal static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static DateTime FromUnixTimeSeconds(long unixTime)
		{
			DateTime epoch = Epoch;
			return epoch.AddSeconds(unixTime);
		}

		internal static long ToUnixTimeSeconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - Epoch).TotalSeconds);
		}

		internal static long UnixTimeNowSeconds()
		{
			return ToUnixTimeSeconds(DateTime.UtcNow);
		}

		internal static string SafeJoin(string separator, IEnumerable<string> items)
		{
			return string.Join(separator, items.Select((string item) => Regex.Replace(item, "([" + separator + "])", "\\$1")));
		}

		internal static bool IsRemoteFile(string filePath)
		{
			return Regex.IsMatch(filePath, "^((ftp|https?|s3|gs):.*)|data:([\\w-]+/[\\w-]+(\\+[\\w-]+)?)?(;[\\w-]+=[\\w-]+)*;base64,([a-zA-Z0-9/+\\n=]+)");
		}

		internal static string Encode(string value)
		{
			return Uri.EscapeUriString(value);
		}

		internal static string EncodeUrlSafe(string s)
		{
			return EncodeUrlSafe(Encoding.UTF8.GetBytes(s));
		}

		internal static string EncodeUrlSafe(byte[] bytes)
		{
			return Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
		}

		internal static byte[] ComputeHash(string s, SignatureAlgorithm signatureAlgorithm = SignatureAlgorithm.SHA1)
		{
			if (signatureAlgorithm == SignatureAlgorithm.SHA256)
			{
				using (SHA256 sHA = SHA256.Create())
				{
					return sHA.ComputeHash(Encoding.UTF8.GetBytes(s));
				}
			}
			using SHA1 sHA2 = SHA1.Create();
			return sHA2.ComputeHash(Encoding.UTF8.GetBytes(s));
		}

		internal static string ComputeHexHash(string s, SignatureAlgorithm signatureAlgorithm = SignatureAlgorithm.SHA1)
		{
			byte[] array = ComputeHash(s, signatureAlgorithm);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		internal static Dictionary<string, string> PrepareJsonHeaders()
		{
			return new Dictionary<string, string> { { "Content-Type", "application/json" } };
		}

		internal static void ShouldBeSpecified(Expression<Func<object>> propertyExpr)
		{
			CheckProperty(propertyExpr, (object val) => val == null, "must be specified");
		}

		internal static void ShouldBeSpecified<T>(Expression<Func<T?>> propertyExpr) where T : struct
		{
			CheckProperty(propertyExpr, (T? val) => !val.HasValue, "must be specified");
		}

		internal static void ShouldNotBeSpecified(Expression<Func<object>> propertyExpr)
		{
			CheckProperty(propertyExpr, (object val) => val != null, "must not be specified");
		}

		internal static void ShouldNotBeEmpty(Expression<Func<string>> propertyExpr, string message = "must not be empty")
		{
			CheckProperty(propertyExpr, string.IsNullOrEmpty, message);
		}

		internal static void ShouldNotBeEmpty<TP>(Expression<Func<List<TP>>> propertyExpr)
		{
			List<TP> list = propertyExpr.Compile()();
			if (list == null || !list.Any())
			{
				throw new ArgumentException(GetPropertyName(propertyExpr.Body) + " must not be empty");
			}
		}

		private static void CheckProperty<T>(Expression<Func<T>> propertyExpr, Func<T, bool> condition, string message = null)
		{
			T arg = propertyExpr.Compile()();
			if (condition(arg))
			{
				throw new ArgumentException(string.IsNullOrEmpty(message) ? (GetPropertyName(propertyExpr.Body) ?? "") : (GetPropertyName(propertyExpr.Body) + " " + message));
			}
		}

		private static string GetPropertyName(System.Linq.Expressions.Expression propertyExpr)
		{
			if (!(propertyExpr is MemberExpression memberExpression))
			{
				if (propertyExpr is UnaryExpression unaryExpression)
				{
					return ((MemberExpression)unaryExpression.Operand).Member.Name;
				}
				return string.Empty;
			}
			return memberExpression.Member.Name;
		}
	}
}
