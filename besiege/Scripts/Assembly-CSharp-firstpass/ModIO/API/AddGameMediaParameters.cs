namespace ModIO.API
{
	public class AddGameMediaParameters : RequestParameters
	{
		public BinaryUpload logo
		{
			set
			{
				SetBinaryData("logo", value.fileName, value.data);
			}
		}

		public BinaryUpload icon
		{
			set
			{
				SetBinaryData("icon", value.fileName, value.data);
			}
		}

		public BinaryUpload headerImage
		{
			set
			{
				SetBinaryData("header", value.fileName, value.data);
			}
		}
	}
}
