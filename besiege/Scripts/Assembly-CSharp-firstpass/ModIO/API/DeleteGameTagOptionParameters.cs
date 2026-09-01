namespace ModIO.API
{
	public class DeleteGameTagOptionParameters : RequestParameters
	{
		public string name
		{
			set
			{
				SetStringValue("name", value);
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
