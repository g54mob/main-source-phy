using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-56)]
	[RequireComponent(typeof(RuntimeWindow))]
	public class RTEGraphicsLayer : MonoBehaviour, IRTEGraphicsLayer
	{
		private Camera m_camera;

		private RenderTextureCamera m_renderTextureCamera;

		private IRTEGraphics m_graphics;

		private IRTECamera m_graphicsCamera;

		private RuntimeCameraWindow m_window;

		[SerializeField]
		private RectTransform m_output;

		public IRTECamera Camera => m_graphicsCamera;

		public RuntimeCameraWindow Window => m_window;

		private void Awake()
		{
			m_window = GetComponent<RuntimeCameraWindow>();
			m_window.IOCContainer.RegisterFallback((IRTEGraphicsLayer)this);
			m_window.CameraResized += OnCameraResized;
			m_graphics = IOC.Resolve<IRTEGraphics>();
			if (m_window.Index >= m_window.Editor.CameraLayerSettings.MaxGraphicsLayers)
			{
				Debug.LogError("m_editorWindow.Index >= m_editorWindow.Editor.CameraLayerSettings.MaxGraphicsLayers");
			}
			PrepareGraphicsLayerCamera();
		}

		private void Start()
		{
			if (m_camera != null && m_window != null && m_window.Camera != null)
			{
				m_camera.projectionMatrix = m_window.Camera.projectionMatrix;
			}
		}

		private void OnDestroy()
		{
			if (m_window != null)
			{
				m_window.IOCContainer.UnregisterFallback((IRTEGraphicsLayer)this);
				m_window.CameraResized -= OnCameraResized;
			}
			if (m_graphicsCamera != null)
			{
				m_graphicsCamera.Destroy();
			}
			if (m_camera != null)
			{
				Object.Destroy(m_camera.gameObject);
			}
			if (m_renderTextureCamera != null && m_renderTextureCamera.OverlayMaterial != null)
			{
				Object.Destroy(m_renderTextureCamera.OverlayMaterial);
			}
		}

		private void OnEnable()
		{
			UpdateGraphicsLayerCamera();
		}

		private void LateUpdate()
		{
			UpdateGraphicsLayerCamera();
		}

		private void OnCameraResized()
		{
			UpdateGraphicsLayerCamera();
		}

		private void PrepareGraphicsLayerCamera()
		{
			bool activeSelf = m_window.Camera.gameObject.activeSelf;
			m_window.Camera.gameObject.SetActive(value: false);
			if (m_window.Editor.IsVR && m_window.Camera.stereoEnabled && m_window.Camera.stereoTargetEye == StereoTargetEyeMask.Both)
			{
				m_camera = Object.Instantiate(m_window.Camera, m_window.Camera.transform.parent);
				m_camera.transform.SetSiblingIndex(m_window.Camera.transform.GetSiblingIndex() + 1);
			}
			else
			{
				m_camera = Object.Instantiate(m_window.Camera, m_window.Camera.transform);
			}
			for (int num = m_camera.transform.childCount - 1; num >= 0; num--)
			{
				Object.Destroy(m_camera.transform.GetChild(num).gameObject);
			}
			Component[] components = m_camera.GetComponents<Component>();
			foreach (Component component in components)
			{
				if (!(component is Transform) && !(component is Camera) && !(component is RenderTextureCamera))
				{
					Object.Destroy(component);
				}
			}
			m_camera.tag = "Untagged";
			m_camera.transform.localPosition = Vector3.zero;
			m_camera.transform.localRotation = Quaternion.identity;
			m_camera.transform.localScale = Vector3.one;
			m_camera.name = "GraphicsLayerCamera";
			m_camera.depth = m_window.Camera.depth + 1f;
			m_camera.cullingMask = 0;
			if (RenderPipelineInfo.Type == RPType.Standard)
			{
				m_graphicsCamera = m_graphics.CreateCamera(m_camera, CameraEvent.BeforeImageEffects);
			}
			else
			{
				m_graphicsCamera = m_graphics.CreateCamera(m_camera, CameraEvent.AfterImageEffectsOpaque);
			}
			m_renderTextureCamera = m_camera.GetComponent<RenderTextureCamera>();
			if (m_renderTextureCamera == null)
			{
				if (RenderPipelineInfo.Type == RPType.Standard)
				{
					if (m_window.RenderTextureUsage == RenderTextureUsage.On || (m_window.RenderTextureUsage == RenderTextureUsage.UsePipelineSettings && RenderPipelineInfo.UseRenderTextures))
					{
						CreateRenderTextureCamera();
					}
					else
					{
						m_camera.clearFlags = CameraClearFlags.Depth;
					}
				}
				else
				{
					RenderPipelineManager.beginContextRendering += OnBeginContextRendering;
					RenderPipelineManager.endContextRendering += OnEndContextRendering;
				}
			}
			else if (m_window.RenderTextureUsage == RenderTextureUsage.Off || (m_window.RenderTextureUsage == RenderTextureUsage.UsePipelineSettings && !RenderPipelineInfo.UseRenderTextures))
			{
				Object.DestroyImmediate(m_renderTextureCamera);
			}
			else
			{
				m_renderTextureCamera.OverlayMaterial = new Material(Shader.Find("Battlehub/RTCommon/RenderTextureOverlay"));
				m_camera.clearFlags = CameraClearFlags.Color;
				m_camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			}
			m_camera.allowHDR = false;
			m_window.Camera.gameObject.SetActive(activeSelf);
			m_camera.gameObject.SetActive(value: true);
		}

		private void OnBeginContextRendering(ScriptableRenderContext arg1, List<Camera> arg2)
		{
			RenderPipelineManager.beginContextRendering -= OnBeginContextRendering;
			TryStackCameras();
		}

		private void OnEndContextRendering(ScriptableRenderContext arg1, List<Camera> arg2)
		{
			RenderPipelineManager.endContextRendering -= OnEndContextRendering;
			TryCreateRenderTextureCamera();
		}

		private void TryStackCameras()
		{
			if (m_window.RenderTextureUsage == RenderTextureUsage.Off || (m_window.RenderTextureUsage == RenderTextureUsage.UsePipelineSettings && !RenderPipelineInfo.UseRenderTextures))
			{
				IOC.Resolve<IRenderPipelineCameraUtility>()?.Stack(Window.Camera, m_camera);
			}
		}

		private void CreateRenderTextureCamera()
		{
			bool activeSelf = m_camera.gameObject.activeSelf;
			m_camera.gameObject.SetActive(value: false);
			m_renderTextureCamera = m_camera.gameObject.AddComponent<RenderTextureCamera>();
			if (m_output != null)
			{
				m_renderTextureCamera.OutputRoot = m_output;
			}
			else
			{
				RuntimeWindow window = IOC.Resolve<IRTE>().GetWindow(RuntimeWindowType.Scene);
				m_renderTextureCamera.OutputRoot = (RectTransform)window.transform;
			}
			m_renderTextureCamera.OverlayMaterial = new Material(Shader.Find("Battlehub/RTCommon/RenderTextureOverlay"));
			m_camera.clearFlags = CameraClearFlags.Color;
			m_camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			m_camera.gameObject.SetActive(activeSelf);
		}

		private void TryCreateRenderTextureCamera()
		{
			if (m_window.RenderTextureUsage == RenderTextureUsage.On || (m_window.RenderTextureUsage == RenderTextureUsage.UsePipelineSettings && RenderPipelineInfo.UseRenderTextures))
			{
				CreateRenderTextureCamera();
			}
		}

		private void UpdateGraphicsLayerCamera()
		{
			if (!(m_camera == null))
			{
				if (m_renderTextureCamera != null)
				{
					m_renderTextureCamera.TryResizeRenderTexture();
				}
				if (m_camera.depth != m_window.Camera.depth + 1f)
				{
					m_camera.depth = m_window.Camera.depth + 1f;
				}
				if (m_camera.fieldOfView != m_window.Camera.fieldOfView)
				{
					m_camera.fieldOfView = m_window.Camera.fieldOfView;
				}
				if (m_camera.orthographic != m_window.Camera.orthographic)
				{
					m_camera.orthographic = m_window.Camera.orthographic;
				}
				if (m_camera.orthographicSize != m_window.Camera.orthographicSize)
				{
					m_camera.orthographicSize = m_window.Camera.orthographicSize;
				}
				if (m_camera.rect != m_window.Camera.rect)
				{
					m_camera.rect = m_window.Camera.rect;
				}
				if (m_camera.enabled != m_window.Camera.enabled)
				{
					m_camera.enabled = m_window.Camera.enabled;
				}
				if (m_window.Camera.pixelWidth > 0 && m_window.Camera.pixelHeight > 0)
				{
					m_camera.projectionMatrix = m_window.Camera.projectionMatrix;
				}
			}
		}
	}
}
