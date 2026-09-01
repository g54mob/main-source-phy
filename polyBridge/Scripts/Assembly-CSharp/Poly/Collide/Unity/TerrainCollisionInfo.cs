using System.Collections.Generic;
using Dreamteck.Splines;
using Poly.Math;
using UnityEngine;

namespace Poly.Collide.Unity
{
	public class TerrainCollisionInfo : MonoBehaviour
	{
		public bool setLastVertexXToZero;

		public GameObject OnAddedToWorld(bool isFlipped)
		{
			return SplineComputerToPolygonCollider.BuildRigidTerrainFromSurfaceSpline(GetComponent<SplineComputer>(), isFlipped, setLastVertexXToZero);
		}

		public PolygonShape[] CreatePolygonShapes_ForBuildMode(bool isFlipped)
		{
			List<PolygonShape> list = new List<PolygonShape>();
			GameObject gameObject = SplineComputerToPolygonCollider.BuildRigidTerrainFromSurfaceSpline(GetComponent<SplineComputer>(), isFlipped, setLastVertexXToZero);
			PolygonCollider[] componentsInChildren = gameObject.GetComponentsInChildren<PolygonCollider>();
			foreach (PolygonCollider polygonCollider in componentsInChildren)
			{
				list.AddRange(polygonCollider.CreateConvexPolygons(in Transform2.identity));
			}
			Object.Destroy(gameObject);
			return list.ToArray();
		}
	}
}
