using UnityEngine.Events;

public class Bug_CameraAbilityGeneric : Bug_CameraAbility
{
	public UnityEvent enableEvent;

	public UnityEvent disableEvent;

	public UnityEvent isOnEvent;

	public UnityEvent isOffEvent;

	public override void Disable()
	{
		disableEvent.Invoke();
	}

	public override void Enable()
	{
		enableEvent.Invoke();
	}

	private void Update()
	{
		if (IsActive)
		{
			isOnEvent.Invoke();
		}
		else
		{
			isOffEvent.Invoke();
		}
	}
}
