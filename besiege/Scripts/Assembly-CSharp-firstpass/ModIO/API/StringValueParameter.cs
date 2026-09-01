namespace ModIO.API
{
	public class StringValueParameter
	{
		public string key = string.Empty;

		public string value = string.Empty;

		public static StringValueParameter Create(string k, object v)
		{
			StringValueParameter stringValueParameter = new StringValueParameter();
			stringValueParameter.key = k;
			if (v != null)
			{
				stringValueParameter.value = v.ToString();
			}
			return stringValueParameter;
		}
	}
}
