using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Core/Machine Analyzer")]
public class MachineAnalyzer : MonoBehaviour
{
	private class LinkEntry
	{
		public BlockBehaviour block;

		public bool findAdjacent;
	}

	private BlockLinkManager linkManager;

	private Machine machine;

	private bool lockedMessages;

	private LinkedList<List<LinkEntry>> analyzeList = new LinkedList<List<LinkEntry>>();

	private Queue<List<LinkEntry>> listPool = new Queue<List<LinkEntry>>();

	private Queue<LinkEntry> entryPool = new Queue<LinkEntry>();

	private bool locked;

	public void Init(BlockLinkManager manager, Machine m)
	{
		linkManager = manager;
		machine = m;
		for (int i = 0; i < 10; i++)
		{
			listPool.Enqueue(new List<LinkEntry>());
			entryPool.Enqueue(new LinkEntry());
		}
	}

	public void SetLocked(bool toggle)
	{
		locked = toggle;
	}

	public void Reset()
	{
		while (analyzeList.Count > 0)
		{
			List<LinkEntry> value = analyzeList.First.Value;
			while (value.Count > 0)
			{
				entryPool.Enqueue(value[0]);
				value.RemoveAt(0);
			}
			listPool.Enqueue(value);
			analyzeList.RemoveFirst();
		}
		if (!machine.analyzing)
		{
			OnReset();
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log(machine.PlayerID + ": Resetting analysis.");
			}
		}
		if (lockedMessages)
		{
			LockNetworkMessages(false);
		}
	}

	private LinkEntry GetEntry()
	{
		if (entryPool.Count > 0)
		{
			return entryPool.Dequeue();
		}
		return new LinkEntry();
	}

	protected void FixedUpdate()
	{
		if (locked || analyzeList.Count == 0)
		{
			return;
		}
		List<LinkEntry> value = analyzeList.First.Value;
		while (value.Count > 0)
		{
			LinkEntry linkEntry = value[0];
			if (linkEntry.block != null)
			{
				machine.FindLinks(linkEntry.block, linkEntry.findAdjacent);
			}
			entryPool.Enqueue(linkEntry);
			value.RemoveAt(0);
		}
		listPool.Enqueue(value);
		analyzeList.RemoveFirst();
		Analyze();
	}

	public void Analyze()
	{
		if (analyzeList.Count > 0)
		{
			return;
		}
		if ((!StatMaster.isMP || !OptionsMaster.networkClusters || machine.isLocalMachine) && machine.analyzing)
		{
			machine.CheckBounds();
			linkManager.Analyze();
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log(machine.PlayerID + ": Analysis done.");
			}
			machine.analyzing = false;
			machine.OnAnalyzeComplete();
		}
		if (lockedMessages)
		{
			LockNetworkMessages(false);
		}
		base.enabled = false;
	}

	private void LockNetworkMessages(bool toggle)
	{
		NetworkAuxAddPiece instance = NetworkAuxAddPiece.Instance;
		if (!object.ReferenceEquals(instance, null))
		{
			instance.LockMessageExecution(toggle);
		}
		lockedMessages = toggle;
	}

	protected void OnDestroy()
	{
		if (lockedMessages)
		{
			LockNetworkMessages(false);
		}
	}

	public void OnReset()
	{
		if (!machine.analyzing)
		{
			locked = false;
			machine.analyzing = true;
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log(machine.PlayerID + ": Starting analysis..");
			}
			machine.OnAnalysisReset();
		}
	}

	public void FindLinks(int delay, BlockBehaviour block, bool findAdjacent)
	{
		bool flag = !StatMaster.isMP || !OptionsMaster.networkClusters || machine.isLocalMachine;
		if (StatMaster.isMP && flag && analyzeList.Count == 0)
		{
			LockNetworkMessages(true);
		}
		OnReset();
		if (flag)
		{
			while (analyzeList.Count <= delay)
			{
				List<LinkEntry> list = null;
				list = ((listPool.Count <= 0) ? new List<LinkEntry>() : listPool.Dequeue());
				analyzeList.AddLast(list);
			}
			LinkEntry entry = GetEntry();
			entry.block = block;
			entry.findAdjacent = findAdjacent;
			analyzeList.Last.Value.Add(entry);
			base.enabled = true;
		}
	}
}
