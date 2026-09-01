using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Shapes
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public readonly struct GradientFillStack : IDisposable
	{
		private static readonly Stack<(bool, GradientFill)> gradients;

		internal static void Push(bool prevOn, GradientFill prevState)
		{
		}

		internal static void Pop()
		{
		}

		internal GradientFillStack(bool on, GradientFill gradient)
		{
		}

		public void Dispose()
		{
		}
	}
}
