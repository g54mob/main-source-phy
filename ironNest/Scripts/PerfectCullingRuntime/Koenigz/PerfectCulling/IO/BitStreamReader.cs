namespace Koenigz.PerfectCulling.IO
{
	public class BitStreamReader
	{
		private const int NumberOfBitsInByte = 8;

		private byte[] m_buffer;

		private int m_currentBits;

		private int m_bufPos;

		private static readonly byte[] BitMask;

		public uint Read(int bits)
		{
			return 0u;
		}

		public void Reset(byte[] buffer)
		{
		}

		private uint ReadBit()
		{
			return 0u;
		}
	}
}
