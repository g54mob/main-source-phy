using UnityEngine;

public class ExternalForceObject
{
	public BasicInfo basicInfo;

	public bool dontCompare;

	public bool waitForUpdate = true;

	public Vector3 position;

	public Vector3 furthestPoint;

	public Vector3 closestPoint;

	public ForceMode forceMode;

	public float powerScale;

	public float power;

	public float antiGravity;

	public Vector3 force;

	public Vector3 force2;

	public float CounterDrag = 1f;

	public float highestPoint;

	public float hightMapValue;

	public Vector3 velocity;

	public Vector3 velNormal = Vector3.zero;

	public ushort playerID;

	public float dragScale = 1f;

	public Vector3 dragScaleDirection = Vector3.zero;

	public Quaternion velRot;

	public Quaternion rigidRotation;

	public Vector3 extendRotated;

	public float extentLength;

	public int boundsNumber;

	public ExternalForceObject()
	{
	}

	public ExternalForceObject(Vector3 pos, BasicInfo b, Vector3 normalVelocity, ForceMode fMode, float pScale, bool compare)
	{
		position = pos;
		forceMode = fMode;
		powerScale = pScale;
		basicInfo = b;
		velNormal = normalVelocity;
		dontCompare = compare;
		waitForUpdate = true;
		velocity = ((!basicInfo.noRigidbody) ? basicInfo.Rigidbody.velocity : Vector3.zero);
		CounterDrag = 1f;
		playerID = 0;
		dragScale = 1f;
		basicInfo.dragScale = 1f;
		dragScaleDirection = Vector3.zero;
	}

	public void WindReciever(Vector3 pos, BasicInfo b)
	{
		position = pos;
		basicInfo = b;
		waitForUpdate = true;
	}

	public void Replace(Vector3 pos, BasicInfo b, Vector3 normalVelocity, ForceMode fMode, float pScale, bool compare)
	{
		position = pos;
		forceMode = fMode;
		powerScale = pScale;
		basicInfo = b;
		velNormal = normalVelocity;
		dontCompare = compare;
		waitForUpdate = true;
		velocity = ((!basicInfo.noRigidbody) ? basicInfo.Rigidbody.velocity : Vector3.zero);
		CounterDrag = 1f;
		playerID = 0;
		dragScale = 1f;
		basicInfo.dragScale = 1f;
		dragScaleDirection = Vector3.zero;
	}
}
