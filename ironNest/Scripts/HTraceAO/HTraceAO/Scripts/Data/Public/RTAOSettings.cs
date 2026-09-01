using System;
using HTraceAO.Scripts.Extensions;
using HTraceAO.Scripts.Globals;
using UnityEngine;

namespace HTraceAO.Scripts.Data.Public
{
	[Serializable]
	public class RTAOSettings
	{
		public DebugModeRTAO DebugMode;

		[SerializeField]
		private float _worldSpaceRadius;

		[SerializeField]
		private float _maxRayBias;

		[SerializeField]
		private float _specularOcclusion;

		[SerializeField]
		public AlphaCutout AlphaCutout;

		[SerializeField]
		public LayerMask LayerMask;

		[SerializeField]
		private int _rayCount;

		[SerializeField]
		public bool CullBackfaces;

		[SerializeField]
		public bool FullResolution;

		[SerializeField]
		public bool Checkerboarding;

		[SerializeField]
		public UpscalingQuality UpscalingQuality;

		[SerializeField]
		public bool UpscalingNormalRejection;

		[SerializeField]
		private int _sampleCountTemporal;

		[SerializeField]
		private float _motionRejection;

		[SerializeField]
		private float _normalRejectionTemporal;

		[SerializeField]
		private float _rejectionStrengthTemporal;

		[SerializeField]
		public ReprojectionFilter ReprojectionFilter;

		[SerializeField]
		private int _pixelRadius;

		[SerializeField]
		private float _filterStrength;

		[SerializeField]
		public bool NormalRejectionSpatial;

		[HExtensions.HRange(0.25f, 5f)]
		public float WorldSpaceRadius
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[HExtensions.HRange(0.001f, 0.02f)]
		public float MaxRayBias
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
		public float SpecularOcclusion
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[HExtensions.HRange(1, 8)]
		public int RayCount
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		[HExtensions.HRange(8, 16)]
		public int SampleCountTemporal
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		[HExtensions.HRange(0f, 1f)]
		public float MotionRejection
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
		public float NormalRejectionTemporal
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
		public float RejectionStrengthTemporal
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[HExtensions.HRange(1, 4)]
		public int PixelRadius
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		[HExtensions.HRange(0f, 1f)]
		public float FilterStrength
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
