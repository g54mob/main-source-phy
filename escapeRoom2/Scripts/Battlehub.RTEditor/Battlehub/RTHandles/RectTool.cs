using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using Battlehub.Utils;
using TMPro;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class RectTool : BaseHandle
	{
		private struct PickResult
		{
			public int Index;

			public float Distance;

			public PickResult(int index, float distance)
			{
				Index = index;
				Distance = distance;
			}
		}

		public float GridSize = 1f;

		[SerializeField]
		private TextMeshPro m_txtSize1;

		[SerializeField]
		private TextMeshPro m_txtSize2;

		[SerializeField]
		private bool m_metric = true;

		private Quaternion m_rotation;

		private Vector3 m_position;

		private Vector3 m_localScale;

		private float m_currentDot;

		private RuntimeHandleAxis m_currentAxis;

		private int m_selectedPointIndex = -1;

		private int m_selectedEdgeIndex = -1;

		private Vector3 m_beginDragPoint;

		private Vector3 m_beginDragOffset;

		private bool m_isInRectTransformMode;

		private Vector3[] m_referencePoints;

		private Bounds m_referenceBounds;

		private Vector3[] m_referenceScale;

		private Vector3[] m_referencePositions;

		private Vector2[] m_referenceRectSizes;

		private MeshFilter m_lines;

		private MeshRenderer m_linesRenderer;

		private MeshFilter m_points;

		private MeshRenderer m_pointsRenderer;

		private static readonly List<RectTool> m_connectedTools = new List<RectTool>();

		private Bounds m_bounds;

		private Vector3[] m_getVertRes = new Vector3[5];

		public bool Metric
		{
			get
			{
				return m_metric;
			}
			set
			{
				if (m_metric != value)
				{
					m_metric = value;
					UpdateText();
				}
			}
		}

		public override RuntimeTool Tool => RuntimeTool.Rect;

		protected override float CurrentGridUnitSize => SizeOfGrid;

		public override float SizeOfGrid
		{
			get
			{
				return GridSize;
			}
			set
			{
				GridSize = value;
			}
		}

		protected override Transform[] Targets_Internal
		{
			get
			{
				return base.Targets_Internal;
			}
			set
			{
				base.Targets_Internal = value;
				RecalculateBoundsAndRebuild();
				Transform[] activeRealTargets = ActiveRealTargets;
				m_isInRectTransformMode = activeRealTargets != null && activeRealTargets.Length != 0 && activeRealTargets.All((Transform t) => t is RectTransform);
			}
		}

		protected override RuntimePivotMode PivotMode => RuntimePivotMode.Center;

		private void SetBounds(Bounds value)
		{
			m_bounds = value;
			UpdatePointsMesh(m_points.sharedMesh, m_currentAxis, m_bounds);
			UpdateLinesMesh(m_lines.sharedMesh, m_currentAxis, m_bounds);
			UpdateText();
		}

		protected override Vector3 GetCommonCenterPosition()
		{
			return m_bounds.center;
		}

		public void RecalculateBoundsAndRebuild()
		{
			m_rotation = Quaternion.identity;
			m_position = Vector3.zero;
			m_localScale = Vector3.one;
			m_bounds = default(Bounds);
			if (m_txtSize1 != null)
			{
				m_txtSize1.text = string.Empty;
			}
			if (m_txtSize2 != null)
			{
				m_txtSize2.text = string.Empty;
			}
			if (RealTargets == null || RealTargets.Length == 0)
			{
				return;
			}
			if (ActiveRealTargets.Length == 1 && (bool)ActiveRealTargets[0].GetComponent<ExposeToEditor>())
			{
				ExposeToEditor component = ActiveRealTargets[0].GetComponent<ExposeToEditor>();
				m_bounds = component.Bounds;
				m_position = component.transform.position;
				m_rotation = component.transform.rotation;
				m_localScale = component.transform.lossyScale;
				if (m_bounds.extents == Vector3.zero)
				{
					if (m_lines != null)
					{
						m_lines.sharedMesh.Clear();
					}
					if (m_points != null)
					{
						m_points.sharedMesh.Clear();
					}
					return;
				}
			}
			else
			{
				Bounds[] array = (from r in ActiveRealTargets.Where((Transform t) => t != null).SelectMany((Transform t) => t.GetComponentsInChildren<Renderer>())
					select r.bounds).Union(from rt in ActiveRealTargets.OfType<RectTransform>()
					select TransformExtensions.TransformBounds(rt.localToWorldMatrix, rt.CalculateRelativeRectTransformBounds())).ToArray();
				if (array.Length == 0)
				{
					if (m_lines != null)
					{
						m_lines.sharedMesh.Clear();
					}
					if (m_points != null)
					{
						m_points.sharedMesh.Clear();
					}
					return;
				}
				m_bounds = array[0];
				for (int num = 1; num < array.Length; num++)
				{
					Bounds bounds = array[num];
					m_bounds.Encapsulate(bounds);
				}
			}
			if (!(m_lines == null) || !(m_points == null))
			{
				m_lines.transform.position = m_position;
				m_lines.transform.rotation = m_rotation;
				m_lines.transform.localScale = m_localScale;
				m_points.transform.position = m_position;
				m_points.transform.rotation = m_rotation;
				m_points.transform.localScale = m_localScale;
				m_currentAxis = GetAxis(out m_currentDot);
				BuildPointsMesh(m_points.sharedMesh, m_currentAxis, m_bounds);
				BuildLineMesh(m_lines.sharedMesh, m_currentAxis, m_bounds);
				if (m_txtSize1 != null)
				{
					m_txtSize1.gameObject.layer = base.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index;
				}
				if (m_txtSize2 != null)
				{
					m_txtSize2.gameObject.layer = base.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index;
				}
				UpdateText();
				UpdateFontSize();
			}
		}

		protected override void Awake()
		{
			base.Awake();
			GameObject gameObject = new GameObject("Lines");
			gameObject.transform.SetParent(base.transform);
			gameObject.layer = base.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index;
			m_lines = gameObject.AddComponent<MeshFilter>();
			m_lines.sharedMesh = new Mesh();
			m_linesRenderer = gameObject.AddComponent<MeshRenderer>();
			Material material = new Material(Shader.Find("Battlehub/RTCommon/LineBillboard"));
			material.SetFloat("_Scale", 1f);
			material.SetColor("_Color", Color.white);
			material.SetInt("_HandleZTest", 8);
			m_linesRenderer.sharedMaterial = material;
			GameObject gameObject2 = new GameObject("Points");
			gameObject2.transform.SetParent(base.transform);
			gameObject2.layer = base.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index;
			m_points = gameObject2.AddComponent<MeshFilter>();
			m_points.sharedMesh = new Mesh();
			m_pointsRenderer = gameObject2.AddComponent<MeshRenderer>();
			Material material2 = new Material(Shader.Find("Hidden/RTHandles/PointBillboard"));
			material2.SetFloat("_Scale", 4.5f);
			material2.SetColor("_Color", Color.white);
			material2.SetInt("_HandleZTest", 8);
			m_pointsRenderer.sharedMaterial = material2;
			if (m_txtSize1 != null && m_txtSize2 != null)
			{
				CanvasRenderer component = m_txtSize1.GetComponent<CanvasRenderer>();
				if (component != null)
				{
					Object.DestroyImmediate(component);
				}
				CanvasRenderer component2 = m_txtSize2.GetComponent<CanvasRenderer>();
				if (component2 != null)
				{
					Object.DestroyImmediate(component2);
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			RecalculateBoundsAndRebuild();
			m_connectedTools.Add(this);
			if (base.RTECamera != null)
			{
				Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>(includeInactive: true);
				base.RTECamera.RenderersCache.Add(componentsInChildren, forceRender: false, forceMatrixRecalculationPerRender: true);
				base.RTECamera.RenderersCache.Refresh();
			}
			float handleScale = Appearance.HandleScale;
			if (m_txtSize1 != null && m_txtSize2 != null)
			{
				Transform obj = m_txtSize1.transform;
				Vector3 localScale = (m_txtSize2.transform.localScale = new Vector3(handleScale, handleScale, handleScale));
				obj.localScale = localScale;
			}
			m_linesRenderer.sharedMaterial.SetFloat("_Scale", handleScale);
			m_pointsRenderer.sharedMaterial.SetFloat("_Scale", 4.5f * handleScale);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_connectedTools.Remove(this);
			if (base.RTECamera != null)
			{
				Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>(includeInactive: true);
				if (base.RTECamera.RenderersCache != null)
				{
					base.RTECamera.RenderersCache.Remove(componentsInChildren);
					base.RTECamera.RenderersCache.Refresh();
				}
			}
		}

		protected override void Start()
		{
			base.Start();
			if (base.RTECamera != null)
			{
				Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>(includeInactive: true);
				base.RTECamera.RenderersCache.Add(componentsInChildren, forceRender: false, forceMatrixRecalculationPerRender: true);
				base.RTECamera.RenderersCache.Refresh();
			}
		}

		protected override void UpdateOverride()
		{
			if (!IsDragging && !base.Editor.Tools.IsViewing && Window.IsPointerOver)
			{
				SelectPointOrEdge();
			}
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
			UpdateFontSize();
		}

		private void UpdateConnectedTools()
		{
			for (int i = 0; i < m_connectedTools.Count; i++)
			{
				RectTool rectTool = m_connectedTools[i];
				if (rectTool != this)
				{
					rectTool.SetBounds(m_bounds);
					rectTool.m_lines.transform.position = m_lines.transform.position;
					rectTool.m_points.transform.position = m_points.transform.position;
				}
			}
		}

		protected override bool OnBeginDrag()
		{
			if (!base.OnBeginDrag())
			{
				return false;
			}
			if (m_bounds.extents == Vector3.zero)
			{
				return false;
			}
			if (IsAxisLocked(m_currentAxis))
			{
				return false;
			}
			SelectPointOrEdge();
			if (m_currentAxis == RuntimeHandleAxis.XY)
			{
				DragPlane = new Plane(m_lines.transform.forward, m_lines.transform.TransformPoint(m_bounds.center));
			}
			else if (m_currentAxis == RuntimeHandleAxis.XZ)
			{
				DragPlane = new Plane(m_lines.transform.up, m_lines.transform.TransformPoint(m_bounds.center));
			}
			else
			{
				DragPlane = new Plane(m_lines.transform.right, m_lines.transform.TransformPoint(m_bounds.center));
			}
			m_referenceBounds = m_bounds;
			m_referenceBounds.extents = NonZero(m_referenceBounds.extents);
			if (!GetPointOnDragPlane(Window.Pointer, out m_beginDragPoint))
			{
				return false;
			}
			if (m_selectedEdgeIndex >= 0)
			{
				m_referenceRectSizes = ActiveTargets.Select((Transform t) => (!(t as RectTransform)) ? Vector2.zero : ((RectTransform)t).rect.size).ToArray();
				m_referencePositions = ActiveTargets.Select((Transform t) => t.position).ToArray();
				m_referenceScale = ActiveTargets.Select((Transform t) => t.localScale).ToArray();
				m_referencePoints = m_lines.sharedMesh.vertices;
				m_beginDragPoint = m_lines.transform.InverseTransformPoint(m_beginDragPoint);
				m_beginDragOffset = NonZero(GetOffset(m_selectedEdgeIndex, m_beginDragPoint, out var _, out var _));
				SetSelectionColorColors();
				return true;
			}
			if (m_selectedPointIndex >= 0)
			{
				m_referenceRectSizes = ActiveTargets.Select((Transform t) => (!(t as RectTransform)) ? Vector2.zero : ((RectTransform)t).rect.size).ToArray();
				m_referencePositions = ActiveTargets.Select((Transform t) => t.position).ToArray();
				m_referenceScale = ActiveTargets.Select((Transform t) => t.localScale).ToArray();
				m_referencePoints = GetVertices().ToArray();
				if (m_selectedPointIndex < m_referencePoints.Length - 1)
				{
					m_beginDragPoint = m_points.transform.InverseTransformPoint(m_beginDragPoint);
					m_beginDragOffset = NonZero(GetOffset(m_selectedPointIndex, m_beginDragPoint, out var _));
				}
				else
				{
					RecalculateBoundsAndRebuild();
					UpdateConnectedTools();
				}
				SetSelectionColorColors();
				return true;
			}
			return false;
		}

		protected override void OnDrag()
		{
			base.OnDrag();
			if (!GetPointOnDragPlane(Window.Pointer, out var point))
			{
				return;
			}
			if (m_selectedPointIndex == m_referencePoints.Length - 1)
			{
				Vector3 vector = point - m_beginDragPoint;
				m_points.transform.position = m_position + vector;
				m_lines.transform.position = m_position + vector;
				if ((double)base.EffectiveGridUnitSize > 0.001)
				{
					Vector3 gridOffset = GetGridOffset(base.EffectiveGridUnitSize, m_points.transform.position);
					m_points.transform.position += gridOffset;
					m_lines.transform.position += gridOffset;
					vector += gridOffset;
				}
				for (int i = 0; i < ActiveTargets.Length; i++)
				{
					ActiveTargets[i].position = m_referencePositions[i] + vector;
				}
				UpdateText();
			}
			else
			{
				Vector3 one = Vector3.one;
				if (m_selectedPointIndex >= 0)
				{
					point = m_points.transform.InverseTransformPoint(point);
					Vector3 refPoint;
					Vector3 offset = GetOffset(m_selectedPointIndex, point, out refPoint);
					if ((double)base.EffectiveGridUnitSize > 0.001)
					{
						float effectiveGridUnitSize = base.EffectiveGridUnitSize;
						effectiveGridUnitSize /= 2f;
						if (!Mathf.Approximately(m_localScale.x, 0f))
						{
							float num = effectiveGridUnitSize / m_localScale.x;
							offset.x = (float)Mathf.RoundToInt(offset.x / num) * num;
						}
						if (!Mathf.Approximately(m_localScale.y, 0f))
						{
							float num2 = effectiveGridUnitSize / m_localScale.y;
							offset.y = (float)Mathf.RoundToInt(offset.y / num2) * num2;
						}
						if (!Mathf.Approximately(m_localScale.z, 0f))
						{
							float num3 = effectiveGridUnitSize / m_localScale.z;
							offset.z = (float)Mathf.RoundToInt(offset.z / num3) * num3;
						}
					}
					m_bounds.center = refPoint + offset;
					Vector3 extents = m_bounds.extents;
					if (m_currentAxis == RuntimeHandleAxis.XY)
					{
						offset.z = extents.z;
						one.x = Mathf.Sign(offset.x / m_beginDragOffset.x);
						one.y = Mathf.Sign(offset.y / m_beginDragOffset.y);
					}
					else if (m_currentAxis == RuntimeHandleAxis.XZ)
					{
						offset.y = extents.y;
						one.x = Mathf.Sign(offset.x / m_beginDragOffset.x);
						one.z = Mathf.Sign(offset.z / m_beginDragOffset.z);
					}
					else
					{
						offset.x = extents.x;
						one.y = Mathf.Sign(offset.y / m_beginDragOffset.y);
						one.z = Mathf.Sign(offset.z / m_beginDragOffset.z);
					}
					m_bounds.extents = new Vector3(Mathf.Abs(offset.x), Mathf.Abs(offset.y), Mathf.Abs(offset.z));
					UpdatePointsMesh(m_points.sharedMesh, m_currentAxis, m_bounds);
					UpdateLinesMesh(m_lines.sharedMesh, m_currentAxis, m_bounds);
					UpdateText();
				}
				else if (m_selectedEdgeIndex >= 0)
				{
					point = m_lines.transform.InverseTransformPoint(point);
					Vector3 p;
					Vector3 p2;
					Vector3 offset2 = GetOffset(m_selectedEdgeIndex, point, out p, out p2);
					if ((double)base.EffectiveGridUnitSize > 0.001)
					{
						float effectiveGridUnitSize2 = base.EffectiveGridUnitSize;
						if (!Mathf.Approximately(m_localScale.x, 0f))
						{
							float num4 = effectiveGridUnitSize2 / m_localScale.x;
							offset2.x = (float)Mathf.RoundToInt(offset2.x / num4) * num4;
						}
						if (!Mathf.Approximately(m_localScale.y, 0f))
						{
							float num5 = effectiveGridUnitSize2 / m_localScale.y;
							offset2.y = (float)Mathf.RoundToInt(offset2.y / num5) * num5;
						}
						if (!Mathf.Approximately(m_localScale.z, 0f))
						{
							float num6 = effectiveGridUnitSize2 / m_localScale.z;
							offset2.z = (float)Mathf.RoundToInt(offset2.z / num6) * num6;
						}
					}
					Vector3 vector2 = (p2 + offset2 - p) / 2f;
					m_bounds.center = (p + p2 + offset2) / 2f;
					Vector3 extents2 = m_bounds.extents;
					if (m_currentAxis == RuntimeHandleAxis.XY)
					{
						vector2.z = extents2.z;
						if (Mathf.Abs(offset2.y) > Mathf.Abs(offset2.x))
						{
							one.y = Mathf.Sign(offset2.y / m_beginDragOffset.y);
						}
						else
						{
							one.x = Mathf.Sign(offset2.x / m_beginDragOffset.x);
						}
					}
					else if (m_currentAxis == RuntimeHandleAxis.XZ)
					{
						vector2.y = extents2.y;
						if (Mathf.Abs(offset2.z) > Mathf.Abs(offset2.x))
						{
							one.z = Mathf.Sign(offset2.z / m_beginDragOffset.z);
						}
						else
						{
							one.x = Mathf.Sign(offset2.x / m_beginDragOffset.x);
						}
					}
					else
					{
						vector2.x = extents2.x;
						if (Mathf.Abs(offset2.z) > Mathf.Abs(offset2.y))
						{
							one.z = Mathf.Sign(offset2.z / m_beginDragOffset.z);
						}
						else
						{
							one.y = Mathf.Sign(offset2.y / m_beginDragOffset.y);
						}
					}
					m_bounds.extents = new Vector3(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y), Mathf.Abs(vector2.z));
					UpdatePointsMesh(m_points.sharedMesh, m_currentAxis, m_bounds);
					UpdateLinesMesh(m_lines.sharedMesh, m_currentAxis, m_bounds);
					UpdateText();
				}
				for (int j = 0; j < ActiveTargets.Length; j++)
				{
					Transform transform = ActiveTargets[j];
					Vector3 a = m_referenceScale[j];
					if (transform is RectTransform)
					{
						Vector3 vector3 = Vector3.Scale(a, new Vector3(m_bounds.extents.x / m_referenceBounds.extents.x * one.x, m_bounds.extents.y / m_referenceBounds.extents.y * one.y, m_bounds.extents.z / m_referenceBounds.extents.z * one.z));
						RectTransform obj = (RectTransform)transform;
						Vector2 vector4 = m_referenceRectSizes[j];
						if (!Mathf.Approximately(a.x, 0f))
						{
							vector4.x *= vector3.x / a.x;
						}
						if (!Mathf.Approximately(a.y, 0f))
						{
							vector4.y *= vector3.y / a.y;
						}
						obj.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vector4.x);
						obj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, vector4.y);
					}
					else
					{
						transform.localScale = Vector3.Scale(a, new Vector3(m_bounds.extents.x / m_referenceBounds.extents.x * one.x, m_bounds.extents.y / m_referenceBounds.extents.y * one.y, m_bounds.extents.z / m_referenceBounds.extents.z * one.z));
					}
					Vector3 vector5 = Vector3.zero;
					ExposeToEditor component = transform.GetComponent<ExposeToEditor>();
					if (component != null)
					{
						vector5 = transform.TransformVector(-component.Bounds.center);
					}
					transform.position = m_referencePositions[j] + (m_points.transform.TransformPoint(m_bounds.center) - m_referencePositions[j]) + vector5;
				}
			}
			UpdateConnectedTools();
		}

		protected override void OnDrop()
		{
			base.OnDrop();
			Targets = RealTargets;
			for (int i = 0; i < m_connectedTools.Count; i++)
			{
				RectTool rectTool = m_connectedTools[i];
				if (rectTool != this)
				{
					rectTool.RecalculateBoundsAndRebuild();
				}
			}
			m_referencePoints = null;
			m_referencePositions = null;
			m_referenceScale = null;
			m_referenceRectSizes = null;
		}

		private Vector3 GetOffset(int selectedEdgeIndex, Vector3 pointOnPlane, out Vector3 p0, out Vector3 p1)
		{
			p0 = m_referencePoints[(selectedEdgeIndex + 2) % 4 * 2];
			p1 = m_referencePoints[(selectedEdgeIndex + 2) % 4 * 2 + 1];
			Vector3 vector = NearestPointOnLine(p0, p1 - p0, pointOnPlane);
			Vector3 vector2 = NearestPointOnLine(m_referencePoints[selectedEdgeIndex * 2], m_referencePoints[selectedEdgeIndex * 2 + 1] - m_referencePoints[selectedEdgeIndex * 2], m_beginDragPoint) - m_beginDragPoint;
			return pointOnPlane + vector2 - vector;
		}

		private Vector3 GetOffset(int selectedPointIndex, Vector3 pointOnPlane, out Vector3 refPoint)
		{
			refPoint = m_referencePoints[(selectedPointIndex + 2) % 4];
			Vector3 vector = m_referencePoints[m_selectedPointIndex] - m_beginDragPoint;
			return (pointOnPlane + vector - refPoint) / 2f;
		}

		private Vector3 NonZero(Vector3 v)
		{
			if (Mathf.Approximately(v.x, 0f))
			{
				v.x = 1E-09f;
			}
			if (Mathf.Approximately(v.y, 0f))
			{
				v.y = 1E-09f;
			}
			if (Mathf.Approximately(v.z, 0f))
			{
				v.z = 1E-09f;
			}
			return v;
		}

		private bool IsAxisLocked(RuntimeHandleAxis axis)
		{
			if (SharedLockObject == null)
			{
				return false;
			}
			return axis switch
			{
				RuntimeHandleAxis.XY => SharedLockObject.RectXY, 
				RuntimeHandleAxis.YZ => SharedLockObject.RectYZ, 
				RuntimeHandleAxis.XZ => SharedLockObject.RectXZ, 
				_ => false, 
			};
		}

		private RuntimeHandleAxis GetAxis(out float dot)
		{
			if (m_isInRectTransformMode)
			{
				dot = 1f;
				return RuntimeHandleAxis.XY;
			}
			Camera camera = Window.Camera;
			Vector3 rhs = (camera.orthographic ? camera.transform.forward : (m_lines.transform.position - camera.transform.position).normalized);
			float num = Vector3.Dot(m_lines.transform.forward, rhs);
			float num2 = Vector3.Dot(m_lines.transform.up, rhs);
			float num3 = Vector3.Dot(m_lines.transform.right, rhs);
			float num4 = Mathf.Abs(num);
			float num5 = Mathf.Abs(num2);
			float num6 = Mathf.Abs(num3);
			if (IsAxisLocked(RuntimeHandleAxis.XY))
			{
				num4 = 0.1f;
			}
			if (IsAxisLocked(RuntimeHandleAxis.XZ))
			{
				num5 = 0.1f;
			}
			if (IsAxisLocked(RuntimeHandleAxis.YZ))
			{
				num6 = 0.1f;
			}
			if (num4 >= num5 && num4 >= num6)
			{
				dot = num;
				return RuntimeHandleAxis.XY;
			}
			if (num5 >= num6 && num5 >= num4)
			{
				dot = num2;
				return RuntimeHandleAxis.XZ;
			}
			dot = num3;
			return RuntimeHandleAxis.YZ;
		}

		private void BuildPointsMesh(Mesh target, RuntimeHandleAxis axis, Bounds bounds)
		{
			if (IsAxisLocked(axis))
			{
				target.Clear();
				return;
			}
			GetVerticesAndColors(axis, bounds, out var color, out var vertices);
			int[] array = new int[5] { 0, 1, 2, 3, 4 };
			Color[] colors = new Color[5] { color, color, color, color, color };
			target.Clear();
			target.subMeshCount = 1;
			target.name = "RectToolVertices";
			if (SystemInfo.supportsGeometryShaders)
			{
				target.vertices = vertices;
				target.SetIndices(array, MeshTopology.Points, 0);
				target.colors = colors;
			}
			else
			{
				GraphicsUtility.CreatePointBillboardMesh(vertices, array, colors, target);
			}
			target.RecalculateBounds();
		}

		private void UpdatePointsMesh(Mesh target, RuntimeHandleAxis axis, Bounds bounds)
		{
			if (IsAxisLocked(axis))
			{
				target.Clear();
				return;
			}
			Vector3[] vertices = GetVertices(axis, bounds);
			if (SystemInfo.supportsGeometryShaders)
			{
				target.vertices = vertices;
			}
			else
			{
				GraphicsUtility.UpdatePointBillboardMeshVertices(vertices, target);
			}
			target.RecalculateBounds();
		}

		private void BuildLineMesh(Mesh target, RuntimeHandleAxis axis, Bounds bounds)
		{
			if (IsAxisLocked(axis))
			{
				target.Clear();
				return;
			}
			GetVerticesAndColors(axis, bounds, out var color, out var vertices);
			Vector3[] vertices2 = new Vector3[8]
			{
				vertices[0],
				vertices[1],
				vertices[1],
				vertices[2],
				vertices[2],
				vertices[3],
				vertices[3],
				vertices[0]
			};
			int[] indices = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
			Color[] colors = new Color[8] { color, color, color, color, color, color, color, color };
			target.Clear();
			target.subMeshCount = 1;
			target.name = "RectToolLines";
			target.vertices = vertices2;
			target.SetIndices(indices, MeshTopology.Lines, 0);
			target.colors = colors;
			target.RecalculateBounds();
		}

		private void UpdateLinesMesh(Mesh target, RuntimeHandleAxis axis, Bounds bounds)
		{
			if (IsAxisLocked(axis))
			{
				target.Clear();
				return;
			}
			Vector3[] vertices = GetVertices(axis, bounds);
			target.vertices = new Vector3[8]
			{
				vertices[0],
				vertices[1],
				vertices[1],
				vertices[2],
				vertices[2],
				vertices[3],
				vertices[3],
				vertices[0]
			};
			target.RecalculateBounds();
		}

		private void UpdateText()
		{
			if ((m_txtSize1 == null && m_txtSize2 == null) || m_points == null)
			{
				return;
			}
			Vector3[] vertices = GetVertices();
			if (m_txtSize1 != null)
			{
				if (IsAxisLocked(m_currentAxis))
				{
					m_txtSize1.gameObject.SetActive(value: false);
				}
				else
				{
					m_txtSize1.gameObject.SetActive(value: true);
					float meters;
					Quaternion quaternion;
					if (m_currentAxis == RuntimeHandleAxis.XY)
					{
						meters = m_bounds.size.x * m_localScale.x;
						quaternion = ((Mathf.Sign(m_currentDot) > 0f) ? Quaternion.identity : Quaternion.Euler(0f, 180f, 0f));
					}
					else if (m_currentAxis == RuntimeHandleAxis.XZ)
					{
						meters = m_bounds.size.x * m_localScale.x;
						quaternion = ((Mathf.Sign(m_currentDot) > 0f) ? Quaternion.Euler(270f, 0f, 180f) : Quaternion.Euler(90f, 0f, 0f));
					}
					else
					{
						quaternion = ((Mathf.Sign(m_currentDot) > 0f) ? Quaternion.Euler(180f, -90f, -90f) : Quaternion.Euler(0f, -90f, -90f));
						meters = m_bounds.size.y * m_localScale.y;
					}
					m_txtSize1.transform.localRotation = m_rotation * quaternion;
					m_txtSize1.transform.position = m_points.transform.TransformPoint(vertices[0] + (vertices[1] - vertices[0]) / 2f);
					m_txtSize1.text = (m_metric ? meters.ToString("F2") : UnitsConverter.MetersToFeetInches(meters));
				}
			}
			if (!(m_txtSize2 != null))
			{
				return;
			}
			if (IsAxisLocked(m_currentAxis))
			{
				m_txtSize2.gameObject.SetActive(value: false);
				return;
			}
			m_txtSize2.gameObject.SetActive(value: true);
			Vector3 position = m_points.transform.TransformPoint(vertices[1] + (vertices[2] - vertices[1]) / 2f);
			float meters2;
			Quaternion quaternion2;
			if (m_currentAxis == RuntimeHandleAxis.XY)
			{
				meters2 = m_bounds.size.y * m_localScale.y;
				quaternion2 = ((Mathf.Sign(m_currentDot) > 0f) ? Quaternion.Euler(0f, 0f, 90f) : Quaternion.Euler(180f, 0f, 90f));
			}
			else if (m_currentAxis == RuntimeHandleAxis.XZ)
			{
				meters2 = m_bounds.size.z * m_localScale.z;
				quaternion2 = ((Mathf.Sign(m_currentDot) > 0f) ? Quaternion.Euler(270f, 0f, 90f) : Quaternion.Euler(90f, 0f, 90f));
			}
			else
			{
				meters2 = m_bounds.size.z * m_localScale.z;
				quaternion2 = ((Mathf.Sign(m_currentDot) > 0f) ? Quaternion.Euler(0f, -270f, 0f) : Quaternion.Euler(0f, -90f, 0f));
				position = m_points.transform.TransformPoint(vertices[0] + (vertices[3] - vertices[0]) / 2f);
			}
			m_txtSize2.transform.localRotation = m_rotation * quaternion2;
			m_txtSize2.transform.position = position;
			m_txtSize2.text = (m_metric ? meters2.ToString("F2") : UnitsConverter.MetersToFeetInches(meters2));
		}

		private void UpdateFontSize()
		{
			if (m_txtSize1 != null)
			{
				m_txtSize1.fontSize = GraphicsUtility.GetScreenScale(m_txtSize1.transform.position, Window.Camera) * 1.7f;
			}
			if (m_txtSize2 != null)
			{
				m_txtSize2.fontSize = GraphicsUtility.GetScreenScale(m_txtSize2.transform.position, Window.Camera) * 1.7f;
			}
		}

		private Color GetColor(RuntimeHandleAxis axis)
		{
			return axis switch
			{
				RuntimeHandleAxis.XY => Appearance.Colors.ZColor, 
				RuntimeHandleAxis.XZ => Appearance.Colors.YColor, 
				_ => Appearance.Colors.XColor, 
			};
		}

		private void GetVerticesAndColors(RuntimeHandleAxis axis, Bounds bounds, out Color color, out Vector3[] vertices)
		{
			Vector3 center = bounds.center;
			Vector3 extents = bounds.extents;
			switch (axis)
			{
			case RuntimeHandleAxis.XY:
				color = Appearance.Colors.ZColor;
				vertices = new Vector3[5]
				{
					center + new Vector3(extents.x, extents.y, 0f),
					center + new Vector3(0f - extents.x, extents.y, 0f),
					center + new Vector3(0f - extents.x, 0f - extents.y, 0f),
					center + new Vector3(extents.x, 0f - extents.y, 0f),
					center
				};
				break;
			case RuntimeHandleAxis.XZ:
				color = Appearance.Colors.YColor;
				vertices = new Vector3[5]
				{
					center + new Vector3(extents.x, 0f, extents.z),
					center + new Vector3(0f - extents.x, 0f, extents.z),
					center + new Vector3(0f - extents.x, 0f, 0f - extents.z),
					center + new Vector3(extents.x, 0f, 0f - extents.z),
					center
				};
				break;
			default:
				color = Appearance.Colors.XColor;
				vertices = new Vector3[5]
				{
					center + new Vector3(0f, extents.y, extents.z),
					center + new Vector3(0f, 0f - extents.y, extents.z),
					center + new Vector3(0f, 0f - extents.y, 0f - extents.z),
					center + new Vector3(0f, extents.y, 0f - extents.z),
					center
				};
				break;
			}
		}

		private Vector3[] GetVertices(RuntimeHandleAxis axis, Bounds bounds)
		{
			Vector3 center = bounds.center;
			Vector3 extents = bounds.extents;
			switch (axis)
			{
			case RuntimeHandleAxis.XY:
				m_getVertRes[0] = center + new Vector3(extents.x, extents.y, 0f);
				m_getVertRes[1] = center + new Vector3(0f - extents.x, extents.y, 0f);
				m_getVertRes[2] = center + new Vector3(0f - extents.x, 0f - extents.y, 0f);
				m_getVertRes[3] = center + new Vector3(extents.x, 0f - extents.y, 0f);
				m_getVertRes[4] = center;
				break;
			case RuntimeHandleAxis.XZ:
				m_getVertRes[0] = center + new Vector3(extents.x, 0f, extents.z);
				m_getVertRes[1] = center + new Vector3(0f - extents.x, 0f, extents.z);
				m_getVertRes[2] = center + new Vector3(0f - extents.x, 0f, 0f - extents.z);
				m_getVertRes[3] = center + new Vector3(extents.x, 0f, 0f - extents.z);
				m_getVertRes[4] = center;
				break;
			default:
				m_getVertRes[0] = center + new Vector3(0f, extents.y, extents.z);
				m_getVertRes[1] = center + new Vector3(0f, 0f - extents.y, extents.z);
				m_getVertRes[2] = center + new Vector3(0f, 0f - extents.y, 0f - extents.z);
				m_getVertRes[3] = center + new Vector3(0f, extents.y, 0f - extents.z);
				m_getVertRes[4] = center;
				break;
			}
			return m_getVertRes;
		}

		private Vector3[] GetVertices()
		{
			return GetVertices(m_currentAxis, m_bounds);
		}

		private PickResult PickPoint(Vector3[] points, float maxDistance = 20f)
		{
			maxDistance *= Appearance.SelectionMargin;
			int index = -1;
			float num = maxDistance * maxDistance;
			Vector3 vector = Window.Pointer.ScreenPoint;
			for (int i = 0; i < points.Length; i++)
			{
				Vector3 position = points[i];
				position = m_points.transform.transform.TransformPoint(position);
				position = Window.Camera.WorldToScreenPoint(position);
				position.z = vector.z;
				float sqrMagnitude = (position - vector).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					index = i;
					num = sqrMagnitude;
				}
			}
			return new PickResult(index, num);
		}

		private PickResult PickEdge(Vector3[] points, float maxDistance = 20f)
		{
			maxDistance *= Appearance.SelectionMargin;
			int minIndex = -1;
			float minDistance = maxDistance * maxDistance;
			Vector3 screenPoint = Window.Pointer.ScreenPoint;
			for (int i = 0; i < points.Length - 2; i++)
			{
				Vector3 p = points[i];
				Vector3 p2 = points[(i + 1) % points.Length];
				TryPickEdge(p, p2, screenPoint, i, ref minDistance, ref minIndex);
			}
			TryPickEdge(points[3], points[0], screenPoint, 3, ref minDistance, ref minIndex);
			return new PickResult(minIndex, minDistance);
		}

		private void TryPickEdge(Vector3 p0, Vector3 p1, Vector3 screenPoint, int i, ref float minDistance, ref int minIndex)
		{
			p0 = m_points.transform.transform.TransformPoint(p0);
			p1 = m_points.transform.transform.TransformPoint(p1);
			p0 = Window.Camera.WorldToScreenPoint(p0);
			p1 = Window.Camera.WorldToScreenPoint(p1);
			p0.z = (p1.z = screenPoint.z);
			float sqrMagnitude = ((Vector3)NearestPointOnSegment(p0, p1, screenPoint) - screenPoint).sqrMagnitude;
			if (sqrMagnitude < minDistance)
			{
				minIndex = i;
				minDistance = sqrMagnitude;
			}
		}

		private Vector2 NearestPointOnSegment(Vector2 origin, Vector2 end, Vector2 point)
		{
			Vector2 vector = end - origin;
			float magnitude = vector.magnitude;
			vector.Normalize();
			float value = Vector2.Dot(point - origin, vector);
			value = Mathf.Clamp(value, 0f, magnitude);
			return origin + vector * value;
		}

		public Vector3 NearestPointOnLine(Vector3 linePnt, Vector3 lineDir, Vector3 pnt)
		{
			lineDir.Normalize();
			float num = Vector3.Dot(pnt - linePnt, lineDir);
			return linePnt + lineDir * num;
		}

		private void SetSelectionColorColors()
		{
			Color[] colors = m_points.sharedMesh.colors;
			for (int i = 0; i < colors.Length; i++)
			{
				colors[i] = Appearance.Colors.SelectionColor;
			}
			m_points.sharedMesh.colors = colors;
			colors = m_lines.sharedMesh.colors;
			for (int j = 0; j < colors.Length; j++)
			{
				colors[j] = Appearance.Colors.SelectionColor;
			}
			m_lines.sharedMesh.colors = colors;
		}

		private void SelectPointOrEdge()
		{
			RuntimeHandleAxis axis = GetAxis(out m_currentDot);
			if (m_currentAxis != axis)
			{
				m_currentAxis = axis;
				RecalculateBoundsAndRebuild();
				m_selectedPointIndex = -1;
				m_selectedEdgeIndex = -1;
			}
			if (m_points.sharedMesh.vertexCount == 0)
			{
				return;
			}
			Vector3[] vertices = GetVertices();
			PickResult pickResult = PickPoint(vertices);
			pickResult.Distance *= 0.1f;
			PickResult pickResult2 = PickEdge(vertices);
			if (pickResult.Distance < pickResult2.Distance)
			{
				if (m_selectedEdgeIndex != -1)
				{
					Color[] colors = m_lines.sharedMesh.colors;
					Color color = GetColor(m_currentAxis);
					colors[m_selectedEdgeIndex * 2] = color;
					colors[m_selectedEdgeIndex * 2 + 1] = color;
					m_lines.sharedMesh.colors = colors;
					m_selectedEdgeIndex = -1;
				}
				if (pickResult.Index == m_selectedPointIndex)
				{
					return;
				}
				Color[] colors2 = m_points.sharedMesh.colors;
				if (m_selectedPointIndex >= 0)
				{
					if (SystemInfo.supportsGeometryShaders)
					{
						colors2[m_selectedPointIndex] = GetColor(m_currentAxis);
					}
					else
					{
						Color color2 = GetColor(m_currentAxis);
						for (int i = 0; i < 4; i++)
						{
							colors2[m_selectedPointIndex * 4 + i] = color2;
						}
					}
				}
				m_selectedPointIndex = pickResult.Index;
				if (m_selectedPointIndex >= 0)
				{
					if (SystemInfo.supportsGeometryShaders)
					{
						colors2[m_selectedPointIndex] = Appearance.Colors.SelectionColor;
					}
					else
					{
						Color color3 = Appearance.Colors.SelectionColor;
						for (int j = 0; j < 4; j++)
						{
							colors2[m_selectedPointIndex * 4 + j] = color3;
						}
					}
				}
				m_points.sharedMesh.colors = colors2;
			}
			else
			{
				if (!(pickResult2.Distance < pickResult.Distance))
				{
					return;
				}
				if (m_selectedPointIndex != -1)
				{
					Color[] colors3 = m_points.sharedMesh.colors;
					if (SystemInfo.supportsGeometryShaders)
					{
						colors3[m_selectedPointIndex] = GetColor(m_currentAxis);
					}
					else
					{
						Color color4 = GetColor(m_currentAxis);
						for (int k = 0; k < 4; k++)
						{
							colors3[m_selectedPointIndex * 4 + k] = color4;
						}
					}
					m_points.sharedMesh.colors = colors3;
					m_selectedPointIndex = -1;
				}
				if (pickResult2.Index != m_selectedEdgeIndex)
				{
					Color[] colors4 = m_lines.sharedMesh.colors;
					if (m_selectedEdgeIndex >= 0)
					{
						Color color5 = GetColor(m_currentAxis);
						colors4[m_selectedEdgeIndex * 2] = color5;
						colors4[m_selectedEdgeIndex * 2 + 1] = color5;
					}
					m_selectedEdgeIndex = pickResult2.Index;
					if (m_selectedEdgeIndex >= 0)
					{
						Color color6 = Appearance.Colors.SelectionColor;
						colors4[m_selectedEdgeIndex * 2] = color6;
						colors4[m_selectedEdgeIndex * 2 + 1] = color6;
					}
				}
			}
		}

		public override void Refresh()
		{
			base.Refresh();
			RecalculateBoundsAndRebuild();
		}
	}
}
