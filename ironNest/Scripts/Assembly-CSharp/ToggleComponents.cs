using UnityEngine;

public class ToggleComponents : MonoBehaviour
{
	[SerializeField]
	private bool toggleOnAwake;

	[SerializeField]
	private bool toggleOnAwakeValue;

	[Space]
	[SerializeField]
	private Component[] components;

	private void Awake()
	{
	}

	public void Toggle(bool isEnabled)
	{
	}
}
