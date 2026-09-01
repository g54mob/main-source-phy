using UnityEngine;

public class DebugCompleteLevelOnKey : SimBehaviour
{
	public KeyCode key = KeyCode.J;

	private void Update()
	{
		if (base.isSimulating && Input.GetKeyDown(key))
		{
			WinCondition.currentObjsCompleted += 10000;
		}
	}
}
