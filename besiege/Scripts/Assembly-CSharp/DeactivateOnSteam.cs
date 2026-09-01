using UnityEngine;

public class DeactivateOnSteam : DeactivateOnBase
{
	public DeactivateOnSteam()
	{
		Deactivated = true;
	}

	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
