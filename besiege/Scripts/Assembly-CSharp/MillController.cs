using UnityEngine;

public class MillController : MonoBehaviour
{
	public float rotationSpeed;

	private float rotationStep;

	private void Update()
	{
		rotationStep = Time.deltaTime + rotationSpeed * Random.Range(1f, 3f);
		base.gameObject.transform.Rotate(Vector3.up, rotationStep);
	}
}
