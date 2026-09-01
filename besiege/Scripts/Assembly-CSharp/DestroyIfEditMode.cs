using UnityEngine;

public class DestroyIfEditMode : SimBehaviour
{
	protected override void Start()
	{
		base.Start();
		InvokeRepeating("CheckAndDestroy", Random.Range(0f, 0.2f), 0.2f);
	}

	private void CheckAndDestroy()
	{
		if (!base.isSimulating)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
