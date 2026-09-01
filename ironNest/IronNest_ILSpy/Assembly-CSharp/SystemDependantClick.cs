using Heathen.SteamworksIntegration.API;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

public class SystemDependantClick : MonoBehaviour
{
	private UnityEvent keyboardClick;

	private UnityEvent gamepadClick;

	private DynamicCursorManager cursorManager;

	public void SystemDependantClickCheck()
	{
		((App._003CInitialised_003Ek__BackingField && (SteamUtils.IsSteamRunningOnSteamDeck() || cursorManager.IsCurrentDeviceGamepad())) ? gamepadClick : keyboardClick)?.Invoke();
	}
}
