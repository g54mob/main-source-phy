using Poly.Base;
using Poly.Draw;
using UnityEngine;

namespace Poly.Physics.Viewers
{
	public class NodeViewer : WorldListener
	{
		public bool showVelocity;

		public override void AfterWorldFixedUpdate()
		{
			if (!Singleton<GlDrawer, int>.instance)
			{
				return;
			}
			foreach (NodeHandle nodeHandle in SingletonBehaviour<World>.instance.nodeHandles)
			{
				Draw(nodeHandle, showVelocity);
			}
		}

		private static void Draw(NodeHandle node, bool showVelocity)
		{
			GlDrawer.color = Color.white;
			float radius = (node.shapeHandleIndex.isValid ? node.shapeHandleIndex.Get().shape.radius : 0.1f);
			GlDrawer.DrawCircle(node.pos, radius);
		}
	}
}
