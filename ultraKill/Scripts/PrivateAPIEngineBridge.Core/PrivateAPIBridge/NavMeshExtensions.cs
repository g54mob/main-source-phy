using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace PrivateAPIBridge
{
	public static class NavMeshExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_0024B41CB630F375E361670347A080F9FA1A
		{
			[SpecialName]
			public static class _003CM_003E_0024B41CB630F375E361670347A080F9FA1A
			{
			}

			[ExtensionMarker("<M>$B41CB630F375E361670347A080F9FA1A")]
			public static void GetSettingsByIndex_Injected(int index, out NavMeshBuildSettings ret)
			{
				throw new NotSupportedException();
			}

			[ExtensionMarker("<M>$B41CB630F375E361670347A080F9FA1A")]
			public static int AddLinkInternal_Injected(ref NavMeshLinkData link, ref Vector3 position, ref Quaternion rotation)
			{
				throw new NotSupportedException();
			}
		}

		public static void GetSettingsByIndex_Injected(int index, out NavMeshBuildSettings ret)
		{
			NavMesh.GetSettingsByIndex_Injected(index, out ret);
		}

		public static int AddLinkInternal_Injected(ref NavMeshLinkData link, ref Vector3 position, ref Quaternion rotation)
		{
			return NavMesh.AddLinkInternal_Injected(ref link, ref position, ref rotation);
		}
	}
}
