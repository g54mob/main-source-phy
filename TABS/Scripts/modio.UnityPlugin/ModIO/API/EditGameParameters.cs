namespace ModIO.API
{
	public class EditGameParameters : RequestParameters
	{
		public GameStatus status
		{
			set
			{
				SetStringValue("status", (int)value);
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

		public string instructions
		{
			set
			{
				SetStringValue("instructions", value);
			}
		}

		public string instructionsURL
		{
			set
			{
				SetStringValue("instructions_url", value);
			}
		}

		public string ugcName
		{
			set
			{
				SetStringValue("ugc_name", value);
			}
		}

		public GameModGalleryPresentation modGalleryPresentation
		{
			set
			{
				SetStringValue("presentation_option", (int)value);
			}
		}

		public GameModSubmissionPermission modSubmissionPermission
		{
			set
			{
				SetStringValue("submission_option", (int)value);
			}
		}

		public GameModCuration modCuration
		{
			set
			{
				SetStringValue("curation_option", (int)value);
			}
		}

		public GameCommunityFeatures communityFeatures
		{
			set
			{
				SetStringValue("community_options", (int)value);
			}
		}

		public GameModRevenuePermissions modRevenuePermissions
		{
			set
			{
				SetStringValue("revenue_options", (int)value);
			}
		}

		public GameAPIPermissions apiPermissions
		{
			set
			{
				SetStringValue("api_access_options", (int)value);
			}
		}

		public GameModContentPermission contentPermission
		{
			set
			{
				SetStringValue("maturity_options", (int)value);
			}
		}
	}
}
