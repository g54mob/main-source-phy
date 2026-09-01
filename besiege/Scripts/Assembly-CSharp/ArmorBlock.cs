using System;
using UnityEngine;

public class ArmorBlock : BlockBehaviour
{
	public int version = 1;

	public TriggerSetJoint joiner;

	protected Vector3 orgPos;

	protected Vector3 newPos;

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating && joiner != null)
		{
			ReferenceMaster.onPreSimulateMachine = (Action<Machine>)Delegate.Combine(ReferenceMaster.onPreSimulateMachine, new Action<Machine>(PreSim));
			orgPos = joiner.transform.localPosition;
			newPos = orgPos;
			newPos.x = (newPos.y = 0f);
		}
	}

	private void PreSim(Machine m)
	{
		Vector3 vector = ((version != 0) ? newPos : orgPos);
		if (joiner.transform.localPosition != vector)
		{
			joiner.transform.localPosition = vector;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		ReferenceMaster.onPreSimulateMachine = (Action<Machine>)Delegate.Remove(ReferenceMaster.onPreSimulateMachine, new Action<Machine>(PreSim));
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating)
		{
			return;
		}
		if (!data.HasKey("bmt-version"))
		{
			if (data.WasLoadedFromFile)
			{
				version = 0;
				data.Write("bmt-version", version);
			}
		}
		else
		{
			version = data.ReadInt("bmt-version");
		}
	}
}
