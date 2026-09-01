using UnityEngine;

public class PlacementLineCursor : MonoBehaviour
{
	public GameObject[] ObjectsToDisable = new GameObject[0];

	public void Enable()
	{
		for (int i = 0; i < ObjectsToDisable.Length; i++)
		{
			ObjectsToDisable[i].SetActive(value: true);
		}
	}

	public void Disable()
	{
		for (int i = 0; i < ObjectsToDisable.Length; i++)
		{
			ObjectsToDisable[i].SetActive(value: false);
		}
	}
}
