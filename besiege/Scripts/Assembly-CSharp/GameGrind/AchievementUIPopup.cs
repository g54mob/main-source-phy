using Localisation;
using UnityEngine;
using UnityEngine.UI;

namespace GameGrind
{
	[DisallowMultipleComponent]
	public class AchievementUIPopup : BaseAchievementSystem
	{
		public enum AnimationType
		{
			Bounce = 0,
			Ease = 1,
			Fade = 2
		}

		public Text popupTitleText;

		public Text popupDescriptionText;

		[SerializeField]
		private Image achievementIconPopup;

		private Animator animator;

		private AudioSource audioSource;

		protected override void Awake()
		{
			audioSource = GetComponent<AudioSource>();
			animator = GetComponent<Animator>();
			base.gameObject.SetActive(false);
		}

		public override void OnAchievementGrant(Achievement achievement)
		{
			base.gameObject.SetActive(true);
			audioSource.Play();
			animator.Play("Achievement_Popup_Base_Animation", 0, 0f);
			string path = achievement.iconPath.Replace("Assets/Journal/Resources/", string.Empty).Replace(".png", string.Empty).Replace(".jpeg", string.Empty);
			achievementIconPopup.sprite = Resources.Load<Sprite>(path);
			popupTitleText.text = string.Format(LocalisationManager.GetTranslation(3499), LocalisationManager.GetTranslation(achievement.title));
			popupDescriptionText.text = LocalisationManager.GetTranslation(achievement.description);
		}

		protected void Update()
		{
			if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.96f)
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
