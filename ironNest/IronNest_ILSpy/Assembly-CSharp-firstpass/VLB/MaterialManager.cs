using System;
using System.Collections;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace VLB;

public static class MaterialManager
{
	public enum BlendingMode
	{
		Additive,
		SoftAdditive,
		TraditionalTransparency,
		Count
	}

	public enum ColorGradient
	{
		Off,
		MatrixLow,
		MatrixHigh,
		Count
	}

	public enum Noise3D
	{
		Off,
		On,
		Count
	}

	public static class SD
	{
		public enum DepthBlend
		{
			Off,
			On,
			Count
		}

		public enum DynamicOcclusion
		{
			Off,
			ClippingPlane,
			DepthTexture,
			Count
		}

		public enum MeshSkewing
		{
			Off,
			On,
			Count
		}

		public enum ShaderAccuracy
		{
			Fast,
			High,
			Count
		}
	}

	public static class HD
	{
		public enum Attenuation
		{
			Linear,
			Quadratic,
			Count
		}

		public enum Shadow
		{
			Off,
			On,
			Count
		}

		public enum Cookie
		{
			Off,
			SingleChannel,
			RGBA,
			Count
		}
	}

	private interface IStaticProperties
	{
		int GetPropertiesCount();

		int GetMaterialID();

		void ApplyToMaterial(Material mat);

		ShaderMode GetShaderMode();
	}

	public struct StaticPropertiesSD : IStaticProperties
	{
		public BlendingMode blendingMode;

		public Noise3D noise3D;

		public SD.DepthBlend depthBlend;

		public ColorGradient colorGradient;

		public SD.DynamicOcclusion dynamicOcclusion;

		public SD.MeshSkewing meshSkewing;

		public SD.ShaderAccuracy shaderAccuracy;

		public static int staticPropertiesCount => 432;

		private int blendingModeID => (int)blendingMode;

		private int noise3DID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledNoise3D)
					{
						return (int)noise3D;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int depthBlendID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledDepthBlend)
					{
						return (int)depthBlend;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int colorGradientID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledColorGradient != FeatureEnabledColorGradient.Off)
					{
						return (int)colorGradient;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int dynamicOcclusionID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledDynamicOcclusion)
					{
						return (int)dynamicOcclusion;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int meshSkewingID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledMeshSkewing)
					{
						return (int)meshSkewing;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int shaderAccuracyID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledShaderAccuracyHigh)
					{
						return (int)shaderAccuracy;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		public ShaderMode GetShaderMode()
		{
			return ShaderMode.SD;
		}

		public int GetPropertiesCount()
		{
			return 432;
		}

		public int GetMaterialID()
		{
			//IL_01ae: Expected I4, but got O
			//IL_0295: Expected O, but got I4
			//IL_029d: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a2: Expected O, but got Unknown
			//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b0: Expected O, but got Unknown
			//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bd: Expected O, but got Unknown
			//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cb: Expected O, but got Unknown
			//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d8: Expected O, but got Unknown
			//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f3: Expected O, but got Unknown
			//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0300: Expected O, but got Unknown
			//IL_0316: Unknown result type (might be due to invalid IL or missing references)
			//IL_031b: Expected O, but got Unknown
			//IL_0323: Unknown result type (might be due to invalid IL or missing references)
			//IL_0328: Expected O, but got Unknown
			//IL_0331: Unknown result type (might be due to invalid IL or missing references)
			//IL_0336: Expected O, but got Unknown
			//IL_033e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0343: Expected I4, but got Unknown
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				Noise3D noise3D = (instance.featureEnabledNoise3D ? this.noise3D : Noise3D.Off);
				Config instance2 = Config.GetInstance(true);
				if ((object)instance2 != null)
				{
					SD.DepthBlend depthBlend = (instance2.featureEnabledDepthBlend ? this.depthBlend : SD.DepthBlend.Off);
					Config instance3 = Config.GetInstance(true);
					if ((object)instance3 != null)
					{
						ColorGradient colorGradient = ((instance3.featureEnabledColorGradient != FeatureEnabledColorGradient.Off) ? this.colorGradient : ColorGradient.Off);
						Config instance4 = Config.GetInstance(true);
						if ((object)instance4 != null)
						{
							SD.DynamicOcclusion dynamicOcclusion = (instance4.featureEnabledDynamicOcclusion ? this.dynamicOcclusion : SD.DynamicOcclusion.Off);
							Config instance5 = Config.GetInstance(true);
							if ((object)instance5 != null)
							{
								SD.MeshSkewing meshSkewing = (instance5.featureEnabledMeshSkewing ? this.meshSkewing : SD.MeshSkewing.Off);
								Config instance6 = Config.GetInstance(true);
								if ((object)instance6 != null)
								{
									bool flag = !instance6.featureEnabledShaderAccuracyHigh;
									SD.ShaderAccuracy shaderAccuracy = SD.ShaderAccuracy.Fast;
									if (!flag)
									{
										shaderAccuracy = this.shaderAccuracy;
									}
									object obj = (int)blendingMode * 2;
									object obj2 = obj + noise3D;
									object obj3 = obj2 * 2;
									object obj4 = depthBlend + obj3;
									object obj5 = obj4 * 2;
									object obj6 = colorGradient + obj5;
									object obj7 = obj4 + obj6;
									object obj8 = obj7 * 2;
									object obj9 = dynamicOcclusion + obj8;
									object obj10 = obj7 + obj9;
									object obj11 = obj10 * 2;
									object obj12 = meshSkewing + obj11;
									object obj13 = obj12 * 2;
									return (int)(shaderAccuracy + obj13);
								}
							}
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}

		public void ApplyToMaterial(Material mat)
		{
			//IL_0050: Expected O, but got I4
			//IL_007e: Expected O, but got I4
			//IL_00a7: Expected O, but got I4
			//IL_00d5: Expected O, but got I4
			//IL_00fe: Expected O, but got I4
			//IL_012c: Expected O, but got I4
			//IL_0155: Expected O, but got I4
			//IL_0183: Expected O, but got I4
			bool[] blendingMode_AlphaAsBlack = BlendingMode_AlphaAsBlack;
			BlendingMode blendingMode = this.blendingMode;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v9 (VLB.MaterialManager+BlendingMode)+20+v52 @ rcx_v3 (System.Boolean[])]");
			bool flag = (nint)0 == 0;
			bool flag2 = !flag;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj = colorGradient - 1;
			bool flag3 = obj == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj2 = colorGradient - 2;
			bool flag4 = obj2 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj3 = depthBlend - 1;
			bool flag5 = obj3 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj4 = noise3D - 1;
			bool flag6 = obj4 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj5 = dynamicOcclusion - 1;
			bool flag7 = obj5 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj6 = dynamicOcclusion - 2;
			bool flag8 = obj6 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj7 = meshSkewing - 1;
			bool flag9 = obj7 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj8 = shaderAccuracy - 1;
			bool flag10 = obj8 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			BlendMode[] blendingMode_SrcFactor = BlendingMode_SrcFactor;
			BlendingMode blendingMode2 = this.blendingMode;
			int blendSrcFactor = ShaderProperties.BlendSrcFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rax_v23 (UnityEngine.Rendering.BlendMode[])+20+v90 @ rcx_v17 (VLB.MaterialManager+BlendingMode)*4]");
			mat.SetInt(blendSrcFactor, 0);
			BlendMode[] blendingMode_DstFactor = BlendingMode_DstFactor;
			BlendingMode blendingMode3 = this.blendingMode;
			int blendDstFactor = ShaderProperties.BlendDstFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v27 (UnityEngine.Rendering.BlendMode[])+20+v158 @ rcx_v21 (VLB.MaterialManager+BlendingMode)*4]");
			mat.SetInt(blendDstFactor, 0);
			mat.SetInt(ShaderProperties.ZTest, 4);
		}
	}

	public struct StaticPropertiesHD : IStaticProperties
	{
		public BlendingMode blendingMode;

		public HD.Attenuation attenuation;

		public Noise3D noise3D;

		public ColorGradient colorGradient;

		public HD.Shadow shadow;

		public HD.Cookie cookie;

		public int raymarchingQualityIndex;

		public static int staticPropertiesCount
		{
			get
			{
				//IL_00d4: Expected I4, but got O
				//IL_00aa: Expected O, but got I4
				//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
				//IL_00f5: Expected I4, but got Unknown
				//IL_0074: Expected O, but got I4
				//IL_00c1: Expected O, but got I4
				//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
				//IL_00e2: Expected I4, but got Unknown
				//IL_008c: Expected O, but got I4
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.m_RaymarchingQualities != null)
					{
						RaymarchingQuality[] raymarchingQualities = instance.m_RaymarchingQualities;
						bool flag = raymarchingQualities.Length < 1;
						object obj = 1;
						if (!flag)
						{
							obj = raymarchingQualities.Length;
						}
						return obj * 216;
					}
					bool flag2 = 1 < 1;
					object obj2 = 1;
					if (!flag2)
					{
						obj2 = 1;
					}
					return obj2 * 216;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int blendingModeID => (int)blendingMode;

		private int attenuationID => (int)attenuation;

		private int noise3DID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledNoise3D)
					{
						return (int)noise3D;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int colorGradientID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledColorGradient != FeatureEnabledColorGradient.Off)
					{
						return (int)colorGradient;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int dynamicOcclusionID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledShadow)
					{
						return (int)shadow;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int cookieID
		{
			get
			{
				//IL_0068: Expected I4, but got O
				Config instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.featureEnabledCookie)
					{
						return (int)cookie;
					}
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}

		private int raymarchingQualityID => raymarchingQualityIndex;

		public ShaderMode GetShaderMode()
		{
			return ShaderMode.HD;
		}

		public int GetPropertiesCount()
		{
			//IL_00d4: Expected I4, but got O
			//IL_00aa: Expected O, but got I4
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Expected I4, but got Unknown
			//IL_0074: Expected O, but got I4
			//IL_00c1: Expected O, but got I4
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected I4, but got Unknown
			//IL_008c: Expected O, but got I4
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				if (instance.m_RaymarchingQualities != null)
				{
					RaymarchingQuality[] raymarchingQualities = instance.m_RaymarchingQualities;
					bool flag = raymarchingQualities.Length < 1;
					object obj = 1;
					if (!flag)
					{
						obj = raymarchingQualities.Length;
					}
					return obj * 216;
				}
				bool flag2 = 1 < 1;
				object obj2 = 1;
				if (!flag2)
				{
					obj2 = 1;
				}
				return obj2 * 216;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}

		public int GetMaterialID()
		{
			//IL_0189: Expected I4, but got O
			//IL_0169: Expected O, but got I4
			//IL_0245: Expected O, but got I4
			//IL_024f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0254: Expected O, but got Unknown
			//IL_025d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0262: Expected O, but got Unknown
			//IL_026a: Unknown result type (might be due to invalid IL or missing references)
			//IL_026f: Expected O, but got Unknown
			//IL_0278: Unknown result type (might be due to invalid IL or missing references)
			//IL_027d: Expected O, but got Unknown
			//IL_0285: Unknown result type (might be due to invalid IL or missing references)
			//IL_028a: Expected O, but got Unknown
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Expected O, but got Unknown
			//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b2: Expected O, but got Unknown
			//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c0: Expected O, but got Unknown
			//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cd: Expected O, but got Unknown
			//IL_02f2: Expected O, but got I4
			//IL_015b: Expected O, but got I4
			//IL_0317: Unknown result type (might be due to invalid IL or missing references)
			//IL_031c: Expected I4, but got Unknown
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				Noise3D noise3D = (instance.featureEnabledNoise3D ? this.noise3D : Noise3D.Off);
				Config instance2 = Config.GetInstance(true);
				if ((object)instance2 != null)
				{
					ColorGradient colorGradient = ((instance2.featureEnabledColorGradient != FeatureEnabledColorGradient.Off) ? this.colorGradient : ColorGradient.Off);
					Config instance3 = Config.GetInstance(true);
					if ((object)instance3 != null)
					{
						HD.Shadow shadow = (instance3.featureEnabledShadow ? this.shadow : HD.Shadow.Off);
						Config instance4 = Config.GetInstance(true);
						if ((object)instance4 != null)
						{
							bool flag = !instance4.featureEnabledCookie;
							HD.Cookie cookie = HD.Cookie.Off;
							if (!flag)
							{
								cookie = this.cookie;
							}
							Config instance5 = Config.GetInstance(true);
							if ((object)instance5 != null)
							{
								object obj;
								if (instance5.m_RaymarchingQualities != null)
								{
									RaymarchingQuality[] raymarchingQualities = instance5.m_RaymarchingQualities;
									obj = raymarchingQualities.Length;
								}
								else
								{
									obj = 1;
								}
								object obj2 = (int)blendingMode * 2;
								object obj3 = attenuation + obj2;
								object obj4 = obj3 * 2;
								object obj5 = noise3D + obj4;
								object obj6 = obj5 * 2;
								object obj7 = colorGradient + obj6;
								object obj8 = obj5 + obj7;
								object obj9 = obj8 * 2;
								object obj10 = shadow + obj9;
								object obj11 = obj10 * 2;
								object obj12 = cookie + obj11;
								object obj13 = obj10 + obj12;
								bool flag2 = (nint)obj < 1;
								object obj14 = 1;
								if (!flag2)
								{
									obj14 = obj;
								}
								object obj15 = obj13 * obj14;
								return obj15 + raymarchingQualityIndex;
							}
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}

		public void ApplyToMaterial(Material mat)
		{
			//IL_0070: Expected O, but got I4
			//IL_0099: Expected O, but got I4
			//IL_00c7: Expected O, but got I4
			//IL_00f0: Expected O, but got I4
			//IL_011e: Expected O, but got I4
			//IL_0147: Expected O, but got I4
			//IL_0175: Expected O, but got I4
			//IL_01ff: Expected O, but got I4
			//IL_0314: Expected O, but got I4
			//IL_01f1: Expected O, but got I4
			//IL_022d: Expected O, but got I4
			bool[] blendingMode_AlphaAsBlack = BlendingMode_AlphaAsBlack;
			BlendingMode blendingMode = this.blendingMode;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v7 (VLB.MaterialManager+BlendingMode)+20+v56 @ rcx_v3 (System.Boolean[])]");
			bool flag = (nint)0 == 0;
			bool flag2 = !flag;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			bool flag3 = attenuation == HD.Attenuation.Linear;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj = attenuation - 1;
			bool flag4 = obj == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj2 = colorGradient - 1;
			bool flag5 = obj2 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj3 = colorGradient - 2;
			bool flag6 = obj3 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj4 = noise3D - 1;
			bool flag7 = obj4 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj5 = shadow - 1;
			bool flag8 = obj5 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj6 = cookie - 1;
			bool flag9 = obj6 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			object obj7 = cookie - 2;
			bool flag10 = obj7 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
			Config instance = Config.GetInstance(true);
			int num = 0;
			int num2 = 0;
			while (true)
			{
				object obj8;
				if (instance.m_RaymarchingQualities != null)
				{
					RaymarchingQuality[] raymarchingQualities = instance.m_RaymarchingQualities;
					obj8 = raymarchingQualities.Length;
				}
				else
				{
					obj8 = 1;
				}
				bool flag11 = (nint)obj8 < 1;
				object obj9 = 1;
				if (!flag11)
				{
					obj9 = obj8;
				}
				if (num < (nint)obj9)
				{
					string raymarchingQuality = ShaderKeywords.HD.GetRaymarchingQuality(num2);
					object obj10 = raymarchingQualityIndex - num2;
					bool flag12 = obj10 == null;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
					num2++;
					instance = Config.GetInstance(true);
					num = num2;
					continue;
				}
				break;
			}
			BlendMode[] blendingMode_SrcFactor = BlendingMode_SrcFactor;
			BlendingMode blendingMode2 = this.blendingMode;
			int blendSrcFactor = ShaderProperties.BlendSrcFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rcx_v21 (UnityEngine.Rendering.BlendMode[])+20+v114 @ rax_v28 (VLB.MaterialManager+BlendingMode)*4]");
			mat.SetInt(blendSrcFactor, 0);
			BlendMode[] blendingMode_DstFactor = BlendingMode_DstFactor;
			BlendingMode blendingMode3 = this.blendingMode;
			int blendDstFactor = ShaderProperties.BlendDstFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v32 (UnityEngine.Rendering.BlendMode[])+20+v215 @ rcx_v25 (VLB.MaterialManager+BlendingMode)*4]");
			mat.SetInt(blendDstFactor, 0);
			mat.SetInt(ShaderProperties.ZTest, 8);
		}
	}

	private class MaterialsGroup
	{
		public Material[] materials;

		public MaterialsGroup(int count)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			Material[] array = new Material[count];
			materials = array;
		}
	}

	private enum ZWrite
	{
		Off,
		On
	}

	public static MaterialPropertyBlock materialPropertyBlock;

	private static readonly BlendMode[] BlendingMode_SrcFactor;

	private static readonly BlendMode[] BlendingMode_DstFactor;

	private static readonly bool[] BlendingMode_AlphaAsBlack;

	private static Hashtable ms_MaterialsGroupSD;

	private static Hashtable ms_MaterialsGroupHD;

	public static Material NewMaterialPersistent(Shader shader, bool gpuInstanced)
	{
		if ((bool)shader)
		{
			Material material = new Material(shader);
			if ((object)material != null)
			{
				material.enableInstancing = gpuInstanced;
				return material;
			}
			return (Material)(object)new NullReferenceException();
		}
		Debug.LogError("Invalid VLB Shader. Please try to reset the VLB Config asset or reinstall the plugin.");
		return null;
	}

	public static Material GetInstancedMaterial(uint groupID, ref StaticPropertiesSD staticProps)
	{
		object obj = default(object);
		IStaticProperties staticProperties = (StaticPropertiesSD)obj;
		IStaticProperties staticProps2 = default(IStaticProperties);
		return GetInstancedMaterial(ms_MaterialsGroupSD, groupID, ref staticProps2);
	}

	public static Material GetInstancedMaterial(uint groupID, ref StaticPropertiesHD staticProps)
	{
		object obj = default(object);
		IStaticProperties staticProperties = (StaticPropertiesHD)obj;
		IStaticProperties staticProps2 = default(IStaticProperties);
		return GetInstancedMaterial(ms_MaterialsGroupHD, groupID, ref staticProps2);
	}

	private unsafe static Material GetInstancedMaterial(Hashtable groups, uint groupID, ref IStaticProperties staticProps)
	{
		//IL_0520: Expected O, but got I
		//IL_000d: Expected I, but got O
		//IL_003c: Expected I, but got O
		//IL_00f4: Expected I, but got O
		//IL_0104: Expected O, but got I
		//IL_0138: Expected I, but got O
		//IL_015e: Expected O, but got I
		//IL_01a3: Expected I, but got O
		//IL_01b9: Expected I, but got O
		//IL_021d: Expected I, but got O
		//IL_00b6: Expected I, but got O
		//IL_00d7: Expected O, but got I
		//IL_0263: Expected I, but got O
		//IL_029b: Expected I, but got O
		//IL_0324: Expected I, but got O
		//IL_0359: Expected I, but got O
		//IL_03d6: Expected I, but got O
		//IL_03db: Expected I, but got O
		//IL_0406: Expected I, but got O
		//IL_0497: Expected I, but got O
		//IL_041c: Expected I, but got O
		//IL_045a: Expected I, but got O
		//IL_04d1: Expected I, but got O
		//IL_04d9: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50290]");
		Hashtable hashtable = (Hashtable)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		nint num = (nint)groups;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v6 (Il2CppClass<System.Collections.Hashtable>)+308]");
		nint num2 = 0;
		object key = default(object);
		object obj = groups.get_Item(key);
		nint num3 = (nint)typeof(MaterialsGroup);
		ref IStaticProperties reference = default(ref IStaticProperties);
		MaterialsGroup materialsGroup2;
		if (obj == null)
		{
			bool flag = reference == null;
			object obj2 = default(object);
			nint num4 = (nint)(&obj2);
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				MaterialsGroup materialsGroup = new MaterialsGroup(0);
				object obj3 = default(object);
				Material[] materials = new Material[obj3];
				materialsGroup.materials = materials;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				nint num5 = (nint)groups;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ r9_v12 (Il2CppClass<System.Collections.Hashtable>)+320]");
				nint num6 = 0;
				IntPtr intPtr = default(IntPtr);
				groups.set_Item((object)(nint)intPtr, (object)materialsGroup);
				materialsGroup2 = materialsGroup;
				nint num7 = intPtr;
				goto IL_01cf;
			}
			throw new NullReferenceException();
		}
		nint num8 = (nint)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rdx_v9 (Il2CppClass<VLB.MaterialManager+MaterialsGroup>)+130]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ r8_v19 (Il2CppClass<System.Object>)+130]");
		nint num9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rdx_v9 (Il2CppClass<VLB.MaterialManager+MaterialsGroup>)+130]");
		bool flag2 = num9 < 0;
		MaterialsGroup materialsGroup3 = (MaterialsGroup)obj;
		nint num10 = (nint)typeof(MaterialsGroup);
		ref IStaticProperties reference2 = ref *(IStaticProperties*)num8;
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ r8_v19 (Il2CppClass<System.Object>)+C8]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rax_v47+FFFFFFF8+v86 @ rax_v46*8]");
			bool flag3 = 0 != (nint)typeof(MaterialsGroup);
			materialsGroup2 = (MaterialsGroup)obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v6 (Il2CppClass<System.Collections.Hashtable>)+308]");
			nint num6 = 0;
			nint num7 = (nint)typeof(MaterialsGroup);
			materialsGroup3 = (MaterialsGroup)obj;
			num10 = (nint)typeof(MaterialsGroup);
			reference2 = ref *(IStaticProperties*)num8;
			if (!flag3)
			{
				goto IL_01cf;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		Material result = default(Material);
		return result;
		IL_01cf:
		bool flag4 = reference == null;
		reference2 = ref *(IStaticProperties*)reference;
		UnityEngine.Object result2;
		if (!flag4)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag5 = materialsGroup2 == null;
			nint num7 = (nint)typeof(IStaticProperties);
			reference2 = ref *(IStaticProperties*)reference;
			if (!flag5)
			{
				Material[] materials2 = materialsGroup2.materials;
				bool flag6 = materialsGroup2.materials == null;
				num7 = (nint)typeof(IStaticProperties);
				reference2 = ref *(IStaticProperties*)reference;
				if (!flag6)
				{
					object obj6 = default(object);
					bool flag7 = (nint)obj6 >= materials2.Length;
					nint num11 = (nint)typeof(IStaticProperties);
					reference2 = ref *(IStaticProperties*)reference;
					if (flag7)
					{
						goto IL_0596;
					}
					result2 = materials2[obj6];
					if (!(materials2[obj6] == null))
					{
						goto IL_05aa;
					}
					Config instance = Config.GetInstance(true);
					reference2 = ref *(IStaticProperties*)reference;
					bool flag8 = reference == null;
					num7 = unchecked((nint)null);
					if (!flag8)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						bool flag9 = (object)instance == null;
						num7 = (nint)typeof(IStaticProperties);
						if (!flag9)
						{
							ShaderMode mode = default(ShaderMode);
							Material material = instance.NewMaterialTransient(mode, gpuInstanced: true);
							bool flag10 = material;
							bool flag11 = !flag10;
							result2 = material;
							if (flag11)
							{
								goto IL_05aa;
							}
							materialsGroup2 = (MaterialsGroup)(object)materialsGroup2.materials;
							bool flag12 = materialsGroup2.materials == null;
							nint num6 = unchecked((nint)null);
							num7 = unchecked((nint)null);
							reference2 = ref *(IStaticProperties*)1;
							if (!flag12)
							{
								bool flag13 = (object)material == null;
								num11 = unchecked((nint)null);
								if (!flag13)
								{
									nint num12 = (nint)materialsGroup2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v574 @ rdx_v24 (Il2CppClass<VLB.MaterialManager+MaterialsGroup>)+40]");
									nint num4 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj7 = default(object);
									bool flag14 = obj7 == null;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v574 @ rdx_v24 (Il2CppClass<VLB.MaterialManager+MaterialsGroup>)+40]");
									num11 = 0;
									num6 = unchecked((nint)null);
									reference2 = ref *(IStaticProperties*)1;
									hashtable = (Hashtable)(object)material;
									if (flag14)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj8 = default(object);
										throw obj8;
									}
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rdi_v3 (VLB.MaterialManager+MaterialsGroup)+18]");
								bool flag15 = (nint)obj6 >= 0;
								num6 = unchecked((nint)null);
								reference2 = ref *(IStaticProperties*)1;
								if (flag15)
								{
									goto IL_0596;
								}
								materialsGroup2 = (MaterialsGroup)reference;
								bool flag16 = reference == null;
								num6 = unchecked((nint)null);
								num7 = (nint)material;
								reference2 = ref *(IStaticProperties*)1;
								if (!flag16)
								{
									reference.ApplyToMaterial(material);
									result2 = material;
									goto IL_05aa;
								}
							}
						}
					}
				}
			}
		}
		throw new NullReferenceException();
		IL_0596:
		throw new IndexOutOfRangeException();
		IL_05aa:
		return (Material)result2;
	}

	public unsafe static bool EnableGPUInstancing(ShaderMode shaderMode, bool enabled)
	{
		//IL_0531: Expected I, but got O
		//IL_0063: Expected O, but got I4
		//IL_05c7: Expected I, but got O
		//IL_015d: Expected I, but got O
		//IL_01a9: Expected O, but got Ref
		//IL_01b8: Expected I, but got O
		//IL_045c: Expected O, but got Ref
		//IL_01f8: Expected I, but got O
		//IL_021e: Expected I, but got O
		//IL_024c: Expected O, but got I
		//IL_0290: Expected I, but got O
		//IL_02ae: Expected O, but got I
		//IL_02e3: Expected I, but got O
		//IL_0301: Expected O, but got I
		//IL_032e: Expected I, but got O
		//IL_0337: Expected O, but got I4
		//IL_034d: Expected I, but got O
		//IL_03cf: Expected O, but got I
		//IL_03e3: Expected I, but got O
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Expected O, but got Unknown
		//IL_0415: Expected I, but got O
		//IL_0438: Expected O, but got I
		//IL_0447: Expected I, but got O
		Config instance = Config.GetInstance(true);
		bool flag = (object)instance == null;
		nint num = unchecked((nint)null);
		if (!flag)
		{
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
			goto IL_049f;
		}
		goto IL_04ca;
		IL_0080:
		if ((instance.m_RenderPipeline != RenderPipeline.BuiltIn && instance.m_RenderingMode == RenderingMode.MultiPass) || (shaderMode == ShaderMode.HD && instance.m_RenderingMode == RenderingMode.MultiPass) || instance.m_RenderingMode != RenderingMode.GPUInstancing)
		{
			goto IL_049f;
		}
		Hashtable hashtable = ((shaderMode != ShaderMode.SD) ? ms_MaterialsGroupHD : ms_MaterialsGroupSD);
		bool flag2 = hashtable == null;
		num = unchecked((nint)null);
		if (!flag2)
		{
			nint num2 = (nint)hashtable;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v684 @ rdx_v11 (Il2CppClass<System.Collections.Hashtable>)+3A0]");
			num = 0;
			ICollection values = hashtable.Values;
			if (values != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj3 = default(object);
				object obj2 = (object)(&obj3);
				bool result = false;
				nint num3 = unchecked((nint)null);
				object obj4 = default(object);
				object obj5 = default(object);
				IntPtr intPtr = default(IntPtr);
				object obj13 = default(object);
				while (true)
				{
					if (obj4 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj5 != null)
						{
							bool flag3 = obj4 == null;
							num3 = unchecked((nint)null);
							if (!flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
								nint num4 = (nint)typeof(MaterialsGroup);
								bool flag4 = intPtr == (IntPtr)0;
								num3 = 1;
								if (flag4)
								{
									continue;
								}
								object obj6 = (nint)intPtr;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ rdx_v20 (Il2CppClass<VLB.MaterialManager+MaterialsGroup>)+130]");
								nint num5 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v506 @ r9_v8+130]");
								nint num6 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ rdx_v20 (Il2CppClass<VLB.MaterialManager+MaterialsGroup>)+130]");
								bool flag5 = num6 < 0;
								IntPtr intPtr2 = intPtr;
								num = (nint)typeof(MaterialsGroup);
								if (!flag5)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v506 @ r9_v8+C8]");
									object obj7 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ rax_v39+FFFFFFF8+v439 @ rax_v38 (Il2CppMethodInfo)*8]");
									bool flag6 = 0 != (nint)typeof(MaterialsGroup);
									intPtr2 = intPtr;
									num = (nint)typeof(MaterialsGroup);
									if (!flag6)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v693 @ rax_v37 (Il2CppMethodInfo)+10]");
										object obj8 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v693 @ rax_v37 (Il2CppMethodInfo)+10]");
										bool flag7 = (nint)0 == 0;
										nint num7 = intPtr;
										nint num8 = (nint)typeof(MaterialsGroup);
										object obj9 = 0;
										intPtr2 = intPtr;
										num = (nint)typeof(MaterialsGroup);
										if (!flag7)
										{
											while (true)
											{
												object obj10 = obj9;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rsi_v10+18]");
												if ((nint)obj10 >= 0)
												{
													break;
												}
												object obj11 = obj9;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rsi_v10+18]");
												bool flag8 = (nint)obj11 >= 0;
												intPtr2 = num7;
												num = num8;
												if (!flag8)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rsi_v10+20+v190 @ rbx_v14*8]");
													bool flag9 = (UnityEngine.Object)0;
													bool flag10 = !flag9;
													num8 = unchecked((nint)null);
													if (!flag10)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rsi_v10+20+v190 @ rbx_v14*8]");
														bool flag11 = (nint)0 == 0;
														intPtr2 = num7;
														num = unchecked((nint)null);
														if (flag11)
														{
															throw new NullReferenceException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rsi_v10+20+v190 @ rbx_v14*8]");
														((Material)0).enableInstancing = enabled;
														result = true;
														num7 = unchecked((nint)null);
														num8 = (enabled ? 1 : 0);
													}
													obj9++;
													continue;
												}
												throw new IndexOutOfRangeException();
											}
											continue;
										}
										throw new NullReferenceException();
									}
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							}
							throw new NullReferenceException();
						}
						object obj12 = (object)(&obj4);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						obj2 = obj13;
						if (obj13 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						break;
					}
					throw new NullReferenceException();
				}
				return result;
			}
		}
		goto IL_04ca;
		IL_049f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180001E00");
		object[] args = default(object[]);
		Debug.LogErrorFormat("To change GPU Instancing at runtime, the VLB plugin's config must be configured to use the GPUInstancing RenderingMode.", args);
		return false;
		IL_04ca:
		throw new NullReferenceException();
	}

	private static void SetBlendingMode(Material mat, int nameID, BlendMode value)
	{
		mat.SetInt(nameID, (int)value);
	}

	private static void SetStencilRef(Material mat, int nameID, int value)
	{
		mat.SetInt(nameID, value);
	}

	private static void SetStencilComp(Material mat, int nameID, CompareFunction value)
	{
		mat.SetInt(nameID, (int)value);
	}

	private static void SetStencilOp(Material mat, int nameID, StencilOp value)
	{
		mat.SetInt(nameID, (int)value);
	}

	private static void SetCull(Material mat, int nameID, CullMode value)
	{
		mat.SetInt(nameID, (int)value);
	}

	private static void SetZWrite(Material mat, int nameID, ZWrite value)
	{
		mat.SetInt(nameID, (int)value);
	}

	private static void SetZTest(Material mat, int nameID, CompareFunction value)
	{
		mat.SetInt(nameID, (int)value);
	}

	static MaterialManager()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		MaterialManager.materialPropertyBlock = materialPropertyBlock;
		BlendingMode_SrcFactor = new BlendMode[3]
		{
			BlendMode.One,
			BlendMode.OneMinusDstColor,
			BlendMode.SrcAlpha
		};
		BlendingMode_DstFactor = new BlendMode[3]
		{
			BlendMode.One,
			BlendMode.One,
			BlendMode.OneMinusSrcAlpha
		};
		BlendingMode_AlphaAsBlack = new bool[3] { true, true, false };
		Hashtable hashtable = new Hashtable(1);
		ms_MaterialsGroupSD = hashtable;
		Hashtable hashtable2 = new Hashtable(1);
		ms_MaterialsGroupHD = hashtable2;
	}
}
