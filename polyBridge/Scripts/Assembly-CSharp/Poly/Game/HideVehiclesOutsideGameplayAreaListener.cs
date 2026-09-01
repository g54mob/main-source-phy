using System;
using System.Collections.Generic;
using Poly.Base;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	[Serializable]
	public class HideVehiclesOutsideGameplayAreaListener : IWorldListener
	{
		[Header("Node Drag")]
		[Range(0f, 1f)]
		public float linearDrag = 0.97f;

		[Header("Rigidbody Drag")]
		[Range(0f, 1f)]
		public float bodyLinearDrag = 0.999f;

		[Range(0f, 1f)]
		public float bodyAngularDrag = 0.999f;

		internal HashSet<Poly.Physics.Rigidbody> overlappingBodies = new HashSet<Poly.Physics.Rigidbody>();

		internal HashSet<Poly.Physics.Vehicle> hiddenVehicles = new HashSet<Poly.Physics.Vehicle>();

		private bool isEnabled;

		private static HideVehiclesOutsideGameplayAreaListener _instance;

		public static HideVehiclesOutsideGameplayAreaListener instance => _instance ?? (_instance = new HideVehiclesOutsideGameplayAreaListener());

		public HideVehiclesOutsideGameplayAreaListener()
		{
			if (_instance != null)
			{
				_instance.Disable();
			}
			_instance = this;
		}

		public static void Add(Poly.Physics.Rigidbody body)
		{
			instance.overlappingBodies.Add(body);
		}

		public static void Clear()
		{
			instance.overlappingBodies.Clear();
		}

		public void UpdateFixed_Manual()
		{
			List<Poly.Physics.Action> actions = SingletonBehaviour<World>.instance.actions;
			for (int i = 0; i < actions.Count; i++)
			{
				Poly.Physics.Vehicle vehicle = actions[i] as Poly.Physics.Vehicle;
				if (!vehicle || !vehicle.isVisible)
				{
					continue;
				}
				if (vehicle.isAddedToWorld && vehicle.allBodies != null)
				{
					bool flag = false;
					Poly.Physics.Rigidbody[] allBodies = vehicle.allBodies;
					foreach (Poly.Physics.Rigidbody rigidbody in allBodies)
					{
						if ((bool)rigidbody && overlappingBodies.Contains(rigidbody))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						vehicle.isVisible = false;
						vehicle.DisableCollisions();
					}
				}
				else
				{
					Debug.LogWarning("Earlier possible crash in release?");
				}
			}
		}

		public void BeforeStep()
		{
			Clear();
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

		internal void Enable()
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
