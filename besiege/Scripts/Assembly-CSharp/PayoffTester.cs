using UnityEngine;

public class PayoffTester : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			WinCondition.currentObjsCompleted++;
		}
	}
}
