using System;
using UnityEngine;

namespace INab.ToonDetailer.URP
{
	[Serializable]
	public class ToonDetailerSettings
	{
		public enum DetailerType
		{
			Both = 0,
			Contours = 1,
			Cavity = 2
		}

		public enum MaskUse
		{
			None = 0,
			NotEqual = 1,
			Equal = 2
		}

		[SerializeField]
		public DetailerType _DetailerType;

		[SerializeField]
		public MaskUse _MaskUse;

		[SerializeField]
		public LayerMask _MaskLayer;

		[SerializeField]
		public bool _ControlViaVolumes;

		[SerializeField]
		public Color _ColorHue;

		[SerializeField]
		public bool _UseFade;

		[SerializeField]
		public bool _FadeAffectsOnlyContours;

		[SerializeField]
		public float _FadeStart;

		[SerializeField]
		public float _FadeEnd;

		[SerializeField]
		[Range(0f, 1f)]
		public float _BlackOffset;

		[SerializeField]
		[Range(0f, 1f)]
		public float _ContoursIntensity;

		[SerializeField]
		[Range(0f, 3f)]
		public float _ContoursThickness;

		[SerializeField]
		[Range(0f, 3f)]
		public float _ContoursElevationStrength;

		[SerializeField]
		[Range(0f, 0.9f)]
		public float _ContoursElevationSmoothness;

		[SerializeField]
		[Range(0f, 3f)]
		public float _ContoursDepressionStrength;

		[SerializeField]
		[Range(0f, 0.9f)]
		public float _ContoursDepressionSmoothness;

		[SerializeField]
		[Range(0f, 1f)]
		public float _CavityIntensity;

		[SerializeField]
		[Range(0f, 1f)]
		public float _CavityRadius;

		[SerializeField]
		[Range(0f, 5f)]
		public float _CavityStrength;

		[SerializeField]
		[Range(1f, 16f)]
		public int _CavitySamples;

		public bool UseMask => false;
	}
}
