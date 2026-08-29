using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(-50)]
	public abstract class BaseHandle : RTEComponent
	{
		protected enum Mode
		{
			XYZ3D = 0,
			XY2D = 1,
			XZ2D = 2,
			YZ2D = 3
		}

		public BaseHandleUnityEvent BeforeDrag = new BaseHandleUnityEvent();

		public BaseHandleUnityEvent Drag = new BaseHandleUnityEvent();

		public BaseHandleUnityEvent Drop = new BaseHandleUnityEvent();

		private IRTECamera m_rteCamera;

		private Vector3 m_prevScale;

		private Vector3 m_prevCamPosition;

		private Quaternion m_prevCamRotation;

		private bool m_prevCamOrthographic;

		private float m_prevCamOrthographicsSize;

		private Rect m_prevCamRect;

		private bool m_refreshOnCameraChanged = true;

		public bool HightlightOnHover = true;

		public bool EnableUndo = true;

		public RuntimeHandlesHitTester HitTester;

		public RuntimeHandlesComponent Appearance;

		public BaseHandleModel Model;

		private RuntimeHandleAxis m_selectedAxis;

		private bool m_isDragging;

		private Plane m_dragPlane;

		private bool m_unitSnapping;

		private LockObject m_sharedLockObject;

		private Transform[] m_activeTargets;

		private Transform[] m_activeRealTargets;

		private Transform[] m_realTargets;

		private Transform[] m_commonCenter;

		private Transform[] m_commonCenterTarget;

		private BaseHandleInput m_input;

		private static List<BaseHandle> m_allHandles = new List<BaseHandle>();

		[SerializeField]
		private Transform[] m_targets;

		private static bool s_commonCenterStandardRTEBehaviour = true;

		protected IRTECamera RTECamera => m_rteCamera;

		protected bool RefreshOnCameraChanged
		{
			get
			{
				return m_refreshOnCameraChanged;
			}
			set
			{
				m_refreshOnCameraChanged = value;
			}
		}

		protected virtual Plane DragPlane
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

		public virtual bool IsDragging => m_isDragging;

		public virtual RuntimeTool Tool => RuntimeTool.Custom;

		protected virtual Quaternion Rotation
		{
			get
			{
				if (ActiveRealTargets == null || ActiveRealTargets.Length == 0 || ActiveRealTargets[0] == null)
				{
					return Quaternion.identity;
				}
				if (PivotRotation != RuntimePivotRotation.Local)
				{
					return Quaternion.identity;
				}
				return ActiveRealTargets[0].rotation;
			}
		}

		public virtual RuntimeHandleAxis SelectedAxis
		{
			get
			{
				return m_selectedAxis;
			}
			set
			{
				if (m_selectedAxis != value)
				{
					m_selectedAxis = value;
					if (Model != null)
					{
						Model.Select(SelectedAxis);
					}
					else if (m_rteCamera != null)
					{
						m_rteCamera.RefreshCommandBuffer();
					}
				}
			}
		}

		public virtual bool UnitSnapping
		{
			get
			{
				return m_unitSnapping;
			}
			set
			{
				m_unitSnapping = value;
			}
		}

		public virtual bool SnapToGrid { get; set; }

		public virtual float SizeOfGrid { get; set; }

		protected float EffectiveGridUnitSize { get; private set; }

		protected virtual float CurrentGridUnitSize => 0f;

		protected virtual LockObject SharedLockObject
		{
			get
			{
				return m_sharedLockObject;
			}
			set
			{
				m_sharedLockObject = value;
				if (m_sharedLockObject != null && base.Editor != null && base.Editor.Tools.LockAxes != null)
				{
					m_sharedLockObject.SetGlobalLock(base.Editor.Tools.LockAxes);
				}
				if (Model != null && !Model.gameObject.IsPrefab())
				{
					Model.SetLock(m_sharedLockObject);
				}
			}
		}

		public virtual LockObject LockObject
		{
			get
			{
				return new LockObject(SharedLockObject);
			}
			set
			{
				if (value == null)
				{
					SharedLockObject = value;
				}
				else
				{
					SharedLockObject = new LockObject(value);
				}
			}
		}

		protected virtual Mode CurrentMode { get; set; }

		public virtual Vector3 Position
		{
			get
			{
				return base.transform.position;
			}
			set
			{
				base.transform.position = value;
			}
		}

		public virtual Transform[] ActiveTargets => m_activeTargets;

		protected virtual Transform[] ActiveRealTargets => m_activeRealTargets;

		public virtual Transform[] RealTargets
		{
			get
			{
				if (m_realTargets == null)
				{
					return Targets;
				}
				return m_realTargets;
			}
		}

		protected static List<BaseHandle> AllHandles => m_allHandles;

		public virtual Transform[] Targets
		{
			get
			{
				return Targets_Internal;
			}
			set
			{
				DestroyCommonCenter(destroyImmediate: true);
				m_realTargets = value;
				GetActiveRealTargets();
				Targets_Internal = value;
				if (Targets_Internal != null && Targets_Internal.Length != 0 && UseCommonCenter())
				{
					Vector3 commonCenterPosition = GetCommonCenterPosition();
					m_commonCenter = new Transform[1];
					m_commonCenter[0] = new GameObject
					{
						name = "CommonCenter"
					}.transform;
					m_commonCenter[0].SetParent(base.transform.parent, worldPositionStays: true);
					m_commonCenter[0].position = commonCenterPosition;
					m_commonCenter[0].rotation = Rotation;
					m_commonCenterTarget = new Transform[m_realTargets.Length];
					for (int i = 0; i < m_commonCenterTarget.Length; i++)
					{
						GameObject gameObject = new GameObject
						{
							name = "ActiveTarget " + m_realTargets[i].name
						};
						gameObject.transform.SetParent(m_commonCenter[0]);
						gameObject.transform.position = m_realTargets[i].position;
						gameObject.transform.rotation = m_realTargets[i].rotation;
						gameObject.transform.localScale = m_realTargets[i].localScale;
						m_commonCenterTarget[i] = gameObject.transform;
					}
					LockObject sharedLockObject = SharedLockObject;
					Targets_Internal = m_commonCenter;
					SharedLockObject = sharedLockObject;
				}
			}
		}

		protected virtual Transform[] Targets_Internal
		{
			get
			{
				return m_targets;
			}
			set
			{
				m_targets = value;
				if (m_targets == null)
				{
					SharedLockObject = LockAxes.Eval(null);
					m_activeTargets = null;
					return;
				}
				m_targets = m_targets.Where((Transform t) => t != null && (t.hideFlags & HideFlags.DontSave) == 0).ToArray();
				HashSet<Transform> hashSet = new HashSet<Transform>();
				for (int num = 0; num < m_targets.Length; num++)
				{
					if (m_targets[num] != null && !hashSet.Contains(m_targets[num]))
					{
						hashSet.Add(m_targets[num]);
					}
				}
				m_targets = hashSet.ToArray();
				if (m_targets.Length == 0)
				{
					SharedLockObject = LockAxes.Eval(new LockAxes[0]);
					m_activeTargets = new Transform[0];
					return;
				}
				if (m_targets.Length == 1)
				{
					m_activeTargets = new Transform[1] { m_targets[0] };
				}
				for (int num2 = 0; num2 < m_targets.Length; num2++)
				{
					Transform transform = m_targets[num2];
					Transform parent = transform.parent;
					while (parent != null)
					{
						if (hashSet.Contains(parent))
						{
							hashSet.Remove(transform);
							break;
						}
						parent = parent.parent;
					}
				}
				m_activeTargets = hashSet.ToArray();
				LockObject lockObject = LockAxes.Eval((from t in m_activeTargets
					where t.GetComponent<LockAxes>() != null
					select t.GetComponent<LockAxes>()).ToArray());
				if (m_activeTargets.Any((Transform target) => target.gameObject.isStatic))
				{
					bool flag = (lockObject.PositionZ = true);
					bool positionX = (lockObject.PositionY = flag);
					lockObject.PositionX = positionX;
					flag = (lockObject.RotationZ = true);
					positionX = (lockObject.RotationY = flag);
					lockObject.RotationX = positionX;
					flag = (lockObject.ScaleZ = true);
					positionX = (lockObject.ScaleY = flag);
					lockObject.ScaleX = positionX;
					lockObject.RotationScreen = true;
					lockObject.RotationFree = true;
				}
				SharedLockObject = lockObject;
				if (m_activeTargets != null && m_activeTargets.Length != 0)
				{
					base.transform.position = m_activeTargets[0].position;
				}
				if (base.IsStarted && Model != null)
				{
					SyncModelTransform();
				}
			}
		}

		public Transform Target
		{
			get
			{
				if (Targets == null || Targets.Length == 0)
				{
					return null;
				}
				return Targets[0];
			}
		}

		protected virtual RuntimePivotMode PivotMode
		{
			get
			{
				LockObject sharedLockObject = SharedLockObject;
				if (sharedLockObject != null && sharedLockObject.PivotMode.HasValue)
				{
					return sharedLockObject.PivotMode.Value;
				}
				return base.Editor.Tools.PivotMode;
			}
		}

		protected virtual Vector3 CustomPivot => base.Editor.Tools.CustomPivotPosition;

		protected virtual RuntimePivotRotation PivotRotation
		{
			get
			{
				LockObject sharedLockObject = SharedLockObject;
				if (sharedLockObject != null && sharedLockObject.PivotRotation.HasValue)
				{
					return sharedLockObject.PivotRotation.Value;
				}
				return base.Editor.Tools.PivotRotation;
			}
		}

		protected void UpdateCurrentMode()
		{
			if (!IsDragging)
			{
				Camera camera = Window.Camera;
				Vector3 position = camera.transform.position;
				Vector3 lhs = (camera.orthographic ? camera.transform.forward : (Position - position).normalized);
				Quaternion rotation = Rotation;
				if (Mathf.Abs(Vector3.Dot(lhs, rotation * Vector3.forward)) >= 0.98f)
				{
					CurrentMode = Mode.XY2D;
				}
				else if (Mathf.Abs(Vector3.Dot(lhs, rotation * Vector3.up)) >= 0.98f)
				{
					CurrentMode = Mode.XZ2D;
				}
				else if (Mathf.Abs(Vector3.Dot(lhs, rotation * Vector3.right)) >= 0.98f)
				{
					CurrentMode = Mode.YZ2D;
				}
				else
				{
					CurrentMode = Mode.XYZ3D;
				}
			}
		}

		private void GetActiveRealTargets()
		{
			if (m_realTargets == null)
			{
				m_activeRealTargets = null;
				return;
			}
			m_realTargets = m_realTargets.Where((Transform t) => t != null && (t.hideFlags & HideFlags.DontSave) == 0).ToArray();
			HashSet<Transform> hashSet = new HashSet<Transform>();
			for (int num = 0; num < m_realTargets.Length; num++)
			{
				if (m_realTargets[num] != null && !hashSet.Contains(m_realTargets[num]))
				{
					hashSet.Add(m_realTargets[num]);
				}
			}
			m_realTargets = hashSet.ToArray();
			if (m_realTargets.Length == 0)
			{
				m_activeRealTargets = new Transform[0];
				return;
			}
			if (m_realTargets.Length == 1)
			{
				m_activeRealTargets = new Transform[1] { m_realTargets[0] };
			}
			for (int num2 = 0; num2 < m_realTargets.Length; num2++)
			{
				Transform transform = m_realTargets[num2];
				Transform parent = transform.parent;
				while (parent != null)
				{
					if (hashSet.Contains(parent))
					{
						hashSet.Remove(transform);
						break;
					}
					parent = parent.parent;
				}
			}
			m_activeRealTargets = hashSet.ToArray();
		}

		protected virtual Vector3 GetCenterPosition(Transform target)
		{
			return target.GetCenter();
		}

		protected virtual bool UseCommonCenter()
		{
			if (s_commonCenterStandardRTEBehaviour)
			{
				if (PivotMode != RuntimePivotMode.Center || RealTargets.Length <= 1)
				{
					if (PivotMode == RuntimePivotMode.Custom)
					{
						return ActiveTargets.Length != 0;
					}
					return false;
				}
				return true;
			}
			if (PivotMode == RuntimePivotMode.Center)
			{
				return ActiveTargets.Length != 0;
			}
			return false;
		}

		protected virtual Vector3 GetCommonCenterPosition()
		{
			if (PivotMode == RuntimePivotMode.Custom)
			{
				return CustomPivot;
			}
			if (s_commonCenterStandardRTEBehaviour)
			{
				return TransformUtility.GetCommonCenter(Targets_Internal);
			}
			if (!UseCommonCenter())
			{
				return TransformUtility.GetCommonCenter(Targets_Internal);
			}
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < ActiveTargets.Length; i++)
			{
				Transform[] componentsInChildren = ActiveTargets[i].GetComponentsInChildren<Transform>();
				list.AddRange(componentsInChildren);
			}
			return TransformUtility.CalculateBounds(list.ToArray(), base.Editor.CameraLayerSettings.RaycastMask, includeInactive: false).center;
		}

		public virtual void Refresh()
		{
			if (m_commonCenter == null || m_commonCenter.Length == 0 || RealTargets == null || RealTargets.Length == 0)
			{
				return;
			}
			Vector3 centerPosition = GetCenterPosition(RealTargets[0]);
			for (int i = 1; i < RealTargets.Length; i++)
			{
				Transform target = RealTargets[i];
				centerPosition += GetCenterPosition(target);
			}
			centerPosition /= (float)RealTargets.Length;
			m_commonCenter[0].position = centerPosition;
			m_commonCenter[0].rotation = Rotation;
			for (int j = 0; j < m_allHandles.Count; j++)
			{
				BaseHandle baseHandle = m_allHandles[j];
				if (baseHandle.Editor == base.Editor && baseHandle.gameObject.activeSelf && baseHandle.m_commonCenter != null && baseHandle.m_commonCenter.Length != 0)
				{
					baseHandle.m_commonCenter[0].position = m_commonCenter[0].position;
					baseHandle.m_commonCenter[0].rotation = m_commonCenter[0].rotation;
					baseHandle.m_commonCenter[0].localScale = m_commonCenter[0].localScale;
				}
				base.transform.position = m_commonCenter[0].position;
			}
			if (Model != null)
			{
				SyncModelTransform();
			}
		}

		protected override void Awake()
		{
			base.Awake();
			m_allHandles.Add(this);
			RuntimeHandlesComponent.InitializeIfRequired(ref Appearance);
			RuntimeHandlesHitTester.InitializeIfRequired(Window, ref HitTester);
			if (m_targets != null && m_targets.Length != 0)
			{
				LockObject sharedLockObject = SharedLockObject;
				if (m_commonCenter == null || m_commonCenter.Length == 0 || m_commonCenter[0] != m_targets[0])
				{
					Targets = m_targets;
				}
				SharedLockObject = sharedLockObject;
			}
			if (Targets == null || Targets.Length == 0)
			{
				LockObject sharedLockObject2 = SharedLockObject;
				Targets = new Transform[1] { base.transform };
				SharedLockObject = sharedLockObject2;
			}
			if (Model != null)
			{
				bool activeSelf = Model.gameObject.activeSelf;
				Model.gameObject.SetActive(value: false);
				Model.Appearance = Appearance;
				BaseHandleModel baseHandleModel = UnityEngine.Object.Instantiate(Model, base.transform.parent);
				baseHandleModel.name = Model.name;
				baseHandleModel.Appearance = Appearance;
				baseHandleModel.Colors = Model.Colors;
				baseHandleModel.Window = Window;
				Model.gameObject.SetActive(activeSelf);
				if (base.enabled)
				{
					baseHandleModel.gameObject.SetActive(value: true);
					Model = baseHandleModel;
					Model.SetLock(SharedLockObject);
				}
				else
				{
					Model = baseHandleModel;
				}
				Model.ModelScale = Appearance.HandleScale;
				Model.SelectionMargin = Appearance.SelectionMargin;
			}
		}

		protected override void Start()
		{
			m_input = GetComponent<BaseHandleInput>();
			if (m_input == null || m_input.Handle != this)
			{
				m_input = base.gameObject.AddComponent<BaseHandleInput>();
				m_input.Handle = this;
			}
			IRTEGraphicsLayer iRTEGraphicsLayer = Window.IOCContainer.Resolve<IRTEGraphicsLayer>();
			if (iRTEGraphicsLayer != null)
			{
				m_rteCamera = iRTEGraphicsLayer.Camera;
			}
			if (m_rteCamera == null && Window.Camera != null)
			{
				IRTEGraphics iRTEGraphics = IOC.Resolve<IRTEGraphics>();
				if (iRTEGraphics != null)
				{
					m_rteCamera = iRTEGraphics.GetOrCreateCamera(Window.Camera, CameraEvent.AfterImageEffectsOpaque, meshesCache: false, renderersCache: false);
				}
				if (m_rteCamera == null)
				{
					m_rteCamera = Window.Camera.gameObject.AddComponent<RTECamera>();
					m_rteCamera.Event = CameraEvent.AfterImageEffectsOpaque;
				}
			}
			if (Model == null && m_rteCamera != null)
			{
				m_prevScale = base.transform.localScale;
				m_prevCamPosition = m_rteCamera.Camera.transform.position;
				m_prevCamRotation = m_rteCamera.Camera.transform.rotation;
				m_prevCamOrthographic = m_rteCamera.Camera.orthographic;
				m_prevCamOrthographicsSize = m_rteCamera.Camera.orthographicSize;
				m_prevCamRect = m_rteCamera.Camera.rect;
				m_rteCamera.CommandBufferRefresh += OnCommandBufferRefresh;
				m_rteCamera.RefreshCommandBuffer();
			}
			OnStartOverride();
		}

		protected virtual void OnEnable()
		{
			base.Editor.Tools.PivotRotationChanged += OnPivotRotationChanged;
			base.Editor.Tools.PivotModeChanged += OnPivotModeChanged;
			base.Editor.Tools.ToolChanged += OnRuntimeToolChanged;
			base.Editor.Tools.LockAxesChanged += OnLockAxesChanged;
			base.Editor.Undo.UndoCompleted += OnUndoCompleted;
			base.Editor.Undo.RedoCompleted += OnRedoCompleted;
			OnEnableOverride();
			if (HitTester != null)
			{
				HitTester.Add(this);
			}
			if (m_input != null)
			{
				m_input.enabled = true;
			}
			if (Model != null)
			{
				SyncModelTransform();
				Model.gameObject.SetActive(value: true);
				if (!Model.gameObject.IsPrefab())
				{
					Model.SetLock(SharedLockObject);
				}
			}
			else if (m_rteCamera != null)
			{
				m_rteCamera.CommandBufferRefresh += OnCommandBufferRefresh;
				m_rteCamera.RefreshCommandBuffer();
			}
		}

		protected virtual void OnDisable()
		{
			if (m_rteCamera != null)
			{
				m_rteCamera.CommandBufferRefresh -= OnCommandBufferRefresh;
				m_rteCamera.RefreshCommandBuffer();
			}
			if (HitTester != null)
			{
				HitTester.Remove(this);
			}
			if (base.Editor != null)
			{
				base.Editor.Tools.PivotModeChanged -= OnPivotModeChanged;
				base.Editor.Tools.PivotRotationChanged -= OnPivotRotationChanged;
				base.Editor.Tools.ToolChanged -= OnRuntimeToolChanged;
				base.Editor.Tools.LockAxesChanged -= OnLockAxesChanged;
				base.Editor.Undo.UndoCompleted -= OnUndoCompleted;
				base.Editor.Undo.RedoCompleted -= OnRedoCompleted;
			}
			DestroyCommonCenter(destroyImmediate: false);
			if (Model != null)
			{
				Model.gameObject.SetActive(value: false);
			}
			if (base.Editor != null && base.Editor.Tools != null && base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
			if (m_input != null)
			{
				m_input.enabled = false;
			}
			OnDisableOverride();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_allHandles.Remove(this);
			if (m_input != null && m_input.Handle == this)
			{
				UnityEngine.Object.Destroy(m_input);
			}
			if (m_rteCamera != null)
			{
				m_rteCamera.CommandBufferRefresh -= OnCommandBufferRefresh;
				m_rteCamera.RefreshCommandBuffer();
			}
			DestroyCommonCenter(destroyImmediate: false);
			if (Model != null && Model.gameObject != null && !Model.gameObject.IsPrefab())
			{
				UnityEngine.Object.Destroy(Model.gameObject);
			}
			if (base.Editor != null && base.Editor.Tools != null && base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
		}

		protected virtual void OnTransformParentChanged()
		{
			OnTransformParentChangedOverride();
		}

		protected virtual void OnTransformParentChangedOverride()
		{
			if (Model != null && !Model.gameObject.IsPrefab())
			{
				Model.transform.SetParent(base.transform.parent, worldPositionStays: true);
				if (base.IsStarted)
				{
					SyncModelTransform();
				}
			}
		}

		protected override void OnWindowDeactivated()
		{
			base.OnWindowDeactivated();
			EndDrag();
			if (base.Editor != null && base.Editor.Tools != null && base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
		}

		private void DestroyCommonCenter(bool destroyImmediate)
		{
			if (m_commonCenter != null)
			{
				for (int i = 0; i < m_commonCenter.Length; i++)
				{
					if ((bool)m_commonCenter[i])
					{
						if (destroyImmediate)
						{
							UnityEngine.Object.DestroyImmediate(m_commonCenter[i].gameObject);
						}
						else
						{
							UnityEngine.Object.Destroy(m_commonCenter[i].gameObject);
						}
					}
				}
			}
			if (m_commonCenterTarget != null)
			{
				for (int j = 0; j < m_commonCenterTarget.Length; j++)
				{
					if ((bool)m_commonCenterTarget[j])
					{
						if (destroyImmediate)
						{
							UnityEngine.Object.DestroyImmediate(m_commonCenterTarget[j].gameObject);
						}
						else
						{
							UnityEngine.Object.Destroy(m_commonCenterTarget[j].gameObject);
						}
					}
				}
			}
			m_commonCenter = null;
			m_commonCenterTarget = null;
		}

		public void doUpdate()
		{
			if (Model != null)
			{
				Model.ModelScale = Appearance.HandleScale;
				Model.SelectionMargin = Appearance.SelectionMargin;
			}
			if (m_isDragging)
			{
				if (base.Editor.Tools.IsViewing)
				{
					m_isDragging = false;
				}
				else
				{
					if (m_unitSnapping)
					{
						EffectiveGridUnitSize = CurrentGridUnitSize;
					}
					else
					{
						EffectiveGridUnitSize = 0f;
					}
					OnDrag();
				}
			}
			else if (!Window.IsPointerOver)
			{
				SelectedAxis = RuntimeHandleAxis.None;
			}
			UpdateOverride();
			if (m_isDragging)
			{
				if (m_commonCenterTarget != null && m_realTargets != null && UseCommonCenter())
				{
					for (int i = 0; i < m_commonCenterTarget.Length; i++)
					{
						Transform transform = m_commonCenterTarget[i];
						Transform obj = m_realTargets[i];
						obj.transform.position = transform.position;
						obj.transform.rotation = transform.rotation;
						obj.transform.localScale = transform.lossyScale;
					}
				}
				if (Drag != null)
				{
					Drag.Invoke(this);
				}
				if (m_commonCenter != null && m_commonCenter.Length != 0)
				{
					for (int j = 0; j < m_allHandles.Count; j++)
					{
						BaseHandle baseHandle = m_allHandles[j];
						if (baseHandle.Editor == base.Editor && baseHandle.gameObject.activeSelf)
						{
							baseHandle.m_commonCenter[0].position = m_commonCenter[0].position;
							baseHandle.m_commonCenter[0].rotation = m_commonCenter[0].rotation;
							baseHandle.m_commonCenter[0].localScale = m_commonCenter[0].localScale;
						}
					}
				}
			}
			if (Window != null)
			{
				if (Model != null)
				{
					SyncModelTransform();
				}
				else
				{
					TryRefreshCommandBuffer();
				}
			}
		}

		protected virtual void UpdateOverride()
		{
			Transform transform = ((Targets != null && Targets.Length != 0 && Targets[0] != null) ? Targets[0] : null);
			if (transform != null && (transform.position != base.transform.position || transform.rotation != base.transform.rotation || transform.localScale != m_prevScale))
			{
				m_prevScale = base.transform.localScale;
				if (IsDragging)
				{
					Vector3 vector = base.transform.position - Targets[0].position;
					for (int i = 0; i < ActiveTargets.Length; i++)
					{
						if (ActiveTargets[i] != null)
						{
							ActiveTargets[i].position += vector;
						}
					}
				}
				else
				{
					base.transform.position = transform.position;
					base.transform.rotation = transform.rotation;
				}
				TryRefreshCommandBuffer();
			}
			TrySelectAxis();
		}

		protected bool TrySelectAxis()
		{
			RuntimeHandleAxis selectedAxis = SelectedAxis;
			if (base.Editor.Tools.IsViewing)
			{
				SelectedAxis = RuntimeHandleAxis.None;
			}
			else if (base.IsWindowActive && Window.IsPointerOver && HightlightOnHover && !IsDragging)
			{
				SelectedAxis = HitTester.GetSelectedAxis(this);
			}
			return SelectedAxis != selectedAxis;
		}

		protected bool TryRefreshCommandBuffer()
		{
			if (Model == null && RTECamera != null)
			{
				RTECamera.RefreshCommandBuffer();
				return true;
			}
			return false;
		}

		protected virtual void LateUpdate()
		{
			if (!m_isDragging && base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
			if (!(Window != null))
			{
				return;
			}
			if (Model != null)
			{
				SyncScale();
			}
			else if (m_refreshOnCameraChanged)
			{
				Camera camera = m_rteCamera.Camera;
				if (m_prevCamPosition != camera.transform.position || m_prevCamRotation != camera.transform.rotation || m_prevCamOrthographic != camera.orthographic || m_prevCamOrthographicsSize != camera.orthographicSize || m_prevCamRect != camera.rect)
				{
					m_prevCamPosition = camera.transform.position;
					m_prevCamRotation = camera.transform.rotation;
					m_prevCamOrthographic = camera.orthographic;
					m_prevCamOrthographicsSize = camera.orthographicSize;
					m_prevCamRect = camera.rect;
					TryRefreshCommandBuffer();
				}
			}
		}

		protected virtual void SyncModelTransform()
		{
			Model.transform.position = Position;
			Model.transform.rotation = Rotation;
			SyncScale();
		}

		private void SyncScale()
		{
			float screenScale = RuntimeHandlesComponent.GetScreenScale(base.transform.position, Window.Camera);
			if (!float.IsInfinity(screenScale) && !float.IsNaN(screenScale))
			{
				screenScale = Mathf.Max(0f, screenScale);
				Vector3 a = (Appearance.InvertZAxis ? (new Vector3(1f, 1f, -1f) * screenScale) : (Vector3.one * screenScale));
				Vector3 lossyScale = base.transform.lossyScale;
				lossyScale.x = 1f / Mathf.Max(1E-05f, lossyScale.x);
				lossyScale.y = 1f / Mathf.Max(1E-05f, lossyScale.y);
				lossyScale.z = 1f / Mathf.Max(1E-05f, lossyScale.z);
				Vector3 localScale = Model.transform.localScale;
				Vector3 vector = Vector3.Scale(a, lossyScale);
				Model.transform.localScale = vector;
				if (localScale == Vector3.zero && localScale != vector)
				{
					Model.UpdateModel();
				}
			}
		}

		public virtual void BeginDrag()
		{
			if (base.Editor.Tools.IsViewing || !base.IsWindowActive || base.Editor.Tools.ActiveTool != null || (Window.Camera != null && !Window.IsPointerOver))
			{
				return;
			}
			m_isDragging = OnBeginDrag();
			if (m_isDragging)
			{
				if (BeforeDrag != null)
				{
					BeforeDrag.Invoke(this);
				}
				base.Editor.Tools.ActiveTool = this;
				BeginRecordTransform();
			}
			else if (base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
		}

		public virtual void EndDrag()
		{
			if (m_isDragging)
			{
				OnDrop();
				EndRecordTransform();
				m_isDragging = false;
				TryRefreshCommandBuffer();
				if (Drop != null)
				{
					Drop.Invoke(this);
				}
			}
		}

		protected virtual bool OnBeginDrag()
		{
			if (!base.IsWindowActive)
			{
				return false;
			}
			if (Target == null)
			{
				return false;
			}
			SelectedAxis = HitTester.GetSelectedAxis(this);
			return true;
		}

		protected virtual void OnDrag()
		{
		}

		protected virtual void OnDrop()
		{
		}

		protected virtual void OnRuntimeToolChanged()
		{
			EndDrag();
		}

		protected virtual void OnPivotModeChanged()
		{
			if (RealTargets != null)
			{
				Targets = RealTargets;
			}
			if (PivotMode != RuntimePivotMode.Center && PivotMode != RuntimePivotMode.Custom)
			{
				m_realTargets = null;
			}
			if (Target != null)
			{
				base.transform.position = Target.position;
			}
			TryRefreshCommandBuffer();
		}

		protected virtual void OnPivotRotationChanged()
		{
			TryRefreshCommandBuffer();
			if (m_commonCenter != null && m_commonCenter.Length != 0)
			{
				Targets = RealTargets;
			}
		}

		protected virtual void OnLockAxesChanged()
		{
			if (SharedLockObject != null)
			{
				LockObject sharedLockObject = SharedLockObject;
				SharedLockObject = sharedLockObject;
			}
			if (Model != null)
			{
				if (!Model.gameObject.IsPrefab())
				{
					Model.SetLock(SharedLockObject);
				}
			}
			else
			{
				TryRefreshCommandBuffer();
			}
		}

		protected virtual void BeginRecordTransform()
		{
			if (!EnableUndo)
			{
				return;
			}
			base.Editor.Undo.BeginRecord();
			for (int i = 0; i < m_activeRealTargets.Length; i++)
			{
				Transform transform = m_activeRealTargets[i];
				if (transform != null)
				{
					base.Editor.Undo.BeginRecordTransform(transform);
				}
			}
			base.Editor.Undo.EndRecord();
		}

		protected virtual void EndRecordTransform()
		{
			if (!EnableUndo)
			{
				return;
			}
			base.Editor.Undo.BeginRecord();
			for (int i = 0; i < m_activeRealTargets.Length; i++)
			{
				Transform transform = m_activeRealTargets[i];
				if (transform != null)
				{
					base.Editor.Undo.EndRecordTransform(transform);
				}
			}
			base.Editor.Undo.EndRecord();
		}

		protected virtual void OnRedoCompleted()
		{
			if ((PivotMode == RuntimePivotMode.Center || PivotMode == RuntimePivotMode.Custom) && m_realTargets != null && (m_realTargets.Length != 1 || m_realTargets[0] != base.transform))
			{
				Targets = m_realTargets;
			}
		}

		protected virtual void OnUndoCompleted()
		{
			if ((PivotMode == RuntimePivotMode.Center || PivotMode == RuntimePivotMode.Custom) && m_realTargets != null && (m_realTargets.Length != 1 || m_realTargets[0] != base.transform))
			{
				Targets = m_realTargets;
			}
		}

		public virtual RuntimeHandleAxis HitTest(out float distance)
		{
			distance = float.PositiveInfinity;
			return RuntimeHandleAxis.None;
		}

		protected virtual Plane GetDragPlane(Matrix4x4 matrix, Vector3 axis)
		{
			return new Plane(matrix.MultiplyVector(axis).normalized, matrix.MultiplyPoint(Vector3.zero));
		}

		protected virtual Plane GetDragPlane(Vector3 axis)
		{
			return new Plane(((!Mathf.Approximately(Mathf.Abs(Vector3.Dot(Window.Camera.transform.forward, Rotation * axis)), 1f)) ? Window.Camera.cameraToWorldMatrix.MultiplyVector(Vector3.forward) : (Window.Camera.transform.position - base.transform.position)).normalized, base.transform.position);
		}

		protected virtual bool GetPointOnDragPlane(Ray ray, out Vector3 point)
		{
			return GetPointOnDragPlane(m_dragPlane, ray, out point);
		}

		protected virtual bool GetPointOnDragPlane(Plane dragPlane, Ray ray, out Vector3 point)
		{
			if (dragPlane.Raycast(ray, out var enter))
			{
				point = ray.GetPoint(enter);
				return true;
			}
			point = Vector3.zero;
			return false;
		}

		protected Vector3 GetGridOffset(float gridSize, Vector3 position)
		{
			Vector3 vector = position;
			position.x = Mathf.Round(position.x / gridSize) * gridSize;
			position.y = Mathf.Round(position.y / gridSize) * gridSize;
			position.z = Mathf.Round(position.z / gridSize) * gridSize;
			return position - vector;
		}

		protected virtual void SetModel(BaseHandleModel model)
		{
			model.Appearance = Appearance;
			model.Window = Window;
			model.ModelScale = Appearance.HandleScale;
			model.SelectionMargin = Appearance.SelectionMargin;
			model.gameObject.SetActive(base.gameObject.activeSelf && base.enabled);
			model.SetLock(SharedLockObject);
			Model = model;
		}

		protected virtual void OnCommandBufferRefresh(IRTECamera camera)
		{
			if (Target != null)
			{
				RefreshCommandBuffer(camera);
			}
		}

		protected virtual void RefreshCommandBuffer(IRTECamera camera)
		{
		}

		[Obsolete("Override Start method instead")]
		protected virtual void OnStartOverride()
		{
		}

		[Obsolete("Use OnEnable instead")]
		protected virtual void OnEnableOverride()
		{
		}

		[Obsolete("Use OnDisable instead")]
		protected virtual void OnDisableOverride()
		{
		}
	}
}
