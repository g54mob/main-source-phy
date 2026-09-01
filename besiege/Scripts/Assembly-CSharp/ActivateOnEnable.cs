using UnityEngine;

public class ActivateOnEnable : MonoBehaviour
{
	public GameObject[] toActivate;

	public void OnEnable()
	{
		for (int i = 0; i < toActivate.Length; i++)
		{
			toActivate[i].SetActive(true);
		}
	}

	public void OnDisable()
	{
		for (int i = 0; i < toActivate.Length && !(toActivate[i] == null); i++)
		{
			toActivate[i].SetActive(false);
		}
	}
}
