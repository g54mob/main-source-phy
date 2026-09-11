using System;
using System.Runtime.CompilerServices;

namespace Interop
{
	public struct InlineArray1<T>
	{
		private T t;

		public readonly ref T this[int index] => ref ((Span<T>)this)[index];

		public readonly ref T this[Index index]
		{
			get
			{
				Span<T> span = this;
				return ref span[index.GetOffset(span.Length)];
			}
		}

		public readonly Span<T> this[Range range]
		{
			get
			{
				Span<T> span = this;
				Range range2 = range;
				return span[range2.Start..range2.End];
			}
		}

		public static implicit operator ReadOnlySpan<T>(in InlineArray1<T> array)
		{
			return InlineArrayHelper.AsReadOnlySpan<T, InlineArray1<T>>(in array);
		}

		public static implicit operator Span<T>(in InlineArray1<T> array)
		{
			return InlineArrayHelper.AsSpan<T, InlineArray1<T>>(ref Unsafe.AsRef(in array));
		}
	}
}
