namespace Koenigz.PerfectCulling.IO
{
	public class BitStreamWriter
	{
		private const uint SingleBitMask = 1u;

		private byte[] m_buf;

		private byte m_currentByte;

		private int m_currenBitPos;

		private int m_currentBufPos;

		public byte[] Buffer => null;

		public int Length => 0;

		public void Reset()
		{
		}

		public int Write(uint value, int bits)
		{
			return 0;
		}

		public void Flush()
		{
		}

		private void WriteBit(uint singleBit)
		{
		}

		private void AppendCurrentByte()
		{
		}
	}
}
