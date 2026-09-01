using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Simulation Steps/Terrain Height Renderer")]
	public class TerrainHeightRenderer : SimulationOutput
	{
		[SerializeField]
		private LayerMask m_renderedLayers;

		public Shader depthShader;

		public float m_cameraNearPlane = 0.3f;

		private GameObject m_cameraObject;

		private Camera m_depthCamera;

		private Texture m_clearTexture;

		private Material m_depthClearMaterial;

		public Shader m_depthClearShader;

		[SerializeField]
		private float m_cameraHeightOffset = -1f;

		private float m_latestFarClipPlane;

		private float m_latestNearClipPlane;

		private float m_latestCameraHeightOffset = -1f;

		public LayerMask renderedLayers
		{
			get
			{
				return m_renderedLayers;
			}
			set
			{
				m_renderedLayers = value;
				if (m_depthCamera != null)
				{
					m_depthCamera.cullingMask = m_renderedLayers;
				}
			}
		}

		public Shader depthClearShader
		{
			get
			{
				return m_depthClearShader;
			}
			set
			{
				m_depthClearShader = value;
				if (m_depthClearMaterial != null)
				{
					m_depthClearMaterial.shader = value;
				}
			}
		}

		public float FarClipPlane
		{
			get
			{
				return m_latestFarClipPlane;
			}
		}

		public float NearClipPlane
		{
			get
			{
				return m_latestNearClipPlane;
			}
		}

		public float CameraHeightOffset
		{
			get
			{
				return m_latestCameraHeightOffset;
			}
		}

		protected void OnDestroy()
		{
			Object.DestroyImmediate(m_depthClearMaterial);
		}

		protected void OnValidate()
		{
			renderedLayers = m_renderedLayers;
		}

		public override void LoadData()
		{
			if (m_cameraObject == null)
			{
				m_cameraObject = CreateDepthCamera();
				m_depthCamera = m_cameraObject.GetComponent<Camera>();
				PositionDepthCamera();
			}
			if (m_clearTexture == null)
			{
				if (simTextureManager == null)
				{
					FindTextureManager();
				}
				m_clearTexture = simTextureManager.GetClearTexture();
			}
			if (m_depthClearMaterial == null)
			{
				m_depthClearMaterial = new Material(m_depthClearShader);
			}
			SetupDepthClearMaterial();
			RunStep();
		}

		private void SetupDepthClearMaterial()
		{
			m_depthClearMaterial.shader = m_depthClearShader;
			m_depthClearMaterial.SetFloat("_Depth", 0f);
		}

		public override void RunStep()
		{
			if (simTextureManager == null)
			{
				FindTextureManager();
			}
			RenderTexture renderTexture = simTextureManager.CreateOutputTexture("StreamBed Depth", true);
			m_depthCamera.targetTexture = renderTexture;
			m_depthCamera.farClipPlane = simulationSize.localSize.y - m_cameraHeightOffset;
			m_depthCamera.nearClipPlane = m_cameraNearPlane;
			Graphics.Blit(m_clearTexture, renderTexture, m_depthClearMaterial);
			m_depthCamera.RenderWithShader(depthShader, "RenderType");
			UpdateOutput(renderTexture);
			m_latestFarClipPlane = m_depthCamera.farClipPlane;
			m_latestNearClipPlane = m_depthCamera.nearClipPlane;
			m_latestCameraHeightOffset = m_cameraHeightOffset;
		}

		private GameObject CreateDepthCamera()
		{
			GameObject gameObject = new GameObject("Surface Bed Depth Camera", typeof(Camera));
			gameObject.transform.parent = base.transform;
			gameObject.transform.Rotate(Vector3.right, -90f);
			gameObject.hideFlags = HideFlags.DontSave;
			gameObject.SetActive(false);
			Camera component = gameObject.GetComponent<Camera>();
			component.renderingPath = RenderingPath.Forward;
			component.orthographic = true;
			component.cullingMask = renderedLayers;
			component.backgroundColor = Color.white;
			component.clearFlags = CameraClearFlags.Nothing;
			component.useOcclusionCulling = false;
			component.SetReplacementShader(depthShader, "RenderType");
			component.orthographicSize = simulationSize.localExtends.z;
			component.aspect = 1f;
			return gameObject;
		}

		private void DestroyDepthCamera()
		{
			if (m_cameraObject != null)
			{
				m_depthCamera = null;
				Object.DestroyImmediate(m_cameraObject);
			}
		}

		private void PositionDepthCamera()
		{
			Transform transform = m_cameraObject.transform;
			transform.parent = base.transform;
			Vector3 center = simulationSize.center;
			center.y += m_cameraHeightOffset - simulationSize.localExtends.y;
			transform.position = center;
		}
	}
}
