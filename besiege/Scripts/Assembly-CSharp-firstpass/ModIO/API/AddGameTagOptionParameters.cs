namespace ModIO.API
{
	public class AddGameTagOptionParameters : RequestParameters
	{
		public string name
		{
			set
			{
				SetStringValue("name", value);
			}
		}

		public bool isMultiTagCategory
		{
			set
			{
				SetStringValue("type", (!value) ? "DROPDOWN" : "CHECKBOXES");
			}
		}

		public string[] tags
		{
			set
			{
				SetStringArrayValue("tags[]", value);
			}
		}

		public bool isHidden
		{
			set
			{
				SetStringValue("hidden", value.ToString());
			}
		}
	}
}
