using UnityEngine;

public class KillIfEditMode : MonoBehaviour
{
	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
