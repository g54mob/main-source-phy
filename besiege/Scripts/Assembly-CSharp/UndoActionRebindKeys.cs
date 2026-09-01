using System.Collections.Generic;

public class UndoActionRebindKeys : UndoAction
{
	public class UndoKeyEntry
	{
		public int buildIndex;

		public XData prevKey;

		public XData newKey;
	}

	private List<UndoKeyEntry> keys;

	public UndoActionRebindKeys(Machine m, List<UndoKeyEntry> k)
	{
		changesOBM = !StatMaster.isMP;
		machine = m;
		keys = k;
	}

	private void RebindKeys(bool useNew)
	{
		OverviewBlockMapper overviewBlockMapper = OverviewBlockMapper.CurrentInstance;
		if (overviewBlockMapper == null)
		{
			overviewBlockMapper = OverviewBlockMapper.Open(machine);
		}
		List<OverviewBlockMapper.RebindEntry> list = new List<OverviewBlockMapper.RebindEntry>();
		for (int i = 0; i < keys.Count; i++)
		{
			UndoKeyEntry undoKeyEntry = keys[i];
			OverviewBlockMapper.RebindEntry rebindEntry = new OverviewBlockMapper.RebindEntry();
			rebindEntry.buildIndex = undoKeyEntry.buildIndex;
			rebindEntry.newKey = ((!useNew) ? undoKeyEntry.prevKey : undoKeyEntry.newKey);
			OverviewBlockMapper.RebindEntry item = rebindEntry;
			list.Add(item);
		}
		UndoAction undoAction;
		overviewBlockMapper.SyncRebindEntries(list, true, out undoAction);
	}

	public override bool Redo()
	{
		RebindKeys(true);
		return true;
	}

	public override bool Undo()
	{
		RebindKeys(false);
		return true;
	}
}
