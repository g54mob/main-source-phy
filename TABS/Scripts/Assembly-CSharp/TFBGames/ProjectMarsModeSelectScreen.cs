using GamepadUI.StateManager.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TFBGames
{
	public class ProjectMarsModeSelectScreen : UISubMenu
	{
		[SerializeField]
		protected Button earthModeButton;

		[SerializeField]
		protected Button createMarsButton;

		[SerializeField]
		protected Button quickMarsButton;

		[SerializeField]
		private GameObject achievementDisclaimer;

		public void SetEarthModeButtonEvent(UnityAction action)
		{
			earthModeButton.onClick.AddListener(action);
		}

		public void SetQuickMarsButtonEvent(UnityAction action)
		{
			quickMarsButton.onClick.AddListener(action);
		}

		public void SetCreateMarsButtonEvent(UnityAction action)
		{
			createMarsButton.onClick.AddListener(action);
		}

		protected override void Awake()
		{
			earthModeButton.gameObject.SetActive(value: true);
			base.Awake();
		}
	}
}
