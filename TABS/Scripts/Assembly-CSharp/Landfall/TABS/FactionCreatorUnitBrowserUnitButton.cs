using UnityEngine;

namespace Landfall.TABS
{
	public class FactionCreatorUnitBrowserUnitButton : UnitButtonBase
	{
		private bool selected;

		private FactionCreatorManager factionCreator;

		private void Awake()
		{
			factionCreator = Object.FindObjectOfType<FactionCreatorManager>();
		}

		public override void OnEnter()
		{
			targetIconColor = 1f;
			UpdateNameSelect(entering: true);
			factionCreator.UpdateAddGlyph(this);
		}

		public override void OnExit()
		{
			targetIconColor = 0f;
			UpdateNameSelect(entering: false);
		}

		public override void OnClick()
		{
			base.OnClick();
			factionCreator.OnClickUnit(this);
			NameSelect.color = GetColorOverride();
			UpdateTextColor(entering: true);
			factionCreator.UpdateAddGlyph(this);
		}

		private Color GetColorOverride()
		{
			Color result = Color.white;
			if (selected)
			{
				result = factionCreator.GetFactionColor();
			}
			return result;
		}

		private void UpdateTextColor(bool entering)
		{
			if (selected)
			{
				UnitName.color = Color.white;
			}
			else if (entering)
			{
				UnitName.color = TextSelectedColor;
			}
			else
			{
				UnitName.color = Color.white;
			}
		}

		private void UpdateNameSelect(bool entering)
		{
			NameSelect.color = GetColorOverride();
			UpdateTextColor(entering);
			if (!entering)
			{
				if (!selected)
				{
					NameSelect.enabled = false;
				}
				else
				{
					NameSelect.enabled = true;
				}
			}
			else
			{
				NameSelect.enabled = true;
			}
		}

		public void SetSelected(bool newState)
		{
			selected = newState;
			UpdateNameSelect(entering: false);
		}

		public void ApplyNewColor()
		{
			NameSelect.color = GetColorOverride();
		}
	}
}
