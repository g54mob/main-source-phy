using Poly.Draw;
using Poly.Physics.Gameplay;
using UnityEngine;

namespace Poly.Collide.Test
{
	public class PolygonClosestPointTest : MonoBehaviour
	{
		public TriggerComponent[] customShapes;

		private void OnDrawGizmos()
		{
			DrawContactPoints();
			DrawAabbs();
		}

		private void DrawContactPoints()
		{
			for (int i = 0; i < customShapes.Length; i++)
			{
				for (int j = i + 1; j < customShapes.Length; j++)
				{
					Trigger trigger = customShapes[i].trigger;
					Trigger trigger2 = customShapes[j].trigger;
					PolygonShape polyA = trigger.shapes[0];
					PolygonShape polyB = trigger2.shapes[0];
					PolygonCollisionProcess.Init(ref polyA, ref trigger.t2, ref polyB, ref trigger2.t2, out var process);
					PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint, doAveragePointPositions: false);
					Vector2 vector = trigger.t2 * closestPoint.pointInLocalA;
					Vector2 vector2 = trigger2.t2 * closestPoint.pointInLocalB;
					Vector2 vector3 = trigger.t2.rotation * closestPoint.normalInLocalA;
					GlDrawer.color = Color.white;
					GlDrawer.DrawCross(vector, 0.2f);
					GlDrawer.color = Color.red;
					GlDrawer.DrawCross(vector2, 0.2f);
					GlDrawer.color = Color.green;
					GlDrawer.DrawLine(vector, vector2);
					GlDrawer.color = Color.blue;
					GlDrawer.DrawLine(vector, vector + vector3 * 0.5f);
				}
			}
		}

		private void DrawAabbs()
		{
		}
	}
}
