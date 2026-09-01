using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Shapes
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public readonly struct ColorStack : IDisposable
	{
		private static readonly Stack<Color> colors;

		internal static void Push(Color prevState)
		{
		}

		internal static void Pop()
		{
		}

		internal ColorStack(Color mtx)
		{
		}

		public void Dispose()
		{
		}
	}
}
