using UnityEngine;

public class UndoActionGroupName : UndoAction
{
	private readonly string oldName;

	private readonly string newName;

	private readonly MKey key;

	public UndoActionGroupName(Machine m, MKey k, string prevName, string name)
	{
		machine = m;
		key = new MKey(string.Empty, string.Empty, KeyCode.None);
		key.DeSerialize(k.Serialize(string.Empty));
		oldName = prevName;
		newName = name;
	}

	public override bool Undo()
	{
		OverviewBlockMapper overviewBlockMapper = OverviewBlockMapper.CurrentInstance;
		if (overviewBlockMapper == null)
		{
			overviewBlockMapper = OverviewBlockMapper.Open(machine);
		}
		InputGroup inputGroup = overviewBlockMapper.inputGroups.Find((InputGroup x) => x.key.Compare(key));
		if (inputGroup != null)
		{
			inputGroup.CustomName = oldName;
			OverviewBlockMapper.SaveInputGroups(machine, overviewBlockMapper.inputGroups);
			overviewBlockMapper.Rebuild();
		}
		else
		{
			Debug.Log("Couldn't find group!");
		}
		return true;
	}

	public override bool Redo()
	{
		OverviewBlockMapper overviewBlockMapper = OverviewBlockMapper.CurrentInstance;
		if (overviewBlockMapper == null)
		{
			overviewBlockMapper = OverviewBlockMapper.Open(machine);
		}
		InputGroup inputGroup = overviewBlockMapper.inputGroups.Find((InputGroup x) => x.key.Compare(key));
		if (inputGroup != null)
		{
			inputGroup.CustomName = newName;
			OverviewBlockMapper.SaveInputGroups(machine, overviewBlockMapper.inputGroups);
			overviewBlockMapper.Rebuild();
		}
		return true;
	}
}
