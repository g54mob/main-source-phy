using System;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class TutorialSequence : MonoBehaviour
	{
		public bool runTutorialOnStart = true;

		[Header("PopUp-Messages")]
		public float delayMultiplier = 1f;

		[Space]
		public float firstDelay = 1f;

		public PopUp firstMessageInitial;

		private PopUp firstMessage;

		[Space]
		public float secondDelay = 20f;

		public PopUp secondMessageInitial;

		private PopUp secondMessage;

		public float hotbarDelay = 2f;

		[Space]
		public float thirdDelay = 30f;

		public PopUp thirdMessageInitial;

		private PopUp thirdMessage;

		public float createObjectDelay = 2f;

		[Space]
		public float playModeDelay = 30f;

		[Space]
		public float settingsDelay = 4f;

		public PopUp settingsMessageInitial;

		private PopUp settingsMessage;

		[Space]
		public float levelMenuDelay = 0.5f;

		public PopUp levelMenuMessageInitial;

		private PopUp levelMenuMessage;

		[Header("Other Objects")]
		public ToolBar toolBarInitial;

		private ToolBar toolBar;

		public GameObject levelMenuButtonInitial;

		private GameObject levelMenuButton;

		public GameObject keybindButtonInitial;

		private GameObject keybindButton;

		public GameObject browserButtonInitial;

		private GameObject browserButton;

		private static bool tutorialRunning;

		private void AssertionCheck()
		{
			firstMessage = firstMessageInitial;
			secondMessage = secondMessageInitial;
			thirdMessage = thirdMessageInitial;
			settingsMessage = settingsMessageInitial;
			levelMenuMessage = levelMenuMessageInitial;
			toolBar = toolBarInitial;
			levelMenuButton = levelMenuButtonInitial;
			keybindButton = keybindButtonInitial;
			browserButton = browserButtonInitial;
		}

		private void Start()
		{
			AssertionCheck();
			AddListeners();
			if (tutorialRunning)
			{
				ResetTutorial();
				LeanTween.delayedCall(settingsDelay * delayMultiplier, (System.Action)delegate
				{
					settingsMessage.Show();
				});
			}
			else if (!runTutorialOnStart)
			{
				NonTutorialInit();
			}
			else
			{
				RunTutorial();
			}
		}

		private void AddListeners()
		{
			firstMessage.onHideComplete.AddListener(delegate
			{
				LeanTween.delayedCall(secondDelay * delayMultiplier, (System.Action)delegate
				{
					secondMessage.Show();
				});
			});
			secondMessage.onHideComplete.AddListener(delegate
			{
				toolBar.BuildCategoryHotbar(4);
				toolBar.BuildSubHotbars();
				string hotbarMessage = ((PlayerActions.Instance.InputType != InputType.Controller) ? "Use the Scrollwheel \n to switch Hazard!" : "Use the Shoulderbuttons \n to switch Hazard!");
				LeanTween.delayedCall(hotbarDelay * delayMultiplier, (System.Action)delegate
				{
					PopUp.CreatePopUp(new Vector3(-110f, -220f), hotbarMessage, demandFocus: false, 4f, 13f).Show();
				});
				LeanTween.delayedCall(thirdDelay * delayMultiplier, (System.Action)delegate
				{
					thirdMessage.Show();
				});
			});
			thirdMessage.onHideComplete.AddListener(delegate
			{
				toolBar.BuildCategoryHotbar(3);
				toolBar.BuildSubHotbars();
				toolBar.SwitchHotbar(1);
				LeanTween.delayedCall(createObjectDelay, (System.Action)delegate
				{
					PopUp.CreatePopUp(new Vector3(110f, -220f), "Press F to Create an Object!", demandFocus: false, 4f, 13f).Show();
				});
				LeanTween.delayedCall(playModeDelay * delayMultiplier, (System.Action)delegate
				{
					string message = "TEST YOUR CREATION \n (Press " + PlayerActions.Instance.m_playmode.Bindings[0].Name + ")";
					PopUp popUp = PopUp.CreatePopUp(Vector3.zero, message, demandFocus: false, float.PositiveInfinity);
					popUp.GetComponentInChildren<Button>().interactable = false;
					popUp.Show();
				});
			});
			settingsMessage.onHideComplete.AddListener(delegate
			{
				LeanTween.delayedCall(levelMenuDelay * delayMultiplier, (System.Action)delegate
				{
					levelMenuMessage.Show();
				});
			});
			levelMenuMessage.onShowComplete.AddListener(delegate
			{
				levelMenuButton.SetActive(value: true);
			});
			levelMenuMessage.onHideComplete.AddListener(delegate
			{
				keybindButton.SetActive(value: true);
				browserButton.SetActive(value: true);
				DMUIManager.Instance.canOpen = true;
				FinishTutorial();
			});
		}

		private void NonTutorialInit()
		{
			DMUIManager.Instance.canOpen = true;
		}

		private void ResetTutorial()
		{
			levelMenuButton.SetActive(value: false);
			keybindButton.SetActive(value: false);
			browserButton.SetActive(value: false);
			DMUIManager.Instance.canOpen = false;
			toolBar.DestroyHotbars();
			DMEditor.Instance.SwitchToDefaultTool();
		}

		private void RunTutorial()
		{
			tutorialRunning = true;
			ResetTutorial();
			LeanTween.delayedCall(firstDelay, (System.Action)delegate
			{
				firstMessage.Show();
			});
		}

		private void FinishTutorial()
		{
			while (InputManager.PeekState() == PopUp.inputState)
			{
				InputManager.RemoveState(PopUp.inputState);
			}
			toolBar.BuildCategoryHotbar();
			toolBar.BuildSubHotbars();
			tutorialRunning = false;
		}

		private void Update()
		{
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.I) && !tutorialRunning)
			{
				RunTutorial();
			}
		}
	}
}
