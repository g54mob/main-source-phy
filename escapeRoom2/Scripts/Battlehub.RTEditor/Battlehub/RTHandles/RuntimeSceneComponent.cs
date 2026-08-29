using System;
using System.Linq;
using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class RuntimeSceneComponent : RuntimeSelectionComponent, IRuntimeSceneComponent, IRuntimeSelectionComponent, IScenePivot
	{
		public Texture2D ViewTexture;

		public Texture2D MoveTexture;

		public Texture2D FreeMoveTexture;

		[SerializeField]
		private RectTransform m_sceneGizmoTransform;

		private bool m_isSceneGizmoEnabled = true;

		[SerializeField]
		private bool m_canPan = true;

		[SerializeField]
		private bool m_canZoom = true;

		[SerializeField]
		private bool m_changeOrthographicSizeOnly;

		[SerializeField]
		private bool m_canRotate = true;

		[SerializeField]
		private bool m_canFreeMove = true;

		[SerializeField]
		private float m_orbitDistance = 5f;

		[SerializeField]
		private float m_boundingSphereRadius = 10000f;

		[SerializeField]
		private float m_minOrthoSize = 0.01f;

		[SerializeField]
		private float m_maxOrthoSize = 10000f;

		[SerializeField]
		private bool m_constantZoomSpeed;

		[SerializeField]
		private float m_zoomSensitivity = 1f;

		[SerializeField]
		private float m_moveSensitivity = 1f;

		[SerializeField]
		private float m_rotationSensitivity = 1f;

		[SerializeField]
		private float m_freeMovementSmoothSpeed = 10f;

		[SerializeField]
		private float m_freeRotationSmoothSpeed = 10f;

		[SerializeField]
		private bool m_rotationInvertX;

		[SerializeField]
		private bool m_rotationInvertY;

		private Quaternion m_targetRotation;

		private Vector3 m_targetPosition;

		private Quaternion m_prevCamRotation;

		private Vector3 m_prevCamPosition;

		private bool m_isSceneGizmoOrientationChanging;

		private Plane m_dragPlane;

		private Vector3 m_lastMousePosition;

		private bool m_lockInput;

		[SerializeField]
		private SceneGizmo m_sceneGizmo;

		private IAnimationInfo m_focusAnimation;

		private Transform m_autoFocusTransform;

		public bool IsSceneGizmoEnabled
		{
			get
			{
				if (m_isSceneGizmoEnabled)
				{
					return m_sceneGizmo != null;
				}
				return false;
			}
			set
			{
				m_isSceneGizmoEnabled = value;
				if (m_sceneGizmo != null)
				{
					m_sceneGizmo.gameObject.SetActive(value);
				}
			}
		}

		public RectTransform SceneGizmoTransform => m_sceneGizmoTransform;

		public bool CanOrbit
		{
			get
			{
				return CanRotate;
			}
			set
			{
				CanRotate = value;
			}
		}

		public bool CanRotate
		{
			get
			{
				return m_canRotate;
			}
			set
			{
				m_canRotate = value;
			}
		}

		public bool CanZoom
		{
			get
			{
				return m_canZoom;
			}
			set
			{
				m_canZoom = value;
			}
		}

		public bool ChangeOrthographicSizeOnly
		{
			get
			{
				return m_changeOrthographicSizeOnly;
			}
			set
			{
				m_changeOrthographicSizeOnly = value;
			}
		}

		public bool CanPan
		{
			get
			{
				return m_canPan;
			}
			set
			{
				m_canPan = value;
			}
		}

		public bool CanFreeMove
		{
			get
			{
				return m_canFreeMove;
			}
			set
			{
				m_canFreeMove = value;
			}
		}

		public float FreeMovementSmoothSpeed
		{
			get
			{
				return m_freeMovementSmoothSpeed;
			}
			set
			{
				m_freeMovementSmoothSpeed = value;
			}
		}

		public float FreeRotationSmoothSpeed
		{
			get
			{
				return m_freeRotationSmoothSpeed;
			}
			set
			{
				m_freeRotationSmoothSpeed = value;
			}
		}

		public bool RotationInvertX
		{
			get
			{
				return m_rotationInvertX;
			}
			set
			{
				m_rotationInvertX = value;
			}
		}

		public bool RotationInvertY
		{
			get
			{
				return m_rotationInvertY;
			}
			set
			{
				m_rotationInvertY = value;
			}
		}

		[Obsolete]
		public float ZoomSpeed
		{
			get
			{
				return ZoomSensitivity;
			}
			set
			{
				ZoomSensitivity = value;
			}
		}

		public bool ConstantZoomSpeed
		{
			get
			{
				return m_constantZoomSpeed;
			}
			set
			{
				m_constantZoomSpeed = value;
			}
		}

		public float ZoomSensitivity
		{
			get
			{
				return m_zoomSensitivity;
			}
			set
			{
				m_zoomSensitivity = value;
			}
		}

		public float MoveSensitivity
		{
			get
			{
				return m_moveSensitivity;
			}
			set
			{
				m_moveSensitivity = value;
			}
		}

		public float RotationSensitivity
		{
			get
			{
				return m_rotationSensitivity;
			}
			set
			{
				m_rotationSensitivity = value;
			}
		}

		public float BoundingSphereRadius
		{
			get
			{
				return m_boundingSphereRadius;
			}
			set
			{
				m_boundingSphereRadius = value;
			}
		}

		public float MinOrthoSize
		{
			get
			{
				return m_minOrthoSize;
			}
			set
			{
				m_maxOrthoSize = value;
			}
		}

		public float MaxOrthoSize
		{
			get
			{
				return m_maxOrthoSize;
			}
			set
			{
				m_maxOrthoSize = value;
			}
		}

		public GameObject GameObject => base.gameObject;

		public override Vector3 Pivot
		{
			get
			{
				return base.Pivot;
			}
			set
			{
				base.Pivot = value;
				m_orbitDistance = (Pivot - m_targetPosition).magnitude;
			}
		}

		public override Vector3 CameraPosition
		{
			get
			{
				return base.CameraPosition;
			}
			set
			{
				base.CameraPosition = value;
				Camera camera = Window.Camera;
				Transform transform = camera.transform;
				m_targetPosition = transform.position;
				m_targetRotation = transform.rotation;
				m_orbitDistance = (Pivot - m_targetPosition).magnitude;
				if (camera.orthographic)
				{
					float num = camera.fieldOfView * (MathF.PI / 180f);
					camera.orthographicSize = m_orbitDistance * Mathf.Sin(num / 2f);
					if (camera.orthographicSize < MinOrthoSize)
					{
						camera.orthographicSize = MinOrthoSize;
					}
					if (camera.orthographicSize > MaxOrthoSize)
					{
						camera.orthographicSize = MaxOrthoSize;
					}
				}
			}
		}

		public override bool IsOrthographic
		{
			get
			{
				return base.IsOrthographic;
			}
			set
			{
				if (m_sceneGizmo != null)
				{
					if (m_sceneGizmo.IsOrthographic != value)
					{
						m_sceneGizmo.IsOrthographic = value;
					}
				}
				else
				{
					base.IsOrthographic = value;
				}
			}
		}

		protected Plane DragPlane
		{
			get
			{
				return m_dragPlane;
			}
			set
			{
				m_dragPlane = value;
			}
		}

		protected Vector3 LastMousePosition
		{
			get
			{
				return m_lastMousePosition;
			}
			set
			{
				m_lastMousePosition = value;
			}
		}

		protected bool IsInputLocked => m_lockInput;

		public SceneGizmo SceneGizmo => m_sceneGizmo;

		public override void SetCameraPositionAndPivot(Vector3 position, Vector3 pivot)
		{
			base.SetCameraPositionAndPivot(position, pivot);
			Transform transform = Window.Camera.transform;
			m_targetPosition = transform.position;
			m_targetRotation = transform.rotation;
			m_orbitDistance = (Pivot - m_targetPosition).magnitude;
		}

		protected override void Awake()
		{
			base.Awake();
			Window.IOCContainer.RegisterFallback((IRuntimeSceneComponent)this);
			if (Run.Instance == null)
			{
				GameObject obj = new GameObject("Run");
				obj.transform.SetParent(base.transform, worldPositionStays: false);
				obj.name = "Run";
				obj.AddComponent<Run>();
			}
			if (ViewTexture == null)
			{
				ViewTexture = Resources.Load<Texture2D>("RTH_Eye");
			}
			if (MoveTexture == null)
			{
				MoveTexture = Resources.Load<Texture2D>("RTH_Hand");
			}
			if (FreeMoveTexture == null)
			{
				FreeMoveTexture = Resources.Load<Texture2D>("RTH_FreeMove");
			}
			if (m_sceneGizmo == null)
			{
				m_sceneGizmo = GetComponentInChildren<SceneGizmo>(includeInactive: true);
			}
			if (m_sceneGizmo != null)
			{
				if (m_sceneGizmo.Window == null)
				{
					m_sceneGizmo.Window = Window;
				}
				m_sceneGizmo.OrientationChanging.AddListener(OnSceneGizmoOrientationChanging);
				m_sceneGizmo.OrientationChanged.AddListener(OnSceneGizmoOrientationChanged);
				m_sceneGizmo.ProjectionChanged.AddListener(OnSceneGizmoProjectionChanged);
				m_sceneGizmo.Pivot = base.PivotTransform;
				if (!IsSceneGizmoEnabled)
				{
					m_sceneGizmo.gameObject.SetActive(value: false);
				}
			}
			Transform transform = Window.Camera.transform;
			transform.LookAt(Pivot);
			m_targetRotation = transform.rotation;
			m_targetPosition = transform.position;
			m_orbitDistance = (Pivot - m_targetPosition).magnitude;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Window.IOCContainer.UnregisterFallback((IRuntimeSceneComponent)this);
			if (m_sceneGizmo != null)
			{
				m_sceneGizmo.OrientationChanging.RemoveListener(OnSceneGizmoOrientationChanging);
				m_sceneGizmo.OrientationChanged.RemoveListener(OnSceneGizmoOrientationChanged);
				m_sceneGizmo.ProjectionChanged.RemoveListener(OnSceneGizmoProjectionChanged);
			}
		}

		protected override void Start()
		{
			if (GetComponent<RuntimeSelectionInputBase>() == null)
			{
				base.gameObject.AddComponent<RuntimeSceneInput>();
			}
			base.Start();
		}

		protected virtual void Update()
		{
			if (base.Editor.Tools.AutoFocus && !(base.Editor.Tools.ActiveTool != null) && !(m_autoFocusTransform == null) && !(m_autoFocusTransform.position == Pivot) && (m_focusAnimation == null || !m_focusAnimation.InProgress) && !m_lockInput)
			{
				Vector3 vector = m_autoFocusTransform.position - SecondaryPivot;
				Window.Camera.transform.position += vector;
				base.PivotTransform.position += vector;
				base.SecondaryPivotTransform.position += vector;
			}
			if (!(base.Grid != null))
			{
				return;
			}
			if (IsOrthographic)
			{
				Transform transform = Window.Camera.transform;
				UpdateGridRotation();
				if (m_prevCamPosition != transform.position || m_prevCamRotation != transform.rotation)
				{
					m_prevCamPosition = transform.position;
					m_prevCamRotation = transform.rotation;
				}
				else if (!m_isSceneGizmoOrientationChanging)
				{
					base.Grid.Alpha += Time.unscaledDeltaTime * 5f;
				}
			}
			else if (IsGridCloseToCamera())
			{
				base.Grid.Alpha -= Time.unscaledDeltaTime * 25f;
			}
			else
			{
				base.Grid.Alpha += Time.unscaledDeltaTime * 5f;
			}
		}

		private bool IsGridCloseToCamera()
		{
			return Mathf.Abs(Window.Camera.transform.position.y - base.Grid.transform.position.y) < 0.1f;
		}

		private void UpdateGridRotation()
		{
			Vector3 forward = Window.Camera.transform.forward;
			if (Mathf.Approximately(Mathf.Abs(Vector3.Dot(Vector3.right, forward)), 1f))
			{
				base.Grid.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
			}
			else if (Mathf.Approximately(Mathf.Abs(Vector3.Dot(Vector3.forward, forward)), 1f))
			{
				base.Grid.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			}
			else
			{
				base.Grid.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}

		public void UpdateCursorState(bool isPointerOverEditorArea, bool pan, bool rotate, bool freeMove)
		{
			if (!isPointerOverEditorArea)
			{
				Window.Editor.CursorHelper.ResetCursor(this);
			}
			else if (freeMove && CanFreeMove)
			{
				base.Editor.CursorHelper.SetCursor(this, FreeMoveTexture, Vector2.one * 0.5f, CursorMode.Auto);
			}
			else if (pan && CanPan)
			{
				if (rotate && base.Editor.Tools.Current == RuntimeTool.View)
				{
					base.Editor.CursorHelper.SetCursor(this, ViewTexture, Vector2.one * 0.5f, CursorMode.Auto);
				}
				else
				{
					base.Editor.CursorHelper.SetCursor(this, MoveTexture, Vector2.one * 0.5f, CursorMode.Auto);
				}
			}
			else if (rotate && CanRotate)
			{
				base.Editor.CursorHelper.SetCursor(this, ViewTexture, Vector2.one * 0.5f, CursorMode.Auto);
			}
			else
			{
				base.Editor.CursorHelper.ResetCursor(this);
			}
		}

		[Obsolete]
		public override void Focus()
		{
			Focus(FocusMode.Selected);
		}

		public override void Focus(FocusMode focusMode = FocusMode.Selected)
		{
			if (m_lockInput)
			{
				return;
			}
			m_autoFocusTransform = null;
			Transform[] transforms;
			if (focusMode == FocusMode.Selected || focusMode == FocusMode.Selected)
			{
				if (base.Selection.activeTransform == null || (base.Selection.activeTransform.gameObject.hideFlags & HideFlags.DontSave) != HideFlags.None || base.Selection.activeGameObject.IsPrefab())
				{
					return;
				}
				m_autoFocusTransform = base.Selection.activeTransform;
				transforms = base.Selection.gameObjects.Select((GameObject go) => go.transform).ToArray();
			}
			else
			{
				transforms = (from r in base.Editor.Object.Get(rootsOnly: true).SelectMany((ExposeToEditor e) => e.GetComponentsInChildren<Transform>())
					where r.gameObject.activeInHierarchy
					select r).ToArray();
			}
			Bounds bounds = TransformUtility.CalculateBounds(transforms);
			if (bounds.extents == Vector3.zero)
			{
				bounds.extents = Vector3.one * 0.5f;
			}
			float objSize = Mathf.Max(bounds.extents.y, bounds.extents.x, bounds.extents.z) * 2f;
			Focus(bounds.center, objSize);
			if (focusMode == FocusMode.Selected || focusMode == FocusMode.Selected)
			{
				if (base.Selection.activeTransform != null)
				{
					base.SecondaryPivotTransform.position = base.Selection.activeTransform.position;
				}
			}
			else
			{
				base.SecondaryPivotTransform.position = bounds.center;
			}
		}

		public override void Focus(Vector3 objPosition, float objSize)
		{
			base.PivotTransform.position = objPosition;
			base.SecondaryPivotTransform.position = objPosition;
			float distance;
			if (ChangeOrthographicSizeOnly && IsOrthographic)
			{
				distance = m_orbitDistance;
			}
			else
			{
				float num = Window.Camera.fieldOfView * (MathF.PI / 180f);
				distance = Mathf.Abs(objSize / Mathf.Sin(num / 2f));
			}
			Focus(distance, objSize);
		}

		private void Focus(float distance, float objSize)
		{
			m_focusAnimation = new Vector3AnimationInfo(Window.Camera.transform.position, base.PivotTransform.position - distance * Window.Camera.transform.forward, 0.1f, AnimationInfo<object, Vector3>.EaseOutCubic, delegate(object target, Vector3 value, float t, bool completed)
			{
				if ((bool)Window.Camera)
				{
					Window.Camera.transform.position = value;
					m_targetPosition = value;
				}
			});
			Run.Instance.Animation(m_focusAnimation);
			Run.Instance.Animation(new FloatAnimationInfo(m_orbitDistance, distance, 0.1f, AnimationInfo<object, Vector3>.EaseOutCubic, delegate(object target, float value, float t, bool completed)
			{
				m_orbitDistance = value;
			}));
			Run.Instance.Animation(new FloatAnimationInfo(Window.Camera.orthographicSize, objSize, 0.1f, AnimationInfo<object, Vector3>.EaseOutCubic, delegate(object target, float value, float t, bool completed)
			{
				if ((bool)Window.Camera)
				{
					Window.Camera.orthographicSize = value;
				}
			}));
		}

		public virtual void Zoom(float deltaZ, Quaternion rotation)
		{
			Zoom(deltaZ, rotation, 0.0001f);
		}

		public virtual void Zoom(float deltaZ, Quaternion rotation, float epsilonSq)
		{
			if (m_lockInput)
			{
				return;
			}
			if (!CanZoom)
			{
				deltaZ = 0f;
			}
			Camera camera = Window.Camera;
			if (camera.orthographic)
			{
				camera.orthographicSize -= deltaZ * camera.orthographicSize;
				if (camera.orthographicSize < MinOrthoSize)
				{
					camera.orthographicSize = MinOrthoSize;
				}
				if (camera.orthographicSize > MaxOrthoSize)
				{
					camera.orthographicSize = MaxOrthoSize;
				}
				if (ChangeOrthographicSizeOnly)
				{
					return;
				}
			}
			Vector3 vector = rotation * Vector3.forward * deltaZ;
			if (m_constantZoomSpeed)
			{
				vector *= ZoomSensitivity * 10f;
			}
			else
			{
				vector *= Mathf.Max(ZoomSensitivity * 10f, Mathf.Abs(m_orbitDistance));
			}
			Transform transform = Window.Camera.transform;
			m_orbitDistance -= vector.z;
			vector.z = 0f;
			Vector3 vector2 = new Vector3(0f, 0f, 0f - m_orbitDistance);
			m_targetPosition = transform.TransformVector(vector) + transform.rotation * vector2 + base.PivotTransform.position;
			if (ClampPosition(Pivot, ref m_targetPosition))
			{
				RecalculateOrbitDistance(m_targetPosition);
			}
			if (!MathHelper.Approximately(m_targetPosition, transform.position, epsilonSq))
			{
				transform.position = m_targetPosition;
			}
		}

		public virtual void Orbit(float deltaX, float deltaY, float deltaZ)
		{
			if (!m_lockInput && CanRotate && (deltaX != 0f || deltaY != 0f || deltaZ != 0f))
			{
				if (m_rotationInvertY)
				{
					deltaY = 0f - deltaY;
				}
				if (m_rotationInvertX)
				{
					deltaX = 0f - deltaX;
				}
				deltaX *= RotationSensitivity;
				deltaY *= RotationSensitivity;
				deltaZ *= RotationSensitivity;
				Transform obj = Window.Camera.transform;
				m_targetRotation = Quaternion.Inverse(Quaternion.Euler(deltaY, 0f, 0f) * Quaternion.Inverse(m_targetRotation) * Quaternion.Euler(0f, 0f - deltaX, 0f));
				obj.rotation = m_targetRotation;
				Zoom(deltaZ, Quaternion.identity, 0f);
			}
		}

		public virtual void BeginPan(Vector3 mousePosition)
		{
			if (m_lockInput || !CanPan)
			{
				return;
			}
			m_lastMousePosition = mousePosition;
			if (Physics.Raycast(Window.Pointer, out var hitInfo))
			{
				m_dragPlane = new Plane(-Window.Camera.transform.forward, hitInfo.point);
				return;
			}
			Plane dragPlane = new Plane(Vector3.up, Vector3.zero);
			Ray ray = Window.Pointer;
			Vector3 forward = Window.Camera.transform.forward;
			float num = Mathf.Max(100f, (Pivot - CameraPosition).magnitude);
			float num2 = Mathf.Max(10f, (Pivot - CameraPosition).magnitude);
			if (dragPlane.Raycast(ray, out var enter) && enter < num)
			{
				m_dragPlane = dragPlane;
				m_dragPlane = new Plane(-forward, ray.GetPoint(enter));
			}
			else
			{
				m_dragPlane = new Plane(-forward, Window.Camera.transform.position + forward * num2);
			}
		}

		public virtual void Pan(Vector3 mousePosition)
		{
			if (!m_lockInput && CanPan && GetPointOnDragPlane(mousePosition, out var point) && GetPointOnDragPlane(m_lastMousePosition, out var point2))
			{
				Transform transform = Window.Camera.transform;
				Vector3 vector = point - point2;
				m_lastMousePosition = mousePosition;
				Vector3 position = transform.position - vector;
				ClampPosition(transform.position, ref position);
				transform.position = position;
				Vector3 position2 = base.PivotTransform.position - vector;
				ClampPosition(base.PivotTransform.position, ref position2);
				base.PivotTransform.position = position2;
				Vector3 position3 = base.SecondaryPivotTransform.position - vector;
				ClampPosition(base.SecondaryPivotTransform.position, ref position3);
				base.SecondaryPivotTransform.position = position3;
				m_targetPosition = transform.position;
				RecalculateOrbitDistance(m_targetPosition);
			}
		}

		public virtual void FreeMove(Vector2 rotate, Vector3 move, float forward)
		{
			rotate *= RotationSensitivity;
			forward *= ZoomSensitivity;
			move *= MoveSensitivity;
			if (m_lockInput || !CanFreeMove)
			{
				return;
			}
			Transform transform = Window.Camera.transform;
			if (m_rotationInvertY)
			{
				rotate.y = 0f - rotate.y;
			}
			if (m_rotationInvertX)
			{
				rotate.x = 0f - rotate.x;
			}
			m_targetRotation = Quaternion.Inverse(Quaternion.Euler(rotate.y, 0f, 0f) * Quaternion.Inverse(m_targetRotation) * Quaternion.Euler(0f, 0f - rotate.x, 0f));
			if (m_freeRotationSmoothSpeed <= 0f)
			{
				transform.rotation = m_targetRotation;
			}
			else if (!MathHelper.Approximately(transform.rotation, m_targetRotation))
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, m_targetRotation, m_freeRotationSmoothSpeed * Time.unscaledDeltaTime);
			}
			Vector3 vector = Vector3.zero;
			if (Window.Camera.orthographic)
			{
				if (Mathf.Approximately(move.y, 0f))
				{
					move.y = forward;
				}
				else
				{
					move.y /= 10f;
				}
				if (!Mathf.Approximately(move.y, 0f))
				{
					Vector3 position = transform.position;
					Zoom(move.y, Quaternion.identity);
					vector = transform.position - position;
					transform.position = position;
					move.y = 0f;
				}
			}
			else if (Mathf.Approximately(move.y, 0f))
			{
				move.y = forward * 50f;
			}
			m_targetPosition = m_targetPosition + vector + transform.forward * move.y + transform.right * move.x + transform.up * move.z;
			if (ClampPosition(Pivot, ref m_targetPosition))
			{
				RecalculateOrbitDistance(m_targetPosition);
			}
			if (m_freeMovementSmoothSpeed <= 0f)
			{
				transform.position = m_targetPosition;
			}
			else if (!MathHelper.Approximately(m_targetPosition, transform.position))
			{
				Vector3 position2 = Vector3.Lerp(transform.position, m_targetPosition, m_freeMovementSmoothSpeed * Time.unscaledDeltaTime);
				transform.position = position2;
			}
			Vector3 position3 = transform.position + transform.forward * m_orbitDistance;
			bool num = ClampPosition(transform.position, ref position3);
			base.SecondaryPivotTransform.position += position3 - Pivot;
			base.PivotTransform.position = position3;
			if (num)
			{
				RecalculateOrbitDistance(transform.position);
			}
		}

		private void OnSceneGizmoOrientationChanging()
		{
			m_lockInput = true;
			m_isSceneGizmoOrientationChanging = true;
			if (IsOrthographic && base.Grid != null)
			{
				base.Grid.Alpha = 0f;
			}
		}

		private void OnSceneGizmoOrientationChanged()
		{
			m_lockInput = false;
			m_isSceneGizmoOrientationChanging = false;
			Pivot = Window.Camera.transform.position + Window.Camera.transform.forward * m_orbitDistance;
			SecondaryPivot = Pivot;
			m_targetRotation = Window.Camera.transform.rotation;
			m_targetPosition = Window.Camera.transform.position;
		}

		private void OnSceneGizmoProjectionChanged()
		{
			float num = Window.Camera.fieldOfView * (MathF.PI / 180f);
			float orthographicSize = (Window.Camera.transform.position - Pivot).magnitude * Mathf.Sin(num / 2f);
			Window.Camera.orthographicSize = orthographicSize;
			if (!IsOrthographic && base.Grid != null)
			{
				base.Grid.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				if (IsGridCloseToCamera())
				{
					base.Grid.Alpha = 0f;
				}
			}
		}

		[Obsolete]
		public Vector3 CenterPoint(Vector3[] vectors)
		{
			return TransformUtility.CenterPoint(vectors);
		}

		private bool GetPointOnDragPlane(Vector3 mouse, out Vector3 point)
		{
			Ray ray = Window.Camera.ScreenPointToRay(mouse);
			if (m_dragPlane.Raycast(ray, out var enter))
			{
				point = ray.GetPoint(enter);
				return true;
			}
			point = Vector3.zero;
			return false;
		}

		private bool ClampPosition(Vector3 origin, ref Vector3 position)
		{
			if (position.magnitude <= m_boundingSphereRadius)
			{
				return false;
			}
			Vector3 vector = origin - position;
			if (vector == Vector3.zero)
			{
				position = position.normalized * m_boundingSphereRadius;
			}
			else
			{
				Vector3 normalized = vector.normalized;
				float num = MathHelper.RaySphereIntertsect(origin, normalized, Vector3.zero, m_boundingSphereRadius);
				if (num == -1f)
				{
					position = position.normalized * m_boundingSphereRadius;
				}
				else
				{
					position = origin + normalized * num;
				}
			}
			return true;
		}

		private void RecalculateOrbitDistance(Vector3 position)
		{
			m_orbitDistance = (Pivot - position).magnitude;
		}
	}
}
