using System;
using Cpp2ILInjected;
using UnityEngine;

namespace INab.ToonDetailer.URP;

[Serializable]
public class ToonDetailerSettings
{
	public enum DetailerType
	{
		Both,
		Contours,
		Cavity
	}

	public enum MaskUse
	{
		None,
		NotEqual,
		Equal
	}

	public DetailerType _DetailerType;

	public MaskUse _MaskUse;

	public LayerMask _MaskLayer;

	public bool _ControlViaVolumes;

	public Color _ColorHue;

	public bool _UseFade;

	public bool _FadeAffectsOnlyContours;

	public float _FadeStart;

	public float _FadeEnd;

	public float _BlackOffset;

	public float _ContoursIntensity;

	public float _ContoursThickness;

	public float _ContoursElevationStrength;

	public float _ContoursElevationSmoothness;

	public float _ContoursDepressionStrength;

	public float _ContoursDepressionSmoothness;

	public float _CavityIntensity;

	public float _CavityRadius;

	public float _CavityStrength;

	public int _CavitySamples;

	public bool UseMask
	{
		get
		{
			bool flag = _MaskUse == MaskUse.None;
			return !flag;
		}
	}

	public ToonDetailerSettings()
	{
		//IL_0012: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206D80]");
		_ColorHue = (Color)0;
		_FadeStart = 40f;
		_FadeEnd = 60f;
		_BlackOffset = 0.5f;
		_ContoursIntensity = 0.5f;
		_ContoursThickness = 1f;
		_ContoursElevationStrength = 1f;
		_ContoursDepressionStrength = 2f;
		_CavityIntensity = 1f;
		_CavityRadius = 0.5f;
		_CavityStrength = 1.25f;
		_CavitySamples = 12;
		base._002Ector();
	}
}
