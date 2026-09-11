using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class PhysicsExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_00243C6D2FB1E62D7E4EA80B1FB0A50B9FB2
		{
			[SpecialName]
			public static class _003CM_003E_00243C6D2FB1E62D7E4EA80B1FB0A50B9FB2
			{
			}

			[ExtensionMarker("<M>$3C6D2FB1E62D7E4EA80B1FB0A50B9FB2")]
			public static RaycastHit[] Internal_RaycastAll(PhysicsScene physicsScene, Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
			{
				throw new NotSupportedException();
			}
		}

		public static RaycastHit[] Internal_RaycastAll(PhysicsScene physicsScene, Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_RaycastAll(physicsScene, ray, maxDistance, mask, queryTriggerInteraction);
		}
	}
}
