namespace ModIO.API
{
	public class DeleteModMediaParameters : RequestParameters
	{
		public string[] images
		{
			set
			{
				SetStringArrayValue("images[]", value);
			}
		}

		public string[] youtube
		{
			set
			{
				SetStringArrayValue("youtube[]", value);
			}
		}

		public string[] sketchfab
		{
			set
			{
				SetStringArrayValue("sketchfab[]", value);
			}
		}
	}
}
