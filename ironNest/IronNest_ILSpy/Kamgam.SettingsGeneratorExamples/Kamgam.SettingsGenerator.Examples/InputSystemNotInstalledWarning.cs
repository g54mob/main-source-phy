using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class InputSystemNotInstalledWarning : MonoBehaviour
{
	public GameObject Target;

	private void OnEnable()
	{
		Target.SetActive(value: false);
		Target.SetActive(value: false);
	}
}
