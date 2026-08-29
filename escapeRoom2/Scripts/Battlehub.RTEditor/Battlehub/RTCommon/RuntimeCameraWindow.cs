using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Battlehub.RTCommon
{
	public class RuntimeCameraWindow : RuntimeWindow
	{
		[SerializeField]
		private RenderTextureUsage m_renderTextureUsage;

		[SerializeField]
		protected Camera m_camera;

		private int m_cameraDepth;

		private Vector3 m_position;

		private Rect m_rect;

		public RenderTextureUsage RenderTextureUsage
		{
			get
			{
				return m_renderTextureUsage;
			}
			set
			{
				m_renderTextureUsage = value;
			}
		}

		public override Camera Camera
		{
			get
			{
				return m_camera;
			}
			set
			{
				if (m_camera == value)
				{
					return;
				}
				if (m_camera != null)
				{
					ResetCullingMask();
					UnregisterGraphicsCamera();
				}
				m_camera = value;
				if (m_camera != null)
				{
					SetCullingMask();
					if (WindowType == RuntimeWindowType.Scene)
					{
						RegisterGraphicsCamera();
					}
					RenderPipelineInfo.XRFix(Camera);
					m_camera.depth = m_cameraDepth;
				}
			}
		}

		public int CameraDepth => m_cameraDepth;

		public event Action CameraResized;

		public virtual void SetCameraDepth(int depth)
		{
			m_cameraDepth = depth;
			if (m_camera != null)
			{
				m_camera.depth = m_cameraDepth;
			}
		}

		protected override void AwakeOverride()
		{
			base.AwakeOverride();
			if (RenderPipelineInfo.Type != RPType.Standard)
			{
				UnityEngine.Object.DestroyImmediate(GetComponent<RTEGraphicsLayer>());
			}
			if (Camera != null)
			{
				CreateWindowBackground();
				if (RenderTextureUsage == RenderTextureUsage.Off || (RenderTextureUsage == RenderTextureUsage.UsePipelineSettings && !RenderPipelineInfo.UseRenderTextures))
				{
					RenderTextureCamera component = Camera.GetComponent<RenderTextureCamera>();
					if (component != null)
					{
						UnityEngine.Object.DestroyImmediate(component);
					}
				}
				RenderPipelineInfo.XRFix(Camera);
			}
			if (m_camera != null)
			{
				SetCullingMask();
				RegisterGraphicsCamera();
			}
		}

		protected virtual void CreateWindowBackground()
		{
			Image component = GetComponent<Image>();
			if (component != null)
			{
				Color color = component.color;
				color.a = 0f;
				component.color = color;
			}
		}

		protected override void OnDestroyOverride()
		{
			base.OnDestroyOverride();
			if (m_camera != null)
			{
				ResetCullingMask();
				UnregisterGraphicsCamera();
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			TryResize();
		}

		protected virtual void Update()
		{
			UpdateOverride();
		}

		protected override void UpdateOverride()
		{
			TryResize();
		}

		protected virtual void RegisterGraphicsCamera()
		{
			IOC.Resolve<IRTEGraphics>()?.RegisterCamera(m_camera);
		}

		protected virtual void UnregisterGraphicsCamera()
		{
			IOC.Resolve<IRTEGraphics>()?.UnregisterCamera(m_camera);
		}

		public IRTECamera GetGraphicsCamera()
		{
			IRTEGraphicsLayer iRTEGraphicsLayer = base.IOCContainer.Resolve<IRTEGraphicsLayer>();
			if (iRTEGraphicsLayer != null)
			{
				return iRTEGraphicsLayer.Camera;
			}
			return IOC.Resolve<IRTEGraphics>().GetOrCreateCamera(Camera, CameraEvent.AfterImageEffectsOpaque, meshesCache: false, renderersCache: false);
		}

		private void TryResize()
		{
			if (m_camera != null && base.ViewRoot != null && (base.ViewRoot.rect != m_rect || base.ViewRoot.position != m_position))
			{
				HandleResize();
				m_rect = base.ViewRoot.rect;
				m_position = base.ViewRoot.position;
			}
		}

		public override void HandleResize()
		{
			if (m_camera == null)
			{
				return;
			}
			Canvas canvas = base.Canvas;
			if (base.ViewRoot != null && canvas != null)
			{
				if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					Vector3[] array = new Vector3[4];
					base.ViewRoot.GetWorldCorners(array);
					ResizeCamera(new Rect(array[0], new Vector2(array[2].x - array[0].x, array[1].y - array[0].y)));
				}
				else if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera != Camera)
				{
					Vector3[] array2 = new Vector3[4];
					base.ViewRoot.GetWorldCorners(array2);
					array2[0] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, array2[0]);
					array2[1] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, array2[1]);
					array2[2] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, array2[2]);
					array2[3] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, array2[3]);
					ResizeCamera(new Rect(size: new Vector2(array2[2].x - array2[0].x, array2[1].y - array2[0].y), position: array2[0]));
				}
			}
		}

		protected virtual void ResizeCamera(Rect pixelRect)
		{
			m_camera.pixelRect = pixelRect;
			if (this.CameraResized != null)
			{
				this.CameraResized();
			}
		}

		protected virtual void SetCullingMask()
		{
			SetCullingMask(m_camera);
		}

		protected virtual void ResetCullingMask()
		{
			ResetCullingMask(m_camera);
		}

		protected virtual void SetCullingMask(Camera camera)
		{
			CameraLayerSettings cameraLayerSettings = base.Editor.CameraLayerSettings;
			camera.cullingMask &= cameraLayerSettings.RaycastMask | (1 << cameraLayerSettings.AllScenesLayer);
		}

		protected virtual void ResetCullingMask(Camera camera)
		{
			CameraLayerSettings cameraLayerSettings = base.Editor.CameraLayerSettings;
			camera.cullingMask |= ~(cameraLayerSettings.RaycastMask | (1 << cameraLayerSettings.AllScenesLayer));
		}
	}
}
