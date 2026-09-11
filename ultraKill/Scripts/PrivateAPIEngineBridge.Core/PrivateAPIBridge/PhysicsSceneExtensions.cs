using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class PhysicsSceneExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_00246540F50E9D38CBF7D50A79023D1F368B
		{
			[SpecialName]
			public static class _003CM_003E_00246540F50E9D38CBF7D50A79023D1F368B
			{
			}

			[ExtensionMarker("<M>$6540F50E9D38CBF7D50A79023D1F368B")]
			public static bool Internal_Raycast(PhysicsScene physicsScene, Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
			{
				throw new NotSupportedException();
			}

			[ExtensionMarker("<M>$6540F50E9D38CBF7D50A79023D1F368B")]
			public static int Internal_RaycastNonAlloc(PhysicsScene physicsScene, Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
			{
				throw new NotSupportedException();
			}
		}

		public static bool Internal_Raycast(PhysicsScene physicsScene, Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_Raycast(physicsScene, ray, maxDistance, ref hit, layerMask, queryTriggerInteraction);
		}

		public static int Internal_RaycastNonAlloc(PhysicsScene physicsScene, Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_RaycastNonAlloc(physicsScene, ray, raycastHits, maxDistance, mask, queryTriggerInteraction);
		}
	}
}
