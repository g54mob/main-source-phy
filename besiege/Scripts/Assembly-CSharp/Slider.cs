using UnityEngine;

public class Slider : MonoBehaviour
{
	public Transform startPoint;

	public Transform endPoint;

	public Transform cylinder;

	public float radius = 0.5f;

	public bool isSet;

	public ConfigurableJoint myJoint;

	public float rayLength = 1f;

	public LayerMask layerMasky;

	public Transform rayPos;

	public CapsuleCollider myCapsuleCollider;

	public DestroyJointIfNull DestroyJointIfNullCode;

	public ConfigurableJoint[] myJoints;

	private RaycastHit hit;

	private Machine machine;

	private float lastMagnitude;

	private void Start()
	{
		machine = GetComponentInParent<Machine>();
		myCapsuleCollider = GetComponent<CapsuleCollider>();
	}

	private void LateUpdate()
	{
		if (!machine.isSimulating && !isSet)
		{
			if (InputManager.LeftMouseButtonReleased())
			{
				Set();
			}
			if (InputManager.LeftMouseButtonHeld())
			{
				endPoint.position = AddPiece.mouseHitPos;
				endPoint.forward = AddPiece.mouseHitNormal + startPoint.forward;
				CreateCylinderBetweenPoints(startPoint.position + startPoint.forward, endPoint.position + endPoint.forward, radius);
			}
		}
	}

	private void CreateCylinderBetweenPoints(Vector3 start, Vector3 end, float width)
	{
		Vector3 vector = end - start;
		Vector3 position = start + vector / 2f;
		cylinder.position = position;
		cylinder.transform.up = vector;
		float sqrMagnitude = vector.sqrMagnitude;
		float num = sqrMagnitude - lastMagnitude;
		if (num < 0f)
		{
			num = 0f - num;
		}
		if (num > OptionsMaster.sqrScaleUpdateThreshold)
		{
			float num2 = Mathf.Sqrt(vector.sqrMagnitude);
			cylinder.transform.localScale = new Vector3(width, num2 / 2f, width);
			lastMagnitude = sqrMagnitude;
		}
	}

	private void Set()
	{
		isSet = true;
		if (Physics.Raycast(rayPos.position, -endPoint.forward, out hit, rayLength, layerMasky))
		{
			startPoint.forward = endPoint.forward;
			CreateCylinderBetweenPoints(startPoint.position + startPoint.forward, endPoint.position + endPoint.forward, radius);
			AddJointy(hit.collider.transform.parent.GetComponent<Rigidbody>(), base.transform);
			cylinder.GetComponent<ConfigurableJoint>().connectedBody = GetComponent<Rigidbody>();
			cylinder.GetComponent<Rigidbody>().isKinematic = false;
		}
		DestroyJointIfNullCode.enabled = true;
	}

	private void SetStartPos(Vector3 pos)
	{
		startPoint.position = pos;
	}

	private void SetEndPos(Vector3 pos)
	{
		endPoint.position = pos;
	}

	private void SetStartRot(Vector3 rot)
	{
		startPoint.forward = rot;
	}

	private void SetEndRot(Vector3 rot)
	{
		endPoint.forward = rot;
	}

	private void AddJointy(Rigidbody target, Transform targetObj)
	{
		ConfigurableJoint configurableJoint = targetObj.gameObject.AddComponent<ConfigurableJoint>();
		configurableJoint.anchor = myJoint.anchor;
		configurableJoint.axis = myJoint.axis;
		configurableJoint.secondaryAxis = myJoint.secondaryAxis;
		configurableJoint.angularXMotion = myJoint.angularXMotion;
		configurableJoint.angularYMotion = myJoint.angularYMotion;
		configurableJoint.angularZMotion = myJoint.angularZMotion;
		configurableJoint.xMotion = myJoint.xMotion;
		configurableJoint.yMotion = myJoint.yMotion;
		configurableJoint.zMotion = myJoint.zMotion;
		configurableJoint.projectionMode = myJoint.projectionMode;
		configurableJoint.projectionDistance = myJoint.projectionDistance;
		configurableJoint.projectionAngle = myJoint.projectionAngle;
		configurableJoint.breakForce = myJoint.breakForce;
		configurableJoint.breakTorque = myJoint.breakTorque;
		configurableJoint.swapBodies = myJoint.swapBodies;
		configurableJoint.connectedBody = target;
	}
}
