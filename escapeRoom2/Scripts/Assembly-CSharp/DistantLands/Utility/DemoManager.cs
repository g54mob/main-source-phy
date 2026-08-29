using UnityEngine;
using UnityEngine.SceneManagement;

namespace DistantLands.Utility
{
	public class DemoManager : MonoBehaviour
	{
		public int nextScene;

		private void Start()
		{
		}

		private void Update()
		{
			if (Input.GetKeyUp(KeyCode.Space))
			{
				SceneManager.LoadScene(nextScene);
			}
		}
	}
}
