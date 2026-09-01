using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorSelectableRiderItem : UnitEditorSelectableItem
	{
		[SerializeField]
		private Button editRiderButton;

		[SerializeField]
		private Button removeRiderButton;

		[SerializeField]
		private UnitEditorRiderUI unitEditorRiderUI;

		public UnitEditorRiderUI RiderUI => unitEditorRiderUI;

		public void EditRider()
		{
			if (editRiderButton != null)
			{
				editRiderButton.onClick.Invoke();
			}
		}

		public void RemoveRider()
		{
			if (removeRiderButton != null)
			{
				removeRiderButton.onClick.Invoke();
			}
		}
	}
}
