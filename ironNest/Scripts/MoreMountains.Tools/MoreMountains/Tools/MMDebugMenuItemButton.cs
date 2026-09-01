using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugMenuItemButton : MonoBehaviour
	{
		[Header("Bindings")]
		public Button TargetButton;

		public Text ButtonText;

		public Image ButtonBg;

		public string ButtonEventName;

		protected bool _listening;

		public virtual void TriggerButtonEvent()
		{
		}

		protected virtual void OnMMDebugMenuButtonEvent(string checkboxEventName, bool active, MMDebugMenuButtonEvent.EventModes eventMode)
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
