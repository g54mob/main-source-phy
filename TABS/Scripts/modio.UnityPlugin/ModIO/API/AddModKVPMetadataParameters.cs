namespace ModIO.API
{
	public class AddModKVPMetadataParameters : RequestParameters
	{
		public string[] metadata
		{
			set
			{
				SetStringArrayValue("metadata[]", value);
			}
		}

		public static string[] ConvertMetadataKVPsToAPIStrings(MetadataKVP[] kvps)
		{
			string[] array = new string[kvps.Length];
			for (int i = 0; i < kvps.Length; i++)
			{
				array[i] = kvps[i].key + ":" + kvps[i].value;
			}
			return array;
		}
	}
}
