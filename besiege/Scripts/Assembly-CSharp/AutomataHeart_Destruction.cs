using UnityEngine;

public class AutomataHeart_Destruction : MonoBehaviour
{
	public BreakOnForce breakCode;

	public AutomataHeart heartControllerCode;

	private void Update()
	{
		if (heartControllerCode.ribCageOpen)
		{
			breakCode.CanDie = true;
		}
		else
		{
			breakCode.CanDie = false;
		}
	}
}
