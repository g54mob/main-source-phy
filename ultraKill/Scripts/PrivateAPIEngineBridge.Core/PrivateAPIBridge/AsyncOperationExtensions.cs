using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class AsyncOperationExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_0024E1E24BCD7E316FBEF1DDD617A0CA13E6
		{
			[SpecialName]
			public static class _003CM_003E_0024E2523B815FAAAEEE7C7FAB1FDA0F2B1F
			{
			}

			[ExtensionMarker("<M>$E2523B815FAAAEEE7C7FAB1FDA0F2B1F")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this AsyncOperation @this)
		{
			return @this.m_Ptr;
		}
	}
}
