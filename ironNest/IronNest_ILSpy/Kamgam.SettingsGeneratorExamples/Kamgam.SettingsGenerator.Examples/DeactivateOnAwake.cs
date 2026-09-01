using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class DeactivateOnAwake : MonoBehaviour
{
	private void Awake()
	{
		GameObject gameObject = base.gameObject;
		gameObject.SetActive(value: false);
	}
}
