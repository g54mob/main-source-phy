using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public static class Spring
	{
		public static void Init(EdgeHandle edge, float springConstant, float dampingConstant)
		{
			float virtualMass = edge.solverEdge.virtualMass;
			SolverSettings settings = edge.world.settings;
			float deltaTimeForVelocityEdge = settings.deltaTimeForVelocityEdge;
			int num = 1;
			if (!settings.integrateInSolverIterations)
			{
				num *= settings.numIterations * settings.numEdgeSubIterations;
			}
			else if (!settings.testOnly_integrateInEdgeSubIterations)
			{
				num *= settings.numEdgeSubIterations;
			}
			float num2 = Mathf.Max(edge.solverEdge.length, 1E-06f);
			float num5;
			float num4;
			if (edge.solverEdge.virtualMass != 0f)
			{
				float num3 = 1f / edge.solverEdge.virtualMass;
				num4 = springConstant / num2 * deltaTimeForVelocityEdge * deltaTimeForVelocityEdge;
				num5 = dampingConstant / num2 * deltaTimeForVelocityEdge;
				num4 *= num3 / settings.edgeTau;
				num5 *= num3 / settings.edgeDamping;
				num5 /= (float)num;
				if (settings.edgeTau < 0.5f)
				{
					num4 *= settings.edgeTau / 0.5f;
					num4 = Mathf.Clamp01(num4);
					num5 = Mathf.Clamp01(num5);
					num4 /= settings.edgeTau / 0.5f;
				}
				else
				{
					num4 = Mathf.Clamp01(num4);
					num5 = Mathf.Clamp01(num5);
				}
				num4 /= (float)num;
			}
			else
			{
				num4 = 0f;
				num5 = 0f;
			}
			edge.solverEdge.stiffness = num4;
			edge.solverEdge.damping = num5;
			edge.solverEdge.isSpring = true;
		}
	}
}
