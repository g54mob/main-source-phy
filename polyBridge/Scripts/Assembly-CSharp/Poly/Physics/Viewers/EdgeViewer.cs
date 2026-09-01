using Poly.Base;
using Poly.Collide;
using Poly.Draw;
using Poly.Extension;
using UnityEngine;

namespace Poly.Physics.Viewers
{
	public class EdgeViewer : WorldListener
	{
		public override void AfterWorldFixedUpdate()
		{
			if (!Singleton<GlDrawer, int>.instance)
			{
				return;
			}
			foreach (EdgeHandle edgeHandle in SingletonBehaviour<World>.instance.edgeHandles)
			{
				Draw(edgeHandle);
			}
		}

		private static void Draw(EdgeHandle edge)
		{
			GlDrawer.color = ColorEx.lightGray;
			Vector3 a = edge.node0.pos;
			Vector3 b = edge.node1.pos;
			GlDrawer.DrawLine(a, b);
			if (edge.shapeHandleIndex.isValid)
			{
				ref ShapeHandle reference = ref edge.shapeHandleIndex.Get();
				float halfLengthX = ((Segment)reference.shape).halfLengthX;
				Vector3 vector = reference.t2 * new Vector2(0f - halfLengthX, 0f);
				Vector3 vector2 = reference.t2 * new Vector2(halfLengthX, 0f);
				float radius = reference.shape.radius;
				Vector3 vector3 = reference.t2.up * radius;
				GlDrawer.DrawCircle(vector, radius);
				GlDrawer.DrawCircle(vector2, radius);
				GlDrawer.DrawLine(vector + vector3, vector2 + vector3);
				GlDrawer.DrawLine(vector - vector3, vector2 - vector3);
			}
		}
	}
}
