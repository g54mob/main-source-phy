using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Clipper2Lib
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct HashCode
	{
		private static readonly uint s_seed = GenerateGlobalSeed();

		private const uint Prime2 = 2246822519u;

		private const uint Prime3 = 3266489917u;

		private const uint Prime4 = 668265263u;

		private const uint Prime5 = 374761393u;

		private static uint GenerateGlobalSeed()
		{
			using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			byte[] array = new byte[4];
			randomNumberGenerator.GetBytes(array);
			return BitConverter.ToUInt32(array, 0);
		}

		public static int Combine<T1, T2>(T1 value1, T2 value2)
		{
			uint queuedValue = (uint)(value1?.GetHashCode() ?? 0);
			uint queuedValue2 = (uint)(value2?.GetHashCode() ?? 0);
			return (int)MixFinal(QueueRound(QueueRound(MixEmptyState() + 8, queuedValue), queuedValue2));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint QueueRound(uint hash, uint queuedValue)
		{
			return RotateLeft(hash + (uint)((int)queuedValue * -1028477379), 17) * 668265263;
		}

		private static uint MixEmptyState()
		{
			return s_seed + 374761393;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixFinal(uint hash)
		{
			hash ^= hash >> 15;
			hash *= 2246822519u;
			hash ^= hash >> 13;
			hash *= 3266489917u;
			hash ^= hash >> 16;
			return hash;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateLeft(uint value, int offset)
		{
			return (value << offset) | (value >> 32 - offset);
		}

		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException("HashCode.GetHashCode() is not supported");
		}

		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			throw new NotSupportedException("HashCode.Equals() is not supported");
		}
	}
}
