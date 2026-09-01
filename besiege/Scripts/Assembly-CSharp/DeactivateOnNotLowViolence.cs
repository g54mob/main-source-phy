using UnityEngine;

public class DeactivateOnNotLowViolence : DeactivateOnBase
{
	public DeactivateOnNotLowViolence()
	{
		Deactivated = true;
	}

	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
