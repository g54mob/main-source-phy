using Poly.Base;
using Poly.Draw;
using UnityEngine;

namespace Poly.Physics.Viewers
{
	public class InertiaViewer : WorldListener
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
			if (body.motion.invInertia != 0f)
			{
				float f = body.motion.invMass / body.motion.invInertia;
				f = Mathf.Sqrt(f);
				GlDrawer.color = Color.magenta;
				GlDrawer.DrawWireCube(back + body.motion.com, body.transform.rotation, Vector3.one * f);
			}
		}
	}
}
