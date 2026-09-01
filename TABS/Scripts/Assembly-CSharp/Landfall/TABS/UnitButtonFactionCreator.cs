using UnityEngine;

namespace Landfall.TABS
{
	public class UnitButtonFactionCreator : UnitButtonBase
	{
		private FactionCreatorManager factionCreator;

		private void Awake()
		{
			factionCreator = Object.FindObjectOfType<FactionCreatorManager>();
		}

		public override void OnClick()
		{
			base.OnClick();
			factionCreator.UnSelectUnit(unitBlueprint);
			factionCreator.UpdateAddGlyph(this);
		}

		public override void OnEnter()
		{
			factionCreator.UpdateAddGlyph(this);
		}
	}
}
