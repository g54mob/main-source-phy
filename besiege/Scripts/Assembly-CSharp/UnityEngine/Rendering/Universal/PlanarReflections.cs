using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.Universal
{
	public class PlanarReflections : MonoBehaviour
	{
		[Serializable]
		public enum ResolutionMulltiplier
		{
			None = 0,
			Eighth = 1,
			Quarter = 2,
			Third = 3,
			Half = 4,
			Full = 5
		}

		[Serializable]
		public class PlanarReflectionSettings
		{
			public ResolutionMulltiplier m_Resolution;

			public float m_ClipPlaneOffset = 0.07f;

			public LayerMask m_ReflectLayers = -1;

			public bool m_Shadows;
		}

		private class PlanarReflectionSettingData
		{
			private readonly bool _fog;

			private readonly int _maxLod;

			private readonly float _lodBias;

			public PlanarReflectionSettingData()
			{
				_fog = RenderSettings.fog;
				_maxLod = QualitySettings.maximumLODLevel;
				_lodBias = QualitySettings.lodBias;
			}

			public void Set()
			{
				GL.invertCulling = true;
				RenderSettings.fog = false;
				QualitySettings.maximumLODLevel = 1;
				prevShadowDistance = QualitySettings.shadowDistance;
				QualitySettings.shadowDistance = 0f;
			}

			public void Restore()
			{
				GL.invertCulling = false;
				RenderSettings.fog = _fog;
				QualitySettings.maximumLODLevel = _maxLod;
				QualitySettings.lodBias = _lodBias;
				QualitySettings.shadowDistance = prevShadowDistance;
			}
		}

		private static PlanarReflections Instance;

		[SerializeField]
		public PlanarReflectionSettings m_settings = new PlanarReflectionSettings();

		public GameObject target;

		[FormerlySerializedAs("camOffset")]
		public float m_planeOffset;

		[SerializeField]
		private WaterFogController waterFogController;

		[SerializeField]
		public Camera realCamera;

		[SerializeField]
		[Header("Reflection Camera Settings")]
		public Color skyColor = Color.blue;

		[SerializeField]
		public Vector2 clipPlane = new Vector2(0.3f, 750f);

		[SerializeField]
		public RenderingPath renderingPath = RenderingPath.Forward;

		private static Camera _reflectionCamera;

		private RenderTexture _reflectionTexture;

		private int _planarReflectionTextureId;

		private Vector2 _oldReflectionTextureSize;

		private static float prevShadowDistance;

		private bool setupDone;

		private bool cameraSetupDone;

		private void OnDisable()
		{
			Cleanup();
		}

		private void OnDestroy()
		{
			Cleanup();
		}

		public void Awake()
		{
			if (!setupDone)
			{
				setupDone = true;
				Instance = this;
				if (_reflectionCamera == null)
				{
					_reflectionCamera = CreateMirrorObjects();
				}
				_planarReflectionTextureId = Shader.PropertyToID("_PlanarReflectionTexture");
				if (target == null)
				{
					target = Object.FindObjectOfType<WaterLod>().gameObject;
				}
				if (waterFogController == null)
				{
					waterFogController = Object.FindObjectOfType<WaterFogController>();
				}
				UpdateReflectionSettings();
			}
		}

		private void Cleanup()
		{
			cameraSetupDone = false;
			if ((bool)_reflectionCamera)
			{
				_reflectionCamera.targetTexture = null;
				SafeDestroy(_reflectionCamera.gameObject);
				_reflectionCamera = null;
			}
			if ((bool)_reflectionTexture)
			{
				_reflectionTexture.DiscardContents();
				RenderTexture.ReleaseTemporary(_reflectionTexture);
				_reflectionTexture = null;
			}
		}

		private static void SafeDestroy(Object obj)
		{
			if (Application.isEditor)
			{
				Object.DestroyImmediate(obj);
			}
			else
			{
				Object.Destroy(obj);
			}
		}

		private void UpdateCamera(Camera src, Camera dest)
		{
			if (!(dest == null) && !cameraSetupDone)
			{
				dest.CopyFrom(src);
				dest.backgroundColor = skyColor;
				dest.clearFlags = CameraClearFlags.Color;
				dest.renderingPath = renderingPath;
				dest.useOcclusionCulling = false;
			}
		}

		private void UpdateCameraClipPlane(Camera dest)
		{
			if (!(dest == null) && !cameraSetupDone)
			{
				dest.nearClipPlane = clipPlane.x;
				dest.farClipPlane = clipPlane.y;
				cameraSetupDone = true;
			}
		}

		private void UpdateReflectionCamera(Camera realCamera)
		{
			if (_reflectionCamera == null)
			{
				_reflectionCamera = CreateMirrorObjects();
			}
			Vector3 vector = Vector3.zero;
			Vector3 up = Vector3.up;
			if (target != null)
			{
				vector = target.transform.position + Vector3.up * m_planeOffset;
				up = target.transform.up;
			}
			float num = realCamera.transform.position.y - m_planeOffset;
			if (num < vector.y)
			{
				vector.y = num;
			}
			UpdateCamera(realCamera, _reflectionCamera);
			float w = 0f - Vector3.Dot(up, vector) - m_settings.m_ClipPlaneOffset;
			Vector4 plane = new Vector4(up.x, up.y, up.z, w);
			Matrix4x4 reflectionMat = Matrix4x4.identity;
			reflectionMat *= Matrix4x4.Scale(new Vector3(1f, -1f, 1f));
			CalculateReflectionMatrix(ref reflectionMat, plane);
			Vector3 pos = realCamera.transform.position - new Vector3(0f, vector.y * 2f, 0f);
			Vector3 position = ReflectPosition(pos);
			_reflectionCamera.transform.forward = new Vector3(realCamera.transform.forward.x, 0f - realCamera.transform.forward.y, realCamera.transform.forward.z);
			_reflectionCamera.worldToCameraMatrix = realCamera.worldToCameraMatrix * reflectionMat;
			Vector4 vector2 = CameraSpacePlane(_reflectionCamera, vector - Vector3.up * 0.1f, up, 1f);
			Matrix4x4 projectionMatrix = realCamera.CalculateObliqueMatrix(vector2);
			_reflectionCamera.projectionMatrix = projectionMatrix;
			_reflectionCamera.transform.position = position;
			_reflectionCamera.cullingMask = m_settings.m_ReflectLayers;
			UpdateCameraClipPlane(_reflectionCamera);
		}

		private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
		{
			reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
			reflectionMat.m01 = -2f * plane[0] * plane[1];
			reflectionMat.m02 = -2f * plane[0] * plane[2];
			reflectionMat.m03 = -2f * plane[3] * plane[0];
			reflectionMat.m10 = -2f * plane[1] * plane[0];
			reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
			reflectionMat.m12 = -2f * plane[1] * plane[2];
			reflectionMat.m13 = -2f * plane[3] * plane[1];
			reflectionMat.m20 = -2f * plane[2] * plane[0];
			reflectionMat.m21 = -2f * plane[2] * plane[1];
			reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
			reflectionMat.m23 = -2f * plane[3] * plane[2];
			reflectionMat.m30 = 0f;
			reflectionMat.m31 = 0f;
			reflectionMat.m32 = 0f;
			reflectionMat.m33 = 1f;
		}

		private static Vector3 ReflectPosition(Vector3 pos)
		{
			return new Vector3(pos.x, 0f - pos.y, pos.z);
		}

		private float GetScaleValue()
		{
			switch (m_settings.m_Resolution)
			{
			case ResolutionMulltiplier.Full:
				return 1f;
			case ResolutionMulltiplier.Half:
				return 0.5f;
			case ResolutionMulltiplier.Third:
				return 0.33f;
			case ResolutionMulltiplier.Quarter:
				return 0.25f;
			case ResolutionMulltiplier.Eighth:
				return 0.125f;
			case ResolutionMulltiplier.None:
				return 0f;
			default:
				return 0.5f;
			}
		}

		private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
		{
			Vector3 v = pos + normal * m_settings.m_ClipPlaneOffset;
			Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
			Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
			Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
			return new Vector4(rhs.x, rhs.y, rhs.z, 0f - Vector3.Dot(lhs, rhs));
		}

		private Camera CreateMirrorObjects()
		{
			GameObject gameObject = new GameObject("Planar Reflections", typeof(Camera));
			Transform transform = base.transform;
			Camera component = gameObject.GetComponent<Camera>();
			component.transform.position = transform.position;
			component.transform.rotation = transform.rotation;
			component.depth = -10f;
			component.enabled = false;
			UpdateCamera(realCamera, component);
			return component;
		}

		private void PlanarReflectionTexture(Camera cam)
		{
			if (_reflectionTexture == null)
			{
				Vector2 vector = ReflectionResolution(cam, 1f);
				if (SystemInfo.graphicsShaderLevel > 30)
				{
					_reflectionTexture = RenderTexture.GetTemporary((int)vector.x, (int)vector.y, 16, RenderTextureFormat.RGB111110Float);
				}
				else
				{
					_reflectionTexture = RenderTexture.GetTemporary((int)vector.x, (int)vector.y, 16, RenderTextureFormat.Default);
				}
			}
			_reflectionCamera.targetTexture = _reflectionTexture;
		}

		private Vector2 ReflectionResolution(Camera cam, float scale)
		{
			int num = (int)((float)cam.pixelWidth * scale * GetScaleValue());
			int num2 = (int)((float)cam.pixelHeight * scale * GetScaleValue());
			return new Vector2(num, num2);
		}

		private void LateUpdate()
		{
			if ((bool)realCamera && SystemInfo.supportsRenderTextures)
			{
				ExecutePlanarReflections(realCamera);
			}
		}

		private void ExecutePlanarReflections(Camera camera)
		{
			if (camera.cameraType != CameraType.Preview && !SingleInstanceFindOnly<MouseOrbit>.Instance.IsOrthographic && WaterFogController.overWater && m_settings.m_Resolution != ResolutionMulltiplier.None)
			{
				UpdateReflectionCamera(camera);
				PlanarReflectionTexture(camera);
				PlanarReflectionSettingData planarReflectionSettingData = new PlanarReflectionSettingData();
				planarReflectionSettingData.Set();
				Shader.EnableKeyword("_PLANAR_REFLECTION_CAMERA");
				_reflectionCamera.Render();
				planarReflectionSettingData.Restore();
				Shader.SetGlobalTexture(_planarReflectionTextureId, _reflectionTexture);
				Shader.DisableKeyword("_PLANAR_REFLECTION_CAMERA");
			}
		}

		public static void UpdateReflectionQuality()
		{
			if (Instance != null)
			{
				Instance.UpdateReflectionSettings();
			}
		}

		private void UpdateReflectionSettings()
		{
			ResolutionMulltiplier reflectionQuality = (ResolutionMulltiplier)OptionsMaster.BesiegeConfig.ReflectionQuality;
			if (reflectionQuality == ResolutionMulltiplier.None)
			{
				Shader.DisableKeyword("Reflection_Cam");
				Shader.SetGlobalTexture(Shader.PropertyToID("_PlanarReflectionTexture"), null);
			}
			if (reflectionQuality == m_settings.m_Resolution)
			{
				return;
			}
			bool flag = reflectionQuality != ResolutionMulltiplier.None;
			if (base.enabled != flag)
			{
				base.enabled = flag;
			}
			bool flag2 = reflectionQuality == ResolutionMulltiplier.Full;
			if ((float)Screen.height > 720f)
			{
				flag2 = flag2 || reflectionQuality == ResolutionMulltiplier.Half;
				if ((float)Screen.height > 1079f)
				{
					flag2 = flag2 || reflectionQuality == ResolutionMulltiplier.Third;
					if ((float)Screen.height > 1279f)
					{
						flag2 = flag2 || reflectionQuality == ResolutionMulltiplier.Quarter;
					}
				}
			}
			if (waterFogController != null)
			{
				waterFogController.SetReflectionBlur(flag2);
				waterFogController.SetPlaneReflection(WaterFogController.overWater);
			}
			m_settings.m_Resolution = reflectionQuality;
			RenderTexture.ReleaseTemporary(_reflectionTexture);
			_reflectionTexture = null;
		}
	}
}
