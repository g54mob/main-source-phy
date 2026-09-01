using System;

namespace Poly.Solver
{
	[Serializable]
	public struct SolverEdge
	{
		public short nodeIdxA;

		public short nodeIdxB;

		public float length;

		public float stiffness;

		public float damping;

		public float virtualMass;

		public float maxImpulsePerIntegration;

		public float maxTensionImpulseFactor;

		public float sumVelImpulses;

		public float sumFullImpulses;

		public float sumFullImpulsesInFrame;

		public float directionX;

		public float directionY;

		public float virtualMass_Stiffness_Tau;

		public float virtualMass_Damping_Damping;

		public float cachedPosError;

		public float lengthVelocity;

		public float invMassA;

		public float invMassB;

		public float impulseLimitFactor;

		public bool isForceClamped;

		public bool pin_isUnbreakable;

		public bool isBroken;

		public bool isRope;

		public bool isSpring;

		public bool pin_isUsing2d;

		public bool wasForceClampedDuringFrame;

		public bool excludeFromMaxStressCalculation;

		public float sumVelImpulses2d_X
		{
			get
			{
				return directionX;
			}
			set
			{
				directionX = value;
			}
		}

		public float sumVelImpulses2d_Y
		{
			get
			{
				return directionY;
			}
			set
			{
				directionY = value;
			}
		}

		public float cachedPosError_X
		{
			get
			{
				return cachedPosError;
			}
			set
			{
				cachedPosError = value;
			}
		}

		public float cachedPosError_Y
		{
			get
			{
				return lengthVelocity;
			}
			set
			{
				lengthVelocity = value;
			}
		}

		public void InitDefaults()
		{
			stiffness = 1f;
			damping = 1f;
		}

		public float GetLastFrameForce(SolverSettings settings)
		{
			float num = 1f / (settings.deltaTimeForVelocityEdge * settings.deltaTimeForVelocityEdge);
			return sumFullImpulsesInFrame * num / (float)settings.numEdgeIntegrationsPerFrame;
		}
	}
}
