namespace Interop
{
	internal static class BitHelper
	{
		public static byte ExtractRange(byte value, byte start, byte length)
		{
			return (byte)((value >> (int)start) & (uint)((1 << (int)length) - 1));
		}

		public static ushort ExtractRange(ushort value, byte start, byte length)
		{
			return (ushort)((value >> (int)start) & (uint)((1 << (int)length) - 1));
		}

		public static uint ExtractRange(uint value, byte start, byte length)
		{
			return (value >> (int)start) & (uint)((1 << (int)length) - 1);
		}

		public static ulong ExtractRange(ulong value, byte start, byte length)
		{
			return (value >> (int)start) & (ulong)((1L << (int)length) - 1);
		}

		public static void SetRange(ref byte value, byte start, byte length, byte flags)
		{
			value = SetRange(value, start, length, flags);
		}

		public static void SetRange(ref ushort value, byte start, byte length, ushort flags)
		{
			value = SetRange(value, start, length, flags);
		}

		public static void SetRange(ref uint value, byte start, byte length, uint flags)
		{
			value = SetRange(value, start, length, flags);
		}

		public static void SetRange(ref ulong value, byte start, byte length, ulong flags)
		{
			value = SetRange(value, start, length, flags);
		}

		public static byte SetRange(byte value, byte start, byte length, byte flags)
		{
			byte b = (byte)((1 << (int)length) - 1);
			byte num = (byte)(b << (int)start);
			byte b2 = (byte)((flags & b) << (int)start);
			return (byte)((~num & value) | b2);
		}

		public static ushort SetRange(ushort value, byte start, byte length, ushort flags)
		{
			ushort num = (ushort)((1 << (int)length) - 1);
			ushort num2 = (ushort)(num << (int)start);
			ushort num3 = (ushort)((flags & num) << (int)start);
			return (ushort)((~num2 & value) | num3);
		}

		public static uint SetRange(uint value, byte start, byte length, uint flags)
		{
			uint num = (uint)((1 << (int)length) - 1);
			uint num2 = num << (int)start;
			uint num3 = (flags & num) << (int)start;
			return (~num2 & value) | num3;
		}

		public static ulong SetRange(ulong value, byte start, byte length, ulong flags)
		{
			ulong num = (ulong)((1L << (int)length) - 1);
			ulong num2 = num << (int)start;
			ulong num3 = (flags & num) << (int)start;
			return (~num2 & value) | num3;
		}
	}
}
