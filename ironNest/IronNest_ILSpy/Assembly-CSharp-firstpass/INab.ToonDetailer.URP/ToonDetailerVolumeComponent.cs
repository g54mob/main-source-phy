using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace INab.ToonDetailer.URP;

public sealed class ToonDetailerVolumeComponent : VolumeComponent, IPostProcessComponent
{
	public ColorParameter _ColorHue;

	public BoolParameter _FadeAffectsOnlyContours;

	public FloatParameter _FadeStart;

	public FloatParameter _FadeEnd;

	public ClampedFloatParameter _BlackOffset;

	public ClampedFloatParameter _ContoursIntensity;

	public ClampedFloatParameter _ContoursThickness;

	public ClampedFloatParameter _ContoursElevationStrength;

	public ClampedFloatParameter _ContoursElevationSmoothness;

	public ClampedFloatParameter _ContoursDepressionStrength;

	public ClampedFloatParameter _ContoursDepressionSmoothness;

	public ClampedFloatParameter _CavityIntensity;

	public ClampedFloatParameter _CavityRadius;

	public ClampedFloatParameter _CavityStrength;

	public ClampedIntParameter _CavitySamples;

	public unsafe ToonDetailerVolumeComponent()
	{
		//IL_029b: Expected O, but got Ref
		object obj = default(object);
		ColorParameter colorHue = new ColorParameter((Color)(&obj));
		_ColorHue = colorHue;
		_FadeAffectsOnlyContours = new BoolParameter(value: false);
		_FadeStart = new FloatParameter(40f);
		_FadeEnd = new FloatParameter(40f);
		bool overrideState = default(bool);
		_BlackOffset = new ClampedFloatParameter(0.5f, 0f, 1f, overrideState);
		_ContoursIntensity = new ClampedFloatParameter(0.5f, 0f, 1f, overrideState);
		_ContoursThickness = new ClampedFloatParameter(1f, 0f, 3f, overrideState);
		_ContoursElevationStrength = new ClampedFloatParameter(1f, 0f, 3f, overrideState);
		_ContoursElevationSmoothness = new ClampedFloatParameter(0f, 0f, 0.9f, overrideState);
		_ContoursDepressionStrength = new ClampedFloatParameter(2f, 0f, 3f, overrideState);
		_ContoursDepressionSmoothness = new ClampedFloatParameter(0f, 0f, 0.9f, overrideState);
		_CavityIntensity = new ClampedFloatParameter(1f, 0f, 1f, overrideState);
		_CavityRadius = new ClampedFloatParameter(0.5f, 0f, 1f, overrideState);
		_CavityStrength = new ClampedFloatParameter(1.25f, 0f, 5f, overrideState);
		_CavitySamples = new ClampedIntParameter(12, 1, 16, overrideState);
		base._002Ector();
		base._003CdisplayName_003Ek__BackingField = "Toon Detailer";
	}

	public bool IsActive()
	{
		//IL_00a6: Expected I4, but got O
		if (_ContoursIntensity != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180777B10");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803DA87Bh\"");
			object obj = default(object);
			if (obj == null)
			{
				if (_CavityIntensity == null)
				{
					goto IL_0098;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180777B10");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803DA87Bh\"");
				if (obj == null)
				{
					return false;
				}
			}
			return true;
		}
		goto IL_0098;
		IL_0098:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
