using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleUIButtonInput : MonoBehaviour
	{
		[Header("References")]
		public Button button;

		[Header("Parameters")]
		public KeyCode keyCode;

		public KeyCode secondaryKeyCode;

		private void Update()
		{
			if (Input.GetKeyDown(keyCode) || Input.GetKeyDown(secondaryKeyCode))
			{
				EventSystem eventSystem = Object.FindAnyObjectByType<EventSystem>();
				if (eventSystem != null && button != null)
				{
					ExecuteEvents.Execute(button.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
				}
			}
		}
	}
}
