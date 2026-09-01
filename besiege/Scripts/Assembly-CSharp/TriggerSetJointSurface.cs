using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (Surface)")]
public class TriggerSetJointSurface : TriggerSetJointBase
{
	private class PossibleTarget
	{
		public Collider collider;

		public Transform parent;

		public bool mechJoint;

		public bool hasRigidbody;
	}

	public BuildSurface block;

	public SphereCollider col;

	public bool loadedPos;

	private Joint myJoint;

	private HashSet<Transform> otherMechJointsParents;

	private List<PossibleTarget> possibleTargets = new List<PossibleTarget>(5);

	private List<Collider> consideredColliders = new List<Collider>(5);

	private void Start()
	{
		if (!block.SimPhysics)
		{
			if (!block.HasParentMachine || block.isSimulating)
			{
				Object.Destroy(base.gameObject);
			}
			return;
		}
		if (!block.isValid)
		{
			Object.Destroy(base.gameObject);
			Object.Destroy(myJoint);
			return;
		}
		if (!loadedPos)
		{
			Debug.Log(string.Concat(block, " ", block.transform.GetSiblingIndex(), " has no updated corner joint position for ", base.transform.GetSiblingIndex()));
		}
		if (!block.isSimulating)
		{
			return;
		}
		myJoint = block.GetJointForTrigger(this);
		otherMechJointsParents = new HashSet<Transform>();
		Collider[] array = Physics.OverlapSphere(base.transform.position, col.radius * Mathf.Max(base.transform.lossyScale.x, base.transform.lossyScale.y, base.transform.lossyScale.z), AddPiece.CreateLayerMask(new int[3] { 12, 14, 22 }));
		if (array != null)
		{
			Collider[] array2 = array;
			foreach (Collider other in array2)
			{
				CheckCollider(other);
			}
		}
		CheckAllJoints();
	}

	private void CheckCollider(Collider other)
	{
		bool flag = other.attachedRigidbody != null;
		if (!flag)
		{
			return;
		}
		GameObject gameObject = other.gameObject;
		Transform transform = other.attachedRigidbody.transform;
		switch (gameObject.layer)
		{
		case 22:
			transform = other.transform.parent;
			if (gameObject.CompareTag("MechanicalTag"))
			{
				otherMechJointsParents.Add(transform);
			}
			break;
		case 12:
		case 14:
			if (!(gameObject.tag == "ClusterIgnore") && !(transform == block.transform) && (!StatMaster.isMP || !(other.attachedRigidbody.transform.parent.name == "Building Machine")) && myJoint != null && myJoint.connectedBody == null)
			{
				bool mechJoint = transform.gameObject.CompareTag("MechanicalTag");
				PossibleTarget possibleTarget = new PossibleTarget();
				possibleTarget.collider = other;
				possibleTarget.parent = transform;
				possibleTarget.mechJoint = mechJoint;
				possibleTarget.hasRigidbody = flag;
				PossibleTarget item = possibleTarget;
				possibleTargets.Add(item);
				consideredColliders.Add(other);
			}
			break;
		}
	}

	private void CheckAllJoints()
	{
		possibleTargets = (from p in possibleTargets
			orderby p.collider.transform.position.x descending, p.parent.GetSiblingIndex() descending
			select p).ToList();
		for (int num = 0; num < possibleTargets.Count; num++)
		{
			PossibleTarget possibleTarget = possibleTargets[num];
			if (possibleTarget.collider == null || !possibleTarget.hasRigidbody)
			{
				continue;
			}
			BlockBehaviour componentInParent = possibleTarget.collider.GetComponentInParent<BlockBehaviour>();
			bool flag = componentInParent != null;
			if (possibleTarget.mechJoint)
			{
				if (flag && !componentInParent.Prefab.mechanicalJoint)
				{
					possibleTarget.mechJoint = false;
				}
				else if (!otherMechJointsParents.Contains(possibleTarget.collider.attachedRigidbody.transform))
				{
					possibleTarget.mechJoint = false;
				}
			}
			if (possibleTarget.mechJoint || ConfigCheckForDoubleJoints(possibleTarget.collider))
			{
				if (flag)
				{
					componentInParent.CreateSimLists();
					componentInParent.jointsToMe.Add(myJoint);
				}
				block.CheckJoint(this, consideredColliders);
				TimedRocket timedRocket = componentInParent as TimedRocket;
				if (!object.ReferenceEquals(timedRocket, null))
				{
					timedRocket.jointsToMeFVC.Add(block.VisualController as FragmentVisualController);
				}
				Object.Destroy(base.gameObject);
				return;
			}
		}
		block.CheckJoint(this, consideredColliders);
		Object.Destroy(base.gameObject);
	}

	private bool ConfigCheckForDoubleJoints(Collider obj)
	{
		Rigidbody attachedRigidbody = obj.attachedRigidbody;
		Joint[] components = attachedRigidbody.GetComponents<Joint>();
		for (int i = 0; i < components.Length; i++)
		{
			if (components[i].connectedBody == block.Rigidbody)
			{
				return false;
			}
		}
		if (myJoint == null)
		{
			return false;
		}
		block.CreateSimLists();
		myJoint.connectedBody = attachedRigidbody;
		block.OnSetJoint(myJoint, obj, consideredColliders);
		if (myJoint.connectedBody != null)
		{
			block.iJointTo.Add(myJoint);
		}
		else
		{
			Debug.LogWarning("Nullref when adding to iJointTo (ConfigCheck)! Other: " + ((block.iJointTo == null) ? "Null" : "iJointTo array") + " > connected body: " + ((!(myJoint.connectedBody != null)) ? "Null" : myJoint.connectedBody.name));
		}
		return true;
	}
}
