using Kamgam.SettingsGenerator;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBindingMirrorInitializer : MonoBehaviour
{
	[SerializeField]
	private SettingsProvider _settingsProvider;

	[SerializeField]
	private InputActionAsset _inputActionAsset;

	[SerializeField]
	private InputBindingMirrorsConfig _mirrorConfig;

	private static InputBindingMirrorInitializer Instance;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void Start()
	{
	}

	private void ConfigureMirrors()
	{
	}

	private void CleanupMirrors()
	{
	}

	private void ApplyMirrors(SettingString setting, InputBindingMirror bindingMirror)
	{
	}

	private void OnSettingStringChanged(SettingString setting)
	{
	}
}
