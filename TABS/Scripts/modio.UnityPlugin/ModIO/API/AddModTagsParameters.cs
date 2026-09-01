namespace ModIO.API
{
	public class AddModTagsParameters : RequestParameters
	{
		public string[] tagNames
		{
			set
			{
				SetStringArrayValue("tags[]", value);
			}
		}
	}
}
