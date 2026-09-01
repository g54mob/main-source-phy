using UnityEngine;

public class EnableObjectsOnAwake : MonoBehaviour
{
	public GameObject[] objectsToEnable;

	private void Awake()
	{
		if (base.gameObject.activeSelf)
		{
			for (int i = 0; i < objectsToEnable.Length; i++)
			{
				objectsToEnable[i].SetActive(value: true);
			}
		}
	}
}
