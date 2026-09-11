using System;
using System.Runtime.CompilerServices;

namespace Interop
{
	public struct InlineArray6<T>
	{
		private T t;

		private T _1;

		private T _2;

		private T _3;

		private T _4;

		private T _5;

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

		public static implicit operator ReadOnlySpan<T>(in InlineArray6<T> array)
		{
			return InlineArrayHelper.AsReadOnlySpan<T, InlineArray6<T>>(in array);
		}

		public static implicit operator Span<T>(in InlineArray6<T> array)
		{
			return InlineArrayHelper.AsSpan<T, InlineArray6<T>>(ref Unsafe.AsRef(in array));
		}
	}
}
