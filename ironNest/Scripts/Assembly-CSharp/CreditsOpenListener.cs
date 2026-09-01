using SleepyNodes;
using UnityEngine;

public class CreditsOpenListener : MonoBehaviour
{
	[SerializeField]
	private CreditsPanel _creditsPanel;

	[SerializeField]
	private EndOfMissionUIController _endOfMissionUIController;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void EndOfMissionUIController_OnMissionSummaryDisplayed(MissionGraph mission)
	{
	}
}
