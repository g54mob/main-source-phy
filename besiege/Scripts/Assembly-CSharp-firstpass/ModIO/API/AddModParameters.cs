namespace ModIO.API
{
	public class AddModParameters : RequestParameters
	{
		public BinaryUpload logo
		{
			set
			{
				SetBinaryData("logo", value.fileName, value.data);
			}
		}

		public string name
		{
			set
			{
				SetStringValue("name", value);
			}
		}

		public string summary
		{
			set
			{
				SetStringValue("summary", value);
			}
		}

		public ModVisibility visibility
		{
			set
			{
				SetStringValue("visible", (int)value);
			}
		}

		public string nameId
		{
			set
			{
				SetStringValue("name_id", value);
			}
		}

		public string descriptionAsHTML
		{
			set
			{
				SetStringValue("description", value);
			}
		}

		public string homepageURL
		{
			set
			{
				SetStringValue("homepage_url", value);
			}
		}

		public ModContentWarnings contentWarnings
		{
			set
			{
				SetStringValue("maturity_option", (int)value);
			}
		}

		public string metadataBlob
		{
			set
			{
				SetStringValue("metadata_blob", value);
			}
		}

		public string[] tags
		{
			set
			{
				SetStringArrayValue("tags[]", value);
			}
		}
	}
}
