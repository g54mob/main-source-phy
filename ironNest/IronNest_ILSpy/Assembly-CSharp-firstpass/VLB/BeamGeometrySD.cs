using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace VLB;

public class BeamGeometrySD : BeamGeometryAbstractBase, MaterialModifier.Interface
{
	private sealed class _003CCoUpdateFadeOut_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BeamGeometrySD _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoUpdateFadeOut_003Ed__17(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_005a: Expected I4, but got I8
			//IL_0147: Expected I4, but got O
			//IL_00b6: Invalid comparison between F4 and I4
			//IL_00d8: Invalid comparison between F4 and I4
			BeamGeometrySD beamGeometrySD = _003C_003E4__this;
			if (_003C_003E1__state == 0 || _003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					VolumetricLightBeamSD master = beamGeometrySD.m_Master;
					if ((object)beamGeometrySD.m_Master != null)
					{
						if (!(master._FadeOutBegin < 0f) && !(master._FadeOutEnd < 0f))
						{
							_003C_003E4__this.ComputeFadeOutFactor();
							_003C_003E2__current = null;
							_003C_003E1__state = 1;
							return true;
						}
						_003C_003E4__this.SetFadeOutFactorProp(1f);
						beamGeometrySD.m_CoFadeOut = null;
						goto IL_0133;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_0133;
			IL_0133:
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private VolumetricLightBeamSD m_Master;

	private MeshType m_CurrentMeshType;

	private MaterialModifier.Callback m_MaterialModifierCallback;

	private Coroutine m_CoFadeOut;

	private Camera m_CurrentCameraRenderingSRP;

	private bool visible
	{
		get
		{
			//IL_0041: Expected I4, but got O
			if ((object)base._003CmeshRenderer_003Ek__BackingField != null)
			{
				return base._003CmeshRenderer_003Ek__BackingField.enabled;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		set
		{
			base._003CmeshRenderer_003Ek__BackingField.enabled = value;
		}
	}

	public int sortingLayerID
	{
		get
		{
			//IL_0041: Expected I4, but got O
			if ((object)base._003CmeshRenderer_003Ek__BackingField != null)
			{
				return base._003CmeshRenderer_003Ek__BackingField.sortingLayerID;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		set
		{
			base._003CmeshRenderer_003Ek__BackingField.sortingLayerID = value;
		}
	}

	public int sortingOrder
	{
		get
		{
			//IL_0041: Expected I4, but got O
			if ((object)base._003CmeshRenderer_003Ek__BackingField != null)
			{
				return base._003CmeshRenderer_003Ek__BackingField.sortingOrder;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		set
		{
			base._003CmeshRenderer_003Ek__BackingField.sortingOrder = value;
		}
	}

	public bool _INTERNAL_IsFadeOutCoroutineRunning
	{
		get
		{
			bool flag = (nint)m_CoFadeOut < 0;
			bool flag2 = m_CoFadeOut == null;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public static bool isCustomRenderPipelineSupported => true;

	private bool shouldUseGPUInstancedMaterial
	{
		get
		{
			//IL_018d: Expected I4, but got O
			//IL_019b: Expected O, but got I4
			//IL_010c: Expected O, but got I4
			//IL_00dd: Expected O, but got I4
			Config instance;
			if ((object)m_Master != null)
			{
				MaterialManager.SD.DynamicOcclusion iNTERNAL_DynamicOcclusionMode = m_Master._INTERNAL_DynamicOcclusionMode;
				if (iNTERNAL_DynamicOcclusionMode == MaterialManager.SD.DynamicOcclusion.DepthTexture)
				{
					return false;
				}
				instance = Config.GetInstance(true);
				if ((object)instance != null)
				{
					if (instance.m_RenderingMode == RenderingMode.SRPBatcher)
					{
						if (instance.m_RenderPipeline != RenderPipeline.BuiltIn)
						{
							RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
							object obj = projectRenderPipeline - 1;
							if ((nint)obj <= 1)
							{
								goto IL_0120;
							}
						}
						object obj2 = 1 - 2;
						return obj2 == null;
					}
					goto IL_0120;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0120:
			RenderingMode renderingMode;
			if (instance.m_RenderPipeline != RenderPipeline.BuiltIn)
			{
				bool flag = instance.m_RenderingMode == RenderingMode.MultiPass;
				renderingMode = RenderingMode.Default;
				if (flag)
				{
					goto IL_018d;
				}
			}
			renderingMode = instance.m_RenderingMode;
			goto IL_018d;
			IL_018d:
			object obj3 = renderingMode - 2;
			return obj3 == null;
		}
	}

	private bool isNoiseEnabled
	{
		get
		{
			//IL_00b7: Expected I4, but got O
			//IL_007e: Invalid comparison between F4 and I4
			if ((object)m_Master != null)
			{
				if (m_Master.isNoiseEnabled)
				{
					VolumetricLightBeamSD master = m_Master;
					if ((object)m_Master == null)
					{
						goto IL_00a9;
					}
					if (master.noiseIntensity > 0f)
					{
						return Noise3D.isSupported;
					}
				}
				return false;
			}
			goto IL_00a9;
			IL_00a9:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private bool isDepthBlendEnabled
	{
		get
		{
			//IL_01bb: Expected I4, but got O
			//IL_0089: Expected O, but got I4
			//IL_0165: Invalid comparison between F4 and I4
			//IL_0179: Invalid comparison between F4 and I4
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
				goto IL_012e;
			}
			goto IL_01ad;
			IL_00a6:
			if ((instance.m_RenderPipeline != RenderPipeline.BuiltIn && instance.m_RenderingMode == RenderingMode.MultiPass) || (instance.m_RenderingMode != RenderingMode.GPUInstancing && instance.m_RenderingMode != RenderingMode.SRPBatcher))
			{
				goto IL_012e;
			}
			return true;
			IL_012e:
			VolumetricLightBeamSD master = m_Master;
			if ((object)m_Master != null)
			{
				bool flag = master.depthBlendDistance < 0f;
				bool flag2 = master.depthBlendDistance == 0f;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				return flag4 & flag3;
			}
			goto IL_01ad;
			IL_01ad:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	protected override VolumetricLightBeamAbstractBase GetMaster()
	{
		return m_Master;
	}

	private float ComputeFadeOutFactor(Transform camTransform)
	{
		//IL_021b: Expected F4, but got I4
		//IL_01d8: Invalid comparison between I4 and F4
		float num14;
		if ((object)m_Master != null)
		{
			if (!m_Master.isFadeOutEnabled)
			{
				return 1f;
			}
			if ((object)base._003CmeshRenderer_003Ek__BackingField != null)
			{
				Bounds bounds = base._003CmeshRenderer_003Ek__BackingField.bounds;
				if ((object)camTransform != null)
				{
					Vector3 position = camTransform.position;
					VolumetricLightBeamSD master = m_Master;
					float num = (float)bounds.m_Center - position.x;
					float num3 = default(float);
					float num2 = num3 - num3;
					float num4 = num3 - position.z;
					if ((object)m_Master != null)
					{
						float num5 = num2 * num2;
						float num6 = num * num;
						float num7 = master._FadeOutBegin * master._FadeOutBegin;
						float num8 = num5 + num6;
						float num9 = master._FadeOutEnd * master._FadeOutEnd;
						float num10 = num4 * num4;
						bool flag = num9 == num7;
						float num11 = num8 + num10;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180375BE5h\"");
						if (!flag)
						{
							float num12 = num7 - num9;
							float num13 = num11 - num9;
							num14 = num13 / num12;
							if (!(0f > num14))
							{
								if (num14 > 1f)
								{
									return 1f;
								}
								goto IL_022b;
							}
						}
						num14 = 0f;
						goto IL_022b;
					}
				}
			}
		}
		throw new NullReferenceException();
		IL_022b:
		return num14;
	}

	private IEnumerator CoUpdateFadeOut()
	{
		_003CCoUpdateFadeOut_003Ed__17 obj = new _003CCoUpdateFadeOut_003Ed__17(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private void ComputeFadeOutFactor()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_01f6: Expected F4, but got I4
		//IL_01aa: Invalid comparison between I4 and F4
		Config instance = Config.GetInstance(true);
		Transform fadeOutCameraTransform = instance.fadeOutCameraTransform;
		float fadeOutFactorProp;
		float num9;
		if (!fadeOutCameraTransform)
		{
			fadeOutFactorProp = 1f;
		}
		else
		{
			if (m_Master.isFadeOutEnabled)
			{
				Bounds bounds = base._003CmeshRenderer_003Ek__BackingField.bounds;
				Vector3 position = fadeOutCameraTransform.position;
				VolumetricLightBeamSD master = m_Master;
				float num = (float)bounds.m_Center - position.x;
				object obj2 = default(object);
				object obj = obj2 - obj2;
				object obj3 = obj2 - position.z;
				object obj4 = obj * obj;
				float num2 = num * num;
				float num3 = master._FadeOutBegin * master._FadeOutBegin;
				float num4 = (float)obj4 + num2;
				float num5 = master._FadeOutEnd * master._FadeOutEnd;
				object obj5 = obj3 * obj3;
				bool flag = num5 == num3;
				float num6 = num4 + (float)obj5;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180375D9Bh\"");
				if (!flag)
				{
					float num7 = num3 - num5;
					float num8 = num6 - num5;
					num9 = num8 / num7;
					if (!(0f > num9))
					{
						if (num9 > 1f)
						{
							fadeOutFactorProp = 1f;
							goto IL_020e;
						}
						goto IL_0219;
					}
				}
				num9 = 0f;
				goto IL_0219;
			}
			fadeOutFactorProp = 1f;
		}
		goto IL_020e;
		IL_020e:
		SetFadeOutFactorProp(fadeOutFactorProp);
		return;
		IL_0219:
		fadeOutFactorProp = num9;
		goto IL_020e;
	}

	private void SetFadeOutFactorProp(float value)
	{
		//IL_000e: Invalid comparison between F4 and I4
		if (!(value > 0f))
		{
			base._003CmeshRenderer_003Ek__BackingField.enabled = false;
			return;
		}
		base._003CmeshRenderer_003Ek__BackingField.enabled = true;
		MaterialChangeStart();
		SetMaterialProp(ShaderProperties.SD.FadeOutFactor, value);
		MaterialChangeStop();
	}

	private void StopFadeOutCoroutine()
	{
		if (m_CoFadeOut != null)
		{
			StopCoroutine(m_CoFadeOut);
			m_CoFadeOut = null;
		}
	}

	public void RestartFadeOutCoroutine()
	{
		if (m_CoFadeOut != null)
		{
			StopCoroutine(m_CoFadeOut);
			m_CoFadeOut = null;
		}
		if ((bool)m_Master && m_Master.isFadeOutEnabled)
		{
			_003CCoUpdateFadeOut_003Ed__17 obj = new _003CCoUpdateFadeOut_003Ed__17(0);
			obj._003C_003E4__this = this;
			Coroutine coFadeOut = StartCoroutine(obj);
			m_CoFadeOut = coFadeOut;
		}
	}

	public void OnMasterEnable()
	{
		base._003CmeshRenderer_003Ek__BackingField.enabled = true;
		RestartFadeOutCoroutine();
	}

	public void OnMasterDisable()
	{
		if (m_CoFadeOut != null)
		{
			StopCoroutine(m_CoFadeOut);
			m_CoFadeOut = null;
		}
		base._003CmeshRenderer_003Ek__BackingField.enabled = false;
	}

	private void OnDisable()
	{
		Action<ScriptableRenderContext, Camera> cb = OnBeginCameraRenderingSRP;
		SRPHelper.UnregisterOnBeginCameraRendering(cb);
		m_CurrentCameraRenderingSRP = null;
	}

	private void OnEnable()
	{
		RestartFadeOutCoroutine();
		Action<ScriptableRenderContext, Camera> cb = OnBeginCameraRenderingSRP;
		SRPHelper.RegisterOnBeginCameraRendering(cb);
	}

	public void Initialize(VolumetricLightBeamSD master)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected I4, but got Unknown
		//IL_010e: Expected O, but got I4
		//IL_0171: Expected O, but got I4
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
		bool flag = shouldUseGPUInstancedMaterial;
		bool flag2 = false;
		object obj2 = 0;
		if (!flag)
		{
			Config instance = Config.GetInstance(true);
			Material customMaterial = instance.NewMaterialTransient(ShaderMode.SD, gpuInstanced: false);
			m_CustomMaterial = customMaterial;
			bool flag3 = ApplyMaterial();
			flag2 = false;
			obj2 = 0;
		}
		VolumetricLightBeamSD master2 = m_Master;
		bool flag4 = SortingLayer.IsValid(master2._SortingLayerID);
		Component master3 = m_Master;
		if (!flag4)
		{
			Transform current = master3.transform;
			string path = Utils.GetPath(current);
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"Beam '{path}' has an invalid sortingLayerID ({arg}). Please fix it by setting a valid layer.";
			Debug.LogError(message);
		}
		else
		{
			MeshRenderer obj3 = base._003CmeshRenderer_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rcx_v24 (UnityEngine.Component)+124]");
			obj3.sortingLayerID = 0;
		}
		VolumetricLightBeamSD master4 = m_Master;
		base._003CmeshRenderer_003Ek__BackingField.sortingOrder = master4._SortingOrder;
		GameObject self2 = base.gameObject;
		MeshFilter orAddComponent2 = Utils.GetOrAddComponent<MeshFilter>(self2);
		base._003CmeshFilter_003Ek__BackingField = orAddComponent2;
		base._003CmeshFilter_003Ek__BackingField.hideFlags = hideFlags;
		GameObject gameObject = base.gameObject;
		gameObject.hideFlags = hideFlags;
		RestartFadeOutCoroutine();
	}

	public void RegenerateMesh(bool masterEnabled)
	{
		//IL_024f: Expected O, but got I4
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
		if ((bool)base._003CconeMesh_003Ek__BackingField && m_CurrentMeshType == MeshType.Custom)
		{
			UnityEngine.Object.DestroyImmediate(base._003CconeMesh_003Ek__BackingField);
		}
		VolumetricLightBeamSD master = m_Master;
		m_CurrentMeshType = master.geomMeshType;
		VolumetricLightBeamSD master2 = m_Master;
		VolumetricLightBeamSD master3;
		Config instance4;
		if (master2.geomMeshType == MeshType.Shared)
		{
			Mesh mesh = GlobalMeshSD.Get();
			base._003CconeMesh_003Ek__BackingField = mesh;
			base._003CmeshFilter_003Ek__BackingField.sharedMesh = base._003CconeMesh_003Ek__BackingField;
		}
		else
		{
			if (master2.geomMeshType == MeshType.Custom)
			{
				master3 = m_Master;
				instance4 = Config.GetInstance(true);
				if (instance4.m_RenderingMode != RenderingMode.SRPBatcher)
				{
					goto IL_0274;
				}
				if (instance4.m_RenderPipeline != RenderPipeline.BuiltIn)
				{
					RenderPipeline projectRenderPipeline = SRPHelper.projectRenderPipeline;
					object obj = projectRenderPipeline - 1;
					if ((nint)obj <= 1)
					{
						goto IL_0274;
					}
				}
				goto IL_0343;
			}
			Debug.LogError("Unsupported MeshType");
		}
		goto IL_02eb;
		IL_02eb:
		UpdateMaterialAndBounds();
		base._003CmeshRenderer_003Ek__BackingField.enabled = masterEnabled;
		return;
		IL_02b3:
		HideFlags proceduralObjectsHideFlags = Consts.Internal.ProceduralObjectsHideFlags;
		base._003CconeMesh_003Ek__BackingField.hideFlags = proceduralObjectsHideFlags;
		base._003CmeshFilter_003Ek__BackingField.mesh = base._003CconeMesh_003Ek__BackingField;
		goto IL_02eb;
		IL_0274:
		if (instance4.m_RenderPipeline != RenderPipeline.BuiltIn && instance4.m_RenderingMode != RenderingMode.MultiPass)
		{
			goto IL_02b3;
		}
		goto IL_0343;
		IL_0343:
		int numSegments = default(int);
		bool cap = default(bool);
		bool doubleSided = default(bool);
		Mesh mesh2 = MeshGenerator.GenerateConeZ_Radii(1f, 1f, 1f, master3.geomCustomSides, numSegments, cap, doubleSided);
		base._003CconeMesh_003Ek__BackingField = mesh2;
		goto IL_02b3;
	}

	private unsafe Vector3 ComputeLocalMatrix()
	{
		//IL_0090: Expected O, but got Ref
		//IL_00f7: Expected O, but got Ref
		//IL_013d: Expected native int or pointer, but got O
		//IL_014f: Expected native int or pointer, but got O
		VolumetricLightBeamSD master = m_Master;
		if ((object)m_Master != null)
		{
			float coneRadiusEnd = m_Master.coneRadiusEnd;
			Transform transform = default(Transform);
			if (!(master.coneRadiusStart < coneRadiusEnd))
			{
				transform = base.transform;
				if ((object)m_Master == null)
				{
					goto IL_0159;
				}
			}
			float maxGeometryDistance = m_Master.maxGeometryDistance;
			if ((object)transform != null)
			{
				object obj = default(object);
				transform.localScale = (Vector3)(&obj);
				Transform transform2 = base.transform;
				if ((object)m_Master != null)
				{
					Quaternion beamInternalLocalRotation = m_Master.beamInternalLocalRotation;
					if ((object)transform2 != null)
					{
						transform2.localRotation = (Quaternion)(&obj);
						Transform transform3 = base.transform;
						if ((object)transform3 != null)
						{
							Vector3 localScale = transform3.localScale;
							Vector3 vector = default(Vector3);
							((Vector3*)(nint)vector)->x = localScale.x;
							((Vector3*)(nint)vector)->z = localScale.z;
							return vector;
						}
					}
				}
			}
		}
		goto IL_0159;
		IL_0159:
		return (Vector3)new NullReferenceException();
	}

	private unsafe MaterialManager.StaticPropertiesSD ComputeMaterialStaticProperties()
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_0017: Expected native int or pointer, but got O
		//IL_0025: Expected native int or pointer, but got O
		//IL_0245: Expected native int or pointer, but got O
		//IL_0253: Expected native int or pointer, but got O
		//IL_0261: Expected native int or pointer, but got O
		//IL_0098: Expected O, but got I4
		//IL_00c7: Expected native int or pointer, but got O
		//IL_0297: Expected native int or pointer, but got O
		//IL_02ae: Expected native int or pointer, but got O
		//IL_02bb: Expected native int or pointer, but got O
		//IL_0194: Expected native int or pointer, but got O
		//IL_014a: Invalid comparison between F4 and I4
		//IL_01cf: Expected native int or pointer, but got O
		//IL_0224: Expected native int or pointer, but got O
		MaterialManager.StaticPropertiesSD staticPropertiesSD = default(MaterialManager.StaticPropertiesSD);
		((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->blendingMode = MaterialManager.BlendingMode.Additive;
		((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->dynamicOcclusion = MaterialManager.SD.DynamicOcclusion.Off;
		((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->shaderAccuracy = MaterialManager.SD.ShaderAccuracy.Fast;
		VolumetricLightBeamSD master = m_Master;
		MaterialManager.ColorGradient colorGradient;
		bool noise3D;
		if ((object)m_Master != null)
		{
			bool flag = master.colorMode != ColorMode.Gradient;
			colorGradient = MaterialManager.ColorGradient.Off;
			if (!flag)
			{
				Utils.FloatPackingPrecision floatPackingPrecision = Utils.GetFloatPackingPrecision();
				object obj = floatPackingPrecision - 64;
				bool flag2 = obj == null;
				colorGradient = (MaterialManager.ColorGradient)((flag2 ? 1 : 0) + 1);
			}
			((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->blendingMode = MaterialManager.BlendingMode.Additive;
			((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->dynamicOcclusion = MaterialManager.SD.DynamicOcclusion.Off;
			((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->shaderAccuracy = MaterialManager.SD.ShaderAccuracy.Fast;
			VolumetricLightBeamSD master2 = m_Master;
			if ((object)m_Master != null)
			{
				((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->blendingMode = (MaterialManager.BlendingMode)master2.blendingMode;
				if ((object)m_Master != null)
				{
					if (m_Master.isNoiseEnabled)
					{
						VolumetricLightBeamSD master3 = m_Master;
						if ((object)m_Master == null)
						{
							goto IL_022e;
						}
						if (master3.noiseIntensity > 0f)
						{
							noise3D = Noise3D.isSupported;
							goto IL_028f;
						}
					}
					noise3D = false;
					goto IL_028f;
				}
			}
		}
		goto IL_022e;
		IL_022e:
		return (MaterialManager.StaticPropertiesSD)new NullReferenceException();
		IL_028f:
		((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->noise3D = (noise3D ? MaterialManager.Noise3D.On : MaterialManager.Noise3D.Off);
		bool depthBlend = isDepthBlendEnabled;
		((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->depthBlend = (depthBlend ? MaterialManager.SD.DepthBlend.On : MaterialManager.SD.DepthBlend.Off);
		((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->colorGradient = colorGradient;
		if ((object)m_Master != null)
		{
			MaterialManager.SD.DynamicOcclusion iNTERNAL_DynamicOcclusionMode_Runtime = m_Master._INTERNAL_DynamicOcclusionMode_Runtime;
			((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->dynamicOcclusion = iNTERNAL_DynamicOcclusionMode_Runtime;
			if ((object)m_Master != null)
			{
				bool hasMeshSkewing = m_Master.hasMeshSkewing;
				((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->meshSkewing = (hasMeshSkewing ? MaterialManager.SD.MeshSkewing.On : MaterialManager.SD.MeshSkewing.Off);
				VolumetricLightBeamSD master4 = m_Master;
				if ((object)m_Master != null)
				{
					bool flag3 = master4.shaderAccuracy == ShaderAccuracy.Fast;
					bool shaderAccuracy = !flag3;
					((MaterialManager.StaticPropertiesSD*)(nint)staticPropertiesSD)->shaderAccuracy = (shaderAccuracy ? MaterialManager.SD.ShaderAccuracy.High : MaterialManager.SD.ShaderAccuracy.Fast);
					return staticPropertiesSD;
				}
			}
		}
		goto IL_022e;
	}

	private bool ApplyMaterial()
	{
		//IL_022f: Expected I4, but got O
		//IL_00b7: Invalid comparison between F4 and I4
		VolumetricLightBeamSD master = m_Master;
		if ((object)m_Master != null)
		{
			if (master.colorMode == ColorMode.Gradient)
			{
				Utils.FloatPackingPrecision floatPackingPrecision = Utils.GetFloatPackingPrecision();
			}
			if ((object)m_Master != null && (object)m_Master != null)
			{
				if (m_Master.isNoiseEnabled)
				{
					VolumetricLightBeamSD master2 = m_Master;
					if ((object)m_Master == null)
					{
						goto IL_0221;
					}
					if (master2.noiseIntensity > 0f)
					{
						bool isSupported = Noise3D.isSupported;
					}
				}
				bool flag = isDepthBlendEnabled;
				if ((object)m_Master != null)
				{
					MaterialManager.SD.DynamicOcclusion iNTERNAL_DynamicOcclusionMode_Runtime = m_Master._INTERNAL_DynamicOcclusionMode_Runtime;
					if ((object)m_Master != null)
					{
						bool hasMeshSkewing = m_Master.hasMeshSkewing;
						if ((object)m_Master != null)
						{
							UnityEngine.Object obj;
							if (shouldUseGPUInstancedMaterial)
							{
								VolumetricLightBeamSD master3 = m_Master;
								if ((object)m_Master == null)
								{
									goto IL_0221;
								}
								MaterialManager.StaticPropertiesSD staticPropertiesSD = default(MaterialManager.StaticPropertiesSD);
								MaterialManager.IStaticProperties staticProperties = staticPropertiesSD;
								MaterialManager.IStaticProperties staticProps = default(MaterialManager.IStaticProperties);
								Material instancedMaterial = MaterialManager.GetInstancedMaterial(MaterialManager.ms_MaterialsGroupSD, master3._003C_INTERNAL_InstancedMaterialGroupID_003Ek__BackingField, ref staticProps);
								obj = instancedMaterial;
							}
							else
							{
								obj = m_CustomMaterial;
								if ((bool)m_CustomMaterial)
								{
									MaterialManager.StaticPropertiesSD staticPropertiesSD2 = default(MaterialManager.StaticPropertiesSD);
									staticPropertiesSD2.ApplyToMaterial(m_CustomMaterial);
								}
							}
							if ((object)base._003CmeshRenderer_003Ek__BackingField != null)
							{
								((Renderer)base._003CmeshRenderer_003Ek__BackingField).SetMaterial((Material)obj);
								return obj != null;
							}
						}
					}
				}
			}
		}
		goto IL_0221;
		IL_0221:
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
		if (!m_CustomMaterial)
		{
			Debug.LogError("Setting a Texture property to a GPU instanced material is not supported");
		}
		else
		{
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

	public void SetDynamicOcclusionCallback(string shaderKeyword, MaterialModifier.Callback cb)
	{
		m_MaterialModifierCallback = cb;
		if (!m_CustomMaterial)
		{
			UpdateMaterialAndBounds();
			return;
		}
		bool flag = cb == null;
		bool flag2 = !flag;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039C980");
		if (cb != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: cb.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public unsafe void UpdateMaterialAndBounds()
	{
		//IL_0008: Expected O, but got Ref
		//IL_085a: Expected O, but got Ref
		//IL_0110: Invalid comparison between F4 and I4
		//IL_09a3: Expected O, but got Ref
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected F4, but got Unknown
		//IL_019e: Expected O, but got Ref
		//IL_020c: Expected O, but got F4
		//IL_08ab: Expected F4, but got I
		//IL_08c2: Expected F4, but got I
		//IL_0288: Expected O, but got Ref
		//IL_026f: Expected O, but got Ref
		//IL_0306: Expected O, but got Ref
		//IL_03ee: Expected F4, but got I4
		//IL_0403: Expected O, but got Ref
		//IL_0426: Expected O, but got Ref
		//IL_0514: Invalid comparison between F4 and I4
		//IL_0674: Expected O, but got Ref
		//IL_06a5: Expected O, but got Ref
		//IL_0966: Expected O, but got Ref
		//IL_09b7: Expected O, but got Ref
		//IL_093b: Expected O, but got Ref
		//IL_07bb: Invalid comparison between F4 and I4
		//IL_0974: Invalid comparison between F4 and I4
		//IL_07f1: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		if (!ApplyMaterial())
		{
			return;
		}
		MaterialChangeStart();
		bool flag = m_CustomMaterial == null;
		bool flag2 = !flag;
		nint num = 0;
		if (!flag2)
		{
			bool flag3 = m_MaterialModifierCallback == null;
			num = 0;
			if (!flag3)
			{
				MaterialModifier.Callback materialModifierCallback = m_MaterialModifierCallback;
				num = ((Delegate)materialModifierCallback).method;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v350.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
		float coneAngle = m_Master.coneAngle;
		float num2 = coneAngle * ((float)Math.PI / 180f);
		float num3 = num2 * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		float num4 = default(float);
		SetMaterialProp(ShaderProperties.SD.ConeSlopeCosSin, (Vector4)(&num4));
		VolumetricLightBeamSD master = m_Master;
		if (master.coneRadiusStart > 0.0001f)
		{
			float coneRadiusEnd = master.coneRadiusEnd;
			if (!(coneRadiusEnd > 0.0001f))
			{
			}
			SetMaterialProp(ShaderProperties.ConeRadius, (Vector4)(&num4));
		}
		float coneApexOffsetZ = m_Master.coneApexOffsetZ;
		if (!(coneApexOffsetZ < 0f))
		{
		}
		float coneApexOffsetZ2 = m_Master.coneApexOffsetZ;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num5 = coneApexOffsetZ2 & 0;
		if (!(num5 > 0.0001f))
		{
			num5 = 0.0001f;
		}
		int geomSides = m_Master.geomSides;
		SetMaterialProp(ShaderProperties.ConeGeomProps, (Vector4)(&num4));
		if (m_Master.usedColorMode != ColorMode.Flat)
		{
			Utils.FloatPackingPrecision floatPackingPrecision = Utils.GetFloatPackingPrecision();
			VolumetricLightBeamSD master2 = m_Master;
			Matrix4x4 matrix4x = Utils.SampleInMatrix(master2.colorGradient, (int)floatPackingPrecision);
			m_ColorGradientMatrix = (Matrix4x4)matrix4x.m00;
			_ = matrix4x.m01;
			_ = matrix4x.m02;
			_ = matrix4x.m03;
		}
		else if (!m_CustomMaterial)
		{
			Color color = default(Color);
			MaterialManager.materialPropertyBlock.SetColor(ShaderProperties.ColorFlat, (Color)(&color));
		}
		else
		{
			Color color2 = default(Color);
			m_CustomMaterial.SetColor(ShaderProperties.ColorFlat, (Color)(&color2));
		}
		m_Master.GetInsideAndOutsideIntensity(out System.Runtime.CompilerServices.Unsafe.As<object, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64)), out System.Runtime.CompilerServices.Unsafe.As<object, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72)));
		int alphaInside = ShaderProperties.SD.AlphaInside;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+40]");
		SetMaterialProp(alphaInside, 0f);
		int alphaOutside = ShaderProperties.SD.AlphaOutside;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+48]");
		SetMaterialProp(alphaOutside, 0f);
		float attenuationLerpLinearQuad = m_Master.attenuationLerpLinearQuad;
		SetMaterialProp(ShaderProperties.SD.AttenuationLerpLinearQuad, attenuationLerpLinearQuad);
		VolumetricLightBeamSD master3 = m_Master;
		float maxGeometryDistance = master3.maxGeometryDistance;
		SetMaterialProp(ShaderProperties.DistanceFallOff, (Vector4)(&num4));
		VolumetricLightBeamSD master4 = m_Master;
		SetMaterialProp(ShaderProperties.SD.DistanceCamClipping, master4.cameraClippingDistance);
		VolumetricLightBeamSD master5 = m_Master;
		bool flag4 = 0.001f > master5.fresnelPow;
		float value = 0.001f;
		if (!flag4)
		{
			value = master5.fresnelPow;
		}
		SetMaterialProp(ShaderProperties.SD.FresnelPow, value);
		VolumetricLightBeamSD master6 = m_Master;
		SetMaterialProp(ShaderProperties.SD.GlareBehind, master6.glareBehind);
		VolumetricLightBeamSD master7 = m_Master;
		SetMaterialProp(ShaderProperties.SD.GlareFrontal, master7.glareFrontal);
		VolumetricLightBeamSD master8 = m_Master;
		bool flag5 = !master8.geomCap;
		bool flag6 = !flag5;
		SetMaterialProp(ShaderProperties.SD.DrawCap, flag6 ? 1 : 0);
		SetMaterialProp(ShaderProperties.SD.TiltVector, (Vector4)(&num4));
		Vector4 additionalClippingPlane = m_Master.additionalClippingPlane;
		Vector2 vector = default(Vector2);
		SetMaterialProp(ShaderProperties.SD.AdditionalClippingPlaneWS, (Vector4)(&vector));
		Config instance = Config.GetInstance(true);
		if (instance.m_RenderPipeline == RenderPipeline.HDRP)
		{
			VolumetricLightBeamSD master9 = m_Master;
			SetMaterialProp(ShaderProperties.HDRPExposureWeight, master9.hdrpExposureWeight);
		}
		if (isDepthBlendEnabled)
		{
			VolumetricLightBeamSD master10 = m_Master;
			SetMaterialProp(ShaderProperties.SD.DepthBlendDistance, master10.depthBlendDistance);
		}
		bool flag7 = m_Master.isNoiseEnabled;
		bool flag8 = !flag7;
		num4 = additionalClippingPlane.x;
		if (!flag8)
		{
			VolumetricLightBeamSD master11 = m_Master;
			bool flag9 = !(master11.noiseIntensity > 0f);
			num4 = additionalClippingPlane.x;
			if (!flag9)
			{
				bool isSupported = Noise3D.isSupported;
				bool flag10 = !isSupported;
				num4 = additionalClippingPlane.x;
				if (!flag10)
				{
					Noise3D.LoadIfNeeded();
					VolumetricLightBeamSD master12 = m_Master;
					if (master12.noiseVelocityUseGlobal)
					{
						Config instance2 = Config.GetInstance(true);
					}
					VolumetricLightBeamSD master13 = m_Master;
					if (master13.noiseScaleUseGlobal)
					{
						Config instance3 = Config.GetInstance(true);
					}
					SetMaterialProp(ShaderProperties.NoiseVelocityAndScale, (Vector4)(&num4));
					VolumetricLightBeamSD master14 = m_Master;
					if (master14.noiseMode == NoiseMode.WorldSpace)
					{
					}
					SetMaterialProp(ShaderProperties.NoiseParam, (Vector4)(&num4));
					float num6 = default(float);
					num4 = num6;
				}
			}
		}
		VolumetricLightBeamSD master15 = m_Master;
		float coneRadiusEnd2 = m_Master.coneRadiusEnd;
		Transform transform = default(Transform);
		if (master15.coneRadiusStart > coneRadiusEnd2)
		{
			transform = base.transform;
		}
		float maxGeometryDistance2 = m_Master.maxGeometryDistance;
		object obj3 = default(object);
		transform.localScale = (Vector3)(&obj3);
		Transform transform2 = base.transform;
		Quaternion beamInternalLocalRotation = m_Master.beamInternalLocalRotation;
		transform2.localRotation = (Quaternion)(&num4);
		Transform transform3 = base.transform;
		Vector3 localScale = transform3.localScale;
		if (m_Master.hasMeshSkewing)
		{
			Vector3 skewingLocalForwardDirectionNormalized = m_Master.skewingLocalForwardDirectionNormalized;
			SetMaterialProp(ShaderProperties.SD.LocalForwardDirection, (Vector4)(&num4));
			if (base._003CconeMesh_003Ek__BackingField != null)
			{
				VolumetricLightBeamSD master16 = m_Master;
				float num7 = skewingLocalForwardDirectionNormalized.x / skewingLocalForwardDirectionNormalized.z;
				object obj4 = default(object);
				float num8 = (float)obj4 / skewingLocalForwardDirectionNormalized.z;
				float num9 = num7 * master16.fallOffEnd;
				float num10 = num8 * master16.fallOffEnd;
				float num11 = num9 / localScale.x;
				object obj5 = default(object);
				float num12 = num10 / (float)obj5;
				if (!(num11 > 0f))
				{
				}
				if (!(num12 > 0f))
				{
				}
				base._003CconeMesh_003Ek__BackingField.bounds = (Bounds)(&num4);
			}
		}
		UpdateMatricesPropertiesForGPUInstancingSRP();
		MaterialChangeStop();
	}

	private unsafe void UpdateMatricesPropertiesForGPUInstancingSRP()
	{
		//IL_0076: Expected O, but got I4
		//IL_011e: Expected O, but got Ref
		//IL_014e: Expected O, but got Ref
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
		if ((instance.m_RenderPipeline == RenderPipeline.BuiltIn || instance.m_RenderingMode != RenderingMode.MultiPass) && instance.m_RenderingMode == RenderingMode.GPUInstancing)
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
		Camera camera;
		if (!SRPHelper.IsUsingCustomRenderPipeline())
		{
			Camera current = Camera.current;
			camera = current;
		}
		else
		{
			camera = m_CurrentCameraRenderingSRP;
		}
		if ((bool)m_Master && (bool)camera && camera.enabled)
		{
			UpdateCameraRelatedProperties(camera);
			m_Master._INTERNAL_OnWillCameraRenderThisBeam(camera);
		}
	}

	private void OnWillCameraRenderThisBeam(Camera cam)
	{
		if ((bool)m_Master && (bool)cam && cam.enabled)
		{
			UpdateCameraRelatedProperties(cam);
			m_Master._INTERNAL_OnWillCameraRenderThisBeam(cam);
		}
	}

	private unsafe void UpdateCameraRelatedProperties(Camera cam)
	{
		//IL_0008: Expected O, but got Ref
		//IL_00b1: Expected O, but got Ref
		//IL_0117: Expected O, but got Ref
		//IL_02b8: Expected O, but got Ref
		//IL_018e: Expected I, but got O
		//IL_01ae: Expected F4, but got I
		//IL_01df: Expected O, but got Ref
		//IL_0370: Expected O, but got Ref
		//IL_0260: Invalid comparison between F4 and I4
		//IL_0320: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if ((bool)cam && (bool)m_Master)
		{
			MaterialChangeStart();
			Transform transform = m_Master.transform;
			Transform transform2 = cam.transform;
			Vector3 position = transform2.position;
			Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
			_ = position.x;
			_ = position.z;
			Vector3 vector = transform.InverseTransformPoint(position2);
			Transform transform3 = base.transform;
			Transform transform4 = cam.transform;
			Vector3 forward = transform4.forward;
			Vector3 direction = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
			_ = forward.x;
			_ = forward.z;
			Vector3 vector2 = transform3.InverseTransformDirection(direction);
			_ = vector2.x;
			_ = vector2.z;
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			if (vector2.x > 1E-05f)
			{
				float num = vector2.z / vector2.x;
				float num2 = num;
			}
			else
			{
				nint num3 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v466 @ rax_v40 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v467 @ rcx_v37 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				float num2 = 0f;
				_ = Vector3.zeroVector;
			}
			if (cam.orthographic)
			{
				float num5 = -1f;
			}
			else
			{
				_ = vector.x;
				Vector3 posOS = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				_ = vector.z;
				float insideBeamFactorFromObjectSpacePos = m_Master.GetInsideBeamFactorFromObjectSpacePos(posOS);
				float num5 = insideBeamFactorFromObjectSpacePos;
			}
			Vector4 value = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-15]");
			_ = 0;
			SetMaterialProp(ShaderProperties.SD.CameraParams, value);
			UpdateMatricesPropertiesForGPUInstancingSRP();
			ColorMode usedColorMode = m_Master.usedColorMode;
			if (usedColorMode == ColorMode.Gradient)
			{
				Matrix4x4 value2 = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
				_ = m_ColorGradientMatrix;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.BeamGeometrySD)+48]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.BeamGeometrySD)+58]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.BeamGeometrySD)+68]");
				_ = 0;
				SetMaterialProp(ShaderProperties.ColorGradientMatrix, value2);
			}
			MaterialChangeStop();
			VolumetricLightBeamSD master = m_Master;
			if (master.depthBlendDistance > 0f)
			{
				DepthTextureMode depthTextureMode = cam.depthTextureMode;
				DepthTextureMode depthTextureMode2 = depthTextureMode | DepthTextureMode.Depth;
				cam.depthTextureMode = depthTextureMode2;
			}
		}
	}
}
