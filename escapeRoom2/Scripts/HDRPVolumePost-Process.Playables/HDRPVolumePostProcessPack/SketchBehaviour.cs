using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class SketchBehaviour : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float intensity;

		[Range(0f, 10f)]
		public float normalBaseOutlineIntensity;

		[Range(0f, 10f)]
		public float depthBaseOutlineIntensity;

		public Color outlineColor = Color.clear;

		public Color surfaceColor = Color.clear;

		[Range(0f, 1f)]
		public float surfaceIntensity;
	}
}
