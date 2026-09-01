using System.Collections;
using UnityEngine;

public class CheckForEnd2 : MonoBehaviour
{
	public RaycastHit hit;

	public float rayLength = 1f;

	public LayerMask layerMasky;

	public Transform myParent;

	public ConfigurableJoint myJoint;

	public bool hasChecked;

	public ConfigurableJoint[] jointys;

	private ConfigurableJoint jointy;

	private Machine machine;

	private IEnumerator Start()
	{
		machine = GetComponentInParent<Machine>();
		if (!(machine == null) && !machine.isSimulating && !hasChecked)
		{
			yield return new WaitForFixedUpdate();
			RayCheck();
		}
	}

	private void RayCheck()
	{
		if (Physics.Raycast(base.transform.position, base.transform.forward, out hit, rayLength, layerMasky))
		{
			Collider collider = hit.collider;
			if (collider.gameObject.layer != 22 && !(collider.attachedRigidbody == null))
			{
				CheckHitObjConfigJoints(collider);
				MonoBehaviour.print(collider.gameObject.name);
				AddJointy();
			}
		}
	}

	private void CheckHitObjConfigJoints(Collider col)
	{
		ConfigurableJoint component = col.attachedRigidbody.GetComponent<ConfigurableJoint>();
		if (component != null && component.connectedBody == jointy.GetComponent<Rigidbody>())
		{
			MonoBehaviour.print("TOO MANY CHEFS");
		}
	}

	private void AddJointy()
	{
		jointy = myParent.gameObject.AddComponent<ConfigurableJoint>();
		jointy.anchor = myJoint.anchor;
		jointy.axis = myJoint.axis;
		jointy.secondaryAxis = myJoint.secondaryAxis;
		jointy.angularXMotion = myJoint.angularXMotion;
		jointy.angularYMotion = myJoint.angularYMotion;
		jointy.angularZMotion = myJoint.angularZMotion;
		jointy.xMotion = myJoint.xMotion;
		jointy.yMotion = myJoint.yMotion;
		jointy.zMotion = myJoint.zMotion;
		jointy.projectionMode = myJoint.projectionMode;
		jointy.projectionDistance = myJoint.projectionDistance;
		jointy.projectionAngle = myJoint.projectionAngle;
		jointy.breakForce = myJoint.breakForce;
		jointy.breakTorque = myJoint.breakTorque;
		jointy.connectedBody = hit.collider.attachedRigidbody;
	}
}
