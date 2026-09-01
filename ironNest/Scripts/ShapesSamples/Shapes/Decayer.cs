using System;
using UnityEngine;

namespace Shapes
{
	[Serializable]
	public class Decayer
	{
		public float decaySpeed;

		public float magnitude;

		public AnimationCurve curve;

		[NonSerialized]
		public float value;

		[NonSerialized]
		public float valueInv;

		[NonSerialized]
		public float t;

		public void SetT(float v)
		{
		}

		public void Update()
		{
		}
	}
}
