using UnityEngine;

namespace GameGrind
{
	public class JournalCanvas : MonoBehaviour
	{
		[SerializeField]
		private KeyCode togglePanel;

		private AchievementUIList panel;

		private void Awake()
		{
			if (Object.FindObjectsOfType<JournalCanvas>().Length > 1)
			{
				Object.Destroy(base.gameObject);
				Debug.LogWarning("Deleted duplicate instance of Journal. Journal should only be installed in the first scene you load achievements in and not in a scene that's revisted often.");
			}
			panel = base.transform.Find("Achievement_UI_List").GetComponent<AchievementUIList>();
			Object.DontDestroyOnLoad(this);
		}

		private void Update()
		{
			if (Input.GetKeyDown(togglePanel) && panel != null)
			{
				ToggleAchievementPanel();
			}
		}

		public void ToggleAchievementPanel()
		{
			panel.TogglePanel();
		}
	}
}
