using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class PingExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_00248A6D7AD233F89F5CDC733D9E49DEA80E
		{
			[SpecialName]
			public static class _003CM_003E_0024AB064E18B24420B54BEE5CAE1B88DF21
			{
			}

			[ExtensionMarker("<M>$AB064E18B24420B54BEE5CAE1B88DF21")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this Ping @this)
		{
			return @this.m_Ptr;
		}
	}
}
