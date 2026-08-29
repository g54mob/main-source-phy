using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-90)]
	public class RenderTextureCamera : MonoBehaviour
	{
		[SerializeField]
		private RectTransform m_outputRoot;

		[SerializeField]
		private Material m_overlayMaterial;

		[SerializeField]
		private bool m_allowMSAA = true;

		[SerializeField]
		private bool m_fullscreen = true;

		private Camera m_camera;

		private RawImage m_output;

		private Canvas m_canvas;

		private CanvasScaler m_canvasScaler;

		private int m_screenWidth;

		private int m_screenHeight;

		private Rect m_outputRect;

		private Vector3 m_position;

		private RenderTexture m_texture;

		private static int m_skipFrames;

		public RectTransform OutputRoot
		{
			get
			{
				return m_outputRoot;
			}
			set
			{
				m_outputRoot = value;
				m_canvas = m_outputRoot.GetComponentInParent<Canvas>();
				m_canvasScaler = m_outputRoot.GetComponentInParent<CanvasScaler>();
			}
		}

		public Material OverlayMaterial
		{
			get
			{
				return m_overlayMaterial;
			}
			set
			{
				m_overlayMaterial = value;
				if (m_output != null)
				{
					m_output.material = m_overlayMaterial;
				}
			}
		}

		public bool AllowMSAA
		{
			get
			{
				return m_allowMSAA;
			}
			set
			{
				if (!(m_output == null) && !(m_camera == null))
				{
					m_allowMSAA = value;
					ResizeRenderTexture();
				}
			}
		}

		public bool Fullscreen
		{
			get
			{
				return m_fullscreen;
			}
			set
			{
				m_fullscreen = value;
				ResizeOutput();
			}
		}

		public Camera Camera => m_camera;

		public RawImage Output
		{
			get
			{
				return m_output;
			}
			set
			{
				m_output = value;
			}
		}

		public RectTransform RectTransform => m_output.rectTransform;

		public Canvas Canvas => m_canvas;

		public static int SkipFrames
		{
			get
			{
				return m_skipFrames;
			}
			set
			{
				m_skipFrames = value;
			}
		}

		private void Awake()
		{
			m_camera = GetComponent<Camera>();
			if (!m_fullscreen)
			{
				m_camera.rect = new Rect(0f, 0f, 1f, 1f);
			}
			GameObject gameObject = null;
			if (m_output == null)
			{
				gameObject = new GameObject(m_camera.name + " Output");
				gameObject.SetActive(value: false);
				m_output = gameObject.AddComponent<RawImage>();
				m_output.raycastTarget = false;
				if (m_overlayMaterial != null)
				{
					m_output.material = m_overlayMaterial;
				}
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.SetParent(m_outputRoot, worldPositionStays: false);
				component.anchorMin = new Vector2(0f, 0f);
				component.anchorMax = new Vector2(1f, 1f);
				component.offsetMin = Vector2.zero;
				component.offsetMax = Vector2.zero;
				component.pivot = Vector2.zero;
			}
			else
			{
				m_outputRoot = m_output.rectTransform;
			}
			m_canvas = m_outputRoot.GetComponentInParent<Canvas>();
			m_canvasScaler = m_outputRoot.GetComponentInParent<CanvasScaler>();
			ResizeRenderTexture();
			ResizeOutput();
			if (gameObject != null)
			{
				gameObject.SetActive(value: true);
			}
		}

		private void OnDestroy()
		{
			if (m_texture != null)
			{
				if (m_camera.targetTexture == m_texture)
				{
					m_camera.targetTexture = null;
				}
				m_texture.Release();
				m_texture = null;
			}
			if (m_output != null)
			{
				Object.Destroy(m_output.gameObject);
			}
		}

		private void LateUpdate()
		{
			TryResizeRenderTexture();
		}

		public bool TryResizeRenderTexture(bool canResizeOutput = true)
		{
			if (m_skipFrames > 0)
			{
				m_skipFrames--;
				return false;
			}
			if (m_output == null)
			{
				return false;
			}
			bool flag = m_outputRect != m_output.rectTransform.rect || m_screenWidth != Screen.width || m_screenHeight != Screen.height;
			bool flag2 = canResizeOutput && (flag || m_output.rectTransform.position != m_position);
			if (m_canvas.renderMode == RenderMode.ScreenSpaceOverlay && m_output.uvRect != m_camera.rect)
			{
				flag2 = true;
			}
			if (flag)
			{
				ResizeRenderTexture();
			}
			if (flag2)
			{
				ResizeOutput();
			}
			return flag || flag2;
		}

		public void ResizeRenderTexture()
		{
			if (m_fullscreen)
			{
				int width = Screen.width;
				int height = Screen.height;
				ResizeRenderTexture(width, height);
			}
			else if (m_output != null)
			{
				Vector2 vector = m_output.rectTransform.rect.size * ((m_canvasScaler != null) ? m_canvasScaler.scaleFactor : 1f);
				int width = Mathf.RoundToInt(vector.x);
				int height = Mathf.RoundToInt(vector.y);
				ResizeRenderTexture(width, height);
			}
		}

		private void ResizeRenderTexture(int sizeX, int sizeY)
		{
			RenderTexture texture = m_texture;
			m_texture = new RenderTexture(Mathf.Max(2, sizeX), Mathf.Max(2, sizeY), 24, RenderTextureFormat.ARGB32);
			m_texture.name = m_camera.name + " RenderTexture";
			m_texture.filterMode = FilterMode.Point;
			m_texture.antiAliasing = ((!m_allowMSAA) ? 1 : Mathf.Max(1, RenderPipelineInfo.MSAASampleCount));
			m_camera.targetTexture = m_texture;
			m_output.texture = m_texture;
			m_outputRect = m_output.rectTransform.rect;
			m_screenWidth = Screen.width;
			m_screenHeight = Screen.height;
			if (texture != null)
			{
				texture.Release();
			}
		}

		public void ResizeOutput()
		{
			if (m_output == null)
			{
				return;
			}
			if (m_fullscreen)
			{
				if (m_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					if (m_camera == null)
					{
						return;
					}
					m_output.uvRect = m_camera.rect;
				}
				else
				{
					RectTransformUtility.ScreenPointToLocalPointInRectangle(m_outputRoot, Vector2.zero, m_canvas.worldCamera, out var localPoint);
					RectTransformUtility.ScreenPointToLocalPointInRectangle(m_outputRoot, new Vector2(Screen.width, Screen.height), m_canvas.worldCamera, out var localPoint2);
					m_output.rectTransform.anchoredPosition = localPoint;
					m_output.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(localPoint2.x - localPoint.x));
					m_output.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(localPoint2.y - localPoint.y));
				}
			}
			m_position = m_output.rectTransform.position;
		}
	}
}
