using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Shapes
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public readonly struct StyleStack : IDisposable
	{
		private static readonly Stack<DrawStyle> styles;

		internal static void Push(DrawStyle prevState)
		{
		}

		internal static void Pop()
		{
		}

		internal StyleStack(DrawStyle style)
		{
		}

		public void Dispose()
		{
		}
	}
}
