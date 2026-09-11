using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class ObjectExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_002491BA3A1FCAE39728B2693A632C857E1E
		{
			[SpecialName]
			public static class _003CM_003E_0024ADCBBB2BE605ADA11882C8B01BA7EA31
			{
			}

			[ExtensionMarker("<M>$ADCBBB2BE605ADA11882C8B01BA7EA31")]
			public IntPtr GetCachedPtr()
			{
				throw new NotSupportedException();
			}

			[ExtensionMarker("<M>$ADCBBB2BE605ADA11882C8B01BA7EA31")]
			public static bool CurrentThreadIsMainThread()
			{
				throw new NotSupportedException();
			}

			[ExtensionMarker("<M>$ADCBBB2BE605ADA11882C8B01BA7EA31")]
			public static int GetOffsetOfInstanceIDInCPlusPlusObject()
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetCachedPtr(this UnityEngine.Object @this)
		{
			return @this.GetCachedPtr();
		}

		public static bool CurrentThreadIsMainThread()
		{
			return UnityEngine.Object.CurrentThreadIsMainThread();
		}

		public static int GetOffsetOfInstanceIDInCPlusPlusObject()
		{
			return UnityEngine.Object.GetOffsetOfInstanceIDInCPlusPlusObject();
		}
	}
}
