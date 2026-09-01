using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameGrind
{
	public class AchievementTester : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				Journal.Increment(1, 1);
			}
			if (Input.GetKeyDown(KeyCode.L))
			{
				Journal.Increment(0, 1);
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				for (int i = 0; i < Journal.achievementMaster.Count; i++)
				{
					Journal.Increment(i, 1);
				}
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				Journal.SetValue(19, 15);
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				Journal.Save();
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				Debug.Log("Scene reloaded.");
			}
		}
	}
}
