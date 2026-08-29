using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(1)]
	public class PositionHandle : BaseHandle
	{
		public float GridSize = 1f;

		private Vector3 m_cursorPosition;

		private Vector3 m_currentPosition;

		private Vector3 m_prevPoint;

		private Matrix4x4 m_matrix;

		private Matrix4x4 m_inverse;

		private Vector2 m_prevMousePosition;

		private int[] m_targetLayers;

		private Transform[] m_snapTargets;

		private Bounds[] m_snapTargetsBounds;

		private ExposeToEditor[] m_allExposedToEditor;

		private bool m_isInVertexSnappingMode;

		private Vector3[] m_boundingBoxCorners = new Vector3[8];

		private Vector3 m_handleOffset;

		private LockObject m_sharedLockObject;

		private Mode m_currentMode;

		private RTHDrawingSettings m_settings = new RTHDrawingSettings();

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

		protected override float CurrentGridUnitSize => SizeOfGrid;

		public bool SnapToGround { get; set; }

		public bool IsInVertexSnappingMode
		{
			get
			{
				return m_isInVertexSnappingMode;
			}
			set
			{
				m_isInVertexSnappingMode = value;
				if (m_isInVertexSnappingMode)
				{
					if ((SharedLockObject == null || !SharedLockObject.IsPositionLocked) && Window.Pointer.XY(Position, out m_prevMousePosition))
					{
						BeginSnap();
					}
				}
				else
				{
					SelectedAxis = RuntimeHandleAxis.None;
					if (!IsInVertexSnappingMode && !base.Editor.Tools.IsSnapping)
					{
						m_handleOffset = Vector3.zero;
					}
				}
				if (Model != null && Model is PositionHandleModel)
				{
					((PositionHandleModel)Model).IsVertexSnapping = value;
				}
			}
		}

		public override Vector3 Position
		{
			get
			{
				return base.transform.position + m_handleOffset;
			}
			set
			{
				base.transform.position = value - m_handleOffset;
			}
		}

		public override RuntimeTool Tool => RuntimeTool.Move;

		protected override LockObject SharedLockObject
		{
			get
			{
				return base.SharedLockObject;
			}
			set
			{
				m_sharedLockObject = value;
				LockObject lockObject = m_sharedLockObject;
				if (m_currentMode != Mode.XYZ3D)
				{
					lockObject = ((m_sharedLockObject != null) ? new LockObject(m_sharedLockObject) : new LockObject());
					switch (m_currentMode)
					{
					case Mode.XY2D:
						lockObject.PositionZ = true;
						break;
					case Mode.XZ2D:
						lockObject.PositionY = true;
						break;
					case Mode.YZ2D:
						lockObject.PositionX = true;
						break;
					}
				}
				base.SharedLockObject = lockObject;
			}
		}

		protected override Mode CurrentMode
		{
			get
			{
				return m_currentMode;
			}
			set
			{
				if (m_currentMode != value)
				{
					m_currentMode = value;
					SharedLockObject = m_sharedLockObject;
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			BaseHandleInput component = GetComponent<BaseHandleInput>();
			if (component == null || component.Handle != this)
			{
				component = base.gameObject.AddComponent<PositionHandleInput>();
				component.Handle = this;
			}
			m_isInVertexSnappingMode = false;
			base.Editor.Tools.IsSnapping = false;
			m_handleOffset = Vector3.zero;
			m_targetLayers = null;
			m_snapTargets = null;
			m_snapTargetsBounds = null;
			m_allExposedToEditor = null;
			base.Editor.Tools.IsSnappingChanged += OnSnappingChanged;
			OnSnappingChanged();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (Window != null && base.Editor != null)
			{
				base.Editor.Tools.IsSnapping = false;
				base.Editor.Tools.IsSnappingChanged -= OnSnappingChanged;
			}
			m_targetLayers = null;
			m_snapTargets = null;
			m_snapTargetsBounds = null;
			m_allExposedToEditor = null;
		}

		protected override void UpdateOverride()
		{
			base.UpdateOverride();
			UpdateCurrentMode();
			if (base.Editor.Tools.IsViewing)
			{
				SelectedAxis = RuntimeHandleAxis.None;
			}
			else
			{
				if (!base.IsWindowActive || !Window.IsPointerOver)
				{
					return;
				}
				IRTE editor = base.Editor;
				if (IsDragging && SnapToGround && SelectedAxis != RuntimeHandleAxis.Y)
				{
					SnapActiveTargetsToGround(ActiveTargets, Window.Camera, rotate: true);
					base.transform.position = Targets[0].position;
				}
				if ((!IsInVertexSnappingMode && !base.Editor.Tools.IsSnapping) || !Window.Pointer.XY(Position, out var result))
				{
					return;
				}
				if (editor.Tools.SnappingMode == SnappingMode.BoundingBox)
				{
					if (IsDragging)
					{
						SelectedAxis = RuntimeHandleAxis.Snap;
						if (m_prevMousePosition != result)
						{
							m_prevMousePosition = result;
							float minDistance = float.MaxValue;
							Vector3 minPoint = Vector3.zero;
							bool minPointFound = false;
							for (int i = 0; i < m_allExposedToEditor.Length; i++)
							{
								ExposeToEditor exposeToEditor = m_allExposedToEditor[i];
								Bounds bounds = exposeToEditor.Bounds;
								m_boundingBoxCorners[0] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
								m_boundingBoxCorners[1] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, 0f - bounds.extents.z);
								m_boundingBoxCorners[2] = bounds.center + new Vector3(bounds.extents.x, 0f - bounds.extents.y, bounds.extents.z);
								m_boundingBoxCorners[3] = bounds.center + new Vector3(bounds.extents.x, 0f - bounds.extents.y, 0f - bounds.extents.z);
								m_boundingBoxCorners[4] = bounds.center + new Vector3(0f - bounds.extents.x, bounds.extents.y, bounds.extents.z);
								m_boundingBoxCorners[5] = bounds.center + new Vector3(0f - bounds.extents.x, bounds.extents.y, 0f - bounds.extents.z);
								m_boundingBoxCorners[6] = bounds.center + new Vector3(0f - bounds.extents.x, 0f - bounds.extents.y, bounds.extents.z);
								m_boundingBoxCorners[7] = bounds.center + new Vector3(0f - bounds.extents.x, 0f - bounds.extents.y, 0f - bounds.extents.z);
								GetMinPoint(ref minDistance, ref minPoint, ref minPointFound, exposeToEditor.BoundsObject.transform);
							}
							if (minPointFound)
							{
								Position = minPoint;
							}
						}
						return;
					}
					SelectedAxis = RuntimeHandleAxis.None;
					if (!(m_prevMousePosition != result))
					{
						return;
					}
					m_prevMousePosition = result;
					float minDistance2 = float.MaxValue;
					Vector3 minPoint2 = Vector3.zero;
					bool minPointFound2 = false;
					for (int j = 0; j < m_snapTargets.Length; j++)
					{
						Transform tr = m_snapTargets[j];
						Bounds bounds2 = m_snapTargetsBounds[j];
						m_boundingBoxCorners[0] = bounds2.center + new Vector3(bounds2.extents.x, bounds2.extents.y, bounds2.extents.z);
						m_boundingBoxCorners[1] = bounds2.center + new Vector3(bounds2.extents.x, bounds2.extents.y, 0f - bounds2.extents.z);
						m_boundingBoxCorners[2] = bounds2.center + new Vector3(bounds2.extents.x, 0f - bounds2.extents.y, bounds2.extents.z);
						m_boundingBoxCorners[3] = bounds2.center + new Vector3(bounds2.extents.x, 0f - bounds2.extents.y, 0f - bounds2.extents.z);
						m_boundingBoxCorners[4] = bounds2.center + new Vector3(0f - bounds2.extents.x, bounds2.extents.y, bounds2.extents.z);
						m_boundingBoxCorners[5] = bounds2.center + new Vector3(0f - bounds2.extents.x, bounds2.extents.y, 0f - bounds2.extents.z);
						m_boundingBoxCorners[6] = bounds2.center + new Vector3(0f - bounds2.extents.x, 0f - bounds2.extents.y, bounds2.extents.z);
						m_boundingBoxCorners[7] = bounds2.center + new Vector3(0f - bounds2.extents.x, 0f - bounds2.extents.y, 0f - bounds2.extents.z);
						if (Targets[j] != null)
						{
							GetMinPoint(ref minDistance2, ref minPoint2, ref minPointFound2, tr);
						}
					}
					if (minPointFound2)
					{
						m_handleOffset = minPoint2 - base.transform.position;
					}
					return;
				}
				if (IsDragging)
				{
					SelectedAxis = RuntimeHandleAxis.Snap;
					if (!(m_prevMousePosition != result))
					{
						return;
					}
					m_prevMousePosition = result;
					Ray ray = Window.Pointer;
					LayerMask layerMask = 16;
					layerMask = ~(int)layerMask;
					layerMask = (int)layerMask & base.Editor.CameraLayerSettings.RaycastMask;
					for (int k = 0; k < m_snapTargets.Length; k++)
					{
						m_targetLayers[k] = m_snapTargets[k].gameObject.layer;
						m_snapTargets[k].gameObject.layer = 4;
					}
					GameObject gameObject = null;
					if (Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, layerMask))
					{
						gameObject = hitInfo.collider.gameObject;
					}
					else
					{
						float num = float.MaxValue;
						for (int l = 0; l < m_allExposedToEditor.Length; l++)
						{
							ExposeToEditor exposeToEditor2 = m_allExposedToEditor[l];
							Bounds bounds3 = exposeToEditor2.Bounds;
							m_boundingBoxCorners[0] = bounds3.center + new Vector3(bounds3.extents.x, bounds3.extents.y, bounds3.extents.z);
							m_boundingBoxCorners[1] = bounds3.center + new Vector3(bounds3.extents.x, bounds3.extents.y, 0f - bounds3.extents.z);
							m_boundingBoxCorners[2] = bounds3.center + new Vector3(bounds3.extents.x, 0f - bounds3.extents.y, bounds3.extents.z);
							m_boundingBoxCorners[3] = bounds3.center + new Vector3(bounds3.extents.x, 0f - bounds3.extents.y, 0f - bounds3.extents.z);
							m_boundingBoxCorners[4] = bounds3.center + new Vector3(0f - bounds3.extents.x, bounds3.extents.y, bounds3.extents.z);
							m_boundingBoxCorners[5] = bounds3.center + new Vector3(0f - bounds3.extents.x, bounds3.extents.y, 0f - bounds3.extents.z);
							m_boundingBoxCorners[6] = bounds3.center + new Vector3(0f - bounds3.extents.x, 0f - bounds3.extents.y, bounds3.extents.z);
							m_boundingBoxCorners[7] = bounds3.center + new Vector3(0f - bounds3.extents.x, 0f - bounds3.extents.y, 0f - bounds3.extents.z);
							for (int m = 0; m < m_boundingBoxCorners.Length; m++)
							{
								if (Window.Pointer.WorldToScreenPoint(Position, exposeToEditor2.BoundsObject.transform.TransformPoint(m_boundingBoxCorners[m]), out var result2))
								{
									float magnitude = (result2 - result).magnitude;
									if (magnitude < num)
									{
										gameObject = exposeToEditor2.gameObject;
										num = magnitude;
									}
								}
							}
						}
					}
					if (gameObject != null)
					{
						float minDistance3 = float.MaxValue;
						Vector3 minPoint3 = Vector3.zero;
						bool minPointFound3 = false;
						Transform meshTransform;
						Mesh mesh = GetMesh(gameObject, out meshTransform);
						GetMinPoint(meshTransform, ref minDistance3, ref minPoint3, ref minPointFound3, mesh);
						if (minPointFound3)
						{
							Position = minPoint3;
						}
					}
					for (int n = 0; n < m_snapTargets.Length; n++)
					{
						m_snapTargets[n].gameObject.layer = m_targetLayers[n];
					}
					return;
				}
				SelectedAxis = RuntimeHandleAxis.None;
				if (m_prevMousePosition != result)
				{
					m_prevMousePosition = result;
					float minDistance4 = float.MaxValue;
					Vector3 minPoint4 = Vector3.zero;
					bool minPointFound4 = false;
					for (int num2 = 0; num2 < RealTargets.Length; num2++)
					{
						Transform meshTransform2;
						Mesh mesh2 = GetMesh(RealTargets[num2].gameObject, out meshTransform2);
						GetMinPoint(meshTransform2, ref minDistance4, ref minPoint4, ref minPointFound4, mesh2);
					}
					if (minPointFound4)
					{
						m_handleOffset = minPoint4 - base.transform.position;
					}
				}
			}
		}

		private void GetMinPoint(Transform meshTransform, ref float minDistance, ref Vector3 minPoint, ref bool minPointFound, Mesh mesh)
		{
			if (!(mesh != null) || !mesh.isReadable)
			{
				return;
			}
			_ = base.Editor;
			Vector3[] vertices = mesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 position = vertices[i];
				position = meshTransform.TransformPoint(position);
				if (Window.Pointer.WorldToScreenPoint(Position, position, out var result) && Window.Pointer.XY(Position, out var result2))
				{
					float magnitude = (result - result2).magnitude;
					if (magnitude < minDistance)
					{
						minPointFound = true;
						minDistance = magnitude;
						minPoint = position;
					}
				}
			}
		}

		private static Mesh GetMesh(GameObject go, out Transform meshTransform)
		{
			Mesh result = null;
			meshTransform = null;
			MeshFilter componentInChildren = go.GetComponentInChildren<MeshFilter>();
			if (componentInChildren != null)
			{
				result = componentInChildren.sharedMesh;
				meshTransform = componentInChildren.transform;
			}
			else
			{
				SkinnedMeshRenderer componentInChildren2 = go.GetComponentInChildren<SkinnedMeshRenderer>();
				if (componentInChildren2 != null)
				{
					result = componentInChildren2.sharedMesh;
					meshTransform = componentInChildren2.transform;
				}
				else
				{
					MeshCollider componentInChildren3 = go.GetComponentInChildren<MeshCollider>();
					if (componentInChildren3 != null)
					{
						result = componentInChildren3.sharedMesh;
						meshTransform = componentInChildren3.transform;
					}
				}
			}
			return result;
		}

		protected override void OnDrop()
		{
			base.OnDrop();
			if (SnapToGrid)
			{
				SnapActiveTargetsToGrid();
			}
			if (SnapToGround)
			{
				SnapActiveTargetsToGround(ActiveTargets, Window.Camera, rotate: true);
				base.transform.position = Targets[0].position;
			}
		}

		private static void SnapActiveTargetsToGround(Transform[] targets, Camera camera, bool rotate)
		{
			Plane[] array = GeometryUtility.CalculateFrustumPlanes(camera);
			foreach (Transform activeTarget in targets)
			{
				Ray ray = new Ray(activeTarget.position, Vector3.up);
				bool flag = false;
				Vector3 origin = activeTarget.position;
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j].Raycast(ray, out var enter))
					{
						flag = true;
						origin = ray.GetPoint(enter);
					}
				}
				if (!flag)
				{
					continue;
				}
				ray = new Ray(origin, Vector3.down);
				RaycastHit[] array2 = (from hit in Physics.RaycastAll(ray)
					where !hit.transform.IsChildOf(activeTarget)
					select hit).ToArray();
				if (array2.Length == 0)
				{
					continue;
				}
				float num = float.PositiveInfinity;
				RaycastHit raycastHit = array2[0];
				for (int num2 = 0; num2 < array2.Length; num2++)
				{
					float magnitude = (activeTarget.position - array2[num2].point).magnitude;
					if (magnitude < num)
					{
						num = magnitude;
						raycastHit = array2[num2];
					}
				}
				activeTarget.position += raycastHit.point - activeTarget.position;
				if (rotate)
				{
					activeTarget.rotation = Quaternion.FromToRotation(activeTarget.up, raycastHit.normal) * activeTarget.rotation;
				}
			}
		}

		private void OnSnappingChanged()
		{
			if (base.Editor.Tools.IsSnapping)
			{
				BeginSnap();
				return;
			}
			m_handleOffset = Vector3.zero;
			if (Model != null && Model is PositionHandleModel)
			{
				((PositionHandleModel)Model).IsVertexSnapping = false;
			}
		}

		private void BeginSnap()
		{
			if (Window.Camera == null)
			{
				return;
			}
			if (Model != null && Model is PositionHandleModel)
			{
				((PositionHandleModel)Model).IsVertexSnapping = true;
			}
			HashSet<Transform> hashSet = new HashSet<Transform>();
			List<Transform> list = new List<Transform>();
			List<Bounds> list2 = new List<Bounds>();
			if (base.Target != null)
			{
				for (int i = 0; i < RealTargets.Length; i++)
				{
					Transform transform = RealTargets[i];
					if (!(transform != null))
					{
						continue;
					}
					ExposeToEditor component = transform.GetComponent<ExposeToEditor>();
					if (component != null)
					{
						list2.Add(component.Bounds);
						list.Add(component.BoundsObject.transform);
						hashSet.Add(component.BoundsObject.transform);
						continue;
					}
					list.Add(transform);
					hashSet.Add(transform);
					MeshFilter component2 = transform.GetComponent<MeshFilter>();
					if (component2 != null && component2.sharedMesh != null)
					{
						list2.Add(component2.sharedMesh.bounds);
						continue;
					}
					SkinnedMeshRenderer component3 = transform.GetComponent<SkinnedMeshRenderer>();
					if (component3 != null && component3.sharedMesh != null)
					{
						list2.Add(component3.sharedMesh.bounds);
						continue;
					}
					Bounds item = new Bounds(Vector3.zero, Vector3.zero);
					list2.Add(item);
				}
			}
			m_snapTargets = list.ToArray();
			m_targetLayers = new int[m_snapTargets.Length];
			m_snapTargetsBounds = list2.ToArray();
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Window.Camera);
			ExposeToEditor[] array = UnityObjectExt.FindObjectsByType<ExposeToEditor>();
			List<ExposeToEditor> list3 = new List<ExposeToEditor>();
			foreach (ExposeToEditor exposeToEditor in array)
			{
				if (exposeToEditor.CanSnap && GeometryUtility.TestPlanesAABB(planes, new Bounds(exposeToEditor.transform.TransformPoint(exposeToEditor.Bounds.center), Vector3.zero)) && !hashSet.Contains(exposeToEditor.transform))
				{
					list3.Add(exposeToEditor);
				}
			}
			m_allExposedToEditor = list3.ToArray();
		}

		private void GetMinPoint(ref float minDistance, ref Vector3 minPoint, ref bool minPointFound, Transform tr)
		{
			_ = base.Editor;
			for (int i = 0; i < m_boundingBoxCorners.Length; i++)
			{
				Vector3 vector = tr.TransformPoint(m_boundingBoxCorners[i]);
				if (Window.Pointer.WorldToScreenPoint(Position, vector, out var result) && Window.Pointer.XY(Position, out var result2))
				{
					float magnitude = (result - result2).magnitude;
					if (magnitude < minDistance)
					{
						minPointFound = true;
						minDistance = magnitude;
						minPoint = vector;
					}
				}
			}
		}

		private bool HitSnapHandle()
		{
			if (Window.Pointer.WorldToScreenPoint(Position, Position, out var result) && Window.Pointer.XY(Position, out var result2))
			{
				if (result.x - 10f <= result2.x && result2.x <= result.x + 10f && result.y - 10f <= result2.y)
				{
					return result2.y <= result.y + 10f;
				}
				return false;
			}
			return false;
		}

		protected override bool OnBeginDrag()
		{
			if (!base.OnBeginDrag())
			{
				return false;
			}
			m_currentPosition = Position;
			m_cursorPosition = Position;
			if ((IsInVertexSnappingMode || base.Editor.Tools.IsSnapping) && SelectedAxis != RuntimeHandleAxis.Snap)
			{
				return HitSnapHandle();
			}
			if (SelectedAxis == RuntimeHandleAxis.XZ)
			{
				DragPlane = GetDragPlane(m_matrix, Vector3.up);
				return GetPointOnDragPlane(Window.Pointer, out m_prevPoint);
			}
			if (SelectedAxis == RuntimeHandleAxis.YZ)
			{
				DragPlane = GetDragPlane(m_matrix, Vector3.right);
				return GetPointOnDragPlane(Window.Pointer, out m_prevPoint);
			}
			if (SelectedAxis == RuntimeHandleAxis.XY)
			{
				DragPlane = GetDragPlane(m_matrix, Vector3.forward);
				return GetPointOnDragPlane(Window.Pointer, out m_prevPoint);
			}
			if (SelectedAxis != RuntimeHandleAxis.None)
			{
				Vector3 axis = Vector3.zero;
				switch (SelectedAxis)
				{
				case RuntimeHandleAxis.X:
					axis = Vector3.right;
					break;
				case RuntimeHandleAxis.Y:
					axis = Vector3.up;
					break;
				case RuntimeHandleAxis.Z:
					axis = Vector3.forward;
					break;
				}
				DragPlane = GetDragPlane(axis);
				bool pointOnDragPlane = GetPointOnDragPlane(Window.Pointer, out m_prevPoint);
				if (!pointOnDragPlane)
				{
					SelectedAxis = RuntimeHandleAxis.None;
				}
				return pointOnDragPlane;
			}
			return false;
		}

		protected override void OnDrag()
		{
			if (IsInVertexSnappingMode || base.Editor.Tools.IsSnapping || !GetPointOnDragPlane(Window.Pointer, out var point))
			{
				return;
			}
			Vector3 vector = m_inverse.MultiplyVector(point - m_prevPoint);
			float magnitude = vector.magnitude;
			switch (SelectedAxis)
			{
			case RuntimeHandleAxis.X:
				vector.y = (vector.z = 0f);
				break;
			case RuntimeHandleAxis.Y:
				vector.x = (vector.z = 0f);
				break;
			case RuntimeHandleAxis.Z:
				vector.x = (vector.y = 0f);
				break;
			case RuntimeHandleAxis.XY:
				vector.z = 0f;
				break;
			case RuntimeHandleAxis.XZ:
				vector.y = 0f;
				break;
			case RuntimeHandleAxis.YZ:
				vector.x = 0f;
				break;
			}
			if (SharedLockObject != null)
			{
				if (SharedLockObject.PositionX)
				{
					vector.x = 0f;
				}
				if (SharedLockObject.PositionY)
				{
					vector.y = 0f;
				}
				if (SharedLockObject.PositionZ)
				{
					vector.z = 0f;
				}
			}
			Vector3 position = Position;
			Vector3 currentPosition = m_currentPosition;
			if ((double)base.EffectiveGridUnitSize == 0.0)
			{
				vector = m_matrix.MultiplyVector(vector).normalized * magnitude;
				base.transform.position += vector;
				m_currentPosition = Position;
				m_cursorPosition = Position;
			}
			else
			{
				vector = m_matrix.MultiplyVector(vector).normalized * magnitude;
				m_cursorPosition += vector;
				Vector3 vector2 = m_cursorPosition - m_currentPosition;
				Vector3 zero = Vector3.zero;
				if (Mathf.Abs(vector2.x * 1.5f) >= base.EffectiveGridUnitSize)
				{
					zero.x = base.EffectiveGridUnitSize * Mathf.Sign(vector2.x);
				}
				if (Mathf.Abs(vector2.y * 1.5f) >= base.EffectiveGridUnitSize)
				{
					zero.y = base.EffectiveGridUnitSize * Mathf.Sign(vector2.y);
				}
				if (Mathf.Abs(vector2.z * 1.5f) >= base.EffectiveGridUnitSize)
				{
					zero.z = base.EffectiveGridUnitSize * Mathf.Sign(vector2.z);
				}
				m_currentPosition += zero;
				Position = m_currentPosition;
				if (SnapToGrid)
				{
					float sizeOfGrid = SizeOfGrid;
					if (!Mathf.Approximately(sizeOfGrid, 0f))
					{
						zero = GetGridOffset(sizeOfGrid, Position);
						m_currentPosition += zero;
						Position = m_currentPosition;
					}
				}
			}
			float num = Window.Camera.farClipPlane * 0.95f;
			if ((Position - Window.Camera.transform.position).magnitude > num)
			{
				Position = position;
				m_currentPosition = currentPosition;
			}
			else
			{
				m_prevPoint = point;
			}
		}

		private void SnapActiveTargetsToGrid()
		{
			float sizeOfGrid = SizeOfGrid;
			if (!Mathf.Approximately(sizeOfGrid, 0f))
			{
				for (int i = 0; i < ActiveTargets.Length; i++)
				{
					Transform obj = ActiveTargets[i];
					Vector3 position = obj.position;
					Vector3 gridOffset = GetGridOffset(sizeOfGrid, position);
					obj.position += gridOffset;
				}
			}
		}

		protected override void RefreshCommandBuffer(IRTECamera camera)
		{
			m_settings.Position = Position;
			m_settings.Rotation = Rotation;
			m_settings.SelectedAxis = SelectedAxis;
			m_settings.LockObject = SharedLockObject;
			Appearance.DoPositionHandle(camera.RTECommandBuffer, camera.Camera, m_settings, IsInVertexSnappingMode || base.Editor.Tools.IsSnapping);
		}

		public override RuntimeHandleAxis HitTest(out float distance)
		{
			m_matrix = Matrix4x4.TRS(Position, Rotation, Appearance.InvertZAxis ? new Vector3(1f, 1f, -1f) : Vector3.one);
			m_inverse = m_matrix.inverse;
			if (Model != null)
			{
				return Model.HitTest(Window.Pointer, out distance);
			}
			return Appearance.HitTestPositionHandle(Window.Camera, Window.Pointer, m_settings, out distance);
		}
	}
}
