using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class WaitForSecondsExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_0024099725B602701AE1DFD853A9DA688EC9
		{
			[SpecialName]
			public static class _003CM_003E_0024B5D44918BD2293682BB71D41B8E990EF
			{
			}

			[ExtensionMarker("<M>$B5D44918BD2293682BB71D41B8E990EF")]
			public float GetSeconds()
			{
				throw new NotSupportedException();
			}
		}

		public static float GetSeconds(this WaitForSeconds @this)
		{
			return @this.m_Seconds;
		}
	}
}
