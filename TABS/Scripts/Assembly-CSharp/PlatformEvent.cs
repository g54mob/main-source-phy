using System;
using UnityEngine.Events;

[Serializable]
public class PlatformEvent
{
	public SettingsInstance.Platform[] platforms;

	public UnityEvent createdOnDesktop;

	public UnityEvent createdOnXbox;

	public UnityEvent createdOnPlaystation;

	public UnityEvent createdOnSwitch;
}
