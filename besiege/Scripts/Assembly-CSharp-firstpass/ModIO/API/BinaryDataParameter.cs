namespace ModIO.API
{
	public class BinaryDataParameter
	{
		public string key = string.Empty;

		public string fileName;

		public string mimeType;

		public byte[] contents;

		public static BinaryDataParameter Create(string key, string fileName, string mimeType, byte[] contents)
		{
			BinaryDataParameter binaryDataParameter = new BinaryDataParameter();
			binaryDataParameter.key = key;
			binaryDataParameter.fileName = fileName;
			binaryDataParameter.mimeType = mimeType;
			binaryDataParameter.contents = contents;
			return binaryDataParameter;
		}
	}
}
