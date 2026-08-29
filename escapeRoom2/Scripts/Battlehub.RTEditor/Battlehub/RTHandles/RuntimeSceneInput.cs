using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.UIElements;

namespace Battlehub.RTHandles
{
	public class RuntimeSceneInput : RuntimeSelectionInput
	{
		public KeyCode FocusKey = KeyCode.F;

		public KeyCode FocusActiveKey = KeyCode.LeftShift;

		public KeyCode SnapToGridKey = KeyCode.G;

		public KeyCode SnapToGridKey2 = KeyCode.LeftShift;

		public KeyCode RotateKey = KeyCode.LeftAlt;

		public KeyCode RotateKey2 = KeyCode.RightAlt;

		public KeyCode RotateKey3 = KeyCode.AltGr;

		public KeyCode FreeMoveZoomKey = KeyCode.LeftAlt;

		public KeyCode FreeMoveZoomKey2 = KeyCode.RightAlt;

		public KeyCode FreeMoveZoomKey3 = KeyCode.AltGr;

		public KeyCode MoveDownKey = KeyCode.Q;

		public KeyCode MoveUpKey = KeyCode.E;

		public MouseButton PanButton = MouseButton.MiddleMouse;

		public float RotateXSensitivity = 5f;

		public float RotateYSensitivity = 5f;

		public float MoveZSensitivity = 1f;

		public float FreeMoveSensitivity = 1f;

		public float FreeZoomSensitivity = 1f;

		public float FreeRotateSensitivity = 5f;

		public bool SwapLRMB;

		[SerializeField]
		private bool m_beginRotateImmediately = true;

		[SerializeField]
		private bool m_beginFreeMoveImmediately = true;

		private bool m_rotate;

		private bool m_rotateActive;

		private bool m_pan;

		private bool m_freeMove;

		private bool m_freeMoveActive;

		private bool m_isActive;

		protected bool BeginRotateImmediately
		{
			get
			{
				return m_beginRotateImmediately;
			}
			set
			{
				m_beginRotateImmediately = value;
			}
		}

		protected bool BeginFreeMoveImmediately
		{
			get
			{
				return m_beginFreeMoveImmediately;
			}
			set
			{
				m_beginFreeMoveImmediately = value;
			}
		}

		protected RuntimeSceneComponent SceneComponent => (RuntimeSceneComponent)m_component;

		protected virtual bool AllowRotateAction()
		{
			return m_component.Editor.Input.GetPointer(SwapLRMB ? 1 : 0);
		}

		protected virtual bool RotateAction()
		{
			IInput input = m_component.Editor.Input;
			if (!input.GetKey(RotateKey) && !input.GetKey(RotateKey2))
			{
				return input.GetKey(RotateKey3);
			}
			return true;
		}

		protected virtual bool FreeMoveZoomAction()
		{
			IInput input = m_component.Editor.Input;
			if (!input.GetKey(RotateKey) && !input.GetKey(RotateKey2))
			{
				return input.GetKey(RotateKey3);
			}
			return true;
		}

		protected virtual bool PanAction()
		{
			IInput input = m_component.Editor.Input;
			RuntimeTools tools = m_component.Editor.Tools;
			if (!input.GetPointer((int)PanButton))
			{
				if (input.GetPointer(SwapLRMB ? 1 : 0) && tools.Current == RuntimeTool.View)
				{
					return tools.ActiveTool == null;
				}
				return false;
			}
			return true;
		}

		protected virtual bool FreeMoveAction()
		{
			IInput input = m_component.Editor.Input;
			if (SwapLRMB)
			{
				if (input.GetPointer(0))
				{
					return RotateAction();
				}
				return false;
			}
			return input.GetPointer(1);
		}

		protected virtual bool FocusAction()
		{
			return m_component.Editor.Input.GetKeyDown(FocusKey);
		}

		protected virtual bool FocusActiveAction()
		{
			return m_component.Editor.Input.GetKey(FocusActiveKey);
		}

		protected virtual bool SnapToGridAction()
		{
			IInput input = m_component.Editor.Input;
			if (input.GetKeyDown(SnapToGridKey))
			{
				return input.GetKey(SnapToGridKey2);
			}
			return false;
		}

		protected virtual Vector2 RotateAxes()
		{
			IInput input = m_component.Editor.Input;
			float axis = input.GetAxis(InputAxis.X);
			float axis2 = input.GetAxis(InputAxis.Y);
			return new Vector2(axis, axis2);
		}

		protected virtual float DeltaTime()
		{
			return Time.unscaledDeltaTime * 50f;
		}

		protected virtual float ZoomAxis()
		{
			return m_component.Editor.Input.GetAxis(InputAxis.Z) * DeltaTime();
		}

		protected virtual Vector3 MoveAxes()
		{
			IInput input = m_component.Editor.Input;
			float num = DeltaTime();
			float x = input.GetAxis(InputAxis.HorizontalRaw) * num;
			float y = input.GetAxis(InputAxis.VerticalRaw) * num;
			float z = 0f;
			if (input.GetKey(MoveUpKey))
			{
				z = 0.5f * num;
			}
			else if (input.GetKey(MoveDownKey))
			{
				z = -0.5f * num;
			}
			return new Vector3(x, y, z);
		}

		protected override void Start()
		{
			base.Start();
			m_component.Editor.ActiveWindowChanged += Editor_ActiveWindowChanged;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (m_component != null && m_component.Editor != null)
			{
				m_component.Editor.ActiveWindowChanged -= Editor_ActiveWindowChanged;
			}
		}

		protected virtual void UpdateCursorState(bool isPointerOverEditorArea, bool pan, bool rotate, bool freeMove)
		{
			bool flag = FreeMoveZoomAction();
			SceneComponent.UpdateCursorState(isPointerOverEditorArea, pan, rotate, freeMove && !flag);
		}

		private void Editor_ActiveWindowChanged(RuntimeWindow deactivatedWindow)
		{
			if (m_component != null)
			{
				if (m_isActive)
				{
					UpdateCursorState(isPointerOverEditorArea: false, pan: false, rotate: false, freeMove: false);
					m_pan = false;
					m_rotate = false;
					m_freeMove = false;
				}
				m_isActive = m_component.IsWindowActive;
			}
		}

		protected override void LateUpdate()
		{
			if (!m_component.IsWindowActive)
			{
				return;
			}
			bool isPointerOver = m_component.Window.IsPointerOver;
			IInput input = m_component.Editor.Input;
			RuntimeTools tools = m_component.Editor.Tools;
			bool flag = AllowRotateAction();
			bool flag2 = (RotateAction() || (SwapLRMB && flag)) && SceneComponent.CanRotate;
			bool flag3 = PanAction() && SceneComponent.CanPan;
			bool flag4 = FreeMoveAction() && SceneComponent.CanFreeMove;
			if (flag3 && tools.Current != RuntimeTool.View)
			{
				flag2 = false;
			}
			bool flag5 = m_rotate != flag2 && flag2;
			if (flag5 && !isPointerOver)
			{
				flag2 = false;
				flag5 = false;
			}
			bool flag6 = m_rotate != flag2 && !flag2;
			m_rotate = flag2;
			if (!m_rotate)
			{
				m_rotateActive = false;
			}
			bool flag7 = m_pan != flag3 && flag3;
			if (flag7 && !isPointerOver)
			{
				flag3 = false;
			}
			bool flag8 = m_pan != flag3 && !flag3;
			m_pan = flag3;
			bool flag9 = m_freeMove != flag4 && flag4;
			if (flag9 && !isPointerOver)
			{
				flag4 = false;
			}
			bool flag10 = m_freeMove != flag4 && !flag4;
			m_freeMove = flag4;
			if (!m_freeMove)
			{
				m_freeMoveActive = false;
			}
			Vector3 pointerXY = input.GetPointerXY(0);
			tools.IsViewing = m_rotate || m_pan || m_freeMove;
			if (flag7 || flag8 || (flag5 && m_beginRotateImmediately) || flag6 || (flag9 && m_beginFreeMoveImmediately) || flag10)
			{
				UpdateCursorState(isPointerOver, m_pan, m_rotate && m_beginRotateImmediately, m_freeMove && m_beginFreeMoveImmediately);
			}
			if (m_freeMove)
			{
				Vector2 vector = RotateAxes();
				Vector3 vector2 = MoveAxes() * FreeMoveSensitivity * 0.25f;
				float num = ZoomAxis() * FreeZoomSensitivity * 0.25f;
				if (FreeMoveZoomAction())
				{
					num = vector.x * FreeZoomSensitivity * 0.025f;
					vector = Vector2.zero;
					vector2 = Vector3.zero;
				}
				else
				{
					vector *= FreeRotateSensitivity;
				}
				SceneComponent.FreeMove(vector, vector2, num);
				if (!m_freeMoveActive && (vector != Vector2.zero || vector2 != Vector3.zero || num != 0f))
				{
					UpdateCursorState(isPointerOver, m_pan, m_rotate, m_freeMove);
					m_freeMoveActive = true;
				}
				return;
			}
			if (m_rotate)
			{
				if (flag)
				{
					Vector2 vector3 = RotateAxes();
					float num2 = ZoomAxis();
					SceneComponent.Orbit(vector3.x * RotateXSensitivity, vector3.y * RotateYSensitivity, num2 * MoveZSensitivity);
					if (!m_rotateActive && (vector3 != Vector2.zero || num2 != 0f))
					{
						UpdateCursorState(isPointerOver, m_pan, m_rotate, m_freeMove);
						m_rotateActive = true;
					}
				}
				else
				{
					Transform transform = m_component.Window.Camera.transform;
					Ray ray = m_component.Window.Pointer;
					SceneComponent.Zoom(ZoomAxis() * MoveZSensitivity, Quaternion.FromToRotation(Vector3.forward, transform.InverseTransformVector(ray.direction).normalized));
				}
				SceneComponent.FreeMove(Vector2.zero, Vector3.zero, 0f);
				return;
			}
			if (m_pan)
			{
				if (flag7)
				{
					SceneComponent.BeginPan(pointerXY);
				}
				SceneComponent.Pan(pointerXY);
				return;
			}
			SceneComponent.FreeMove(Vector2.zero, Vector3.zero, 0f);
			if (!isPointerOver)
			{
				return;
			}
			SceneComponent.Zoom(ZoomAxis() * MoveZSensitivity, Quaternion.identity);
			BeginSelectAction();
			if (SelectAction())
			{
				SelectGO();
			}
			if (SnapToGridAction())
			{
				SceneComponent.SnapToGrid();
			}
			if (FocusAction())
			{
				if (FocusActiveAction())
				{
					SceneComponent.Focus(FocusMode.AllActive);
				}
				else if (SceneComponent.Selection.activeTransform != null && SceneComponent.Selection.activeTransform.GetComponent<Terrain>() == null)
				{
					SceneComponent.Focus(FocusMode.Selected);
				}
			}
			if (SelectAllAction())
			{
				SceneComponent.SelectAll();
			}
		}
	}
}
