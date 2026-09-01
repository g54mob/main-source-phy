using Landfall.TABS;
using ModIO.UI;
using UnityEngine;

namespace TFBGames
{
	public class ProjectMarsBattleUserUI : PlayerProfileUI
	{
		[SerializeField]
		private GameObjectToggle checkboxToggle;

		private SimpleStateAnimation stateAnimation;

		public void Open()
		{
			if (stateAnimation != null)
			{
				stateAnimation.SetState(SimpleStateAnimation.State.State01);
			}
		}

		public void Close()
		{
			if (stateAnimation != null)
			{
				stateAnimation.SetState(SimpleStateAnimation.State.State02);
			}
		}

		private void Awake()
		{
			stateAnimation = GetComponent<SimpleStateAnimation>();
			if (base.Profile != null)
			{
				base.Profile.StatusUpdated += base.UpdateStatusLabel;
				base.Profile.StatusUpdated += UpdateReadyState;
			}
		}

		public override void SetPlayerProfile(PlayerProfile profile)
		{
			base.SetPlayerProfile(profile);
			if (base.Profile != null)
			{
				base.Profile.StatusUpdated += base.UpdateStatusLabel;
				base.Profile.StatusUpdated += UpdateReadyState;
			}
		}

		protected void UpdateReadyState(string status)
		{
			if (checkboxToggle != null)
			{
				bool isOn = status.ToLower().Contains(LocalMultiplayerPlayerStatus.Ready.ToString().ToLower());
				checkboxToggle.isOn = isOn;
			}
		}

		protected override void ClearProfile()
		{
			if (base.Profile != null)
			{
				base.Profile.StatusUpdated -= base.UpdateStatusLabel;
				base.Profile.StatusUpdated -= UpdateReadyState;
			}
			base.ClearProfile();
		}
	}
}
