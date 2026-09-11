using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class AnimationCurveExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_002474306D0980FDA01B0186C03EB679DA80
		{
			[SpecialName]
			public static class _003CM_003E_0024EA000A37D2B81EA157FEF1A37BAC3F23
			{
			}

			[ExtensionMarker("<M>$EA000A37D2B81EA157FEF1A37BAC3F23")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this AnimationCurve @this)
		{
			return @this.m_Ptr;
		}
	}
}
