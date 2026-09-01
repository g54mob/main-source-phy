using System;
using System.Diagnostics;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	[Serializable]
	[DebuggerDisplay("w#: {worldIdx} n: {node0.worldIdx} - {node1.worldIdx}")]
	public class EdgeHandle : WorldObjectImpl
	{
		public float originalLength;

		[NonSerialized]
		public Edge unityEdgeComponent;

		[NonSerialized]
		public NodeHandle node0;

		[NonSerialized]
		public NodeHandle node1;

		public EdgeMaterial material;

		internal EdgeMaterial originalMaterial;

		public SolverEdge solverEdge;

		public Poly.Solver.Motion optional_motion;

		public bool runtime_isMarkedAsColliding;

		public float length => solverEdge.length;

		internal float maxForce { get; set; }

		public float maxForce_ActualFraction { get; set; }

		public override bool isDynamic
		{
			get
			{
				if (node0.solverNode.invMass == 0f)
				{
					return node1.solverNode.invMass != 0f;
				}
				return true;
			}
		}

		public Vec2 pos_slow => 0.5f * (node0.pos + node1.pos);

		public float stressNormalized => System.Math.Abs(stressNormalizedSigned);

		public float stressNormalizedSigned
		{
			get
			{
				float num = (0f - solverEdge.sumFullImpulsesInFrame / (float)world.settings.numEdgeIntegrationsPerFrame) / solverEdge.maxImpulsePerIntegration;
				if (0f < solverEdge.sumFullImpulses)
				{
					num /= solverEdge.maxTensionImpulseFactor;
				}
				return num;
			}
		}

		public EdgeHandle(NodeHandle node0, NodeHandle node1)
		{
			this.node0 = node0;
			this.node1 = node1;
			maxForce_ActualFraction = 1f;
		}

		internal void UpdateCachedStrength()
		{
			if ((bool)world)
			{
				maxForce = material.strength * world.settings.strengthMultiplier;
				solverEdge.maxImpulsePerIntegration = maxForce * world.settings.deltaTimeForVelocityEdge * world.settings.deltaTimeForVelocityEdge * maxForce_ActualFraction;
				solverEdge.maxTensionImpulseFactor = material.tensionStrengthFactor;
			}
		}

		public void OverrideMaterial(EdgeMaterial overrideMaterial)
		{
			_ = material != originalMaterial;
			if (material != overrideMaterial)
			{
				material = overrideMaterial;
				UpdateCachedStrength();
			}
		}

		public void RestoreMaterial()
		{
			if (material != originalMaterial)
			{
				material = originalMaterial;
				UpdateCachedStrength();
			}
		}

		public void CacheVirtualMassAndSolverStiffness()
		{
			float virtualMass = solverEdge.virtualMass;
			float num = node0.solverNode.invMass + node1.solverNode.invMass;
			solverEdge.virtualMass = ((num != 0f) ? (1f / num) : 0f);
			solverEdge.invMassA = node0.solverNode.invMass;
			solverEdge.invMassB = node1.solverNode.invMass;
			if ((bool)material && material.isSpring)
			{
				Spring.Init(this, material.springConstant, material.dampingConstant);
			}
			else
			{
				solverEdge.stiffness = (solverEdge.damping = 1f);
				if ((bool)material && material.overrideSolverStiffness)
				{
					float num2 = material.realativeStiffness;
					if (!(0f <= num2) || !(num2 <= 1f))
					{
						UnityEngine.Debug.LogWarning("EdgeMaterial.relativeStiffness must be between 0 & 1.");
						num2 = Mathf.Clamp01(num2);
					}
					solverEdge.stiffness = num2;
					solverEdge.damping = num2;
				}
			}
			float num3 = solverEdge.virtualMass / (virtualMass + 5.877472E-39f);
			if (num3 < 1f)
			{
				solverEdge.sumVelImpulses *= num3;
				if (solverEdge.pin_isUsing2d)
				{
					solverEdge.sumVelImpulses2d_X *= num3;
					solverEdge.sumVelImpulses2d_Y *= num3;
				}
			}
			solverEdge.impulseLimitFactor = 1f;
			solverEdge.wasForceClampedDuringFrame = false;
			solverEdge.isForceClamped = false;
		}

		public void CacheCollisionDataForEdge(float nodeToMotionVelocityMultiplier)
		{
			if (shapeHandleIndex.isValid)
			{
				Poly.Solver.Motion.ComputeEdge_ComT_Mass_Inertia(this, ref optional_motion);
				optional_motion = Poly.Solver.Motion.ConvertNodesToMotion_OutsideSolver(ref node0.solverNode, ref node1.solverNode, ref optional_motion, nodeToMotionVelocityMultiplier);
			}
		}

		public override void SetWorldAndIndex(World world, int index)
		{
			base.SetWorldAndIndex(world, index);
			_ = shapeHandleIndex.isValid;
		}

		public void ResetNodeIndices()
		{
			solverEdge.nodeIdxA = node0.worldIdx;
			solverEdge.nodeIdxB = node1.worldIdx;
			if ((bool)optional_motion.segment)
			{
				optional_motion.segment.worldIdx0 = node0.worldIdx;
				optional_motion.segment.worldIdx1 = node1.worldIdx;
			}
		}

		public NodeHandle GetOther(NodeHandle n)
		{
			if (n != node0)
			{
				return node0;
			}
			return node1;
		}

		public void CacheTransform2InShapeHandles_Util()
		{
			if (0 <= (short)shapeHandleIndex)
			{
				World.shapeHandleArray[(short)shapeHandleIndex].CacheTransform2(world.settings.nodeToMotionVelocityMultiplier);
			}
		}
	}
}
