using UnityEngine;

public class MasterSlaveAxisMapper : MonoBehaviour
{
	public enum AxisType
	{
		Position = 0,
		Rotation = 1
	}

	public enum Axis
	{
		X = 0,
		Y = 1,
		Z = 2
	}

	[Header("Master Settings")]
	public Transform masterObject;

	public AxisType masterAxisType;

	public Axis masterAxis;

	public float masterStart;

	public float masterEnd;

	[Header("Slave Settings")]
	public Transform slaveObject;

	public Vector3 slavePositionStart;

	public Vector3 slavePositionEnd;

	public Vector3 slaveRotationStart;

	public Vector3 slaveRotationEnd;

	[Header("Slave Mapping")]
	public bool mapPosition;

	public bool mapRotation;

	private void Update()
	{
	}

	private float Map01Clamped(float value, float start, float end)
	{
		return 0f;
	}

	private float GetLocalAxis(Vector3 vec, Axis axis)
	{
		return 0f;
	}

	private float GetLocalRotationSigned(Transform t, Axis axis)
	{
		return 0f;
	}

	private float GetSignedAngle(Quaternion q, Vector3 axis)
	{
		return 0f;
	}
}
