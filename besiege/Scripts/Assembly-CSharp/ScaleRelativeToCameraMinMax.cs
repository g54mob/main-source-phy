using UnityEngine;

public class ScaleRelativeToCameraMinMax : MonoBehaviour
{
	private const float DefaultFov = 41f;

	public float objectScale = 1f;

	public float min;

	public float max = 10f;

	public bool lookAtCam;

	private Vector3 initialScale;

	private Camera cam;

	private MouseOrbit orbit;

	private Transform t;

	private Transform camTransform;

	protected void Awake()
	{
		initialScale = base.transform.localScale;
		cam = SingleInstanceFindOnly<AddPiece>.Instance.mainCam;
		camTransform = cam.transform;
		t = base.transform;
	}

	protected void OnEnable()
	{
		orbit = SingleInstanceFindOnly<MouseOrbit>.Instance;
		UpdateScale();
	}

	protected void LateUpdate()
	{
		UpdateScale();
	}

	protected void UpdateScale()
	{
		float num = cam.fieldOfView / 41f;
		float num2 = Mathf.Abs(orbit.cameraPlane.GetDistanceToPoint(t.position));
		float value = num2 * objectScale * num;
		value = Mathf.Clamp(value, min, max);
		Vector3 vector = initialScale * value;
		Vector3 localScale = new Vector3((!(vector.x > 0f)) ? (0f - vector.x) : vector.x, (!(vector.y > 0f)) ? (0f - vector.y) : vector.y, (!(vector.z > 0f)) ? (0f - vector.z) : vector.z);
		t.localScale = localScale;
		if (lookAtCam)
		{
			t.LookAt(camTransform);
		}
	}
}
