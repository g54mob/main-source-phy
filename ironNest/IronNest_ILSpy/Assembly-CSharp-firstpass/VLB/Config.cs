using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace VLB;

public class Config : ScriptableObject
{
	public const string ClassName = "Config";

	public const string kAssetName = "VLBConfigOverride";

	public const string kAssetNameExt = ".asset";

	public bool geometryOverrideLayer;

	public int geometryLayerID;

	public string geometryTag;

	public int geometryRenderQueue;

	public int geometryRenderQueueHD;

	private RenderPipeline m_RenderPipeline;

	private RenderingMode m_RenderingMode;

	public float ditheringFactor;

	public bool useLightColorTemperature;

	public int sharedMeshSides;

	public int sharedMeshSegments;

	public float hdBeamsCameraBlendingDistance;

	public int urpDepthCameraScriptableRendererIndex;

	public float globalNoiseScale;

	public Vector3 globalNoiseVelocity;

	public string fadeOutCameraTag;

	public Texture3D noiseTexture3D;

	public ParticleSystem dustParticlesPrefab;

	public Texture2D ditheringNoiseTexture;

	public Texture2D jitteringNoiseTexture;

	public FeatureEnabledColorGradient featureEnabledColorGradient;

	public bool featureEnabledDepthBlend;

	public bool featureEnabledNoise3D;

	public bool featureEnabledDynamicOcclusion;

	public bool featureEnabledMeshSkewing;

	public bool featureEnabledShaderAccuracyHigh;

	public bool featureEnabledShadow;

	public bool featureEnabledCookie;

	private RaymarchingQuality[] m_RaymarchingQualities;

	private int m_DefaultRaymarchingQualityUniqueID;

	private int pluginVersion;

	private Material _DummyMaterial;

	private Material _DummyMaterialHD;

	private Shader _BeamShader;

	private Shader _BeamShaderHD;

	private Camera m_CachedFadeOutCamera;

	private static Config ms_Instance;

	public RenderPipeline renderPipeline
	{
		get
		{
			return m_RenderPipeline;
		}
		set
		{
			Debug.LogError("Modifying the RenderPipeline in standalone builds is not permitted");
		}
	}

	public RenderingMode renderingMode
	{
		get
		{
			return m_RenderingMode;
		}
		set
		{
			Debug.LogError("Modifying the RenderingMode in standalone builds is not permitted");
		}
	}

	public bool SD_useSinglePassShader
	{
		get
		{
			//IL_0058: Expected O, but got I4
			if (m_RenderingMode == RenderingMode.SRPBatcher)
			{
				if (m_RenderPipeline != RenderPipeline.BuiltIn)
				{
					RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
					object obj = projectRenderPipeline - 1;
					if ((nint)obj <= 1)
					{
						goto IL_007e;
					}
				}
				return true;
			}
			goto IL_007e;
			IL_007e:
			RenderingMode renderingMode;
			if (m_RenderPipeline != RenderPipeline.BuiltIn)
			{
				bool flag = m_RenderingMode == RenderingMode.MultiPass;
				renderingMode = RenderingMode.Default;
				if (flag)
				{
					goto IL_00d4;
				}
			}
			renderingMode = m_RenderingMode;
			goto IL_00d4;
			IL_00d4:
			bool flag2 = renderingMode == RenderingMode.MultiPass;
			return !flag2;
		}
	}

	public bool SD_requiresDoubleSidedMesh
	{
		get
		{
			//IL_0058: Expected O, but got I4
			if (m_RenderingMode == RenderingMode.SRPBatcher)
			{
				if (m_RenderPipeline != RenderPipeline.BuiltIn)
				{
					RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
					object obj = projectRenderPipeline - 1;
					if ((nint)obj <= 1)
					{
						goto IL_007e;
					}
				}
				return true;
			}
			goto IL_007e;
			IL_007e:
			RenderingMode renderingMode;
			if (m_RenderPipeline != RenderPipeline.BuiltIn)
			{
				bool flag = m_RenderingMode == RenderingMode.MultiPass;
				renderingMode = RenderingMode.Default;
				if (flag)
				{
					goto IL_00d4;
				}
			}
			renderingMode = m_RenderingMode;
			goto IL_00d4;
			IL_00d4:
			bool flag2 = renderingMode == RenderingMode.MultiPass;
			return !flag2;
		}
	}

	public Transform fadeOutCameraTransform
	{
		get
		{
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Expected O, but got Unknown
			//IL_007b: Expected O, but got I4
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Expected O, but got Unknown
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0154: Expected O, but got Unknown
			if (m_CachedFadeOutCamera != null)
			{
				if ((object)m_CachedFadeOutCamera == null)
				{
					goto IL_01d8;
				}
				if (m_CachedFadeOutCamera.isActiveAndEnabled)
				{
					goto IL_016d;
				}
			}
			GameObject[] array = GameObject.FindGameObjectsWithTag(fadeOutCameraTag);
			if (array != null)
			{
				object obj = array + 32;
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				for (object obj2 = 0; (nint)obj2 < array.Length; obj2++, obj += 8)
				{
					if (!(UnityEngine.Object)obj)
					{
						continue;
					}
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
						if (!obj3)
						{
							continue;
						}
						if ((object)obj3 != null)
						{
							if (!((Behaviour)obj3).isActiveAndEnabled)
							{
								continue;
							}
							m_CachedFadeOutCamera = (Camera)obj3;
							break;
						}
					}
					goto IL_01d8;
				}
			}
			goto IL_016d;
			IL_016d:
			if (m_CachedFadeOutCamera != null)
			{
				if ((object)m_CachedFadeOutCamera != null)
				{
					return m_CachedFadeOutCamera.transform;
				}
				goto IL_01d8;
			}
			return null;
			IL_01d8:
			return (Transform)(object)new NullReferenceException();
		}
	}

	public string fadeOutCameraName
	{
		get
		{
			if (m_CachedFadeOutCamera != null)
			{
				if ((object)m_CachedFadeOutCamera != null)
				{
					return m_CachedFadeOutCamera.name;
				}
				return (string)(object)new NullReferenceException();
			}
			return "Invalid Camera";
		}
	}

	public int defaultRaymarchingQualityUniqueID => m_DefaultRaymarchingQualityUniqueID;

	public int raymarchingQualitiesCount
	{
		get
		{
			if (m_RaymarchingQualities != null)
			{
				RaymarchingQuality[] raymarchingQualities = m_RaymarchingQualities;
				bool flag = raymarchingQualities.Length < 1;
				int result = 1;
				if (!flag)
				{
					result = raymarchingQualities.Length;
				}
				return result;
			}
			bool flag2 = 1 < 1;
			int result2 = 1;
			if (!flag2)
			{
				result2 = 1;
			}
			return result2;
		}
	}

	public bool isHDRPExposureWeightSupported
	{
		get
		{
			//IL_0010: Expected O, but got I4
			object obj = m_RenderPipeline - 2;
			return obj == null;
		}
	}

	public bool hasRenderPipelineMismatch
	{
		get
		{
			RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
			bool flag = m_RenderPipeline == RenderPipeline.BuiltIn;
			bool flag2 = projectRenderPipeline == RenderPipeline.BuiltIn;
			return flag2 ^ flag;
		}
	}

	public static Config Instance => GetInstance(assertIfNotFound: true);

	public bool IsSRPBatcherSupported()
	{
		//IL_005b: Expected O, but got I4
		if (m_RenderPipeline != RenderPipeline.BuiltIn)
		{
			bool projectRenderPipeline = (byte)SRPHelper.projectRenderPipeline != 0;
			if (projectRenderPipeline)
			{
				return projectRenderPipeline;
			}
			object obj = (projectRenderPipeline ? 1 : 0) - 2;
			return obj == null;
		}
		return false;
	}

	public RenderingMode GetActualRenderingMode(ShaderMode shaderMode)
	{
		//IL_0058: Expected O, but got I4
		if (m_RenderingMode != RenderingMode.SRPBatcher)
		{
			goto IL_0075;
		}
		if (m_RenderPipeline != RenderPipeline.BuiltIn)
		{
			RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
			object obj = projectRenderPipeline - 1;
			if ((nint)obj <= 1)
			{
				goto IL_0075;
			}
		}
		goto IL_00f5;
		IL_0075:
		if ((m_RenderPipeline != RenderPipeline.BuiltIn && m_RenderingMode == RenderingMode.MultiPass) || (shaderMode == ShaderMode.HD && m_RenderingMode == RenderingMode.MultiPass))
		{
			goto IL_00f5;
		}
		return m_RenderingMode;
		IL_00f5:
		return RenderingMode.Default;
	}

	public Shader GetBeamShader(ShaderMode mode)
	{
		//IL_001b: Expected O, but got I4
		//IL_0044: Expected O, but got I
		//IL_0032: Expected O, but got I4
		bool flag = mode != ShaderMode.SD;
		object obj = 192;
		if (!flag)
		{
			obj = 184;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rax_v2+this @ rcx (VLB.Config)]");
		return (Shader)0;
	}

	private unsafe ref Shader GetBeamShaderInternal(ShaderMode mode)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected Ref, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected Ref, but got Unknown
		bool flag = mode == ShaderMode.SD;
		ref Shader reference = ref *(Shader*)(this + 192);
		ref Shader result = ref *(Shader*)(this + 184);
		if (!flag)
		{
			result = ref reference;
		}
		return ref result;
	}

	private int GetRenderQueueInternal(ShaderMode mode)
	{
		if (mode != ShaderMode.SD)
		{
			return geometryRenderQueueHD;
		}
		return geometryRenderQueue;
	}

	public Material NewMaterialTransient(ShaderMode mode, bool gpuInstanced)
	{
		//IL_017b: Expected O, but got I4
		//IL_000e: Expected O, but got I4
		//IL_0029: Expected O, but got I
		//IL_0057: Expected O, but got I
		//IL_0131: Expected O, but got I4
		//IL_0148: Expected O, but got I4
		bool flag = mode != ShaderMode.SD;
		object obj = 192;
		if (!flag)
		{
			obj = 184;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rax_v3+this @ rcx (VLB.Config)]");
		Material material2;
		if ((bool)(UnityEngine.Object)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rax_v3+this @ rcx (VLB.Config)]");
			Material material = new Material((Shader)0);
			if ((object)material == null)
			{
				goto IL_0152;
			}
			material.enableInstancing = gpuInstanced;
			material2 = material;
		}
		else
		{
			Debug.LogError("Invalid VLB Shader. Please try to reset the VLB Config asset or reinstall the plugin.");
			material2 = null;
		}
		if ((bool)material2)
		{
			if ((object)material2 == null)
			{
				goto IL_0152;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb edx,edx\"");
			material2.hideFlags = HideFlags.HideAndDontSave;
			bool flag2 = mode != ShaderMode.SD;
			object obj2 = 44;
			if (!flag2)
			{
				obj2 = 40;
			}
			Material material3 = material2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ rdx_v7+this @ rcx (VLB.Config)]");
			material3.renderQueue = 0;
		}
		return material2;
		IL_0152:
		return (Material)(object)new NullReferenceException();
	}

	public void SetURPScriptableRendererIndexToDepthCamera(Camera camera)
	{
		if (urpDepthCameraScriptableRendererIndex >= 0)
		{
			UniversalAdditionalCameraData universalAdditionalCameraData = CameraExtensions.GetUniversalAdditionalCameraData(camera);
			if ((bool)universalAdditionalCameraData)
			{
				universalAdditionalCameraData.m_RendererIndex = urpDepthCameraScriptableRendererIndex;
			}
		}
	}

	public void ForceUpdateFadeOutCamera()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		GameObject[] array = GameObject.FindGameObjectsWithTag(fadeOutCameraTag);
		if (array == null)
		{
			return;
		}
		object obj = array + 32;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < array.Length)
		{
			if ((bool)(UnityEngine.Object)obj)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				if ((bool)obj3 && ((Behaviour)obj3).isActiveAndEnabled)
				{
					m_CachedFadeOutCamera = (Camera)obj3;
					break;
				}
			}
			obj2++;
			obj += 8;
		}
	}

	public RaymarchingQuality GetRaymarchingQualityForIndex(int index)
	{
		RaymarchingQuality[] raymarchingQualities = m_RaymarchingQualities;
		if (index < raymarchingQualities.Length)
		{
			return raymarchingQualities[index];
		}
		return (RaymarchingQuality)(object)new IndexOutOfRangeException();
	}

	public RaymarchingQuality GetRaymarchingQualityForUniqueID(int id)
	{
		int raymarchingQualityIndexForUniqueID = GetRaymarchingQualityIndexForUniqueID(id);
		if (raymarchingQualityIndexForUniqueID < 0)
		{
			return null;
		}
		RaymarchingQuality[] raymarchingQualities = m_RaymarchingQualities;
		if (raymarchingQualityIndexForUniqueID < raymarchingQualities.Length)
		{
			return raymarchingQualities[raymarchingQualityIndexForUniqueID];
		}
		return (RaymarchingQuality)(object)new IndexOutOfRangeException();
	}

	public int GetRaymarchingQualityIndexForUniqueID(int id)
	{
		//IL_0020: Expected O, but got I4
		//IL_01f5: Expected I4, but got O
		//IL_005f: Expected O, but got I
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_01a1: Expected I4, but got I8
		//IL_0130: Expected I, but got O
		RaymarchingQuality[] raymarchingQualities = m_RaymarchingQualities;
		int num = 0;
		int num2 = 0;
		RaymarchingQuality[] array = (RaymarchingQuality[])32;
		RaymarchingQuality[] array3 = default(RaymarchingQuality[]);
		object obj2 = default(object);
		while (true)
		{
			if (num < raymarchingQualities.Length)
			{
				bool flag = num2 >= raymarchingQualities.Length;
				int num3 = 0;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ rcx_v4 (VLB.RaymarchingQuality[])+v55 @ rcx_v9 (VLB.RaymarchingQuality[])]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ rcx_v4 (VLB.RaymarchingQuality[])+v55 @ rcx_v9 (VLB.RaymarchingQuality[])]");
				if ((nint)0 == 0)
				{
					goto IL_00a8;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rdx_v16+1C]");
				if ((nint)0 != id)
				{
					goto IL_00a8;
				}
			}
			else
			{
				object[] array2 = new object[1];
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				if (array2 == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (int)ex;
				}
				if (array3 != null)
				{
					nint num4 = (nint)array2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ rdx_v14 (Il2CppClass<System.Object[]>)+40]");
					int num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag2 = obj2 == null;
					raymarchingQualities = array3;
					if (flag2)
					{
						break;
					}
				}
				array2[0] = array3;
				Debug.LogErrorFormat("Failed to find RaymarchingQualityIndex for Unique ID {0}", array2);
				num2 = -1;
			}
			return num2;
			IL_00a8:
			num2++;
			array = (RaymarchingQuality[])(array + 8);
			num = num2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
		object obj3 = default(object);
		throw obj3;
	}

	public bool IsRaymarchingQualityUniqueIDValid(int id)
	{
		int raymarchingQualityIndexForUniqueID = GetRaymarchingQualityIndexForUniqueID(id);
		int num = raymarchingQualityIndexForUniqueID >> 31;
		return (byte)(num ^ 1) != 0;
	}

	private void CreateDefaultRaymarchingQualityPreset(bool onlyIfNeeded)
	{
		//IL_00a3: Expected O, but got I4
		//IL_0195: Expected O, but got I4
		//IL_00e8: Expected I, but got O
		//IL_0123: Expected O, but got I4
		//IL_0287: Expected O, but got I4
		//IL_01da: Expected I, but got O
		//IL_01ea: Expected O, but got I
		//IL_0215: Expected O, but got I4
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Expected O, but got Unknown
		//IL_036c: Expected O, but got I4
		//IL_0374: Expected I4, but got O
		//IL_02cc: Expected I, but got O
		//IL_02dc: Expected O, but got I
		//IL_0307: Expected O, but got I4
		//IL_03bf: Expected O, but got I4
		//IL_03c7: Expected I4, but got O
		if (m_RaymarchingQualities != null)
		{
			RaymarchingQuality[] raymarchingQualities = m_RaymarchingQualities;
			if (raymarchingQualities.Length != 0 && onlyIfNeeded)
			{
				return;
			}
		}
		RaymarchingQuality[] raymarchingQualities2 = new RaymarchingQuality[3];
		m_RaymarchingQualities = raymarchingQualities2;
		RaymarchingQuality[] raymarchingQualities3 = m_RaymarchingQualities;
		RaymarchingQuality raymarchingQuality = RaymarchingQuality.New("Fast", 1, 5);
		bool flag = m_RaymarchingQualities == null;
		int num = 5;
		object obj = 0;
		int num2 = 1;
		string text = "Fast";
		if (!flag)
		{
			if (raymarchingQuality != null)
			{
				nint num3 = (nint)raymarchingQualities3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ rdx_v25 (Il2CppClass<VLB.RaymarchingQuality[]>)+40]");
				num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj2 = default(object);
				bool flag2 = obj2 == null;
				num = 5;
				obj = 0;
				text = (string)(object)raymarchingQuality;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					RaymarchingQuality raymarchingQuality2 = default(RaymarchingQuality);
					throw raymarchingQuality2;
				}
			}
			raymarchingQualities3[0] = raymarchingQuality;
			RaymarchingQuality[] raymarchingQualities4 = m_RaymarchingQualities;
			RaymarchingQuality raymarchingQuality3 = RaymarchingQuality.New("Balanced", 2, 10);
			bool flag3 = m_RaymarchingQualities == null;
			num = 10;
			obj = 0;
			num2 = 2;
			text = "Balanced";
			if (!flag3)
			{
				if (raymarchingQuality3 != null)
				{
					nint num4 = (nint)raymarchingQualities4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v434 @ rdx_v23 (Il2CppClass<VLB.RaymarchingQuality[]>)+40]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj4 = default(object);
					bool flag4 = obj4 == null;
					num = 10;
					obj = 0;
					RaymarchingQuality raymarchingQuality4 = raymarchingQuality3;
					if (flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						RaymarchingQuality raymarchingQuality5 = default(RaymarchingQuality);
						throw raymarchingQuality5;
					}
				}
				raymarchingQualities4[1] = raymarchingQuality3;
				RaymarchingQuality[] raymarchingQualities5 = m_RaymarchingQualities;
				RaymarchingQuality raymarchingQuality6 = RaymarchingQuality.New("High", 3, 20);
				bool flag5 = m_RaymarchingQualities == null;
				num = 20;
				obj = 0;
				num2 = 3;
				text = "High";
				if (!flag5)
				{
					if (raymarchingQuality6 != null)
					{
						nint num5 = (nint)raymarchingQualities5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rdx_v21 (Il2CppClass<VLB.RaymarchingQuality[]>)+40]");
						object obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj6 = default(object);
						bool flag6 = obj6 == null;
						num = 20;
						obj = 0;
						RaymarchingQuality raymarchingQuality7 = raymarchingQuality6;
						if (flag6)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj7 = default(object);
							throw obj7;
						}
					}
					text = (string)(m_RaymarchingQualities + 48);
					raymarchingQualities5[2] = raymarchingQuality6;
					RaymarchingQuality[] raymarchingQualities6 = m_RaymarchingQualities;
					bool flag7 = m_RaymarchingQualities == null;
					num = 20;
					obj = 0;
					num2 = (int)raymarchingQuality6;
					if (!flag7)
					{
						RaymarchingQuality raymarchingQuality8 = raymarchingQualities6[1];
						bool flag8 = raymarchingQualities6[1] == null;
						num = 20;
						obj = 0;
						num2 = (int)raymarchingQuality6;
						if (!flag8)
						{
							m_DefaultRaymarchingQualityUniqueID = raymarchingQuality8._UniqueID;
							return;
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	private static void OnStartup()
	{
		//IL_003e: Expected F4, but got I4
		Config instance = GetInstance(assertIfNotFound: true);
		instance.m_CachedFadeOutCamera = null;
		Config instance2 = GetInstance(assertIfNotFound: true);
		Shader.SetGlobalFloatImpl(value: (!SystemInfo.usesReversedZBuffer) ? 0f : 1f, name: ShaderProperties.GlobalUsesReversedZBuffer);
		Shader.SetGlobalFloatImpl(ShaderProperties.GlobalDitheringFactor, instance2.ditheringFactor);
		Shader.SetGlobalTextureImpl(ShaderProperties.GlobalDitheringNoiseTex, (Texture)instance2.ditheringNoiseTexture);
		Shader.SetGlobalFloatImpl(ShaderProperties.HD.GlobalCameraBlendingDistance, instance2.hdBeamsCameraBlendingDistance);
		Shader.SetGlobalTextureImpl(ShaderProperties.HD.GlobalJitteringNoiseTex, (Texture)instance2.jitteringNoiseTexture);
		Config instance3 = GetInstance(assertIfNotFound: true);
		RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
		bool flag = instance3.m_RenderPipeline == RenderPipeline.BuiltIn;
		bool flag2 = projectRenderPipeline == RenderPipeline.BuiltIn;
		if (flag2 != flag)
		{
			Config instance4 = GetInstance(assertIfNotFound: true);
			Debug.LogError("It looks like the 'Render Pipeline' is not correctly set in the config. Please make sure to select the proper value depending on your pipeline in use.", instance4);
		}
	}

	public void Reset()
	{
		//IL_00fc: Expected I, but got O
		//IL_007f: Expected I4, but got I8
		geometryOverrideLayer = true;
		geometryLayerID = 1;
		geometryTag = "Untagged";
		geometryRenderQueue = 3000;
		geometryRenderQueueHD = 3100;
		sharedMeshSides = 24;
		sharedMeshSegments = 5;
		globalNoiseScale = 0.5f;
		nint num = (nint)typeof(Consts.Beam);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (Il2CppClass<VLB.Consts+Beam>)+B8]");
		nint num2 = 0;
		globalNoiseVelocity = Consts.Beam.NoiseVelocityDefault;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v6 (Il2CppStaticFields<VLB.Consts+Beam>)+18]");
		_ = 0;
		Debug.LogError("Modifying the RenderPipeline in standalone builds is not permitted");
		Debug.LogError("Modifying the RenderingMode in standalone builds is not permitted");
		ditheringFactor = 0f;
		useLightColorTemperature = true;
		fadeOutCameraTag = "MainCamera";
		featureEnabledColorGradient = FeatureEnabledColorGradient.HighOnly;
		featureEnabledDepthBlend = true;
		featureEnabledShaderAccuracyHigh = true;
		hdBeamsCameraBlendingDistance = 0.5f;
		urpDepthCameraScriptableRendererIndex = -1;
		CreateDefaultRaymarchingQualityPreset(onlyIfNeeded: false);
		ResetInternalData();
	}

	private void RefreshGlobalShaderProperties()
	{
		//IL_001c: Expected F4, but got I4
		Shader.SetGlobalFloatImpl(value: (!SystemInfo.usesReversedZBuffer) ? 0f : 1f, name: ShaderProperties.GlobalUsesReversedZBuffer);
		Shader.SetGlobalFloatImpl(ShaderProperties.GlobalDitheringFactor, ditheringFactor);
		Shader.SetGlobalTextureImpl(ShaderProperties.GlobalDitheringNoiseTex, (Texture)ditheringNoiseTexture);
		Shader.SetGlobalFloatImpl(ShaderProperties.HD.GlobalCameraBlendingDistance, hdBeamsCameraBlendingDistance);
		Shader.SetGlobalTextureImpl(ShaderProperties.HD.GlobalJitteringNoiseTex, (Texture)jitteringNoiseTexture);
	}

	public void ResetInternalData()
	{
		UnityEngine.Object obj = Resources.Load("Noise3D_64x64x64");
		if ((object)obj == null)
		{
			noiseTexture3D = null;
		}
		else
		{
			bool flag = (object)obj.GetType() != typeof(Texture3D);
			UnityEngine.Object obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			noiseTexture3D = (Texture3D)obj2;
			if ((object)obj.GetType() == typeof(Texture3D))
			{
				goto IL_0211;
			}
		}
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ParticleSystem));
		UnityEngine.Object obj3 = Resources.Load("DustParticles", typeFromHandle);
		if ((object)obj3 == null)
		{
			dustParticlesPrefab = null;
		}
		else
		{
			bool flag2 = (object)obj3.GetType() != typeof(ParticleSystem);
			UnityEngine.Object obj4 = null;
			if (!flag2)
			{
				obj4 = obj3;
			}
			dustParticlesPrefab = (ParticleSystem)obj4;
			if ((object)obj3.GetType() == typeof(ParticleSystem))
			{
				goto IL_026a;
			}
		}
		goto IL_0211;
		IL_0211:
		Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Texture2D));
		UnityEngine.Object obj5 = Resources.Load("VLBDitheringNoise", typeFromHandle2);
		if ((object)obj5 == null)
		{
			ditheringNoiseTexture = null;
		}
		else
		{
			bool flag3 = (object)obj5.GetType() != typeof(Texture2D);
			UnityEngine.Object obj6 = null;
			if (!flag3)
			{
				obj6 = obj5;
			}
			ditheringNoiseTexture = (Texture2D)obj6;
			if ((object)obj5.GetType() == typeof(Texture2D))
			{
				return;
			}
		}
		goto IL_026a;
		IL_026a:
		Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Texture2D));
		UnityEngine.Object obj7 = Resources.Load("VLBBlueNoise", typeFromHandle3);
		if ((object)obj7 == null)
		{
			jitteringNoiseTexture = null;
			return;
		}
		bool flag4 = (object)obj7.GetType() != typeof(Texture2D);
		UnityEngine.Object obj8 = null;
		if (!flag4)
		{
			obj8 = obj7;
		}
		jitteringNoiseTexture = (Texture2D)obj8;
		if ((object)obj7.GetType() == typeof(Texture2D))
		{
			/*Error: End of method reached without returning.*/;
		}
	}

	public ParticleSystem NewVolumetricDustParticles()
	{
		if ((bool)dustParticlesPrefab)
		{
			ParticleSystem particleSystem = UnityEngine.Object.Instantiate(dustParticlesPrefab);
			if ((object)particleSystem != null)
			{
				particleSystem.useAutoRandomSeed = false;
				particleSystem.name = "Dust Particles";
				GameObject gameObject = particleSystem.gameObject;
				if ((object)gameObject != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb edx,edx\"");
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					GameObject gameObject2 = particleSystem.gameObject;
					if ((object)gameObject2 != null)
					{
						gameObject2.SetActive(value: true);
						return particleSystem;
					}
				}
			}
			return (ParticleSystem)(object)new NullReferenceException();
		}
		if (Application.isPlaying)
		{
			Debug.LogError("Failed to instantiate VolumetricDustParticles prefab.");
		}
		return null;
	}

	private void OnEnable()
	{
		CreateDefaultRaymarchingQualityPreset(onlyIfNeeded: true);
		pluginVersion = 20205;
	}

	private void HandleBackwardCompatibility(int serializedVersion, int newVersion)
	{
	}

	private static Config LoadAssetInternal(string assetName)
	{
		return Resources.Load<Config>(assetName);
	}

	private unsafe static Config GetInstance(bool assertIfNotFound)
	{
		//IL_00a1: Expected O, but got Ref
		if (ms_Instance == null)
		{
			RuntimePlatform platform = Application.platform;
			IntPtr intPtr = default(IntPtr);
			string text = ((Enum)(&intPtr)).ToString();
			string path = "VLBConfigOverride" + text;
			Config config = Resources.Load<Config>(path);
			bool flag = config == null;
			bool flag2 = !flag;
			Config config2 = config;
			if (!flag2)
			{
				Config config3 = Resources.Load<Config>("VLBConfigOverride");
				config2 = config3;
			}
			ms_Instance = config2;
			bool flag3 = ms_Instance == null;
		}
		return ms_Instance;
	}

	public Config()
	{
		//IL_0082: Expected I4, but got I8
		//IL_009b: Expected I, but got O
		//IL_010e: Expected I4, but got I8
		geometryOverrideLayer = true;
		geometryLayerID = 1;
		geometryTag = "Untagged";
		geometryRenderQueue = 3000;
		geometryRenderQueueHD = 3100;
		m_RenderingMode = RenderingMode.Default;
		useLightColorTemperature = true;
		sharedMeshSides = 24;
		sharedMeshSegments = 5;
		hdBeamsCameraBlendingDistance = 0.5f;
		urpDepthCameraScriptableRendererIndex = -1;
		globalNoiseScale = 0.5f;
		nint num = (nint)typeof(Consts.Beam);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (Il2CppClass<VLB.Consts+Beam>)+B8]");
		nint num2 = 0;
		globalNoiseVelocity = Consts.Beam.NoiseVelocityDefault;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rax_v6 (Il2CppStaticFields<VLB.Consts+Beam>)+18]");
		_ = 0;
		fadeOutCameraTag = "MainCamera";
		featureEnabledColorGradient = FeatureEnabledColorGradient.HighOnly;
		featureEnabledDepthBlend = true;
		featureEnabledShaderAccuracyHigh = true;
		featureEnabledCookie = true;
		pluginVersion = -1;
		base._002Ector();
	}
}
