using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop
{
	internal static class InlineArrayHelper
	{
		public static Span<T> AsSpan<T, TArray>(ref TArray array) where TArray : struct
		{
			return MemoryMarshal.CreateSpan(ref Unsafe.As<TArray, T>(ref array), Unsafe.SizeOf<TArray>() / Unsafe.SizeOf<T>());
		}

		public static ReadOnlySpan<T> AsReadOnlySpan<T, TArray>(in TArray array) where TArray : struct
		{
			return MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<TArray, T>(ref Unsafe.AsRef(in array)), Unsafe.SizeOf<TArray>() / Unsafe.SizeOf<T>());
		}
	}
}
