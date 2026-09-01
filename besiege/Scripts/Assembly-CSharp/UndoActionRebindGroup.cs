using UnityEngine;

public class UndoActionRebindGroup : UndoAction
{
	private readonly MKey prevKey;

	private readonly MKey newKey;

	public UndoActionRebindGroup(Machine m, MKey oldKey, MKey key)
	{
		changesOBM = !StatMaster.isMP;
		machine = m;
		prevKey = new MKey(string.Empty, string.Empty, KeyCode.None);
		XStringArray raw = oldKey.Serialize(string.Empty);
		prevKey.DeSerialize(raw);
		newKey = new MKey(string.Empty, string.Empty, KeyCode.None);
		XStringArray raw2 = key.Serialize(string.Empty);
		newKey.DeSerialize(raw2);
	}

	private void UndoGroupKey(MKey oldKey, MKey key)
	{
		OverviewBlockMapper overviewBlockMapper = OverviewBlockMapper.CurrentInstance;
		if (overviewBlockMapper == null)
		{
			overviewBlockMapper = OverviewBlockMapper.Open(machine);
		}
		InputGroup inputGroup = overviewBlockMapper.inputGroups.Find((InputGroup x) => x.key.Contains(oldKey));
		if (inputGroup != null)
		{
			inputGroup.key.MatchKeys(key);
			overviewBlockMapper.OnEditGroupKey(inputGroup, true);
		}
	}

	public override bool Undo()
	{
		UndoGroupKey(newKey, prevKey);
		return true;
	}

	public override bool Redo()
	{
		UndoGroupKey(prevKey, newKey);
		return true;
	}
}
