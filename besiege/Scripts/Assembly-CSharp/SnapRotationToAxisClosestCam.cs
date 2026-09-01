using UnityEngine;

public class SnapRotationToAxisClosestCam : MonoBehaviour
{
	public Transform parent;

	private void LateUpdate()
	{
		Vector3 position = base.transform.position;
		Vector3 vector = parent.InverseTransformPoint(Camera.main.transform.position) - parent.InverseTransformPoint(position);
		Quaternion localRotation = Quaternion.LookRotation(vector * -1f, base.transform.up);
		localRotation.eulerAngles = new Vector3(Mathf.Round(localRotation.eulerAngles.x / 90f) * 90f, Mathf.Round(localRotation.eulerAngles.y / 90f) * 90f, Mathf.Round(localRotation.eulerAngles.z / 90f) * 90f);
		base.transform.localRotation = localRotation;
	}
}
