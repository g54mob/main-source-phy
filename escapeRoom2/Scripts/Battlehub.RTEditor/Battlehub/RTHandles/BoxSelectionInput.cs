using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class BoxSelectionInput : MonoBehaviour
	{
		private int MouseButton;

		protected BoxSelection m_boxSelection;

		protected IRTE m_editor;

		private bool m_pointerPressed;

		private void Start()
		{
			if (m_boxSelection == null)
			{
				m_boxSelection = GetComponent<BoxSelection>();
			}
			m_editor = m_boxSelection.Editor;
		}

		private void LateUpdate()
		{
			if (!m_editor.Input.GetPointer(MouseButton) && m_pointerPressed)
			{
				m_pointerPressed = false;
				m_boxSelection.EndSelect();
			}
			if (m_boxSelection.enabled && m_boxSelection.IsWindowActive && (!(m_editor.Tools.ActiveTool != null) || !(m_editor.Tools.ActiveTool != m_boxSelection)) && m_editor.Input.GetPointerDown(MouseButton))
			{
				m_pointerPressed = true;
				m_boxSelection.BeginSelect();
			}
		}
	}
}
