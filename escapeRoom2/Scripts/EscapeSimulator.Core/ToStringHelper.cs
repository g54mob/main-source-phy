using System;
using System.Collections.Generic;
using System.Text;

public static class ToStringHelper
{
	private const string NULL = "null";

	private const string EMPTY_COLLECTION = "[]";

	public static string Stringify<T>(T[] array, Func<T, string> itemFormatter)
	{
		if (array == null)
		{
			return "null";
		}
		if (array.Length == 0)
		{
			return "[]";
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine();
		for (int i = 0; i < array.Length; i++)
		{
			T arg = array[i];
			stringBuilder.Append($"[{i}]: {itemFormatter(arg)}");
			if (i < array.Length - 1)
			{
				stringBuilder.Append('\n');
			}
		}
		return IndentMultiline(stringBuilder.ToString());
	}

	public static string Stringify<T>(List<T> list, Func<T, string> itemFormatter)
	{
		if (list == null)
		{
			return "null";
		}
		if (list.Count == 0)
		{
			return "[]";
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine();
		for (int i = 0; i < list.Count; i++)
		{
			T arg = list[i];
			stringBuilder.Append($"[{i}]: {itemFormatter(arg)}");
			if (i < list.Count - 1)
			{
				stringBuilder.Append('\n');
			}
		}
		return IndentMultiline(stringBuilder.ToString());
	}

	public static string Stringify<T>(HashSet<T> set, Func<T, string> itemFormatter)
	{
		if (set == null)
		{
			return "null";
		}
		if (set.Count == 0)
		{
			return "[]";
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine();
		int num = 0;
		foreach (T item in set)
		{
			stringBuilder.Append($"[{num}]: {itemFormatter(item)}");
			if (num < set.Count - 1)
			{
				stringBuilder.Append('\n');
			}
			num++;
		}
		return IndentMultiline(stringBuilder.ToString());
	}

	public static string Stringify<K, V>(Dictionary<K, V> dictionary, Func<K, string> keyFormatter, Func<V, string> valueFormatter)
	{
		if (dictionary == null)
		{
			return "null";
		}
		if (dictionary.Count == 0)
		{
			return "[]";
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine();
		int num = 0;
		foreach (var (arg, arg2) in dictionary)
		{
			stringBuilder.AppendLine("key: " + keyFormatter(arg));
			stringBuilder.Append("value: " + valueFormatter(arg2));
			if (num < dictionary.Count - 1)
			{
				stringBuilder.Append('\n');
			}
			num++;
		}
		return IndentMultiline(stringBuilder.ToString());
	}

	public static string Stringify<T>(T? nullable, Func<T, string> valueFormatter) where T : struct
	{
		if (!nullable.HasValue)
		{
			return "null";
		}
		return valueFormatter(nullable.Value);
	}

	public static string Stringify(string value)
	{
		if (value == null)
		{
			return "null";
		}
		return "\"" + value + "\"";
	}

	public static string Stringify(IReadWrite value)
	{
		if (value == null)
		{
			return "null";
		}
		return "\n" + IndentMultiline(value.ToString());
	}

	private static string IndentMultiline(string text, int indentLevel = 1)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		string[] array = text.Split('\n');
		string text2 = new string(' ', indentLevel * 4);
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = text2 + array[i].TrimEnd('\r');
		}
		return string.Join("\n", array);
	}
}
