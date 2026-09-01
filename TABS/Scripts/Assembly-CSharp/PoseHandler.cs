using System.Collections;
using UnityEngine;

public class PoseHandler : MonoBehaviour
{
	public StaticAnimation rideAnim;

	public GameObject poseTarget;

	private GameObject target;

	public RigPoseFollower[] rigPoseFollowers = new RigPoseFollower[11];

	private Transform myHead;

	private Transform myTorso;

	private Transform myHip;

	private Transform myRightArm;

	private Transform myRightHand;

	private Transform myLeftArm;

	private Transform myLeftHand;

	private Transform myRightLeg;

	private Transform myRightKnee;

	private Transform myLeftLeg;

	private Transform myLeftKnee;

	private DataHandler data;

	private IEnumerator Animate(StaticAnimation animation)
	{
		yield return new WaitForSeconds(animation.startDelay);
		target.SetActive(value: true);
		target.GetComponent<Animator>().Play(animation.animationName);
		data.GetComponent<SetGlobalJointSettings>().SetJointConstraints(free: true);
		data.playingStaticAnim = true;
		float t = 0f;
		while (t < animation.time)
		{
			for (int i = 0; i < rigPoseFollowers.Length; i++)
			{
				if (rigPoseFollowers[i].isUsed)
				{
					rigPoseFollowers[i].rig.velocity *= 0f;
					rigPoseFollowers[i].rig.angularVelocity *= 0f;
					if (animation.followRotaiton)
					{
						rigPoseFollowers[i].rig.transform.rotation = rigPoseFollowers[i].target.rotation;
					}
					if (animation.followPosition)
					{
						rigPoseFollowers[i].rig.transform.position = rigPoseFollowers[i].target.position;
					}
				}
			}
			t += Time.deltaTime;
			yield return null;
		}
		data.playingStaticAnim = false;
		target.SetActive(value: false);
		data.GetComponent<SetGlobalJointSettings>().SetJointConstraints();
	}

	private void OnDestroy()
	{
		if ((bool)target)
		{
			Object.Destroy(target);
		}
	}

	public void PlayRide(Vector3 pos, Quaternion rot)
	{
		PlayAnimation(rideAnim, pos, rot);
	}

	public void PlayAnimation(StaticAnimation animation, Vector3 pos, Quaternion rot)
	{
		if (!target)
		{
			Init(pos, rot);
		}
		StopAllCoroutines();
		target.transform.SetPositionAndRotation(pos, rot);
		CheckRigs(animation);
		StartCoroutine(Animate(animation));
	}

	private void CheckRigs(StaticAnimation animation)
	{
		for (int i = 0; i < rigPoseFollowers.Length; i++)
		{
			rigPoseFollowers[i].isUsed = false;
		}
		if (animation.animateHead)
		{
			rigPoseFollowers[0].isUsed = true;
		}
		if (animation.animateTorso)
		{
			rigPoseFollowers[1].isUsed = true;
		}
		if (animation.animateHip)
		{
			rigPoseFollowers[2].isUsed = true;
		}
		if (animation.animateLeftArm)
		{
			rigPoseFollowers[3].isUsed = true;
		}
		if (animation.animateLefftHand)
		{
			rigPoseFollowers[4].isUsed = true;
		}
		if (animation.animateRightArm)
		{
			rigPoseFollowers[5].isUsed = true;
		}
		if (animation.animateRightHand)
		{
			rigPoseFollowers[6].isUsed = true;
		}
		if (animation.animateLeftLeg)
		{
			rigPoseFollowers[7].isUsed = true;
		}
		if (animation.animateLeftKnee)
		{
			rigPoseFollowers[8].isUsed = true;
		}
		if (animation.animateRightLeg)
		{
			rigPoseFollowers[9].isUsed = true;
		}
		if (animation.animateRightKnee)
		{
			rigPoseFollowers[10].isUsed = true;
		}
	}

	private void Init(Vector3 pos, Quaternion rot)
	{
		data = GetComponent<DataHandler>();
		target = Object.Instantiate(poseTarget, pos, rot);
		myHead = GetComponentInChildren<Head>().transform;
		myTorso = GetComponentInChildren<Torso>().transform;
		myHip = GetComponentInChildren<Hip>().transform;
		myLeftArm = GetComponentInChildren<ArmLeft>().transform;
		myLeftHand = GetComponentInChildren<HandLeft>().transform;
		myRightArm = GetComponentInChildren<ArmRight>().transform;
		myRightHand = GetComponentInChildren<HandRight>().transform;
		myLeftLeg = GetComponentInChildren<LegLeft>().transform;
		myLeftKnee = GetComponentInChildren<KneeLeft>().transform;
		myRightLeg = GetComponentInChildren<LegRight>().transform;
		myRightKnee = GetComponentInChildren<KneeRight>().transform;
		rigPoseFollowers[0].rig = myHead.GetComponent<Rigidbody>();
		rigPoseFollowers[0].target = target.GetComponentInChildren<Head>().transform;
		rigPoseFollowers[1].rig = myTorso.GetComponent<Rigidbody>();
		rigPoseFollowers[1].target = target.GetComponentInChildren<Torso>().transform;
		rigPoseFollowers[2].rig = myHip.GetComponent<Rigidbody>();
		rigPoseFollowers[2].target = target.GetComponentInChildren<Hip>().transform;
		rigPoseFollowers[3].rig = myLeftArm.GetComponent<Rigidbody>();
		rigPoseFollowers[3].target = target.GetComponentInChildren<ArmLeft>().transform;
		rigPoseFollowers[4].rig = myLeftHand.GetComponent<Rigidbody>();
		rigPoseFollowers[4].target = target.GetComponentInChildren<HandLeft>().transform;
		rigPoseFollowers[5].rig = myRightArm.GetComponent<Rigidbody>();
		rigPoseFollowers[5].target = target.GetComponentInChildren<ArmRight>().transform;
		rigPoseFollowers[6].rig = myRightHand.GetComponent<Rigidbody>();
		rigPoseFollowers[6].target = target.GetComponentInChildren<HandRight>().transform;
		rigPoseFollowers[7].rig = myLeftLeg.GetComponent<Rigidbody>();
		rigPoseFollowers[7].target = target.GetComponentInChildren<LegLeft>().transform;
		rigPoseFollowers[8].rig = myLeftKnee.GetComponent<Rigidbody>();
		rigPoseFollowers[8].target = target.GetComponentInChildren<KneeLeft>().transform;
		rigPoseFollowers[9].rig = myRightLeg.GetComponent<Rigidbody>();
		rigPoseFollowers[9].target = target.GetComponentInChildren<LegRight>().transform;
		rigPoseFollowers[10].rig = myRightKnee.GetComponent<Rigidbody>();
		rigPoseFollowers[10].target = target.GetComponentInChildren<KneeRight>().transform;
	}
}
