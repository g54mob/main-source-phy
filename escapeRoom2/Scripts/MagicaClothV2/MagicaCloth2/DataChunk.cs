namespace MagicaCloth2
{
	public struct DataChunk
	{
		public int startIndex;

		public int dataLength;

		public bool IsValid => dataLength > 0;

		public static DataChunk Empty => default(DataChunk);

		public DataChunk(int sindex, int length)
		{
			startIndex = sindex;
			dataLength = length;
		}

		public DataChunk(int sindex)
		{
			startIndex = sindex;
			dataLength = 1;
		}

		public void Clear()
		{
			startIndex = 0;
			dataLength = 0;
		}

		public override string ToString()
		{
			return $"[startIndex={startIndex}, dataLength={dataLength}]";
		}
	}
}
