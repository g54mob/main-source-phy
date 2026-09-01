using System;

namespace ModIO
{
	[Serializable]
	public struct ValueFormatting
	{
		public enum Method
		{
			None = 0,
			ByteCount = 1,
			AbbreviatedNumber = 2,
			TimeStampAsDateTime = 3,
			Percentage = 4,
			SecondsAsTime = 5
		}

		public Method method;

		public string toStringParameter;

		public static string FormatValue(object value, Method method, string toStringParameter)
		{
			string empty = string.Empty;
			if (string.IsNullOrEmpty(toStringParameter))
			{
				toStringParameter = "G";
			}
			switch (method)
			{
			case Method.ByteCount:
			{
				long value3 = 0L;
				if (value != null)
				{
					value3 = (long)value;
				}
				empty = ByteCount(value3, toStringParameter);
				break;
			}
			case Method.AbbreviatedNumber:
			{
				int value2 = 0;
				if (value != null)
				{
					value2 = (int)value;
				}
				empty = AbbreviateInteger(value2, toStringParameter);
				break;
			}
			case Method.TimeStampAsDateTime:
				empty = ((value != null) ? ServerTimeStamp.ToLocalDateTime((int)value).ToString(toStringParameter) : "--");
				break;
			case Method.Percentage:
				empty = ((value != null) ? (((float)value * 100f).ToString(toStringParameter) + "%") : "--%");
				break;
			case Method.SecondsAsTime:
			{
				int seconds = 0;
				if (value != null)
				{
					seconds = (int)value;
				}
				empty = SecondsAsTime(seconds);
				break;
			}
			default:
				empty = null;
				if (value != null && !string.IsNullOrEmpty(toStringParameter))
				{
					if (value is float num)
					{
						empty = num.ToString(toStringParameter);
					}
					else if (value is int num2)
					{
						empty = num2.ToString(toStringParameter);
					}
					else if (value is long num3)
					{
						empty = num3.ToString(toStringParameter);
					}
				}
				if (empty == null)
				{
					empty = ((value == null) ? string.Empty : value.ToString());
				}
				break;
			}
			return empty;
		}

		public static string AbbreviateInteger(int value, string toStringParameter)
		{
			if (string.IsNullOrEmpty(toStringParameter))
			{
				toStringParameter = "G";
			}
			if (value < 1000)
			{
				return value.ToString();
			}
			if (value < 100000)
			{
				return ((float)(value / 100) / 10f).ToString(toStringParameter) + "K";
			}
			if (value < 10000000)
			{
				return value / 1000 + "K";
			}
			if (value < 1000000000)
			{
				return ((float)(value / 100000) / 10f).ToString(toStringParameter) + "M";
			}
			return value / 1000000 + "M";
		}

		public static string ByteCount(long value, string toStringParameter)
		{
			string[] array = new string[4] { "B", "KB", "MB", "GB" };
			int num = 0;
			long num2 = value;
			long num3 = 0L;
			while (num2 > 1024 && num + 1 < array.Length)
			{
				num3 = num2;
				num2 /= 1024;
				num++;
			}
			if (num > 0 && num2 < 100)
			{
				return ((decimal)num3 / 1024m).ToString(toStringParameter) + array[num];
			}
			return num2 + array[num];
		}

		public static string SecondsAsTime(int seconds)
		{
			int num = 0;
			int num2 = 0;
			if (seconds > 60)
			{
				num = (int)Math.Floor((float)seconds / 60f);
				seconds %= 60;
			}
			if (num > 60)
			{
				num2 = (int)Math.Floor((float)num / 60f);
				num %= 60;
			}
			return num2.ToString("00") + ":" + num.ToString("00") + ":" + seconds.ToString("00");
		}
	}
}
