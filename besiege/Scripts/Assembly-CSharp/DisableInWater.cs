using UnityEngine;

public class DisableInWater : MonoBehaviour
{
	public BasicInfo bInfo;

	public GameObject[] toDisable;

	public GameObject[] toEnable;

	public bool reverseWhenExitWater;

	private bool wasInWater;

	private void Update()
	{
		if (!wasInWater && bInfo.InWater)
		{
			wasInWater = true;
			for (int i = 0; i < toDisable.Length; i++)
			{
				toDisable[i].SetActive(false);
			}
			for (int j = 0; j < toEnable.Length; j++)
			{
				toEnable[j].SetActive(true);
			}
		}
		else if (reverseWhenExitWater && wasInWater && !bInfo.InWater)
		{
			wasInWater = false;
			for (int k = 0; k < toDisable.Length; k++)
			{
				toDisable[k].SetActive(true);
			}
			for (int l = 0; l < toEnable.Length; l++)
			{
				toEnable[l].SetActive(false);
			}
		}
	}
}
