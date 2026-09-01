namespace ModIO.API
{
	public class DeleteModTagsParameters : RequestParameters
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
