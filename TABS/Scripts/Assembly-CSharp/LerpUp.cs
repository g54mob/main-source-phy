using UnityEngine;

public class LerpUp : MonoBehaviour
{
	[Range(0f, 5f)]
	public float amount = 0.5f;

	private void Start()
	{
		base.transform.rotation = Quaternion.LookRotation(base.transform.forward + amount * Vector3.up);
	}
}
