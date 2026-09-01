namespace ModIO.API
{
	public class BinaryUpload
	{
		public string fileName = string.Empty;

		public byte[] data;

		public static BinaryUpload Create(string fileName, byte[] data)
		{
			return new BinaryUpload
			{
				fileName = fileName,
				data = data
			};
		}
	}
}
