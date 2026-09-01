using UnityEngine;

public class LookAtcamAndscale : MonoBehaviour
{
	public Transform target;

	public Transform myTransform;

	public Vector3 point;

	public float scaleAmount;

	public float distanceScaleAmount = 1f;

	public float refreshRate = 0.3f;

	private float distance;

	private Vector3 lastPosition;

	private Vector3 lastTargetPosition;

	private Vector3 scale;

	private Transform childTransform;

	private void Start()
	{
		if (StatMaster.isHeadless)
		{
			base.enabled = false;
			return;
		}
		target = Camera.main.transform;
		myTransform = base.transform;
		Vector3 vector = myTransform.lossyScale;
		if (vector.x == 0f || vector.y == 0f || vector.z == 0f)
		{
			vector = Vector3.one;
		}
		scale = new Vector3(myTransform.localScale.x / vector.x, myTransform.localScale.y / vector.y, myTransform.localScale.z / vector.z);
		childTransform = myTransform.GetChild(0);
		UpdateOrientation(target.position);
	}

	private void Update()
	{
		if (StatMaster.Mode.keyMapView)
		{
			Vector3 position = target.position;
			if (myTransform.position != lastPosition || position != lastTargetPosition)
			{
				UpdateOrientation(position);
			}
		}
	}

	private void UpdateOrientation(Vector3 targetPos)
	{
		childTransform.LookAt(targetPos);
		distance = Vector3.Distance(targetPos, myTransform.position) * scaleAmount;
		base.transform.localScale = scale * distance;
		lastPosition = myTransform.position;
		lastTargetPosition = targetPos;
	}
}
