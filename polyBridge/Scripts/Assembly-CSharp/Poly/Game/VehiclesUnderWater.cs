using System;
using System.Collections.Generic;
using Poly.Base;
using Poly.Physics;
using Poly.UI;
using UnityEngine;

namespace Poly.Game
{
	[Serializable]
	public class VehiclesUnderWater : IWorldListener
	{
		[Tooltip("Disabling this with quadratic resistance causes weird rotating/swinging artifacts. Also upward oriented CompactCar sinks faster than capsized one.")]
		public bool applyToWheels = true;

		[Header("Linear Drag")]
		[Range(0f, 1f)]
		public float linearDrag = 0.999f;

		[Range(0f, 1f)]
		public float angularDrag = 0.999f;

		[Header("Quadratic Resistance")]
		[Tooltip("This capsizes the vehicles (probably due to lack of bouyancy for the chassis, and car goes heaviest part down)")]
		public bool proprtionalToSqareVelocity;

		[ShowIf("proprtionalToSqareVelocity", false, false, "")]
		public float linearQuadDrag = 0.3f;

		[ShowIf("proprtionalToSqareVelocity", false, false, "")]
		public float angularQuadDrag = 0.1f;

		private List<Vehicle> vehicles = new List<Vehicle>();

		private bool isEnabled;

		[NonSerialized]
		public int instanceId = _nextInstanceId++;

		private static VehiclesUnderWater _instance;

		private static int _nextInstanceId;

		public static VehiclesUnderWater instance => _instance ?? (_instance = new VehiclesUnderWater());

		private VehiclesUnderWater()
		{
			if (_instance != null)
			{
				vehicles = _instance.vehicles;
				_instance.vehicles = null;
				_instance.Disable();
			}
			_instance = this;
		}

		public static void Add(Vehicle v)
		{
			instance.Enable();
			instance.vehicles.Add(v);
		}

		public static void Remove(Vehicle v)
		{
			instance.vehicles.Remove(v);
		}

		public static void Clear()
		{
			instance.vehicles.Clear();
		}

		public void UpdateFixed_Manual()
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			float num = Mathf.Pow(1f - linearDrag, fixedDeltaTime);
			float num2 = Mathf.Pow(1f - angularDrag, fixedDeltaTime);
			foreach (Vehicle vehicle in vehicles)
			{
				if ((!vehicle.Physics || vehicle.Physics.wheels == null || vehicle.Physics.wheels.Length == 0 || !(vehicle.Physics.wheels[0].motion.com.y < 0f)) && !vehicle.WheelsUnderWater())
				{
					continue;
				}
				Poly.Physics.Rigidbody[] array = (applyToWheels ? vehicle.Physics.allBodies : vehicle.Physics.chassis);
				foreach (Poly.Physics.Rigidbody rigidbody in array)
				{
					if (proprtionalToSqareVelocity)
					{
						float b = 1f - linearQuadDrag * rigidbody.linearVelocity.magnitude * fixedDeltaTime;
						float b2 = 1f - angularQuadDrag * Mathf.Abs(rigidbody.angularVelocityDeg) * (MathF.PI / 180f) * fixedDeltaTime;
						b = Mathf.Max(0.2f, b);
						b2 = Mathf.Max(0.2f, b2);
						rigidbody.motion.linVel *= b;
						rigidbody.motion.linVel *= b2;
					}
					else
					{
						rigidbody.motion.linVel *= num;
						rigidbody.motion.angVel *= num2;
					}
				}
			}
		}

		public void BeforeStep()
		{
		}

		public void AfterWorldCleared()
		{
			Clear();
		}

		public void AfterWorldFrameUpdate()
		{
		}

		public void AfterWorldFixedUpdate()
		{
			UpdateFixed_Manual();
		}

		private void Enable()
		{
			if (!isEnabled)
			{
				SingletonBehaviour<World>.instance.worldListeners.Add(this);
				isEnabled = true;
			}
		}

		private void Disable()
		{
			if (isEnabled && (bool)SingletonBehaviour<World>.instance)
			{
				SingletonBehaviour<World>.instance.worldListeners.Remove(this);
				isEnabled = false;
			}
		}
	}
}
