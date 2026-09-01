using UnityEngine;

namespace ModIO.UI
{
	public class TestMenuLauncher : MonoBehaviour
	{
		public const float BUTTON_HOLD_TIME = 2f;

		public TestMenu testMenu;

		private float buttonTimer;

		private void Update()
		{
			if (testMenu.gameObject.activeSelf)
			{
				return;
			}
			if (Input.GetKey(KeyCode.JoystickButton1))
			{
				buttonTimer += Time.unscaledDeltaTime;
				if (buttonTimer > 2f)
				{
					testMenu.ActivateTestMenu();
				}
			}
			else
			{
				buttonTimer = 0f;
			}
		}
	}
}
