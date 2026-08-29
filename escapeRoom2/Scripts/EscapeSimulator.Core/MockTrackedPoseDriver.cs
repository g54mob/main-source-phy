using UnityEngine;

public class MockTrackedPoseDriver : MonoBehaviour
{
	public Transform parent;

	public Vector3 parentOffset = new Vector3(0.2f, -0.1f, 0.5f);

	private float yaw;

	private float roll;

	public void update(bool isActive)
	{
		if (isActive)
		{
			yaw += Input.GetAxis("Mouse X") * 5f;
			roll += Input.GetAxis("Mouse Y") * 5f;
			roll = Mathf.Clamp(roll, -80f, 120f);
			base.transform.localRotation = Quaternion.Euler(0f - roll, yaw, 0f);
		}
		if (parent != null)
		{
			base.transform.rotation = parent.rotation * Quaternion.Euler(0f - roll, yaw, 0f);
			base.transform.position = parent.TransformPoint(parentOffset);
		}
	}
}
