using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Shapes
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public readonly struct MatrixStack : IDisposable
	{
		private static readonly Stack<Matrix4x4> matrices;

		internal static void Push(Matrix4x4 prevState)
		{
		}

		internal static void Pop()
		{
		}

		internal MatrixStack(Matrix4x4 mtx)
		{
		}

		public void Dispose()
		{
		}
	}
}
