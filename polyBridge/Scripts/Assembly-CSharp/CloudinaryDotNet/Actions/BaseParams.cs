using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CloudinaryDotNet.Core;

namespace CloudinaryDotNet.Actions
{
	public abstract class BaseParams
	{
		private SortedDictionary<string, object> customParams = new SortedDictionary<string, object>();

		public abstract void Check();

		public virtual BaseParams Copy()
		{
			return (BaseParams)MemberwiseClone();
		}

		public virtual SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>(customParams);
			AddParamsToDictionary(sortedDictionary);
			return sortedDictionary;
		}

		public void AddCustomParam(string key, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				customParams.Add(key, value);
			}
		}

		protected static void AddParam(SortedDictionary<string, object> dict, string key, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				dict.Add(key, value);
			}
		}

		protected static void AddParam(SortedDictionary<string, object> dict, string key, DateTime value)
		{
			if (value != DateTime.MinValue)
			{
				dict.Add(key, value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
			}
		}

		protected static void AddParam(SortedDictionary<string, object> dict, string key, float value)
		{
			dict.Add(key, value.ToString(CultureInfo.InvariantCulture));
		}

		protected static void AddParam(SortedDictionary<string, object> dict, string key, long value)
		{
			dict.Add(key, value.ToString(CultureInfo.InvariantCulture));
		}

		protected static void AddParam(SortedDictionary<string, object> dict, string key, IEnumerable<string> value)
		{
			if (value != null)
			{
				dict.Add(key, value);
			}
		}

		protected static void AddParam(SortedDictionary<string, object> dict, string key, bool value)
		{
			dict.Add(key, value ? "true" : "false");
		}

		protected static void AddParam(SortedDictionary<string, object> dict, string key, bool? value)
		{
			if (value.HasValue)
			{
				AddParam(dict, key, value.Value);
			}
		}

		protected static void AddCoordinates(SortedDictionary<string, object> dict, string key, object coordObj)
		{
			if (coordObj == null)
			{
				return;
			}
			if (coordObj is Rectangle rectangle)
			{
				dict.Add(key, string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
			}
			else if (coordObj is List<Rectangle>)
			{
				List<Rectangle> source = (List<Rectangle>)coordObj;
				dict.Add(key, string.Join("|", source.Select((Rectangle r) => string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", r.X, r.Y, r.Width, r.Height)).ToArray()));
			}
			else
			{
				dict.Add(key, coordObj.ToString());
			}
		}

		protected virtual void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
		}
	}
}
