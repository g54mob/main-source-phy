using System;
using System.Threading.Tasks;
using Battlehub.RTCommon;
using Battlehub.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Battlehub.RTHandles
{
	[RequireComponent(typeof(Camera))]
	public class SceneGizmo : RTEComponent
	{
		public Button BtnProjection;

		public Transform Pivot;

		[HideInInspector]
		public Vector2 Size = new Vector2(96f, 96f);

		public Vector2 PivotPoint = new Vector2(1f, 0f);

		public Vector2 Anchor = new Vector2(1f, 0f);

		[HideInInspector]
		public Vector3 Up = Vector3.up;

		public RuntimeHandlesComponent Appearance;

		public UnityEvent OrientationChanging;

		public UnityEvent OrientationChanged;

		public UnityEvent ProjectionChanged;

		private float m_scale;

		private Rect m_cameraPixelRect;

		private float m_aspect;

		private Camera m_camera;

		private MaterialPropertyBlock[] m_propertyBlocks;

		private float m_xAlpha = 1f;

		private float m_yAlpha = 1f;

		private float m_zAlpha = 1f;

		private float m_animationDuration = 0.2f;

		private bool m_mouseOver;

		private Vector3 m_selectedAxis;

		private GameObject m_collidersGO;

		private BoxCollider m_colliderProj;

		private BoxCollider m_colliderUp;

		private BoxCollider m_colliderDown;

		private BoxCollider m_colliderForward;

		private BoxCollider m_colliderBackward;

		private BoxCollider m_colliderLeft;

		private BoxCollider m_colliderRight;

		private Collider[] m_colliders;

		private Vector3 m_gizmoPosition;

		private IAnimationInfo m_rotateAnimation;

		private float m_screenHeight;

		private float m_screenWidth;

		private bool m_projectionChanged;

		[SerializeField]
		private Color m_textColor = Color.white;

		private Material m_material;

		private GameObject m_output;

		private RenderTexture m_renderTexture;

		private IRTECamera m_rteCamera;

		private RTECamera m_rteGizmoCamera;

		private IRenderPipelineCameraUtility m_cameraUtility;

		private bool m_disableCamera;

		public bool IsOrthographic
		{
			get
			{
				return m_camera.orthographic;
			}
			set
			{
				m_projectionChanged = true;
				m_camera.orthographic = value;
				Window.Camera.orthographic = value;
				if (BtnProjection != null)
				{
					Text componentInChildren = BtnProjection.GetComponentInChildren<Text>();
					if (componentInChildren != null)
					{
						if (value)
						{
							componentInChildren.text = "Ortho";
						}
						else
						{
							componentInChildren.text = "Persp";
						}
					}
					else
					{
						TextMeshProUGUI componentInChildren2 = BtnProjection.GetComponentInChildren<TextMeshProUGUI>();
						if (componentInChildren2 != null)
						{
							if (value)
							{
								componentInChildren2.text = "Ortho";
							}
							else
							{
								componentInChildren2.text = "Persp";
							}
						}
					}
				}
				if (ProjectionChanged != null)
				{
					ProjectionChanged.Invoke();
					InitColliders();
				}
			}
		}

		public Color TextColor
		{
			get
			{
				return m_textColor;
			}
			set
			{
				m_textColor = value;
				SetTextColor();
			}
		}

		private void SetTextColor()
		{
			if (BtnProjection != null)
			{
				Text componentInChildren = BtnProjection.GetComponentInChildren<Text>();
				if (componentInChildren != null)
				{
					componentInChildren.color = m_textColor;
				}
				TextMeshProUGUI componentInChildren2 = BtnProjection.GetComponentInChildren<TextMeshProUGUI>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.color = m_textColor;
				}
			}
			RefreshCommandBuffer();
		}

		protected override void Awake()
		{
			base.Awake();
			RuntimeHandlesComponent.InitializeIfRequired(ref Appearance);
			if (Pivot == null)
			{
				Pivot = base.transform;
			}
			m_collidersGO = new GameObject();
			m_collidersGO.transform.SetParent(base.transform, worldPositionStays: false);
			m_collidersGO.transform.position = GetGizmoPosition();
			m_collidersGO.transform.rotation = Quaternion.identity;
			m_collidersGO.name = "Colliders";
			m_colliderProj = m_collidersGO.AddComponent<BoxCollider>();
			m_colliderUp = m_collidersGO.AddComponent<BoxCollider>();
			m_colliderDown = m_collidersGO.AddComponent<BoxCollider>();
			m_colliderLeft = m_collidersGO.AddComponent<BoxCollider>();
			m_colliderRight = m_collidersGO.AddComponent<BoxCollider>();
			m_colliderForward = m_collidersGO.AddComponent<BoxCollider>();
			m_colliderBackward = m_collidersGO.AddComponent<BoxCollider>();
			Collider[] colliders = new BoxCollider[7] { m_colliderProj, m_colliderUp, m_colliderDown, m_colliderRight, m_colliderLeft, m_colliderForward, m_colliderBackward };
			m_colliders = colliders;
			DisableColliders();
			m_camera = GetComponent<Camera>();
			m_rteGizmoCamera = m_camera.gameObject.AddComponent<RTECamera>();
			m_rteGizmoCamera.Event = CameraEvent.BeforeImageEffects;
			m_rteGizmoCamera.CommandBufferRefresh += OnCommandBufferRefresh;
			m_propertyBlocks = new MaterialPropertyBlock[6]
			{
				new MaterialPropertyBlock(),
				new MaterialPropertyBlock(),
				new MaterialPropertyBlock(),
				new MaterialPropertyBlock(),
				new MaterialPropertyBlock(),
				new MaterialPropertyBlock()
			};
			m_cameraUtility = IOC.Resolve<IRenderPipelineCameraUtility>();
			if (m_cameraUtility != null)
			{
				m_cameraUtility.SetBackgroundColor(m_camera, new Color(0f, 0f, 0f, 0f));
				m_cameraUtility.EnablePostProcessing(m_camera, value: false);
				m_cameraUtility.PostProcessingEnabled += OnPostProcessingEnabled;
			}
			m_material = new Material(Shader.Find("Battlehub/RTHandles/RawImage"));
			m_output = new GameObject("SceneGizmoOutput");
			m_output.gameObject.SetActive(value: false);
			m_output.transform.SetParent(Window.Camera.transform, worldPositionStays: false);
			m_output.transform.localPosition = Vector3.forward * m_camera.nearClipPlane;
			m_output.AddComponent<MeshFilter>().sharedMesh = Appearance.CreateRawImageMesh();
			m_output.AddComponent<MeshRenderer>().sharedMaterial = m_material;
			m_camera.clearFlags = CameraClearFlags.Color;
			m_camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			if (m_cameraUtility != null)
			{
				m_cameraUtility.ResetCullingMask(m_camera);
			}
			else
			{
				m_camera.cullingMask = 0;
			}
			m_camera.orthographic = Window.Camera.orthographic;
			m_camera.rect = new Rect(0f, 0f, 1f, 1f);
			if (RenderPipelineInfo.Type == RPType.Standard)
			{
				m_camera.stereoTargetEye = StereoTargetEyeMask.None;
			}
			m_screenHeight = Screen.height;
			m_screenWidth = Screen.width;
			UpdateLayout();
			InitColliders();
			UpdateAlpha(ref m_xAlpha, Vector3.right, 1f);
			UpdateAlpha(ref m_yAlpha, Vector3.up, 1f);
			UpdateAlpha(ref m_zAlpha, Vector3.forward, 1f);
			Sync();
			if (Run.Instance == null)
			{
				GameObject obj = new GameObject();
				obj.name = "Run";
				obj.AddComponent<Run>();
			}
			if (BtnProjection != null)
			{
				BtnProjection.onClick.AddListener(OnBtnModeClick);
				SetTextColor();
			}
			if (!GetComponent<SceneGizmoInput>())
			{
				base.gameObject.AddComponent<SceneGizmoInput>();
			}
		}

		private void OnPostProcessingEnabled(Camera camera, bool enabled)
		{
			if (camera == Window.Camera)
			{
				UpdateLayout();
				RefreshCommandBuffer();
			}
		}

		protected override async void Start()
		{
			base.Start();
			if (IsOrthographic != Window.Camera.orthographic)
			{
				IsOrthographic = Window.Camera.orthographic;
			}
			Init();
			await Task.Yield();
			RefreshCommandBuffer();
			await Task.Yield();
			RefreshCommandBuffer();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (BtnProjection != null)
			{
				BtnProjection.onClick.RemoveListener(OnBtnModeClick);
			}
			if (base.Editor != null && base.Editor.Tools != null && base.Editor.Tools.ActiveTool == this && base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
			if (m_rteGizmoCamera != null)
			{
				m_rteGizmoCamera.CommandBufferRefresh -= OnCommandBufferRefresh;
				UnityEngine.Object.Destroy(m_rteGizmoCamera);
			}
			if (m_material != null)
			{
				UnityEngine.Object.Destroy(m_material);
			}
			if (m_renderTexture != null)
			{
				UnityEngine.Object.Destroy(m_renderTexture);
			}
			if (m_output != null)
			{
				UnityEngine.Object.Destroy(m_output);
			}
			if (m_cameraUtility != null)
			{
				m_cameraUtility.PostProcessingEnabled -= OnPostProcessingEnabled;
			}
		}

		protected virtual void OnEnable()
		{
			if (base.IsStarted)
			{
				Init();
			}
		}

		private void Init()
		{
			Camera camera = Window.Camera;
			IRTEGraphicsLayer iRTEGraphicsLayer = Window.IOCContainer.Resolve<IRTEGraphicsLayer>();
			if (iRTEGraphicsLayer != null && iRTEGraphicsLayer.Camera != null)
			{
				camera = iRTEGraphicsLayer.Camera.Camera;
			}
			if (!(camera == null))
			{
				UpdateAlpha(ref m_xAlpha, Vector3.right, 1f);
				UpdateAlpha(ref m_yAlpha, Vector3.up, 1f);
				UpdateAlpha(ref m_zAlpha, Vector3.forward, 1f);
				IRTEGraphics iRTEGraphics = IOC.Resolve<IRTEGraphics>();
				m_rteCamera = iRTEGraphics.CreateCamera(camera, CameraEvent.AfterImageEffects, meshesCache: false, renderersCache: true);
				m_rteCamera.RenderersCache.Add(m_output.GetComponent<Renderer>());
				m_rteCamera.RefreshCommandBuffer();
				RefreshCommandBuffer();
				if (BtnProjection != null)
				{
					BtnProjection.gameObject.SetActive(value: true);
				}
			}
		}

		protected virtual void OnDisable()
		{
			if (m_rteCamera != null)
			{
				m_rteCamera.Destroy();
				m_rteCamera = null;
			}
			if (BtnProjection != null)
			{
				BtnProjection.gameObject.SetActive(value: false);
			}
		}

		private void OnBtnModeClick()
		{
			IsOrthographic = !Window.Camera.orthographic;
		}

		protected override void OnWindowDeactivated()
		{
			base.OnWindowDeactivated();
			if (base.Editor != null && base.Editor.Tools != null && base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
		}

		private void Update()
		{
			bool num = Sync();
			float delta = Time.unscaledDeltaTime / m_animationDuration;
			bool flag = UpdateAlpha(ref m_xAlpha, Vector3.right, delta);
			flag |= UpdateAlpha(ref m_yAlpha, Vector3.up, delta);
			flag |= UpdateAlpha(ref m_zAlpha, Vector3.forward, delta);
			if (num || flag || m_mouseOver || m_projectionChanged)
			{
				if (flag)
				{
					DisableColliders();
					EnableColliders();
				}
				m_projectionChanged = false;
				RefreshCommandBuffer();
			}
			else if (!m_disableCamera)
			{
				m_disableCamera = true;
			}
			else if (RenderPipelineInfo.Type != RPType.HDRP)
			{
				m_camera.enabled = false;
				m_disableCamera = false;
			}
			if (base.Editor.Tools.IsViewing)
			{
				if (m_selectedAxis != Vector3.zero)
				{
					m_selectedAxis = Vector3.zero;
					RefreshCommandBuffer();
				}
				return;
			}
			if (base.Editor.Tools.ActiveTool != null && base.Editor.Tools.ActiveTool != this)
			{
				if (m_selectedAxis != Vector3.zero)
				{
					m_selectedAxis = Vector3.zero;
					RefreshCommandBuffer();
				}
				return;
			}
			bool flag2 = false;
			if (m_camera.pixelRect.Contains(ScreenPointToViewPoint(Window.Pointer.ScreenPoint)) && base.Editor.ActiveWindow == Window && Window.IsPointerOver)
			{
				if (!m_mouseOver || flag)
				{
					InitColliders();
					EnableColliders();
				}
				Collider collider = HitTest();
				if (collider == null)
				{
					m_selectedAxis = Vector3.zero;
				}
				else if (collider == m_colliderProj)
				{
					m_selectedAxis = Vector3.one;
				}
				else if (collider == m_colliderUp)
				{
					m_selectedAxis = Vector3.up;
				}
				else if (collider == m_colliderDown)
				{
					m_selectedAxis = Vector3.down;
				}
				else if (collider == m_colliderForward)
				{
					m_selectedAxis = Vector3.forward;
				}
				else if (collider == m_colliderBackward)
				{
					m_selectedAxis = Vector3.back;
				}
				else if (collider == m_colliderRight)
				{
					m_selectedAxis = Vector3.right;
				}
				else if (collider == m_colliderLeft)
				{
					m_selectedAxis = Vector3.left;
				}
				if (m_selectedAxis != Vector3.zero || flag2)
				{
					base.Editor.Tools.ActiveTool = this;
				}
				else if (base.Editor.Tools.ActiveTool == this)
				{
					base.Editor.Tools.ActiveTool = null;
				}
				m_mouseOver = true;
				return;
			}
			if (m_mouseOver)
			{
				DisableColliders();
				if (base.Editor.Tools.ActiveTool == this)
				{
					base.Editor.Tools.ActiveTool = null;
				}
			}
			if (m_selectedAxis != Vector3.zero)
			{
				m_selectedAxis = Vector3.zero;
				RefreshCommandBuffer();
			}
			m_mouseOver = false;
		}

		private void RefreshCommandBuffer()
		{
			if (m_camera != null)
			{
				m_camera.enabled = true;
				m_rteGizmoCamera.RefreshCommandBuffer();
			}
		}

		private void OnCommandBufferRefresh(IRTECamera rteCamera)
		{
			Appearance.DoSceneGizmo(rteCamera.RTECommandBuffer, m_propertyBlocks, m_camera, GetGizmoPosition(), Quaternion.identity, m_selectedAxis, Appearance.SceneGizmoScale, m_textColor, m_xAlpha, m_yAlpha, m_zAlpha);
		}

		public void DoSceneGizmo()
		{
			m_projectionChanged = true;
		}

		public void Click()
		{
			if (m_selectedAxis != Vector3.zero)
			{
				if (m_selectedAxis == Vector3.one)
				{
					IsOrthographic = !IsOrthographic;
					RefreshCommandBuffer();
				}
				else
				{
					ChangeOrientation(-m_selectedAxis);
				}
			}
		}

		public void ChangeOrientation(Vector3 axis)
		{
			if ((m_rotateAnimation == null || !m_rotateAnimation.InProgress) && OrientationChanging != null)
			{
				OrientationChanging.Invoke();
			}
			if (m_rotateAnimation != null)
			{
				m_rotateAnimation.Abort();
			}
			Vector3 pivot = Pivot.transform.position;
			Vector3 radiusVector = Vector3.back * (Window.Camera.transform.position - pivot).magnitude;
			Quaternion to = Quaternion.LookRotation(axis, Up);
			m_rotateAnimation = new QuaternionAnimationInfo(Window.Camera.transform.rotation, to, 0.4f, AnimationInfo<object, Quaternion>.EaseOutCubic, delegate(object target, Quaternion value, float t, bool completed)
			{
				Window.Camera.transform.position = pivot + value * radiusVector;
				Window.Camera.transform.rotation = value;
				if (completed)
				{
					DisableColliders();
					EnableColliders();
					if (OrientationChanged != null)
					{
						OrientationChanged.Invoke();
					}
				}
			});
			Run.Instance.Animation(m_rotateAnimation);
		}

		private bool Sync()
		{
			bool result = false;
			if (m_screenHeight != (float)Screen.height || m_screenWidth != (float)Screen.width || m_cameraPixelRect != Window.Camera.pixelRect || m_scale != Appearance.SceneGizmoScale)
			{
				UpdateLayout();
				result = true;
			}
			if (m_aspect != m_camera.aspect)
			{
				m_aspect = m_camera.aspect;
				result = true;
			}
			if (m_camera.depth != Window.Camera.depth + 1f)
			{
				m_camera.depth = Window.Camera.depth + 1f;
				result = true;
			}
			Quaternion rotation = Window.Camera.transform.rotation;
			if (rotation != m_camera.transform.rotation)
			{
				m_camera.transform.rotation = rotation;
				result = true;
			}
			return result;
		}

		private void EnableColliders()
		{
			m_colliderProj.enabled = true;
			if (m_zAlpha == 1f)
			{
				m_colliderForward.enabled = true;
				m_colliderBackward.enabled = true;
			}
			if (m_yAlpha == 1f)
			{
				m_colliderUp.enabled = true;
				m_colliderDown.enabled = true;
			}
			if (m_xAlpha == 1f)
			{
				m_colliderRight.enabled = true;
				m_colliderLeft.enabled = true;
			}
		}

		private void DisableColliders()
		{
			for (int i = 0; i < m_colliders.Length; i++)
			{
				m_colliders[i].enabled = false;
			}
		}

		private Vector2 ScreenPointToViewPoint(Vector2 screenPoint)
		{
			Vector2 vector = Size * Appearance.SceneGizmoScale;
			Rect pixelRect = Window.Camera.pixelRect;
			float num = pixelRect.width * Anchor.x - vector.x * PivotPoint.x;
			float num2 = pixelRect.height - (pixelRect.height * Anchor.y + (vector.y - vector.y * PivotPoint.y));
			screenPoint.x -= num + pixelRect.x;
			screenPoint.y -= num2 + pixelRect.y;
			return screenPoint;
		}

		private Collider HitTest()
		{
			Ray ray = m_camera.ScreenPointToRay(ScreenPointToViewPoint(Window.Pointer.ScreenPoint));
			float num = float.MaxValue;
			Collider result = null;
			for (int i = 0; i < m_colliders.Length; i++)
			{
				if (m_colliders[i].Raycast(ray, out var hitInfo, m_gizmoPosition.magnitude * 5f) && hitInfo.distance < num)
				{
					num = hitInfo.distance;
					result = hitInfo.collider;
				}
			}
			return result;
		}

		private Vector3 GetGizmoPosition()
		{
			return base.transform.TransformPoint(Vector3.forward * 5f);
		}

		private void InitColliders()
		{
			m_gizmoPosition = GetGizmoPosition();
			float num = RuntimeHandlesComponent.GetScreenScale(m_gizmoPosition, m_camera) * Appearance.SceneGizmoScale;
			m_collidersGO.transform.rotation = Quaternion.identity;
			m_collidersGO.transform.position = GetGizmoPosition();
			m_collidersGO.layer = LayerMask.NameToLayer("Ignore Raycast");
			m_colliderProj.size = new Vector3(0.22500001f, 0.22500001f, 0.22500001f) * num;
			m_colliderUp.size = new Vector3(0.15f, 0.3f, 0.15f) * num;
			m_colliderUp.center = new Vector3(0f, 0.22500001f, 0f) * num;
			m_colliderDown.size = new Vector3(0.15f, 0.3f, 0.15f) * num;
			m_colliderDown.center = new Vector3(0f, -0.22500001f, 0f) * num;
			m_colliderForward.size = new Vector3(0.15f, 0.15f, 0.3f) * num;
			m_colliderForward.center = new Vector3(0f, 0f, 0.22500001f) * num;
			m_colliderBackward.size = new Vector3(0.15f, 0.15f, 0.3f) * num;
			m_colliderBackward.center = new Vector3(0f, 0f, -0.22500001f) * num;
			m_colliderRight.size = new Vector3(0.3f, 0.15f, 0.15f) * num;
			m_colliderRight.center = new Vector3(0.22500001f, 0f, 0f) * num;
			m_colliderLeft.size = new Vector3(0.3f, 0.15f, 0.15f) * num;
			m_colliderLeft.center = new Vector3(-0.22500001f, 0f, 0f) * num;
		}

		private bool UpdateAlpha(ref float alpha, Vector3 axis, float delta)
		{
			if ((double)Math.Abs(Vector3.Dot(Window.Camera.transform.forward, axis)) > 0.9)
			{
				if (alpha > 0f)
				{
					alpha -= delta;
					if (alpha < 0f)
					{
						alpha = 0f;
					}
					return true;
				}
			}
			else if (alpha < 1f)
			{
				alpha += delta;
				if (alpha > 1f)
				{
					alpha = 1f;
				}
				return true;
			}
			return false;
		}

		public void UpdateLayout()
		{
			m_screenHeight = Screen.height;
			m_screenWidth = Screen.width;
			m_cameraPixelRect = Window.Camera.pixelRect;
			m_scale = Appearance.SceneGizmoScale;
			if (m_camera == null)
			{
				return;
			}
			m_aspect = m_camera.aspect;
			if (Window.Camera != null)
			{
				bool flag = false;
				if (m_camera.pixelRect.height == 0f || m_camera.pixelRect.width == 0f)
				{
					return;
				}
				if (!base.enabled)
				{
					flag = true;
				}
				m_camera.depth = Window.Camera.depth + 1f;
				m_aspect = m_camera.aspect;
				if (flag)
				{
					InitColliders();
				}
			}
			Vector2 pivotPoint = PivotPoint;
			Vector2 anchor = Anchor;
			if (m_renderTexture != null)
			{
				UnityEngine.Object.Destroy(m_renderTexture);
			}
			Vector2 vector = Size * Appearance.SceneGizmoScale;
			RenderTexture renderTexture = m_renderTexture;
			m_renderTexture = new RenderTexture((int)vector.x, (int)vector.y, 24, RenderTextureFormat.ARGB32);
			m_renderTexture.filterMode = FilterMode.Point;
			m_renderTexture.antiAliasing = Mathf.Max(1, RenderPipelineInfo.MSAASampleCount);
			m_material.SetTexture("_MainTex", m_renderTexture);
			m_material.SetFloat("_Width", vector.x);
			m_material.SetFloat("_Height", vector.y);
			m_material.SetVector("_PivotAndAnchor", new Vector4(pivotPoint.x, pivotPoint.y, anchor.x, anchor.y));
			m_camera.targetTexture = m_renderTexture;
			if (renderTexture != null)
			{
				renderTexture.Release();
				UnityEngine.Object.Destroy(renderTexture);
			}
		}
	}
}
