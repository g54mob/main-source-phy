using UnityEngine;

public class DeletePlayerPrefs : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown("f10"))
		{
			PlayerPrefs.DeleteAll();
		}
	}
}
