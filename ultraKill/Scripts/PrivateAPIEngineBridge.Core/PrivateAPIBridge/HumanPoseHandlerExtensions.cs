using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class HumanPoseHandlerExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_00248742944454552C97F353BA59E4341107
		{
			[SpecialName]
			public static class _003CM_003E_00249D1366C3A4A894F985C47B56F1B98C42
			{
			}

			[ExtensionMarker("<M>$9D1366C3A4A894F985C47B56F1B98C42")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this HumanPoseHandler @this)
		{
			return @this.m_Ptr;
		}
	}
}
