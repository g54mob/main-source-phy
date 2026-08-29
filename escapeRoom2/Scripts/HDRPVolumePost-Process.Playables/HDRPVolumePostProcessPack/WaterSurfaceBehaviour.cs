using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class WaterSurfaceBehaviour : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float intensity;

		[Range(1f, 10f)]
		public int layerCount;

		[Range(0f, 5f)]
		public float brightness;

		[Range(0.001f, 1f)]
		public float brightness2;

		[Range(0f, 5f)]
		public float speed;

		[Range(0f, 5f)]
		public float speed2;

		[Range(0f, 5f)]
		public float meshDetail;

		[Range(0f, 5f)]
		public float floatSpeedX;

		[Range(0f, 5f)]
		public float floatSpeedY;

		public Color color = Color.clear;

		[Range(1f, 10f)]
		public float surfacePower;

		[Range(0f, 5f)]
		public float refrectiveIndex;
	}
}
