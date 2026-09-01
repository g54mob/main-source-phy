using UnityEngine;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	[RequireComponent(typeof(Selectable))]
	public class SelectionLingerer : MonoBehaviour
	{
		protected Selectable selectable;

		protected bool _selectableIsInteractable;

		public Selectable Selectable => null;

		public void OnEnable()
		{
		}

		public void Update()
		{
		}
	}
}
