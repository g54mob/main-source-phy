using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(-55)]
	public class RuntimeSelectionComponent : RTEComponent, IRuntimeSelectionComponent, IScenePivot
	{
		private enum SelectionMode
		{
			UseColliders = 0,
			UseRenderers = 1,
			UseRenderersBeforeColliders = 2
		}

		[SerializeField]
		private OutlineManager m_outlineManager;

		[SerializeField]
		private PositionHandle m_positionHandle;

		[SerializeField]
		private RotationHandle m_rotationHandle;

		[SerializeField]
		private ScaleHandle m_scaleHandle;

		[SerializeField]
		private RectTool m_rectTool;

		[SerializeField]
		private BaseHandle m_customHandle;

		[SerializeField]
		private BoxSelection m_boxSelection;

		[SerializeField]
		private SceneGrid m_grid;

		[SerializeField]
		private Transform m_pivot;

		[SerializeField]
		private Transform m_secondaryPivot;

		private bool m_isPositionHandleEnabled = true;

		private bool m_isRotationHandleEnabled = true;

		private bool m_isScaleHandleEnabled = true;

		private bool m_isRectToolEnabled = true;

		private bool m_isBoxSelectionEnabled = true;

		private bool m_isSelectionVisible = true;

		[SerializeField]
		private bool m_canSelect = true;

		[SerializeField]
		private bool m_canSelectAll = true;

		[SerializeField]
		private bool m_canSelectExposedOnly = true;

		private SelectionPicker m_selectionPicker;

		[SerializeField]
		private SelectionMode m_selectionMode;

		private bool m_isGridVisible = true;

		private bool m_isGridEnabled;

		private bool m_gridZTest = true;

		private IRuntimeSelection m_selectionOverride;

		protected readonly List<RaycastResult> m_uiRaycastResults = new List<RaycastResult>();

		protected HashSet<GameObject> m_uiBoxcastResults = new HashSet<GameObject>();

		private Ray m_prevPointer;

		private bool m_wasUnitSnappingEnabled;

		protected Transform PivotTransform => m_pivot;

		protected Transform SecondaryPivotTransform => m_secondaryPivot;

		public virtual bool IsOrthographic
		{
			get
			{
				return Window.Camera.orthographic;
			}
			set
			{
				Window.Camera.orthographic = value;
			}
		}

		public virtual float OrthographicSize
		{
			get
			{
				return Window.Camera.orthographicSize;
			}
			set
			{
				Window.Camera.orthographicSize = value;
			}
		}

		public virtual Vector3 CameraPosition
		{
			get
			{
				return Window.Camera.transform.position;
			}
			set
			{
				Window.Camera.transform.position = value;
				Window.Camera.transform.LookAt(Pivot);
			}
		}

		public virtual Vector3 Pivot
		{
			get
			{
				return m_pivot.transform.position;
			}
			set
			{
				m_pivot.transform.position = value;
				Window.Camera.transform.LookAt(Pivot);
			}
		}

		public virtual Vector3 SecondaryPivot
		{
			get
			{
				return m_secondaryPivot.transform.position;
			}
			set
			{
				m_secondaryPivot.transform.position = value;
			}
		}

		public BoxSelection BoxSelection => m_boxSelection;

		public PositionHandle PositionHandle => m_positionHandle;

		public RotationHandle RotationHandle => m_rotationHandle;

		public ScaleHandle ScaleHandle => m_scaleHandle;

		public RectTool RectTool => m_rectTool;

		public BaseHandle CustomHandle
		{
			get
			{
				return m_customHandle;
			}
			set
			{
				if (m_customHandle == value)
				{
					return;
				}
				if (m_customHandle != null)
				{
					m_customHandle.BeforeDrag.RemoveListener(OnBeforeDrag);
					m_customHandle.Drop.RemoveListener(OnDrop);
				}
				m_customHandle = value;
				if (!(m_customHandle != null))
				{
					return;
				}
				m_customHandle.Window = Window;
				m_customHandle.gameObject.SetActive(value: false);
				if (m_customHandle.BeforeDrag == null)
				{
					m_customHandle.BeforeDrag = new BaseHandleUnityEvent();
				}
				m_customHandle.BeforeDrag.AddListener(OnBeforeDrag);
				if (m_customHandle.Drop == null)
				{
					m_customHandle.Drop = new BaseHandleUnityEvent();
				}
				m_customHandle.Drop.AddListener(OnDrop);
				if (base.Editor.Tools.Current == RuntimeTool.Custom)
				{
					Transform[] handleTargets = GetHandleTargets();
					if (handleTargets != null && handleTargets.Length != 0)
					{
						m_customHandle.Targets = handleTargets;
						m_customHandle.gameObject.SetActive(value: true);
					}
					else
					{
						m_customHandle.gameObject.SetActive(value: false);
					}
				}
			}
		}

		public SceneGrid Grid => m_grid;

		public bool IsPositionHandleEnabled
		{
			get
			{
				if (m_isPositionHandleEnabled)
				{
					return m_positionHandle != null;
				}
				return false;
			}
			set
			{
				m_isPositionHandleEnabled = value;
				if (m_positionHandle != null)
				{
					if (value && base.Editor.Tools.Current == RuntimeTool.Move)
					{
						m_positionHandle.Targets = GetHandleTargets();
					}
					m_positionHandle.gameObject.SetActive(value && base.Editor.Tools.Current == RuntimeTool.Move && m_positionHandle.Target != null);
				}
			}
		}

		public bool IsRotationHandleEnabled
		{
			get
			{
				if (m_isRotationHandleEnabled)
				{
					return m_rotationHandle != null;
				}
				return false;
			}
			set
			{
				m_isRotationHandleEnabled = value;
				if (m_rotationHandle != null)
				{
					if (value && base.Editor.Tools.Current == RuntimeTool.Rotate)
					{
						m_rotationHandle.Targets = GetHandleTargets();
					}
					m_rotationHandle.gameObject.SetActive(value && base.Editor.Tools.Current == RuntimeTool.Rotate && m_rotationHandle.Target != null);
				}
			}
		}

		public bool IsScaleHandleEnabled
		{
			get
			{
				if (m_isScaleHandleEnabled)
				{
					return m_scaleHandle != null;
				}
				return false;
			}
			set
			{
				m_isScaleHandleEnabled = value;
				if (m_scaleHandle != null)
				{
					if (value && base.Editor.Tools.Current == RuntimeTool.Scale)
					{
						m_scaleHandle.Targets = GetHandleTargets();
					}
					m_scaleHandle.gameObject.SetActive(value && base.Editor.Tools.Current == RuntimeTool.Scale && m_scaleHandle.Target != null);
				}
			}
		}

		public bool IsRectToolEnabled
		{
			get
			{
				if (m_isRectToolEnabled)
				{
					return m_rectTool != null;
				}
				return false;
			}
			set
			{
				m_isRectToolEnabled = value;
				if (m_rectTool != null)
				{
					if (value && base.Editor.Tools.Current == RuntimeTool.Rect)
					{
						m_rectTool.Targets = GetHandleTargets();
					}
					m_rectTool.gameObject.SetActive(value && base.Editor.Tools.Current == RuntimeTool.Rect && m_rectTool.Target != null);
				}
			}
		}

		public bool IsBoxSelectionEnabled
		{
			get
			{
				if (m_isBoxSelectionEnabled)
				{
					return m_boxSelection != null;
				}
				return false;
			}
			set
			{
				m_isBoxSelectionEnabled = value;
				if (m_boxSelection != null)
				{
					m_boxSelection.enabled = m_isBoxSelectionEnabled;
				}
			}
		}

		public bool IsSelectionVisible
		{
			get
			{
				return m_isSelectionVisible;
			}
			set
			{
				m_isSelectionVisible = value;
			}
		}

		public bool CanSelect
		{
			get
			{
				return m_canSelect;
			}
			set
			{
				m_canSelect = value;
			}
		}

		public bool CanSelectAll
		{
			get
			{
				return m_canSelectAll;
			}
			set
			{
				m_canSelectAll = value;
			}
		}

		public bool IsGridVisible
		{
			get
			{
				return m_isGridVisible;
			}
			set
			{
				if (m_isGridVisible != value)
				{
					m_isGridVisible = value;
					ApplyIsGridVisible();
				}
			}
		}

		public bool IsGridEnabled
		{
			get
			{
				return m_isGridEnabled;
			}
			set
			{
				if (m_isGridEnabled != value)
				{
					m_isGridEnabled = value;
					ApplyIsGridEnabled();
				}
			}
		}

		public bool GridZTest
		{
			get
			{
				return m_gridZTest;
			}
			set
			{
				if (m_gridZTest != value)
				{
					m_gridZTest = value;
					ApplyGridZTest();
				}
			}
		}

		public float SizeOfGrid
		{
			get
			{
				if (m_grid == null)
				{
					return 0.5f;
				}
				return m_grid.SizeOfGrid;
			}
			set
			{
				if (!(m_grid == null))
				{
					m_grid.SizeOfGrid = value;
					ApplySizeOfGrid();
				}
			}
		}

		public IRuntimeSelection Selection
		{
			get
			{
				if (m_selectionOverride != null)
				{
					return m_selectionOverride;
				}
				return base.Editor.Selection;
			}
			set
			{
				if (m_selectionOverride != value)
				{
					if (m_selectionOverride != null)
					{
						m_selectionOverride.SelectionChanged -= OnRuntimeSelectionChanged;
					}
					m_selectionOverride = value;
					if (m_selectionOverride == base.Editor.Selection)
					{
						m_selectionOverride = null;
					}
					if (m_selectionOverride != null)
					{
						OnRuntimeSelectionChanged(base.Editor.Selection.objects);
						m_selectionOverride.SelectionChanged += OnRuntimeSelectionChanged;
					}
					if (m_outlineManager != null)
					{
						m_outlineManager.Selection = m_selectionOverride;
					}
				}
			}
		}

		public event EventHandler<RuntimeSelectionFilteringArgs> Filtering;

		public event EventHandler<RuntimeSelectionChangingArgs> SelectionChanging;

		public event EventHandler SelectionChanged;

		public virtual void SetCameraPositionAndPivot(Vector3 position, Vector3 pivot)
		{
			Window.Camera.transform.position = position;
			Window.Camera.transform.LookAt(pivot);
			m_pivot.transform.position = pivot;
		}

		private void ApplyIsGridVisible()
		{
			if (m_grid != null)
			{
				m_grid.gameObject.SetActive(m_isGridVisible);
			}
		}

		private void ApplyIsGridEnabled()
		{
			if (m_positionHandle != null)
			{
				m_positionHandle.SnapToGrid = IsGridEnabled;
			}
			if (m_scaleHandle != null)
			{
				m_scaleHandle.SnapToGrid = IsGridEnabled;
			}
			if (m_customHandle != null)
			{
				m_customHandle.SnapToGrid = IsGridEnabled;
			}
		}

		private void ApplyGridZTest()
		{
			if (m_grid != null)
			{
				m_grid.ZTest = m_gridZTest;
			}
		}

		private void ApplySizeOfGrid()
		{
			if (m_positionHandle != null)
			{
				m_positionHandle.SizeOfGrid = SizeOfGrid;
			}
			if (m_scaleHandle != null)
			{
				m_scaleHandle.SizeOfGrid = SizeOfGrid;
			}
			if (m_rectTool != null)
			{
				m_rectTool.SizeOfGrid = SizeOfGrid;
			}
			if (m_customHandle != null)
			{
				m_customHandle.SizeOfGrid = SizeOfGrid;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			Window.IOCContainer.RegisterFallback((IScenePivot)this);
			Window.IOCContainer.RegisterFallback((IRuntimeSelectionComponent)this);
			m_selectionPicker = new SelectionPicker(Window, delegate(FilteringArgs args)
			{
				OnSelectionFiltering(this, args);
			});
			if (m_outlineManager == null)
			{
				m_outlineManager = GetComponentInChildren<OutlineManager>(includeInactive: true);
				if (m_outlineManager != null)
				{
					m_outlineManager.Camera = Window.Camera;
				}
			}
			if (m_boxSelection == null)
			{
				m_boxSelection = GetComponentInChildren<BoxSelection>(includeInactive: true);
			}
			if (m_positionHandle == null)
			{
				m_positionHandle = GetComponentInChildren<PositionHandle>(includeInactive: true);
			}
			if (m_rotationHandle == null)
			{
				m_rotationHandle = GetComponentInChildren<RotationHandle>(includeInactive: true);
			}
			if (m_scaleHandle == null)
			{
				m_scaleHandle = GetComponentInChildren<ScaleHandle>(includeInactive: true);
			}
			if (m_rectTool == null)
			{
				m_rectTool = GetComponentInChildren<RectTool>(includeInactive: true);
			}
			if (m_grid == null)
			{
				m_grid = GetComponentInChildren<SceneGrid>(includeInactive: true);
			}
			if (m_boxSelection != null)
			{
				if (m_boxSelection.Window == null)
				{
					m_boxSelection.Window = Window;
				}
				m_boxSelection.Filtering += OnSelectionFiltering;
			}
			if (m_positionHandle != null)
			{
				if (m_positionHandle.Window == null)
				{
					m_positionHandle.Window = Window;
				}
				m_positionHandle.gameObject.SetActive(value: true);
				m_positionHandle.gameObject.SetActive(value: false);
				m_positionHandle.BeforeDrag.AddListener(OnBeforeDrag);
				m_positionHandle.Drop.AddListener(OnDrop);
			}
			if (m_rotationHandle != null)
			{
				if (m_rotationHandle.Window == null)
				{
					m_rotationHandle.Window = Window;
				}
				m_rotationHandle.gameObject.SetActive(value: true);
				m_rotationHandle.gameObject.SetActive(value: false);
				m_rotationHandle.BeforeDrag.AddListener(OnBeforeDrag);
				m_rotationHandle.Drop.AddListener(OnDrop);
			}
			if (m_scaleHandle != null)
			{
				if (m_scaleHandle.Window == null)
				{
					m_scaleHandle.Window = Window;
				}
				m_scaleHandle.gameObject.SetActive(value: true);
				m_scaleHandle.gameObject.SetActive(value: false);
				m_scaleHandle.BeforeDrag.AddListener(OnBeforeDrag);
				m_scaleHandle.Drop.AddListener(OnDrop);
			}
			if (m_rectTool != null)
			{
				if (m_rectTool.Window == null)
				{
					m_rectTool.Window = Window;
				}
				m_rectTool.gameObject.SetActive(value: true);
				m_rectTool.gameObject.SetActive(value: false);
				m_rectTool.BeforeDrag.AddListener(OnBeforeDrag);
				m_rectTool.Drop.AddListener(OnDrop);
			}
			if (m_grid != null && m_grid.Window == null)
			{
				m_grid.Window = Window;
			}
			base.Editor.Selection.SelectionChanged += OnRuntimeEditorSelectionChanged;
			base.Editor.Tools.ToolChanged += OnRuntimeToolChanged;
			if (m_pivot == null)
			{
				GameObject gameObject = new GameObject("Pivot");
				gameObject.transform.SetParent(base.transform, worldPositionStays: true);
				gameObject.transform.position = Vector3.zero;
				m_pivot = gameObject.transform;
			}
			if (m_secondaryPivot == null)
			{
				GameObject gameObject2 = new GameObject("SecondaryPivot");
				gameObject2.transform.SetParent(base.transform, worldPositionStays: true);
				gameObject2.transform.position = Vector3.zero;
				m_secondaryPivot = gameObject2.transform;
			}
			ApplySizeOfGrid();
			ApplyIsGridEnabled();
			ApplyIsGridVisible();
			ApplyGridZTest();
			OnRuntimeEditorSelectionChanged(null);
		}

		protected override void Start()
		{
			if (GetComponent<RuntimeSelectionInputBase>() == null)
			{
				base.gameObject.AddComponent<RuntimeSelectionInput>();
			}
			base.Start();
			if (m_positionHandle != null && !m_positionHandle.gameObject.activeSelf)
			{
				m_positionHandle.gameObject.SetActive(value: true);
				m_positionHandle.gameObject.SetActive(value: false);
			}
			if (m_rotationHandle != null && !m_rotationHandle.gameObject.activeSelf)
			{
				m_rotationHandle.gameObject.SetActive(value: true);
				m_rotationHandle.gameObject.SetActive(value: false);
			}
			if (m_scaleHandle != null && !m_scaleHandle.gameObject.activeSelf)
			{
				m_scaleHandle.gameObject.SetActive(value: true);
				m_scaleHandle.gameObject.SetActive(value: false);
			}
			if (m_rectTool != null && !m_rectTool.gameObject.activeSelf)
			{
				m_rectTool.gameObject.SetActive(value: true);
				m_rectTool.gameObject.SetActive(value: false);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Window.IOCContainer.UnregisterFallback((IScenePivot)this);
			Window.IOCContainer.UnregisterFallback((IRuntimeSelectionComponent)this);
			if (m_boxSelection != null)
			{
				m_boxSelection.Filtering -= OnSelectionFiltering;
			}
			if (base.Editor != null)
			{
				base.Editor.Tools.ToolChanged -= OnRuntimeToolChanged;
				base.Editor.Selection.SelectionChanged -= OnRuntimeEditorSelectionChanged;
			}
			if (m_positionHandle != null)
			{
				m_positionHandle.BeforeDrag.RemoveListener(OnBeforeDrag);
				m_positionHandle.Drop.RemoveListener(OnDrop);
			}
			if (m_rotationHandle != null)
			{
				m_rotationHandle.BeforeDrag.RemoveListener(OnBeforeDrag);
				m_rotationHandle.Drop.RemoveListener(OnDrop);
			}
			if (m_scaleHandle != null)
			{
				m_scaleHandle.BeforeDrag.RemoveListener(OnBeforeDrag);
				m_scaleHandle.Drop.RemoveListener(OnDrop);
			}
			if (m_rectTool != null)
			{
				m_rectTool.BeforeDrag.RemoveListener(OnBeforeDrag);
				m_rectTool.Drop.RemoveListener(OnDrop);
			}
			if (m_customHandle != null)
			{
				m_customHandle.BeforeDrag.RemoveListener(OnBeforeDrag);
				m_customHandle.Drop.RemoveListener(OnDrop);
			}
			if (m_selectionOverride != null)
			{
				m_selectionOverride.SelectionChanged -= OnRuntimeSelectionChanged;
				m_selectionOverride = null;
			}
			m_selectionPicker = null;
		}

		protected virtual IEnumerable<BaseRaycaster> FindRaycasters()
		{
			return from raycaster in UnityObjectExt.FindObjectsByType<BaseRaycaster>()
				where raycaster.GetComponentInParent<IRTE>() == null
				select raycaster;
		}

		protected virtual IList<RaycastResult> RaycastUIObjects()
		{
			m_uiRaycastResults.Clear();
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			foreach (BaseRaycaster item in FindRaycasters())
			{
				Canvas component = item.GetComponent<Canvas>();
				Camera worldCamera = component.worldCamera;
				component.worldCamera = Window.Camera;
				if (item is GraphicRaycaster)
				{
					GraphicRaycaster.BlockingObjects blockingObjects = GraphicRaycaster.BlockingObjects.None;
					GraphicRaycaster obj = (GraphicRaycaster)item;
					blockingObjects = obj.blockingObjects;
					obj.blockingObjects = GraphicRaycaster.BlockingObjects.All;
					pointerEventData.position = Window.Pointer.ScreenPoint;
					item.Raycast(pointerEventData, m_uiRaycastResults);
					obj.blockingObjects = blockingObjects;
				}
				else
				{
					item.Raycast(pointerEventData, m_uiRaycastResults);
				}
				component.worldCamera = worldCamera;
			}
			m_uiRaycastResults.Sort(RaycastComparer);
			return m_uiRaycastResults;
		}

		protected virtual IEnumerable<GameObject> BoxcastUIObjects()
		{
			m_uiBoxcastResults.Clear();
			Bounds selectionBounds = m_boxSelection.SelectionBounds;
			Vector3[] array = new Vector3[4];
			foreach (BaseRaycaster item in FindRaycasters())
			{
				CanvasRenderer[] componentsInChildren = item.GetComponentsInChildren<CanvasRenderer>();
				foreach (CanvasRenderer canvasRenderer in componentsInChildren)
				{
					((RectTransform)canvasRenderer.transform).GetWorldCorners(array);
					for (int j = 0; j < 4; j++)
					{
						Vector3 point = Window.Camera.WorldToScreenPoint(array[j]);
						point.z = 0f;
						if (!selectionBounds.Contains(point))
						{
							continue;
						}
						if (CanSelectObject(canvasRenderer.gameObject))
						{
							ExposeToEditor componentInParent = canvasRenderer.GetComponentInParent<ExposeToEditor>();
							if (componentInParent != null)
							{
								m_uiBoxcastResults.Add(componentInParent.gameObject);
							}
						}
						break;
					}
				}
			}
			return m_uiBoxcastResults;
		}

		private static int RaycastComparer(RaycastResult lhs, RaycastResult rhs)
		{
			if (lhs.module != rhs.module)
			{
				if (lhs.module.eventCamera != null && rhs.module.eventCamera != null && lhs.module.eventCamera.depth != rhs.module.eventCamera.depth)
				{
					if (lhs.module.eventCamera.depth < rhs.module.eventCamera.depth)
					{
						return 1;
					}
					if (lhs.module.eventCamera.depth == rhs.module.eventCamera.depth)
					{
						return 0;
					}
					return -1;
				}
				if (lhs.module.sortOrderPriority != rhs.module.sortOrderPriority)
				{
					return rhs.module.sortOrderPriority.CompareTo(lhs.module.sortOrderPriority);
				}
				if (lhs.module.renderOrderPriority != rhs.module.renderOrderPriority)
				{
					return rhs.module.renderOrderPriority.CompareTo(lhs.module.renderOrderPriority);
				}
			}
			if (lhs.sortingLayer != rhs.sortingLayer)
			{
				int layerValueFromID = SortingLayer.GetLayerValueFromID(rhs.sortingLayer);
				int layerValueFromID2 = SortingLayer.GetLayerValueFromID(lhs.sortingLayer);
				return layerValueFromID.CompareTo(layerValueFromID2);
			}
			if (lhs.sortingOrder != rhs.sortingOrder)
			{
				return rhs.sortingOrder.CompareTo(lhs.sortingOrder);
			}
			if (lhs.depth != rhs.depth)
			{
				return rhs.depth.CompareTo(lhs.depth);
			}
			if (lhs.distance != rhs.distance)
			{
				return lhs.distance.CompareTo(rhs.distance);
			}
			return lhs.index.CompareTo(rhs.index);
		}

		protected virtual RaycastHit[] Raycast3DObjects()
		{
			return (from hit in Physics.RaycastAll(Window.Pointer, float.MaxValue)
				where CanSelectObject(hit.collider.gameObject)
				orderby GetDepth(hit.transform)
				select hit).ToArray();
		}

		protected virtual Renderer[] PickRenderers()
		{
			return (from hit in m_selectionPicker.Pick()
				where CanSelectObject(hit.gameObject)
				orderby GetDepth(hit.transform)
				select hit).ToArray();
		}

		public virtual void SelectGO(bool multiselect, bool allowUnselect)
		{
			if (!CanSelect || (m_boxSelection != null && m_boxSelection.IsThresholdPassed))
			{
				return;
			}
			IList<RaycastResult> list = RaycastUIObjects();
			if (list.Count > 0)
			{
				ProcessUIObjects(multiselect, allowUnselect, list);
			}
			else if (m_selectionMode == SelectionMode.UseRenderers)
			{
				Renderer[] renderers = PickRenderers();
				ProcessRenderers(multiselect, allowUnselect, renderers);
			}
			else if (m_selectionMode == SelectionMode.UseRenderersBeforeColliders)
			{
				Renderer[] array = PickRenderers();
				if (array.Length != 0)
				{
					ProcessRenderers(multiselect, allowUnselect, array);
					return;
				}
				RaycastHit[] hits = Raycast3DObjects();
				ProcessHits(multiselect, allowUnselect, hits);
			}
			else
			{
				RaycastHit[] hits2 = Raycast3DObjects();
				ProcessHits(multiselect, allowUnselect, hits2);
			}
		}

		private void ProcessUIObjects(bool multiselect, bool allowUnselect, IList<RaycastResult> raycastResults)
		{
			SelectGO(multiselect, allowUnselect, raycastResults, (RaycastResult raycastResult) => raycastResult.gameObject);
		}

		private void ProcessHits(bool multiselect, bool allowUnselect, RaycastHit[] hits)
		{
			hits = ((hits.Length == 0) ? new RaycastHit[0] : FilterHits(hits));
			if (hits.Length != 1 || !(hits[0].collider is TerrainCollider) || !TrySelectRenderer(multiselect, allowUnselect))
			{
				SelectGO(multiselect, allowUnselect, hits, (RaycastHit hit) => hit.collider.gameObject);
			}
		}

		private void ProcessRenderers(bool multiselect, bool allowUnselect, Renderer[] renderers)
		{
			SelectGO(multiselect, allowUnselect, renderers, (Renderer renderer) => renderer.gameObject);
		}

		private bool TrySelectRenderer(bool multiselect, bool allowUnselect)
		{
			Renderer[] array = m_selectionPicker.Pick();
			if (array.Length == 0)
			{
				return false;
			}
			GameObject nextGo = GetNextGo(array, (Renderer r) => r.gameObject);
			if (CanSelectObject(nextGo) && GetFilteredSelection(multiselect, allowUnselect, nextGo, out var filteredSelection) && filteredSelection != null && filteredSelection.Length != 0)
			{
				UpdateSelection(multiselect, filteredSelection);
				return true;
			}
			return false;
		}

		private RaycastHit[] FilterHits(RaycastHit[] hits)
		{
			IEnumerable<RaycastHit> enumerable = hits.OrderBy((RaycastHit hit) => hit.distance);
			if (this.Filtering != null)
			{
				RuntimeSelectionFilteringArgs runtimeSelectionFilteringArgs = new RuntimeSelectionFilteringArgs(enumerable);
				this.Filtering(this, runtimeSelectionFilteringArgs);
				enumerable = runtimeSelectionFilteringArgs.Hits;
			}
			RaycastHit closestHit = enumerable.FirstOrDefault();
			return enumerable.Where((RaycastHit h) => IsReachable(h.transform, closestHit.transform)).ToArray();
		}

		private void SelectGO<T>(bool multiselect, bool allowUnselect, IList<T> hits, Func<T, GameObject> getGameObject)
		{
			if (hits.Count > 0)
			{
				GameObject nextGo = GetNextGo(hits, getGameObject);
				if (CanSelectObject(nextGo))
				{
					SelectGO(multiselect, allowUnselect, nextGo);
				}
				else if (!multiselect)
				{
					TryToClearSelection();
				}
			}
			else if (!multiselect)
			{
				TryToClearSelection();
			}
		}

		private void SelectGO(bool multiselect, bool allowUnselect, GameObject hitGO)
		{
			if (GetFilteredSelection(multiselect, allowUnselect, hitGO, out var filteredSelection))
			{
				UpdateSelection(multiselect, filteredSelection);
			}
		}

		private bool GetFilteredSelection(bool multiselect, bool allowUnselect, GameObject hitGO, out UnityEngine.Object[] filteredSelection)
		{
			if (multiselect)
			{
				IList<UnityEngine.Object> list = ((Selection.objects == null) ? new List<UnityEngine.Object>() : Selection.objects.ToList());
				if (list.Contains(hitGO))
				{
					list.Remove(hitGO);
					if (!allowUnselect)
					{
						list.Insert(0, hitGO);
					}
				}
				else
				{
					list.Insert(0, hitGO);
				}
				UnityEngine.Object[] selected = list.ToArray();
				if (RaiseSelectionChanging(selected, out filteredSelection) && filteredSelection.Length != 0)
				{
					filteredSelection = filteredSelection.OrderByDescending((UnityEngine.Object o) => o == hitGO).ToArray();
					return true;
				}
			}
			else
			{
				UnityEngine.Object[] selected2 = new GameObject[1] { hitGO };
				if (RaiseSelectionChanging(selected2, out filteredSelection) && filteredSelection.Length != 0)
				{
					return true;
				}
			}
			return false;
		}

		private void UpdateSelection(bool multiselect, UnityEngine.Object[] selection)
		{
			if (selection == null)
			{
				Selection.objects = null;
			}
			else if (multiselect)
			{
				if (base.Editor.Undo.Enabled)
				{
					base.Editor.Undo.Select(Selection, selection, selection.FirstOrDefault());
				}
				else
				{
					Selection.Select(selection.FirstOrDefault(), selection);
				}
			}
			else
			{
				Selection.objects = selection;
			}
			RaiseSelectionChanged();
		}

		private int GetDepth(Transform tr)
		{
			int num = 0;
			while (tr.parent != null)
			{
				num++;
				tr = tr.parent;
			}
			return num;
		}

		private bool IsReachable(Transform t1, Transform t2)
		{
			Transform transform = t1;
			while (transform != null)
			{
				if (transform == t2)
				{
					return true;
				}
				transform = transform.parent;
			}
			Transform transform2 = t2;
			while (transform2 != null)
			{
				if (transform2 == t1)
				{
					return true;
				}
				transform2 = transform2.parent;
			}
			return false;
		}

		private GameObject GetNextGo<T>(IList<T> hits, Func<T, GameObject> getGameObject)
		{
			int nextIndex = GetNextIndex(hits, getGameObject);
			GameObject gameObject = getGameObject(hits[nextIndex]);
			ExposeToEditor componentInParent = gameObject.GetComponentInParent<ExposeToEditor>();
			if (!(componentInParent != null))
			{
				return gameObject;
			}
			return componentInParent.gameObject;
		}

		private int GetNextIndex<T>(IList<T> hits, Func<T, GameObject> getGameObject)
		{
			int num = -1;
			if (hits == null || hits.Count == 0)
			{
				return num;
			}
			Ray prevPointer = Window.Pointer;
			if (m_prevPointer.origin == prevPointer.origin && m_prevPointer.direction == prevPointer.direction && Selection.activeGameObject != null)
			{
				for (int i = 0; i < hits.Count; i++)
				{
					if (Selection.IsSelected(getGameObject(hits[i])))
					{
						num = i;
					}
				}
			}
			m_prevPointer = prevPointer;
			num++;
			return num % hits.Count;
		}

		private void TryToClearSelection()
		{
			if (RaiseSelectionChanging(new UnityEngine.Object[0], out var filteredSelection))
			{
				if (filteredSelection.Length == 0)
				{
					Selection.activeObject = null;
				}
				else
				{
					Selection.objects = filteredSelection;
				}
				RaiseSelectionChanged();
			}
		}

		private bool RaiseSelectionChanging(UnityEngine.Object[] selected, out UnityEngine.Object[] filteredSelection)
		{
			if (base.Editor.Tools.SelectionMode == Battlehub.RTCommon.SelectionMode.Root)
			{
				selected = RuntimeSelectionUtil.GetRoots(selected);
			}
			if (this.SelectionChanging != null)
			{
				RuntimeSelectionChangingArgs runtimeSelectionChangingArgs = new RuntimeSelectionChangingArgs(selected);
				this.SelectionChanging(this, runtimeSelectionChangingArgs);
				filteredSelection = runtimeSelectionChangingArgs.Selected.ToArray();
				return !runtimeSelectionChangingArgs.Cancel;
			}
			filteredSelection = selected;
			return true;
		}

		private void RaiseSelectionChanged()
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, EventArgs.Empty);
			}
		}

		public virtual void SnapToGrid()
		{
			GameObject[] gameObjects = Selection.gameObjects;
			if (gameObjects != null && gameObjects.Length != 0)
			{
				Transform transform = gameObjects[0].transform;
				Vector3 position = transform.position;
				if ((double)SizeOfGrid < 0.01)
				{
					SizeOfGrid = 0.01f;
				}
				position.x = Mathf.Round(position.x / SizeOfGrid) * SizeOfGrid;
				position.y = Mathf.Round(position.y / SizeOfGrid) * SizeOfGrid;
				position.z = Mathf.Round(position.z / SizeOfGrid) * SizeOfGrid;
				Vector3 vector = position - transform.position;
				base.Editor.Undo.BeginRecord();
				for (int i = 0; i < gameObjects.Length; i++)
				{
					base.Editor.Undo.BeginRecordTransform(gameObjects[i].transform);
					gameObjects[i].transform.position += vector;
					base.Editor.Undo.EndRecordTransform(gameObjects[i].transform);
				}
				base.Editor.Undo.EndRecord();
				if (m_rectTool != null && base.Editor.Tools.Current == RuntimeTool.Rect)
				{
					m_rectTool.RecalculateBoundsAndRebuild();
				}
			}
		}

		public virtual void SelectAll()
		{
			if (!CanSelect || !CanSelectAll)
			{
				return;
			}
			UnityEngine.Object[] array = (from exposed in base.Editor.Object.Get(rootsOnly: false)
				select exposed.gameObject).ToArray();
			UnityEngine.Object[] array2 = array;
			UnityEngine.Object[] filteredSelection = array2;
			if (RaiseSelectionChanging(array2, out filteredSelection))
			{
				if (filteredSelection.Length == 0)
				{
					Selection.objects = null;
				}
				else
				{
					Selection.objects = array2;
				}
				RaiseSelectionChanged();
			}
		}

		private void OnRuntimeToolChanged()
		{
			bool flag = Selection.activeTransform != null;
			if (m_positionHandle != null)
			{
				if (flag && base.Editor.Tools.Current == RuntimeTool.Move && IsPositionHandleEnabled)
				{
					m_positionHandle.transform.position = Selection.activeTransform.position;
					m_positionHandle.Targets = GetHandleTargets();
					m_positionHandle.gameObject.SetActive(m_positionHandle.Targets.Length != 0);
				}
				else
				{
					m_positionHandle.gameObject.SetActive(value: false);
				}
			}
			if (m_rotationHandle != null)
			{
				if (flag && base.Editor.Tools.Current == RuntimeTool.Rotate && IsRotationHandleEnabled)
				{
					m_rotationHandle.transform.position = Selection.activeTransform.position;
					m_rotationHandle.Targets = GetHandleTargets();
					m_rotationHandle.gameObject.SetActive(m_rotationHandle.Targets.Length != 0);
				}
				else
				{
					m_rotationHandle.gameObject.SetActive(value: false);
				}
			}
			if (m_scaleHandle != null)
			{
				if (flag && base.Editor.Tools.Current == RuntimeTool.Scale && IsScaleHandleEnabled)
				{
					m_scaleHandle.transform.position = Selection.activeTransform.position;
					m_scaleHandle.Targets = GetHandleTargets();
					m_scaleHandle.gameObject.SetActive(m_scaleHandle.Targets.Length != 0);
				}
				else
				{
					m_scaleHandle.gameObject.SetActive(value: false);
				}
			}
			if (m_rectTool != null)
			{
				if (flag && base.Editor.Tools.Current == RuntimeTool.Rect && IsRectToolEnabled)
				{
					m_rectTool.transform.position = Selection.activeTransform.position;
					m_rectTool.Targets = GetHandleTargets();
					m_rectTool.gameObject.SetActive(m_rectTool.Targets.Length != 0);
				}
				else
				{
					m_rectTool.gameObject.SetActive(value: false);
				}
			}
			if (m_customHandle != null)
			{
				if (flag && base.Editor.Tools.Current == RuntimeTool.Custom)
				{
					m_customHandle.transform.position = Selection.activeTransform.position;
					m_customHandle.Targets = GetHandleTargets();
					m_customHandle.gameObject.SetActive(m_customHandle.Targets.Length != 0);
				}
				else
				{
					m_customHandle.gameObject.SetActive(value: false);
				}
			}
		}

		private void OnSelectionFiltering(object sender, FilteringArgs e)
		{
			if (e.Object == null)
			{
				e.Cancel = true;
			}
			if (!e.Object.GetComponent<ExposeToEditor>() && m_canSelectExposedOnly)
			{
				e.Cancel = true;
			}
		}

		public void BoxSelect(GameObject[] gameObjects, bool multiselect)
		{
			if (!CanSelect)
			{
				return;
			}
			IEnumerable<GameObject> first = BoxcastUIObjects();
			UnityEngine.Object[] selected = first.Union(gameObjects).ToArray();
			if (!RaiseSelectionChanging(selected, out var filteredSelection))
			{
				return;
			}
			if (multiselect)
			{
				if (filteredSelection.Length != 0)
				{
					if (Selection.objects == null)
					{
						Selection.objects = filteredSelection;
					}
					else
					{
						HashSet<UnityEngine.Object> hashSet = new HashSet<UnityEngine.Object>();
						selected = Selection.objects;
						foreach (UnityEngine.Object item in selected)
						{
							hashSet.Add(item);
						}
						selected = filteredSelection;
						foreach (UnityEngine.Object item2 in selected)
						{
							hashSet.Add(item2);
						}
						Selection.objects = hashSet.ToArray();
					}
				}
			}
			else if (filteredSelection.Length == 0)
			{
				Selection.objects = null;
			}
			else
			{
				Selection.objects = filteredSelection;
			}
			RaiseSelectionChanged();
		}

		private void OnRuntimeEditorSelectionChanged(UnityEngine.Object[] unselected)
		{
			HandleRuntimeSelectionChange(base.Editor.Selection, unselected);
			if (base.Editor.Selection == Selection)
			{
				UpdateHandlesState();
			}
		}

		private void OnRuntimeSelectionChanged(UnityEngine.Object[] unselected)
		{
			HandleRuntimeSelectionChange(m_selectionOverride, unselected);
			UpdateHandlesState();
		}

		private void UpdateHandlesState()
		{
			if (Selection.activeGameObject == null || Selection.activeGameObject.IsPrefab())
			{
				SetHandlesActive(isActive: false);
				return;
			}
			SetHandlesActive(isActive: false);
			OnRuntimeToolChanged();
		}

		private void HandleRuntimeSelectionChange(IRuntimeSelection selection, UnityEngine.Object[] unselected)
		{
			if (!IsSelectionVisible)
			{
				return;
			}
			if (unselected != null)
			{
				for (int i = 0; i < unselected.Length; i++)
				{
					GameObject gameObject = unselected[i] as GameObject;
					if (gameObject != null)
					{
						ExposeToEditor component = gameObject.GetComponent<ExposeToEditor>();
						if ((bool)component && component.Unselected != null)
						{
							component.Unselected.Invoke(component);
						}
					}
				}
			}
			GameObject[] gameObjects = selection.gameObjects;
			if (gameObjects == null)
			{
				return;
			}
			foreach (GameObject gameObject2 in gameObjects)
			{
				ExposeToEditor component2 = gameObject2.GetComponent<ExposeToEditor>();
				if ((bool)component2 && !gameObject2.IsPrefab() && !gameObject2.isStatic && component2.Selected != null)
				{
					component2.Selected.Invoke(component2);
				}
			}
		}

		private void SetHandlesActive(bool isActive)
		{
			if (m_positionHandle != null)
			{
				m_positionHandle.gameObject.SetActive(isActive);
			}
			if (m_rotationHandle != null)
			{
				m_rotationHandle.gameObject.SetActive(isActive);
			}
			if (m_scaleHandle != null)
			{
				m_scaleHandle.gameObject.SetActive(isActive);
			}
			if (m_rectTool != null)
			{
				m_rectTool.gameObject.SetActive(isActive);
			}
			if (m_customHandle != null)
			{
				m_customHandle.gameObject.SetActive(isActive);
			}
		}

		protected virtual bool CanSelectObject(GameObject go)
		{
			if (m_canSelectExposedOnly)
			{
				return go.GetComponentInParent<ExposeToEditor>();
			}
			return true;
		}

		protected virtual bool CanTransformObject(GameObject go)
		{
			if (go == null)
			{
				return false;
			}
			ExposeToEditor componentInParent = go.GetComponentInParent<ExposeToEditor>();
			if (componentInParent == null)
			{
				return true;
			}
			return componentInParent.CanTransform;
		}

		public virtual Transform[] GetHandleTargets()
		{
			if (Selection.gameObjects == null)
			{
				return null;
			}
			return (from g in Selection.gameObjects
				where CanTransformObject(g)
				select g.transform into g
				orderby Selection.activeTransform == g descending
				select g).ToArray();
		}

		public virtual void Focus(FocusMode focusMode = FocusMode.Selected)
		{
		}

		public virtual void Focus(Vector3 objPositon, float objSize)
		{
		}

		private void OnBeforeDrag(BaseHandle handle)
		{
			if (IsGridEnabled)
			{
				m_wasUnitSnappingEnabled = base.Editor.Tools.UnitSnapping;
				base.Editor.Tools.UnitSnapping = true;
			}
		}

		private void OnDrop(BaseHandle handle)
		{
			if (IsGridEnabled)
			{
				base.Editor.Tools.UnitSnapping = m_wasUnitSnappingEnabled;
			}
		}

		[Obsolete]
		public virtual void Focus()
		{
		}

		[Obsolete("Use GetHandleTargets() instead")]
		protected virtual Transform[] GetTargets()
		{
			return GetHandleTargets();
		}
	}
}
