using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.transform.rotation = Quaternion.LookRotation(base.transform.position - Camera.main.transform.position);
	}
}
