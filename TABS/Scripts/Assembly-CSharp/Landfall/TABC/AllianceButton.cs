using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABC
{
	public class AllianceButton : MonoBehaviour
	{
		public TeirDots[] teirDots;

		public Image lightImage;

		public Image darkImage;

		public Image icon;

		public Image iconShadow;

		public Image border;

		public Color darkColor;

		public Color almostDarkColor;

		public bool darkTheme;

		public AllianceToolTip toolTip;

		private Alliance myAlliance;

		public int myUnitsInFaction;

		private int levelsUnlocked;

		private void Start()
		{
		}

		public void Init(Alliance alliance)
		{
			myAlliance = alliance;
			icon.sprite = alliance.sprite;
			iconShadow.sprite = alliance.sprite;
			if (darkTheme)
			{
				lightImage.color = almostDarkColor;
				darkImage.color = darkColor;
				iconShadow.color = darkColor;
				icon.color = alliance.color;
				border.color = alliance.color;
			}
			else
			{
				lightImage.color = alliance.color;
				darkImage.color = alliance.shadowColor;
				iconShadow.color = alliance.shadowColor;
			}
		}

		public void UpdateAlliance(int unitsInFaction)
		{
			myUnitsInFaction = unitsInFaction;
			levelsUnlocked = myAlliance.GetUnlockedLevels(myUnitsInFaction);
			SetUnlockDots();
		}

		private void SetUnlockDots()
		{
			Color color = myAlliance.color;
			color.a = 0.4f;
			Color shadowColor = myAlliance.shadowColor;
			shadowColor.a = 0.4f;
			Color color2 = darkColor;
			color2.a = 0.4f;
			int num = 0;
			for (int i = 0; i < myAlliance.bonuses.Length; i++)
			{
				for (int j = 0; j < myAlliance.bonuses[i].unitsNeeded; j++)
				{
					num++;
					teirDots[i].darkDot[j].gameObject.SetActive(value: true);
					if (num > myUnitsInFaction)
					{
						teirDots[i].coloredDot[j].color = color;
						teirDots[i].shadowDot[j].color = shadowColor;
						teirDots[i].darkDot[j].color = color2;
					}
					else
					{
						teirDots[i].coloredDot[j].color = myAlliance.color;
						teirDots[i].shadowDot[j].color = myAlliance.shadowColor;
						teirDots[i].darkDot[j].color = darkColor;
					}
				}
			}
		}

		private void SetUnlockDot(bool unlocked)
		{
		}

		public void ShowToolTip()
		{
			if (DragHandler.instance.draggedObject == null)
			{
				toolTip.Open(this, myAlliance, levelsUnlocked);
			}
		}

		public void Close()
		{
			toolTip.Close();
		}

		public void ShowAlliance()
		{
			AllianceHandler.instance.ShowAllianceVisual(myAlliance);
		}

		public void HideAlliance()
		{
			AllianceHandler.instance.HideAllianceVisual();
		}
	}
}
