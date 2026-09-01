using Landfall.MonoBatch;
using UnityEngine;

public class FollowTransform : BatchedMonobehaviour
{
	public Transform target;

	public Vector3 worldOffset;

	public bool rotation;

	public bool destroyOnTargetNull = true;

	protected override void Start()
	{
		base.Start();
	}

	public override void BatchedUpdate()
	{
		if ((bool)target)
		{
			base.transform.position = target.position + worldOffset;
			if (rotation)
			{
				base.transform.rotation = target.rotation;
			}
		}
		else if (destroyOnTargetNull)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
