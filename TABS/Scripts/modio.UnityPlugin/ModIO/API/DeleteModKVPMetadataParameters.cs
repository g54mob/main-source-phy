namespace ModIO.API
{
	public class DeleteModKVPMetadataParameters : RequestParameters
	{
		public string[] metadataKeys
		{
			set
			{
				SetStringArrayValue("metadata[]", value);
			}
		}
	}
}
