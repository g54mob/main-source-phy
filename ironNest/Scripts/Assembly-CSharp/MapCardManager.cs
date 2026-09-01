using System.Collections.Generic;
using SleepyNodes;
using UnityEngine;

public class MapCardManager : MonoBehaviour
{
	[Header("Setup")]
	public OperationGraph Campaign;

	[Header("Runtime")]
	public bool ForceAllShown;

	public List<MapCard> MapCards;

	public void UpdateMapCards()
	{
	}

	[Button("Force Reveal All")]
	public void ForceRevealAll()
	{
	}

	[Button("Force Reveal All Complete")]
	public void ForceRevealAllComplete()
	{
	}
}
