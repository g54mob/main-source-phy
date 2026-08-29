using UnityEngine;

namespace Battlehub.RTCommon
{
	public class RuntimeUndoInput : MonoBehaviour
	{
		public KeyCode UndoKey = KeyCode.Z;

		public KeyCode RedoKey = KeyCode.Y;

		public KeyCode RuntimeModifierKey = KeyCode.LeftControl;

		public KeyCode EditorModifierKey = KeyCode.LeftShift;

		private static RuntimeUndoInput m_instance;

		private IRTE m_rte;

		public KeyCode ModifierKey => RuntimeModifierKey;

		public static bool IsInitialized => m_instance != null;

		private void Awake()
		{
			m_rte = IOC.Resolve<IRTE>();
			if (m_rte == null)
			{
				Debug.LogError("m_rte is null");
			}
			m_instance = this;
		}

		private void OnDestroy()
		{
			if (m_instance == this)
			{
				m_instance = null;
			}
		}

		private void Update()
		{
			if (UndoAction())
			{
				if (!m_rte.Undo.IsRecordingValues && !m_rte.Undo.IsRecording)
				{
					m_rte.Undo.Undo();
				}
			}
			else if (RedoAction() && !m_rte.Undo.IsRecordingValues && !m_rte.Undo.IsRecording)
			{
				m_rte.Undo.Redo();
			}
		}

		protected virtual bool UndoAction()
		{
			if (m_rte.Input.GetKeyDown(UndoKey))
			{
				return m_rte.Input.GetKey(ModifierKey);
			}
			return false;
		}

		protected virtual bool RedoAction()
		{
			if (m_rte.Input.GetKeyDown(RedoKey))
			{
				return m_rte.Input.GetKey(ModifierKey);
			}
			return false;
		}
	}
}
