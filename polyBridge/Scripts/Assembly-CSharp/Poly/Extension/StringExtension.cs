namespace Poly.Extension
{
	public static class StringExtension
	{
		public static string Repeat(this string s, int numRepeats)
		{
			string text = "";
			for (int i = 0; i < numRepeats; i++)
			{
				text += s;
			}
			return text;
		}
	}
}
