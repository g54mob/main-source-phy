using System;
using System.Collections.Generic;
using Poly.Base;
using Poly.Determinism;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	[Serializable]
	public class BridgeUnderWater : IWorldListener
	{
		[Header("Node Drag")]
		[Range(0f, 1f)]
		public float linearDrag = 0.97f;

		[Header("Rigidbody Drag")]
		[Range(0f, 1f)]
		public float bodyLinearDrag = 0.999f;

		[Range(0f, 1f)]
		public float bodyAngularDrag = 0.999f;

		internal List<NodeHandle> overlappingNodes = new List<NodeHandle>();

		internal HashSet<Poly.Physics.Rigidbody> overlappingBodies = new HashSet<Poly.Physics.Rigidbody>();

		private bool isEnabled;

		private static BridgeUnderWater _instance;

		public static BridgeUnderWater instance => _instance ?? (_instance = new BridgeUnderWater());

		public BridgeUnderWater()
		{
			if (_instance != null)
			{
				_instance.Disable();
			}
			_instance = this;
		}

		public static void Add(NodeHandle n)
		{
			instance.overlappingNodes.Add(n);
		}

		public static void Add(Poly.Physics.Rigidbody body)
		{
			instance.overlappingBodies.Add(body);
		}

		public static void Clear()
		{
			instance.overlappingNodes.Clear();
			instance.overlappingBodies.Clear();
		}

		public void UpdateFixed_Manual()
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			float num = Mathf.Pow(1f - linearDrag, fixedDeltaTime);
			foreach (NodeHandle overlappingNode in overlappingNodes)
			{
				DeterminismLog.LogEvent(overlappingNode.unityNodeComponent, Poly.Determinism.EventType.WaterDrag);
				overlappingNode.solverNode.vel *= num;
			}
			float num2 = Mathf.Pow(1f - bodyLinearDrag, fixedDeltaTime);
			float num3 = Mathf.Pow(1f - bodyAngularDrag, fixedDeltaTime);
			foreach (Poly.Physics.Rigidbody overlappingBody in overlappingBodies)
			{
				overlappingBody.motion.linVel *= num2;
				overlappingBody.motion.angVel *= num3;
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
