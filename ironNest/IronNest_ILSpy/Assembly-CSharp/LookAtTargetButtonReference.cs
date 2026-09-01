using UnityEngine;
using UnityEngine.Events;

public class LookAtTargetButtonReference : MonoBehaviour
{
	public LookAtTarget targetButton;

	public void SetButtonActive(bool isActive)
	{
		if (targetButton != null)
		{
			targetButton.SetActive(isActive);
		}
	}

	public void RegisterOnClickDown(UnityAction action)
	{
		if (targetButton != null)
		{
			LookAtTarget lookAtTarget = targetButton;
			lookAtTarget.onClickDown.AddListener(action);
		}
	}

	public void RegisterOnClickUp(UnityAction action)
	{
		if (targetButton != null)
		{
			LookAtTarget lookAtTarget = targetButton;
			lookAtTarget.onClickUp.AddListener(action);
		}
	}
}
