using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint")]
public class TriggerSetJoint : TriggerSetJointBase
{
	public BlockBehaviour block;

	private bool otherMechJoint;

	private Collider colliderToJoinTo;

	private Transform parentToJoinTo;

	private bool canParentBlock;

	private Joint myJoint;

	private bool mechJointTag;

	private BlockType blockType;

	private float prevTimeSliderValue;

	private bool isInitialized;

	private List<Transform> otherMechJointsParents;

	private List<Collider> colliders;

	public bool dontParent;

	private void Start()
	{
		canJoinMultiple = false;
		if (!block.SimPhysics)
		{
			if (!block.HasParentMachine || block.isSimulating)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		isInitialized = true;
		blockType = block.Prefab.Type;
		switch (blockType)
		{
		case BlockType.WoodenPanel:
		case BlockType.ArmorPlateSmall:
		case BlockType.ArmorPlateRound:
		case BlockType.ArmorPlateLarge:
			canParentBlock = true;
			break;
		}
		if (block.isSimulating)
		{
			mechJointTag = base.gameObject.CompareTag("MechanicalTag");
			if (!mechJointTag)
			{
				mechJointTag = block.Prefab.mechanicalJoint;
			}
			myJoint = block.blockJoint;
			otherMechJointsParents = new List<Transform>();
			if (colliders != null)
			{
				for (int i = 0; i < colliders.Count; i++)
				{
					Collider collider = colliders[i];
					if (collider != null)
					{
						OnTriggerEnter(collider);
					}
				}
			}
			StartCoroutine(CheckAllJoints());
		}
		else
		{
			otherMechJoint = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isInitialized)
		{
			if (colliders == null)
			{
				colliders = new List<Collider>();
			}
			colliders.Add(other);
			return;
		}
		if (!block.SimPhysics || !block.isSimulating)
		{
			otherMechJoint = false;
			return;
		}
		GameObject gameObject = other.gameObject;
		int layer = gameObject.layer;
		Transform parent = other.transform.parent;
		bool flag = gameObject.CompareTag("MechanicalTag");
		if (!flag && gameObject.CompareTag("StayKinematic"))
		{
			BlockBehaviour componentInParent = gameObject.GetComponentInParent<BlockBehaviour>();
			if (componentInParent != null && componentInParent.Prefab.mechanicalJoint)
			{
				flag = true;
			}
		}
		if (!mechJointTag)
		{
			if (flag)
			{
				otherMechJoint = true;
				otherMechJointsParents.Add(parent);
			}
		}
		else if (flag)
		{
			otherMechJoint = false;
		}
		if ((layer == 12 || layer == 14) && !(parent == block.transform) && (!StatMaster.isMP || !other.attachedRigidbody || !other.attachedRigidbody.transform.parent.name.Equals("Building Machine", StringComparison.OrdinalIgnoreCase)) && myJoint != null && myJoint.connectedBody == null && (isDynamicLink || !colliderToJoinTo || !other.CompareTag("OnlyMechanicalJoints")))
		{
			colliderToJoinTo = other;
			parentToJoinTo = parent;
			while (parentToJoinTo != null)
			{
				parentToJoinTo = parentToJoinTo.parent;
			}
		}
	}

	private IEnumerator CheckAllJoints()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		bool hasCollToJoinTo = colliderToJoinTo != null;
		if (!hasCollToJoinTo && parentToJoinTo != null)
		{
			colliderToJoinTo = parentToJoinTo.GetComponentInChildren<Collider>();
			hasCollToJoinTo = colliderToJoinTo != null;
		}
		BlockBehaviour otherBlock = ((!hasCollToJoinTo) ? null : colliderToJoinTo.GetComponentInParent<BlockBehaviour>());
		bool hasBlock = otherBlock != null;
		if (otherMechJoint && hasCollToJoinTo && (bool)colliderToJoinTo.attachedRigidbody)
		{
			if (!colliderToJoinTo.attachedRigidbody.gameObject.CompareTag("MechanicalTag") && hasBlock && !otherBlock.Prefab.mechanicalJoint)
			{
				otherMechJoint = false;
			}
			else if (!otherMechJointsParents.Contains(colliderToJoinTo.attachedRigidbody.transform))
			{
				otherMechJoint = false;
			}
		}
		if (!otherMechJoint && hasCollToJoinTo)
		{
			if (myJoint is HingeJoint)
			{
				HingeCheckForDoubleJoints(colliderToJoinTo);
			}
			else
			{
				StartCoroutine(ConfigCheckForDoubleJoints(colliderToJoinTo));
			}
		}
		if (hasBlock)
		{
			otherBlock.CreateSimLists();
			otherBlock.jointsToMe.Add(myJoint);
		}
		block.CheckJoints();
		if (StatMaster.UseJointParenting && canParentBlock)
		{
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			if (prevTimeSliderValue != 0f)
			{
				TimeSlider.Instance.delegateTimeScale = prevTimeSliderValue;
			}
			yield return new WaitForFixedUpdate();
		}
		TimedRocket timedRocket = otherBlock as TimedRocket;
		if (!object.ReferenceEquals(timedRocket, null))
		{
			timedRocket.jointsToMeFVC.Add(block.VisualController as FragmentVisualController);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private IEnumerator ConfigCheckForDoubleJoints(Collider obj)
	{
		if (obj == null || obj.transform == null)
		{
			yield break;
		}
		Rigidbody objectBody = obj.attachedRigidbody;
		if (objectBody == null)
		{
			yield break;
		}
		bool connect = false;
		Joint objectJoint = objectBody.GetComponent<Joint>();
		if (objectJoint != null)
		{
			if (objectJoint.connectedBody != block.Rigidbody)
			{
				connect = true;
			}
		}
		else
		{
			connect = true;
		}
		if (!connect)
		{
			yield break;
		}
		bool hasBlock = block != null;
		if (hasBlock)
		{
			block.CreateSimLists();
		}
		if (StatMaster.UseJointParenting && canParentBlock && hasBlock && block.jointsToMe.Count == 0 && block.iJointTo.Count == 0)
		{
			StartCoroutine(ParentJoint(obj, objectBody.transform));
		}
		else
		{
			if (!(myJoint != null))
			{
				yield break;
			}
			if (myJoint.transform == obj.attachedRigidbody.transform)
			{
				Debug.LogError("[TriggerSetJoint] Connection Error: connectedBody == this body on " + myJoint.transform.name);
			}
			myJoint.connectedBody = obj.attachedRigidbody;
			if (hasBlock)
			{
				if (myJoint.connectedBody != null)
				{
					block.iJointTo.Add(myJoint);
				}
				else
				{
					Debug.LogWarning("Nullref when adding to iJointTo (ConfigCheck)! Other: " + ((block.iJointTo == null) ? "Null" : "iJointTo array") + " > connected body: " + ((!(myJoint.connectedBody != null)) ? "Null" : myJoint.connectedBody.name));
				}
			}
		}
	}

	private IEnumerator ParentJoint(Collider obj, Transform objectParent)
	{
		BlockBehaviour otherBlock = objectParent.GetComponent<BlockBehaviour>();
		switch (otherBlock.Prefab.Type)
		{
		case BlockType.Wheel:
		case BlockType.Hinge:
		case BlockType.SteeringBlock:
		case BlockType.Piston:
		case BlockType.Swivel:
		case BlockType.SpinningBlock:
		case BlockType.ArmorPlateSmall:
		case BlockType.SteeringHinge:
		case BlockType.ArmorPlateRound:
		case BlockType.ArmorPlateLarge:
		case BlockType.CogMediumPowered:
		case BlockType.LargeWheel:
		case BlockType.Rocket:
		case BlockType.BuildSurface:
		case BlockType.SqrBalloon:
		case BlockType.SkateWheel:
		case BlockType.FlyWheel:
			canParentBlock = false;
			break;
		}
		if (otherBlock.Prefab.Type == BlockType.SkateWheel && !isDynamicLink)
		{
			yield break;
		}
		if (blockType == BlockType.WoodenPanel && otherBlock.fireTag == null)
		{
			canParentBlock = false;
		}
		if (dontParent)
		{
			canParentBlock = false;
		}
		if (canParentBlock)
		{
			Rigidbody objectRB = obj.attachedRigidbody;
			myJoint.connectedBody = objectRB;
			block.jointBreakForce = myJoint.breakForce;
			if (!otherBlock.gotChildBlocks)
			{
				otherBlock.gotChildBlocks = true;
				otherBlock.originalMass = objectRB.mass;
				otherBlock.originalCOM = objectRB.centerOfMass;
			}
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			prevTimeSliderValue = TimeSlider.Instance.delegateTimeScale;
			TimeSlider.Instance.delegateTimeScale = 0f;
			Collider[] myColliders = block.Rigidbody.transform.GetComponentsInChildren<Collider>();
			Collider[] OtherColliders = objectRB.transform.GetComponentsInChildren<Collider>();
			for (int i = 0; i < myColliders.Length; i++)
			{
				for (int j = 0; j < OtherColliders.Length; j++)
				{
					Physics.IgnoreCollision(myColliders[i], OtherColliders[j]);
				}
			}
			otherBlock.CreateSimLists();
			otherBlock.parentedColliders.Add(block, myColliders);
			UnityEngine.Object.DestroyImmediate(myJoint);
			if (block.gameObject.CompareTag("ArmourTag"))
			{
				SoundOnCollide parentSound = objectRB.GetComponent<SoundOnCollide>();
				if ((bool)parentSound)
				{
					SoundOnCollide mySound = block.GetComponent<SoundOnCollide>();
					mySound.SetSourceCollider(parentSound);
				}
			}
			block.originalCOM = block.Rigidbody.centerOfMass;
			block.originalMass = block.Rigidbody.mass;
			block.originalDrag = block.Rigidbody.drag;
			block.originalADrag = block.Rigidbody.angularDrag;
			block.fireTag = block.gameObject.GetComponent<FireTag>();
			block.parentBlock = otherBlock;
			GameObject gyro = new GameObject();
			Transform gyroTransform = gyro.transform;
			gyroTransform.parent = block.ParentMachine.BuildingMachine;
			Transform blockTransform = block.transform;
			gyroTransform.localPosition = blockTransform.localPosition;
			gyroTransform.localRotation = blockTransform.localRotation;
			gyroTransform.localScale = blockTransform.localScale;
			gyroTransform.SetParent(objectRB.transform, true);
			gyroTransform.localRotation = Quaternion.identity;
			gyroTransform.localScale = new Vector3(gyroTransform.localScale.x / gyroTransform.lossyScale.x, gyroTransform.localScale.y / gyroTransform.lossyScale.y, gyroTransform.localScale.z / gyroTransform.lossyScale.z);
			block.transform.SetParent(gyroTransform, true);
			float massDiff = block.Rigidbody.mass / (objectRB.mass + block.Rigidbody.mass);
			objectRB.centerOfMass += (objectRB.transform.InverseTransformPoint(block.Rigidbody.worldCenterOfMass) - objectRB.centerOfMass) * massDiff;
			objectRB.mass += block.Rigidbody.mass;
			PinLock pLock = block.Rigidbody.GetComponent<PinLock>();
			if (pLock != null)
			{
				pLock.pinBlock.CreatePinLock(objectRB);
				UnityEngine.Object.Destroy(pLock);
			}
			block.isParented = true;
			block.CreateSimLists();
			if (block.grabbers.Count > 0)
			{
				for (int k = 0; k < block.grabbers.Count; k++)
				{
					block.grabbers[k].currentJoint.connectedBody = objectRB;
				}
			}
			UnityEngine.Object.DestroyImmediate(block.Rigidbody);
			block.Rigidbody = objectRB;
			if (otherBlock.parentCollision == null)
			{
				otherBlock.parentCollision = (block.parentCollision = otherBlock.gameObject.AddComponent<CollisionEnterHook>());
				otherBlock.parentCollision.thisBlock = otherBlock;
			}
			else
			{
				block.parentCollision = otherBlock.parentCollision;
			}
			ReduceBreakForceOnImpact reduceForce = block.GetComponent<ReduceBreakForceOnImpact>();
			if (!object.ReferenceEquals(reduceForce, null))
			{
				block.jointBreakForce = reduceForce.firstBreakForce;
			}
			yield return new WaitForFixedUpdate();
			for (int l = 0; l < myColliders.Length; l++)
			{
				if (myColliders[l] == null)
				{
					continue;
				}
				for (int m = 0; m < OtherColliders.Length; m++)
				{
					if (!(OtherColliders[m] == null))
					{
						Physics.IgnoreCollision(myColliders[l], OtherColliders[m], false);
					}
				}
			}
		}
		else
		{
			myJoint.connectedBody = obj.attachedRigidbody;
			if ((bool)block)
			{
				block.CreateSimLists();
				block.iJointTo.Add(myJoint);
			}
		}
	}

	private void HingeCheckForDoubleJoints(Collider obj)
	{
		Rigidbody attachedRigidbody = obj.attachedRigidbody;
		Joint component = attachedRigidbody.GetComponent<Joint>();
		if (component != null && (component is ConfigurableJoint || component is HingeJoint) && component.connectedBody == block.Rigidbody)
		{
			return;
		}
		myJoint.autoConfigureConnectedAnchor = false;
		myJoint.connectedAnchor = base.transform.position;
		myJoint.connectedBody = obj.attachedRigidbody;
		myJoint.autoConfigureConnectedAnchor = true;
		myJoint.connectedAnchor = Vector3.zero;
		if (block != null)
		{
			block.CreateSimLists();
			if (myJoint.connectedBody != null)
			{
				block.iJointTo.Add(myJoint);
			}
			else
			{
				Debug.LogWarning("Nullref when adding to iJointTo (HingeCheck)! Other: " + ((block.iJointTo == null) ? "Null" : "iJointTo array") + " > connected body: " + ((!(myJoint.connectedBody != null)) ? "Null" : myJoint.connectedBody.name));
			}
		}
	}
}
