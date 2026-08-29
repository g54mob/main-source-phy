using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class MobileSceneInput : RuntimeSelectionInputBase
	{
		public float RotateSensitivity = 0.25f;

		public float ZoomSensitivity = 0.005f;

		public float MoveSensitivity = 0.1f;

		[SerializeField]
		private MobileSceneControls m_sceneControls;

		private bool m_isActive;

		private MobileSceneControls.Mode m_mode;

		private bool m_zoom;

		private bool m_pan;

		private bool m_rotate;

		private bool m_orbit;

		private bool m_cameraTransformChanged;

		private bool m_isDragging;

		private Vector3 m_defaultCameraPosition;

		private Vector3 m_defaultPivot;

		private Vector3 m_betweenTouches;

		private Vector3 m_prevCameraPosition;

		private Vector3 m_prevPivot;

		private Vector2 m_prevScreenPoint;

		protected RuntimeSceneComponent SceneComponent => (RuntimeSceneComponent)m_component;

		protected override void Start()
		{
			base.Start();
			m_defaultCameraPosition = SceneComponent.CameraPosition;
			m_defaultPivot = SceneComponent.Pivot;
			SceneComponent.BoxSelection.Selection += OnBoxSelection;
			SceneComponent.Editor.ActiveWindowChanged += Editor_ActiveWindowChanged;
			if (m_sceneControls == null)
			{
				m_sceneControls = Resources.Load<MobileSceneControls>("RTH_MobileSceneControls");
				if (m_sceneControls != null)
				{
					RuntimeCameraWindow runtimeCameraWindow = (RuntimeCameraWindow)SceneComponent.Window;
					m_sceneControls = Object.Instantiate(m_sceneControls, runtimeCameraWindow.ViewRoot, worldPositionStays: false);
				}
			}
			else if (m_sceneControls.gameObject.IsPrefab())
			{
				RuntimeCameraWindow runtimeCameraWindow2 = (RuntimeCameraWindow)SceneComponent.Window;
				m_sceneControls = Object.Instantiate(m_sceneControls, runtimeCameraWindow2.ViewRoot, worldPositionStays: false);
			}
			if (m_sceneControls != null)
			{
				m_sceneControls.Focus += OnFocus;
				m_sceneControls.ModeChanged += OnModeChanged;
				m_sceneControls.ResetPosition += OnResetPosition;
				m_mode = m_sceneControls.CurrentMode;
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (SceneComponent != null)
			{
				if (SceneComponent.Editor != null)
				{
					SceneComponent.Editor.ActiveWindowChanged -= Editor_ActiveWindowChanged;
				}
				SceneComponent.BoxSelection.Selection -= OnBoxSelection;
			}
			if (m_sceneControls != null)
			{
				m_sceneControls.Focus -= OnFocus;
				m_sceneControls.ModeChanged -= OnModeChanged;
				m_sceneControls.ResetPosition -= OnResetPosition;
			}
		}

		private void Editor_ActiveWindowChanged(RuntimeWindow deactivatedWindow)
		{
			if (SceneComponent != null)
			{
				if (m_isActive)
				{
					m_zoom = false;
				}
				m_isActive = SceneComponent.IsWindowActive;
			}
		}

		private void OnFocus()
		{
			SceneComponent.Focus(FocusMode.Selected);
		}

		protected override void OnBoxSelection(object sender, BoxSelectionArgs e)
		{
			if (m_sceneControls != null)
			{
				m_sceneControls.Cancel();
			}
			base.OnBoxSelection(sender, e);
		}

		private void OnModeChanged(MobileSceneControls.Mode mode)
		{
			m_mode = mode;
			SceneComponent.IsBoxSelectionEnabled = m_mode == MobileSceneControls.Mode.BoxSelection;
		}

		private void OnResetPosition()
		{
			SceneComponent.SecondaryPivot = m_defaultPivot;
			SceneComponent.Pivot = m_defaultPivot;
			SceneComponent.CameraPosition = m_defaultCameraPosition;
		}

		protected override void SelectGO()
		{
			RuntimeTools tools = m_component.Editor.Tools;
			IRuntimeSelection selection = m_component.Selection;
			if ((!(tools.ActiveTool != null) || !(tools.ActiveTool != m_component.BoxSelection)) && selection.Enabled)
			{
				OnSelectGO();
			}
		}

		protected override void BeginSelectAction()
		{
			if (SceneComponent.Window.IsPointerOver)
			{
				base.BeginSelectAction();
			}
		}

		protected virtual bool BeginDrag()
		{
			if (!SceneComponent.Window.IsPointerOver)
			{
				return false;
			}
			ITouchInput touchInput = SceneComponent.Editor.TouchInput;
			if (touchInput.TouchCount > 0)
			{
				return touchInput.GetTouch(0).phase == TouchPhase.Began;
			}
			return false;
		}

		protected virtual bool EndDrag()
		{
			ITouchInput touchInput = SceneComponent.Editor.TouchInput;
			if (touchInput.TouchCount == 0)
			{
				return true;
			}
			TouchPhase phase = touchInput.GetTouch(0).phase;
			if (phase != TouchPhase.Ended)
			{
				return phase == TouchPhase.Canceled;
			}
			return true;
		}

		protected virtual bool MoveAction()
		{
			if (!SceneComponent.Window.IsPointerOver || !m_isDragging)
			{
				return false;
			}
			ITouchInput touchInput = SceneComponent.Editor.TouchInput;
			if (touchInput.TouchCount == 1 && touchInput.GetTouch(0).tapCount == 2)
			{
				return !SceneComponent.IsOrthographic;
			}
			return false;
		}

		protected virtual bool PanAction()
		{
			if (!SceneComponent.Window.IsPointerOver || !m_isDragging)
			{
				return false;
			}
			if (SceneComponent.Editor.TouchInput.TouchCount == 1 && (m_mode == MobileSceneControls.Mode.Pan || SceneComponent.IsOrthographic))
			{
				return !SceneComponent.IsBoxSelectionEnabled;
			}
			return false;
		}

		protected virtual bool RotateAction()
		{
			if (!SceneComponent.Window.IsPointerOver || !m_isDragging)
			{
				return false;
			}
			ITouchInput touchInput = SceneComponent.Editor.TouchInput;
			if (m_mode == MobileSceneControls.Mode.View && touchInput.TouchCount == 1)
			{
				return !SceneComponent.IsOrthographic;
			}
			return false;
		}

		protected virtual bool OrbitAction()
		{
			if (!SceneComponent.Window.IsPointerOver || !m_isDragging)
			{
				return false;
			}
			ITouchInput touchInput = SceneComponent.Editor.TouchInput;
			if (m_mode == MobileSceneControls.Mode.Orbit && touchInput.TouchCount == 1)
			{
				return !SceneComponent.IsOrthographic;
			}
			return false;
		}

		protected virtual bool ZoomAction()
		{
			if (!SceneComponent.Window.IsPointerOver || !m_isDragging)
			{
				return false;
			}
			return SceneComponent.Editor.TouchInput.TouchCount == 2;
		}

		protected virtual Vector2 RotateAxes()
		{
			return SceneComponent.Window.Pointer.ScreenPoint - m_prevScreenPoint;
		}

		protected virtual float ZoomAxis()
		{
			ITouchInput touchInput = SceneComponent.Editor.TouchInput;
			Vector3 vector = touchInput.GetTouch(0).position;
			Vector3 vector2 = touchInput.GetTouch(1).position;
			Vector3 betweenTouches = vector - vector2;
			float result = betweenTouches.magnitude - m_betweenTouches.magnitude;
			m_betweenTouches = betweenTouches;
			return result;
		}

		protected override void LateUpdate()
		{
			if (m_sceneControls != null)
			{
				m_sceneControls.IsOrthographicMode = SceneComponent.IsOrthographic;
			}
			RuntimeSceneComponent sceneComponent = SceneComponent;
			if (!sceneComponent.IsWindowActive)
			{
				m_isDragging = false;
				return;
			}
			RuntimeWindow window = sceneComponent.Window;
			ITouchInput touchInput = sceneComponent.Editor.TouchInput;
			RuntimeTools tools = sceneComponent.Editor.Tools;
			if (tools.ActiveTool != null)
			{
				m_isDragging = false;
				return;
			}
			if (m_isDragging)
			{
				if (EndDrag())
				{
					m_isDragging = false;
				}
			}
			else if (BeginDrag())
			{
				m_isDragging = true;
			}
			bool flag = ZoomAction();
			bool flag2 = flag != m_zoom && (m_zoom = flag);
			bool flag3 = PanAction();
			bool flag4 = flag3 != m_pan && (m_pan = flag3);
			bool flag5 = RotateAction();
			bool flag6 = flag5 != m_rotate && (m_rotate = flag5);
			bool flag7 = OrbitAction();
			bool flag8 = flag7 != m_orbit && (m_orbit = flag7);
			bool flag9 = MoveAction();
			bool isViewing = tools.IsViewing;
			tools.IsViewing = flag3 || flag5 || flag7 || flag;
			if (tools.IsViewing && tools.IsViewing != isViewing)
			{
				ResetCameraTransformChanged();
			}
			if (!m_cameraTransformChanged)
			{
				Vector3 vector = m_prevPivot - m_prevCameraPosition;
				Vector3 vector2 = sceneComponent.Pivot - sceneComponent.CameraPosition;
				if (!Mathf.Approximately(vector.magnitude, vector2.magnitude) || Vector3.Dot(vector.normalized, vector2.normalized) < 0.999f)
				{
					m_cameraTransformChanged = true;
				}
				if (flag3 && !MathHelper.Approximately(m_prevCameraPosition, sceneComponent.CameraPosition))
				{
					m_cameraTransformChanged = true;
				}
			}
			BeginSelectAction();
			if (!m_cameraTransformChanged && SelectAction())
			{
				SelectGO();
			}
			if (flag)
			{
				if (flag2)
				{
					m_betweenTouches = touchInput.GetTouch(0).position - touchInput.GetTouch(1).position;
				}
				float deltaZ = ZoomAxis() * ZoomSensitivity;
				if (m_mode == MobileSceneControls.Mode.Orbit)
				{
					sceneComponent.Orbit(0f, 0f, deltaZ);
				}
				else
				{
					sceneComponent.Zoom(deltaZ, Quaternion.FromToRotation(Vector3.forward, window.Camera.transform.InverseTransformVector(window.Pointer.Ray.direction).normalized));
				}
				sceneComponent.FreeMove(Vector2.zero, Vector3.zero, 0f);
			}
			else if (flag3)
			{
				if (flag4)
				{
					sceneComponent.BeginPan(window.Pointer.ScreenPoint);
				}
				sceneComponent.Pan(window.Pointer.ScreenPoint);
			}
			else if (flag5)
			{
				if (flag6)
				{
					m_prevScreenPoint = window.Pointer.ScreenPoint;
				}
				Vector3 move = Vector3.zero;
				if (flag9)
				{
					move = Vector3.up * MoveSensitivity;
				}
				Vector2 rotate = RotateAxes() * RotateSensitivity;
				sceneComponent.FreeMove(rotate, move, 0f);
			}
			else if (flag7)
			{
				if (flag8)
				{
					m_prevScreenPoint = window.Pointer.ScreenPoint;
				}
				Vector2 vector3 = RotateAxes() * RotateSensitivity;
				sceneComponent.Orbit(vector3.x, vector3.y, 0f);
				sceneComponent.FreeMove(Vector2.zero, Vector3.zero, 0f);
			}
			else
			{
				sceneComponent.FreeMove(Vector2.zero, Vector3.zero, 0f);
			}
			m_prevScreenPoint = window.Pointer.ScreenPoint;
		}

		private void ResetCameraTransformChanged()
		{
			m_prevPivot = SceneComponent.Pivot;
			m_prevCameraPosition = SceneComponent.CameraPosition;
			m_cameraTransformChanged = false;
		}
	}
}
