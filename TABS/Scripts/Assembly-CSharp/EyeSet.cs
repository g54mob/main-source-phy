using System;
using Landfall.TABS;
using UnityEngine;

[Serializable]
public class EyeSet
{
	public AnimationCurve eyeWidthMultiplierCurve;

	public AnimationCurve separateEyeScaleCurve;

	public AnimationCurve allEyesScaleCurve;

	public AnimationCurve parentObjectScaleCurve;

	public AnimationCurve pupilSize;

	public AnimationCurve allPupilSize;

	public bool useCurves;

	public GameObject obj;

	public UnitRig.GearType m_gearType;
}
