using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTHandles.Demo
{
	public class SimpleEditor : MonoBehaviour
	{
		[SerializeField]
		private Toggle m_viewToggle;

		[SerializeField]
		private Toggle m_positionToggle;

		[SerializeField]
		private Toggle m_rotationToggle;

		[SerializeField]
		private Toggle m_scaleToggle;

		[SerializeField]
		private Toggle m_rectToggle;

		[SerializeField]
		private Toggle m_pivotModeToggle;

		[SerializeField]
		private Toggle m_pivotRotationToggle;

		[SerializeField]
		private Button m_undoButton;

		[SerializeField]
		private Button m_redoButton;

		private IRTE m_editor;

		protected IRTE Editor => m_editor;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
			m_editor = IOC.Resolve<IRTE>();
			m_editor.Tools.ToolChanged += OnToolChanged;
			m_editor.Tools.PivotModeChanged += OnPivotModeChanged;
			m_editor.Tools.PivotRotationChanged += OnPivotRotationChanged;
			m_editor.Undo.UndoCompleted += UpdateUndoRedoButtons;
			m_editor.Undo.RedoCompleted += UpdateUndoRedoButtons;
			m_editor.Undo.StateChanged += UpdateUndoRedoButtons;
			SubscribeUIEvents();
			UpdateUndoRedoButtons();
		}

		protected virtual void OnDestroy()
		{
			if (m_editor != null)
			{
				m_editor.Tools.ToolChanged -= OnToolChanged;
				m_editor.Tools.PivotModeChanged -= OnPivotModeChanged;
				m_editor.Tools.PivotRotationChanged -= OnPivotRotationChanged;
				m_editor.Undo.UndoCompleted -= UpdateUndoRedoButtons;
				m_editor.Undo.RedoCompleted -= UpdateUndoRedoButtons;
				m_editor.Undo.StateChanged -= UpdateUndoRedoButtons;
			}
			UnsubscribeUIEvents();
		}

		protected virtual void SubscribeUIEvents()
		{
			if ((bool)m_viewToggle)
			{
				m_viewToggle.onValueChanged.AddListener(OnViewToggle);
			}
			if ((bool)m_positionToggle)
			{
				m_positionToggle.onValueChanged.AddListener(OnPositionToggle);
			}
			if ((bool)m_rotationToggle)
			{
				m_rotationToggle.onValueChanged.AddListener(OnRotationToggle);
			}
			if ((bool)m_scaleToggle)
			{
				m_scaleToggle.onValueChanged.AddListener(OnScaleToogle);
			}
			if ((bool)m_rectToggle)
			{
				m_rectToggle.onValueChanged.AddListener(OnRectToggle);
			}
			if ((bool)m_pivotModeToggle)
			{
				m_pivotModeToggle.onValueChanged.AddListener(OnPivotModeToggle);
			}
			if ((bool)m_pivotRotationToggle)
			{
				m_pivotRotationToggle.onValueChanged.AddListener(OnPivotRotationToggle);
			}
			if ((bool)m_undoButton)
			{
				m_undoButton.onClick.AddListener(OnUndoClick);
			}
			if ((bool)m_redoButton)
			{
				m_redoButton.onClick.AddListener(OnRedoClick);
			}
		}

		protected virtual void UnsubscribeUIEvents()
		{
			if ((bool)m_viewToggle)
			{
				m_viewToggle.onValueChanged.RemoveListener(OnViewToggle);
			}
			if ((bool)m_positionToggle)
			{
				m_positionToggle.onValueChanged.RemoveListener(OnPositionToggle);
			}
			if ((bool)m_rotationToggle)
			{
				m_rotationToggle.onValueChanged.RemoveListener(OnRotationToggle);
			}
			if ((bool)m_scaleToggle)
			{
				m_scaleToggle.onValueChanged.RemoveListener(OnScaleToogle);
			}
			if ((bool)m_rectToggle)
			{
				m_rectToggle.onValueChanged.RemoveListener(OnRectToggle);
			}
			if ((bool)m_pivotModeToggle)
			{
				m_pivotModeToggle.onValueChanged.AddListener(OnPivotModeToggle);
			}
			if ((bool)m_pivotRotationToggle)
			{
				m_pivotRotationToggle.onValueChanged.RemoveListener(OnPivotRotationToggle);
			}
			if ((bool)m_undoButton)
			{
				m_undoButton.onClick.RemoveListener(OnUndoClick);
			}
			if ((bool)m_redoButton)
			{
				m_redoButton.onClick.RemoveListener(OnRedoClick);
			}
		}

		private void OnToolChanged()
		{
			UnsubscribeUIEvents();
			switch (m_editor.Tools.Current)
			{
			case RuntimeTool.View:
				if (m_viewToggle != null)
				{
					m_viewToggle.isOn = true;
				}
				break;
			case RuntimeTool.Move:
				if (m_positionToggle != null)
				{
					m_positionToggle.isOn = true;
				}
				break;
			case RuntimeTool.Rotate:
				if (m_rotationToggle != null)
				{
					m_rotationToggle.isOn = true;
				}
				break;
			case RuntimeTool.Scale:
				if (m_scaleToggle != null)
				{
					m_scaleToggle.isOn = true;
				}
				break;
			case RuntimeTool.Rect:
				if (m_rectToggle != null)
				{
					m_rectToggle.isOn = true;
				}
				break;
			case RuntimeTool.None:
				if (m_viewToggle != null)
				{
					m_viewToggle.isOn = false;
				}
				if (m_positionToggle != null)
				{
					m_positionToggle.isOn = false;
				}
				if (m_rotationToggle != null)
				{
					m_rotationToggle.isOn = false;
				}
				if (m_scaleToggle != null)
				{
					m_scaleToggle.isOn = false;
				}
				if (m_rectToggle != null)
				{
					m_rectToggle.isOn = false;
				}
				break;
			}
			SubscribeUIEvents();
		}

		private void OnPivotModeChanged()
		{
			UnsubscribeUIEvents();
			m_pivotModeToggle.isOn = m_editor.Tools.PivotMode == RuntimePivotMode.Center;
			Text component = m_pivotModeToggle.GetComponent<Text>();
			if (component != null)
			{
				component.text = m_editor.Tools.PivotMode.ToString() + " (Z)";
			}
			SubscribeUIEvents();
		}

		private void OnPivotRotationChanged()
		{
			UnsubscribeUIEvents();
			m_pivotRotationToggle.isOn = m_editor.Tools.PivotRotation == RuntimePivotRotation.Global;
			Text component = m_pivotRotationToggle.GetComponent<Text>();
			if (component != null)
			{
				component.text = m_editor.Tools.PivotRotation.ToString() + " (X)";
			}
			SubscribeUIEvents();
		}

		private void UpdateUndoRedoButtons()
		{
			if ((bool)m_undoButton)
			{
				m_undoButton.interactable = m_editor.Undo.CanUndo;
			}
			if ((bool)m_redoButton)
			{
				m_redoButton.interactable = m_editor.Undo.CanRedo;
			}
		}

		private void OnViewToggle(bool value)
		{
			m_editor.Tools.Current = RuntimeTool.View;
		}

		private void OnPositionToggle(bool value)
		{
			m_editor.Tools.Current = RuntimeTool.Move;
		}

		private void OnRotationToggle(bool value)
		{
			m_editor.Tools.Current = RuntimeTool.Rotate;
		}

		private void OnScaleToogle(bool value)
		{
			m_editor.Tools.Current = RuntimeTool.Scale;
		}

		private void OnRectToggle(bool value)
		{
			m_editor.Tools.Current = RuntimeTool.Rect;
		}

		private void OnPivotModeToggle(bool value)
		{
			m_editor.Tools.PivotMode = ((!value) ? RuntimePivotMode.Pivot : RuntimePivotMode.Center);
		}

		private void OnPivotRotationToggle(bool value)
		{
			m_editor.Tools.PivotRotation = (value ? RuntimePivotRotation.Global : RuntimePivotRotation.Local);
		}

		private void OnUndoClick()
		{
			m_editor.Undo.Undo();
		}

		private void OnRedoClick()
		{
			m_editor.Undo.Redo();
		}
	}
}
