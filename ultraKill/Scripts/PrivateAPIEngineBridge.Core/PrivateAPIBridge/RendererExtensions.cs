using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class RendererExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_002470B9E77625CF1C3C021A4C12A62EA1AE
		{
			[SpecialName]
			public static class _003CM_003E_00248481089E942AB56A3C2D247F40F27A22
			{
			}

			[ExtensionMarker("<M>$8481089E942AB56A3C2D247F40F27A22")]
			public int GetMaterialCount()
			{
				throw new NotSupportedException();
			}
		}

		public static int GetMaterialCount(this Renderer @this)
		{
			return @this.GetMaterialCount();
		}
	}
}
