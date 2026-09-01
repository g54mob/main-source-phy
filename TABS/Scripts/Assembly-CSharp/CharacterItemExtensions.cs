using Landfall.TABS;
using UnityEngine;

public static class CharacterItemExtensions
{
	private const string ValidateCustomContentSettingsKey = "VALIDATE_CUSTOM_CONTENT";

	public static bool ShouldBeRunTimeBaked(this CharacterItem characterItem)
	{
		bool num = characterItem.GetComponentsInChildren<MeshRenderer>(includeInactive: false).Length != 0;
		bool flag = characterItem.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive: false).Length != 0;
		return num || flag;
	}

	public static bool ShouldCustomContentBeValidated()
	{
		GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (service == null)
		{
			return false;
		}
		return service.GetSettingsInstance("VALIDATE_CUSTOM_CONTENT").currentValue == 1;
	}
}
