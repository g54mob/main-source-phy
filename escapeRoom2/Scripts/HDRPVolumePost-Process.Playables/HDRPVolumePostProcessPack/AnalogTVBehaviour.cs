using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class AnalogTVBehaviour : PlayableBehaviour
	{
		public float masterVolume;

		public float lineCount;

		public Vector2 chromaticR = Vector2.zero;

		public Vector2 chromaticG = Vector2.zero;

		public Vector2 chromaticB = Vector2.zero;

		public float speed;
	}
}
