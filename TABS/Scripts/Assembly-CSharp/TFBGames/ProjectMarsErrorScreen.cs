using System;
using GamepadUI.StateManager.Core;
using TMPro;
using UnityEngine;

namespace TFBGames
{
	public class ProjectMarsErrorScreen : UISubMenu
	{
		[SerializeField]
		private TMP_Text messageText;

		private event Action errorScreenCallback;

		public void DisplayMessage(string message, Action callbackAction = null)
		{
			if (messageText != null)
			{
				messageText.text = message;
			}
			this.errorScreenCallback = callbackAction;
		}

		public override void Close()
		{
			base.Close();
			Action action = this.errorScreenCallback;
			this.errorScreenCallback = null;
			action?.Invoke();
		}
	}
}
