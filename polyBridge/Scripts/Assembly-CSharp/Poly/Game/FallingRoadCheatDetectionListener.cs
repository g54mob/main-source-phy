using Pb;
using Poly.Base;
using Poly.Physics;
using Poly.Solver;
using UnityEngine;

namespace Poly.Game
{
	[RequireComponent(typeof(Poly.Physics.Vehicle))]
	public class FallingRoadCheatDetectionListener : TemplateForAudioListener, IWorldListener
	{
		private Poly.Physics.Vehicle vehicle;

		private Poly.Physics.Rigidbody[] bodies;

		private FloatHistory impulseHistory = new FloatHistory(10);

		private int fallingRoadImpactCooldown;

		private const int fallingRoadImpactCooldownDuration = 10;

		private static float invDeltaTimeForMotion;

		private int debug_numContactsWithRoad;

		public bool fallingRoadDetectedInLast10Frames { get; private set; }

		public FallingRoadCheatDetectionListener()
		{
			impactVelocityThreshold = 1f;
		}

		private void OnEnable()
		{
			vehicle = GetComponentInParent<Poly.Physics.Vehicle>();
			bodies = vehicle.GetComponentsInChildren<Poly.Physics.Rigidbody>();
			Poly.Physics.Rigidbody[] array = bodies;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Add(this);
			}
			new FloatHistory(10);
			SingletonBehaviour<World>.instance.worldListeners.Add(this);
		}

		private void OnDisable()
		{
			SingletonBehaviour<World>.instance.worldListeners.Remove(this);
			Poly.Physics.Rigidbody[] array = bodies;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Remove(this);
			}
			Clear();
		}

		private static bool IsCollisionWithRoad(in ContactData data)
		{
			if (data.otherLayer != Layer.RoadEdge)
			{
				return data.otherLayer == Layer.RoadEdgeConnectedToSplitNode;
			}
			return true;
		}

		private static ref Poly.Solver.Motion GetMotion(in ContactData data)
		{
			return ref data.otherObject.GetComponent<Edge>().handle.optional_motion;
		}

		private static bool IsIllegalFallingRoad(in ContactData data, in Poly.Solver.Motion motion, in ContactPointInfo point)
		{
			Vec2 vec = motion.linVel * invDeltaTimeForMotion;
			float num = ((motion.invMass != 0f) ? (1f / motion.invMass) : 0f);
			float num2 = Vec2.Dot(in Vec2.up, point.normal * data.normalSign);
			if (-0.707f < num2 && vec.y < -0.5f && 50f < num * vec.sqrMagnitude)
			{
				return Pb.Mathf.Abs(vec.x / vec.y) < 0.1736f;
			}
			return false;
		}

		public override bool OnImpact(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (IsCollisionWithRoad(in data))
			{
				if (IsIllegalFallingRoad(in data, in GetMotion(in data), in point))
				{
					fallingRoadImpactCooldown = 10;
				}
				if (0 < fallingRoadImpactCooldown)
				{
					impulseHistory.Current += point.impulseApplied.magnitude;
				}
			}
			return false;
		}

		public override void OnTouchingPointEnter(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (IsCollisionWithRoad(in data))
			{
				debug_numContactsWithRoad++;
			}
		}

		public override bool OnTouchingPointStay(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (IsCollisionWithRoad(in data))
			{
				if (IsIllegalFallingRoad(in data, in GetMotion(in data), in point))
				{
					fallingRoadImpactCooldown = 10;
				}
				if (0 < fallingRoadImpactCooldown)
				{
					impulseHistory.Current += point.impulseApplied.magnitude;
				}
			}
			return false;
		}

		public override void OnTouchingPointExit(ref ContactData data, int pointIdx)
		{
			if (IsCollisionWithRoad(in data))
			{
				debug_numContactsWithRoad--;
			}
		}

		protected override void Clear()
		{
			vehicle = null;
			bodies = null;
			base.Clear();
		}

		public void BeforeStep()
		{
			invDeltaTimeForMotion = 1f / SingletonBehaviour<World>.instance.settings.deltaTimeForVelocity;
		}

		public void AfterWorldCleared()
		{
		}

		public void AfterWorldFrameUpdate()
		{
		}

		public void AfterWorldFixedUpdate()
		{
			float num = 0f;
			foreach (float item in impulseHistory)
			{
				num += item;
			}
			fallingRoadDetectedInLast10Frames = vehicle.mass * 5f < num;
			_ = fallingRoadDetectedInLast10Frames;
			impulseHistory.MoveNext();
			impulseHistory.Current = 0f;
			fallingRoadImpactCooldown = Pb.Mathf.Max(0, fallingRoadImpactCooldown - 1);
		}
	}
}
