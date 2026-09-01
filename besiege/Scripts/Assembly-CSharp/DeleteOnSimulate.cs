using UnityEngine;

public class DeleteOnSimulate : SimBehaviour
{
	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			base.transform.parent = null;
			Object.Destroy(base.gameObject);
		}
	}
}
