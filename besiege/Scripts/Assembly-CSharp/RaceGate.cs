using UnityEngine;

public class RaceGate : MonoBehaviour
{
	public ConfigJointRotater[] nextGate;

	public GameObject nextFinishLine;

	private void FinishLine()
	{
		for (int i = 0; i < nextGate.Length; i++)
		{
			nextGate[i].StartRotate();
		}
		nextFinishLine.SetActive(true);
	}
}
