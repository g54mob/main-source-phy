using System.Linq;
using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(3)]
	public class RotationHandle : BaseHandle
	{
		public float GridSize = 15f;

		public float XSpeed = 0.5f;

		public float YSpeed = 0.5f;

		private float m_deltaX;

		private float m_deltaY;

		private Vector2 m_prevPointer;

		private Quaternion m_targetInverse = Quaternion.identity;

		private Matrix4x4 m_targetInverseMatrix;

		private Vector3 m_startingRotationAxis = Vector3.zero;

		private Quaternion m_targetRotation = Quaternion.identity;

		private Quaternion m_startingRotation = Quaternion.identity;

		private Quaternion m_startinRotationInv = Quaternion.identity;

		private LockObject m_sharedLockObject;

		private Mode m_currentMode;

		private Vector3[] m_accumulatedEuler;

		private Vector3[] m_startingEuler;

		private Quaternion[] m_localRotationsBuffer;

		private ExposeToEditor[] m_exposedTargets;

		private bool m_forceScreenRotationMode;

		private RTHDrawingSettings m_settings = new RTHDrawingSettings();

		private Quaternion StartingRotation
		{
			get
			{
				if (PivotRotation != RuntimePivotRotation.Global)
				{
					return Quaternion.identity;
				}
				return m_startingRotation;
			}
		}

		private Quaternion StartingRotationInv
		{
			get
			{
				if (PivotRotation != RuntimePivotRotation.Global)
				{
					return Quaternion.identity;
				}
				return m_startinRotationInv;
			}
		}

		private Quaternion TargetRotation
		{
			get
			{
				if (PivotRotation != RuntimePivotRotation.Global)
				{
					return ActiveRealTargets[0].rotation;
				}
				return base.Target.rotation;
			}
		}

		protected override float CurrentGridUnitSize => GridSize;

		public override RuntimeTool Tool => RuntimeTool.Rotate;

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
						lockObject.RotationX = true;
						lockObject.RotationY = true;
						lockObject.RotationFree = true;
						lockObject.RotationScreen = true;
						break;
					case Mode.XZ2D:
						lockObject.RotationX = true;
						lockObject.RotationZ = true;
						lockObject.RotationFree = true;
						lockObject.RotationScreen = true;
						break;
					case Mode.YZ2D:
						lockObject.RotationY = true;
						lockObject.RotationZ = true;
						lockObject.RotationFree = true;
						lockObject.RotationScreen = true;
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

		public override Transform[] Targets
		{
			get
			{
				return base.Targets;
			}
			set
			{
				base.Targets = value;
				if (ActiveRealTargets != null)
				{
					m_exposedTargets = ActiveRealTargets.Select((Transform t) => t.GetComponent<ExposeToEditor>()).ToArray();
					m_startingEuler = new Vector3[m_exposedTargets.Length];
					m_accumulatedEuler = new Vector3[m_exposedTargets.Length];
					m_localRotationsBuffer = new Quaternion[m_exposedTargets.Length];
				}
				else
				{
					m_exposedTargets = null;
					m_startingEuler = null;
					m_accumulatedEuler = null;
					m_localRotationsBuffer = null;
				}
			}
		}

		protected override void Start()
		{
			base.Start();
			OnPivotRotationChanged();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			OnPivotRotationChanged();
			base.Editor.Tools.PivotRotationChanged += OnPivotRotationChanged;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			base.Editor.Tools.PivotRotationChanged -= OnPivotRotationChanged;
		}

		protected override void UpdateOverride()
		{
			base.UpdateOverride();
			if (base.Editor.Tools.IsViewing)
			{
				SelectedAxis = RuntimeHandleAxis.None;
			}
			else if (base.IsWindowActive && Window.IsPointerOver && !IsDragging)
			{
				UpdateMatricesAndRotations(forceUpdate: false);
			}
		}

		protected override void OnPivotRotationChanged()
		{
			UpdateMatricesAndRotations(forceUpdate: true);
			base.OnPivotRotationChanged();
		}

		private void UpdateMatricesAndRotations(bool forceUpdate)
		{
			if (!(base.Target == null))
			{
				if (HightlightOnHover)
				{
					m_targetInverseMatrix = Matrix4x4.TRS(base.Target.position, TargetRotation * StartingRotationInv, Vector3.one).inverse;
				}
				if (forceUpdate || m_targetRotation != TargetRotation)
				{
					m_startingRotation = TargetRotation;
					m_startinRotationInv = Quaternion.Inverse(m_startingRotation);
					m_targetRotation = TargetRotation;
				}
			}
		}

		private bool ForceScreenRotationMode()
		{
			if (SelectedAxis == RuntimeHandleAxis.Free)
			{
				return false;
			}
			if (SelectedAxis == RuntimeHandleAxis.X)
			{
				return (double)Mathf.Abs(Vector3.Dot(Window.Camera.transform.forward, base.Target.rotation * StartingRotationInv * Vector3.right)) > 0.8;
			}
			if (SelectedAxis == RuntimeHandleAxis.Y)
			{
				return (double)Mathf.Abs(Vector3.Dot(Window.Camera.transform.forward, base.Target.rotation * StartingRotationInv * Vector3.up)) > 0.8;
			}
			if (SelectedAxis == RuntimeHandleAxis.Z)
			{
				return (double)Mathf.Abs(Vector3.Dot(Window.Camera.transform.forward, base.Target.rotation * StartingRotationInv * Vector3.forward)) > 0.8;
			}
			return false;
		}

		private Quaternion ScreenRotation(Vector3 delta)
		{
			Vector3 vector = m_targetInverseMatrix.MultiplyVector(Window.Camera.cameraToWorldMatrix.MultiplyVector(-Vector3.forward));
			if (SelectedAxis == RuntimeHandleAxis.Screen)
			{
				Quaternion result = Quaternion.AngleAxis(delta.x, vector);
				if (SharedLockObject == null || !SharedLockObject.RotationScreen)
				{
					return result;
				}
			}
			else if (SelectedAxis == RuntimeHandleAxis.X)
			{
				Vector3 vector2 = Quaternion.Inverse(base.Target.rotation) * (base.Target.rotation * StartingRotationInv * Vector3.right);
				Quaternion result2 = Quaternion.AngleAxis(delta.x * Mathf.Sign(Vector3.Dot(vector2, vector)), vector2);
				if (SharedLockObject == null || !SharedLockObject.RotationX)
				{
					return result2;
				}
			}
			else if (SelectedAxis == RuntimeHandleAxis.Y)
			{
				Vector3 vector3 = Quaternion.Inverse(base.Target.rotation) * (base.Target.rotation * StartingRotationInv * Vector3.up);
				Quaternion result3 = Quaternion.AngleAxis(delta.x * Mathf.Sign(Vector3.Dot(vector3, vector)), vector3);
				if (SharedLockObject == null || !SharedLockObject.RotationY)
				{
					return result3;
				}
			}
			else if (SelectedAxis == RuntimeHandleAxis.Z)
			{
				Vector3 vector4 = Quaternion.Inverse(base.Target.rotation) * (base.Target.rotation * StartingRotationInv * Vector3.forward);
				Quaternion result4 = Quaternion.AngleAxis(delta.x * Mathf.Sign(Vector3.Dot(vector4, vector)), vector4);
				if (SharedLockObject == null || !SharedLockObject.RotationZ)
				{
					return result4;
				}
			}
			return Quaternion.identity;
		}

		protected override bool OnBeginDrag()
		{
			if (base.Target == null)
			{
				return false;
			}
			m_targetRotation = base.Target.rotation;
			m_targetInverseMatrix = Matrix4x4.TRS(base.Target.position, base.Target.rotation * StartingRotationInv, Vector3.one).inverse;
			if (!base.OnBeginDrag())
			{
				return false;
			}
			m_deltaX = 0f;
			m_deltaY = 0f;
			if (Window.Pointer.XY(base.Target.position, out var result))
			{
				m_prevPointer = result;
			}
			else
			{
				SelectedAxis = RuntimeHandleAxis.None;
			}
			m_forceScreenRotationMode = ForceScreenRotationMode();
			if (SelectedAxis == RuntimeHandleAxis.Screen || m_forceScreenRotationMode)
			{
				if (Window.Pointer.WorldToScreenPoint(base.Target.position, base.Target.position, out var result2))
				{
					if (Window.Pointer.XY(base.Target.position, out result))
					{
						float num = Mathf.Atan2(result.y - result2.y, result.x - result2.x);
						m_targetInverse = Quaternion.Inverse(Quaternion.AngleAxis(57.29578f * num, Vector3.forward));
						m_targetInverseMatrix = Matrix4x4.TRS(base.Target.position, base.Target.rotation, Vector3.one).inverse;
						m_prevPointer = result;
					}
					else
					{
						SelectedAxis = RuntimeHandleAxis.None;
					}
				}
				else
				{
					SelectedAxis = RuntimeHandleAxis.None;
				}
			}
			else
			{
				if (SelectedAxis == RuntimeHandleAxis.X)
				{
					m_startingRotationAxis = base.Target.rotation * Quaternion.Inverse(StartingRotation) * Vector3.right;
				}
				else if (SelectedAxis == RuntimeHandleAxis.Y)
				{
					m_startingRotationAxis = base.Target.rotation * Quaternion.Inverse(StartingRotation) * Vector3.up;
				}
				else if (SelectedAxis == RuntimeHandleAxis.Z)
				{
					m_startingRotationAxis = base.Target.rotation * Quaternion.Inverse(StartingRotation) * Vector3.forward;
				}
				m_targetInverse = Quaternion.Inverse(base.Target.rotation);
			}
			for (int i = 0; i < m_exposedTargets.Length; i++)
			{
				if (m_exposedTargets[i] != null)
				{
					m_startingEuler[i] = m_exposedTargets[i].LocalEuler;
				}
			}
			return SelectedAxis != RuntimeHandleAxis.None;
		}

		protected override void OnDrag()
		{
			base.OnDrag();
			if (!Window.Pointer.XY(base.Target.position, out var result))
			{
				return;
			}
			float num = result.x - m_prevPointer.x;
			float num2 = result.y - m_prevPointer.y;
			m_prevPointer = result;
			num *= XSpeed;
			num2 *= YSpeed;
			m_deltaX += num;
			m_deltaY += num2;
			if (!Window.Pointer.ToWorldMatrix(base.Target.position, out var matrix))
			{
				return;
			}
			Vector3 vector = StartingRotation * Quaternion.Inverse(base.Target.rotation) * matrix.MultiplyVector(new Vector3(m_deltaY, 0f - m_deltaX, 0f));
			Quaternion quaternion;
			if (SelectedAxis == RuntimeHandleAxis.Screen || m_forceScreenRotationMode)
			{
				vector = m_targetInverse * new Vector3(m_deltaY, 0f - m_deltaX, 0f);
				if (base.EffectiveGridUnitSize != 0f)
				{
					if (Mathf.Abs(vector.x) >= base.EffectiveGridUnitSize)
					{
						vector.x = Mathf.Sign(vector.x) * base.EffectiveGridUnitSize;
						m_deltaX = 0f;
						m_deltaY = 0f;
					}
					else
					{
						vector.x = 0f;
					}
				}
				quaternion = ScreenRotation(vector);
			}
			else if (SelectedAxis == RuntimeHandleAxis.X)
			{
				Vector3 axis = Quaternion.Inverse(base.Target.rotation) * m_startingRotationAxis;
				if (base.EffectiveGridUnitSize != 0f)
				{
					if (Mathf.Abs(vector.x) >= base.EffectiveGridUnitSize)
					{
						vector.x = Mathf.Sign(vector.x) * base.EffectiveGridUnitSize;
						m_deltaX = 0f;
						m_deltaY = 0f;
					}
					else
					{
						vector.x = 0f;
					}
				}
				if (SharedLockObject != null && SharedLockObject.RotationX)
				{
					vector.x = 0f;
				}
				quaternion = Quaternion.AngleAxis(vector.x, axis);
			}
			else if (SelectedAxis == RuntimeHandleAxis.Y)
			{
				Vector3 axis2 = Quaternion.Inverse(base.Target.rotation) * m_startingRotationAxis;
				if (base.EffectiveGridUnitSize != 0f)
				{
					if (Mathf.Abs(vector.y) >= base.EffectiveGridUnitSize)
					{
						vector.y = Mathf.Sign(vector.y) * base.EffectiveGridUnitSize;
						m_deltaX = 0f;
						m_deltaY = 0f;
					}
					else
					{
						vector.y = 0f;
					}
				}
				if (SharedLockObject != null && SharedLockObject.RotationY)
				{
					vector.y = 0f;
				}
				quaternion = Quaternion.AngleAxis(vector.y, axis2);
			}
			else if (SelectedAxis == RuntimeHandleAxis.Z)
			{
				Vector3 axis3 = Quaternion.Inverse(base.Target.rotation) * m_startingRotationAxis;
				if (base.EffectiveGridUnitSize != 0f)
				{
					if (Mathf.Abs(vector.z) >= base.EffectiveGridUnitSize)
					{
						vector.z = Mathf.Sign(vector.z) * base.EffectiveGridUnitSize;
						m_deltaX = 0f;
						m_deltaY = 0f;
					}
					else
					{
						vector.z = 0f;
					}
				}
				if (SharedLockObject != null && SharedLockObject.RotationZ)
				{
					vector.z = 0f;
				}
				quaternion = Quaternion.AngleAxis(vector.z, axis3);
			}
			else
			{
				vector = StartingRotationInv * vector;
				if (SharedLockObject != null && SharedLockObject.RotationFree)
				{
					vector.x = 0f;
					vector.y = 0f;
					vector.z = 0f;
				}
				quaternion = Quaternion.Euler(vector.x, vector.y, vector.z);
				m_deltaX = 0f;
				m_deltaY = 0f;
			}
			if (base.EffectiveGridUnitSize == 0f)
			{
				m_deltaX = 0f;
				m_deltaY = 0f;
			}
			for (int i = 0; i < m_exposedTargets.Length; i++)
			{
				ExposeToEditor exposeToEditor = m_exposedTargets[i];
				if (exposeToEditor != null)
				{
					m_localRotationsBuffer[i] = exposeToEditor.transform.localRotation;
				}
			}
			for (int j = 0; j < ActiveTargets.Length; j++)
			{
				ActiveTargets[j].rotation *= quaternion;
			}
			for (int k = 0; k < m_exposedTargets.Length; k++)
			{
				ExposeToEditor exposeToEditor2 = m_exposedTargets[k];
				if (exposeToEditor2 != null)
				{
					Quaternion localRotation = exposeToEditor2.transform.localRotation;
					Vector3 eulerAngles = (Quaternion.Inverse(m_localRotationsBuffer[k]) * localRotation).eulerAngles;
					eulerAngles.x = ((eulerAngles.x < 180f) ? eulerAngles.x : (-360f + eulerAngles.x));
					eulerAngles.y = ((eulerAngles.y < 180f) ? eulerAngles.y : (-360f + eulerAngles.y));
					eulerAngles.z = ((eulerAngles.z < 180f) ? eulerAngles.z : (-360f + eulerAngles.z));
					m_accumulatedEuler[k] += eulerAngles;
					exposeToEditor2.SetLocalEulerAngles(m_startingEuler[k] + m_accumulatedEuler[k]);
				}
			}
		}

		protected override void OnDrop()
		{
			base.OnDrop();
			m_targetRotation = base.Target.rotation;
			for (int i = 0; i < m_exposedTargets.Length; i++)
			{
				if (m_exposedTargets[i] != null)
				{
					m_exposedTargets[i].LocalEuler = m_startingEuler[i] + m_accumulatedEuler[i];
				}
				m_accumulatedEuler[i] = Vector3.zero;
			}
			OnPivotRotationChanged();
		}

		protected override void SyncModelTransform()
		{
			base.SyncModelTransform();
			if (base.Target != null)
			{
				Model.transform.rotation = base.Target.rotation * StartingRotationInv;
			}
		}

		protected override void RefreshCommandBuffer(IRTECamera camera)
		{
			m_settings.Position = base.Target.position;
			m_settings.Rotation = base.Target.rotation * StartingRotationInv;
			m_settings.SelectedAxis = SelectedAxis;
			m_settings.LockObject = SharedLockObject;
			Appearance.DoRotationHandle(camera.RTECommandBuffer, camera.Camera, m_settings, base.Editor.IsVR);
		}

		public override RuntimeHandleAxis HitTest(out float distance)
		{
			if (Model != null)
			{
				return Model.HitTest(Window.Pointer, out distance);
			}
			return Appearance.HitTestRotationHandle(Window.Camera, Window.Pointer, m_settings, out distance);
		}
	}
}
