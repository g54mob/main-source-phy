using UnityEngine;

public class ScaleRelativeToCamera : MonoBehaviour
{
	private const float DefaultFov = 41f;

	public float objectScale = 1f;

	private Vector3 initialScale;

	private Camera cam;

	protected void Awake()
	{
		initialScale = base.transform.localScale;
		cam = Camera.main;
	}

	protected void OnEnable()
	{
		UpdateScale();
	}

	protected void LateUpdate()
	{
		UpdateScale();
	}

	protected void UpdateScale()
	{
		float distanceToPoint = new Plane(cam.transform.forward, cam.transform.position).GetDistanceToPoint(base.transform.position);
		float num = cam.fieldOfView / 41f;
		Vector3 vector = initialScale * distanceToPoint * objectScale * num;
		Vector3 localScale = new Vector3((!(vector.x > 0f)) ? (0f - vector.x) : vector.x, (!(vector.y > 0f)) ? (0f - vector.y) : vector.y, (!(vector.z > 0f)) ? (0f - vector.z) : vector.z);
		base.transform.localScale = localScale;
	}
}
