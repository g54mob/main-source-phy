using UnityEngine;

[AddComponentMenu("Camera-Control/Smooth Look At")]
public class SmoothLookAt : MonoBehaviour
{
	public Transform target;

	public float damping = 6f;

	public bool smooth = true;

	private void LateUpdate()
	{
		if (!(target == null))
		{
			if (smooth)
			{
				Quaternion b = Quaternion.LookRotation(target.position - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * damping);
			}
			else
			{
				base.transform.LookAt(target);
			}
		}
	}

	private void Start()
	{
		if ((bool)GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
}
