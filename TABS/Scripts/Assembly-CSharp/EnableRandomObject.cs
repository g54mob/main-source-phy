using UnityEngine;

public class EnableRandomObject : MonoBehaviour
{
	public GameObject[] objects;

	private void Start()
	{
		EnableObject();
	}

	public void EnableObject()
	{
		objects[Random.Range(0, objects.Length)].SetActive(value: true);
	}
}
