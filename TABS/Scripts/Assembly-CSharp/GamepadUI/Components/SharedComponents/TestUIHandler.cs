using GamepadUI.StateManager.Core;
using UnityEngine;

namespace GamepadUI.Components.SharedComponents
{
	public class TestUIHandler : UIComponent
	{
		protected override void OnOpen()
		{
			Debug.Log("TestHandler opened Directly");
		}

		protected override void OnClose()
		{
			Debug.Log("TestHandler Closed Directly");
		}
	}
}
