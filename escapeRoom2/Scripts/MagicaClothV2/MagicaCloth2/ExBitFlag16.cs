using System;
using System.Runtime.CompilerServices;

namespace MagicaCloth2
{
	[Serializable]
	public struct ExBitFlag16
	{
		public ushort Value;

		public ExBitFlag16(ushort initialValue = 0)
		{
			Value = initialValue;
		}

		public void Clear()
		{
			Value = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFlag(ushort flag, bool sw)
		{
			if (sw)
			{
				Value |= flag;
			}
			else
			{
				Value = (ushort)(Value & ~flag);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(ushort flag)
		{
			return (Value & flag) != 0;
		}
	}
}
