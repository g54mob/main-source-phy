using System.Collections.Generic;
using System.Linq;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	[RequireComponent(typeof(Poly.Physics.Vehicle))]
	public class VehicleShutEngineWhileAirborneListener : TemplateForAudioListener
	{
		private Poly.Physics.Rigidbody[] poweredWheels;

		private List<Transform> wheelTransforms = new List<Transform>();

		private int numContactsOnWheels;

		private Poly.Physics.Vehicle vehicle;

		public VehicleShutEngineWhileAirborneListener()
		{
			impactVelocityThreshold = 1f;
		}

		private void OnEnable()
		{
			vehicle = GetComponentInParent<Poly.Physics.Vehicle>();
			poweredWheels = (from j in GetComponentsInChildren<WheelJoint>()
				where j.enableMotor
				select j.connectedBody).ToArray();
			Poly.Physics.Rigidbody[] array = poweredWheels;
			for (int num = 0; num < array.Length; num++)
			{
				array[num].collisionListeners.Add(this);
			}
			wheelTransforms.AddRange(poweredWheels.Select((Poly.Physics.Rigidbody r) => r.transform));
		}

		private void OnDisable()
		{
			Poly.Physics.Rigidbody[] array = poweredWheels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Remove(this);
			}
			Clear();
		}

		public override bool OnImpact(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (wheelTransforms.Contains(data.receivingObject))
			{
				return base.OnImpact(ref data, pointIdx, in point);
			}
			return false;
		}

		public override void OnTouchingPointEnter(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (wheelTransforms.Contains(data.receivingObject))
			{
				numContactsOnWheels++;
				if (numContactsOnWheels == 1)
				{
					vehicle.holdClutch = false;
				}
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
				if (numContactsOnWheels == 0)
				{
					vehicle.holdClutch = true;
				}
			}
		}

		protected override void Clear()
		{
			wheelTransforms.Clear();
			numContactsOnWheels = 0;
			base.Clear();
		}
	}
}
