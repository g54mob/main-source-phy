using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugMenuItemCheckbox : MonoBehaviour
	{
		[Header("Bindings")]
		public MMDebugMenuSwitch Switch;

		public Text SwitchText;

		public string CheckboxEventName;

		protected bool _valueSetThisFrame;

		protected bool _listening;

		public virtual void TriggerCheckboxEvent()
		{
		}

		public virtual void TriggerCheckboxEventTrue()
		{
		}

		public virtual void TriggerCheckboxEventFalse()
		{
		}

		protected virtual void OnMMDebugMenuCheckboxEvent(string checkboxEventName, bool value, MMDebugMenuCheckboxEvent.EventModes eventMode)
		{
		}

		public virtual void OnEnable()
		{
		}

		public virtual void OnDestroy()
		{
		}
	}
}
