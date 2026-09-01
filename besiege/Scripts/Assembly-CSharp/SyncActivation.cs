using UnityEngine;

public class SyncActivation : MonoBehaviour
{
	public GameObject[] toActivate;

	public bool inverse = true;

	public void OnEnable()
	{
		for (int i = 0; i < toActivate.Length; i++)
		{
			toActivate[i].SetActive(!inverse);
		}
	}

	public void OnDisable()
	{
		for (int i = 0; i < toActivate.Length; i++)
		{
			if (!(toActivate[i] == null))
			{
				toActivate[i].SetActive(inverse);
			}
		}
	}
}
