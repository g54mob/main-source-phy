using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class ConcentrationLineBehaviour : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float intensity;

		public uint lineFrequency;

		public Vector2 centerViewportInScreen = Vector2.zero;

		[Range(0f, 2f)]
		public float blankRadius;

		[Range(0f, 1f)]
		public float colorThreshold;

		[Range(0.001f, 2f)]
		public float blankAspect;

		[Range(0f, 1f)]
		public float lineRotation;

		public Color lineColor = new Color(0f, 0f, 0f, 0f);
	}
}
