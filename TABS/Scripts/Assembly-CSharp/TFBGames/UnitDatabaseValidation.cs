using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public static class UnitDatabaseValidation
	{
		public static bool ValidateBluePrints(List<UnitBlueprint> unitBlueprints, bool saveAssetChanges = false)
		{
			bool flag = true;
			foreach (UnitBlueprint unitBlueprint in unitBlueprints)
			{
				flag &= unitBlueprint.Validate();
			}
			return flag;
		}

		public static bool ValidateComponents(List<GameObject> objectsToValidate, bool saveAssetChanges = false)
		{
			bool flag = true;
			foreach (GameObject item in objectsToValidate)
			{
				IValidatable[] componentsInChildren = item.GetComponentsInChildren<IValidatable>(includeInactive: true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					flag &= ValidateComponent(componentsInChildren[i]);
				}
			}
			return flag;
		}

		private static bool ValidateComponent(IValidatable validatable)
		{
			return validatable?.Validate() ?? true;
		}
	}
}
