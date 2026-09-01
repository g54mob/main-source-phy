using System.Collections.Generic;
using Dreamteck.Splines;
using Poly.Math;
using Poly.Physics;
using UnityEngine;

namespace Poly.Collide.Unity
{
	public class PlaceableCollisionInfo : MonoBehaviour
	{
		public Layer layer;

		public bool isFlipped { get; set; }

		public bool isTerrainIsland { get; set; }

		public bool isMiddleIsland { get; set; }

		public void OnAddedToWorld()
		{
			SplineComputerToPolygonCollider.BuildRigidBodyFromSplines(GetComponents<SplineComputer>(), layer, isFlipped, isTerrainIsland, isMiddleIsland);
		}

		public PolygonShape[] CreatePolygonShapes_ForBuildMode()
		{
			List<PolygonShape> list = new List<PolygonShape>();
			GameObject gameObject = SplineComputerToPolygonCollider.BuildRigidBodyFromSplines(GetComponents<SplineComputer>(), layer, isFlipped);
			PolygonCollider[] componentsInChildren = gameObject.GetComponentsInChildren<PolygonCollider>();
			foreach (PolygonCollider polygonCollider in componentsInChildren)
			{
				Transform2 shapeOrigin = ((Transform2)gameObject.transform).inverse_unoptimized;
				polygonCollider.transform.position = Vector3.zero;
				polygonCollider.transform.rotation = Quaternion.identity;
				list.AddRange(polygonCollider.CreateConvexPolygons(in shapeOrigin));
			}
			Object.Destroy(gameObject);
			return list.ToArray();
		}
	}
}
