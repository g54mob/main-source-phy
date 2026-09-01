using UnityEngine;

public class DeactivateOnNotCinematic : DeactivateOnBase
{
	public DeactivateOnNotCinematic()
	{
		Deactivated = true;
	}

	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
