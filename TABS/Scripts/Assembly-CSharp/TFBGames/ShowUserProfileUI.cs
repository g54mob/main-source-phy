using UnityEngine;

namespace TFBGames
{
	public class ShowUserProfileUI : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Specify if the user can change profiles via the user profile UI.")]
		protected bool canChangeProfile;

		[SerializeField]
		[Tooltip("Show the profile UI on start.")]
		protected bool showOnStart;

		[SerializeField]
		[Tooltip("Hide the profile UI when this game object is destroyed (e.g. hide it when the scene change destroys this game object).")]
		protected bool hideOnDestroy;

		[SerializeField]
		[Tooltip("Toggle the profile UI's visibility based on the visibility of these animated objects.")]
		protected CodeAnimation[] toggleVisiblityAnimations;

		private IUserProfileUI userProfileUI;

		private void Start()
		{
			userProfileUI = ServiceLocator.GetService<IUserProfileUI>();
			if (showOnStart)
			{
				ShowProfileUI(visible: true);
			}
			SubscribeToEvents();
		}

		private void OnDestroy()
		{
			if (hideOnDestroy)
			{
				ShowProfileUI(visible: false);
			}
			UnsubscribeFromEvents();
		}

		private void SubscribeToEvents()
		{
			if (userProfileUI == null || toggleVisiblityAnimations == null || toggleVisiblityAnimations.Length == 0)
			{
				return;
			}
			int i = 0;
			for (int num = toggleVisiblityAnimations.Length; i < num; i++)
			{
				CodeAnimation codeAnimation = toggleVisiblityAnimations[i];
				if (!(codeAnimation == null))
				{
					codeAnimation.InPlayed += OnInPlayed;
					codeAnimation.OutPlayed += OnOutPlayed;
				}
			}
		}

		private void UnsubscribeFromEvents()
		{
			if (toggleVisiblityAnimations == null || toggleVisiblityAnimations.Length == 0)
			{
				return;
			}
			int i = 0;
			for (int num = toggleVisiblityAnimations.Length; i < num; i++)
			{
				CodeAnimation codeAnimation = toggleVisiblityAnimations[i];
				if (!(codeAnimation == null))
				{
					codeAnimation.InPlayed -= OnInPlayed;
					codeAnimation.OutPlayed -= OnOutPlayed;
				}
			}
		}

		private void OnInPlayed()
		{
			ShowProfileUI(visible: true);
		}

		private void OnOutPlayed()
		{
			ShowProfileUI(visible: false);
		}

		private void ShowProfileUI(bool visible)
		{
			if (userProfileUI == null || userProfileUI.IsVisible == visible)
			{
				if (visible && userProfileUI != null)
				{
					userProfileUI.SetCanChangeProfile(canChangeProfile);
				}
			}
			else if (visible)
			{
				userProfileUI.Show(canChangeProfile);
			}
			else
			{
				userProfileUI.Hide();
			}
		}
	}
}
