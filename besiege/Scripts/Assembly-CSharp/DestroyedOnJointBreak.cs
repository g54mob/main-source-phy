using UnityEngine;

public class DestroyedOnJointBreak : MonoBehaviour
{
	private void OnJointBreak()
	{
		WinCondition.currentObjsCompleted++;
	}
}
