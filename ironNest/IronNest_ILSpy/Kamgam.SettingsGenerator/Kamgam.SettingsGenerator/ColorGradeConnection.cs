using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class ColorGradeConnection : Connection<float>
{
	public enum ColorGradeEffect
	{
		Brightness = 1,
		Gain = 3,
		Gamma = 4,
		Lift = 5,
		PostExposure = 17,
		Saturation = 18,
		ColorFilter = 27,
		Contrast = 2,
		HueShift = 6,
		LdrLutContribution = 7,
		MixerBlueOutBlueIn = 8,
		MixerBlueOutGreenIn = 9,
		MixerBlueOutRedIn = 10,
		MixerGreenOutBlueIn = 11,
		MixerGreenOutGreenIn = 12,
		MixerGreenOutRedIn = 13,
		MixerRedOutBlueIn = 14,
		MixerRedOutGreenIn = 15,
		MixerRedOutRedIn = 16,
		Temperature = 19,
		Tint = 20,
		ToneCurveGamma = 21,
		ToneCurveShoulderAngle = 22,
		ToneCurveShoulderLength = 23,
		ToneCurveShoulderStrength = 24,
		ToneCurveToeLength = 25,
		ToneCurveToeStrength = 26,
		SMHShadows = 28,
		SMHMidtones = 29,
		SMHHighlights = 30
	}

	protected ColorGradeEffect _effect;

	protected LiftGammaGain _liftGammaGain;

	protected ColorAdjustments _colorAdjustment;

	protected ChannelMixer _channelMixer;

	protected WhiteBalance _whiteBalance;

	protected ShadowsMidtonesHighlights _shadowsMidtonesHighlights;

	public float _defaultValue;

	public ColorGradeEffect Effect => _effect;

	public static bool IsLiftGammaGain(ColorGradeEffect effect)
	{
		//IL_000e: Expected O, but got I4
		//IL_003f: Expected O, but got I4
		object obj = effect - 4;
		if ((nint)obj <= 1)
		{
			return true;
		}
		object obj2 = effect - 3;
		return obj2 == null;
	}

	public static bool IsColorAdjustment(ColorGradeEffect effect)
	{
		//IL_000e: Expected O, but got I4
		//IL_0059: Expected O, but got I4
		object obj = effect - 17;
		if ((nint)obj > 1 && effect != ColorGradeEffect.Contrast)
		{
			object obj2 = effect - 6;
			return obj2 == null;
		}
		return true;
	}

	public static bool IsChannelMixer(ColorGradeEffect effect)
	{
		//IL_000e: Expected O, but got I4
		//IL_003f: Expected O, but got I4
		object obj = effect - 8;
		if ((nint)obj <= 7)
		{
			return true;
		}
		object obj2 = effect - 16;
		return obj2 == null;
	}

	public static bool IsWhiteBalance(ColorGradeEffect effect)
	{
		//IL_0034: Expected O, but got I4
		if (effect == ColorGradeEffect.Temperature)
		{
			return true;
		}
		object obj = effect - 20;
		return obj == null;
	}

	public static bool IsShadowsMidtonesHighlights(ColorGradeEffect effect)
	{
		//IL_000e: Expected O, but got I4
		//IL_003f: Expected O, but got I4
		object obj = effect - 28;
		if ((nint)obj <= 1)
		{
			return true;
		}
		object obj2 = effect - 30;
		return obj2 == null;
	}

	public unsafe ColorGradeConnection(ColorGradeEffect effect = ColorGradeEffect.Gamma)
	{
		//IL_003d: Expected O, but got I4
		//IL_0092: Expected O, but got I4
		//IL_00fa: Expected O, but got I4
		//IL_017f: Expected O, but got I4
		//IL_01c5: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		SettingsVolume instance = SettingsVolume.Instance;
		if (!(instance != null))
		{
			return;
		}
		object obj = effect - 4;
		_effect = effect;
		LiftGammaGain liftGammaGain;
		if ((nint)obj > 1 && effect != ColorGradeEffect.Gain)
		{
			object obj2 = effect - 17;
			if ((nint)obj2 > 1 && effect != ColorGradeEffect.Contrast && effect != ColorGradeEffect.HueShift)
			{
				object obj3 = effect - 8;
				if ((nint)obj3 > 7 && effect != ColorGradeEffect.MixerRedOutRedIn)
				{
					if (effect != ColorGradeEffect.Temperature && effect != ColorGradeEffect.Tint)
					{
						object obj4 = effect - 28;
						if ((nint)obj4 > 1 && effect != ColorGradeEffect.SMHHighlights)
						{
							object obj5 = default(object);
							string text = ((Enum)(&obj5)).ToString();
							string message = "The '" + text + "' color grading effect is not supported in the universal render pipeline.";
							Logger.LogWarning(message);
							goto IL_042b;
						}
						SettingsVolume instance2 = SettingsVolume.Instance;
						ShadowsMidtonesHighlights orAddComponent = instance2.GetOrAddComponent<ShadowsMidtonesHighlights>();
						_shadowsMidtonesHighlights = orAddComponent;
						_shadowsMidtonesHighlights.Override(_shadowsMidtonesHighlights, 1f);
						liftGammaGain = (LiftGammaGain)(object)_shadowsMidtonesHighlights;
					}
					else
					{
						SettingsVolume instance3 = SettingsVolume.Instance;
						WhiteBalance orAddComponent2 = instance3.GetOrAddComponent<WhiteBalance>();
						_whiteBalance = orAddComponent2;
						_whiteBalance.Override(_whiteBalance, 1f);
						liftGammaGain = (LiftGammaGain)(object)_whiteBalance;
					}
				}
				else
				{
					SettingsVolume instance4 = SettingsVolume.Instance;
					ChannelMixer orAddComponent3 = instance4.GetOrAddComponent<ChannelMixer>();
					_channelMixer = orAddComponent3;
					ChannelMixer channelMixer = _channelMixer;
					channelMixer.blueOutBlueIn.value = 100f;
					ChannelMixer channelMixer2 = _channelMixer;
					channelMixer2.redOutRedIn.value = 100f;
					ChannelMixer channelMixer3 = _channelMixer;
					channelMixer3.greenOutGreenIn.value = 100f;
					_channelMixer.Override(_channelMixer, 1f);
					liftGammaGain = (LiftGammaGain)(object)_channelMixer;
				}
			}
			else
			{
				SettingsVolume instance5 = SettingsVolume.Instance;
				ColorAdjustments orAddComponent4 = instance5.GetOrAddComponent<ColorAdjustments>();
				_colorAdjustment = orAddComponent4;
				_colorAdjustment.Override(_colorAdjustment, 1f);
				liftGammaGain = (LiftGammaGain)(object)_colorAdjustment;
			}
		}
		else
		{
			SettingsVolume instance6 = SettingsVolume.Instance;
			LiftGammaGain orAddComponent5 = instance6.GetOrAddComponent<LiftGammaGain>();
			_liftGammaGain = orAddComponent5;
			_liftGammaGain.Override(_liftGammaGain, 1f);
			liftGammaGain = _liftGammaGain;
		}
		liftGammaGain.active = false;
		goto IL_042b;
		IL_042b:
		UpdateDefaultValue();
	}

	public void UpdateDefaultValue()
	{
		//IL_0aff: Expected I4, but got I8
		//IL_0072: Expected O, but got I4
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0928: Expected I4, but got I8
		//IL_00ef: Expected O, but got I4
		//IL_04d5: Expected I4, but got I8
		//IL_03bc: Expected I4, but got I8
		//IL_01f6: Expected I4, but got I8
		LiftGammaGain liftGammaGain;
		LiftGammaGain liftGammaGain2;
		LiftGammaGain liftGammaGain3;
		ChannelMixer channelMixer;
		ClampedFloatParameter clampedFloatParameter;
		if (_effect != ColorGradeEffect.Lift && _effect != ColorGradeEffect.Gamma && _effect != ColorGradeEffect.Gain)
		{
			if (_effect != ColorGradeEffect.PostExposure)
			{
				object obj = _effect + -2;
				object obj2 = obj & 0xFFFFFFEFL;
				if (obj2 != null && _effect != ColorGradeEffect.HueShift)
				{
					if (_effect != ColorGradeEffect.MixerBlueOutBlueIn)
					{
						object obj3 = _effect - 9;
						if ((nint)obj3 > 6 && _effect != ColorGradeEffect.MixerRedOutRedIn)
						{
							if (_effect != ColorGradeEffect.Temperature && _effect != ColorGradeEffect.Tint)
							{
								if (_effect != ColorGradeEffect.SMHShadows && _effect != ColorGradeEffect.SMHMidtones && _effect != ColorGradeEffect.SMHHighlights)
								{
									return;
								}
								SettingsVolume instance = SettingsVolume.Instance;
								ShadowsMidtonesHighlights shadowsMidtonesHighlights = instance.FindDefaultVolumeComponent<ShadowsMidtonesHighlights>(useStackAsFallback: false, -1);
								bool flag = shadowsMidtonesHighlights == null;
								if (!flag && shadowsMidtonesHighlights.active != flag)
								{
									if (_effect == ColorGradeEffect.SMHShadows)
									{
										bool overrideState = ((LiftGammaGain)(object)shadowsMidtonesHighlights).lift.overrideState;
										bool flag2 = !overrideState;
										liftGammaGain = (LiftGammaGain)(object)shadowsMidtonesHighlights;
										if (!flag2)
										{
											goto IL_0291;
										}
									}
									if (_effect == ColorGradeEffect.SMHMidtones)
									{
										bool overrideState2 = ((LiftGammaGain)(object)shadowsMidtonesHighlights).gamma.overrideState;
										bool flag3 = !overrideState2;
										liftGammaGain2 = (LiftGammaGain)(object)shadowsMidtonesHighlights;
										if (!flag3)
										{
											goto IL_02f8;
										}
									}
									if (_effect == ColorGradeEffect.SMHHighlights)
									{
										bool overrideState3 = ((LiftGammaGain)(object)shadowsMidtonesHighlights).gain.overrideState;
										bool flag4 = !overrideState3;
										liftGammaGain3 = (LiftGammaGain)(object)shadowsMidtonesHighlights;
										if (!flag4)
										{
											goto IL_035f;
										}
										return;
									}
									return;
								}
							}
							else
							{
								SettingsVolume instance2 = SettingsVolume.Instance;
								WhiteBalance whiteBalance = instance2.FindDefaultVolumeComponent<WhiteBalance>(useStackAsFallback: false, -1);
								bool flag5 = whiteBalance == null;
								if (!flag5 && whiteBalance.active != flag5)
								{
									if (_effect == ColorGradeEffect.Temperature)
									{
										bool overrideState4 = whiteBalance.temperature.overrideState;
										channelMixer = (ChannelMixer)(object)whiteBalance;
										if (overrideState4)
										{
											goto IL_0868;
										}
									}
									if (_effect == ColorGradeEffect.Tint && whiteBalance.tint.overrideState)
									{
										clampedFloatParameter = whiteBalance.tint;
										goto IL_087a;
									}
									return;
								}
							}
							goto IL_0bc2;
						}
					}
					SettingsVolume instance3 = SettingsVolume.Instance;
					ChannelMixer channelMixer2 = instance3.FindDefaultVolumeComponent<ChannelMixer>(useStackAsFallback: false, -1);
					bool flag6 = channelMixer2 == null;
					if (!flag6 && channelMixer2.active != flag6)
					{
						if (_effect == ColorGradeEffect.MixerBlueOutBlueIn && channelMixer2.blueOutBlueIn.overrideState)
						{
							clampedFloatParameter = channelMixer2.blueOutBlueIn;
						}
						else if (_effect == ColorGradeEffect.MixerBlueOutGreenIn && channelMixer2.blueOutGreenIn.overrideState)
						{
							clampedFloatParameter = channelMixer2.blueOutGreenIn;
						}
						else if (_effect == ColorGradeEffect.MixerBlueOutRedIn && channelMixer2.blueOutRedIn.overrideState)
						{
							clampedFloatParameter = channelMixer2.blueOutRedIn;
						}
						else if (_effect == ColorGradeEffect.MixerGreenOutBlueIn && channelMixer2.greenOutBlueIn.overrideState)
						{
							clampedFloatParameter = channelMixer2.greenOutBlueIn;
						}
						else if (_effect == ColorGradeEffect.MixerGreenOutGreenIn && channelMixer2.greenOutGreenIn.overrideState)
						{
							clampedFloatParameter = channelMixer2.greenOutGreenIn;
						}
						else if (_effect == ColorGradeEffect.MixerGreenOutRedIn && channelMixer2.greenOutRedIn.overrideState)
						{
							clampedFloatParameter = channelMixer2.greenOutRedIn;
						}
						else if (_effect == ColorGradeEffect.MixerRedOutBlueIn && channelMixer2.redOutBlueIn.overrideState)
						{
							clampedFloatParameter = channelMixer2.redOutBlueIn;
						}
						else
						{
							if (_effect != ColorGradeEffect.MixerRedOutGreenIn || !channelMixer2.redOutGreenIn.overrideState)
							{
								if (_effect == ColorGradeEffect.MixerRedOutRedIn)
								{
									bool overrideState5 = channelMixer2.redOutRedIn.overrideState;
									bool flag7 = !overrideState5;
									channelMixer = channelMixer2;
									if (!flag7)
									{
										goto IL_0868;
									}
									return;
								}
								return;
							}
							clampedFloatParameter = channelMixer2.redOutGreenIn;
						}
						goto IL_087a;
					}
					if (_effect == ColorGradeEffect.MixerBlueOutBlueIn || _effect == ColorGradeEffect.MixerGreenOutGreenIn || _effect == ColorGradeEffect.MixerRedOutRedIn)
					{
						_defaultValue = 100f;
						return;
					}
					goto IL_0bc2;
				}
			}
			SettingsVolume instance4 = SettingsVolume.Instance;
			ColorAdjustments colorAdjustments = instance4.FindDefaultVolumeComponent<ColorAdjustments>(useStackAsFallback: false, -1);
			bool flag8 = colorAdjustments == null;
			if (!flag8 && colorAdjustments.active != flag8)
			{
				if (_effect == ColorGradeEffect.PostExposure)
				{
					bool overrideState6 = colorAdjustments.postExposure.overrideState;
					channelMixer = (ChannelMixer)(object)colorAdjustments;
					if (overrideState6)
					{
						goto IL_0868;
					}
				}
				if (_effect == ColorGradeEffect.Saturation && colorAdjustments.saturation.overrideState)
				{
					clampedFloatParameter = colorAdjustments.saturation;
				}
				else if (_effect == ColorGradeEffect.Contrast && colorAdjustments.contrast.overrideState)
				{
					clampedFloatParameter = colorAdjustments.contrast;
				}
				else
				{
					if (_effect != ColorGradeEffect.HueShift || !colorAdjustments.hueShift.overrideState)
					{
						return;
					}
					clampedFloatParameter = colorAdjustments.hueShift;
				}
				goto IL_087a;
			}
		}
		else
		{
			SettingsVolume instance5 = SettingsVolume.Instance;
			LiftGammaGain liftGammaGain4 = instance5.FindDefaultVolumeComponent<LiftGammaGain>(useStackAsFallback: false, -1);
			bool flag9 = liftGammaGain4 == null;
			if (!flag9 && liftGammaGain4.active != flag9)
			{
				bool flag10 = _effect == ColorGradeEffect.Lift;
				liftGammaGain = liftGammaGain4;
				if (flag10)
				{
					goto IL_0291;
				}
				bool flag11 = _effect == ColorGradeEffect.Gamma;
				liftGammaGain2 = liftGammaGain4;
				if (flag11)
				{
					goto IL_02f8;
				}
				if (_effect == ColorGradeEffect.Gain)
				{
					liftGammaGain3 = liftGammaGain4;
					goto IL_035f;
				}
				return;
			}
		}
		goto IL_0bc2;
		IL_0868:
		clampedFloatParameter = channelMixer.redOutRedIn;
		goto IL_087a;
		IL_087a:
		float value = clampedFloatParameter.value;
		float defaultValue = default(float);
		_defaultValue = defaultValue;
		return;
		IL_0371:
		Vector4Parameter vector4Parameter;
		_defaultValue = vector4Parameter.value.w;
		return;
		IL_0bc2:
		_defaultValue = 0f;
		return;
		IL_035f:
		vector4Parameter = liftGammaGain3.gain;
		goto IL_0371;
		IL_02f8:
		vector4Parameter = liftGammaGain2.gamma;
		goto IL_0371;
		IL_0291:
		vector4Parameter = liftGammaGain.lift;
		goto IL_0371;
	}

	public override float Get()
	{
		//IL_0072: Expected O, but got I4
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_00ef: Expected O, but got I4
		//IL_0a52: Expected O, but got I4
		//IL_0a71: Expected I, but got O
		//IL_0a81: Expected O, but got I
		//IL_0a91: Expected O, but got I
		//IL_0b1c: Expected O, but got I4
		//IL_0ba2: Expected O, but got I4
		//IL_0c28: Expected O, but got I4
		//IL_057b: Expected O, but got I4
		//IL_0601: Expected O, but got I4
		//IL_0687: Expected O, but got I4
		//IL_070d: Expected O, but got I4
		//IL_0793: Expected O, but got I4
		//IL_042a: Expected O, but got I4
		//IL_0819: Expected O, but got I4
		//IL_089f: Expected O, but got I4
		//IL_0999: Expected O, but got I4
		//IL_0925: Expected O, but got I4
		//IL_04b0: Expected O, but got I4
		LiftGammaGain liftGammaGain;
		Vector4Parameter vector4Parameter;
		ColorAdjustments colorAdjustments;
		FloatParameter floatParameter;
		if (_effect != ColorGradeEffect.Lift && _effect != ColorGradeEffect.Gamma && _effect != ColorGradeEffect.Gain)
		{
			if (_effect != ColorGradeEffect.PostExposure)
			{
				object obj = _effect - 2;
				object obj2 = obj & 0xFFFFFFEFL;
				if (obj2 != null && _effect != ColorGradeEffect.HueShift)
				{
					if (_effect != ColorGradeEffect.MixerBlueOutBlueIn)
					{
						object obj3 = _effect - 9;
						if ((nint)obj3 > 6 && _effect != ColorGradeEffect.MixerRedOutRedIn)
						{
							if (_effect != ColorGradeEffect.Temperature && _effect != ColorGradeEffect.Tint)
							{
								if ((_effect == ColorGradeEffect.SMHShadows || _effect == ColorGradeEffect.SMHMidtones || _effect == ColorGradeEffect.SMHHighlights) && _shadowsMidtonesHighlights != null)
								{
									ShadowsMidtonesHighlights shadowsMidtonesHighlights = _shadowsMidtonesHighlights;
									if (shadowsMidtonesHighlights.active)
									{
										if (_effect == ColorGradeEffect.SMHShadows && shadowsMidtonesHighlights.shadows.overrideState)
										{
											liftGammaGain = (LiftGammaGain)(object)_shadowsMidtonesHighlights;
											goto IL_0cdd;
										}
										if (_effect == ColorGradeEffect.SMHMidtones)
										{
											ShadowsMidtonesHighlights shadowsMidtonesHighlights2 = _shadowsMidtonesHighlights;
											if (shadowsMidtonesHighlights2.midtones.overrideState)
											{
												ShadowsMidtonesHighlights shadowsMidtonesHighlights3 = _shadowsMidtonesHighlights;
												vector4Parameter = shadowsMidtonesHighlights3.midtones;
												goto IL_0cef;
											}
										}
										if (_effect == ColorGradeEffect.SMHHighlights)
										{
											ShadowsMidtonesHighlights shadowsMidtonesHighlights4 = _shadowsMidtonesHighlights;
											if (shadowsMidtonesHighlights4.highlights.overrideState)
											{
												ShadowsMidtonesHighlights shadowsMidtonesHighlights5 = _shadowsMidtonesHighlights;
												vector4Parameter = shadowsMidtonesHighlights5.highlights;
												goto IL_0cef;
											}
										}
									}
								}
							}
							else if (_whiteBalance != null)
							{
								WhiteBalance whiteBalance = _whiteBalance;
								if (whiteBalance.active)
								{
									if (_effect == ColorGradeEffect.Temperature && whiteBalance.temperature.overrideState)
									{
										colorAdjustments = (ColorAdjustments)(object)_whiteBalance;
										object obj4 = 0;
										goto IL_0a57;
									}
									if (_effect == ColorGradeEffect.Tint)
									{
										WhiteBalance whiteBalance2 = _whiteBalance;
										if (whiteBalance2.tint.overrideState)
										{
											WhiteBalance whiteBalance3 = _whiteBalance;
											floatParameter = whiteBalance3.tint;
											object obj4 = 0;
											goto IL_0a69;
										}
									}
								}
							}
							goto IL_0dfb;
						}
					}
					if (_channelMixer != null)
					{
						ChannelMixer channelMixer = _channelMixer;
						if (channelMixer.active)
						{
							if (_effect == ColorGradeEffect.MixerBlueOutBlueIn && channelMixer.blueOutBlueIn.overrideState)
							{
								ChannelMixer channelMixer2 = _channelMixer;
								floatParameter = channelMixer2.blueOutBlueIn;
								object obj4 = 0;
								goto IL_0a69;
							}
							if (_effect == ColorGradeEffect.MixerBlueOutGreenIn)
							{
								ChannelMixer channelMixer3 = _channelMixer;
								if (channelMixer3.blueOutGreenIn.overrideState)
								{
									ChannelMixer channelMixer4 = _channelMixer;
									floatParameter = channelMixer4.blueOutGreenIn;
									object obj4 = 0;
									goto IL_0a69;
								}
							}
							if (_effect == ColorGradeEffect.MixerBlueOutRedIn)
							{
								ChannelMixer channelMixer5 = _channelMixer;
								if (channelMixer5.blueOutRedIn.overrideState)
								{
									ChannelMixer channelMixer6 = _channelMixer;
									floatParameter = channelMixer6.blueOutRedIn;
									object obj4 = 0;
									goto IL_0a69;
								}
							}
							if (_effect == ColorGradeEffect.MixerGreenOutBlueIn)
							{
								ChannelMixer channelMixer7 = _channelMixer;
								if (channelMixer7.greenOutBlueIn.overrideState)
								{
									ChannelMixer channelMixer8 = _channelMixer;
									floatParameter = channelMixer8.greenOutBlueIn;
									object obj4 = 0;
									goto IL_0a69;
								}
							}
							if (_effect == ColorGradeEffect.MixerGreenOutGreenIn)
							{
								ChannelMixer channelMixer9 = _channelMixer;
								if (channelMixer9.greenOutGreenIn.overrideState)
								{
									ChannelMixer channelMixer10 = _channelMixer;
									floatParameter = channelMixer10.greenOutGreenIn;
									object obj4 = 0;
									goto IL_0a69;
								}
							}
							if (_effect == ColorGradeEffect.MixerGreenOutRedIn)
							{
								ChannelMixer channelMixer11 = _channelMixer;
								if (channelMixer11.greenOutRedIn.overrideState)
								{
									ChannelMixer channelMixer12 = _channelMixer;
									floatParameter = channelMixer12.greenOutRedIn;
									object obj4 = 0;
									goto IL_0a69;
								}
							}
							if (_effect == ColorGradeEffect.MixerRedOutBlueIn)
							{
								ChannelMixer channelMixer13 = _channelMixer;
								if (channelMixer13.redOutBlueIn.overrideState)
								{
									ChannelMixer channelMixer14 = _channelMixer;
									floatParameter = channelMixer14.redOutBlueIn;
									object obj4 = 0;
									goto IL_0a69;
								}
							}
							if (_effect == ColorGradeEffect.MixerRedOutGreenIn)
							{
								ChannelMixer channelMixer15 = _channelMixer;
								if (channelMixer15.redOutGreenIn.overrideState)
								{
									ChannelMixer channelMixer16 = _channelMixer;
									floatParameter = channelMixer16.redOutGreenIn;
									object obj4 = 0;
									goto IL_0a69;
								}
							}
							if (_effect == ColorGradeEffect.MixerRedOutRedIn)
							{
								ChannelMixer channelMixer17 = _channelMixer;
								if (channelMixer17.redOutRedIn.overrideState)
								{
									colorAdjustments = (ColorAdjustments)(object)_channelMixer;
									object obj4 = 0;
									goto IL_0a57;
								}
							}
						}
					}
					goto IL_0dfb;
				}
			}
			if (_colorAdjustment != null)
			{
				ColorAdjustments colorAdjustment = _colorAdjustment;
				if (colorAdjustment.active)
				{
					if (_effect == ColorGradeEffect.PostExposure && colorAdjustment.postExposure.overrideState)
					{
						colorAdjustments = _colorAdjustment;
						object obj4 = 0;
						goto IL_0a57;
					}
					goto IL_0a9b;
				}
			}
		}
		else if (_liftGammaGain != null)
		{
			LiftGammaGain liftGammaGain2 = _liftGammaGain;
			if (liftGammaGain2.active)
			{
				if (_effect == ColorGradeEffect.Lift && liftGammaGain2.lift.overrideState)
				{
					liftGammaGain = _liftGammaGain;
					goto IL_0cdd;
				}
				if (_effect == ColorGradeEffect.Gamma)
				{
					LiftGammaGain liftGammaGain3 = _liftGammaGain;
					if (liftGammaGain3.gamma.overrideState)
					{
						LiftGammaGain liftGammaGain4 = _liftGammaGain;
						vector4Parameter = liftGammaGain4.gamma;
						goto IL_0cef;
					}
				}
				if (_effect == ColorGradeEffect.Gain)
				{
					LiftGammaGain liftGammaGain5 = _liftGammaGain;
					if (liftGammaGain5.gain.overrideState)
					{
						LiftGammaGain liftGammaGain6 = _liftGammaGain;
						vector4Parameter = liftGammaGain6.gain;
						goto IL_0cef;
					}
				}
			}
		}
		goto IL_0dfb;
		IL_0dfb:
		return _defaultValue;
		IL_0cdd:
		vector4Parameter = liftGammaGain.lift;
		goto IL_0cef;
		IL_0a9b:
		if (_effect == ColorGradeEffect.Saturation)
		{
			ColorAdjustments colorAdjustment2 = _colorAdjustment;
			if (colorAdjustment2.saturation.overrideState)
			{
				ColorAdjustments colorAdjustment3 = _colorAdjustment;
				floatParameter = colorAdjustment3.saturation;
				object obj4 = 0;
				goto IL_0a69;
			}
		}
		if (_effect == ColorGradeEffect.Contrast)
		{
			ColorAdjustments colorAdjustment4 = _colorAdjustment;
			if (colorAdjustment4.contrast.overrideState)
			{
				ColorAdjustments colorAdjustment5 = _colorAdjustment;
				floatParameter = colorAdjustment5.contrast;
				object obj4 = 0;
				goto IL_0a69;
			}
		}
		if (_effect == ColorGradeEffect.HueShift)
		{
			ColorAdjustments colorAdjustment6 = _colorAdjustment;
			if (colorAdjustment6.hueShift.overrideState)
			{
				ColorAdjustments colorAdjustment7 = _colorAdjustment;
				floatParameter = colorAdjustment7.hueShift;
				object obj4 = 0;
				goto IL_0a69;
			}
		}
		goto IL_0dfb;
		IL_0a69:
		nint num = (nint)floatParameter;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1821 @ rdx_v18 (Il2CppClass<UnityEngine.Rendering.FloatParameter>)+218]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1821 @ rdx_v18 (Il2CppClass<UnityEngine.Rendering.FloatParameter>)+220]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v1232 @ rax_v26 (should have been resolved before IL gen)");
		goto IL_0a9b;
		IL_0a57:
		floatParameter = colorAdjustments.postExposure;
		goto IL_0a69;
		IL_0cef:
		Vector4 value = vector4Parameter.value;
		float result = default(float);
		return result;
	}

	public override void Set(float value)
	{
		//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Expected O, but got Unknown
		//IL_0072: Expected O, but got I4
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Expected O, but got Unknown
		//IL_00ef: Expected O, but got I4
		VolumeParameter<Vector4> volumeParameter;
		ColorAdjustments colorAdjustments;
		if (_effect != ColorGradeEffect.Lift && _effect != ColorGradeEffect.Gamma && _effect != ColorGradeEffect.Gain)
		{
			if (_effect != ColorGradeEffect.PostExposure)
			{
				object obj = _effect + -2;
				object obj2 = obj & 0xFFFFFFEFL;
				if (obj2 != null && _effect != ColorGradeEffect.HueShift)
				{
					if (_effect != ColorGradeEffect.MixerBlueOutBlueIn)
					{
						object obj3 = _effect - 9;
						if ((nint)obj3 > 6 && _effect != ColorGradeEffect.MixerRedOutRedIn)
						{
							if (_effect != ColorGradeEffect.Temperature && _effect != ColorGradeEffect.Tint)
							{
								if (_effect == ColorGradeEffect.SMHShadows || _effect == ColorGradeEffect.SMHMidtones || _effect == ColorGradeEffect.SMHHighlights)
								{
									if (!(_shadowsMidtonesHighlights != null))
									{
										return;
									}
									ShadowsMidtonesHighlights shadowsMidtonesHighlights = _shadowsMidtonesHighlights;
									shadowsMidtonesHighlights.active = true;
									if (_effect != ColorGradeEffect.SMHShadows)
									{
										if (_effect != ColorGradeEffect.SMHMidtones)
										{
											if (_effect != ColorGradeEffect.SMHHighlights)
											{
												goto IL_0914;
											}
											ShadowsMidtonesHighlights shadowsMidtonesHighlights2 = _shadowsMidtonesHighlights;
											volumeParameter = shadowsMidtonesHighlights2.highlights;
											_ = 1065353216;
											_ = 1065353216;
											_ = 1065353216;
										}
										else
										{
											ShadowsMidtonesHighlights shadowsMidtonesHighlights3 = _shadowsMidtonesHighlights;
											volumeParameter = shadowsMidtonesHighlights3.midtones;
											_ = 1065353216;
											_ = 1065353216;
											_ = 1065353216;
										}
									}
									else
									{
										ShadowsMidtonesHighlights shadowsMidtonesHighlights4 = _shadowsMidtonesHighlights;
										volumeParameter = shadowsMidtonesHighlights4.shadows;
										_ = 1065353216;
										_ = 1065353216;
										_ = 1065353216;
									}
									goto IL_08f3;
								}
							}
							else
							{
								if (!(_whiteBalance != null))
								{
									return;
								}
								WhiteBalance whiteBalance = _whiteBalance;
								whiteBalance.active = true;
								if (_effect == ColorGradeEffect.Temperature)
								{
									WhiteBalance whiteBalance2 = _whiteBalance;
									volumeParameter = (VolumeParameter<Vector4>)(object)whiteBalance2.temperature;
									goto IL_03c5;
								}
								if (_effect == ColorGradeEffect.Tint)
								{
									colorAdjustments = (ColorAdjustments)(object)_whiteBalance;
									goto IL_03b3;
								}
							}
							goto IL_0914;
						}
					}
					if (!(_channelMixer != null))
					{
						return;
					}
					ChannelMixer channelMixer = _channelMixer;
					channelMixer.active = true;
					if (_effect != ColorGradeEffect.MixerBlueOutBlueIn)
					{
						if (_effect != ColorGradeEffect.MixerBlueOutGreenIn)
						{
							if (_effect != ColorGradeEffect.MixerBlueOutRedIn)
							{
								if (_effect != ColorGradeEffect.MixerGreenOutBlueIn)
								{
									if (_effect != ColorGradeEffect.MixerGreenOutGreenIn)
									{
										if (_effect != ColorGradeEffect.MixerGreenOutRedIn)
										{
											if (_effect != ColorGradeEffect.MixerRedOutBlueIn)
											{
												if (_effect == ColorGradeEffect.MixerRedOutGreenIn)
												{
													colorAdjustments = (ColorAdjustments)(object)_channelMixer;
													goto IL_03b3;
												}
												if (_effect != ColorGradeEffect.MixerRedOutRedIn)
												{
													goto IL_0914;
												}
												ChannelMixer channelMixer2 = _channelMixer;
												volumeParameter = (VolumeParameter<Vector4>)(object)channelMixer2.redOutRedIn;
											}
											else
											{
												ChannelMixer channelMixer3 = _channelMixer;
												volumeParameter = (VolumeParameter<Vector4>)(object)channelMixer3.redOutBlueIn;
											}
										}
										else
										{
											ChannelMixer channelMixer4 = _channelMixer;
											volumeParameter = (VolumeParameter<Vector4>)(object)channelMixer4.greenOutRedIn;
										}
									}
									else
									{
										ChannelMixer channelMixer5 = _channelMixer;
										volumeParameter = (VolumeParameter<Vector4>)(object)channelMixer5.greenOutGreenIn;
									}
								}
								else
								{
									ChannelMixer channelMixer6 = _channelMixer;
									volumeParameter = (VolumeParameter<Vector4>)(object)channelMixer6.greenOutBlueIn;
								}
							}
							else
							{
								ChannelMixer channelMixer7 = _channelMixer;
								volumeParameter = (VolumeParameter<Vector4>)(object)channelMixer7.blueOutRedIn;
							}
						}
						else
						{
							ChannelMixer channelMixer8 = _channelMixer;
							volumeParameter = (VolumeParameter<Vector4>)(object)channelMixer8.blueOutGreenIn;
						}
					}
					else
					{
						ChannelMixer channelMixer9 = _channelMixer;
						volumeParameter = (VolumeParameter<Vector4>)(object)channelMixer9.blueOutBlueIn;
					}
					goto IL_03c5;
				}
			}
			if (!(_colorAdjustment != null))
			{
				return;
			}
			ColorAdjustments colorAdjustment = _colorAdjustment;
			colorAdjustment.active = true;
			if (_effect != ColorGradeEffect.PostExposure)
			{
				if (_effect != ColorGradeEffect.Saturation)
				{
					if (_effect == ColorGradeEffect.Contrast)
					{
						colorAdjustments = _colorAdjustment;
						goto IL_03b3;
					}
					if (_effect != ColorGradeEffect.HueShift)
					{
						goto IL_0914;
					}
					ColorAdjustments colorAdjustment2 = _colorAdjustment;
					volumeParameter = (VolumeParameter<Vector4>)(object)colorAdjustment2.hueShift;
				}
				else
				{
					ColorAdjustments colorAdjustment3 = _colorAdjustment;
					volumeParameter = (VolumeParameter<Vector4>)(object)colorAdjustment3.saturation;
				}
			}
			else
			{
				ColorAdjustments colorAdjustment4 = _colorAdjustment;
				volumeParameter = (VolumeParameter<Vector4>)(object)colorAdjustment4.postExposure;
			}
			goto IL_03c5;
		}
		if (!(_liftGammaGain != null))
		{
			return;
		}
		LiftGammaGain liftGammaGain = _liftGammaGain;
		liftGammaGain.active = true;
		if (_effect != ColorGradeEffect.Lift)
		{
			if (_effect != ColorGradeEffect.Gamma)
			{
				if (_effect != ColorGradeEffect.Gain)
				{
					goto IL_0914;
				}
				LiftGammaGain liftGammaGain2 = _liftGammaGain;
				volumeParameter = liftGammaGain2.gain;
				_ = 1065353216;
				_ = 1065353216;
				_ = 1065353216;
			}
			else
			{
				LiftGammaGain liftGammaGain3 = _liftGammaGain;
				_ = 1065353216;
				_ = 1065353216;
				_ = 1065353216;
				volumeParameter = liftGammaGain3.gamma;
			}
		}
		else
		{
			LiftGammaGain liftGammaGain4 = _liftGammaGain;
			_ = 1065353216;
			_ = 1065353216;
			_ = 1065353216;
			volumeParameter = liftGammaGain4.lift;
		}
		goto IL_08f3;
		IL_0943:
		UnityEngine.Object x;
		volumeParameter.Override((Vector4)x);
		goto IL_0914;
		IL_08f3:
		object obj4 = default(object);
		x = (UnityEngine.Object)(obj4 - 32);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-20]");
		_ = 0;
		goto IL_0943;
		IL_03b3:
		volumeParameter = (VolumeParameter<Vector4>)(object)colorAdjustments.contrast;
		goto IL_03c5;
		IL_03c5:
		x = (UnityEngine.Object)(obj4 + 16);
		goto IL_0943;
		IL_0914:
		base.NotifyListenersIfChanged(value);
	}
}
