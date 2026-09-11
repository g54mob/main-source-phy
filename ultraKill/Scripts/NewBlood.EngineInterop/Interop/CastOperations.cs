using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Interop
{
	public static class CastOperations
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static class UpCasts
		{
			public static ref TTo static_cast<TFrom, TTo>(ref TFrom from) where TFrom : struct, IUpCastable<TTo> where TTo : struct
			{
				return ref UpCast<TFrom, TTo>(ref from);
			}

			public unsafe static TTo* static_cast<TFrom, TTo>(TFrom* from) where TFrom : unmanaged, IUpCastable<TTo> where TTo : unmanaged
			{
				return UpCast<TFrom, TTo>(from);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static class DownCasts
		{
			public static ref TTo static_cast<TFrom, TTo>(ref TFrom from) where TFrom : struct where TTo : struct, IUpCastable<TFrom>
			{
				return ref DownCast<TFrom, TTo>(ref from);
			}

			public unsafe static TTo* static_cast<TFrom, TTo>(TFrom* from) where TFrom : unmanaged where TTo : unmanaged, IUpCastable<TFrom>
			{
				return DownCast<TFrom, TTo>(from);
			}
		}

		public static ref TTo UpCast<TFrom, TTo>(ref TFrom from) where TFrom : struct, IUpCastable<TTo> where TTo : struct
		{
			return ref from.Cast();
		}

		public unsafe static TTo* UpCast<TFrom, TTo>(TFrom* from) where TFrom : unmanaged, IUpCastable<TTo> where TTo : unmanaged
		{
			return (TTo*)Unsafe.AsPointer(ref from->Cast());
		}

		public static ref TTo DownCast<TFrom, TTo>(ref TFrom from) where TFrom : struct where TTo : struct, IUpCastable<TFrom>
		{
			return ref Unsafe.As<TFrom, TTo>(ref Unsafe.SubtractByteOffset(ref from, GetOffsetFromBase<TFrom, TTo>()));
		}

		public unsafe static TTo* DownCast<TFrom, TTo>(TFrom* from) where TFrom : unmanaged where TTo : unmanaged, IUpCastable<TFrom>
		{
			return (TTo*)((byte*)from - GetOffsetFromBase<TFrom, TTo>());
		}

		[DoesNotReturn]
		public static Exception ThrowAmbiguousCast()
		{
			throw new InvalidCastException();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static nint GetOffsetFromBase<TBase, TDerived>() where TBase : struct where TDerived : struct, IUpCastable<TBase>
		{
			Unsafe.SkipInit<TDerived>(out var value);
			return Unsafe.ByteOffset(ref Unsafe.As<TDerived, byte>(ref value), ref Unsafe.As<TBase, byte>(ref value.Cast()));
		}
	}
}
