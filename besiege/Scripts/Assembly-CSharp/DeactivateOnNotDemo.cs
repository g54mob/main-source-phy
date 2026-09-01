using UnityEngine;

public class DeactivateOnNotDemo : DeactivateOnBase
{
	public DeactivateOnNotDemo()
	{
		Deactivated = true;
	}

	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
