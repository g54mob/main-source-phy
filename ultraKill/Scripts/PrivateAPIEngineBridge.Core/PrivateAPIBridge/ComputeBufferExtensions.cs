using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class ComputeBufferExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_0024BA6B7672065ABE008DB5533A1B061F14
		{
			[SpecialName]
			public static class _003CM_003E_00243DAC0D8B83FF98AB5F03C4F1B7D9F879
			{
			}

			[ExtensionMarker("<M>$3DAC0D8B83FF98AB5F03C4F1B7D9F879")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this ComputeBuffer @this)
		{
			return @this.m_Ptr;
		}
	}
}
