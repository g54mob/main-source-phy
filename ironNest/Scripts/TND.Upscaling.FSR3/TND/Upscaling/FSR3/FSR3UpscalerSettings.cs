using System;
using TND.Upscaling.Framework;
using UnityEngine;

namespace TND.Upscaling.FSR3
{
	[Serializable]
	public class FSR3UpscalerSettings : UpscalerSettingsBase
	{
		[Serializable]
		public struct AdvancedParameters
		{
			[Range(0f, 1f)]
			public float velocityFactor;

			[Range(0f, 1f)]
			public float reactivenessScale;

			[Range(0f, 1f)]
			public float shadingChangeScale;

			[Range(0f, 1f)]
			public float accumulationAddedPerFrame;

			[Range(-1f, 1f)]
			public float minDisocclusionAccumulation;
		}

		public Texture transparencyAndCompositionMask;

		public bool enableDebugView;

		public AdvancedParameters advancedParameters;
	}
}
