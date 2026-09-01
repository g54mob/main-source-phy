using UnityEngine;

public class CalcDistance : MonoBehaviour
{
	public Vector3 machinePos;

	private Vector3 distance;

	public float effectiveDistance;

	private void Start()
	{
	}

	private void Update()
	{
		machinePos = Machine.Active().MachineCenterPos;
		distance = machinePos - base.transform.position;
		effectiveDistance = distance.sqrMagnitude;
	}
}
