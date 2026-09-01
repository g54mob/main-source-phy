namespace ModIO.API
{
	public class BinaryDataParameter
	{
		public string key = "";

		public string fileName;

		public string mimeType;

		public byte[] contents;

		public static BinaryDataParameter Create(string key, string fileName, string mimeType, byte[] contents)
		{
			return new BinaryDataParameter
			{
				key = key,
				fileName = fileName,
				mimeType = mimeType,
				contents = contents
			};
		}
	}
}
