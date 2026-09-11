namespace plog.Helpers
{
	public static class HashingHelper
	{
		public static int Fnv1A(string input)
		{
			int num = -2128831035;
			foreach (char c in input)
			{
				num ^= c;
				num *= 16777619;
			}
			return num;
		}
	}
}
