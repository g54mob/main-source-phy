using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class DynamicOcclusionDepthBuffer : DynamicOcclusionAbstractBase
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<Camera> _003C_003E9__15_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003CInstantiateOrActivateDepthCamera_003Eb__15_0(Camera cam)
		{
			GameObject gameObject = cam.gameObject;
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
	}

	public new const string ClassName = "DynamicOcclusionDepthBuffer";

	public LayerMask layerMask;

	public bool useOcclusionCulling;

	public int depthMapResolution;

	public float fadeDistanceToSurface;

	private Camera m_DepthCamera;

	private bool m_NeedToUpdateOcclusionNextFrame;

	protected override string GetShaderKeyword()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39D17]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		return "VLB_OCCLUSION_DEPTH_TEXTURE";
	}

	protected override MaterialManager.SD.DynamicOcclusion GetDynamicOcclusionMode()
	{
		return MaterialManager.SD.DynamicOcclusion.DepthTexture;
	}

	private void ProcessOcclusionInternal()
	{
		UpdateDepthCameraPropertiesAccordingToBeam();
		m_DepthCamera.Render();
	}

	protected override bool OnProcessOcclusion(ProcessOcclusionSource source)
	{
		//IL_006c: Expected I4, but got O
		if (!SRPHelper.IsUsingCustomRenderPipeline())
		{
			UpdateDepthCameraPropertiesAccordingToBeam();
			if ((object)m_DepthCamera != null)
			{
				m_DepthCamera.Render();
				return true;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		m_NeedToUpdateOcclusionNextFrame = true;
		return true;
	}

	private void Update()
	{
		if (m_NeedToUpdateOcclusionNextFrame && (bool)m_Master && (bool)m_DepthCamera)
		{
			int frameCount = Time.frameCount;
			if (frameCount > 1)
			{
				UpdateDepthCameraPropertiesAccordingToBeam();
				m_DepthCamera.Render();
				m_NeedToUpdateOcclusionNextFrame = false;
			}
		}
	}

	private unsafe void UpdateDepthCameraPropertiesAccordingToBeam()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_008c: Expected O, but got I
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_018c: Expected O, but got I
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		//IL_013b: Expected O, but got F4
		float coneApexOffsetZ = m_Master.coneApexOffsetZ;
		VolumetricLightBeamSD master = m_Master;
		Vector2 tiltFactor = master.tiltFactor;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = tiltFactor & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v4 (VLB.VolumetricLightBeamSD)+100]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
		{
			obj = obj2;
		}
		float maxGeometryDistance = (float)obj + master.fallOffEnd;
		Vector2 tiltFactor2 = master.tiltFactor;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj3 = tiltFactor2 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v4 (VLB.VolumetricLightBeamSD)+100]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj4 = num2 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
		{
			obj3 = obj4;
		}
		float num3 = master.spotAngle * ((float)Math.PI / 180f);
		float num4 = num3 * 0.5f;
		object obj5 = obj3 + master.fallOffEnd;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
		VolumetricLightBeamSD master2 = m_Master;
		float num5 = (float)obj5 * num4;
		if (master2.dimensions == Dimensions.Dim3D)
		{
		}
		Vector3 lossyScale = m_Master.GetLossyScale();
		bool flag = m_Master.IsScalable();
		Quaternion beamInternalLocalRotation = m_Master.beamInternalLocalRotation;
		float coneRadiusEnd = default(float);
		Vector3 beamLocalForward = default(Vector3);
		Vector3 lossyScale2 = default(Vector3);
		bool isScalable = default(bool);
		object obj6 = default(object);
		Utils.SetupDepthCamera(m_DepthCamera, coneApexOffsetZ, maxGeometryDistance, master.coneRadiusStart, coneRadiusEnd, beamLocalForward, lossyScale2, isScalable, (Quaternion)num5, (byte)(&obj6) != 0);
	}

	public bool HasLayerMaskIssues()
	{
		//IL_00db: Expected I4, but got O
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected I4, but got Unknown
		//IL_00b9: Expected O, but got I4
		Config instance = Config.Instance;
		if ((object)instance != null)
		{
			if (!instance.geometryOverrideLayer)
			{
				return false;
			}
			Config instance2 = Config.Instance;
			if ((object)instance2 != null)
			{
				int num = 1 << instance2.geometryLayerID;
				object obj = this + 112;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
				object obj2 = default(object);
				int num2 = obj2 & num;
				object obj3 = num2 - num;
				return obj3 == null;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	protected override void OnValidateProperties()
	{
		//IL_00c9: Expected O, but got I4
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected I4, but got Unknown
		//IL_019a: Invalid comparison between F4 and I4
		//IL_00aa: Expected F4, but got I4
		int num = waitXFrames;
		if (waitXFrames >= 1)
		{
			if (num > 60)
			{
				num = 60;
			}
		}
		else
		{
			num = 1;
		}
		waitXFrames = num;
		object obj = depthMapResolution - 1;
		object obj2 = obj >> 16;
		object obj3 = obj2 | obj;
		object obj4 = obj3 >> 8;
		object obj5 = obj4 | obj3;
		object obj6 = obj5 >> 4;
		object obj7 = obj6 | obj5;
		object obj8 = obj7 >> 2;
		object obj9 = obj8 | obj7;
		object obj10 = obj9 >> 1;
		object obj11 = obj10 | obj9;
		int num2 = obj11 + 1;
		if (num2 >= 8)
		{
			if (num2 > 2048)
			{
				num2 = 2048;
			}
		}
		else
		{
			num2 = 8;
		}
		depthMapResolution = num2;
		float num3 = fadeDistanceToSurface;
		if (fadeDistanceToSurface < 0f)
		{
			num3 = 0f;
		}
		fadeDistanceToSurface = num3;
	}

	private void InstantiateOrActivateDepthCamera()
	{
		if (m_DepthCamera == null)
		{
			GameObject self = base.gameObject;
			Action<Camera> lambda = _003C_003Ec._003C_003E9__15_0;
			if (_003C_003Ec._003C_003E9__15_0 == null)
			{
				lambda = (_003C_003Ec._003C_003E9__15_0 = delegate(Camera cam)
				{
					GameObject obj = cam.gameObject;
					UnityEngine.Object.DestroyImmediate(obj);
				});
			}
			Utils.ForeachComponentsInDirectChildrenOnly(self, lambda, includeInactive: true);
			Camera depthCamera = Utils.NewWithComponent<Camera>("Depth Camera");
			m_DepthCamera = depthCamera;
			if ((bool)m_DepthCamera && (bool)m_Master)
			{
				m_DepthCamera.enabled = false;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
				int cullingMask = default(int);
				m_DepthCamera.cullingMask = cullingMask;
				m_DepthCamera.clearFlags = CameraClearFlags.Depth;
				m_DepthCamera.depthTextureMode = DepthTextureMode.Depth;
				m_DepthCamera.renderingPath = RenderingPath.VertexLit;
				m_DepthCamera.useOcclusionCulling = useOcclusionCulling;
				GameObject gameObject = m_DepthCamera.gameObject;
				HideFlags proceduralObjectsHideFlags = Consts.Internal.ProceduralObjectsHideFlags;
				gameObject.hideFlags = proceduralObjectsHideFlags;
				Transform transform = m_DepthCamera.transform;
				Transform parent = base.transform;
				transform.SetParent(parent, worldPositionStays: false);
				Config instance = Config.Instance;
				instance.SetURPScriptableRendererIndexToDepthCamera(m_DepthCamera);
				RenderTextureFormat format = default(RenderTextureFormat);
				RenderTexture targetTexture = new RenderTexture(depthMapResolution, depthMapResolution, 16, format);
				m_DepthCamera.targetTexture = targetTexture;
				UpdateDepthCameraPropertiesAccordingToBeam();
			}
		}
		else
		{
			GameObject gameObject2 = m_DepthCamera.gameObject;
			gameObject2.SetActive(value: true);
		}
	}

	protected override void OnEnablePostValidate()
	{
		InstantiateOrActivateDepthCamera();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if ((bool)m_DepthCamera)
		{
			GameObject gameObject = m_DepthCamera.gameObject;
			gameObject.SetActive(value: false);
		}
	}

	protected override void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricLightBeamSD master = default(VolumetricLightBeamSD);
		m_Master = master;
		VolumetricLightBeamSD master2 = m_Master;
		MaterialManager.SD.DynamicOcclusion dynamicOcclusionMode = GetDynamicOcclusionMode();
		master2.m_INTERNAL_DynamicOcclusionMode = dynamicOcclusionMode;
	}

	protected override void OnDestroy()
	{
		VolumetricLightBeamSD master = m_Master;
		master.m_INTERNAL_DynamicOcclusionMode = MaterialManager.SD.DynamicOcclusion.Off;
		DisableOcclusion();
		if ((bool)m_DepthCamera)
		{
			RenderTexture targetTexture = m_DepthCamera.targetTexture;
			if ((bool)targetTexture)
			{
				RenderTexture targetTexture2 = m_DepthCamera.targetTexture;
				targetTexture2.Release();
				RenderTexture targetTexture3 = m_DepthCamera.targetTexture;
				UnityEngine.Object.DestroyImmediate(targetTexture3);
				m_DepthCamera.targetTexture = null;
			}
			GameObject obj = m_DepthCamera.gameObject;
			UnityEngine.Object.DestroyImmediate(obj);
			m_DepthCamera = null;
		}
	}

	private void DestroyDepthCamera()
	{
		if ((bool)m_DepthCamera)
		{
			RenderTexture targetTexture = m_DepthCamera.targetTexture;
			if ((bool)targetTexture)
			{
				RenderTexture targetTexture2 = m_DepthCamera.targetTexture;
				targetTexture2.Release();
				RenderTexture targetTexture3 = m_DepthCamera.targetTexture;
				UnityEngine.Object.DestroyImmediate(targetTexture3);
				m_DepthCamera.targetTexture = null;
			}
			GameObject obj = m_DepthCamera.gameObject;
			UnityEngine.Object.DestroyImmediate(obj);
			m_DepthCamera = null;
		}
	}

	protected unsafe override void OnModifyMaterialCallback(MaterialModifier.Interface owner)
	{
		//IL_0021: Expected I, but got O
		//IL_0059: Expected O, but got I
		//IL_0062: Expected O, but got I4
		//IL_00dc: Invalid comparison between F4 and I4
		//IL_0182: Invalid comparison between F4 and I4
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0137: Expected O, but got Ref
		RenderTexture targetTexture = m_DepthCamera.targetTexture;
		nint num = (nint)owner;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r10_v2 (Il2CppClass<VLB.MaterialModifier+Interface>)+12E]");
		if ((nint)0 >= (nint)0)
		{
			goto IL_0099;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r10_v2 (Il2CppClass<VLB.MaterialModifier+Interface>)+B0]");
		object obj = 0;
		object obj2 = 0;
		while (true)
		{
			object obj3 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ r8_v13+v259 @ rax_v31*8]");
			if (0 != (nint)typeof(MaterialModifier.Interface))
			{
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r10_v2 (Il2CppClass<VLB.MaterialModifier+Interface>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_0099;
			}
			break;
		}
		goto IL_00a8;
		IL_00a8:
		owner.SetMaterialProp(ShaderProperties.SD.DynamicOcclusionDepthTexture, targetTexture);
		Vector3 lossyScale = m_Master.GetLossyScale();
		if (!(lossyScale.x < 0f))
		{
		}
		if (!(lossyScale.z < 0f))
		{
		}
		object obj5 = default(object);
		if ((nint)obj5 >= 0 || !m_DepthCamera.orthographic)
		{
		}
		float num2 = default(float);
		owner.SetMaterialProp(ShaderProperties.SD.DynamicOcclusionDepthProps, (Vector4)(&num2));
		return;
		IL_0099:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_00a8;
	}

	public DynamicOcclusionDepthBuffer()
	{
		//IL_002a: Expected I4, but got I8
		layerMask = Consts.DynOcclusion.LayerMaskDefault;
		useOcclusionCulling = true;
		depthMapResolution = 128;
		updateRate = DynamicOcclusionUpdateRate.EveryXFrames;
		waitXFrames = 3;
		base.m_LastFrameRendered = -2147483648;
		((MonoBehaviour)this)._002Ector();
	}
}
