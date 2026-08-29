using System;

namespace Battlehub.RTCommon
{
	public class Record
	{
		private object m_oldState;

		private object m_newState;

		private object m_target;

		private UndoRedoCallback m_redoCallback;

		private UndoRedoCallback m_undoCallback;

		private PurgeCallback m_purgeCallback;

		private EraseReferenceCallback m_eraseCallback;

		public object Target
		{
			get
			{
				return m_target;
			}
			set
			{
				m_target = value;
			}
		}

		public object OldState
		{
			get
			{
				return m_oldState;
			}
			set
			{
				m_oldState = value;
			}
		}

		public object NewState
		{
			get
			{
				return m_newState;
			}
			set
			{
				m_newState = value;
			}
		}

		public Record(object target, object newState, object oldState, UndoRedoCallback redoCallback, UndoRedoCallback undoCallback, PurgeCallback purgeCallback, EraseReferenceCallback eraseCallback)
		{
			if (redoCallback == null)
			{
				throw new ArgumentNullException("redoCallback");
			}
			if (undoCallback == null)
			{
				throw new ArgumentNullException("undoCallback");
			}
			m_target = target;
			m_redoCallback = redoCallback;
			m_undoCallback = undoCallback;
			m_purgeCallback = purgeCallback;
			m_eraseCallback = eraseCallback;
			m_newState = newState;
			m_oldState = oldState;
		}

		public bool Undo()
		{
			return m_undoCallback(this);
		}

		public bool Redo()
		{
			return m_redoCallback(this);
		}

		public void Purge()
		{
			if (m_purgeCallback != null)
			{
				m_purgeCallback(this);
			}
		}

		public bool Erase(object oldRef, object newRef)
		{
			bool result = false;
			if (m_eraseCallback != null)
			{
				result = m_eraseCallback(this, oldRef, newRef);
			}
			return result;
		}
	}
}
