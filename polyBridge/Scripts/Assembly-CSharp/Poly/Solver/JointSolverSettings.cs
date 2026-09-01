using System;
using UnityEngine;

namespace Poly.Solver
{
	[Serializable]
	public class JointSolverSettings
	{
		[Range(0f, 1f)]
		public float jointTau = 0.8f;

		[Range(0f, 1f)]
		public float jointDamping = 0.8f;

		public bool useJointWarmstarting;

		[Range(0f, 1f)]
		public float jointWarmstartingRatio = 1f;

		[Range(0f, 1f)]
		public float jointPosTau = 1f;

		[Range(0f, 4f)]
		public int numJointPostProjectionIterations = 1;

		public float maxJointPositionCorrection = 0.2f;

		public bool useSharedPivotPoint;

		[Range(0f, 1f)]
		public float posErrorLimit = 0.15f;

		public bool clipSpringForceToWithinPrismaticLimits = true;

		[Header("Experimental")]
		public bool warmstartVehicleEngine;

		[NonSerialized]
		public bool useParkingBrakes;

		[NonSerialized]
		public float force2ImpulseRB;
	}
}
