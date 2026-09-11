using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class TrackedReferenceExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_0024A1E941AD1C95238A97A0BC0FD3FA114C
		{
			[SpecialName]
			public static class _003CM_003E_002413B5211221A15166C27A496189A818FD
			{
			}

			[ExtensionMarker("<M>$13B5211221A15166C27A496189A818FD")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this TrackedReference @this)
		{
			return @this.m_Ptr;
		}
	}
}
