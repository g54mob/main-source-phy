using UnityEngine;

public class DisplayLogicInTabs : MonoBehaviour
{
	public GameObject[] hide;

	public GameObject[] move;

	public GameObject[] display;

	public Vector3 moveValue = Vector3.zero;

	private void Awake()
	{
		for (int i = 0; i < hide.Length; i++)
		{
			hide[i].SetActive(false);
		}
		for (int j = 0; j < move.Length; j++)
		{
			move[j].transform.localPosition += moveValue;
		}
		for (int k = 0; k < display.Length; k++)
		{
			display[k].SetActive(true);
		}
		Object.Destroy(this);
	}
}
