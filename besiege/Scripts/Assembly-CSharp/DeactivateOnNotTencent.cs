using UnityEngine;

public class DeactivateOnNotTencent : DeactivateOnBase
{
	public DeactivateOnNotTencent()
	{
		Deactivated = true;
	}

	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
