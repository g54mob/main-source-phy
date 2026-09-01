using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimation : MonoBehaviour
{
	private Animator _animator;

	private string _movementSpeed;

	private string jump;

	public UnityEvent OnStep;

	public void SetSpeed(float speed)
	{
		_animator.SetFloat(_movementSpeed, speed);
	}

	public void Jump(string jumpTrigger)
	{
		if (!string.IsNullOrEmpty(jumpTrigger))
		{
			_animator.SetTrigger(jumpTrigger);
		}
		else
		{
			_animator.SetTrigger(jump);
		}
	}

	public void PlayAnimation(string trigger)
	{
		if (!string.IsNullOrEmpty(trigger))
		{
			_animator.SetTrigger(trigger);
		}
	}

	public void StepEvent()
	{
		OnStep.Invoke();
	}

	public void SetBool(string paramName, bool value)
	{
		if (!string.IsNullOrEmpty(paramName))
		{
			_animator.SetBool(paramName, value);
		}
	}

	public void SetTrigger(string paramName)
	{
		if (!string.IsNullOrEmpty(paramName))
		{
			_animator.SetTrigger(paramName);
		}
	}

	public void ResetTrigger(string paramName)
	{
		if (!string.IsNullOrEmpty(paramName))
		{
			_animator.ResetTrigger(paramName);
		}
	}

	public AgentAnimation()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A882]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_movementSpeed = "MovementSpeed";
		jump = "Jump";
		base._002Ector();
	}
}
