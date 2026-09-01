using System;

namespace CloudinaryDotNet
{
	public static class Crc32
	{
		private static readonly uint[] Table = GenerateTable();

		public static uint ComputeChecksum(byte[] bytes)
		{
			uint num = uint.MaxValue;
			for (int i = 0; i < bytes.Length; i++)
			{
				byte b = (byte)((num & 0xFF) ^ bytes[i]);
				num = (num >> 8) ^ Table[b];
			}
			return ~num;
		}

		public static byte[] ComputeChecksumBytes(byte[] bytes)
		{
			return BitConverter.GetBytes(ComputeChecksum(bytes));
		}

		private static uint[] GenerateTable()
		{
			uint[] array = new uint[256];
			for (uint num = 0u; num < array.Length; num++)
			{
				uint num2 = num;
				for (int num3 = 8; num3 > 0; num3--)
				{
					num2 = (((num2 & 1) != 1) ? (num2 >> 1) : ((num2 >> 1) ^ 0xEDB88320u));
				}
				array[num] = num2;
			}
			return array;
		}
	}
}
