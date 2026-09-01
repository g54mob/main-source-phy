using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMDebugMenuCheckboxEventListener : MonoBehaviour
	{
		[Header("Events")]
		public string CheckboxEventName;

		public MMDCheckboxPressedEvent MMDPressedEvent;

		public MMDCheckboxTrueEvent MMDTrueEvent;

		public MMDCheckboxFalseEvent MMDFalseEvent;

		[Header("Test")]
		public bool TestValue;

		[MMInspectorButton("TestSetValue")]
		public bool TestSetValueButton;

		protected virtual void TestSetValue()
		{
		}

		protected virtual void OnMMDebugMenuCheckboxEvent(string checkboxNameEvent, bool value, MMDebugMenuCheckboxEvent.EventModes eventMode)
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
