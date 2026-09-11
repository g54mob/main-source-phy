using System;
using System.Runtime.CompilerServices;

namespace Interop
{
	public struct InlineArray21<T>
	{
		private T t;

		private T _1;

		private T _2;

		private T _3;

		private T _4;

		private T _5;

		private T _6;

		private T _7;

		private T _8;

		private T _9;

		private T _10;

		private T _11;

		private T _12;

		private T _13;

		private T _14;

		private T _15;

		private T _16;

		private T _17;

		private T _18;

		private T _19;

		private T _20;

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

		public static implicit operator ReadOnlySpan<T>(in InlineArray21<T> array)
		{
			return InlineArrayHelper.AsReadOnlySpan<T, InlineArray21<T>>(in array);
		}

		public static implicit operator Span<T>(in InlineArray21<T> array)
		{
			return InlineArrayHelper.AsSpan<T, InlineArray21<T>>(ref Unsafe.AsRef(in array));
		}
	}
}
