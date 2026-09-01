using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TFBGames
{
	public class SceneLoadingButton : Button
	{
		public void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}
