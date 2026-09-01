using UnityEngine;

public class DeleteColliderOnSimulate : SimBehaviour
{
	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			Object.Destroy(GetComponent<Collider>());
		}
	}
}
