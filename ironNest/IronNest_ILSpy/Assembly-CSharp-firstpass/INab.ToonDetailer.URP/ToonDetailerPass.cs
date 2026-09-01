using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace INab.ToonDetailer.URP;

public class ToonDetailerPass : ScriptableRenderPass
{
	private class PassData
	{
		public Material material;

		public TextureHandle source;

		public bool UseMask;

		public TextureHandle depthMask;

		public int shaderPass;

		public bool ControlViaVolumes;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static BaseRenderFunc<PassData, RasterGraphContext> _003C_003E9__11_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal unsafe void _003CRecordRenderGraph_003Eb__11_0(PassData data, RasterGraphContext context)
		{
			//IL_0012: Expected O, but got Ref
			object obj = default(object);
			ExecuteMainPass(data, (RasterGraphContext)(&obj));
		}
	}

	private ToonDetailerSettings m_Settings;

	private Material m_Material;

	private static MaterialPropertyBlock s_SharedPropertyBlock;

	private static readonly int kDepthMaskTexture;

	private static readonly int kBlitTexturePropertyId;

	private static readonly int kBlitScaleBiasPropertyId;

	public ToonDetailerPass(string passName)
	{
		ProfilingSampler profilingSampler = new ProfilingSampler(passName);
		base.profilingSampler = profilingSampler;
	}

	public void Setup(ref Material material, ref ToonDetailerSettings settings)
	{
		m_Material = material;
		m_Settings = settings;
	}

	private unsafe static void ExecuteMainPass(PassData data, RasterGraphContext context)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0048: Expected O, but got I
		//IL_014b: Expected O, but got Ref
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_0064: Expected O, but got Ref
		//IL_05d3: Expected O, but got Ref
		//IL_0104: Expected O, but got Ref
		//IL_0221: Expected O, but got Ref
		s_SharedPropertyBlock.Clear();
		object obj = data + 24;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAAB0");
		object obj2 = default(object);
		bool flag = obj2 == null;
		IntPtr intPtr = default(IntPtr);
		Texture texture = (Texture)(nint)intPtr;
		TextureHandle textureHandle = default(TextureHandle);
		if (!flag)
		{
			Texture texture2 = (TextureHandle)(&textureHandle);
			s_SharedPropertyBlock.SetTextureImpl(kBlitTexturePropertyId, texture2);
			texture = texture2;
			textureHandle = data.source;
		}
		if (data.UseMask)
		{
			object obj3 = data + 44;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAAB0");
			object obj4 = default(object);
			if (obj4 != null)
			{
				Texture value = (TextureHandle)(&textureHandle);
				s_SharedPropertyBlock.SetTextureImpl(kDepthMaskTexture, value);
				textureHandle = data.depthMask;
			}
		}
		s_SharedPropertyBlock.SetVector(kBlitScaleBiasPropertyId, (Vector4)(&textureHandle));
		if (data.ControlViaVolumes)
		{
			VolumeManager instance = VolumeManager.instance;
			ToonDetailerVolumeComponent toonDetailerVolumeComponent;
			if (instance._003Cstack_003Ek__BackingField != null)
			{
				ToonDetailerVolumeComponent component = instance._003Cstack_003Ek__BackingField.GetComponent<ToonDetailerVolumeComponent>();
				toonDetailerVolumeComponent = component;
			}
			else
			{
				toonDetailerVolumeComponent = null;
			}
			if (toonDetailerVolumeComponent != null)
			{
				Color value2 = toonDetailerVolumeComponent._ColorHue.value;
				s_SharedPropertyBlock.SetColor("_ColorHue", (Color)(&textureHandle));
				float value3 = toonDetailerVolumeComponent._FadeStart.value;
				s_SharedPropertyBlock.SetFloat("_FadeStart", value2.r);
				float value4 = toonDetailerVolumeComponent._FadeEnd.value;
				s_SharedPropertyBlock.SetFloat("_FadeEnd", value2.r);
				float value5 = toonDetailerVolumeComponent._BlackOffset.value;
				s_SharedPropertyBlock.SetFloat("_BlackOffset", value2.r);
				float value6 = toonDetailerVolumeComponent._ContoursIntensity.value;
				s_SharedPropertyBlock.SetFloat("_ContoursIntensity", value2.r);
				float value7 = toonDetailerVolumeComponent._ContoursThickness.value;
				s_SharedPropertyBlock.SetFloat("_ContoursThickness", value2.r);
				float value8 = toonDetailerVolumeComponent._ContoursElevationStrength.value;
				float value9 = toonDetailerVolumeComponent._ContoursElevationSmoothness.value;
				float num = 1f - value2.r;
				float num2 = 0.7f / num;
				float num3 = num2 * value2.r;
				float value10 = num3 * 3f;
				s_SharedPropertyBlock.SetFloat("_ContoursElevationStrength", value10);
				float value11 = toonDetailerVolumeComponent._ContoursElevationSmoothness.value;
				float value12 = 1f - value2.r;
				s_SharedPropertyBlock.SetFloat("_ContoursElevationSmoothness", value12);
				float value13 = toonDetailerVolumeComponent._ContoursDepressionStrength.value;
				float value14 = toonDetailerVolumeComponent._ContoursDepressionSmoothness.value;
				float num4 = 1f - value2.r;
				float num5 = 0.7f / num4;
				float num6 = num5 * value2.r;
				float value15 = num6 + num6;
				s_SharedPropertyBlock.SetFloat("_ContoursDepressionStrength", value15);
				float value16 = toonDetailerVolumeComponent._ContoursDepressionSmoothness.value;
				float value17 = 1f - value2.r;
				s_SharedPropertyBlock.SetFloat("_ContoursDepressionSmoothness", value17);
				float value18 = toonDetailerVolumeComponent._CavityIntensity.value;
				s_SharedPropertyBlock.SetFloat("_CavityIntensity", value2.r);
				float value19 = toonDetailerVolumeComponent._CavityRadius.value;
				s_SharedPropertyBlock.SetFloat("_CavityRadius", value2.r);
				float value20 = toonDetailerVolumeComponent._CavityStrength.value;
				s_SharedPropertyBlock.SetFloat("_CavityStrength", value2.r);
				int value21 = toonDetailerVolumeComponent._CavitySamples.value;
				s_SharedPropertyBlock.SetInt("_CavitySamples", value21);
			}
		}
		object obj5 = default(object);
		MeshTopology topology = default(MeshTopology);
		int vertexCount = default(int);
		int instanceCount = default(int);
		MaterialPropertyBlock properties = default(MaterialPropertyBlock);
		context.cmd.DrawProcedural((Matrix4x4)(&obj5), data.material, data.shaderPass, topology, vertexCount, instanceCount, properties);
	}

	private unsafe void UpdateMaterialProperties(bool orthographic)
	{
		//IL_003b: Expected O, but got I4
		//IL_01fb: Expected O, but got I4
		//IL_0087: Expected O, but got I4
		//IL_0151: Expected O, but got I4
		//IL_0253: Expected O, but got I4
		//IL_00b8: Expected O, but got I4
		//IL_019b: Expected O, but got I4
		//IL_00f1: Expected O, but got I4
		//IL_02ac: Expected O, but got I4
		//IL_02d4: Expected O, but got I4
		//IL_02ef: Expected F4, but got O
		//IL_0303: Expected O, but got Ref
		//IL_032f: Expected O, but got I4
		//IL_0368: Expected O, but got I4
		//IL_03d1: Expected O, but got I4
		//IL_040a: Expected O, but got I4
		//IL_0473: Expected O, but got I4
		//IL_04ac: Expected O, but got I4
		//IL_0515: Expected O, but got I4
		//IL_054e: Expected O, but got I4
		//IL_0646: Expected O, but got I4
		//IL_05bd: Expected O, but got I4
		//IL_0746: Expected O, but got I4
		//IL_0e68: Expected O, but got I4
		//IL_06b2: Expected O, but got I4
		//IL_0785: Expected O, but got I4
		//IL_06fd: Expected O, but got I4
		//IL_07e6: Expected O, but got I4
		//IL_0820: Expected O, but got I4
		//IL_088a: Expected O, but got I4
		//IL_08c4: Expected O, but got I4
		//IL_0965: Expected O, but got I4
		//IL_099f: Expected O, but got I4
		//IL_0a0c: Expected O, but got I4
		//IL_0a46: Expected O, but got I4
		//IL_0aee: Expected O, but got I4
		//IL_0b30: Expected O, but got I4
		//IL_0ba5: Expected O, but got I4
		//IL_0be7: Expected O, but got I4
		//IL_0c51: Expected O, but got I4
		//IL_0c8b: Expected O, but got I4
		//IL_0cf5: Expected O, but got I4
		//IL_0d2f: Expected O, but got I4
		//IL_0d76: Expected I4, but got O
		//IL_0d81: Expected I4, but got O
		//IL_0d8a: Expected O, but got I4
		//IL_0dbb: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
		ToonDetailerSettings settings = m_Settings;
		bool flag = m_Settings == null;
		bool flag2 = orthographic;
		object obj = 0;
		string text = "_ORTHOGRAPHIC";
		if (!flag)
		{
			bool flag3 = settings._DetailerType == ToonDetailerSettings.DetailerType.Both;
			if (!flag3)
			{
				object obj2 = settings._DetailerType - 1;
				if (!flag3)
				{
					bool flag4 = (nint)obj2 != 1;
					flag2 = orthographic;
					obj = 0;
					text = "_ORTHOGRAPHIC";
					if (flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException();
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex;
					}
					bool flag5 = (object)m_Material == null;
					flag2 = orthographic;
					obj = 0;
					text = "_ORTHOGRAPHIC";
					if (!flag5)
					{
						m_Material.DisableKeyword("_USE_CONTOURS");
						flag2 = false;
						text = "_USE_CONTOURS";
						goto IL_0239;
					}
				}
				else
				{
					bool flag6 = (object)m_Material == null;
					flag2 = orthographic;
					obj = 0;
					text = "_ORTHOGRAPHIC";
					if (!flag6)
					{
						m_Material.EnableKeyword("_USE_CONTOURS");
						bool flag7 = (object)m_Material == null;
						flag2 = false;
						obj = 0;
						text = "_USE_CONTOURS";
						if (!flag7)
						{
							m_Material.DisableKeyword("_USE_CAVITY");
							flag2 = false;
							text = "_USE_CAVITY";
							goto IL_0288;
						}
					}
				}
			}
			else
			{
				bool flag8 = (object)m_Material == null;
				flag2 = orthographic;
				obj = 0;
				text = "_ORTHOGRAPHIC";
				if (!flag8)
				{
					m_Material.EnableKeyword("_USE_CONTOURS");
					flag2 = false;
					text = "_USE_CONTOURS";
					goto IL_0239;
				}
			}
		}
		goto IL_0def;
		IL_0239:
		bool flag9 = (object)m_Material == null;
		obj = 0;
		if (!flag9)
		{
			m_Material.EnableKeyword("_USE_CAVITY");
			flag2 = false;
			text = "_USE_CAVITY";
			goto IL_0288;
		}
		goto IL_0def;
		IL_0288:
		ToonDetailerSettings settings2 = m_Settings;
		bool flag10 = m_Settings == null;
		obj = 0;
		Material material;
		if (!flag10)
		{
			bool flag11 = (object)m_Material == null;
			obj = 0;
			if (!flag11)
			{
				float num = (float)settings2._ColorHue;
				object obj3 = default(object);
				m_Material.SetColor("_ColorHue", (Color)(&obj3));
				ToonDetailerSettings settings3 = m_Settings;
				bool flag12 = m_Settings == null;
				flag2 = (byte)(&obj3) != 0;
				obj = 0;
				text = "_ColorHue";
				if (!flag12)
				{
					bool flag13 = (object)m_Material == null;
					flag2 = (byte)(&obj3) != 0;
					obj = 0;
					text = "_ColorHue";
					if (!flag13)
					{
						float fadeStart = settings3._FadeStart;
						m_Material.SetFloat("_FadeStart", settings3._FadeStart);
						ToonDetailerSettings settings4 = m_Settings;
						bool flag14 = m_Settings == null;
						flag2 = (byte)(&obj3) != 0;
						obj = 0;
						text = "_FadeStart";
						if (!flag14)
						{
							bool flag15 = (object)m_Material == null;
							flag2 = (byte)(&obj3) != 0;
							obj = 0;
							text = "_FadeStart";
							if (!flag15)
							{
								fadeStart = settings4._FadeEnd;
								m_Material.SetFloat("_FadeEnd", settings4._FadeEnd);
								ToonDetailerSettings settings5 = m_Settings;
								bool flag16 = m_Settings == null;
								flag2 = (byte)(&obj3) != 0;
								obj = 0;
								text = "_FadeEnd";
								if (!flag16)
								{
									bool flag17 = (object)m_Material == null;
									flag2 = (byte)(&obj3) != 0;
									obj = 0;
									text = "_FadeEnd";
									if (!flag17)
									{
										fadeStart = settings5._BlackOffset;
										m_Material.SetFloat("_BlackOffset", settings5._BlackOffset);
										ToonDetailerSettings settings6 = m_Settings;
										bool flag18 = m_Settings == null;
										flag2 = (byte)(&obj3) != 0;
										obj = 0;
										text = "_BlackOffset";
										if (!flag18)
										{
											bool flag19 = (object)m_Material == null;
											flag2 = (byte)(&obj3) != 0;
											obj = 0;
											text = "_BlackOffset";
											if (!flag19)
											{
												if (!settings6._UseFade)
												{
													m_Material.DisableKeyword("_FADE_ON");
													bool flag20 = (object)m_Material == null;
													flag2 = false;
													obj = 0;
													text = "_FADE_ON";
													if (flag20)
													{
														goto IL_0def;
													}
													m_Material.DisableKeyword("_FADE_COUNTOURS_ONLY");
													flag2 = false;
													text = "_FADE_COUNTOURS_ONLY";
												}
												else
												{
													m_Material.EnableKeyword("_FADE_ON");
													flag2 = false;
													text = "_FADE_ON";
												}
												ToonDetailerSettings settings7 = m_Settings;
												bool flag21 = m_Settings == null;
												obj = 0;
												if (!flag21)
												{
													if (settings7._FadeAffectsOnlyContours && settings7._UseFade)
													{
														bool flag22 = (object)m_Material == null;
														obj = 0;
														if (!flag22)
														{
															m_Material.EnableKeyword("_FADE_COUNTOURS_ONLY");
															material = m_Material;
															bool flag23 = (object)m_Material == null;
															flag2 = false;
															obj = 0;
															text = "_FADE_COUNTOURS_ONLY";
															if (!flag23)
															{
																text = "_FADE_ON";
																goto IL_0e2e;
															}
														}
													}
													else
													{
														material = m_Material;
														bool flag24 = (object)m_Material == null;
														obj = 0;
														if (!flag24)
														{
															text = "_FADE_COUNTOURS_ONLY";
															goto IL_0e2e;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0def;
		IL_0def:
		throw new NullReferenceException();
		IL_0e2e:
		material.DisableKeyword(text);
		ToonDetailerSettings settings8 = m_Settings;
		bool flag25 = m_Settings == null;
		flag2 = false;
		obj = 0;
		if (!flag25)
		{
			bool flag26 = (object)m_Material == null;
			flag2 = false;
			obj = 0;
			if (!flag26)
			{
				float fadeStart = settings8._ContoursIntensity;
				m_Material.SetFloat("_ContoursIntensity", settings8._ContoursIntensity);
				ToonDetailerSettings settings9 = m_Settings;
				bool flag27 = m_Settings == null;
				flag2 = false;
				obj = 0;
				text = "_ContoursIntensity";
				if (!flag27)
				{
					bool flag28 = (object)m_Material == null;
					flag2 = false;
					obj = 0;
					text = "_ContoursIntensity";
					if (!flag28)
					{
						fadeStart = settings9._ContoursThickness;
						m_Material.SetFloat("_ContoursThickness", settings9._ContoursThickness);
						ToonDetailerSettings settings10 = m_Settings;
						bool flag29 = m_Settings == null;
						flag2 = false;
						obj = 0;
						text = "_ContoursThickness";
						if (!flag29)
						{
							bool flag30 = (object)m_Material == null;
							flag2 = false;
							obj = 0;
							text = "_ContoursThickness";
							if (!flag30)
							{
								float num = 1f - settings10._ContoursElevationSmoothness;
								float num2 = 0.7f / num;
								float num3 = num2 * settings10._ContoursElevationStrength;
								fadeStart = num3 * 3f;
								m_Material.SetFloat("_ContoursElevationStrength", fadeStart);
								ToonDetailerSettings settings11 = m_Settings;
								bool flag31 = m_Settings == null;
								flag2 = false;
								obj = 0;
								text = "_ContoursElevationStrength";
								if (!flag31)
								{
									bool flag32 = (object)m_Material == null;
									flag2 = false;
									obj = 0;
									text = "_ContoursElevationStrength";
									if (!flag32)
									{
										fadeStart = 1f - settings11._ContoursElevationSmoothness;
										m_Material.SetFloat("_ContoursElevationSmoothness", fadeStart);
										ToonDetailerSettings settings12 = m_Settings;
										bool flag33 = m_Settings == null;
										flag2 = false;
										obj = 0;
										text = "_ContoursElevationSmoothness";
										if (!flag33)
										{
											bool flag34 = (object)m_Material == null;
											flag2 = false;
											obj = 0;
											text = "_ContoursElevationSmoothness";
											if (!flag34)
											{
												num = 1f - settings12._ContoursDepressionSmoothness;
												float num4 = 0.7f / num;
												float num5 = num4 * settings12._ContoursDepressionStrength;
												float num6 = num5 + num5;
												m_Material.SetFloat("_ContoursDepressionStrength", num6);
												ToonDetailerSettings settings13 = m_Settings;
												bool flag35 = m_Settings == null;
												fadeStart = num6;
												flag2 = false;
												obj = 0;
												text = "_ContoursDepressionStrength";
												if (!flag35)
												{
													bool flag36 = (object)m_Material == null;
													fadeStart = num6;
													flag2 = false;
													obj = 0;
													text = "_ContoursDepressionStrength";
													if (!flag36)
													{
														float num7 = 1f - settings13._ContoursDepressionSmoothness;
														m_Material.SetFloat("_ContoursDepressionSmoothness", num7);
														ToonDetailerSettings settings14 = m_Settings;
														bool flag37 = m_Settings == null;
														fadeStart = num7;
														flag2 = false;
														obj = 0;
														text = "_ContoursDepressionSmoothness";
														if (!flag37)
														{
															bool flag38 = (object)m_Material == null;
															fadeStart = num7;
															flag2 = false;
															obj = 0;
															text = "_ContoursDepressionSmoothness";
															if (!flag38)
															{
																fadeStart = settings14._CavityIntensity;
																m_Material.SetFloat("_CavityIntensity", settings14._CavityIntensity);
																ToonDetailerSettings settings15 = m_Settings;
																bool flag39 = m_Settings == null;
																flag2 = false;
																obj = 0;
																text = "_CavityIntensity";
																if (!flag39)
																{
																	bool flag40 = (object)m_Material == null;
																	flag2 = false;
																	obj = 0;
																	text = "_CavityIntensity";
																	if (!flag40)
																	{
																		fadeStart = settings15._CavityRadius;
																		m_Material.SetFloat("_CavityRadius", settings15._CavityRadius);
																		ToonDetailerSettings settings16 = m_Settings;
																		bool flag41 = m_Settings == null;
																		flag2 = false;
																		obj = 0;
																		text = "_CavityRadius";
																		if (!flag41)
																		{
																			bool flag42 = (object)m_Material == null;
																			flag2 = false;
																			obj = 0;
																			text = "_CavityRadius";
																			if (!flag42)
																			{
																				fadeStart = settings16._CavityStrength;
																				m_Material.SetFloat("_CavityStrength", settings16._CavityStrength);
																				flag2 = (byte)(int)m_Settings != 0;
																				bool flag43 = (byte)(int)(~m_Settings) != 0;
																				obj = 0;
																				text = "_CavityStrength";
																				if (!flag43)
																				{
																					bool flag44 = (object)m_Material == null;
																					obj = 0;
																					text = "_CavityStrength";
																					if (!flag44)
																					{
																						Material material2 = m_Material;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v646 @ r8_v2 (System.Boolean)+64]");
																						material2.SetInt("_CavitySamples", 0);
																						return;
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0def;
	}

	public unsafe override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
	{
		//IL_01f7: Expected O, but got I4
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0299: Expected O, but got Ref
		//IL_02dc: Expected O, but got Ref
		//IL_0364: Expected O, but got Ref
		//IL_0389: Expected O, but got Ref
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Expected O, but got Unknown
		//IL_03ed: Expected O, but got Ref
		//IL_0406: Expected I, but got O
		//IL_0498: Expected O, but got I
		//IL_04be: Expected O, but got Ref
		//IL_043e: Expected O, but got I
		//IL_0447: Expected O, but got I4
		//IL_051e: Expected O, but got I
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Expected O, but got Unknown
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Expected O, but got Unknown
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Expected O, but got Unknown
		UniversalResourceData universalResourceData = frameData.Get<UniversalResourceData>();
		UniversalCameraData universalCameraData = frameData.Get<UniversalCameraData>();
		bool orthographic = universalCameraData.camera.orthographic;
		UpdateMaterialProperties(orthographic);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180384A50");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180741680");
		UniversalResourceData universalResourceData2 = frameData.Get<UniversalResourceData>();
		ContextContainer contextContainer = default(ContextContainer);
		IRenderAttachmentRenderGraphBuilder renderAttachmentRenderGraphBuilder = default(IRenderAttachmentRenderGraphBuilder);
		if (frameData.Contains<ToonDetailer.TextureRefData>())
		{
			ToonDetailer.TextureRefData textureRefData = frameData.Get<ToonDetailer.TextureRefData>();
			if (textureRefData == null)
			{
				throw new NullReferenceException();
			}
			if (contextContainer == null)
			{
				throw new NullReferenceException();
			}
			_ = textureRefData.depthMaskTexture;
			if (contextContainer == null)
			{
				throw new NullReferenceException();
			}
			if (renderAttachmentRenderGraphBuilder == null)
			{
				throw new NullReferenceException();
			}
			object obj = contextContainer + 44;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003F60");
			ContextContainer contextContainer2 = null;
		}
		ToonDetailerSettings settings = m_Settings;
		UnityEngine.Rendering.RenderGraphModule.ResourceHandle resourceHandle = default(UnityEngine.Rendering.RenderGraphModule.ResourceHandle);
		object obj3;
		if (m_Settings != null)
		{
			bool flag = settings._MaskUse == ToonDetailerSettings.MaskUse.None;
			bool flag2 = !flag;
			if (contextContainer != null)
			{
				ToonDetailerSettings settings2 = m_Settings;
				if (m_Settings != null)
				{
					ContextContainer contextContainer2 = (ContextContainer)settings2._MaskUse;
					if (contextContainer != null)
					{
						_ = settings2._MaskUse;
						if (contextContainer != null)
						{
							contextContainer.m_Items = (ContextContainer.Item[])(object)m_Material;
							contextContainer2 = (ContextContainer)(contextContainer + 16);
							if (universalResourceData != null)
							{
								TextureHandle cameraColor = universalResourceData.cameraColor;
								bool flag3 = contextContainer == null;
								contextContainer2 = (ContextContainer)(&resourceHandle);
								if (!flag3)
								{
									contextContainer.m_ActiveItemIndices = (List<uint>)cameraColor.handle;
									ToonDetailerSettings settings3 = m_Settings;
									bool flag4 = m_Settings == null;
									contextContainer2 = (ContextContainer)(&resourceHandle);
									if (!flag4)
									{
										if (contextContainer != null)
										{
											_ = settings3._ControlViaVolumes;
											TextureHandle cameraColor2 = universalResourceData.cameraColor;
											TextureHandle texture = default(TextureHandle);
											TextureDesc textureDesc = renderGraph.GetTextureDesc(ref texture);
											TextureDesc desc = default(TextureDesc);
											TextureHandle textureHandle = renderGraph.CreateTexture(ref desc);
											bool flag5 = contextContainer == null;
											contextContainer2 = (ContextContainer)(&resourceHandle);
											if (!flag5)
											{
												bool flag6 = renderAttachmentRenderGraphBuilder == null;
												contextContainer2 = (ContextContainer)(&resourceHandle);
												if (!flag6)
												{
													object obj2 = contextContainer + 24;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003F60");
													bool flag7 = renderAttachmentRenderGraphBuilder == null;
													contextContainer2 = null;
													if (!flag7)
													{
														renderAttachmentRenderGraphBuilder.SetRenderAttachment((TextureHandle)(&resourceHandle), 0);
														BaseRenderFunc<PassData, RasterGraphContext> baseRenderFunc = _003C_003Ec._003C_003E9__11_0;
														if (_003C_003Ec._003C_003E9__11_0 == null)
														{
															baseRenderFunc = (_003C_003Ec._003C_003E9__11_0 = delegate(PassData data, RasterGraphContext context)
															{
																//IL_0012: Expected O, but got Ref
																object obj12 = default(object);
																ExecuteMainPass(data, (RasterGraphContext)(&obj12));
															});
														}
														nint num = 0;
														nint num2 = (nint)renderAttachmentRenderGraphBuilder;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1324 @ r9_v27 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+12E]");
														if ((nint)0 >= (nint)0)
														{
															goto IL_047e;
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1324 @ r9_v27 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+B0]");
														obj3 = 0;
														object obj4 = 0;
														while (true)
														{
															object obj5 = obj4 + obj4;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1399 @ r8_v30+v1366 @ rax_v78*8]");
															if ((nint)0 == 0)
															{
																break;
															}
															obj4++;
															object obj6 = obj4;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1324 @ r9_v27 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+12E]");
															if ((nint)obj6 < 0)
															{
																continue;
															}
															goto IL_047e;
														}
														object obj7 = obj4 + obj4;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1399 @ r8_v30+8+v1401 @ rdx_v46*8]");
														nint num3 = 0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1322 @ r14_v20 (Il2CppMethodInfo)+50]");
														object obj8 = num3 + 0;
														object obj9 = obj8 << 4;
														object obj10 = obj9 + 312;
														object obj11 = obj10 + num2;
														goto IL_049d;
													}
													throw new NullReferenceException();
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
		IL_047e:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1322 @ r14_v20 (Il2CppMethodInfo)+50]");
		obj3 = 0;
		goto IL_049d;
		IL_049d:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FC0");
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1417 @ rax_v69+8] (should have been resolved before IL gen)");
		universalResourceData.cameraColor = (TextureHandle)(&resourceHandle);
		if (renderAttachmentRenderGraphBuilder != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		}
	}

	public void Dispose()
	{
	}

	static ToonDetailerPass()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		s_SharedPropertyBlock = materialPropertyBlock;
		int num = Shader.PropertyToID("_DepthMaskRT");
		kDepthMaskTexture = num;
		int num2 = Shader.PropertyToID("_BlitTexture");
		kBlitTexturePropertyId = num2;
		int num3 = Shader.PropertyToID("_BlitScaleBias");
		kBlitScaleBiasPropertyId = num3;
	}
}
