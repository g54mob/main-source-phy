using UnityEngine;

public class DestroyOnOtherDestroy : MonoBehaviour
{
	public GameObject other;

	private void LateUpdate()
	{
		if (other == null)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
