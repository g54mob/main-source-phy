using Poly.Base;
using Poly.Draw;
using UnityEngine;

namespace Poly.Physics.Viewers
{
	public class CenterOfMassViewer : WorldListener
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
		}

		public static void Draw(Rigidbody body)
		{
			Vector3 back = Vector3.back;
			GlDrawer.DrawLine(back + body.motion.com, back + (body.motion.com + body.t2.right), Color.red);
			GlDrawer.DrawLine(back + body.motion.com, back + (body.motion.com + body.t2.up), Color.green);
		}
	}
}
