using UnityEngine;

public class PhysFollowDropObj : MonoBehaviour
{
	public float followPower = 100f;

	public bool ignoreYpos;

	public bool hasDropped;

	public float dragAmount = 1f;

	public SpringJoint jointToBreak;

	public Rigidbody cargoRigidbody;

	public float upForce = 1000f;

	public float cargoDragAmount = 100f;

	public LineRenderer lineRenderer;

	public DrawLineRender lineRenderCode;

	private Transform myTransform;

	private Rigidbody myRigidbody;

	private Vector3 targetPos;

	private void Start()
	{
		myTransform = base.transform;
		myRigidbody = GetComponent<Rigidbody>();
		upForce = Mathf.Abs(Physics.gravity.y) * cargoRigidbody.mass;
		if (StatMaster.levelSimulating)
		{
			cargoRigidbody.isKinematic = false;
			myRigidbody.isKinematic = false;
		}
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && Input.GetKeyDown("o"))
		{
			Drop();
		}
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating && !hasDropped)
		{
			targetPos = Machine.Active().MiddlePosition;
			if (ignoreYpos)
			{
				targetPos.y = myTransform.position.y;
			}
			myRigidbody.AddForce((targetPos - myTransform.position) * followPower);
			myRigidbody.AddForce(-myRigidbody.velocity * dragAmount);
			cargoRigidbody.AddForce(-cargoRigidbody.velocity * cargoDragAmount);
			myRigidbody.AddForce(Vector3.up * upForce);
		}
	}

	private void Drop()
	{
		hasDropped = true;
		Object.Destroy(jointToBreak);
		Object.Destroy(lineRenderer);
		Object.Destroy(lineRenderCode);
	}
}
