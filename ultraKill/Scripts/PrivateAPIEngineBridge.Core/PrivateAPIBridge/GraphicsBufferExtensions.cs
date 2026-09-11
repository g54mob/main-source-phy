using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class GraphicsBufferExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_0024748A35D4310DE578C922D4ECCDFA3D94
		{
			[SpecialName]
			public static class _003CM_003E_0024BD8CB8F1C7771982F79E1A25AE6CEE5A
			{
			}

			[ExtensionMarker("<M>$BD8CB8F1C7771982F79E1A25AE6CEE5A")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this GraphicsBuffer @this)
		{
			return @this.m_Ptr;
		}
	}
}
