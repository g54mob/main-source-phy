using Localisation;
using UnityEngine;
using UnityEngine.UI;

namespace GameGrind
{
	public class AchievementUIElement : MonoBehaviour
	{
		public Text titleText;

		public Text descriptionText;

		public Text valueText;

		public Text rewardText;

		public UnityEngine.UI.Slider valueSlider;

		public Image sliderFill;

		[SerializeField]
		private Image valueBackground;

		[SerializeField]
		private Image sliderBackground;

		[SerializeField]
		private Image iconImage;

		public Color progressBarCompleteColor;

		[SerializeField]
		private Color valueBackgroundColor;

		[SerializeField]
		public Color altRowShading;

		[SerializeField]
		private Image checkmark;

		[SerializeField]
		[Header("Secret Achievements Options")]
		private Color progressBarSecretiveColor;

		[SerializeField]
		private Color secretValueBGColor;

		[SerializeField]
		private bool secretShowReward;

		[SerializeField]
		private TextAnchor secretTextAlignment;

		public void SetAchievementValues(Achievement achievement)
		{
			if (achievement.secret && !achievement.completed)
			{
				rewardText.text = ((!secretShowReward) ? string.Empty : achievement.points.ToString());
				valueText.text = string.Empty;
				string path = achievement.iconPath.Replace("Assets/Journal/Resources/", string.Empty).Replace(".png", string.Empty).Replace(".jpeg", string.Empty);
				iconImage.sprite = Resources.Load<Sprite>(path);
				valueBackground.color = secretValueBGColor;
				Text text = descriptionText;
				TextAnchor alignment = secretTextAlignment;
				titleText.alignment = alignment;
				text.alignment = alignment;
				Image image = sliderFill;
				Color color = progressBarSecretiveColor;
				sliderBackground.color = color;
				image.color = color;
			}
			else if ((achievement.secret && achievement.completed) || !achievement.secret)
			{
				titleText.text = LocalisationManager.GetTranslation(achievement.title);
				string path2 = achievement.iconPath.Replace("Assets/Journal/Resources/", string.Empty).Replace(".png", string.Empty).Replace(".jpeg", string.Empty);
				iconImage.sprite = Resources.Load<Sprite>(path2);
				descriptionText.text = LocalisationManager.GetTranslation(achievement.description);
				rewardText.text = achievement.points.ToString();
				if (achievement.displayAsPercentage)
				{
					valueText.text = ((float)achievement.value / (float)achievement.neededValue * 100f).ToString("0.0") + "%";
				}
				else
				{
					valueText.text = achievement.value + "/" + achievement.neededValue;
				}
				valueSlider.value = (float)achievement.value / (float)achievement.neededValue * 100f;
				descriptionText.alignment = TextAnchor.MiddleLeft;
				titleText.alignment = TextAnchor.UpperLeft;
				valueBackground.color = valueBackgroundColor;
				if (achievement.completed)
				{
					sliderFill.color = progressBarCompleteColor;
					iconImage.color = new Color(1f, 1f, 1f, 1f);
					checkmark.gameObject.SetActive(true);
				}
				else
				{
					iconImage.color = new Color(1f, 1f, 1f, 0.5f);
				}
			}
			TranslateAchievement(achievement);
		}

		public void TranslateAchievement(Achievement achievement)
		{
			if (achievement.secret && !achievement.completed)
			{
				titleText.text = LocalisationManager.GetTranslation(3656);
				descriptionText.text = LocalisationManager.GetTranslation(3657);
			}
			else
			{
				titleText.text = LocalisationManager.GetTranslation(achievement.title);
				descriptionText.text = LocalisationManager.GetTranslation(achievement.description);
			}
		}
	}
}
