using GamepadUI.StateManager.Core;
using UnityEngine;

namespace TFBGames
{
	public class LocalMultiplayerUIHandler : UISubMenu
	{
		[SerializeField]
		protected LocalMultiplayerUIComponent[] UIComponents;

		private InputService inputService;

		private void Start()
		{
			inputService = ServiceLocator.GetService<InputService>();
		}

		public override void Open()
		{
			base.Open();
			EnableNextWorkflowStep(0);
			if (inputService != null)
			{
				inputService.ClearPlayerInputDevices();
			}
		}

		public void EnableNextWorkflowStep(int nextWorkflowStep)
		{
			DisableAllComponents();
			if (nextWorkflowStep < UIComponents.Length)
			{
				UIComponents[nextWorkflowStep].UIComponent.SetActive(value: true);
				if (UIComponents[nextWorkflowStep].ComponentAnimation != null)
				{
					LocalMultiplayerUIComponent obj = UIComponents[nextWorkflowStep];
					obj.ComponentAnimation.PlayIn();
					MultiplayerSettingsMenu component = obj.UIComponent.GetComponent<MultiplayerSettingsMenu>();
					if ((bool)component)
					{
						component.Open();
					}
				}
			}
			else
			{
				base.Close();
				Debug.LogWarning(string.Format("{0} does not contain {1} elements, {2} has been closed", "UIComponents", nextWorkflowStep, "UISubMenu"));
			}
		}

		private void DisableAllComponents()
		{
			LocalMultiplayerUIComponent[] uIComponents = UIComponents;
			foreach (LocalMultiplayerUIComponent localMultiplayerUIComponent in uIComponents)
			{
				if (localMultiplayerUIComponent.UIComponent.activeInHierarchy && localMultiplayerUIComponent.ComponentAnimation != null)
				{
					localMultiplayerUIComponent.ComponentAnimation.PlayOut();
				}
				localMultiplayerUIComponent.UIComponent.SetActive(value: false);
			}
		}
	}
}
