using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Shapes
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public readonly struct DashStack : IDisposable
	{
		private static readonly Stack<(bool, DashStyle)> dashes;

		internal static void Push(bool prevOn, DashStyle prevState)
		{
		}

		internal static void Pop()
		{
		}

		internal DashStack(bool on, DashStyle dash)
		{
		}

		public void Dispose()
		{
		}
	}
}
