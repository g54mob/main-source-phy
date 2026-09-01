using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Shapes
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public readonly struct StateStack : IDisposable
	{
		internal static void Push(DrawStyle style, Matrix4x4 mtx)
		{
		}

		internal static void Pop()
		{
		}

		internal StateStack(DrawStyle style, Matrix4x4 mtx)
		{
		}

		public void Dispose()
		{
		}
	}
}
