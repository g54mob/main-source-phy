using System;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class CurveSerializeData
	{
		public float value;

		public bool useCurve;

		public AnimationCurve curve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		public CurveSerializeData()
		{
		}

		public CurveSerializeData(float value)
		{
			this.value = value;
			useCurve = false;
			curve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
		}

		public CurveSerializeData(float value, float curveStart, float curveEnd, bool useCurve = true)
		{
			this.value = value;
			this.useCurve = useCurve;
			curve = AnimationCurve.Linear(0f, Mathf.Clamp01(curveStart), 1f, Mathf.Clamp01(curveEnd));
		}

		public CurveSerializeData(float value, AnimationCurve curve)
		{
			this.value = value;
			useCurve = true;
			this.curve = curve;
		}

		public void SetValue(float value)
		{
			this.value = value;
			useCurve = false;
		}

		public void SetValue(float value, float curveStart, float curveEnd, bool useCurve = true)
		{
			this.value = value;
			this.useCurve = useCurve;
			curve = AnimationCurve.Linear(0f, Mathf.Clamp01(curveStart), 1f, Mathf.Clamp01(curveEnd));
		}

		public void SetValue(float value, AnimationCurve curve)
		{
			this.value = value;
			useCurve = true;
			this.curve = curve;
		}

		public void DataValidate(float min, float max)
		{
			value = Mathf.Clamp(value, min, max);
		}

		public float Evaluate(float time)
		{
			if (useCurve)
			{
				return curve.Evaluate(time) * value;
			}
			return value;
		}

		public float4x4 ConvertFloatArray()
		{
			if (useCurve)
			{
				return DataUtility.ConvertAnimationCurve(curve) * value;
			}
			return value;
		}

		public CurveSerializeData Clone()
		{
			return new CurveSerializeData
			{
				value = value,
				useCurve = useCurve,
				curve = new AnimationCurve(curve.keys)
				{
					preWrapMode = curve.preWrapMode,
					postWrapMode = curve.postWrapMode
				}
			};
		}
	}
}
