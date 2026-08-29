using UnityEngine;

namespace Battlehub.RTCommon
{
	public class RTEBaseInput : MonoBehaviour
	{
		[SerializeField]
		protected KeyCode RuntimeModifierKey = KeyCode.LeftControl;

		[SerializeField]
		protected KeyCode EditorModifierKey = KeyCode.LeftShift;

		[SerializeField]
		protected KeyCode OpenEditorKey = KeyCode.F12;

		[SerializeField]
		protected KeyCode PlayKey = KeyCode.F5;

		private IRTE m_editor;

		protected KeyCode ModifierKey => RuntimeModifierKey;

		protected virtual bool OpenEditorAction()
		{
			return m_editor.Input.GetKeyDown(OpenEditorKey);
		}

		protected virtual bool PlayAction()
		{
			return m_editor.Input.GetKeyDown(PlayKey);
		}

		protected virtual void Start()
		{
			m_editor = IOC.Resolve<IRTE>();
		}

		protected virtual void Update()
		{
			if (!m_editor.IsInputFieldActive)
			{
				UpdateOverride();
			}
		}

		protected virtual void UpdateOverride()
		{
			if (OpenEditorAction())
			{
				m_editor.IsOpened = !m_editor.IsOpened;
			}
			if (m_editor.IsOpened && PlayAction())
			{
				m_editor.IsPlaying = !m_editor.IsPlaying;
			}
		}
	}
}
