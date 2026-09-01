using System.Collections.Generic;
using UnityEngine;

namespace Poly.Physics.ThirdParty
{
	public class ConvexDecomposerTester : MonoBehaviour
	{
		private PolygonCollider2D col;

		public bool show;

		private void Start()
		{
			col = GetComponent<PolygonCollider2D>();
			if (col == null)
			{
				Debug.LogError("There is no 'PolygonCollider2D' attached to the object 'BayazitDecomposerTester' is attached to.");
			}
		}

		private void OnDrawGizmos()
		{
			if (!Application.isPlaying || !show)
			{
				return;
			}
			Gizmos.color = Color.green;
			List<Vec2> list = new List<Vec2>();
			Vector2[] points = col.points;
			foreach (Vec2 vec in points)
			{
				Vec2 item = (Vec2)base.transform.TransformPoint(vec);
				list.Add(item);
			}
			foreach (List<Vec2> item2 in ConvexDecomposer.ConvexPartition(list))
			{
				for (int j = 0; j < item2.Count; j++)
				{
					Vec2 vec2 = item2[j];
					Gizmos.DrawLine(to: item2[(j + 1 < item2.Count) ? (j + 1) : 0], from: vec2);
				}
			}
		}
	}
}
