using System.Collections.Generic;

public class SandboxUndoState
{
	public List<string> m_SelectedItemGuids = new List<string>();

	public SandboxLayoutData m_State;

	public SandboxUndoState()
	{
		foreach (SandboxItem item in SandboxSelectionSet.m_Items)
		{
			m_SelectedItemGuids.Add(item.m_UndoGuid);
		}
		m_State = SandboxLayout.SerializeToProxies();
	}
}
