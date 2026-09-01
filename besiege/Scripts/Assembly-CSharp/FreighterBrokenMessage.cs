using UnityEngine;

[AddComponentMenu("Destruction/Freighter Broken Message")]
public class FreighterBrokenMessage : MonoBehaviour
{
	public FreighterAI freighterAiCode;

	private void OnJointBreak()
	{
		freighterAiCode.Break();
		AddToPercentageBar();
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}
}
