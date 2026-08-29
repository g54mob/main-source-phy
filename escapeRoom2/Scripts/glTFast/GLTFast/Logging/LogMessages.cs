using System.Text;

namespace GLTFast.Logging
{
	public static class LogMessages
	{
		public static string GetFullMessage(LogCode code, params string[] messages)
		{
			string[] array;
			if (code == LogCode.None)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (messages == null)
				{
					return "";
				}
				array = messages;
				foreach (string value in array)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(";");
					}
					stringBuilder.Append(value);
				}
				return stringBuilder.ToString();
			}
			if (messages == null)
			{
				return code.ToString();
			}
			StringBuilder stringBuilder2 = new StringBuilder(code.ToString());
			array = messages;
			foreach (string value2 in array)
			{
				stringBuilder2.Append(";");
				stringBuilder2.Append(value2);
			}
			return stringBuilder2.ToString();
		}
	}
}
