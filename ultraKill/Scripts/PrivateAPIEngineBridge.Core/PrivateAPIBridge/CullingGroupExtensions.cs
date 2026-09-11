using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class CullingGroupExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_00241353324D9FA414AC28EF0778C2FEBD8A
		{
			[SpecialName]
			public static class _003CM_003E_00240019612229E0E0D5C0E0232153543EB4
			{
			}

			[ExtensionMarker("<M>$0019612229E0E0D5C0E0232153543EB4")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this CullingGroup @this)
		{
			return @this.m_Ptr;
		}
	}
}
