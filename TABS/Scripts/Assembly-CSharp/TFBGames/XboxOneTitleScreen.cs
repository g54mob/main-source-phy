using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;

namespace TFBGames
{
	public class XboxOneTitleScreen : MonoBehaviour
	{
		private enum State
		{
			WaitForInput = 0,
			ShowWarning = 1,
			SelectUser = 2,
			Done = 3
		}

		private const float DisableInputDuration = 1f;

		private const string WarningMessage = "No user profile has been selected. Progress will not be saved if you continue. Continue?";

		[SerializeField]
		[Tooltip("Holds the press button text, which will be hidden when the scene starts loading.")]
		protected GameObject pressButtonTextHolder;

		[SerializeField]
		[Tooltip("Holds the loading indicator, which will be shown when the scene starts loading.")]
		protected GameObject loadingIndicatorHolder;

		private IPlatformUtils platformUtils;

		private AccountManager accountManager;

		private PlayerActions playerActions;

		private float startTime;

		private State state;

		private bool showWarningInNextUpdate;

		private void Start()
		{
			platformUtils = ServiceLocator.GetService<IPlatformUtils>();
			accountManager = ServiceLocator.GetService<AccountManager>();
			playerActions = PlayerActions.Instance;
			startTime = Time.realtimeSinceStartup;
			Localizer.InitializeWithDefaultSystemLanguage();
			ShowPressButtonText(visible: true);
			ShowLoadingIndicator(visible: false);
		}

		private void Update()
		{
			if (Time.realtimeSinceStartup - startTime < 1f)
			{
				return;
			}
			switch (state)
			{
			case State.WaitForInput:
				if (!platformUtils.IsUIOpenOrLostFocus && playerActions.m_accept.WasPressed)
				{
					if (accountManager.ActiveAccount == null)
					{
						SetState(State.ShowWarning);
					}
					else
					{
						SetState(State.SelectUser);
					}
				}
				break;
			case State.SelectUser:
				SetState(State.Done);
				break;
			case State.ShowWarning:
				if (showWarningInNextUpdate)
				{
					showWarningInNextUpdate = false;
					ServiceLocator.GetService<ModalPanel>().Choice("POPUP_ERROR", "No user profile has been selected. Progress will not be saved if you continue. Continue?", delegate
					{
						SetState(State.SelectUser);
					}, delegate
					{
						SetState(State.WaitForInput);
					});
				}
				break;
			}
		}

		private void SetState(State newState)
		{
			state = newState;
			switch (state)
			{
			case State.ShowWarning:
				showWarningInNextUpdate = true;
				break;
			case State.SelectUser:
				ShowPressButtonText(visible: false);
				ShowLoadingIndicator(visible: true);
				break;
			case State.Done:
				accountManager.OnSelectedActiveAccount();
				break;
			}
		}

		private void ShowPressButtonText(bool visible)
		{
			if (pressButtonTextHolder != null && pressButtonTextHolder.activeSelf != visible)
			{
				pressButtonTextHolder.SetActive(visible);
			}
		}

		private void ShowLoadingIndicator(bool visible)
		{
			if (loadingIndicatorHolder != null && loadingIndicatorHolder.activeSelf != visible)
			{
				loadingIndicatorHolder.SetActive(visible);
			}
		}
	}
}
