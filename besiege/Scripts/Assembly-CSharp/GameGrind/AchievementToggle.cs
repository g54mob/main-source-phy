using UnityEngine;
using UnityEngine.UI;

namespace GameGrind
{
	public class AchievementToggle : MonoBehaviour
	{
		private JournalCanvas journalCanvas;

		private AchievementUIList achievementPanel;

		private void Start()
		{
			journalCanvas = Object.FindObjectOfType<JournalCanvas>();
			GetComponent<Button>().onClick.AddListener(delegate
			{
				journalCanvas.ToggleAchievementPanel();
			});
		}
	}
}
