using System;

namespace Battlehub.RTCommon
{
	public class BusyContext : IDisposable
	{
		private IRTE m_editor;

		public BusyContext(IRTE editor)
		{
			m_editor = editor;
			m_editor.IsBusy = true;
		}

		public void Dispose()
		{
			if (m_editor != null)
			{
				m_editor.IsBusy = false;
			}
		}
	}
}
