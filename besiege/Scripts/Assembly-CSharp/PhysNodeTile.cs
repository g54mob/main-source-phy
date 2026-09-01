using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Destruction/Tiles/PhysNodeTile")]
public class PhysNodeTile : PhysNodeBase, IExplosionEffect
{
	public Action<PhysNodeTile, PhysNode> onNodeBreak;

	public PhysNode[] nodes;

	public JointNode[] jointNodes;

	public ConfigurableJoint jointToCopy;

	public float destroyThreshold;

	public ExplosiveProperty explosiveProperty;

	private float totalMass;

	private bool broken;

	public static float MomentOfLastTileDestroyed;

	protected override void Start()
	{
		for (int i = 0; i < jointNodes.Length; i++)
		{
			JointNode jointNode = jointNodes[i];
			jointNode.nodes = new PhysNode[jointNode.nodeColliders.Length];
			for (int j = 0; j < jointNode.nodeColliders.Length; j++)
			{
				GetNode(jointNode.nodeColliders[j], out jointNode.nodes[j]);
			}
		}
		totalMass = myBody.mass;
		base.Start();
		if (base.isSimulating)
		{
			StartCoroutine(DelayedSetJointStrength());
		}
	}

	protected override void OnDrawGizmosSelected()
	{
		if (jointNodes != null)
		{
			for (int i = 0; i < jointNodes.Length; i++)
			{
				DrawGizmos(jointNodes[i]);
			}
		}
	}

	private bool GetNode(Collider c, out PhysNode node)
	{
		node = null;
		for (int i = 0; i < nodes.Length; i++)
		{
			if (nodes[i].active && nodes[i].collider == c)
			{
				node = nodes[i];
				return true;
			}
		}
		return false;
	}

	private bool ContainsCollider(JointNode jointNode, Collider c)
	{
		int instanceID = c.GetInstanceID();
		if (jointNode.nodes == null)
		{
			return false;
		}
		for (int i = 0; i < jointNode.nodes.Length; i++)
		{
			if (jointNode.nodes[i] != null && jointNode.nodes[i].collider.GetInstanceID() == instanceID)
			{
				return true;
			}
		}
		return false;
	}

	protected override void CreateJoints()
	{
		for (int i = 0; i < jointNodes.Length; i++)
		{
			JointNode jointNode = jointNodes[i];
			Vector3 position = jointNode.orientation.position;
			Vector3 forward = jointNode.orientation.forward;
			ConnectResult connectResult;
			if (Connect(position, forward, out connectResult))
			{
				ConfigurableJoint configurableJoint = (jointNode.joint = myBody.gameObject.AddComponent<ConfigurableJoint>());
				configurableJoint.xMotion = jointToCopy.xMotion;
				configurableJoint.yMotion = jointToCopy.yMotion;
				configurableJoint.zMotion = jointToCopy.zMotion;
				configurableJoint.angularXMotion = jointToCopy.angularXMotion;
				configurableJoint.angularYMotion = jointToCopy.angularYMotion;
				configurableJoint.angularZMotion = jointToCopy.angularZMotion;
				configurableJoint.projectionMode = jointToCopy.projectionMode;
				configurableJoint.projectionAngle = jointToCopy.projectionAngle;
				configurableJoint.projectionDistance = jointToCopy.projectionDistance;
				configurableJoint.enablePreprocessing = jointToCopy.enablePreprocessing;
				configurableJoint.breakForce = float.PositiveInfinity;
				configurableJoint.breakTorque = float.PositiveInfinity;
				jointNode.nodeBreakForce = jointToCopy.breakForce;
				jointNode.nodeBreakTorque = jointToCopy.breakTorque;
				jointNode.nodesInRange = ConfigureNode(jointNode, connectResult);
				if (jointNode.nodesInRange > 1)
				{
					configurableJoint.breakForce *= jointNode.nodesInRange;
					configurableJoint.breakTorque *= jointNode.nodesInRange;
				}
				if (jointNode.nodesInRange == 0)
				{
					Debug.LogWarning("No nodes in range for joint node (index=" + i + " path=" + Machine.GetObjectPath(base.gameObject) + ")!", base.gameObject);
				}
			}
			if (jointNode.nodes.Length == 1 || jointNode.joint == null)
			{
				UnityEngine.Object.Destroy(jointNode.orientation.gameObject);
				jointNode.hasOrientation = false;
			}
		}
		UnityEngine.Object.Destroy(jointToCopy);
	}

	protected override int ConfigureNode(JointNode jointNode, ConnectResult connectResult)
	{
		int result = base.ConfigureNode(jointNode, connectResult);
		if (!connectResult.groundJoint)
		{
			result = NodesInRange(jointNode, connectResult.collider.attachedRigidbody);
		}
		return result;
	}

	private int NodesInRange(JointNode jointNode, Rigidbody body)
	{
		int num = 0;
		ConfigurableJoint joint = jointNode.joint;
		if (jointNode.orientation != null && joint.connectedBody == body)
		{
			Vector3 localDirection = joint.transform.InverseTransformDirection(jointNode.orientation.forward);
			Vector3 anchor = joint.anchor;
			for (int i = 0; i < jointNode.nodes.Length; i++)
			{
				if (jointNode.nodes[i] != null && jointNode.nodes[i].active && InRange(GetNodePosition(jointNode, i, anchor, localDirection), body))
				{
					num++;
				}
			}
		}
		return num;
	}

	protected override void OnTileBreak(PhysNodeTile nodeTile, PhysNode node)
	{
		for (int i = 0; i < jointNodes.Length; i++)
		{
			JointNode jointNode = jointNodes[i];
			if (jointNode.joint == null || jointNode.joint.connectedBody != nodeTile.myBody)
			{
				continue;
			}
			if (jointNode.hasOrientation)
			{
				int num = NodesInRange(jointNodes[i], nodeTile.myBody);
				if (num == 0)
				{
					OnTileBreak(jointNode, nodeTile, node);
					UnityEngine.Object.Destroy(jointNode.orientation.gameObject);
					jointNode.hasOrientation = false;
				}
				else
				{
					jointNode.joint.breakForce = jointNode.nodeBreakForce * (float)num;
					jointNode.joint.breakTorque = jointNode.nodeBreakTorque * (float)num;
				}
			}
			else if (jointNode.collider == node.collider)
			{
				OnTileBreak(jointNode, nodeTile, node);
			}
		}
		DropSupports(node);
	}

	protected void DropSupports(PhysNode node)
	{
		for (int i = 0; i < node.objectImSupporting.Length; i++)
		{
			if (node.objectImSupporting[i].activeInHierarchy)
			{
				node.objectImSupporting[i].SetActive(false);
			}
		}
		if (node.jointsImSupporting.Length <= 0)
		{
			return;
		}
		for (int j = 0; j < node.jointsImSupporting.Length; j++)
		{
			if ((bool)node.jointsImSupporting[j])
			{
				Joint obj = node.jointsImSupporting[j];
				float num = 0f;
				node.jointsImSupporting[j].breakTorque = num;
				obj.breakForce = num;
			}
		}
		node.jointsImSupporting = new Joint[0];
	}

	protected override void RegisterCallbacks()
	{
		for (int i = 0; i < jointNodes.Length; i++)
		{
			RegisterCallbacks(jointNodes[i]);
		}
	}

	public void BreakNode(Collision collision)
	{
		PhysNode node;
		if (base.SimPhysics && base.isSimulating && (GetNode(collision.collider, out node) || (collision.contacts.Length > 0 && (GetNode(collision.contacts[0].thisCollider, out node) || GetNode(collision.contacts[0].otherCollider, out node)))))
		{
			BreakNode(node, collision.relativeVelocity);
		}
	}

	public void BreakNode(Collider collider, Vector3 relativeVelocity)
	{
		PhysNode node;
		if (GetNode(collider, out node))
		{
			BreakNode(node, relativeVelocity);
		}
	}

	protected void OnJointBreak()
	{
		if (!base.SimPhysics || !base.isSimulating)
		{
			return;
		}
		if (broken)
		{
			if (!basicInfo.noRigidbody)
			{
				UnityEngine.Object.Destroy(basicInfo.Rigidbody);
				base.gameObject.SetActive(false);
				basicInfo.noRigidbody = true;
			}
			return;
		}
		int num = 0;
		int num2 = -1;
		bool[] array = new bool[jointNodes.Length];
		for (int i = 0; i < jointNodes.Length; i++)
		{
			if (jointNodes[i].joint != null)
			{
				array[i] = true;
				num2 = i;
				num++;
			}
		}
		if (num > 0)
		{
			if (num == 1)
			{
				BreakJointNode(jointNodes[num2], myBody.velocity);
			}
			else
			{
				StartCoroutine(IEGetBrokenJoint(array));
			}
		}
	}

	private IEnumerator IEGetBrokenJoint(bool[] jointExists)
	{
		yield return null;
		for (int i = 0; i < jointExists.Length; i++)
		{
			JointNode jointNode = jointNodes[i];
			if (jointExists[i] && jointNode.joint == null)
			{
				BreakJointNode(jointNode, myBody.velocity);
			}
		}
	}

	protected void OnCollisionEnter(Collision collision)
	{
		if (base.SimPhysics && base.isSimulating && !ReferenceMaster.IgnoreBreakCollisions.Contains(collision.gameObject))
		{
			Vector3 relativeVelocity = collision.relativeVelocity;
			PhysNode node;
			if ((relativeVelocity.sqrMagnitude >= destroyThreshold || (collision.transform.gameObject.layer == 29 && relativeVelocity.sqrMagnitude >= 1f)) && (GetNode(collision.collider, out node) || (collision.contacts.Length > 0 && (GetNode(collision.contacts[0].thisCollider, out node) || GetNode(collision.contacts[0].otherCollider, out node)))))
			{
				BreakNode(node, relativeVelocity);
			}
		}
	}

	private void BreakJointNode(JointNode jointNode, Vector3 dir)
	{
		for (int i = 0; i < jointNode.nodes.Length; i++)
		{
			PhysNode physNode = jointNode.nodes[i];
			BreakNode(physNode, dir);
		}
	}

	public Transform BreakNode(PhysNode physNode, Vector3 dir)
	{
		if (physNode == null || !physNode.active || physNode.breakInto == null)
		{
			return null;
		}
		physNode.active = false;
		Transform transform = physNode.collider.transform;
		Transform transform2 = (physNode.breakInstance = (UnityEngine.Object.Instantiate(physNode.breakInto, transform.position, base.transform.rotation, base.transform.parent) as GameObject).transform);
		transform2.localScale = base.transform.localScale;
		bool flag = (HasBasicInfo && !basicInfo.noRigidbody) || (!HasBasicInfo && myBody != null);
		if (base.SimPhysics && flag)
		{
			InheritForce component = transform2.GetComponent<InheritForce>();
			component.forceToAdd = dir;
			component.torqueToAdd = myBody.angularVelocity;
			component.AddForce();
		}
		physNode.collider.gameObject.SetActive(false);
		float num = totalMass / (float)nodes.Length;
		myBody.mass -= num;
		myBody.ResetCenterOfMass();
		AddToPercentageBar(physNode);
		if (onNodeBreak != null)
		{
			onNodeBreak(this, physNode);
		}
		int num2 = 0;
		for (int i = 0; i < nodes.Length; i++)
		{
			if (nodes[i].active)
			{
				num2++;
			}
		}
		if (num2 == 0)
		{
			for (int i = 0; i < jointNodes.Length; i++)
			{
				ConfigurableJoint joint = jointNodes[i].joint;
				if ((bool)joint)
				{
					float breakForce = (joint.breakTorque = 0f);
					joint.breakForce = breakForce;
					jointNodes[i].joint = null;
				}
			}
			basicInfo.Rigidbody.AddForce(Vector3.up);
			broken = true;
			OnBreak();
		}
		else
		{
			for (int i = 0; i < jointNodes.Length; i++)
			{
				JointNode jointNode = jointNodes[i];
				if (!ContainsCollider(jointNode, physNode.collider) || jointNode.joint == null)
				{
					continue;
				}
				int num4 = NodesInRange(jointNode, jointNode.joint.connectedBody);
				if (num4 > 0)
				{
					jointNode.joint.breakForce = jointNode.nodeBreakForce * (float)num4;
					jointNode.joint.breakTorque = jointNode.nodeBreakTorque * (float)num4;
					continue;
				}
				ConfigurableJoint joint2 = jointNode.joint;
				float breakForce = (joint2.breakTorque = 0f);
				joint2.breakForce = breakForce;
				jointNode.joint = null;
				if (jointNode.nodes.Length > 1)
				{
					UnityEngine.Object.Destroy(jointNode.orientation.gameObject);
					jointNode.hasOrientation = false;
				}
			}
		}
		DropSupports(physNode);
		return transform2;
	}

	public void ExplodeFromFire()
	{
		for (int i = 0; i < nodes.Length; i++)
		{
			BreakNode(nodes[i], Vector3.zero);
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.enabled || !base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & ReferenceMaster.EnumToInt((int)explosiveProperty)) != 0)
		{
			float num = 0f;
			bool result = false;
			for (int i = 0; i < nodes.Length; i++)
			{
				if (inWater)
				{
					Vector3 center = nodes[i].collider.bounds.center;
					num = (explosionPos - center).sqrMagnitude;
					if (num > 100f)
					{
						continue;
					}
				}
				Transform transform = BreakNode(nodes[i], Vector3.zero);
				if (!object.ReferenceEquals(transform, null))
				{
					InheritExplosion component = transform.GetComponent<InheritExplosion>();
					if ((bool)component)
					{
						component.InheritForce(power, explosionPos, radius, upPower);
					}
				}
				result = true;
			}
			return result;
		}
		return false;
	}

	protected void AddToPercentageBar(PhysNode node)
	{
		if (!StatMaster.isMP && node.collider.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
			MomentOfLastTileDestroyed = Time.time;
		}
	}

	protected IEnumerator DelayedSetJointStrength()
	{
		for (int i = 0; i < 20; i++)
		{
			yield return new WaitForFixedUpdate();
		}
		for (int j = 0; j < jointNodes.Length; j++)
		{
			JointNode jointNode = jointNodes[j];
			if (jointNode != null && !(jointNode.joint == null))
			{
				if (jointNode.nodesInRange > 1)
				{
					jointNode.joint.breakForce = jointNode.nodeBreakForce * (float)jointNode.nodesInRange;
					jointNode.joint.breakTorque = jointNode.nodeBreakTorque * (float)jointNode.nodesInRange;
				}
				else
				{
					jointNode.joint.breakForce = jointNode.nodeBreakForce;
					jointNode.joint.breakTorque = jointNode.nodeBreakTorque;
				}
			}
		}
	}
}
