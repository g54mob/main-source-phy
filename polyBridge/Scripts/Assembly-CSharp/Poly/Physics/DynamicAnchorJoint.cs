using System;
using System.Collections.Generic;
using Poly.Base;
using Poly.Draw;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	[RequireComponent(typeof(Rigidbody))]
	public class DynamicAnchorJoint : WorldObject
	{
		internal short worldIdx;

		public Vec2 anchor;

		internal Vec2 pivotInLocal;

		public Node connectedNode;

		[NonSerialized]
		[Range(0f, 1f)]
		public float stiffness = 1f;

		[NonSerialized]
		[Range(0f, 1f)]
		public float damping = 1f;

		[Header("Debug settings")]
		public bool drawGizmos;

		public bool debug_dontUseAveragedPivot = true;

		private Vec2 velImpulse_SinceIntegration;

		private DynamicAnchorSolveProcess processBuffer;

		public World world { get; private set; }

		public bool isAddedToWorld => worldIdx >= 0;

		public Rigidbody body { get; set; }

		internal Rigidbody body0 => body;

		internal NodeHandle node1 => connectedNode.handle;

		public virtual void SetWorldAndIndex(World world, int index)
		{
			this.world = world;
			worldIdx = (short)index;
		}

		protected new void Awake()
		{
			base.Awake();
			worldIdx = -1;
			body = GetComponent<Rigidbody>();
		}

		protected new void OnValidate()
		{
			base.OnValidate();
		}

		protected new void OnDestroy()
		{
			base.OnDestroy();
		}

		protected new void OnEnable()
		{
			base.OnEnable();
			Registry<DynamicAnchorJoint>.Add(this);
		}

		protected new void OnDisable()
		{
			base.OnDisable();
			Registry<DynamicAnchorJoint>.Remove(this);
		}

		public void PrepForSolving(SolverSettings settings)
		{
			if (!settings.dynamicAnchors.useJointWarmstarting)
			{
				velImpulse_SinceIntegration = Vec2.zero;
			}
			if (settings.solveDynamicAnchorsInBridgeSolver)
			{
				processBuffer.BuildProcess_PerFrame(body0, node1, pivotInLocal);
			}
		}

		public void Warmstart(SolverSettings settings, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr)
		{
			JointSolverSettings dynamicAnchors = settings.dynamicAnchors;
			ref Poly.Solver.Motion reference = ref motionsPtr[body0.worldIdx];
			ref SolverNode reference2 = ref nodesPtr[node1.worldIdx];
			Poly.Solver.Motion.ComputeFromNode(in reference2, out var result, settings.nodeToMotionVelocityMultiplier);
			Quaternion quaternion = Quaternion.AngleAxis(reference.angle * 57.29578f, Vector3.forward);
			Vec2 a = reference.com + (Vec2)(quaternion * pivotInLocal);
			Vec2 b = result.com;
			Vec2 point = Vec2.LerpUnclamped(in a, in b, 0.5f);
			velImpulse_SinceIntegration *= dynamicAnchors.jointWarmstartingRatio;
			reference.ApplyImpulse(point, -velImpulse_SinceIntegration);
			result.ApplyImpulse(point, velImpulse_SinceIntegration);
			reference2.vel = result.linVel * settings.motionToNodeVelocityMultiplier;
		}

		public void Warmstart_BridgeSolver(SolverSettings settings, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr)
		{
			ref Poly.Solver.Motion m = ref motionsPtr[body0.worldIdx];
			ref SolverNode n = ref nodesPtr[node1.worldIdx];
			processBuffer.RecalculatePositionErrors_PerIntegration(nodesPtr, motionsPtr);
			velImpulse_SinceIntegration *= settings.dynamicAnchors.jointWarmstartingRatio;
			processBuffer.ApplyImpulse_DirX(ref m, ref n, velImpulse_SinceIntegration.x);
			processBuffer.ApplyImpulse_DirY(ref m, ref n, velImpulse_SinceIntegration.y);
		}

		public void Solve(SolverSettings settings, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr)
		{
			ref Poly.Solver.Motion reference = ref motionsPtr[body0.worldIdx];
			ref SolverNode reference2 = ref nodesPtr[node1.worldIdx];
			if (reference.invMass + reference2.invMass > 0f)
			{
				Poly.Solver.Motion.ComputeFromNode(in reference2, out var result, settings.nodeToMotionVelocityMultiplier);
				SolveHinge(settings, ref reference, ref result);
				reference2.vel = result.linVel * settings.motionToNodeVelocityMultiplier;
			}
		}

		public void Solve_BridgeSolver(SolverSettings settings, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr)
		{
			ref Poly.Solver.Motion reference = ref motionsPtr[body0.worldIdx];
			ref SolverNode reference2 = ref nodesPtr[node1.worldIdx];
			if (reference.invMass + reference2.invMass > 0f)
			{
				JointSolverSettings dynamicAnchors = settings.dynamicAnchors;
				ref DynamicAnchorSolveProcess reference3 = ref processBuffer;
				float velErrors_DirX = reference3.GetVelErrors_DirX(in reference, in reference2);
				float num = (0f - reference3.posError.x) * stiffness * dynamicAnchors.jointTau / reference3.invVirtualMass.x;
				float num2 = (0f - velErrors_DirX) * damping * dynamicAnchors.jointDamping / reference3.invVirtualMass.x;
				float impulse = num + num2;
				reference3.ApplyImpulse_DirX(ref reference, ref reference2, impulse);
				velImpulse_SinceIntegration.x += num2;
				velErrors_DirX = reference3.GetVelErrors_DirY(in reference, in reference2);
				float num3 = (0f - reference3.posError.y) * stiffness * dynamicAnchors.jointTau / reference3.invVirtualMass.y;
				num2 = (0f - velErrors_DirX) * damping * dynamicAnchors.jointDamping / reference3.invVirtualMass.y;
				impulse = num3 + num2;
				reference3.ApplyImpulse_DirY(ref reference, ref reference2, impulse);
				velImpulse_SinceIntegration.y += num2;
			}
		}

		public void SolvePosition_BridgeSolver(SolverSettings settings, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr)
		{
			ref Poly.Solver.Motion reference = ref motionsPtr[body0.worldIdx];
			ref SolverNode reference2 = ref nodesPtr[node1.worldIdx];
			if (reference.invMass + reference2.invMass > 0f)
			{
				JointSolverSettings dynamicAnchors = settings.dynamicAnchors;
				float maxJointPositionCorrection = dynamicAnchors.maxJointPositionCorrection;
				ref DynamicAnchorSolveProcess reference3 = ref processBuffer;
				float num = reference3.GetPosErrors_DirX(in reference, in reference2);
				if (num < 0f - maxJointPositionCorrection)
				{
					num = 0f - maxJointPositionCorrection;
				}
				else if (maxJointPositionCorrection < num)
				{
					num = maxJointPositionCorrection;
				}
				float impulse = (0f - num) * stiffness * dynamicAnchors.jointPosTau / reference3.invVirtualMass.x;
				reference3.ApplyPositionCorrection_DirX(ref reference, ref reference2, impulse);
				float num2 = reference3.GetPosErrors_DirY(in reference, in reference2);
				if (num2 < 0f - maxJointPositionCorrection)
				{
					num2 = 0f - maxJointPositionCorrection;
				}
				else if (maxJointPositionCorrection < num2)
				{
					num2 = maxJointPositionCorrection;
				}
				float impulse2 = (0f - num2) * stiffness * dynamicAnchors.jointPosTau / reference3.invVirtualMass.y;
				reference3.ApplyPositionCorrection_DirY(ref reference, ref reference2, impulse2);
			}
		}

		private void SolveHinge(SolverSettings settings, ref Poly.Solver.Motion motion0, ref Poly.Solver.Motion motion1)
		{
			JointSolverSettings dynamicAnchors = settings.dynamicAnchors;
			Quaternion quaternion = Quaternion.AngleAxis(motion0.angle * 57.29578f, Vector3.forward);
			Vec2 a = motion0.com + (Vec2)(quaternion * pivotInLocal);
			Vec2 b = motion1.com;
			Vec2 pivotPoint0InWorld = Vec2.LerpUnclamped(in a, in b, 0.5f);
			Vec2 a2 = b - a;
			Vec2[] array = new Vec2[2]
			{
				Vec2.right,
				Vec2.up
			};
			Vec2 vec = default(Vec2);
			if (debug_dontUseAveragedPivot)
			{
				vec.x = JointUtil.ComputeInverseVirtualMass(in motion0, in motion1, in a, in b, in array[0]);
				vec.y = JointUtil.ComputeInverseVirtualMass(in motion0, in motion1, in a, in b, in array[1]);
			}
			else
			{
				vec.x = JointUtil.ComputeInverseVirtualMass(in motion0, in motion1, in pivotPoint0InWorld, in pivotPoint0InWorld, in array[0]);
				vec.y = JointUtil.ComputeInverseVirtualMass(in motion0, in motion1, in pivotPoint0InWorld, in pivotPoint0InWorld, in array[1]);
			}
			for (int i = 0; i < 2; i++)
			{
				Vec2 b2 = Vec2.zero;
				switch (i)
				{
				case 0:
					b2.x = 1f / vec.x;
					break;
				case 1:
					b2.y = 1f / vec.y;
					break;
				}
				Vec2 a3 = motion1.GetPointVelocity(b) - motion0.GetPointVelocity(a);
				Vec2 vec2 = -Vec2.Scale(ref a2, ref b2) * stiffness * dynamicAnchors.jointTau;
				Vec2 vec3 = -Vec2.Scale(ref a3, ref b2) * damping * dynamicAnchors.jointDamping;
				Vec2 vec4 = vec2 + vec3;
				velImpulse_SinceIntegration += vec3;
				motion0.ApplyImpulse(pivotPoint0InWorld, -vec4);
				motion1.ApplyImpulse(pivotPoint0InWorld, vec4);
			}
		}

		private void OnDrawGizmos()
		{
			if (drawGizmos && Application.isPlaying)
			{
				GlDrawer.color = Color.white;
				Vec2 vec = body0.motion.TransformPoint_Slow(pivotInLocal);
				GlDrawer.DrawLine(body0.transform.position, vec);
			}
		}

		internal void CalcAnchor()
		{
			body.CacheTransform2();
			anchor = body.t2.InvMul(connectedNode.pos);
		}

		internal void CalcPivot()
		{
			body.CacheTransform2();
			pivotInLocal = body.motion.InverseTransformPoint_Slow(body.t2 * anchor);
		}

		public static void All_Warmstart(List<DynamicAnchorJoint> dynamicAnchorJoints, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < dynamicAnchorJoints.Count; i++)
			{
				dynamicAnchorJoints[i].Warmstart(settings, nodesPtr, motionsPtr);
			}
		}

		public static void All_Solve(List<DynamicAnchorJoint> dynamicAnchorJoints, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < dynamicAnchorJoints.Count; i++)
			{
				dynamicAnchorJoints[i].Solve(settings, nodesPtr, motionsPtr);
			}
		}

		public static void All_SolvePosition(List<DynamicAnchorJoint> dynamicAnchorJoints, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
		}

		public static void All_Warmstart_BridgeSolver(List<DynamicAnchorJoint> dynamicAnchorJoints, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < dynamicAnchorJoints.Count; i++)
			{
				dynamicAnchorJoints[i].Warmstart_BridgeSolver(settings, nodesPtr, motionsPtr);
			}
		}

		public static void All_Solve_BridgeSolver(List<DynamicAnchorJoint> dynamicAnchorJoints, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < dynamicAnchorJoints.Count; i++)
			{
				dynamicAnchorJoints[i].Solve_BridgeSolver(settings, nodesPtr, motionsPtr);
			}
		}

		public static void All_SolvePosition_BridgeSolver(List<DynamicAnchorJoint> dynamicAnchorJoints, SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr, SolverSettings settings)
		{
			for (int i = 0; i < dynamicAnchorJoints.Count; i++)
			{
				dynamicAnchorJoints[i].SolvePosition_BridgeSolver(settings, nodesPtr, motionsPtr);
			}
		}
	}
}
