using UnityEngine;
using UnityEngine.Events;

public class LookAtTargetButtonReference : MonoBehaviour
{
	[Tooltip("Assign a LookAtTarget button here to control it from other scripts.")]
	public LookAtTarget targetButton;

	public void SetButtonActive(bool isActive)
	{
	}

	public void RegisterOnClickDown(UnityAction action)
	{
	}

	public void RegisterOnClickUp(UnityAction action)
	{
	}
}
