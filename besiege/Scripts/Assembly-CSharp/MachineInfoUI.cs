using System;
using UnityEngine;

public class MachineInfoUI : MonoBehaviour
{
	public TextMesh entityCounter;

	public TextMesh totalBlocksCounter;

	public TextMesh blockCountTextMesh;

	public TextMesh blockCostTextMesh;

	public TextMesh clusterCountTextMesh;

	public MeshRenderer clusterCountIcon;

	protected MeshRenderer clusterIcon;

	public GameObject[] hideInSP = new GameObject[0];

	public GameObject[] moveInSP = new GameObject[0];

	public AlignUI[] aligners = new AlignUI[0];

	public float spOffset = 0.3411113f;

	protected void Awake()
	{
		clusterIcon = clusterCountTextMesh.GetComponent<MeshRenderer>();
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineChanged));
		ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineModified, new Action<Machine>(OnMachineModified));
		StatMaster.entityCountChanged = (Action<int>)Delegate.Combine(StatMaster.entityCountChanged, new Action<int>(UpdateEntityCount));
		StatMaster.totalBlocksChanged = (Action)Delegate.Combine(StatMaster.totalBlocksChanged, new Action(UpdateTotalBlockCount));
		if (!StatMaster.isMP)
		{
			for (int i = 0; i < hideInSP.Length; i++)
			{
				hideInSP[i].SetActive(false);
			}
			for (int j = 0; j < moveInSP.Length; j++)
			{
				moveInSP[j].transform.localPosition += Vector3.up * spOffset;
			}
			for (int k = 0; k < aligners.Length; k++)
			{
				AlignUI alignUI = aligners[k];
				alignUI.mode = alignUI.target.mode;
				alignUI.target = alignUI.target.target;
				alignUI.quad = alignUI.target.quad;
				alignUI.Align();
			}
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineChanged));
		ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineModified, new Action<Machine>(OnMachineModified));
		StatMaster.entityCountChanged = (Action<int>)Delegate.Remove(StatMaster.entityCountChanged, new Action<int>(UpdateEntityCount));
		StatMaster.totalBlocksChanged = (Action)Delegate.Remove(StatMaster.totalBlocksChanged, new Action(UpdateTotalBlockCount));
	}

	private void OnMachineChanged(Machine machine)
	{
		if (machine != null)
		{
			OnMachineModified(machine);
		}
		else
		{
			SetText("0", "0", "0");
		}
	}

	private void OnMachineModified(Machine machine)
	{
		StatMaster.BlockCount = machine.DisplayBlockCount;
		SetText(StatMaster.BlockCount.ToString(), machine.ClusterCount.ToString(), machine.BlocksCost.ToString());
	}

	private void SetText(string blockText, string clusterText, string costText)
	{
		if (blockCountTextMesh != null)
		{
			blockCountTextMesh.text = blockText;
		}
		if (clusterCountTextMesh != null)
		{
			clusterCountTextMesh.text = "     " + clusterText;
			Transform transform = clusterCountIcon.transform;
			Vector3 position = transform.position;
			transform.position = new Vector3(clusterIcon.bounds.min.x + 0.08f, position.y, position.z);
		}
		if (blockCostTextMesh != null)
		{
			blockCostTextMesh.text = string.Empty + costText;
		}
	}

	public void UpdateEntityCount(int count)
	{
		if (!(entityCounter == null))
		{
			entityCounter.text = string.Empty + count;
		}
	}

	public void UpdateTotalBlockCount()
	{
		if (totalBlocksCounter == null)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (!playerData.isSpectator)
			{
				num += playerData.machine.DisplayBlockCount;
			}
		}
		totalBlocksCounter.text = string.Empty + num;
	}
}
