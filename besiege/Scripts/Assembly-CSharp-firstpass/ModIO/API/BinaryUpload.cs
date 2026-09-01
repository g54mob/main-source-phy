namespace ModIO.API
{
	public class BinaryUpload
	{
		public string fileName = string.Empty;

		public byte[] data;

		public static BinaryUpload Create(string fileName, byte[] data)
		{
			BinaryUpload binaryUpload = new BinaryUpload();
			binaryUpload.fileName = fileName;
			binaryUpload.data = data;
			return binaryUpload;
		}
	}
}
