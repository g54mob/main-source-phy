namespace ModIO.API
{
	public class AddModMediaParameters : RequestParameters
	{
		public BinaryUpload logo
		{
			set
			{
				SetBinaryData("logo", value.fileName, value.data);
			}
		}

		public BinaryUpload galleryImages
		{
			set
			{
				SetBinaryData("images", "images.zip", value.data);
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
