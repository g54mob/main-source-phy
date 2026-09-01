namespace ModIO.API
{
	public class EditModParameters : RequestParameters
	{
		public const int NAME_CHAR_LIMIT = 80;

		public const int NAMEID_CHAR_LIMIT = 80;

		public const int SUMMARY_CHAR_LIMIT = 250;

		public const int DESCRIPTION_CHAR_MIN = 100;

		public const int DESCRIPTION_CHAR_LIMIT = 50000;

		public const int METADATA_CHAR_LIMIT = 50000;

		public ModStatus status
		{
			set
			{
				SetStringValue("status", (int)value);
			}
		}

		public ModVisibility visibility
		{
			set
			{
				SetStringValue("visible", (int)value);
			}
		}

		public string name
		{
			set
			{
				SetStringValue("name", value);
			}
		}

		public string nameId
		{
			set
			{
				SetStringValue("name_id", value);
			}
		}

		public string summary
		{
			set
			{
				SetStringValue("summary", value);
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
	}
}
