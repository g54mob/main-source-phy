using UnityEngine;

public class ExampleClass : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Object.Destroy(other.gameObject);
	}
}
