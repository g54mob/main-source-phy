using UnityEngine;

public class DeactivateOnNotModIO : DeactivateOnBase
{
	public DeactivateOnNotModIO()
	{
		Deactivated = true;
	}

	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
