using System.Collections;
using UnityEngine;

public class LevelObjectJoint : SimBehaviour
{
	public Joint joint;

	public bool joinToAllStatic;

	public Vector3 localJointAxis = Vector3.down;

	public int floorLayer = 29;

	public LayerMask layerMask;

	public float distance;

	public bool backwardsCompatible = true;

	public bool hasDefaultJoint = true;

	public bool keepJointOnNoTarget;

	public bool onlyUpright;

	public Collider ignoredColliderOnJoint;

	public GameObject[] hide = new GameObject[0];

	[HideInInspector]
	public float breakForce = 20000f;

	[HideInInspector]
	public bool hinge;

	private bool connectToFloor;

	public bool joinOtherTargetsTogether;

	protected override void Start()
	{
		base.Start();
		if (backwardsCompatible)
		{
			if (!hasDefaultJoint && joint != null)
			{
				Object.Destroy(joint);
			}
		}
		else
		{
			if (!StatMaster.levelSimulating || !base.SimPhysics)
			{
				return;
			}
			if (onlyUpright && hasDefaultJoint && Vector3.Dot(base.transform.up, Vector3.up) < 0.707f)
			{
				if (joint != null)
				{
					Object.Destroy(joint);
				}
				return;
			}
			Vector3 vector = base.transform.TransformDirection(localJointAxis);
			Vector3 vector2 = ((!hasDefaultJoint) ? base.transform.position : base.transform.TransformPoint(joint.anchor));
			Debug.DrawLine(vector2, vector2 + vector * distance, Color.white, 2f);
			if (joinOtherTargetsTogether && (bool)ignoredColliderOnJoint)
			{
				ignoredColliderOnJoint.enabled = false;
			}
			float num = float.MaxValue;
			float num2 = float.MaxValue;
			Rigidbody rigidbody = null;
			Rigidbody secondClosestBody = null;
			connectToFloor = false;
			bool connectOtherToFloor = false;
			float radius = ((!joinOtherTargetsTogether) ? 0.1f : base.transform.localScale.x);
			Collider[] array = Physics.OverlapCapsule(vector2, vector2 + vector * distance, radius, layerMask, QueryTriggerInteraction.Ignore);
			Collider[] array2 = array;
			foreach (Collider collider in array2)
			{
				Rigidbody componentInParent = collider.GetComponentInParent<Rigidbody>();
				if (componentInParent == null)
				{
					if (collider.gameObject.layer == floorLayer)
					{
						connectToFloor = true;
					}
					else if (joinToAllStatic)
					{
						if (num == float.MaxValue)
						{
							connectToFloor = true;
						}
						else if (num2 == float.MaxValue)
						{
							connectOtherToFloor = true;
						}
					}
				}
				else
				{
					if (collider.transform.IsChildOf(base.transform))
					{
						continue;
					}
					float sqrMagnitude = (componentInParent.ClosestPointOnBounds(vector2) - vector2).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						if (joinOtherTargetsTogether && componentInParent != rigidbody)
						{
							secondClosestBody = rigidbody;
							num2 = num;
							connectOtherToFloor = connectToFloor;
						}
						num = sqrMagnitude;
						rigidbody = componentInParent;
						connectToFloor = false;
					}
					else if (joinOtherTargetsTogether && sqrMagnitude < num2 && componentInParent != rigidbody)
					{
						secondClosestBody = componentInParent;
						num2 = sqrMagnitude;
						connectOtherToFloor = false;
					}
				}
			}
			if (joinOtherTargetsTogether)
			{
				JoinTargetsTogether(rigidbody, secondClosestBody, connectOtherToFloor);
			}
			else if (rigidbody != null)
			{
				joint.connectedBody = rigidbody;
				if ((bool)ignoredColliderOnJoint)
				{
					ignoredColliderOnJoint.enabled = false;
				}
				StartCoroutine(IEStart());
			}
			else if (!connectToFloor && !keepJointOnNoTarget)
			{
				Object.Destroy(joint);
			}
			Hide();
		}
	}

	private void JoinTargetsTogether(Rigidbody closestBody, Rigidbody secondClosestBody, bool connectOtherToFloor)
	{
		bool flag = !connectToFloor && closestBody != null && !closestBody.isKinematic;
		bool flag2 = !connectOtherToFloor && secondClosestBody != null && !secondClosestBody.isKinematic;
		if (!flag && !flag2)
		{
			Hide();
			return;
		}
		bool flag3 = false;
		if (!flag)
		{
			connectToFloor = true;
			closestBody = null;
			flag3 = true;
		}
		else if (!flag2)
		{
			connectOtherToFloor = true;
			secondClosestBody = null;
		}
		if (connectToFloor)
		{
			flag3 = true;
		}
		else if (flag && flag2)
		{
			flag3 = closestBody.mass < secondClosestBody.mass;
		}
		if (flag3)
		{
			Rigidbody rigidbody = closestBody;
			closestBody = secondClosestBody;
			secondClosestBody = rigidbody;
			bool flag4 = connectToFloor;
			connectToFloor = connectOtherToFloor;
			connectOtherToFloor = flag4;
			flag4 = flag;
			flag = flag2;
			flag2 = flag4;
		}
		Joint joint = ((!flag) ? null : closestBody.GetComponent<Joint>());
		if (flag && (bool)joint && joint.connectedBody == secondClosestBody)
		{
			Hide();
			return;
		}
		if (flag2)
		{
			joint = secondClosestBody.GetComponent<Joint>();
			if ((bool)joint && joint.connectedBody == closestBody)
			{
				Hide();
				return;
			}
		}
		if (hinge)
		{
			this.joint = closestBody.gameObject.AddComponent<HingeJoint>();
			this.joint.axis = closestBody.transform.InverseTransformDirection(base.transform.forward);
		}
		else
		{
			this.joint = closestBody.gameObject.AddComponent<FixedJoint>();
		}
		Joint obj = this.joint;
		float breakTorque = breakForce;
		this.joint.breakTorque = breakTorque;
		obj.breakForce = breakTorque;
		this.joint.anchor = closestBody.transform.InverseTransformPoint(base.transform.position);
		this.joint.autoConfigureConnectedAnchor = true;
		this.joint.connectedBody = secondClosestBody;
		connectToFloor = connectToFloor || connectOtherToFloor;
		StartCoroutine(IEStart());
	}

	private IEnumerator IEStart()
	{
		for (int i = 0; i < 6; i++)
		{
			yield return null;
		}
		if (joint != null && joint.connectedBody == null && !connectToFloor)
		{
			Object.Destroy(joint);
		}
	}

	private void Hide()
	{
		for (int i = 0; i < hide.Length; i++)
		{
			hide[i].SetActive(false);
		}
	}
}
