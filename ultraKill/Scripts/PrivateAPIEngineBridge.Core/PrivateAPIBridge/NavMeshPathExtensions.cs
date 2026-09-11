using System;
using System.Runtime.CompilerServices;
using UnityEngine.AI;

namespace PrivateAPIBridge
{
	public static class NavMeshPathExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_0024305023D6BA59BFD9CBF172CABAEB91B8
		{
			[SpecialName]
			public static class _003CM_003E_002464A8EEE40AFB84C7A3523E0E9875424F
			{
			}

			[ExtensionMarker("<M>$64A8EEE40AFB84C7A3523E0E9875424F")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this NavMeshPath @this)
		{
			return @this.m_Ptr;
		}
	}
}
