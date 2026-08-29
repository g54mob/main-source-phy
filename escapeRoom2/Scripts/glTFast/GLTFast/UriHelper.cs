using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GLTFast
{
	internal static class UriHelper
	{
		public static Uri GetBaseUri(Uri uri)
		{
			if (uri == null)
			{
				return null;
			}
			if (!uri.IsAbsoluteUri)
			{
				return new Uri(Path.GetDirectoryName(uri.OriginalString) ?? "", UriKind.Relative);
			}
			string originalString = uri.OriginalString;
			int num = originalString.LastIndexOfAny(new char[2] { '/', '\\' });
			if (num >= 0)
			{
				return new Uri(originalString.Substring(0, num + 1));
			}
			return new Uri("", UriKind.Relative);
		}

		public static Uri GetUriString(string uri, Uri baseUri)
		{
			uri = Uri.UnescapeDataString(uri);
			if (Uri.TryCreate(uri, UriKind.Absolute, out var result))
			{
				return result;
			}
			if (baseUri != null)
			{
				uri = RemoveDotSegments(uri, out var parentLevels);
				if (baseUri.IsAbsoluteUri)
				{
					for (int i = 0; i < parentLevels; i++)
					{
						baseUri = new Uri(baseUri, "..");
					}
					return new Uri(Combine(baseUri.OriginalString, uri));
				}
				string text = baseUri.OriginalString;
				for (int j = 0; j < parentLevels; j++)
				{
					text = Path.GetDirectoryName(text);
					if (string.IsNullOrEmpty(text))
					{
						baseUri = new Uri("", UriKind.Relative);
						break;
					}
					baseUri = new Uri(text, UriKind.Relative);
				}
				return new Uri(Path.Combine(baseUri.OriginalString, uri), UriKind.Relative);
			}
			return new Uri(uri, UriKind.RelativeOrAbsolute);
		}

		public static string RemoveDotSegments(string uri, out int parentLevels)
		{
			List<string> list = new List<string>();
			int num = 0;
			parentLevels = 0;
			while (true)
			{
				int num2 = uri.IndexOfAny(new char[2]
				{
					Path.DirectorySeparatorChar,
					Path.AltDirectorySeparatorChar
				}, num);
				bool num3 = num2 >= 0;
				int num4 = (num3 ? (num2 - num) : (uri.Length - num));
				if (num4 > 0)
				{
					string text = uri.Substring(num, num4);
					if (text == "..")
					{
						if (list.Count > 0)
						{
							list.RemoveAt(list.Count - 1);
						}
						else
						{
							parentLevels++;
						}
					}
					else if (text != ".")
					{
						list.Add(text);
					}
				}
				if (!num3)
				{
					break;
				}
				num = num2 + 1;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string item in list)
			{
				if (!flag)
				{
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
				stringBuilder.Append(item);
				flag = false;
			}
			return stringBuilder.ToString();
		}

		public static bool? IsGltfBinary(Uri uri)
		{
			string text = (uri.IsAbsoluteUri ? uri.LocalPath : uri.OriginalString);
			if (text.LastIndexOf('.', text.Length - 1, Mathf.Min(5, text.Length)) < 0)
			{
				return null;
			}
			if (text.EndsWith(".glb", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (text.EndsWith(".gltf", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			return null;
		}

		internal static ImageFormat GetImageFormatFromUri(string uri)
		{
			if (string.IsNullOrEmpty(uri))
			{
				return ImageFormat.Unknown;
			}
			int num = uri.LastIndexOf('?');
			if (num < 0)
			{
				num = uri.Length;
			}
			int num2 = uri.LastIndexOf('.', num - 1, Mathf.Min(5, num));
			if (num2 < 0)
			{
				return ImageFormat.Unknown;
			}
			string text = uri.Substring(num2 + 1, num - num2 - 1);
			if (text.Equals("png", StringComparison.OrdinalIgnoreCase))
			{
				return ImageFormat.PNG;
			}
			if (text.Equals("jpg", StringComparison.OrdinalIgnoreCase) || text.Equals("jpeg", StringComparison.OrdinalIgnoreCase))
			{
				return ImageFormat.Jpeg;
			}
			if (text.Equals("ktx", StringComparison.OrdinalIgnoreCase) || text.Equals("ktx2", StringComparison.OrdinalIgnoreCase))
			{
				return ImageFormat.Ktx;
			}
			return ImageFormat.Unknown;
		}

		internal static string Combine(string baseUri, string uri)
		{
			int num = baseUri.LastIndexOfAny(new char[2] { '/', '\\' });
			string text = ((uri.IndexOfAny(new char[2] { '/', '\\' }, 0, 1) == 0) ? uri.Substring(1) : uri);
			if (num > 0)
			{
				if (num != baseUri.Length - 1)
				{
					return $"{baseUri}{baseUri[num]}{text}";
				}
				return baseUri + text;
			}
			return baseUri + "/" + text;
		}
	}
}
