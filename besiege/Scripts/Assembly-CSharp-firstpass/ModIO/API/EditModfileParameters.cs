namespace ModIO.API
{
	public class EditModfileParameters : RequestParameters
	{
		public string version
		{
			set
			{
				SetStringValue("version", value);
			}
		}

		public string changelog
		{
			set
			{
				SetStringValue("changelog", value);
			}
		}

		public bool isActiveBuild
		{
			set
			{
				SetStringValue("active", value);
			}
		}

		public string metadataBlob
		{
			set
			{
				SetStringValue("metadata_blob", value);
			}
		}
	}
}
