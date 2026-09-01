using System.Collections.Generic;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorStatsPageUI : UnitEditorSubMenu
	{
		public GameObject statsCell;

		public Transform contentParent;

		private List<UnitEditorStatCell> cells = new List<UnitEditorStatCell>();

		public void SpawnStats(UnitEditorManager.StatsWrapper[] stats)
		{
			ClearStats();
			for (int i = 0; i < stats.Length; i++)
			{
				cells.Add(SpawnStatsCell(stats[i]));
			}
			UIHelpers.CreateExplicitLinearNavigation(contentParent.GetSelectableChildren(), horizontal: false);
		}

		private void ClearStats()
		{
			if (cells == null || cells.Count <= 0)
			{
				return;
			}
			foreach (UnitEditorStatCell cell in cells)
			{
				cell.Selected -= OnItemSelected;
				Object.Destroy(cell.gameObject);
			}
			cells.Clear();
		}

		protected override void PerformIncreaseAction()
		{
			base.PerformIncreaseAction();
			if (base.SelectedItem is UnitEditorStatCell unitEditorStatCell)
			{
				unitEditorStatCell.Increase();
			}
		}

		protected override void PerformDecreaseAction()
		{
			base.PerformDecreaseAction();
			if (base.SelectedItem is UnitEditorStatCell unitEditorStatCell)
			{
				unitEditorStatCell.Decrease();
			}
		}

		public void UpdateStat()
		{
			int count = cells.Count;
			for (int i = 0; i < count; i++)
			{
				cells[i].UpdateValue(forceNewValue: true);
			}
		}

		private UnitEditorStatCell SpawnStatsCell(UnitEditorManager.StatsWrapper stat)
		{
			UnitEditorStatCell component = Object.Instantiate(statsCell, contentParent).GetComponent<UnitEditorStatCell>();
			component.Init(stat);
			component.Selected += OnItemSelected;
			return component;
		}

		protected override void OnItemSelected(UnitEditorSelectableItem item)
		{
			if (item is UnitEditorStatCell selectedItem)
			{
				base.SelectedItem = selectedItem;
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			ClearStats();
		}
	}
}
