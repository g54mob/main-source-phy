using System;
using System.Runtime.CompilerServices;

namespace MagicaCloth2
{
	[Serializable]
	public struct ExBitFlag8
	{
		public byte Value;

		public ExBitFlag8(byte initialValue = 0)
		{
			Value = initialValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			Value = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFlag(byte flag, bool sw)
		{
			if (sw)
			{
				Value |= flag;
			}
			else
			{
				Value = (byte)(Value & ~flag);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(byte flag)
		{
			return (Value & flag) != 0;
		}
	}
}
