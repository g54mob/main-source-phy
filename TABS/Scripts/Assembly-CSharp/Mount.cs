using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI;
using Landfall.TABS.GameState;
using UnityEngine;

public class Mount : MonoBehaviour
{
	private DataHandler data;

	private DataHandler otherData;

	private ConfigurableJoint[] joints = new ConfigurableJoint[3];

	private bool isMounted;

	private MountPos myMountPos;

	private MovementHandler mountMove;

	private RigidbodyHolder rigHolder;

	private UnitAPI m_unitApi;

	private UnitAPI o_unitApi;

	private bool inputChanged;

	private float counter;

	private bool hasDied;

	public DataHandler OtherData => otherData;

	public int SitId { get; private set; }

	public bool IsMounted => isMounted;

	public void Start()
	{
		m_unitApi = GetComponent<UnitAPI>();
	}

	public GameObject[] EnterMount(UnitBlueprint mountBluePrint, Unit mountUnit, int sitID)
	{
		SitId = sitID;
		rigHolder = GetComponentInChildren<RigidbodyHolder>();
		GameObject[] result = new GameObject[0];
		data = GetComponentInChildren<DataHandler>();
		if (data != null && data.unit != null && data.unit.IsRemotelyControlled)
		{
			data.unit.UnfreezeBody();
		}
		SpawnFreezeBody[] componentsInChildren = GetComponentsInChildren<SpawnFreezeBody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.DestroyImmediate(componentsInChildren[i]);
		}
		Rigidbody[] allRigs = data.allRigs.AllRigs;
		for (int j = 0; j < allRigs.Length; j++)
		{
			allRigs[j].isKinematic = false;
		}
		Unit component = GetComponent<Unit>();
		if (!mountUnit)
		{
			result = mountBluePrint.Spawn(base.transform.position, base.transform.rotation, component.Team, out mountUnit);
		}
		MountPos[] componentsInChildren2 = mountUnit.gameObject.GetComponentsInChildren<MountPos>();
		otherData = mountUnit.GetComponentInChildren<DataHandler>();
		base.transform.SetPositionAndRotation(componentsInChildren2[sitID].transform.position - data.hip.position + base.transform.position, componentsInChildren2[sitID].transform.rotation);
		if (componentsInChildren2[sitID].giveForwardObjectToRider)
		{
			data.characterForwardObject = mountUnit.GetComponentInChildren<DirectionObject>().transform;
			data.unitToInheritDirectionFrom = otherData;
			GetComponentInChildren<GeneralInput>().AllowRotationChange = false;
		}
		if (componentsInChildren2[sitID].inheritVelocity)
		{
			mountMove = mountUnit.GetComponentInChildren<MovementHandler>();
			if (mountMove.otherHolders == null)
			{
				mountMove.otherHolders = new List<RigidbodyHolder>();
			}
			mountMove.otherHolders.Add(rigHolder);
		}
		myMountPos = componentsInChildren2[sitID];
		StartCoroutine(LockJoints(componentsInChildren2[sitID]));
		if (myMountPos.removeArmColliders)
		{
			ToggleColliders(on: false);
		}
		for (int k = 0; k < myMountPos.explosionsToProtectAgainst.Length; k++)
		{
			myMountPos.explosionsToProtectAgainst[k].AddProtectedRig(data.mainRig);
		}
		if (myMountPos.overrideInputDirection || myMountPos.overrideInputFull)
		{
			o_unitApi = otherData.GetComponentInParent<UnitAPI>();
			o_unitApi.SetActive(active: false);
			inputChanged = true;
		}
		if (myMountPos.disablePossession)
		{
			PossesionCamera componentInChildren = base.transform.root.GetComponentInChildren<PossesionCamera>();
			if ((bool)componentInChildren)
			{
				Object.Destroy(componentInChildren);
			}
		}
		isMounted = true;
		return result;
	}

	private IEnumerator LockJoints(MountPos mountPos)
	{
		if (mountPos.mountAnimation != null)
		{
			yield return new WaitForSeconds(mountPos.mountAnimation.startMountAnimationDelay);
		}
		PoseHandler component = data.GetComponent<PoseHandler>();
		if ((bool)component)
		{
			if (mountPos.mountAnimation != null && mountPos.mountAnimation.animationName != "")
			{
				component.PlayAnimation(mountPos.mountAnimation, data.hip.transform.position, base.transform.rotation);
			}
			else
			{
				component.PlayRide(data.hip.transform.position, base.transform.rotation);
			}
		}
		yield return new WaitForSeconds(0.1f);
		if (mountPos.mountType == MountPos.MountType.Sit || mountPos.mountType == MountPos.MountType.SitAndHold)
		{
			joints[0] = JointActions.AttachJoint(data.hip.GetComponent<Rigidbody>(), mountPos.GetComponentInParent<Rigidbody>(), mountPos.angles, 25f * mountPos.angularJointStrength, lockRot: false, lockPos: false, 100f * mountPos.jointStrength);
		}
		if (mountPos.mountType == MountPos.MountType.Hold || mountPos.mountType == MountPos.MountType.SitAndHold)
		{
			if ((bool)data.leftHand)
			{
				joints[1] = JointActions.AttachJoint(data.leftHand.GetComponent<Rigidbody>(), mountPos.GetComponentInParent<Rigidbody>(), mountPos.angles, 25f * mountPos.angularJointStrength, lockRot: false, lockPos: true, 100f * mountPos.jointStrength);
			}
			if ((bool)data.rightHand)
			{
				joints[2] = JointActions.AttachJoint(data.rightHand.GetComponent<Rigidbody>(), mountPos.GetComponentInParent<Rigidbody>(), mountPos.angles, 25f * mountPos.angularJointStrength, lockRot: false, lockPos: true, 100f * mountPos.jointStrength);
			}
		}
		if (mountPos.LockedLegs)
		{
			if ((bool)data.footLeft)
			{
				joints[1] = JointActions.AttachJoint(data.footLeft.GetComponent<Rigidbody>(), mountPos.GetComponentInParent<Rigidbody>(), mountPos.angles, 25f * mountPos.angularJointStrength, lockRot: false, lockPos: false, 100f * mountPos.jointStrength);
			}
			if ((bool)data.footRight)
			{
				joints[2] = JointActions.AttachJoint(data.footRight.GetComponent<Rigidbody>(), mountPos.GetComponentInParent<Rigidbody>(), mountPos.angles, 25f * mountPos.angularJointStrength, lockRot: false, lockPos: false, 100f * mountPos.jointStrength);
			}
			for (int i = 0; i < joints.Length; i++)
			{
				if ((bool)joints[i])
				{
					joints[i].enableCollision = mountPos.EnableCollision;
				}
			}
		}
		if (myMountPos.breakForce == 0f)
		{
			yield break;
		}
		for (int j = 0; j < joints.Length; j++)
		{
			if ((bool)joints[j])
			{
				joints[j].breakTorque = myMountPos.breakForce;
				joints[j].breakForce = myMountPos.breakForce;
			}
		}
	}

	private void Update()
	{
		if (!isMounted)
		{
			if (myMountPos.overrideInputDirection || (myMountPos.overrideInputFull && inputChanged))
			{
				o_unitApi.SetActive(active: true);
				inputChanged = false;
			}
			return;
		}
		if (myMountPos.killRiderIfVehicleDies && otherData.Dead && !data.Dead)
		{
			data.healthHandler.Die();
		}
		if (myMountPos.overrideInputDirection && (bool)otherData && ServiceLocator.GetService<GameStateManager>().GameState == GameState.BattleState)
		{
			otherData.input.Direction.rotation = data.input.Direction.rotation;
			otherData.input.inputDirection = Vector3.forward;
		}
		if (myMountPos.overrideInputFull && ServiceLocator.GetService<GameStateManager>().GameState == GameState.BattleState && (bool)otherData)
		{
			otherData.input.Direction.rotation = data.input.Direction.rotation;
			otherData.input.inputDirection = data.input.inputDirection;
		}
		if (data.Dead && !hasDied)
		{
			if (myMountPos.killVehicleIfRiderDies && !otherData.Dead)
			{
				KillMount();
			}
			hasDied = true;
		}
		if (data.Dead || otherData.Dead)
		{
			Fall();
			return;
		}
		if (otherData.isGrounded && isMounted && myMountPos.setGrounded)
		{
			data.TouchGround(new Vector3(data.mainRig.position.x, data.head.transform.position.y - data.baseHeight * 0.8f, data.mainRig.position.z), Vector3.up);
		}
		else
		{
			data.takeFallDamage = false;
		}
		if (myMountPos.sendGroundedToMount && data.isGrounded)
		{
			otherData.TouchGround(data.groundPosition, data.groundNormal);
		}
		if (myMountPos.dropMountWithinRange != 0f && data.distanceToTarget < myMountPos.dropMountWithinRange)
		{
			if (myMountPos.dropDelay != 0f)
			{
				counter += Time.deltaTime;
				if (counter > myMountPos.dropDelay)
				{
					Fall();
					counter = 0f;
				}
			}
			else
			{
				Fall();
			}
		}
		if (Vector3.Angle(Vector3.up, data.mainRig.transform.up) > myMountPos.letGoAngle && !myMountPos.neverLetGo)
		{
			Fall();
		}
	}

	private void ToggleColliders(bool on)
	{
		if ((bool)data.rightHand)
		{
			data.rightHand.GetComponentInChildren<Collider>().enabled = on;
		}
		if ((bool)data.leftHand)
		{
			data.leftHand.GetComponentInChildren<Collider>().enabled = on;
		}
	}

	private void Fall()
	{
		if (myMountPos.setMassOnMountOnDrop != 0f)
		{
			otherData.mainRig.mass = myMountPos.setMassOnMountOnDrop;
		}
		if (myMountPos.upwardsDropForce != 0f)
		{
			otherData.mainRig.AddForce(Vector3.up * myMountPos.upwardsDropForce, ForceMode.VelocityChange);
		}
		if (myMountPos.forwardDropForce != 0f)
		{
			otherData.mainRig.AddForce(otherData.characterForwardObject.forward * myMountPos.forwardDropForce, ForceMode.VelocityChange);
		}
		if (myMountPos.killVehicleIfRiderDies)
		{
			KillMount();
		}
		if ((bool)mountMove && mountMove.otherHolders.Contains(rigHolder) && Random.value < 0.02f)
		{
			mountMove.otherHolders.Remove(rigHolder);
		}
		if (myMountPos.removeArmColliders)
		{
			ToggleColliders(on: true);
		}
		for (int i = 0; i < joints.Length; i++)
		{
			Object.Destroy(joints[i]);
		}
		isMounted = false;
		m_unitApi.SetIsRider(isMounted);
		if (myMountPos.inheritVelocity && Random.value > 0.01f)
		{
			mountMove = myMountPos.GetComponentInChildren<MovementHandler>();
			if ((bool)mountMove && mountMove.otherHolders != null && mountMove.otherHolders.Contains(rigHolder))
			{
				mountMove.otherHolders.Remove(rigHolder);
			}
		}
		if (myMountPos.upwardDismountForce != 0f)
		{
			data.mainRig.AddForce(Vector3.up * myMountPos.upwardDismountForce, ForceMode.VelocityChange);
		}
		if (myMountPos.forwardDismountForce != 0f)
		{
			data.mainRig.AddForce(otherData.characterForwardObject.forward * myMountPos.forwardDismountForce, ForceMode.VelocityChange);
		}
	}

	private void KillMount()
	{
		if (myMountPos.dieDelay != 0f)
		{
			StartCoroutine(DelayDie());
		}
		else
		{
			DoDeath();
		}
	}

	private IEnumerator DelayDie()
	{
		yield return new WaitForSeconds(myMountPos.dieDelay);
		DoDeath();
	}

	private void DoDeath()
	{
		if (myMountPos.riderDieEvent != null)
		{
			myMountPos.riderDieEvent.Invoke();
		}
		otherData.GetComponent<Damagable>().TakeDamage(float.PositiveInfinity, Vector3.zero, null);
	}
}
