using System;
using UnityEngine;

public class DisableOnSimulate : MonoBehaviour
{
	public Transform objToDisable;

	protected void Start()
	{
		if (StatMaster.levelSimulating)
		{
			OnSimulationToggled(true);
		}
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
	}

	private void OnSimulationToggled(bool toggle)
	{
		objToDisable.gameObject.SetActive(!toggle);
	}
}
