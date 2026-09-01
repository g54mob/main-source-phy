using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class InputSystemBindingDemo : MonoBehaviour
{
	public SettingsProvider Provider;

	public void Awake()
	{
	}

	public void Start()
	{
		Settings settings = Provider.Settings;
	}
}
