using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace VLB;

public class VolumetricShadowHD : MonoBehaviour
{
	private enum ProcessOcclusionSource
	{
		RenderLoop,
		OnEnable,
		EditorUpdate,
		User
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<Camera> _003C_003E9__43_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003CInstantiateOrActivateDepthCamera_003Eb__43_0(Camera cam)
		{
			GameObject gameObject = cam.gameObject;
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
	}

	public const string ClassName = "VolumetricShadowHD";

	private float m_Strength;

	private ShadowUpdateRate m_UpdateRate;

	private int m_WaitXFrames;

	private LayerMask m_LayerMask;

	private bool m_UseOcclusionCulling;

	private int m_DepthMapResolution;

	private int m_DepthMapDepth;

	private VolumetricLightBeamHD m_Master;

	private TransformUtils.Packed m_TransformPacked;

	private int m_LastFrameRendered;

	private Camera m_DepthCamera;

	private bool m_NeedToUpdateOcclusionNextFrame;

	private static bool _INTERNAL_ApplyRandomFrameOffset = true;

	public float strength
	{
		get
		{
			return m_Strength;
		}
		set
		{
			bool flag = m_Strength == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018038A86Ch\"");
			if (!flag)
			{
				m_Strength = value;
				SetDirty();
			}
		}
	}

	public ShadowUpdateRate updateRate
	{
		get
		{
			return m_UpdateRate;
		}
		set
		{
			m_UpdateRate = value;
		}
	}

	public int waitXFrames
	{
		get
		{
			return m_WaitXFrames;
		}
		set
		{
			m_WaitXFrames = value;
		}
	}

	public LayerMask layerMask
	{
		get
		{
			return m_LayerMask;
		}
		set
		{
			m_LayerMask = value;
			UpdateDepthCameraProperties();
		}
	}

	public bool useOcclusionCulling
	{
		get
		{
			return m_UseOcclusionCulling;
		}
		set
		{
			m_UseOcclusionCulling = value;
			UpdateDepthCameraProperties();
		}
	}

	public int depthMapResolution
	{
		get
		{
			return m_DepthMapResolution;
		}
		set
		{
			if (m_DepthCamera != null && Application.isPlaying)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C73]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				string arg = base.name;
				string format = $"Can't change {arg} Shadow.depthMapResolution property at runtime after DepthCamera initialization";
				object[] args = Array.Empty<object>();
				Debug.LogErrorFormat(format, args);
			}
			m_DepthMapResolution = value;
		}
	}

	public int depthMapDepth
	{
		get
		{
			return m_DepthMapDepth;
		}
		set
		{
			if (m_DepthCamera != null && Application.isPlaying)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C74]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				string arg = base.name;
				string format = $"Can't change {arg} Shadow.depthMapDepth property at runtime after DepthCamera initialization";
				object[] args = Array.Empty<object>();
				Debug.LogErrorFormat(format, args);
			}
			m_DepthMapDepth = value;
		}
	}

	public int _INTERNAL_LastFrameRendered => m_LastFrameRendered;

	public void ProcessOcclusionManually()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 2 Invalid \"Jump target not found in method: 0x180389FB0\"");
	}

	public void UpdateDepthCameraProperties()
	{
		if ((bool)m_DepthCamera)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			int cullingMask = default(int);
			m_DepthCamera.cullingMask = cullingMask;
			m_DepthCamera.useOcclusionCulling = m_UseOcclusionCulling;
		}
	}

	private void ProcessOcclusion(ProcessOcclusionSource source)
	{
		//IL_0182: Expected O, but got I4
		Config instance = Config.GetInstance(true);
		if (!instance.featureEnabledShadow)
		{
			return;
		}
		int frameCount = Time.frameCount;
		if (m_LastFrameRendered != frameCount || !Application.isPlaying || source != ProcessOcclusionSource.OnEnable)
		{
			if (!SRPHelper.IsUsingCustomRenderPipeline())
			{
				UpdateDepthCameraPropertiesAccordingToBeam();
				m_DepthCamera.Render();
			}
			else
			{
				m_NeedToUpdateOcclusionNextFrame = true;
			}
			SetDirty();
			object obj = m_UpdateRate & ShadowUpdateRate.OnBeamMove;
			if (obj != null)
			{
				Transform self = base.transform;
				m_TransformPacked = (TransformUtils.Packed)TransformUtils.GetWorldPacked(self).position;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rax_v21 (VLB.TransformUtils+Packed)+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rax_v21 (VLB.TransformUtils+Packed)+20]");
				_ = 0;
			}
			int frameCount2 = Time.frameCount;
			m_LastFrameRendered = frameCount2;
			if (m_LastFrameRendered < 0 && _INTERNAL_ApplyRandomFrameOffset)
			{
				int num = UnityEngine.Random.Range(0, m_WaitXFrames);
				int lastFrameRendered = num + m_LastFrameRendered;
				m_LastFrameRendered = lastFrameRendered;
			}
		}
	}

	public unsafe static void ApplyMaterialProperties(VolumetricShadowHD instance, BeamGeometryHD geom)
	{
		//IL_013f: Expected I, but got O
		//IL_0168: Expected F4, but got I
		//IL_00da: Expected O, but got F4
		//IL_0176: Invalid comparison between F4 and I4
		//IL_01b4: Expected O, but got Ref
		if ((bool)instance && instance.enabled)
		{
			RenderTexture targetTexture = instance.m_DepthCamera.targetTexture;
			geom.SetMaterialProp(ShaderProperties.HD.ShadowDepthTexture, targetTexture);
			VolumetricLightBeamHD master = instance.m_Master;
			float num;
			Vector3 vector;
			if (master.m_Scalable)
			{
				Vector3 lossyScale = master.GetLossyScale();
				num = lossyScale.z;
				vector = (Vector3)lossyScale.x;
			}
			else
			{
				nint num2 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ rax_v27 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num3 = 0;
				vector = Vector3.oneVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v388 @ rcx_v20 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
				num = 0f;
			}
			if ((nint)vector >= 0)
			{
			}
			if (!(num < 0f))
			{
			}
			object obj = default(object);
			if ((nint)obj >= 0 || instance.m_DepthCamera.orthographic)
			{
				object obj2 = default(object);
				geom.SetMaterialProp(ShaderProperties.HD.ShadowProps, (Vector4)(&obj2));
			}
		}
		else
		{
			geom.SetMaterialProp(ShaderProperties.HD.ShadowDepthTexture, BeamGeometryHD.InvalidTexture.NoDepth);
		}
	}

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricLightBeamHD master = default(VolumetricLightBeamHD);
		m_Master = master;
	}

	private void OnEnable()
	{
		//IL_013c: Expected O, but got I4
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Expected I4, but got Unknown
		//IL_024a: Expected O, but got I4
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected I4, but got Unknown
		//IL_00ed: Expected O, but got I4
		//IL_0107: Expected O, but got I4
		int num = m_WaitXFrames;
		if (m_WaitXFrames >= 1)
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
		m_WaitXFrames = num;
		object obj = m_DepthMapResolution - 1;
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
		m_DepthMapResolution = num2;
		int num3 = m_DepthMapDepth;
		if (m_DepthMapDepth >= 16)
		{
			if (num3 > 32)
			{
				num3 = 32;
			}
		}
		else
		{
			num3 = 16;
		}
		int num4 = num3 >> 31;
		int num5 = num4 & 7;
		object obj12 = num3 + num5;
		int num6 = obj12 & -8;
		m_DepthMapDepth = num6;
		InstantiateOrActivateDepthCamera();
		if (base.enabled)
		{
			object obj13 = m_UpdateRate & ShadowUpdateRate.Never;
			bool flag = obj13 == null;
			object obj14 = !flag;
			if (obj14 == null)
			{
				ProcessOcclusion(ProcessOcclusionSource.OnEnable);
			}
		}
	}

	private void OnDisable()
	{
		if ((bool)m_DepthCamera)
		{
			GameObject gameObject = m_DepthCamera.gameObject;
			gameObject.SetActive(value: false);
		}
		SetDirty();
	}

	private void OnDestroy()
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

	private void ProcessOcclusionInternal()
	{
		UpdateDepthCameraPropertiesAccordingToBeam();
		m_DepthCamera.Render();
	}

	private void OnBeamEnabled()
	{
		//IL_0033: Expected O, but got I4
		//IL_004d: Expected O, but got I4
		if (base.enabled)
		{
			object obj = m_UpdateRate & ShadowUpdateRate.Never;
			bool flag = obj == null;
			object obj2 = !flag;
			if (obj2 == null)
			{
				ProcessOcclusion(ProcessOcclusionSource.OnEnable);
			}
		}
	}

	public unsafe void OnWillCameraRenderThisBeam(Camera cam, BeamGeometryHD beamGeom)
	{
		//IL_00ad: Expected O, but got I4
		//IL_011e: Expected O, but got I4
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_0155: Expected O, but got I4
		if (!base.enabled || !(cam != null) || !cam.enabled)
		{
			return;
		}
		int frameCount = Time.frameCount;
		if (frameCount == m_LastFrameRendered || m_UpdateRate == ShadowUpdateRate.Never)
		{
			return;
		}
		object obj = m_UpdateRate & ShadowUpdateRate.OnBeamMove;
		if (obj != null)
		{
			Transform transf = base.transform;
			TransformUtils.Packed packed = (TransformUtils.Packed)(this + 72);
			if (!((TransformUtils.Packed*)packed)->IsSame(transf))
			{
				goto IL_0171;
			}
		}
		object obj2 = m_UpdateRate & ShadowUpdateRate.EveryXFrames;
		if (obj2 != null)
		{
			int frameCount2 = Time.frameCount;
			object obj3 = m_LastFrameRendered + m_WaitXFrames;
			if (frameCount2 >= (nint)obj3)
			{
				goto IL_0171;
			}
			return;
		}
		return;
		IL_0171:
		ProcessOcclusion(ProcessOcclusionSource.RenderLoop);
	}

	private void LateUpdate()
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
		//IL_0110: Expected O, but got F4
		float coneApexOffsetZ = m_Master.GetConeApexOffsetZ(counterApplyScaleForUnscalableBeam: true);
		VolumetricLightBeamHD master = m_Master;
		float num = Utils.ComputeConeRadiusEnd(master.m_FallOffEnd, master.m_SpotAngle);
		if (m_Master.GetDimensions() == Dimensions.Dim3D)
		{
		}
		Vector3 lossyScale = m_Master.GetLossyScale();
		Quaternion beamInternalLocalRotation = m_Master.beamInternalLocalRotation;
		float coneRadiusEnd = default(float);
		Vector3 beamLocalForward = default(Vector3);
		Vector3 lossyScale2 = default(Vector3);
		bool isScalable = default(bool);
		object obj = default(object);
		Utils.SetupDepthCamera(m_DepthCamera, coneApexOffsetZ, master.m_FallOffEnd, master.m_ConeRadiusStart, coneRadiusEnd, beamLocalForward, lossyScale2, isScalable, (Quaternion)num, (byte)(&obj) != 0);
	}

	private void InstantiateOrActivateDepthCamera()
	{
		if (m_DepthCamera == null)
		{
			GameObject self = base.gameObject;
			Action<Camera> lambda = _003C_003Ec._003C_003E9__43_0;
			if (_003C_003Ec._003C_003E9__43_0 == null)
			{
				lambda = (_003C_003Ec._003C_003E9__43_0 = delegate(Camera cam)
				{
					GameObject obj = cam.gameObject;
					UnityEngine.Object.DestroyImmediate(obj);
				});
			}
			Utils.ForeachComponentsInDirectChildrenOnly(self, lambda, includeInactive: true);
			Camera depthCamera = Utils.NewWithComponent<Camera>("Depth Camera");
			m_DepthCamera = depthCamera;
			if (!m_DepthCamera || !m_Master)
			{
				return;
			}
			m_DepthCamera.enabled = false;
			UpdateDepthCameraProperties();
			m_DepthCamera.clearFlags = CameraClearFlags.Depth;
			m_DepthCamera.depthTextureMode = DepthTextureMode.Depth;
			m_DepthCamera.renderingPath = RenderingPath.Forward;
			GameObject gameObject = m_DepthCamera.gameObject;
			HideFlags proceduralObjectsHideFlags = Consts.Internal.ProceduralObjectsHideFlags;
			gameObject.hideFlags = proceduralObjectsHideFlags;
			Transform transform = m_DepthCamera.transform;
			Transform parent = base.transform;
			transform.SetParent(parent, worldPositionStays: false);
			Config instance = Config.GetInstance(true);
			if (instance.urpDepthCameraScriptableRendererIndex >= 0)
			{
				UniversalAdditionalCameraData universalAdditionalCameraData = CameraExtensions.GetUniversalAdditionalCameraData(m_DepthCamera);
				if ((bool)universalAdditionalCameraData)
				{
					universalAdditionalCameraData.m_RendererIndex = instance.urpDepthCameraScriptableRendererIndex;
				}
			}
			RenderTextureFormat format = default(RenderTextureFormat);
			RenderTexture targetTexture = new RenderTexture(m_DepthMapResolution, m_DepthMapResolution, m_DepthMapDepth, format);
			m_DepthCamera.targetTexture = targetTexture;
			UpdateDepthCameraPropertiesAccordingToBeam();
		}
		else
		{
			GameObject gameObject2 = m_DepthCamera.gameObject;
			gameObject2.SetActive(value: true);
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

	private void OnValidateProperties()
	{
		//IL_00f5: Expected O, but got I4
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected I4, but got Unknown
		//IL_0203: Expected O, but got I4
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected I4, but got Unknown
		int num = m_WaitXFrames;
		if (m_WaitXFrames >= 1)
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
		m_WaitXFrames = num;
		object obj = m_DepthMapResolution - 1;
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
		m_DepthMapResolution = num2;
		int num3 = m_DepthMapDepth;
		if (m_DepthMapDepth >= 16)
		{
			if (num3 > 32)
			{
				m_DepthMapDepth = 32;
				return;
			}
		}
		else
		{
			num3 = 16;
		}
		int num4 = num3 >> 31;
		int num5 = num4 & 7;
		object obj12 = num3 + num5;
		int num6 = obj12 & -8;
		m_DepthMapDepth = num6;
	}

	private void SetDirty()
	{
		if ((bool)m_Master)
		{
			m_Master.SetPropertyDirty(DirtyProps.ShadowProps);
		}
	}

	public static bool INTERNAL_GetApplyRandomFrameOffset()
	{
		return _INTERNAL_ApplyRandomFrameOffset;
	}

	public static void INTERNAL_EnableApplyRandomFrameOffset()
	{
		_INTERNAL_ApplyRandomFrameOffset = true;
	}

	public void INTERNAL_DisableApplyRandomFrameOffset()
	{
		//IL_0023: Expected I4, but got I8
		_INTERNAL_ApplyRandomFrameOffset = false;
		m_LastFrameRendered = -2147483648;
	}

	public VolumetricShadowHD()
	{
		//IL_0066: Expected I4, but got I8
		m_Strength = 1f;
		m_UpdateRate = ShadowUpdateRate.EveryXFrames;
		m_WaitXFrames = 3;
		m_LayerMask = Consts.Shadow.LayerMaskDefault;
		m_UseOcclusionCulling = true;
		m_DepthMapResolution = 128;
		m_DepthMapDepth = 16;
		m_LastFrameRendered = -2147483648;
		base._002Ector();
	}
}
