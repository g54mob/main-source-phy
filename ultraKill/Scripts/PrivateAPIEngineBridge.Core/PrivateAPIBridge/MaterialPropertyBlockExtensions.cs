using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class MaterialPropertyBlockExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_00242D5D871AAFC8FF4ECF2C64E7F9969EC0
		{
			[SpecialName]
			public static class _003CM_003E_002460C2B79944139A8DFB5D13DC71B531F5
			{
			}

			[ExtensionMarker("<M>$60C2B79944139A8DFB5D13DC71B531F5")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this MaterialPropertyBlock @this)
		{
			return @this.m_Ptr;
		}
	}
}
