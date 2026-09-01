using System;
using System.IO;
using System.Linq;

public static class PathHelper
{
	private static readonly char[] _filePathSeparators = new char[3] { '/', '\\', ':' };

	private static readonly char[] _uriSeparators = new char[1] { '/' };

	public static string GetExtension(this string path)
	{
		return path.GetExtensionInternal(_filePathSeparators);
	}

	public static string GetExtension(this Uri uri)
	{
		return uri.LocalPath.GetExtensionInternal(_uriSeparators);
	}

	public static string GetExtension(this string path, char[] separators)
	{
		if (separators != null && separators.Contains('.'))
		{
			throw new ArgumentException("separators can't contain '.'");
		}
		return path.GetExtensionInternal(separators);
	}

	private static string GetExtensionInternal(this string path, char[] separators)
	{
		if (path == null)
		{
			return null;
		}
		int length = path.Length;
		for (int num = length - 1; num >= 0; num--)
		{
			char c = path[num];
			if (c == '.')
			{
				return (num == length - 1) ? string.Empty : path.Substring(num, length - num);
			}
			if (separators != null && separators.Contains(c))
			{
				break;
			}
		}
		return string.Empty;
	}

	public static string GetFileName(this string path)
	{
		return GetFileNameInternal(path, _filePathSeparators);
	}

	public static string GetFileName(this Uri uri)
	{
		return GetFileNameInternal(uri.LocalPath, _uriSeparators);
	}

	public static string GetFileName(this string path, char[] separators)
	{
		if (separators != null && separators.Contains('.'))
		{
			throw new ArgumentException("separators can't contain '.'");
		}
		return GetFileNameInternal(path, separators);
	}

	private static string GetFileNameInternal(string path, char[] separators)
	{
		if (path != null)
		{
			int length = path.Length;
			for (int num = length - 1; num >= 0; num--)
			{
				char value = path[num];
				if (separators.Contains(value))
				{
					return path.Substring(num + 1, length - num - 1);
				}
			}
		}
		return path;
	}

	public static string RemoveInvalidChars(string filename, char? replacedLetter = null)
	{
		if (filename == null)
		{
			return null;
		}
		char[] invalidChars = Path.GetInvalidFileNameChars();
		if (!replacedLetter.HasValue)
		{
			return new string(filename.Where((char x) => !invalidChars.Contains(x)).ToArray());
		}
		return new string(filename.Select((char x) => (!invalidChars.Contains(x)) ? x : replacedLetter.Value).ToArray());
	}
}
