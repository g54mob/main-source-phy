using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public static class BatchingHelper
{
	public static bool forceEnableDepthBlend
	{
		get
		{
			//IL_0141: Expected I4, but got O
			//IL_014f: Expected O, but got I4
			//IL_0089: Expected O, but got I4
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				if (instance.m_RenderingMode != RenderingMode.SRPBatcher)
				{
					goto IL_00a6;
				}
				if (instance.m_RenderPipeline != RenderPipeline.BuiltIn)
				{
					RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
					object obj = projectRenderPipeline - 1;
					if ((nint)obj <= 1)
					{
						goto IL_00a6;
					}
				}
				goto IL_00ed;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00a6:
			if (instance.m_RenderPipeline != RenderPipeline.BuiltIn && instance.m_RenderingMode == RenderingMode.MultiPass)
			{
				goto IL_00ed;
			}
			RenderingMode renderingMode = instance.m_RenderingMode;
			if (instance.m_RenderingMode == RenderingMode.GPUInstancing)
			{
				return true;
			}
			goto IL_0141;
			IL_0141:
			object obj2 = renderingMode - 3;
			return obj2 == null;
			IL_00ed:
			renderingMode = RenderingMode.Default;
			goto IL_0141;
		}
	}

	public static bool IsGpuInstancingEnabled(Material material)
	{
		//IL_003d: Expected I4, but got O
		if ((object)material != null)
		{
			return material.enableInstancing;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static void SetMaterialProperties(Material material, bool enableGpuInstancing)
	{
		material.enableInstancing = enableGpuInstancing;
	}

	private unsafe static bool DoesRenderingModePreventBatching(ShaderMode shaderMode, ref string reasons)
	{
		//IL_0119: Expected O, but got I4
		//IL_020e: Expected I4, but got O
		//IL_0063: Expected O, but got I4
		//IL_023b: Expected I4, but got O
		Config instance = Config.GetInstance(true);
		if (instance.m_RenderingMode != RenderingMode.SRPBatcher)
		{
			goto IL_0080;
		}
		if (instance.m_RenderPipeline != RenderPipeline.BuiltIn)
		{
			RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
			object obj = projectRenderPipeline - 1;
			if ((nint)obj <= 1)
			{
				goto IL_0080;
			}
		}
		goto IL_01f8;
		IL_0080:
		if ((instance.m_RenderPipeline == RenderPipeline.BuiltIn || instance.m_RenderingMode != RenderingMode.MultiPass) && (shaderMode != ShaderMode.HD || instance.m_RenderingMode != RenderingMode.MultiPass))
		{
			object obj2 = instance.m_RenderingMode - 2;
			if ((nint)obj2 <= 1)
			{
				return false;
			}
		}
		goto IL_01f8;
		IL_01f8:
		RenderingMode renderingMode = default(RenderingMode);
		object arg = renderingMode;
		object obj3 = default(object);
		object arg2 = (RenderingMode)obj3;
		string text = $"Current Rendering Mode is '{arg}'. To enable batching, use '{arg2}'";
		ref string reference = ref *(string*)text;
		Config instance2 = Config.GetInstance(true);
		if ((object)instance2 != null)
		{
			if (instance2.m_RenderPipeline != RenderPipeline.BuiltIn)
			{
				object arg3 = renderingMode;
				string text2 = $" or '{arg3}'";
				string text3 = reasons + text2;
				reference = ref *(string*)text3;
			}
			return true;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static bool CanBeBatched(VolumetricLightBeamSD beamA, VolumetricLightBeamSD beamB, ref string reasons)
	{
		//IL_070b: Expected I4, but got O
		//IL_0081: Expected O, but got I4
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00db: Expected O, but got I
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_0126: Expected O, but got I
		//IL_0146: Expected O, but got I4
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Expected O, but got Unknown
		//IL_034d: Expected I4, but got O
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_0372: Expected I4, but got O
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_025a: Expected I4, but got O
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_027f: Expected I4, but got O
		//IL_03a8: Expected I, but got O
		//IL_03ba: Expected O, but got I4
		//IL_01a0: Expected I, but got O
		//IL_01b2: Expected O, but got I4
		//IL_02b5: Expected I, but got O
		//IL_02c7: Expected O, but got I4
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Expected O, but got Unknown
		//IL_043d: Expected I4, but got O
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Expected O, but got Unknown
		//IL_0462: Expected I4, but got O
		//IL_04f6: Invalid comparison between F4 and I4
		//IL_050a: Invalid comparison between F4 and I4
		//IL_0534: Expected O, but got I4
		//IL_0542: Invalid comparison between F4 and I4
		//IL_0556: Invalid comparison between F4 and I4
		//IL_057f: Expected O, but got I4
		//IL_0498: Expected I, but got O
		//IL_04aa: Expected O, but got I4
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Expected O, but got Unknown
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Expected O, but got Unknown
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Expected O, but got Unknown
		//IL_0693: Expected I4, but got O
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Expected O, but got Unknown
		//IL_06b8: Expected I4, but got O
		//IL_0613: Expected I, but got O
		//IL_0625: Expected O, but got I4
		bool result;
		object obj3 = default(object);
		if (!DoesRenderingModePreventBatching(ShaderMode.SD, ref reasons))
		{
			bool flag = CanBeBatched(beamA, ref reasons);
			bool flag2 = CanBeBatched(beamB, ref reasons);
			result = flag2 & flag;
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				bool flag3 = !instance.featureEnabledDynamicOcclusion;
				object obj = 0;
				if (flag3)
				{
					goto IL_072b;
				}
				if ((object)beamA != null)
				{
					object obj2 = obj3 + 48;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+30]");
					bool flag4 = (UnityEngine.Object)0 == null;
					if ((object)beamB != null)
					{
						object obj4 = obj3 + 48;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+30]");
						bool flag5 = (UnityEngine.Object)0 == null;
						bool flag6 = flag4 == flag5;
						obj = 0;
						if (!flag6)
						{
							string name = beamA.name;
							string name2 = beamB.name;
							string toAppend = $"{name}/{name2}: dynamically occluded and non occluded beams cannot be batched together";
							AppendErrorMessage(ref reasons, toAppend);
							nint num = unchecked((nint)null);
							result = false;
							obj = 0;
						}
						goto IL_072b;
					}
				}
			}
			goto IL_06fd;
		}
		return false;
		IL_072b:
		Config instance2 = Config.GetInstance(true);
		if ((object)instance2 != null)
		{
			if (instance2.featureEnabledColorGradient != FeatureEnabledColorGradient.Off)
			{
				if ((object)beamA != null && (object)beamB != null)
				{
					if (beamA.colorMode != beamB.colorMode)
					{
						object obj5 = obj3 + 48;
						_ = beamA.colorMode;
						object arg = (ColorMode)obj5;
						object obj6 = obj3 - 16;
						_ = beamB.colorMode;
						object arg2 = (ColorMode)obj6;
						string toAppend2 = $"'Color Mode' mismatch: {arg} / {arg2}";
						AppendErrorMessage(ref reasons, toAppend2);
						nint num = unchecked((nint)null);
						result = false;
						object obj = 0;
					}
					goto IL_0306;
				}
			}
			else if ((object)beamA != null && (object)beamB != null)
			{
				goto IL_0306;
			}
		}
		goto IL_06fd;
		IL_0306:
		if (beamA.blendingMode != beamB.blendingMode)
		{
			object obj7 = obj3 + 48;
			_ = beamA.blendingMode;
			object arg3 = (BlendingMode)obj7;
			object obj8 = obj3 - 16;
			_ = beamB.blendingMode;
			object arg4 = (BlendingMode)obj8;
			string toAppend3 = $"'Blending Mode' mismatch: {arg3} / {arg4}";
			AppendErrorMessage(ref reasons, toAppend3);
			nint num = unchecked((nint)null);
			result = false;
			object obj = 0;
		}
		Config instance3 = Config.GetInstance(true);
		if ((object)instance3 != null)
		{
			if (instance3.featureEnabledNoise3D)
			{
				bool isNoiseEnabled = beamA.isNoiseEnabled;
				bool isNoiseEnabled2 = beamB.isNoiseEnabled;
				if (isNoiseEnabled != isNoiseEnabled2)
				{
					object obj9 = obj3 + 48;
					_ = beamA.noiseMode;
					object arg5 = (NoiseMode)obj9;
					object obj10 = obj3 - 16;
					_ = beamB.noiseMode;
					object arg6 = (NoiseMode)obj10;
					string toAppend4 = $"'3D Noise' enabled mismatch: {arg5} / {arg6}";
					AppendErrorMessage(ref reasons, toAppend4);
					nint num = unchecked((nint)null);
					result = false;
					object obj = 0;
				}
			}
			Config instance4 = Config.GetInstance(true);
			if ((object)instance4 != null)
			{
				if (instance4.featureEnabledDepthBlend && !forceEnableDepthBlend)
				{
					bool flag7 = beamA.depthBlendDistance < 0f;
					bool flag8 = beamA.depthBlendDistance == 0f;
					bool flag9 = !flag7;
					bool flag10 = !flag8;
					object obj11 = flag10 & flag9;
					bool flag11 = beamB.depthBlendDistance < 0f;
					bool flag12 = beamB.depthBlendDistance == 0f;
					bool flag13 = !flag11;
					bool flag14 = !flag12;
					object obj12 = flag14 & flag13;
					if (obj11 != obj12)
					{
						object obj13 = obj3 + 48;
						_ = beamA.depthBlendDistance;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object obj14 = obj3 - 16;
						_ = beamB.depthBlendDistance;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg7 = default(object);
						object arg8 = default(object);
						string toAppend5 = $"'Opaque Geometry Blending' mismatch: {arg7} / {arg8}";
						AppendErrorMessage(ref reasons, toAppend5);
						nint num = unchecked((nint)null);
						result = false;
						object obj = 0;
					}
				}
				Config instance5 = Config.GetInstance(true);
				if ((object)instance5 != null)
				{
					if (instance5.featureEnabledShaderAccuracyHigh && beamA.shaderAccuracy != beamB.shaderAccuracy)
					{
						object obj15 = obj3 + 48;
						_ = beamA.shaderAccuracy;
						object arg9 = (ShaderAccuracy)obj15;
						object obj16 = obj3 - 16;
						_ = beamB.shaderAccuracy;
						object arg10 = (ShaderAccuracy)obj16;
						string toAppend6 = $"'Shader Accuracy' mismatch: {arg9} / {arg10}";
						AppendErrorMessage(ref reasons, toAppend6);
						result = false;
					}
					return result;
				}
			}
		}
		goto IL_06fd;
		IL_06fd:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static bool CanBeBatched(VolumetricLightBeamSD beam, ref string reasons)
	{
		//IL_0256: Expected I4, but got O
		//IL_006c: Expected O, but got I4
		Config instance = Config.GetInstance(true);
		if ((object)instance == null)
		{
			goto IL_0248;
		}
		if (instance.m_RenderingMode != RenderingMode.SRPBatcher)
		{
			goto IL_0092;
		}
		bool flag = instance.m_RenderPipeline == RenderPipeline.BuiltIn;
		bool result = true;
		if (!flag)
		{
			RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
			object obj = projectRenderPipeline - 1;
			bool flag2 = (nint)obj > 1;
			result = true;
			if (!flag2)
			{
				goto IL_0092;
			}
		}
		goto IL_0281;
		IL_0092:
		if (instance.m_RenderPipeline != RenderPipeline.BuiltIn)
		{
			bool flag3 = instance.m_RenderingMode == RenderingMode.MultiPass;
			result = true;
			if (flag3)
			{
				goto IL_0281;
			}
		}
		bool flag4 = instance.m_RenderingMode != RenderingMode.GPUInstancing;
		result = true;
		if (!flag4)
		{
			if ((object)beam == null)
			{
				goto IL_0248;
			}
			bool flag5 = beam.geomMeshType == MeshType.Shared;
			result = true;
			if (!flag5)
			{
				string name = beam.name;
				string toAppend = $"{name} is not using shared mesh";
				AppendErrorMessage(ref reasons, toAppend);
				result = false;
			}
		}
		goto IL_0281;
		IL_0281:
		Config instance2 = Config.GetInstance(true);
		if ((object)instance2 != null)
		{
			if (instance2.featureEnabledDynamicOcclusion)
			{
				if ((object)beam == null)
				{
					goto IL_0248;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				if (obj2 != null)
				{
					string name2 = beam.name;
					string toAppend2 = $"{name2} is using the DynamicOcclusion DepthBuffer feature";
					AppendErrorMessage(ref reasons, toAppend2);
					result = false;
				}
			}
			return result;
		}
		goto IL_0248;
		IL_0248:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static bool CanBeBatched(VolumetricLightBeamHD beamA, VolumetricLightBeamHD beamB, ref string reasons)
	{
		//IL_0697: Expected I4, but got O
		//IL_03df: Expected O, but got I4
		//IL_042a: Expected O, but got I4
		bool result;
		BlendingMode blendingMode = default(BlendingMode);
		BlendingMode blendingMode2 = default(BlendingMode);
		if (!DoesRenderingModePreventBatching(ShaderMode.HD, ref reasons))
		{
			bool flag = CanBeBatched(beamA, ref reasons);
			bool flag2 = CanBeBatched(beamB, ref reasons);
			result = flag2 & flag;
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				if (instance.featureEnabledColorGradient != FeatureEnabledColorGradient.Off)
				{
					if ((object)beamA != null)
					{
						Config instance2 = Config.GetInstance(true);
						if ((object)instance2 != null)
						{
							ColorMode colorMode = ((instance2.featureEnabledColorGradient != FeatureEnabledColorGradient.Off) ? beamA.m_ColorMode : ColorMode.Flat);
							if ((object)beamB != null)
							{
								Config instance3 = Config.GetInstance(true);
								if ((object)instance3 != null)
								{
									ColorMode colorMode2 = ((instance3.featureEnabledColorGradient != FeatureEnabledColorGradient.Off) ? beamB.m_ColorMode : ColorMode.Flat);
									if (colorMode != colorMode2)
									{
										Config instance4 = Config.GetInstance(true);
										ColorMode colorMode3 = ((instance4.featureEnabledColorGradient != FeatureEnabledColorGradient.Off) ? beamA.m_ColorMode : ColorMode.Flat);
										object arg = (ColorMode)blendingMode;
										Config instance5 = Config.GetInstance(true);
										bool flag3 = instance5.featureEnabledColorGradient == FeatureEnabledColorGradient.Off;
										ColorMode colorMode4 = ColorMode.Flat;
										if (!flag3)
										{
											colorMode4 = beamB.m_ColorMode;
										}
										object arg2 = (ColorMode)blendingMode2;
										string toAppend = $"'Color Mode' mismatch: {arg} / {arg2}";
										AppendErrorMessage(ref reasons, toAppend);
										blendingMode2 = (BlendingMode)colorMode4;
										blendingMode = (BlendingMode)colorMode3;
										result = false;
									}
									goto IL_026a;
								}
							}
						}
					}
				}
				else if ((object)beamA != null && (object)beamB != null)
				{
					goto IL_026a;
				}
			}
			goto IL_0689;
		}
		return false;
		IL_0689:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_026a:
		if (beamA.m_BlendingMode != beamB.m_BlendingMode)
		{
			object arg3 = blendingMode;
			object arg4 = blendingMode2;
			string toAppend2 = $"'Blending Mode' mismatch: {arg3} / {arg4}";
			AppendErrorMessage(ref reasons, toAppend2);
			blendingMode2 = beamB.m_BlendingMode;
			blendingMode = beamA.m_BlendingMode;
			result = false;
		}
		bool flag4 = beamA.m_AttenuationEquation == beamB.m_AttenuationEquation;
		AttenuationEquationHD attenuationEquationHD = (AttenuationEquationHD)blendingMode2;
		AttenuationEquationHD attenuationEquationHD2 = (AttenuationEquationHD)blendingMode;
		if (!flag4)
		{
			object arg5 = (AttenuationEquationHD)blendingMode;
			object arg6 = (AttenuationEquationHD)blendingMode2;
			string toAppend3 = $"'Attenuation Equation' mismatch: {arg5} / {arg6}";
			AppendErrorMessage(ref reasons, toAppend3);
			attenuationEquationHD = beamB.m_AttenuationEquation;
			attenuationEquationHD2 = beamA.m_AttenuationEquation;
			result = false;
		}
		Config instance6 = Config.GetInstance(true);
		if ((object)instance6 != null)
		{
			if (instance6.featureEnabledNoise3D)
			{
				bool flag5 = beamA.m_NoiseMode < NoiseMode.Disabled;
				bool flag6 = beamA.m_NoiseMode == NoiseMode.Disabled;
				bool flag7 = !flag5;
				bool flag8 = !flag6;
				object obj = flag8 & flag7;
				bool flag9 = beamB.m_NoiseMode < NoiseMode.Disabled;
				bool flag10 = beamB.m_NoiseMode == NoiseMode.Disabled;
				bool flag11 = !flag9;
				bool flag12 = !flag10;
				object obj2 = flag12 & flag11;
				if (obj != obj2)
				{
					object arg7 = (NoiseMode)attenuationEquationHD2;
					object arg8 = (NoiseMode)attenuationEquationHD;
					string toAppend4 = $"'3D Noise' enabled mismatch: {arg7} / {arg8}";
					AppendErrorMessage(ref reasons, toAppend4);
					result = false;
				}
			}
			if (beamA.m_RaymarchingQualityID == beamB.m_RaymarchingQualityID)
			{
				goto IL_07ad;
			}
			Config instance7 = Config.GetInstance(true);
			if ((object)instance7 != null)
			{
				int raymarchingQualityIndexForUniqueID = instance7.GetRaymarchingQualityIndexForUniqueID(beamA.m_RaymarchingQualityID);
				if (raymarchingQualityIndexForUniqueID >= 0)
				{
					RaymarchingQuality[] raymarchingQualities = instance7.m_RaymarchingQualities;
					if (instance7.m_RaymarchingQualities != null)
					{
						RaymarchingQuality raymarchingQuality = raymarchingQualities[raymarchingQualityIndexForUniqueID];
						if (raymarchingQualities[raymarchingQualityIndexForUniqueID] != null)
						{
							Config instance8 = Config.GetInstance(true);
							if ((object)instance8 != null)
							{
								int raymarchingQualityIndexForUniqueID2 = instance8.GetRaymarchingQualityIndexForUniqueID(beamB.m_RaymarchingQualityID);
								if (raymarchingQualityIndexForUniqueID2 >= 0)
								{
									RaymarchingQuality[] raymarchingQualities2 = instance8.m_RaymarchingQualities;
									if (instance8.m_RaymarchingQualities != null)
									{
										RaymarchingQuality raymarchingQuality2 = raymarchingQualities2[raymarchingQualityIndexForUniqueID2];
										if (raymarchingQualities2[raymarchingQualityIndexForUniqueID2] != null)
										{
											string toAppend5 = $"'Raymarching Quality' mismatch: {raymarchingQuality.name} / {raymarchingQuality2.name}";
											AppendErrorMessage(ref reasons, toAppend5);
											result = false;
											goto IL_07ad;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0689;
		IL_07ad:
		return result;
	}

	public static bool CanBeBatched(VolumetricLightBeamHD beam, ref string reasons)
	{
		//IL_0179: Expected I4, but got O
		Config instance = Config.GetInstance(true);
		if ((object)instance != null)
		{
			bool flag = !instance.featureEnabledShadow;
			bool result = true;
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (!flag)
			{
				if ((object)beam == null)
				{
					goto IL_016b;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				bool flag2 = obj != null;
				bool flag3 = !flag2;
				result = true;
				if (!flag3)
				{
					string name = beam.name;
					string toAppend = $"{name} is using the Shadow feature";
					AppendErrorMessage(ref reasons, toAppend);
					result = false;
				}
			}
			Config instance2 = Config.GetInstance(true);
			if ((object)instance2 != null)
			{
				if (instance2.featureEnabledCookie)
				{
					if ((object)beam == null)
					{
						goto IL_016b;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					if (obj != null)
					{
						string name2 = beam.name;
						string toAppend2 = $"{name2} is using the Cookie feature";
						AppendErrorMessage(ref reasons, toAppend2);
						result = false;
					}
				}
				return result;
			}
		}
		goto IL_016b;
		IL_016b:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static bool CanBeBatched(VolumetricLightBeamAbstractBase beamA, VolumetricLightBeamAbstractBase beamB, ref string reasons)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_015a: Expected I, but got O
		//IL_0162: Expected I, but got O
		//IL_0172: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_01ae: Expected O, but got I
		//IL_00b9: Expected I, but got O
		//IL_00c9: Expected O, but got I
		//IL_0200: Expected I, but got O
		//IL_0210: Expected O, but got I
		//IL_0105: Expected O, but got I
		//IL_024c: Expected O, but got I
		if ((object)beamA != null)
		{
			nint num = (nint)typeof(VolumetricLightBeamSD);
			nint num2 = (nint)beamA;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v2 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v144 @ rax_v11+FFFFFFF8+v47 @ rax_v4*8]");
				if (0 == (nint)typeof(VolumetricLightBeamSD) && (object)beamB != null)
				{
					nint num4 = (nint)beamB;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rdx_v2 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
					if (num5 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
						object obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rax_v13+FFFFFFF8+v159 @ rax_v12*8]");
						if (0 == (nint)typeof(VolumetricLightBeamSD))
						{
							return CanBeBatched((VolumetricLightBeamSD)beamA, (VolumetricLightBeamSD)beamB, ref reasons);
						}
					}
				}
			}
			nint num6 = (nint)typeof(VolumetricLightBeamHD);
			nint num7 = (nint)beamA;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r8_v4 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num8 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r8_v4 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v7+FFFFFFF8+v66 @ rax_v6*8]");
				if (0 == (nint)typeof(VolumetricLightBeamHD) && (object)beamB != null)
				{
					nint num9 = (nint)beamB;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
					nint num10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v3 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
					if (num10 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r8_v5 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
						object obj8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v9+FFFFFFF8+v68 @ rax_v8*8]");
						if (0 == (nint)typeof(VolumetricLightBeamHD))
						{
							return CanBeBatched((VolumetricLightBeamHD)beamA, (VolumetricLightBeamHD)beamB, ref reasons);
						}
					}
				}
			}
		}
		return false;
	}

	private unsafe static void AppendErrorMessage(ref string message, string toAppend)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C56]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		ref string reference;
		if (message != "")
		{
			string text = message + "\n";
			reference = ref *(string*)text;
		}
		string text2 = message + "- " + toAppend;
		reference = ref *(string*)text2;
	}
}
