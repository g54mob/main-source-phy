using System;
using UnityEngine;

[Serializable]
public class LineEffectInstance
{
	public enum CurveType
	{
		Add = 0,
		Multiply = 1
	}

	[Space(20f)]
	public bool active = true;

	public CurveType curveType;

	public AnimationCurve mainCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);

	public float mainCurveMultiplier = 1f;

	public float mainCurveTiling = 1f;

	public bool tilingPerMeter = true;

	public AnimationCurve effectOverLineCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	public float mainCurveScrollSpeed;

	public AnimationCurve effectOverTimeCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
}
