using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABC
{
	public class AllianceToolTip : MonoBehaviour
	{
		private UIFade fade;

		public Image icon1;

		public Image icon2;

		public Image border;

		public Image bar;

		public TextMeshProUGUI title;

		public Populate populate;

		private PopUpLevelUI[] levels;

		private void Start()
		{
			fade = GetComponent<UIFade>();
		}

		public void Open(AllianceButton allianceButton, Alliance alliance, int unlocks)
		{
			title.text = alliance.Name;
			title.color = alliance.color;
			border.color = alliance.color;
			bar.color = alliance.color;
			icon1.sprite = alliance.sprite;
			icon2.sprite = alliance.sprite;
			icon1.color = alliance.color;
			icon2.color = alliance.color;
			fade.isVisible = true;
			SetLevels(alliance, unlocks);
			fade.UpdateList();
		}

		private void SetLevels(Alliance alliance, int unlocks)
		{
			if (levels != null)
			{
				for (int i = 0; i < levels.Length; i++)
				{
					Object.Destroy(levels[i].gameObject);
				}
			}
			int num = 0;
			populate.times = alliance.bonuses.Length;
			levels = populate.DoPopulate<PopUpLevelUI>().ToArray();
			for (int j = 0; j < levels.Length; j++)
			{
				num += alliance.bonuses[j].unitsNeeded;
				levels[j].Init(alliance.bonuses[j], alliance, unlocks >= j, num);
			}
		}

		public void Close()
		{
			fade.isVisible = false;
		}
	}
}
