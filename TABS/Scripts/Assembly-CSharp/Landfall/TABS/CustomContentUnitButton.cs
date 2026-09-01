using UnityEngine;

namespace Landfall.TABS
{
	public class CustomContentUnitButton : UnitButtonBase
	{
		public override void OnClick()
		{
			base.OnClick();
			Object.FindObjectOfType<UnitCreatorFactionBrowser>().ShowUnit(unitBlueprint);
		}
	}
}
