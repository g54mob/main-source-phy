using UnityEngine;
using UnityEngine.Events;

public class AgentAnimation : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private string _movementSpeed;

	[SerializeField]
	private string jump;

	public UnityEvent OnStep;

	public void SetSpeed(float speed)
	{
	}

	public void Jump(string jumpTrigger)
	{
	}

	public void PlayAnimation(string trigger)
	{
	}

	public void StepEvent()
	{
	}

	public void SetBool(string paramName, bool value)
	{
	}

	public void SetTrigger(string paramName)
	{
	}

	public void ResetTrigger(string paramName)
	{
	}
}
