using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class CoroutineExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_00247FE1DCD1EBC8AA0274018BD425855861
		{
			[SpecialName]
			public static class _003CM_003E_0024B79815FC47293A364AA6567FD8637E10
			{
			}

			[ExtensionMarker("<M>$B79815FC47293A364AA6567FD8637E10")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this Coroutine @this)
		{
			return @this.m_Ptr;
		}
	}
}
