using System;
using InControl;
using Landfall.TABS_Input;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GamepadUI.StateManager.Core
{
	[Serializable]
	public class ChangeStateEvent
	{
		public Button Button;

		public string ActionName;

		[FormerlySerializedAs("Component")]
		public UIComponent ComponentToOpen;

		public bool BackgroundWhenOpened;

		[FormerlySerializedAs("SelectEvent")]
		public UnityEvent OnPressedEvent;

		public PlayerAction PlayerAction { get; private set; }

		public void SetPlayerAction(PlayerActions playerActions)
		{
			PlayerAction = playerActions.GetPlayerActionByName(ActionName);
		}
	}
}
