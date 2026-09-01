using UnityEngine;

public class RemoveOnMountedUnit : MonoBehaviour
{
	private void Start()
	{
		Mount component = base.transform.root.GetComponent<Mount>();
		if ((bool)component && component.IsMounted)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
