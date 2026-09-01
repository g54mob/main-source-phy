using UnityEngine;

public class VortexRotataor : MonoBehaviour
{
	public float AnglePerSecond;

	private void Update()
	{
		base.transform.Rotate(Vector3.forward * AnglePerSecond * Time.deltaTime);
	}
}
