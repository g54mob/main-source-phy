using UnityEngine;

[AddComponentMenu("Camera-Control/Smooth Follow")]
public class SmoothFollow : MonoBehaviour
{
	public Transform target;

	public float distance = 10f;

	public float height = 5f;

	public float heightDamping = 2f;

	public float rotationDamping = 3f;

	private void LateUpdate()
	{
		if (!(target == null))
		{
			float y = target.eulerAngles.y;
			float b = target.position.y + height;
			float y2 = base.transform.eulerAngles.y;
			float y3 = base.transform.position.y;
			y2 = Mathf.LerpAngle(y2, y, rotationDamping * Time.deltaTime);
			y3 = Mathf.Lerp(y3, b, heightDamping * Time.deltaTime);
			Quaternion quaternion = Quaternion.Euler(0f, y2, 0f);
			base.transform.position = target.position;
			base.transform.position -= quaternion * Vector3.forward * distance;
			Vector3 position = base.transform.position;
			base.transform.position = new Vector3(position.x, y3, position.z);
			base.transform.LookAt(target);
		}
	}
}
