using System;
using UnityEngine;

namespace Shapes
{
	public class ChargeBar : MonoBehaviour
	{
		[Header("Gameplay")]
		[SerializeField]
		private float chargeSpeed;

		[SerializeField]
		private float chargeDecaySpeed;

		[NonSerialized]
		public bool isCharging;

		private float charge;

		[Header("Style")]
		public Color tickColor;

		public Gradient chargeFillGradient;

		[Range(0f, 0.1f)]
		public float tickSizeSmol;

		[Range(0f, 0.1f)]
		public float tickSizeLorge;

		[Range(0f, 0.05f)]
		public float tickTickness;

		[Range(0f, 0.5f)]
		public float fontSize;

		[Range(0f, 0.5f)]
		public float fontSizeLorge;

		[Range(0f, 0.1f)]
		public float percentLabelOffset;

		[Range(0f, 0.4f)]
		public float fontGrowRangePrev;

		[Range(0f, 0.4f)]
		public float fontGrowRangeNext;

		[Header("Animation")]
		public AnimationCurve chargeFillCurve;

		public AnimationCurve animChargeShakeMagnitude;

		[Range(0f, 0.05f)]
		public float chargeShakeMagnitude;

		public float chargeShakeSpeed;

		public void UpdateCharge()
		{
		}

		public void DrawBar(FpsController fpsController, float barRadius)
		{
		}
	}
}
