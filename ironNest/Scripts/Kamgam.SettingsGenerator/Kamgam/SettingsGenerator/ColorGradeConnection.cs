using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class ColorGradeConnection : Connection<float>
	{
		public enum ColorGradeEffect
		{
			[InspectorName("[na] Brigthness")]
			Brightness = 1,
			[InspectorName("Gain   (-1 to 1)")]
			Gain = 3,
			[InspectorName("Gamma   (-1 to 1)")]
			Gamma = 4,
			[InspectorName("Lift   (-1 to 1)")]
			Lift = 5,
			[InspectorName("PostExposure   (-10 to 10)")]
			PostExposure = 17,
			[InspectorName("Saturation   (-100 to 100)")]
			Saturation = 18,
			[InspectorName("[na] ColorFilter")]
			ColorFilter = 27,
			[InspectorName("Contrast   (-100 to 100 or more)")]
			Contrast = 2,
			[InspectorName("HueShift   (-175 to 175)")]
			HueShift = 6,
			[InspectorName("[na] LdrLutContribution")]
			LdrLutContribution = 7,
			[InspectorName("MixerBlueOutBlueIn   (-200 to 200)")]
			MixerBlueOutBlueIn = 8,
			MixerBlueOutGreenIn = 9,
			MixerBlueOutRedIn = 10,
			MixerGreenOutBlueIn = 11,
			MixerGreenOutGreenIn = 12,
			MixerGreenOutRedIn = 13,
			MixerRedOutBlueIn = 14,
			MixerRedOutGreenIn = 15,
			MixerRedOutRedIn = 16,
			[InspectorName("Temperature   (-100 to 100)")]
			Temperature = 19,
			[InspectorName("Tint   (-100 to 100)")]
			Tint = 20,
			[InspectorName("[na] ToneCurveGamma")]
			ToneCurveGamma = 21,
			[InspectorName("[na] ToneCurveShoulderAngle")]
			ToneCurveShoulderAngle = 22,
			[InspectorName("[na] ToneCurveShoulderLength")]
			ToneCurveShoulderLength = 23,
			[InspectorName("[na] ToneCurveShoulderStrength")]
			ToneCurveShoulderStrength = 24,
			[InspectorName("[na] ToneCurveToeLength")]
			ToneCurveToeLength = 25,
			[InspectorName("[na] ToneCurveToeStrength")]
			ToneCurveToeStrength = 26,
			[InspectorName("SMH Shadows   (-1 to 1)")]
			SMHShadows = 28,
			[InspectorName("SMH Midtones   (-1 to 1)")]
			SMHMidtones = 29,
			[InspectorName("SMH Highlights   (-1 to 1)")]
			SMHHighlights = 30
		}

		protected ColorGradeEffect _effect;

		protected LiftGammaGain _liftGammaGain;

		protected ColorAdjustments _colorAdjustment;

		protected ChannelMixer _channelMixer;

		protected WhiteBalance _whiteBalance;

		protected ShadowsMidtonesHighlights _shadowsMidtonesHighlights;

		public float _defaultValue;

		public ColorGradeEffect Effect => default(ColorGradeEffect);

		public static bool IsLiftGammaGain(ColorGradeEffect effect)
		{
			return false;
		}

		public static bool IsColorAdjustment(ColorGradeEffect effect)
		{
			return false;
		}

		public static bool IsChannelMixer(ColorGradeEffect effect)
		{
			return false;
		}

		public static bool IsWhiteBalance(ColorGradeEffect effect)
		{
			return false;
		}

		public static bool IsShadowsMidtonesHighlights(ColorGradeEffect effect)
		{
			return false;
		}

		public ColorGradeConnection(ColorGradeEffect effect = ColorGradeEffect.Gamma)
		{
		}

		public void UpdateDefaultValue()
		{
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float value)
		{
		}
	}
}
