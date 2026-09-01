using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace VLB;

public class BeamGeometryHD : BeamGeometryAbstractBase
{
	public enum InvalidTexture
	{
		Null,
		NoDepth
	}

	private VolumetricLightBeamHD m_Master;

	private VolumetricCookieHD m_Cookie;

	private VolumetricShadowHD m_Shadow;

	private Camera m_CurrentCameraRenderingSRP;

	private DirtyProps m_DirtyProps;

	public bool visible
	{
		set
		{
			if ((bool)base._003CmeshRenderer_003Ek__BackingField)
			{
				base._003CmeshRenderer_003Ek__BackingField.enabled = value;
			}
		}
	}

	public int sortingLayerID
	{
		set
		{
			if ((bool)base._003CmeshRenderer_003Ek__BackingField)
			{
				base._003CmeshRenderer_003Ek__BackingField.sortingLayerID = value;
			}
		}
	}

	public int sortingOrder
	{
		set
		{
			if ((bool)base._003CmeshRenderer_003Ek__BackingField)
			{
				base._003CmeshRenderer_003Ek__BackingField.sortingOrder = value;
			}
		}
	}

	public static bool isCustomRenderPipelineSupported => true;

	private bool shouldUseGPUInstancedMaterial
	{
		get
		{
			//IL_015c: Expected I4, but got O
			//IL_0063: Expected O, but got I4
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
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
				goto IL_012e;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_012e:
			return false;
			IL_0080:
			if ((instance.m_RenderPipeline != RenderPipeline.BuiltIn && instance.m_RenderingMode == RenderingMode.MultiPass) || instance.m_RenderingMode == RenderingMode.MultiPass || instance.m_RenderingMode != RenderingMode.GPUInstancing || !(m_Cookie == null))
			{
				goto IL_012e;
			}
			return m_Shadow == null;
		}
	}

	private bool isNoiseEnabled
	{
		get
		{
			//IL_0095: Expected I4, but got O
			//IL_005c: Invalid comparison between F4 and I4
			VolumetricLightBeamHD master = m_Master;
			if ((object)m_Master != null)
			{
				if (master.m_NoiseMode > NoiseMode.Disabled && master.m_NoiseIntensity > 0f)
				{
					return Noise3D.isSupported;
				}
				return false;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	protected override VolumetricLightBeamAbstractBase GetMaster()
	{
		return m_Master;
	}

	private void OnDisable()
	{
		Action<ScriptableRenderContext, Camera> cb = OnBeginCameraRenderingSRP;
		SRPHelper.UnregisterOnBeginCameraRendering(cb);
		m_CurrentCameraRenderingSRP = null;
	}

	private void OnEnable()
	{
		Action<ScriptableRenderContext, Camera> cb = OnBeginCameraRenderingSRP;
		SRPHelper.RegisterOnBeginCameraRendering(cb);
	}

	public void Initialize(VolumetricLightBeamHD master)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected I4, but got Unknown
		//IL_010b: Expected O, but got I4
		//IL_0252: Expected I, but got O
		//IL_016e: Expected O, but got I4
		//IL_0205: Expected I, but got O
		m_Master = master;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb esi,esi\"");
		object obj = default(object);
		HideFlags hideFlags = (HideFlags)(obj + 61);
		Transform transform = base.transform;
		Transform parent = master.transform;
		transform.SetParent(parent, worldPositionStays: false);
		GameObject self = base.gameObject;
		MeshRenderer orAddComponent = Utils.GetOrAddComponent<MeshRenderer>(self);
		base._003CmeshRenderer_003Ek__BackingField = orAddComponent;
		base._003CmeshRenderer_003Ek__BackingField.hideFlags = hideFlags;
		base._003CmeshRenderer_003Ek__BackingField.shadowCastingMode = ShadowCastingMode.Off;
		base._003CmeshRenderer_003Ek__BackingField.receiveShadows = false;
		base._003CmeshRenderer_003Ek__BackingField.reflectionProbeUsage = ReflectionProbeUsage.Off;
		base._003CmeshRenderer_003Ek__BackingField.lightProbeUsage = LightProbeUsage.Off;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricCookieHD cookie = default(VolumetricCookieHD);
		m_Cookie = cookie;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricShadowHD shadow = default(VolumetricShadowHD);
		m_Shadow = shadow;
		bool flag = shouldUseGPUInstancedMaterial;
		bool flag2 = false;
		object obj2 = 0;
		if (!flag)
		{
			Config instance = Config.GetInstance(true);
			Material customMaterial = instance.NewMaterialTransient(ShaderMode.HD, gpuInstanced: false);
			m_CustomMaterial = customMaterial;
			bool flag3 = ApplyMaterial();
			flag2 = false;
			obj2 = 0;
		}
		if (m_Master.DoesSupportSorting2D())
		{
			int id = m_Master.GetSortingLayerID();
			bool flag4 = SortingLayer.IsValid(id);
			Component master2 = m_Master;
			if (!flag4)
			{
				Transform current = master2.transform;
				string path = Utils.GetPath(current);
				VolumetricLightBeamHD master3 = m_Master;
				nint num = (nint)master3;
				int num2 = master3.GetSortingLayerID();
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"Beam '{path}' has an invalid sortingLayerID ({arg}). Please fix it by setting a valid layer.";
				Debug.LogError(message);
			}
			else
			{
				nint num3 = (nint)master2;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v554 @ rdx_v37 (Il2CppClass<UnityEngine.Component>)+1F8] (should have been resolved before IL gen)");
				int num4 = default(int);
				sortingLayerID = num4;
			}
			int num5 = m_Master.GetSortingOrder();
			sortingOrder = num5;
		}
		GameObject self2 = base.gameObject;
		MeshFilter orAddComponent2 = Utils.GetOrAddComponent<MeshFilter>(self2);
		base._003CmeshFilter_003Ek__BackingField = orAddComponent2;
		base._003CmeshFilter_003Ek__BackingField.hideFlags = hideFlags;
		GameObject gameObject = base.gameObject;
		gameObject.hideFlags = hideFlags;
	}

	public void RegenerateMesh()
	{
		Config instance = Config.GetInstance(true);
		int layer;
		GameObject gameObject3;
		if (!instance.geometryOverrideLayer)
		{
			GameObject gameObject = base.gameObject;
			GameObject gameObject2 = m_Master.gameObject;
			layer = gameObject2.layer;
			gameObject3 = gameObject;
		}
		else
		{
			GameObject gameObject4 = base.gameObject;
			Config instance2 = Config.GetInstance(true);
			layer = instance2.geometryLayerID;
			gameObject3 = gameObject4;
		}
		gameObject3.layer = layer;
		GameObject gameObject5 = base.gameObject;
		Config instance3 = Config.GetInstance(true);
		gameObject5.tag = instance3.geometryTag;
		Mesh mesh = GlobalMeshHD.Get();
		base._003CconeMesh_003Ek__BackingField = mesh;
		base._003CmeshFilter_003Ek__BackingField.sharedMesh = base._003CconeMesh_003Ek__BackingField;
		UpdateMaterialAndBounds();
	}

	private unsafe Vector3 ComputeLocalMatrix()
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_0017: Expected native int or pointer, but got O
		//IL_00ab: Expected native int or pointer, but got O
		//IL_00c2: Expected native int or pointer, but got O
		//IL_00cf: Expected native int or pointer, but got O
		//IL_0134: Expected O, but got Ref
		//IL_0134: Expected O, but got Ref
		//IL_0145: Expected native int or pointer, but got O
		//IL_0157: Expected native int or pointer, but got O
		//IL_017b: Expected O, but got Ref
		//IL_01e2: Expected O, but got Ref
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = 0f;
		((Vector3*)(nint)vector)->z = 0f;
		VolumetricLightBeamHD master = m_Master;
		if ((object)m_Master != null)
		{
			float num = master.m_ConeRadiusStart;
			float num2 = Utils.ComputeConeRadiusEnd(master.m_FallOffEnd, master.m_SpotAngle);
			if (master.m_ConeRadiusStart < num2)
			{
				num = num2;
			}
			VolumetricLightBeamHD master2 = m_Master;
			if ((object)m_Master != null)
			{
				((Vector3*)(nint)vector)->z = master2.m_FallOffEnd;
				VolumetricLightBeamHD master3 = m_Master;
				((Vector3*)(nint)vector)->x = num;
				((Vector3*)(nint)vector)->y = num;
				if ((object)m_Master != null)
				{
					float x = default(float);
					if (!master3.m_Scalable)
					{
						Vector3 lossyScale = m_Master.GetLossyScale();
						object obj = default(object);
						Vector3 vector2 = Utils.Divide((Vector3)(&x), (Vector3)(&obj));
						((Vector3*)(nint)vector)->x = vector2.x;
						((Vector3*)(nint)vector)->z = vector2.z;
						x = vector.x;
					}
					Transform transform = base.transform;
					if ((object)transform != null)
					{
						transform.localScale = (Vector3)(&x);
						Transform transform2 = base.transform;
						if ((object)m_Master != null)
						{
							Quaternion beamInternalLocalRotation = m_Master.beamInternalLocalRotation;
							if ((object)transform2 != null)
							{
								object obj2 = default(object);
								transform2.localRotation = (Quaternion)(&obj2);
								return vector;
							}
						}
					}
				}
			}
		}
		return (Vector3)new NullReferenceException();
	}

	private unsafe MaterialManager.StaticPropertiesHD ComputeMaterialStaticProperties()
	{
		//IL_02d4: Expected native int or pointer, but got O
		//IL_02e2: Expected native int or pointer, but got O
		//IL_02f0: Expected native int or pointer, but got O
		//IL_0327: Expected native int or pointer, but got O
		//IL_0335: Expected native int or pointer, but got O
		//IL_0343: Expected native int or pointer, but got O
		//IL_00cf: Expected native int or pointer, but got O
		//IL_00a0: Expected O, but got I4
		//IL_0124: Expected native int or pointer, but got O
		//IL_0379: Expected native int or pointer, but got O
		//IL_0386: Expected native int or pointer, but got O
		//IL_0185: Invalid comparison between F4 and I4
		//IL_01d0: Expected native int or pointer, but got O
		//IL_0398: Expected native int or pointer, but got O
		//IL_0248: Expected O, but got I4
		//IL_02b3: Expected native int or pointer, but got O
		MaterialManager.StaticPropertiesHD staticPropertiesHD = default(MaterialManager.StaticPropertiesHD);
		((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->blendingMode = MaterialManager.BlendingMode.Additive;
		((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->shadow = MaterialManager.HD.Shadow.Off;
		((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->raymarchingQualityIndex = 0;
		VolumetricLightBeamHD master = m_Master;
		if ((object)m_Master != null)
		{
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				bool flag = instance.featureEnabledColorGradient == FeatureEnabledColorGradient.Off;
				MaterialManager.ColorGradient colorGradient = MaterialManager.ColorGradient.Off;
				if (!flag)
				{
					bool flag2 = master.m_ColorMode != ColorMode.Gradient;
					colorGradient = MaterialManager.ColorGradient.Off;
					if (!flag2)
					{
						Utils.FloatPackingPrecision floatPackingPrecision = Utils.GetFloatPackingPrecision();
						object obj = floatPackingPrecision - 64;
						bool flag3 = obj == null;
						colorGradient = (MaterialManager.ColorGradient)((flag3 ? 1 : 0) + 1);
					}
				}
				((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->blendingMode = MaterialManager.BlendingMode.Additive;
				((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->shadow = MaterialManager.HD.Shadow.Off;
				((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->raymarchingQualityIndex = 0;
				VolumetricLightBeamHD master2 = m_Master;
				if ((object)m_Master != null)
				{
					((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->blendingMode = (MaterialManager.BlendingMode)master2.m_BlendingMode;
					VolumetricLightBeamHD master3 = m_Master;
					if ((object)m_Master != null)
					{
						bool flag4 = master3.m_AttenuationEquation == AttenuationEquationHD.Linear;
						bool attenuation = !flag4;
						((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->attenuation = (attenuation ? MaterialManager.HD.Attenuation.Quadratic : MaterialManager.HD.Attenuation.Linear);
						VolumetricLightBeamHD master4 = m_Master;
						if ((object)m_Master != null)
						{
							bool noise3D = master4.m_NoiseMode > NoiseMode.Disabled && master4.m_NoiseIntensity > 0f && Noise3D.isSupported;
							((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->noise3D = (noise3D ? MaterialManager.Noise3D.On : MaterialManager.Noise3D.Off);
							((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->colorGradient = colorGradient;
							bool shadow = m_Shadow != null;
							((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->shadow = (shadow ? MaterialManager.HD.Shadow.On : MaterialManager.HD.Shadow.Off);
							bool flag5 = m_Cookie != null;
							bool flag6 = !flag5;
							MaterialManager.HD.Cookie cookie = MaterialManager.HD.Cookie.Off;
							if (!flag6)
							{
								VolumetricCookieHD cookie2 = m_Cookie;
								if ((object)m_Cookie == null)
								{
									goto IL_02bd;
								}
								object obj2 = cookie2.m_Channel - 4;
								bool flag7 = obj2 == null;
								cookie = (MaterialManager.HD.Cookie)((flag7 ? 1 : 0) + 1);
							}
							((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->cookie = cookie;
							VolumetricLightBeamHD master5 = m_Master;
							if ((object)m_Master != null)
							{
								Config instance2 = Config.GetInstance(true);
								if ((object)instance2 != null)
								{
									int raymarchingQualityIndexForUniqueID = instance2.GetRaymarchingQualityIndexForUniqueID(master5.m_RaymarchingQualityID);
									((MaterialManager.StaticPropertiesHD*)(nint)staticPropertiesHD)->raymarchingQualityIndex = raymarchingQualityIndexForUniqueID;
									return staticPropertiesHD;
								}
							}
						}
					}
				}
			}
		}
		goto IL_02bd;
		IL_02bd:
		return (MaterialManager.StaticPropertiesHD)new NullReferenceException();
	}

	private bool ApplyMaterial()
	{
		//IL_00d7: Expected I4, but got O
		//IL_010f: Expected O, but got I4
		MaterialManager.StaticPropertiesHD staticPropertiesHD = ComputeMaterialStaticProperties();
		UnityEngine.Object obj;
		if (shouldUseGPUInstancedMaterial)
		{
			VolumetricLightBeamHD master = m_Master;
			if ((object)m_Master == null)
			{
				goto IL_00c9;
			}
			MaterialManager.BlendingMode blendingMode = default(MaterialManager.BlendingMode);
			MaterialManager.IStaticProperties staticProperties = (MaterialManager.StaticPropertiesHD)blendingMode;
			MaterialManager.IStaticProperties staticProps = default(MaterialManager.IStaticProperties);
			Material instancedMaterial = MaterialManager.GetInstancedMaterial(MaterialManager.ms_MaterialsGroupHD, master._003C_INTERNAL_InstancedMaterialGroupID_003Ek__BackingField, ref staticProps);
			obj = instancedMaterial;
		}
		else
		{
			obj = m_CustomMaterial;
			if ((bool)m_CustomMaterial)
			{
				MaterialManager.StaticPropertiesHD staticPropertiesHD2 = default(MaterialManager.StaticPropertiesHD);
				staticPropertiesHD2.ApplyToMaterial(m_CustomMaterial);
			}
		}
		if ((object)base._003CmeshRenderer_003Ek__BackingField != null)
		{
			((Renderer)base._003CmeshRenderer_003Ek__BackingField).SetMaterial((Material)obj);
			return obj != null;
		}
		goto IL_00c9;
		IL_00c9:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public void SetMaterialProp(int nameID, float value)
	{
		if (!m_CustomMaterial)
		{
			MaterialManager.materialPropertyBlock.SetFloatImpl(nameID, value);
		}
		else
		{
			m_CustomMaterial.SetFloat(nameID, value);
		}
	}

	public unsafe void SetMaterialProp(int nameID, Vector4 value)
	{
		//IL_0052: Expected O, but got Ref
		//IL_0039: Expected O, but got Ref
		object obj = default(object);
		if (!m_CustomMaterial)
		{
			MaterialManager.materialPropertyBlock.SetVector(nameID, (Vector4)(&obj));
		}
		else
		{
			m_CustomMaterial.SetVector(nameID, (Vector4)(&obj));
		}
	}

	public unsafe void SetMaterialProp(int nameID, Color value)
	{
		//IL_0052: Expected O, but got Ref
		//IL_0039: Expected O, but got Ref
		object obj = default(object);
		if (!m_CustomMaterial)
		{
			MaterialManager.materialPropertyBlock.SetColor(nameID, (Color)(&obj));
		}
		else
		{
			m_CustomMaterial.SetColor(nameID, (Color)(&obj));
		}
	}

	public unsafe void SetMaterialProp(int nameID, Matrix4x4 value)
	{
		//IL_0052: Expected O, but got Ref
		//IL_0039: Expected O, but got Ref
		object obj = default(object);
		if (!m_CustomMaterial)
		{
			MaterialManager.materialPropertyBlock.SetMatrix(nameID, (Matrix4x4)(&obj));
		}
		else
		{
			m_CustomMaterial.SetMatrix(nameID, (Matrix4x4)(&obj));
		}
	}

	public void SetMaterialProp(int nameID, Texture value)
	{
		if ((bool)m_CustomMaterial)
		{
			m_CustomMaterial.SetTexture(nameID, value);
		}
	}

	public void SetMaterialProp(int nameID, InvalidTexture invalidTexture)
	{
		if ((bool)m_CustomMaterial)
		{
			bool flag = invalidTexture != InvalidTexture.NoDepth;
			Texture2D value = null;
			if (!flag)
			{
				value = ((!SystemInfo.usesReversedZBuffer) ? Texture2D.whiteTexture : Texture2D.blackTexture);
			}
			m_CustomMaterial.SetTexture(nameID, value);
		}
	}

	private void MaterialChangeStart()
	{
		if (m_CustomMaterial == null)
		{
			((Renderer)base._003CmeshRenderer_003Ek__BackingField).Internal_GetPropertyBlock(MaterialManager.materialPropertyBlock);
		}
	}

	private void MaterialChangeStop()
	{
		if (m_CustomMaterial == null)
		{
			((Renderer)base._003CmeshRenderer_003Ek__BackingField).Internal_SetPropertyBlock(MaterialManager.materialPropertyBlock);
		}
	}

	public void SetPropertyDirty(DirtyProps prop)
	{
		//IL_004c: Expected I4, but got O
		//IL_0059: Expected I4, but got O
		DirtyProps dirtyProps = m_DirtyProps | prop;
		m_DirtyProps = dirtyProps;
		object obj = default(object);
		Enum mask = (DirtyProps)obj;
		object obj2 = default(object);
		Enum flags = (DirtyProps)obj2;
		if (Utils.HasAtLeastOneFlag(mask, flags))
		{
			UpdateMaterialAndBounds();
		}
	}

	private void UpdateMaterialAndBounds()
	{
		//IL_0076: Invalid comparison between F4 and I4
		if (ApplyMaterial())
		{
			MaterialChangeStart();
			VolumetricLightBeamHD master = m_Master;
			m_DirtyProps = DirtyProps.All;
			if (master.m_NoiseMode > NoiseMode.Disabled && master.m_NoiseIntensity > 0f && Noise3D.isSupported)
			{
				Noise3D.LoadIfNeeded();
			}
			Vector3 vector = ComputeLocalMatrix();
			UpdateMatricesPropertiesForGPUInstancingSRP();
			MaterialChangeStop();
		}
	}

	private unsafe void UpdateMatricesPropertiesForGPUInstancingSRP()
	{
		//IL_0076: Expected O, but got I4
		//IL_0140: Expected O, but got Ref
		//IL_0170: Expected O, but got Ref
		if (!SRPHelper.IsUsingCustomRenderPipeline())
		{
			return;
		}
		Config instance = Config.GetInstance(true);
		if (instance.m_RenderingMode == RenderingMode.SRPBatcher)
		{
			if (instance.m_RenderPipeline == RenderPipeline.BuiltIn)
			{
				return;
			}
			RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
			object obj = projectRenderPipeline - 1;
			if ((nint)obj > 1)
			{
				return;
			}
		}
		if ((instance.m_RenderPipeline == RenderPipeline.BuiltIn || instance.m_RenderingMode != RenderingMode.MultiPass) && instance.m_RenderingMode != RenderingMode.MultiPass && instance.m_RenderingMode == RenderingMode.GPUInstancing)
		{
			Transform transform = base.transform;
			Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
			float num = default(float);
			SetMaterialProp(ShaderProperties.LocalToWorldMatrix, (Matrix4x4)(&num));
			Transform transform2 = base.transform;
			Matrix4x4 worldToLocalMatrix = transform2.worldToLocalMatrix;
			SetMaterialProp(ShaderProperties.WorldToLocalMatrix, (Matrix4x4)(&num));
		}
	}

	private void OnBeginCameraRenderingSRP(ScriptableRenderContext context, Camera cam)
	{
		m_CurrentCameraRenderingSRP = cam;
	}

	private void OnWillRenderObject()
	{
		Camera cam = (SRPHelper.IsUsingCustomRenderPipeline() ? m_CurrentCameraRenderingSRP : Camera.current);
		OnWillCameraRenderThisBeam(cam);
	}

	private unsafe void OnWillCameraRenderThisBeam(Camera cam)
	{
		//IL_0183: Expected O, but got I
		//IL_0204: Expected O, but got I
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		//IL_0247: Expected O, but got I
		if (!m_Master || !cam || !cam.enabled)
		{
			return;
		}
		UpdateMaterialPropertiesForCamera(cam);
		if (!m_Shadow)
		{
			return;
		}
		Behaviour shadow = m_Shadow;
		if (!m_Shadow.enabled || !(cam != null) || !cam.enabled)
		{
			return;
		}
		int frameCount = Time.frameCount;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v5 (UnityEngine.Behaviour)+70]");
		if ((nint)frameCount == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v5 (UnityEngine.Behaviour)+24]");
		if ((nint)0 == 1)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v5 (UnityEngine.Behaviour)+24]");
		object obj = (nint)0 & (nint)4;
		if (obj != null)
		{
			Transform transf = m_Shadow.transform;
			TransformUtils.Packed packed = (TransformUtils.Packed)(m_Shadow + 72);
			if (!((TransformUtils.Packed*)packed)->IsSame(transf))
			{
				goto IL_0263;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v5 (UnityEngine.Behaviour)+24]");
		object obj2 = (nint)0 & (nint)8;
		if (obj2 != null)
		{
			int frameCount2 = Time.frameCount;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v5 (UnityEngine.Behaviour)+70]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rbx_v5 (UnityEngine.Behaviour)+28]");
			object obj3 = num + 0;
			if (frameCount2 >= (nint)obj3)
			{
				goto IL_0263;
			}
			return;
		}
		return;
		IL_0263:
		m_Shadow.ProcessOcclusion(VolumetricShadowHD.ProcessOcclusionSource.RenderLoop);
	}

	private unsafe void UpdateDirtyMaterialProperties()
	{
		//IL_0008: Expected O, but got Ref
		//IL_001d: Expected O, but got I4
		//IL_0068: Expected O, but got I4
		//IL_00eb: Expected O, but got I4
		//IL_0136: Expected O, but got I4
		//IL_02b6: Expected O, but got I4
		//IL_0452: Expected O, but got I4
		//IL_070a: Expected O, but got I4
		//IL_0748: Expected O, but got I4
		//IL_0503: Invalid comparison between F4 and I4
		//IL_0482: Expected O, but got Ref
		//IL_08fb: Expected O, but got Ref
		//IL_065a: Invalid comparison between F4 and I4
		//IL_01f6: Expected O, but got F4
		//IL_054c: Expected O, but got I4
		//IL_028d: Expected O, but got Ref
		//IL_059b: Expected O, but got I4
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Expected F4, but got Unknown
		//IL_025d: Expected O, but got Ref
		//IL_03b0: Expected O, but got Ref
		//IL_0400: Expected O, but got Ref
		//IL_069c: Expected O, but got I
		//IL_086f: Expected O, but got Ref
		//IL_0619: Expected O, but got I
		//IL_0937: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (m_DirtyProps == DirtyProps.None)
		{
			return;
		}
		object obj3 = m_DirtyProps & DirtyProps.Intensity;
		if (obj3 != null)
		{
			VolumetricLightBeamHD master = m_Master;
			SetMaterialProp(ShaderProperties.HD.Intensity, master.m_Intensity);
		}
		object obj4 = m_DirtyProps & DirtyProps.HDRPExposureWeight;
		if (obj4 != null)
		{
			Config instance = Config.GetInstance(true);
			if (instance.m_RenderPipeline == RenderPipeline.HDRP)
			{
				VolumetricLightBeamHD master2 = m_Master;
				SetMaterialProp(ShaderProperties.HDRPExposureWeight, master2.m_HDRPExposureWeight);
			}
		}
		object obj5 = m_DirtyProps & DirtyProps.SideSoftness;
		if (obj5 != null)
		{
			VolumetricLightBeamHD master3 = m_Master;
			SetMaterialProp(ShaderProperties.HD.SideSoftness, master3.m_SideSoftness);
		}
		object obj6 = m_DirtyProps & DirtyProps.Color;
		if (obj6 != null)
		{
			VolumetricLightBeamHD master4 = m_Master;
			Config instance2 = Config.GetInstance(true);
			if (instance2.featureEnabledColorGradient != FeatureEnabledColorGradient.Off && master4.m_ColorMode != ColorMode.Flat)
			{
				Utils.FloatPackingPrecision floatPackingPrecision = Utils.GetFloatPackingPrecision();
				VolumetricLightBeamHD master5 = m_Master;
				Matrix4x4 matrix4x = Utils.SampleInMatrix(master5.m_ColorGradient, (int)floatPackingPrecision);
				m_ColorGradientMatrix = (Matrix4x4)matrix4x.m00;
				_ = matrix4x.m01;
				_ = matrix4x.m02;
				_ = matrix4x.m03;
			}
			else
			{
				VolumetricLightBeamHD master6 = m_Master;
				if (!m_CustomMaterial)
				{
					_ = master6.m_ColorFlat;
					Color value = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
					MaterialManager.materialPropertyBlock.SetColor(ShaderProperties.ColorFlat, value);
				}
				else
				{
					_ = master6.m_ColorFlat;
					Color value2 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
					m_CustomMaterial.SetColor(ShaderProperties.ColorFlat, value2);
				}
			}
		}
		object obj7 = m_DirtyProps & DirtyProps.Cone;
		if (obj7 == null)
		{
			goto IL_0442;
		}
		VolumetricLightBeamHD master7 = m_Master;
		float coneRadiusStart = master7.m_ConeRadiusStart;
		if (!(master7.m_ConeRadiusStart > 0.0001f))
		{
			coneRadiusStart = 0.0001f;
		}
		float num = Utils.ComputeConeRadiusEnd(master7.m_FallOffEnd, master7.m_SpotAngle);
		bool flag = num > 0.0001f;
		float num2 = num;
		if (!flag)
		{
			num2 = 0.0001f;
		}
		Vector4 value3 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		_ = 0;
		SetMaterialProp(ShaderProperties.ConeRadius, value3);
		VolumetricLightBeamHD master8 = m_Master;
		float num3 = Utils.ComputeConeRadiusEnd(master8.m_FallOffEnd, master8.m_SpotAngle);
		float num4 = master8.m_ConeRadiusStart / num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 0000000180374CEAh\"");
		float num5;
		float num8;
		if (num4 == 1f)
		{
			num5 = 3.4028235E+38f;
		}
		else
		{
			float num6 = num4 * master8.m_FallOffEnd;
			float num7 = 1f - num4;
			num5 = num6 / num7;
			if (num5 < 0f)
			{
				num8 = -1f;
				goto IL_08b5;
			}
		}
		num8 = 1f;
		goto IL_08b5;
		IL_0442:
		object obj8 = m_DirtyProps & DirtyProps.Jittering;
		if (obj8 != null)
		{
			VolumetricLightBeamHD master9 = m_Master;
			Vector4 value4 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
			_ = master9.m_JitteringFactor;
			_ = master9.m_JitteringFrameRate;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v445 @ rcx_v30 (VLB.VolumetricLightBeamHD)+9C]");
			_ = 0;
			_ = master9.m_JitteringLerpRange;
			SetMaterialProp(ShaderProperties.HD.Jittering, value4);
		}
		VolumetricLightBeamHD master10 = m_Master;
		if (master10.m_NoiseMode > NoiseMode.Disabled && master10.m_NoiseIntensity > 0f && Noise3D.isSupported)
		{
			object obj9 = m_DirtyProps & (DirtyProps)0x3000;
			if (obj9 != null)
			{
				VolumetricLightBeamHD master11 = m_Master;
				if (master11.m_NoiseMode == NoiseMode.WorldSpace)
				{
					Vector4 value5 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
					_ = 0;
					SetMaterialProp(ShaderProperties.NoiseParam, value5);
				}
			}
			object obj10 = m_DirtyProps & DirtyProps.NoiseVelocityAndScale;
			if (obj10 != null)
			{
				VolumetricLightBeamHD master12 = m_Master;
				if (master12.m_NoiseVelocityUseGlobal)
				{
					Config instance3 = Config.GetInstance(true);
					Vector3 globalNoiseVelocity = instance3.globalNoiseVelocity;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v419 @ rax_v29 (VLB.Config)+5C]");
					object obj11 = 0;
				}
				else
				{
					Vector3 globalNoiseVelocity = master12.m_NoiseVelocityLocal;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ rax_v21 (VLB.VolumetricLightBeamHD)+BC]");
					object obj11 = 0;
				}
				VolumetricLightBeamHD master13 = m_Master;
				if (master13.m_NoiseScaleUseGlobal)
				{
					Config instance4 = Config.GetInstance(true);
					float globalNoiseScale = instance4.globalNoiseScale;
				}
				else
				{
					float globalNoiseScale = master13.m_NoiseScaleLocal;
				}
				Vector4 value6 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-59]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-55]");
				_ = 0;
				SetMaterialProp(ShaderProperties.NoiseVelocityAndScale, value6);
			}
		}
		object obj12 = m_DirtyProps & DirtyProps.CookieProps;
		if (obj12 != null)
		{
			VolumetricCookieHD.ApplyMaterialProperties(m_Cookie, this);
		}
		object obj13 = m_DirtyProps & DirtyProps.ShadowProps;
		if (obj13 != null)
		{
			VolumetricShadowHD.ApplyMaterialProperties(m_Shadow, this);
		}
		m_DirtyProps = DirtyProps.None;
		return;
		IL_08b5:
		float num9 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num10 = num9 & 0;
		if (!(num10 > 0.0001f))
		{
			num10 = 0.0001f;
		}
		Config instance5 = Config.GetInstance(true);
		Vector4 value7 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		_ = 0;
		float num11 = num10 * num8;
		_ = instance5.sharedMeshSides;
		SetMaterialProp(ShaderProperties.ConeGeomProps, value7);
		VolumetricLightBeamHD master14 = m_Master;
		Vector4 value8 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		_ = master14.m_FallOffStart;
		_ = master14.m_FallOffEnd;
		_ = master14.m_FallOffEnd;
		_ = 0;
		SetMaterialProp(ShaderProperties.DistanceFallOff, value8);
		Vector3 vector = ComputeLocalMatrix();
		goto IL_0442;
	}

	private unsafe void UpdateMaterialPropertiesForCamera(Camera cam)
	{
		//IL_0008: Expected O, but got Ref
		//IL_02f6: Expected I, but got O
		//IL_031f: Expected F4, but got I
		//IL_00b4: Expected O, but got F4
		//IL_02ba: Expected O, but got Ref
		//IL_00f0: Expected O, but got Ref
		//IL_0332: Expected O, but got Ref
		//IL_0167: Expected I, but got O
		//IL_0187: Expected F4, but got I
		//IL_03c4: Expected O, but got Ref
		//IL_01b3: Expected O, but got Ref
		//IL_0374: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if ((bool)cam && (bool)m_Master)
		{
			MaterialChangeStart();
			VolumetricLightBeamHD master = m_Master;
			if (master.m_Scalable)
			{
				Vector3 lossyScale = master.GetLossyScale();
				float z = lossyScale.z;
				Vector3 vector = (Vector3)lossyScale.x;
			}
			else
			{
				nint num = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ rax_v46 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num2 = 0;
				Vector3 vector = Vector3.oneVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v456 @ rcx_v40 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
				float z = 0f;
			}
			Vector4 value = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
			_ = 0;
			SetMaterialProp(ShaderProperties.HD.TransformScale, value);
			Transform transform = base.transform;
			Transform transform2 = cam.transform;
			Vector3 forward = transform2.forward;
			Vector3 direction = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
			_ = forward.x;
			_ = forward.z;
			Vector3 vector2 = transform.InverseTransformDirection(direction);
			_ = vector2.x;
			_ = vector2.z;
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			if (vector2.x > 1E-05f)
			{
				float num3 = vector2.z / vector2.x;
				float num4 = num3;
			}
			else
			{
				nint num5 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v513 @ rax_v43 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v514 @ rcx_v37 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				float num4 = 0f;
				_ = Vector3.zeroVector;
			}
			Vector4 value2 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-15]");
			_ = 0;
			_ = 0;
			SetMaterialProp(ShaderProperties.HD.CameraForwardOS, value2);
			Transform transform3 = cam.transform;
			Vector3 forward2 = transform3.forward;
			_ = 0;
			Vector4 value3 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
			_ = forward2.x;
			_ = forward2.z;
			SetMaterialProp(ShaderProperties.HD.CameraForwardWS, value3);
			UpdateDirtyMaterialProperties();
			VolumetricLightBeamHD master2 = m_Master;
			Config instance = Config.GetInstance(true);
			if (instance.featureEnabledColorGradient != FeatureEnabledColorGradient.Off && master2.m_ColorMode == ColorMode.Gradient)
			{
				Matrix4x4 value4 = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
				_ = m_ColorGradientMatrix;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.BeamGeometryHD)+48]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.BeamGeometryHD)+58]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.BeamGeometryHD)+68]");
				_ = 0;
				SetMaterialProp(ShaderProperties.ColorGradientMatrix, value4);
			}
			UpdateMatricesPropertiesForGPUInstancingSRP();
			MaterialChangeStop();
			DepthTextureMode depthTextureMode = cam.depthTextureMode;
			DepthTextureMode depthTextureMode2 = depthTextureMode | DepthTextureMode.Depth;
			cam.depthTextureMode = depthTextureMode2;
		}
	}
}
