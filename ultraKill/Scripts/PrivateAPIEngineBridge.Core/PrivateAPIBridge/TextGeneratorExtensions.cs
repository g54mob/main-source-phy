using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class TextGeneratorExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_002498D7820D64F04973F55ECC6BE38AFB15
		{
			[SpecialName]
			public static class _003CM_003E_00248E03975253C964E476865C7F82EF4B4C
			{
			}

			[ExtensionMarker("<M>$8E03975253C964E476865C7F82EF4B4C")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this TextGenerator @this)
		{
			return @this.m_Ptr;
		}
	}
}
