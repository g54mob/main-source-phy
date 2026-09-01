using Poly.Base;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public class Hydraulics
	{
		public HydraulicsDefinition config;

		internal EdgeHandle edge;

		private bool startedDecelerating;

		private float deltaLengthPerIntegration;

		private float deltaVelocityPerIntegration;

		public bool isMoving { get; private set; }

		public float currentSpeed { get; private set; }

		private bool isExtendedToTarget { get; set; }

		private float baseLength { get; set; }

		public void Activate()
		{
			isExtendedToTarget = !isExtendedToTarget;
			startedDecelerating = false;
		}

		public float CalcProgress()
		{
			if (isMoving)
			{
				float a = baseLength;
				float b = baseLength * (1f + config.targetLengthFractionDelta);
				Values.SwapIf(!isExtendedToTarget, ref a, ref b);
				return (edge.solverEdge.length - a) / (b - a);
			}
			return 1f;
		}

		public static Hydraulics Create(EdgeHandle edge, HydraulicsDefinition define)
		{
			return new Hydraulics
			{
				config = define,
				edge = edge,
				baseLength = edge.solverEdge.length
			};
		}

		public void Dispose()
		{
			edge = null;
		}

		public void UpdateHydraulics(float stepDeltaTime)
		{
			float num = (isExtendedToTarget ? (baseLength * (1f + config.targetLengthFractionDelta)) : baseLength) - edge.solverEdge.length;
			if (Mathf.Abs(num) < 0.0001f)
			{
				num = 0f;
			}
			if (num == 0f != !isMoving)
			{
				if (!isMoving)
				{
					isMoving = true;
					startedDecelerating = false;
				}
				else
				{
					isMoving = false;
					currentSpeed = 0f;
				}
			}
			float num2 = 0f;
			float num3 = currentSpeed;
			if (isMoving)
			{
				float num4 = currentSpeed / config.acceleration;
				float num5 = currentSpeed * num4 * 0.5f;
				if (Mathf.Abs(num) < num5 * 1.1f)
				{
					num2 = (0f - config.acceleration) * Mathf.Sign(num);
					num3 = currentSpeed - config.acceleration * stepDeltaTime;
					startedDecelerating = true;
				}
				else if (currentSpeed < config.maxSpeed && !startedDecelerating)
				{
					num2 = config.acceleration * Mathf.Sign(num);
					num3 = currentSpeed + config.acceleration * stepDeltaTime;
				}
				else
				{
					num2 = 0f;
				}
			}
			else
			{
				currentSpeed = 0f;
			}
			float num6 = currentSpeed * stepDeltaTime;
			num = Mathf.Clamp(num, 0f - num6, num6);
			int numEdgeIntegrationsPerFrame = SingletonBehaviour<World>.instance.settings.numEdgeIntegrationsPerFrame;
			float num7 = stepDeltaTime / (float)numEdgeIntegrationsPerFrame;
			deltaLengthPerIntegration = num / (float)numEdgeIntegrationsPerFrame;
			deltaVelocityPerIntegration = num2 * num7 * num7;
			currentSpeed = num3;
		}

		public void UpdateInSolverOnIntegration(SolverEdge[] solverEdges)
		{
			if (isMoving)
			{
				deltaLengthPerIntegration += deltaVelocityPerIntegration;
				solverEdges[edge.worldIdx].length += deltaLengthPerIntegration;
				solverEdges[edge.worldIdx].lengthVelocity = deltaLengthPerIntegration;
			}
			else
			{
				solverEdges[edge.worldIdx].lengthVelocity = 0f;
			}
		}

		public static implicit operator bool(Hydraulics obj)
		{
			return obj != null;
		}
	}
}
