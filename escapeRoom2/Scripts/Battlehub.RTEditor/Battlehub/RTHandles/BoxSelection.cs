using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTHandles
{
	public class BoxSelection : RTEComponent, IBoxSelection
	{
		public Sprite Graphics;

		protected Image m_image;

		protected RectTransform m_rectTransform;

		protected Canvas m_canvas;

		protected bool m_isDragging;

		protected Vector3 m_startMousePosition;

		protected Vector2 m_startPt;

		protected Vector2 m_endPt;

		private Camera m_windowCanvasCamera;

		private RectTransform m_windowRectTransform;

		public Vector2 ScreenSpaceMargin = new Vector2(2f, 2f);

		public bool UseCameraSpace;

		[SerializeField]
		private BoxSelectionMethod m_method;

		private BoxSelectionMethod m_methodOverride;

		private SelectionPicker m_selectionRenderer;

		public BoxSelectionMethod Method
		{
			get
			{
				return m_method;
			}
			set
			{
				m_method = value;
			}
		}

		public BoxSelectionMethod MethodOverride
		{
			get
			{
				if (m_methodOverride != BoxSelectionMethod.Vertex)
				{
					return m_methodOverride;
				}
				return m_method;
			}
			set
			{
				m_methodOverride = value;
			}
		}

		public Bounds SelectionBounds { get; private set; }

		public bool IsThresholdPassed
		{
			get
			{
				if (m_rectTransform != null)
				{
					return m_rectTransform.sizeDelta.magnitude > 25f;
				}
				return false;
			}
		}

		public bool IsDragging => m_isDragging;

		public Canvas Canvas => m_canvas;

		public event EventHandler<BeginBoxSelectionArgs> Begin;

		public event EventHandler<FilteringArgs> Filtering;

		public event EventHandler<BoxSelectionArgs> Selection;

		protected override void Awake()
		{
			base.Awake();
			Window.IOCContainer.RegisterFallback((IBoxSelection)this);
			if (m_canvas == null)
			{
				GameObject gameObject = new GameObject("BoxSelection");
				gameObject.layer = base.gameObject.layer;
				gameObject.transform.SetParent(base.transform.parent, worldPositionStays: false);
				m_canvas = gameObject.AddComponent<Canvas>();
				m_canvas.sortingOrder = 2;
			}
			if (UseCameraSpace)
			{
				m_canvas.worldCamera = Window.Camera;
				m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
				m_canvas.planeDistance = Window.Camera.nearClipPlane + 0.05f;
				m_canvas.transform.rotation = Quaternion.identity;
				m_canvas.transform.position = Vector3.zero;
			}
			else
			{
				m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			base.transform.SetParent(m_canvas.gameObject.transform, worldPositionStays: false);
			base.transform.rotation = Quaternion.identity;
			base.transform.position = Vector3.zero;
			CanvasScaler canvasScaler = m_canvas.GetComponent<CanvasScaler>();
			if (canvasScaler == null)
			{
				canvasScaler = m_canvas.gameObject.AddComponent<CanvasScaler>();
			}
			canvasScaler.referencePixelsPerUnit = 1f;
			if (!GetComponent<BoxSelectionInput>())
			{
				base.gameObject.AddComponent<BoxSelectionInput>();
			}
			m_rectTransform = GetComponent<RectTransform>();
			if (m_rectTransform == null)
			{
				m_rectTransform = base.gameObject.AddComponent<RectTransform>();
			}
			m_rectTransform.sizeDelta = new Vector2(0f, 0f);
			m_rectTransform.pivot = new Vector2(0f, 0f);
			m_rectTransform.anchoredPosition = new Vector3(0f, 0f);
			m_image = base.gameObject.AddComponent<Image>();
			m_image.type = Image.Type.Sliced;
			if (Graphics == null)
			{
				Graphics = Resources.Load<Sprite>("RTH_BoxSelection");
			}
			m_image.sprite = Graphics;
			m_image.raycastTarget = false;
			m_selectionRenderer = new SelectionPicker(Window, delegate(FilteringArgs args)
			{
				this.Filtering?.Invoke(this, args);
			});
		}

		protected override void Start()
		{
			base.Start();
			m_windowRectTransform = Window.ViewRoot;
			Canvas componentInParent = m_windowRectTransform.GetComponentInParent<Canvas>();
			m_windowCanvasCamera = ((componentInParent.renderMode != RenderMode.ScreenSpaceOverlay) ? componentInParent.worldCamera : null);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Window.IOCContainer.UnregisterFallback((IBoxSelection)this);
			if (base.Editor != null && base.Editor.Tools != null && base.Editor.Tools.ActiveTool == this && base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
			m_selectionRenderer = null;
		}

		protected override void OnWindowActivated()
		{
			base.OnWindowActivated();
			m_canvas.enabled = true;
		}

		protected override void OnWindowDeactivated()
		{
			m_canvas.enabled = false;
			base.OnWindowDeactivated();
			if (base.Editor != null && base.Editor.Tools != null && base.Editor.Tools.ActiveTool == this && base.Editor.Tools.ActiveTool == this)
			{
				base.Editor.Tools.ActiveTool = null;
			}
		}

		public void BeginSelect()
		{
			if (base.Editor.Tools.ActiveTool != null || m_windowRectTransform == null)
			{
				return;
			}
			if (this.Begin != null)
			{
				BeginBoxSelectionArgs beginBoxSelectionArgs = new BeginBoxSelectionArgs();
				this.Begin(this, beginBoxSelectionArgs);
				if (beginBoxSelectionArgs.Cancel)
				{
					return;
				}
			}
			m_startMousePosition = Window.Pointer.ScreenPoint;
			m_isDragging = GetPoint(out m_startPt) && (!Window.Editor.IsOpened || Window.IsPointerOver);
			if (m_isDragging)
			{
				m_rectTransform.anchoredPosition = m_startPt;
				m_rectTransform.sizeDelta = new Vector2(0f, 0f);
			}
		}

		public void EndSelect()
		{
			if (m_isDragging)
			{
				m_isDragging = false;
				HitTest();
				m_rectTransform.sizeDelta = new Vector2(0f, 0f);
				if (base.Editor.Tools.ActiveTool == this)
				{
					base.Editor.Tools.ActiveTool = null;
				}
			}
		}

		public Renderer[] Pick(Renderer[] renderers = null, bool filterObjects = true)
		{
			return m_selectionRenderer.Pick(renderers, filterObjects);
		}

		public Renderer[] Pick(Renderer[] renderers, Bounds bounds, bool filterObjects = true)
		{
			return m_selectionRenderer.Pick(renderers, bounds, filterObjects);
		}

		public Color32[] BeginPick(out Vector2Int texSize, Renderer[] renderers = null)
		{
			return m_selectionRenderer.BeginPick(out texSize, renderers);
		}

		public Renderer[] EndPick(Color32[] texPixels, Vector2Int texSize, Renderer[] renderers = null)
		{
			return m_selectionRenderer.EndPick(texPixels, texSize, renderers);
		}

		private void Update()
		{
			if (!base.Editor.Selection.Enabled || (base.Editor.Tools.ActiveTool != this && base.Editor.Tools.ActiveTool != null))
			{
				return;
			}
			if (base.Editor.Tools.IsViewing || !base.Editor.Tools.IsBoxSelectionEnabled)
			{
				if (base.Editor.Tools.ActiveTool == this)
				{
					base.Editor.Tools.ActiveTool = null;
				}
				m_isDragging = false;
				m_rectTransform.sizeDelta = new Vector2(0f, 0f);
			}
			else if (m_isDragging)
			{
				GetPoint(out m_endPt);
				Vector2 vector = m_endPt - m_startPt;
				base.Editor.Tools.ActiveTool = this;
				if (vector != Vector2.zero)
				{
					base.Editor.Tools.ActiveTool = this;
				}
				m_rectTransform.sizeDelta = new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
				m_rectTransform.localScale = new Vector3(Mathf.Sign(vector.x), Mathf.Sign(vector.y), 1f);
			}
		}

		private void HitTest()
		{
			if (!IsThresholdPassed)
			{
				return;
			}
			Vector3 center = (m_startMousePosition + (Vector3)Window.Pointer.ScreenPoint) / 2f;
			center.z = 0f;
			Bounds selectionBounds = (SelectionBounds = new Bounds(center, m_rectTransform.sizeDelta));
			FilteringArgs filteringArgs = new FilteringArgs();
			Renderer[] array = UnityObjectExt.FindObjectsByType<Renderer>();
			HashSet<GameObject> hashSet;
			if (MethodOverride == BoxSelectionMethod.PixelPerfectDepthTest)
			{
				IEnumerable<Renderer> renderers = m_selectionRenderer.PixelPerfectDepthTest(array, SelectionBounds);
				hashSet = new HashSet<GameObject>(from rend in FiterRenderers(renderers, filteringArgs)
					select rend.gameObject);
			}
			else
			{
				hashSet = new HashSet<GameObject>();
				Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Window.Camera);
				foreach (Renderer obj in array)
				{
					Bounds bounds2 = obj.bounds;
					GameObject go = obj.gameObject;
					TrySelect(ref selectionBounds, hashSet, filteringArgs, ref bounds2, go, frustumPlanes);
				}
			}
			SpriteGizmo[] array2 = UnityObjectExt.FindObjectsByType<SpriteGizmo>();
			foreach (SpriteGizmo spriteGizmo in array2)
			{
				if (TransformCenter(ref selectionBounds, spriteGizmo.transform))
				{
					FilterGameObjects(hashSet, filteringArgs, spriteGizmo.gameObject);
				}
			}
			if (this.Selection != null)
			{
				this.Selection(this, new BoxSelectionArgs
				{
					GameObjects = hashSet.ToArray()
				});
			}
			else
			{
				IRuntimeSelection selection = base.Editor.Selection;
				UnityEngine.Object[] objects = hashSet.ToArray();
				selection.objects = objects;
			}
		}

		private HashSet<Renderer> FiterRenderers(IEnumerable<Renderer> renderers, FilteringArgs filteringArgs)
		{
			HashSet<Renderer> hashSet = new HashSet<Renderer>();
			foreach (Renderer renderer in renderers)
			{
				if (hashSet.Contains(renderer))
				{
					continue;
				}
				if (this.Filtering != null)
				{
					filteringArgs.Object = renderer.gameObject;
					this.Filtering(this, filteringArgs);
					if (!filteringArgs.Cancel)
					{
						hashSet.Add(renderer);
					}
					filteringArgs.Reset();
				}
				else
				{
					hashSet.Add(renderer);
				}
			}
			return hashSet;
		}

		private void TrySelect(ref Bounds selectionBounds, HashSet<GameObject> selection, FilteringArgs args, ref Bounds bounds, GameObject go, Plane[] frustumPlanes)
		{
			if (!GeometryUtility.TestPlanesAABB(frustumPlanes, bounds))
			{
				return;
			}
			bool flag;
			if (MethodOverride == BoxSelectionMethod.LooseFitting)
			{
				flag = LooseFitting(ref selectionBounds, ref bounds);
			}
			else if (MethodOverride != BoxSelectionMethod.Vertex)
			{
				flag = ((MethodOverride != BoxSelectionMethod.BoundsCenter) ? TransformCenter(ref selectionBounds, go.transform) : BoundsCenter(ref selectionBounds, ref bounds));
			}
			else
			{
				flag = LooseFitting(ref selectionBounds, ref bounds);
				if (flag && !selection.Contains(go))
				{
					flag = false;
					MeshFilter component = go.GetComponent<MeshFilter>();
					if (component != null && component.sharedMesh != null)
					{
						Vector3[] vertices = component.sharedMesh.vertices;
						for (int i = 0; i < vertices.Length; i++)
						{
							Vector3 position = go.transform.TransformPoint(vertices[i]);
							position = Window.Camera.WorldToScreenPoint(position);
							position.z = 0f;
							if (selectionBounds.Contains(position))
							{
								flag = true;
								break;
							}
						}
					}
					else
					{
						SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
						if (component2 != null && component2.sharedMesh != null)
						{
							Mesh mesh = new Mesh();
							component2.BakeMesh(mesh);
							Matrix4x4 matrix4x = Matrix4x4.TRS(go.transform.localPosition, go.transform.localRotation, Vector3.one);
							if (component2.transform.parent != null)
							{
								matrix4x *= component2.transform.parent.localToWorldMatrix;
							}
							Vector3[] vertices2 = mesh.vertices;
							for (int j = 0; j < vertices2.Length; j++)
							{
								Vector3 position2 = matrix4x.MultiplyPoint(vertices2[j]);
								position2 = Window.Camera.WorldToScreenPoint(position2);
								position2.z = 0f;
								if (selectionBounds.Contains(position2))
								{
									flag = true;
									break;
								}
							}
							UnityEngine.Object.Destroy(mesh);
						}
					}
				}
			}
			if (flag)
			{
				FilterGameObjects(selection, args, go);
			}
		}

		private void FilterGameObjects(HashSet<GameObject> selection, FilteringArgs args, GameObject go)
		{
			if (selection.Contains(go))
			{
				return;
			}
			if (this.Filtering != null)
			{
				args.Object = go;
				this.Filtering(this, args);
				if (!args.Cancel)
				{
					selection.Add(go);
				}
				args.Reset();
			}
			else
			{
				selection.Add(go);
			}
		}

		private bool TransformCenter(ref Bounds selectionBounds, Transform tr)
		{
			Vector3 point = Window.Camera.WorldToScreenPoint(tr.position);
			point.z = 0f;
			return selectionBounds.Contains(point);
		}

		private bool BoundsCenter(ref Bounds selectionBounds, ref Bounds bounds)
		{
			Vector3 point = Window.Camera.WorldToScreenPoint(bounds.center);
			point.z = 0f;
			return selectionBounds.Contains(point);
		}

		private bool LooseFitting(ref Bounds selectionBounds, ref Bounds bounds)
		{
			Vector3 position = bounds.center + new Vector3(0f - bounds.extents.x, 0f - bounds.extents.y, 0f - bounds.extents.z);
			Vector3 position2 = bounds.center + new Vector3(0f - bounds.extents.x, 0f - bounds.extents.y, bounds.extents.z);
			Vector3 position3 = bounds.center + new Vector3(0f - bounds.extents.x, bounds.extents.y, 0f - bounds.extents.z);
			Vector3 position4 = bounds.center + new Vector3(0f - bounds.extents.x, bounds.extents.y, bounds.extents.z);
			Vector3 position5 = bounds.center + new Vector3(bounds.extents.x, 0f - bounds.extents.y, 0f - bounds.extents.z);
			Vector3 position6 = bounds.center + new Vector3(bounds.extents.x, 0f - bounds.extents.y, bounds.extents.z);
			Vector3 position7 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, 0f - bounds.extents.z);
			Vector3 position8 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
			position = Window.Camera.WorldToScreenPoint(position);
			position2 = Window.Camera.WorldToScreenPoint(position2);
			position3 = Window.Camera.WorldToScreenPoint(position3);
			position4 = Window.Camera.WorldToScreenPoint(position4);
			position5 = Window.Camera.WorldToScreenPoint(position5);
			position6 = Window.Camera.WorldToScreenPoint(position6);
			position7 = Window.Camera.WorldToScreenPoint(position7);
			position8 = Window.Camera.WorldToScreenPoint(position8);
			float x = Mathf.Min(position.x, position2.x, position3.x, position4.x, position5.x, position6.x, position7.x, position8.x);
			float x2 = Mathf.Max(position.x, position2.x, position3.x, position4.x, position5.x, position6.x, position7.x, position8.x);
			float y = Mathf.Min(position.y, position2.y, position3.y, position4.y, position5.y, position6.y, position7.y, position8.y);
			float y2 = Mathf.Max(position.y, position2.y, position3.y, position4.y, position5.y, position6.y, position7.y, position8.y);
			Vector3 vector = new Vector2(x, y);
			Vector3 vector2 = new Vector2(x2, y2);
			Bounds bounds2 = new Bounds((vector + vector2) / 2f, vector2 - vector);
			return selectionBounds.Intersects(bounds2);
		}

		private bool GetPoint(out Vector2 localPoint)
		{
			Camera cam = null;
			if (m_canvas.renderMode != RenderMode.ScreenSpaceOverlay)
			{
				cam = m_canvas.worldCamera;
			}
			Vector3 vector;
			if (UseCameraSpace)
			{
				vector = Window.Pointer.ScreenPoint;
			}
			else
			{
				vector = Window.Editor.Input.GetPointerXY(0);
				Rect rect = m_windowRectTransform.rect;
				Vector2 vector2 = RectTransformUtility.WorldToScreenPoint(m_windowCanvasCamera, m_windowRectTransform.TransformPoint(rect.min)) + ScreenSpaceMargin;
				Vector2 vector3 = RectTransformUtility.WorldToScreenPoint(m_windowCanvasCamera, m_windowRectTransform.TransformPoint(rect.max)) - ScreenSpaceMargin;
				vector.x = Mathf.Clamp(vector.x, vector2.x, vector3.x);
				vector.y = Mathf.Clamp(vector.y, vector2.y, vector3.y);
			}
			return RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvas.GetComponent<RectTransform>(), vector, cam, out localPoint);
		}
	}
}
