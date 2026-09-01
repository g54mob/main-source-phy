using System;
using HTraceAO.Scripts.Extensions;
using HTraceAO.Scripts.Globals;
using UnityEngine;

namespace HTraceAO.Scripts.Data.Public
{
	[Serializable]
	public class GeneralSettings
	{
		[SerializeField]
		public HBuffer HBuffer;

		[SerializeField]
		public AmbientOcclusionMode AmbientOcclusionMode;

		[SerializeField]
		private float _intensity;

		[SerializeField]
		private float _directLightOcclusion;

		[HExtensions.HRange(0.1f, 4f)]
		public float Intensity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[HExtensions.HRange(0f, 1f)]
		public float DirectLightOcclusion
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}
	}
}
