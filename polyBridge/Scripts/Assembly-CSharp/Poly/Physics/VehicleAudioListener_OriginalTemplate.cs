using System.Collections.Generic;
using System.Linq;
using Poly.Draw;
using UnityEngine;

namespace Poly.Physics
{
	[RequireComponent(typeof(Vehicle))]
	public class VehicleAudioListener_OriginalTemplate : TemplateForAudioListener
	{
		public Rigidbody[] chassisParts;

		public Rigidbody[] wheels;

		public float maxLocalPosY_ToRejectChassisImpact = 0.1f;

		public float maxDotLocalUp_ToRejectChassisImpact = -0.707f;

		public bool ignoreWheelContactWhenRejectingChassisImpacts;

		public bool logImpactEvents;

		private int numContactsOnWheels;

		private List<Transform> chassisTransforms = new List<Transform>();

		private List<Transform> wheelTransforms = new List<Transform>();

		public VehicleAudioListener_OriginalTemplate()
		{
			impactVelocityThreshold = 1f;
		}

		private void OnEnable()
		{
			Rigidbody[] array = chassisParts;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Add(this);
			}
			array = wheels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Add(this);
			}
			chassisTransforms.AddRange(chassisParts.Select((Rigidbody r) => r.transform));
			wheelTransforms.AddRange(wheels.Select((Rigidbody r) => r.transform));
		}

		private void OnDisable()
		{
			Rigidbody[] array = chassisParts;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Remove(this);
			}
			array = wheels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Remove(this);
			}
			Clear();
		}

		public override bool OnImpact(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (chassisTransforms.Contains(data.receivingObject))
			{
				float num = Vec2.Dot((Vec2)data.receivingObject.up, point.normal * data.normalSign);
				Vec2 position = point.position;
				if (data.normalSign == -1f)
				{
					position += point.normal * point.distance;
				}
				float num2 = Vec2.Dot((Vec2)data.receivingObject.up, position - (Vec2)data.receivingObject.position);
				if ((!ignoreWheelContactWhenRejectingChassisImpacts && 0 >= numContactsOnWheels) || !(num <= maxDotLocalUp_ToRejectChassisImpact) || !(num2 <= maxLocalPosY_ToRejectChassisImpact))
				{
					if (logImpactEvents)
					{
						Debug.Log("Receiving: " + data.receivingObject.name);
					}
					return base.OnImpact(ref data, pointIdx, in point);
				}
				if (logImpactEvents)
				{
					Debug.Log("Rejecting: " + data.receivingObject.name);
				}
				GlDrawer.color = Color.red;
				GlDrawer.DrawArrow(point.position, point.normal, Color.red);
			}
			return false;
		}

		public override void OnTouchingPointEnter(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (wheelTransforms.Contains(data.receivingObject))
			{
				numContactsOnWheels++;
			}
		}

		public override bool OnTouchingPointStay(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			return false;
		}

		public override void OnTouchingPointExit(ref ContactData data, int pointIdx)
		{
			if (wheelTransforms.Contains(data.receivingObject))
			{
				numContactsOnWheels--;
			}
		}

		protected override void Clear()
		{
			chassisTransforms.Clear();
			wheelTransforms.Clear();
			base.Clear();
		}
	}
}
