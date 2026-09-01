using UnityEngine;

public abstract class PlatformSpecificOverride : MonoBehaviour
{
	[SerializeField]
	protected SettingsInstance.Platform platformsToOverride;

	protected virtual void Awake()
	{
		if (platformsToOverride.HasFlag(GlobalSettingsHandler.CurrentPlatform))
		{
			ApplyPlatformOverride();
		}
	}

	protected abstract void ApplyPlatformOverride();
}
