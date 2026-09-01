using System;
using UnityEngine;

namespace Poly.Physics
{
	[Serializable]
	public class HydraulicsDefinition
	{
		[Range(-1f, 9f)]
		public float targetLengthFractionDelta = 1f;

		[Range(0f, 10f)]
		public float acceleration = 0.5f;

		[Range(0f, 10f)]
		public float maxSpeed = 10f;
	}
}
