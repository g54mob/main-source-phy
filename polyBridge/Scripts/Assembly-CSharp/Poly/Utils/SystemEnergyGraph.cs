using System.Collections.Generic;
using Poly.Base;
using Poly.Draw;
using Poly.Physics;
using UnityEngine;

namespace Poly.Utils
{
	public class SystemEnergyGraph : WorldListener
	{
		public int historySize = 1000;

		private World world;

		private List<float> energyHistory = new List<float>();

		private void Awake()
		{
			world = SingletonBehaviour<World>.instance;
		}

		public override void BeforeStep()
		{
			float item = CalcSystemEnergy();
			energyHistory.Add(item);
			if (energyHistory.Count > historySize)
			{
				energyHistory.RemoveAt(0);
			}
			Draw();
		}

		private float CalcSystemEnergy()
		{
			float magnitude = SingletonBehaviour<World>.instance.settings.gravity.magnitude;
			float num = 0f;
			foreach (Poly.Physics.Rigidbody body in SingletonBehaviour<World>.instance.bodies)
			{
				float num2 = body.mass * magnitude * body.motion.com.y;
				float num3 = 0.5f * body.mass * body.motion.linVel.sqrMagnitude + 0.5f * body.inertia * body.motion.angVel * body.motion.angVel;
				num += num2 + num3;
			}
			return num;
		}

		private void Draw()
		{
			GlDrawer.color = Color.red;
			Vec2 vec = Vec2.zero;
			for (int i = 0; i < energyHistory.Count; i++)
			{
				Vec2 a = new Vec2((float)i * 0.02f, energyHistory[i]);
				Vec2 b = (Vec2)base.transform.lossyScale;
				a = Vec2.Scale(ref a, ref b) + (Vec2)base.transform.position;
				if (i > 0)
				{
					GlDrawer.DrawLine(vec, a);
				}
				vec = a;
			}
		}
	}
}
