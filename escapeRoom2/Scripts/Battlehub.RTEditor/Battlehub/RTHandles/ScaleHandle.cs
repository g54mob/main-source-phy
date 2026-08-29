using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(2)]
	public class ScaleHandle : BaseHandle
	{
		public bool AbsoluteGrid;

		public float GridSize = 0.1f;

		public Vector3 MinScale = new Vector3(float.MinValue, float.MinValue, float.MinValue);

		private Vector3 m_prevPoint;

		private Matrix4x4 m_matrix;

		private Matrix4x4 m_inverse;

		private Vector3 m_roundedScale;

		private Vector3 m_scale;

		private Vector3[] m_refScales;

		private float m_screenScale;

		private LockObject m_sharedLockObject;

		private Mode m_currentMode;

		[SerializeField]
		private bool m_isUniform;

		private RTHDrawingSettings m_settings = new RTHDrawingSettings();

		public override bool SnapToGrid
		{
			get
			{
				return AbsoluteGrid;
			}
			set
			{
				AbsoluteGrid = value;
			}
		}

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

		public override RuntimeTool Tool => RuntimeTool.Scale;

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
						lockObject.ScaleZ = true;
						break;
					case Mode.XZ2D:
						lockObject.ScaleY = true;
						break;
					case Mode.YZ2D:
						lockObject.ScaleX = true;
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

		public bool IsUniform
		{
			get
			{
				return m_isUniform;
			}
			set
			{
				if (m_isUniform != value)
				{
					m_isUniform = value;
					TryUpdateModelProperties();
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			m_scale = Vector3.one;
			m_roundedScale = m_scale;
		}

		protected override void Start()
		{
			base.Start();
			TryUpdateModelProperties();
		}

		protected override void UpdateOverride()
		{
			base.UpdateOverride();
			UpdateCurrentMode();
		}

		protected override bool OnBeginDrag()
		{
			if (!base.OnBeginDrag())
			{
				return false;
			}
			if (SelectedAxis == RuntimeHandleAxis.Free)
			{
				DragPlane = GetDragPlane(Vector3.zero);
			}
			else if (SelectedAxis == RuntimeHandleAxis.None)
			{
				return false;
			}
			m_refScales = new Vector3[ActiveTargets.Length];
			for (int i = 0; i < m_refScales.Length; i++)
			{
				Quaternion quaternion = ((PivotRotation == RuntimePivotRotation.Global) ? ActiveTargets[i].rotation : Quaternion.identity);
				m_refScales[i] = quaternion * ActiveTargets[i].localScale;
			}
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
			if (SelectedAxis == RuntimeHandleAxis.Free)
			{
				m_prevPoint = Window.Pointer.ScreenPoint;
				return true;
			}
			DragPlane = GetDragPlane(axis);
			bool pointOnDragPlane = GetPointOnDragPlane(Window.Pointer, out m_prevPoint);
			if (!pointOnDragPlane)
			{
				SelectedAxis = RuntimeHandleAxis.None;
			}
			return pointOnDragPlane;
		}

		protected override void OnDrag()
		{
			base.OnDrag();
			if (SelectedAxis == RuntimeHandleAxis.Free)
			{
				Vector2 vector = Window.Pointer.ScreenPoint - (Vector2)m_prevPoint;
				m_prevPoint = Window.Pointer.ScreenPoint;
				Rect rect = Window.ViewRoot.rect;
				float num = Mathf.Max(rect.width, rect.height);
				float num2 = vector.magnitude / (num * 0.25f);
				float num3 = ((Mathf.Abs(vector.x) > Mathf.Abs(vector.y)) ? Mathf.Sign(vector.x) : Mathf.Sign(vector.y));
				if (SharedLockObject != null)
				{
					if (!SharedLockObject.ScaleX)
					{
						m_scale.x += num3 * num2;
					}
					if (!SharedLockObject.ScaleY)
					{
						m_scale.y += num3 * num2;
					}
					if (!SharedLockObject.ScaleZ)
					{
						m_scale.z += num3 * num2;
					}
				}
				else
				{
					m_scale.x += num3 * num2;
					m_scale.y += num3 * num2;
					m_scale.z += num3 * num2;
				}
			}
			else
			{
				if (!GetPointOnDragPlane(Window.Pointer, out var point))
				{
					return;
				}
				Vector3 vector2 = (point - m_prevPoint) / m_screenScale;
				m_prevPoint = point;
				Vector3 vector3 = m_inverse.MultiplyVector(vector2);
				float magnitude = vector3.magnitude;
				if (SelectedAxis == RuntimeHandleAxis.X)
				{
					if (SharedLockObject == null || !SharedLockObject.ScaleX)
					{
						float num4 = Mathf.Sign(vector3.x) * magnitude;
						if (IsUniform)
						{
							m_scale += Vector3.one * num4;
						}
						else
						{
							m_scale.x += num4;
						}
					}
				}
				else if (SelectedAxis == RuntimeHandleAxis.Y)
				{
					if (SharedLockObject == null || !SharedLockObject.ScaleY)
					{
						float num5 = Mathf.Sign(vector3.y) * magnitude;
						if (IsUniform)
						{
							m_scale += Vector3.one * num5;
						}
						else
						{
							m_scale.y += num5;
						}
					}
				}
				else if (SelectedAxis == RuntimeHandleAxis.Z && (SharedLockObject == null || !SharedLockObject.ScaleZ))
				{
					float num6 = Mathf.Sign(vector3.z) * magnitude;
					if (IsUniform)
					{
						m_scale += Vector3.one * num6;
					}
					else
					{
						m_scale.z += num6;
					}
				}
			}
			if (SnapToGrid)
			{
				for (int i = 0; i < m_refScales.Length; i++)
				{
					Quaternion rotation = ((PivotRotation == RuntimePivotRotation.Global) ? Targets[i].rotation : Quaternion.identity);
					float num7 = base.EffectiveGridUnitSize * 2f;
					m_roundedScale = Vector3.Scale(m_refScales[i], m_scale);
					if ((double)base.EffectiveGridUnitSize > 0.01)
					{
						m_roundedScale.x = (float)Mathf.RoundToInt(m_roundedScale.x / num7) * num7;
						m_roundedScale.y = (float)Mathf.RoundToInt(m_roundedScale.y / num7) * num7;
						m_roundedScale.z = (float)Mathf.RoundToInt(m_roundedScale.z / num7) * num7;
					}
					Vector3 localScale = Quaternion.Inverse(rotation) * m_roundedScale;
					localScale.x = Mathf.Max(MinScale.x, localScale.x);
					localScale.y = Mathf.Max(MinScale.y, localScale.y);
					localScale.z = Mathf.Max(MinScale.z, localScale.z);
					ActiveTargets[i].localScale = localScale;
				}
				if (Model != null)
				{
					Model.SetScale(m_scale);
				}
			}
			else
			{
				m_roundedScale = m_scale;
				if ((double)base.EffectiveGridUnitSize > 0.01)
				{
					m_roundedScale.x = (float)Mathf.RoundToInt(m_roundedScale.x / base.EffectiveGridUnitSize) * base.EffectiveGridUnitSize;
					m_roundedScale.y = (float)Mathf.RoundToInt(m_roundedScale.y / base.EffectiveGridUnitSize) * base.EffectiveGridUnitSize;
					m_roundedScale.z = (float)Mathf.RoundToInt(m_roundedScale.z / base.EffectiveGridUnitSize) * base.EffectiveGridUnitSize;
				}
				if (Model != null)
				{
					Model.SetScale(m_roundedScale);
				}
				for (int j = 0; j < m_refScales.Length; j++)
				{
					Vector3 localScale2 = Quaternion.Inverse((PivotRotation == RuntimePivotRotation.Global) ? Targets[j].rotation : Quaternion.identity) * Vector3.Scale(m_refScales[j], m_roundedScale);
					localScale2.x = Mathf.Max(MinScale.x, localScale2.x);
					localScale2.y = Mathf.Max(MinScale.y, localScale2.y);
					localScale2.z = Mathf.Max(MinScale.z, localScale2.z);
					ActiveTargets[j].localScale = localScale2;
				}
			}
		}

		protected override void OnDrop()
		{
			base.OnDrop();
			m_scale = Vector3.one;
			m_roundedScale = m_scale;
			if (Model != null)
			{
				Model.SetScale(m_roundedScale);
			}
		}

		protected override void RefreshCommandBuffer(IRTECamera camera)
		{
			m_settings.Position = base.Target.position;
			m_settings.Rotation = Rotation;
			m_settings.Scale = m_roundedScale;
			m_settings.SelectedAxis = SelectedAxis;
			m_settings.LockObject = SharedLockObject;
			Appearance.DoScaleHandle(camera.RTECommandBuffer, camera.Camera, m_settings, IsUniform);
		}

		public override RuntimeHandleAxis HitTest(out float distance)
		{
			m_screenScale = RuntimeHandlesComponent.GetScreenScale(base.transform.position, Window.Camera) * Appearance.HandleScale;
			m_matrix = Matrix4x4.TRS(base.transform.position, Rotation, Appearance.InvertZAxis ? new Vector3(1f, 1f, -1f) : Vector3.one);
			m_inverse = m_matrix.inverse;
			if (!(Model != null))
			{
				return Appearance.HitTestScaleHandle(Window.Camera, Window.Pointer, m_settings, out distance);
			}
			return Model.HitTest(Window.Pointer, out distance);
		}

		private void TryUpdateModelProperties()
		{
			ScaleHandleModel scaleHandleModel = Model as ScaleHandleModel;
			if (scaleHandleModel != null)
			{
				scaleHandleModel.IsUniform = IsUniform;
			}
		}
	}
}
