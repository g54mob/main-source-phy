using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMDebugMenuButtonEventListener : MonoBehaviour
	{
		[Header("Event")]
		public string ButtonEventName;

		public MMDButtonPressedEvent MMDEvent;

		[Header("Test")]
		public bool TestValue;

		[MMInspectorButton("TestSetValue")]
		public bool TestSetValueButton;

		protected virtual void TestSetValue()
		{
		}

		protected virtual void OnMMDebugMenuButtonEvent(string buttonEventName, bool value, MMDebugMenuButtonEvent.EventModes eventMode)
		{
		}

		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}
	}
}
