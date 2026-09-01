using UnityEngine;

public class DeactivateOnLowViolence : DeactivateOnBase
{
	public void Awake()
	{
		if (SingleInstance<StatMaster>.Instance.LowViolence)
		{
			Deactivated = true;
			Object.Destroy(base.gameObject);
		}
	}
}
