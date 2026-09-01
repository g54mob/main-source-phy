using System;
using UnityEngine;

[AddComponentMenu("Simulation/EnableOnSimulate")]
public class EnableOnSimulate : MonoBehaviour
{
	public GameObject[] objToEnable;

	public Behaviour[] componentToEnable;

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
		for (int i = 0; i < objToEnable.Length; i++)
		{
			objToEnable[i].SetActive(toggle);
		}
		for (int j = 0; j < componentToEnable.Length; j++)
		{
			componentToEnable[j].enabled = toggle;
		}
	}
}
