namespace ModIO.API
{
	public class AddModfileParameters : RequestParameters
	{
		public BinaryUpload zippedBinaryData
		{
			set
			{
				SetBinaryData("filedata", value.fileName, value.data);
			}
		}

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
				SetStringValue("active", value.ToString());
			}
		}

		public string fileHash
		{
			set
			{
				SetStringValue("filehash", value);
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
