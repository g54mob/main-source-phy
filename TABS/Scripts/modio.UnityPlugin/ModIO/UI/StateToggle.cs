using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Toggle))]
	public class StateToggle : StateToggleDisplay
	{
		public override bool isOn
		{
			get
			{
				return base.gameObject.GetComponent<Toggle>().isOn;
			}
			set
			{
				base.gameObject.GetComponent<Toggle>().SetIsOnWithoutNotify(value);
			}
		}
	}
}
