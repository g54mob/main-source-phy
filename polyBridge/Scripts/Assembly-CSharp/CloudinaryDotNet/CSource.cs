namespace CloudinaryDotNet
{
	public class CSource
	{
		public string Source;

		public string SourceToSign;

		public CSource(string source)
		{
			SourceToSign = (Source = source);
		}

		public static CSource operator +(CSource src, string value)
		{
			return OpAddition(src, value);
		}

		public static CSource Add(CSource src, string value)
		{
			return OpAddition(src, value);
		}

		private static CSource OpAddition(CSource src, string value)
		{
			src.Source += value;
			src.SourceToSign += value;
			return src;
		}
	}
}
