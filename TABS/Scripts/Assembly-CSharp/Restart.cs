using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}
	}
}
