using System.Collections.Generic;
using Kamgam.SettingsGenerator;
using UnityEngine;
using UnityEngine.Events;

public class SettingsExitHandler : MonoBehaviour
{
	[SerializeField]
	private SettingsProvider _settingsProvider;

	[SerializeField]
	private UnityEvent _onExit;

	[SerializeField]
	private UnityEvent<List<ISetting>> _onUnappliedSettings;

	public void TryExitSettings()
	{
	}

	private bool HasUnappliedSettings(out List<ISetting> unappliedSettings)
	{
		unappliedSettings = null;
		return false;
	}
}
