using Poly.Base;
using Poly.Collide;
using Poly.Draw;
using Poly.Physics.Gameplay;
using UnityEngine;

namespace Poly.Physics.Viewers
{
	public class ShapeViewer : WorldListener
	{
		public override void AfterWorldFixedUpdate()
		{
			if (!Singleton<GlDrawer, int>.instance)
			{
				return;
			}
			foreach (Rigidbody body in SingletonBehaviour<World>.instance.bodies)
			{
				Draw(body);
			}
			foreach (Trigger trigger in Singleton<TriggerManager, int>.instance.triggers)
			{
				Draw(trigger);
			}
		}

		private static void Draw(Rigidbody body)
		{
			if (body._shapeHandleIndices == null)
			{
				return;
			}
			ShapeHandleIndex[] shapeHandleIndices = body._shapeHandleIndices;
			foreach (short num in shapeHandleIndices)
			{
				ref ShapeHandle reference = ref World.shapeHandleArray[num];
				if (reference.shape.type == Shape.Type.Polygon)
				{
					((PolygonShape)reference.shape).DrawGizmos(reference.t2);
				}
			}
		}

		private static void Draw(Trigger trigger)
		{
			foreach (PolygonShape shape in trigger.shapes)
			{
				shape.DrawGizmos(trigger.t2, Color.blue, drawSomeDiagonals: false);
			}
		}
	}
}
