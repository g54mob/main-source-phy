using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorEquippedItemStatsPage : UnitEditorSubMenu
	{
		[SerializeField]
		private UnitEditorStatCell statItem;

		public override void Open()
		{
			base.Open();
			if (statItem != null)
			{
				statItem.Selected += OnItemSelected;
			}
		}

		public override void Close()
		{
			base.Close();
			if (statItem != null)
			{
				statItem.Selected -= OnItemSelected;
			}
		}

		protected override void PerformIncreaseAction()
		{
			base.PerformIncreaseAction();
			if (statItem != null && base.SelectedItem != null && base.SelectedItem == statItem)
			{
				statItem.Increase();
			}
		}

		protected override void PerformDecreaseAction()
		{
			base.PerformDecreaseAction();
			if (statItem != null && base.SelectedItem != null && base.SelectedItem == statItem)
			{
				statItem.Decrease();
			}
		}
	}
}
