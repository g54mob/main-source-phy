using UnityEngine;

public class BarleyDestroy : MonoBehaviour
{
	public bool isFlattened;

	private void OnTriggerEnter(Collider other)
	{
		if (!isFlattened && other.transform.root.gameObject.layer == 8)
		{
			FlattenBarley();
		}
	}

	private void FlattenBarley()
	{
		isFlattened = true;
		GetComponent<Renderer>().enabled = false;
	}
}
