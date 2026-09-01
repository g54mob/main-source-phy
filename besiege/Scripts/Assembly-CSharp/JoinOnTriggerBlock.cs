using System;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/JoinOnTriggerBlock")]
public class JoinOnTriggerBlock : SimBehaviour
{
	protected const float GRAB_ON_INPUT_TIME = 0.05f;

	public bool isJoined;

	public GameObject parentObj;

	public Rigidbody parentRB;

	public AudioClip grabSound;

	public AudioClip dropSound;

	public int blockIdIgnore;

	public ConfigurableJoint currentJoint;

	public bool staticJoint;

	public GameObject lastGrabbedGameObject;

	public SphereCollider myCollider;

	public AudioSource audioSource;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	public LayerMask mask = -1;

	public float allowGrabTimer;

	public float breakJointDelay = 0.4f;

	private float regrabLastDuration = 2f;

	private float regrabLastTimer;

	private float breakJointTimer;

	private bool grabStatic;

	private bool grabStaticOnly;

	private bool flexible;

	[NonSerialized]
	public bool isStarting = true;

	private float startingTimer;

	protected override void Start()
	{
		base.Start();
		if (HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block && base.isSimulating)
		{
			if ((int)mask == -1)
			{
				mask = SingleInstanceFindOnly<AddPiece>.Instance.layerMasky;
			}
			SetupMixers();
			if (base.SimPhysics)
			{
				isStarting = true;
				return;
			}
			base.enabled = false;
			UnityEngine.Object.Destroy(myCollider);
		}
	}

	public void SetupMixers()
	{
		mixer = audioSource.outputAudioMixerGroup;
		underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
	}

	private float FixedFrame(int i)
	{
		return Time.fixedDeltaTime * (float)i - 0.001f;
	}

	public void FixedUpdateBlock()
	{
		if (isStarting)
		{
			if (startingTimer > FixedFrame(4) && allowGrabTimer <= 0f)
			{
				allowGrabTimer = 0.05f;
			}
			if (startingTimer > FixedFrame(8))
			{
				regrabLastTimer = 0f;
				isStarting = false;
			}
			startingTimer += Time.fixedDeltaTime;
		}
		regrabLastTimer -= Time.fixedDeltaTime;
		allowGrabTimer -= Time.fixedDeltaTime;
		breakJointTimer -= Time.fixedDeltaTime;
		if (isJoined)
		{
			if (lastGrabbedGameObject == null || !lastGrabbedGameObject.activeInHierarchy)
			{
				BreakJoint();
			}
			else if (currentJoint != null && currentJoint.connectedBody != null && !currentJoint.connectedBody.gameObject.activeInHierarchy)
			{
				BreakJoint();
			}
		}
		if (!isJoined && !(breakJointTimer > 0f) && !(allowGrabTimer <= 0f))
		{
			Vector3 localScale = parentObj.transform.localScale;
			Collider[] array = Physics.OverlapSphere(base.transform.position, myCollider.radius * Mathf.Max(localScale.x, localScale.y, localScale.z), mask, QueryTriggerInteraction.Ignore);
			for (int i = 0; i < array.Length && !TryGrab(array[i]); i++)
			{
			}
		}
	}

	public bool TryGrab(Collider other)
	{
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (attachedRigidbody == parentRB)
		{
			return false;
		}
		if (attachedRigidbody != null)
		{
			if (attachedRigidbody.isKinematic)
			{
				if (!grabStatic)
				{
					return false;
				}
			}
			else if (grabStaticOnly)
			{
				return false;
			}
			if (attachedRigidbody.gameObject != lastGrabbedGameObject || regrabLastTimer < 0f)
			{
				if (!StatMaster.isMP && AchievementTrophyPickup.IsAvailable)
				{
					AchievementTrophyPickup component = attachedRigidbody.GetComponent<AchievementTrophyPickup>();
					if ((bool)component)
					{
						component.OnTriggerEnter(other);
						return false;
					}
				}
				lastGrabbedGameObject = attachedRigidbody.gameObject;
				staticJoint = attachedRigidbody.isKinematic;
				AddJointy(attachedRigidbody);
				return true;
			}
		}
		else if (grabStatic)
		{
			lastGrabbedGameObject = other.gameObject;
			staticJoint = true;
			AddJointy(null);
			return true;
		}
		return false;
	}

	public void SetCanGrabStatic(bool canGrab, bool staticOnly)
	{
		grabStatic = canGrab;
		grabStaticOnly = staticOnly;
	}

	public void OnKeyPressed()
	{
		if (isJoined)
		{
			OnKeyRelease();
		}
		else
		{
			OnKeyGrab();
		}
	}

	public void OnKeyRelease()
	{
		if (isJoined && base.SimPhysics)
		{
			BreakJoint();
		}
	}

	public void OnKeyGrab()
	{
		if (!isJoined)
		{
			if (isStarting)
			{
				regrabLastTimer = regrabLastDuration;
			}
			allowGrabTimer = 0.05f;
		}
	}

	public void StopGrab()
	{
		allowGrabTimer = 0f;
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
	}

	public void BreakJoint()
	{
		regrabLastTimer = regrabLastDuration;
		if (!(currentJoint != null))
		{
			return;
		}
		ConfigurableJoint configurableJoint = currentJoint;
		Rigidbody connectedBody = currentJoint.connectedBody;
		if ((bool)connectedBody)
		{
			BasicInfo component = connectedBody.GetComponent<BasicInfo>();
			bool flag = component != null;
			if (flag)
			{
				component.SetGrabbed(false, this);
			}
			if ((!flag || !component.isDestroyed) && connectedBody.gameObject.activeInHierarchy)
			{
				configurableJoint.projectionMode = JointProjectionMode.None;
				float breakForce = (configurableJoint.breakTorque = 0f);
				configurableJoint.breakForce = breakForce;
				parentRB.AddForce(-base.transform.forward * Mathf.Epsilon);
				return;
			}
		}
		isJoined = false;
		staticJoint = false;
		breakJointTimer = breakJointDelay;
		UnityEngine.Object.Destroy(configurableJoint);
		PlayGrabSound(false);
	}

	public void BlockJointBreak()
	{
		if (currentJoint == null || currentJoint.breakForce == 0f)
		{
			if (isJoined)
			{
				isJoined = false;
				breakJointTimer = breakJointDelay;
				PlayGrabSound(false);
			}
			staticJoint = false;
		}
	}

	public void SetMixer(bool underwater)
	{
		if (underwater)
		{
			audioSource.outputAudioMixerGroup = underwaterMixer;
		}
		else
		{
			audioSource.outputAudioMixerGroup = mixer;
		}
	}

	public void PlayGrabSound(bool grab)
	{
		audioSource.PlayOneShot((!grab) ? dropSound : grabSound);
		if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly && base.SimPhysics)
		{
			base.NetBlock.Event(NetworkEntity.EntityEvent.PlayGrabSound, (byte)(grab ? 1u : 0u));
		}
	}

	private void AddJointy(Rigidbody target)
	{
		if (currentJoint != null || !base.SimPhysics)
		{
			return;
		}
		PlayGrabSound(true);
		isJoined = true;
		ConfigurableJoint configurableJoint = parentObj.gameObject.AddComponent<ConfigurableJoint>();
		if (flexible)
		{
			configurableJoint.anchor = base.transform.localPosition + Vector3.forward * 0.25f;
			configurableJoint.axis = new Vector3(1f, 0f, 0f);
			SoftJointLimitSpring linearLimitSpring = configurableJoint.linearLimitSpring;
			SoftJointLimit linearLimit = configurableJoint.linearLimit;
			linearLimitSpring.spring = 100000f;
			linearLimitSpring.damper = 1000f;
			linearLimit.limit = 0.1f;
			configurableJoint.linearLimitSpring = linearLimitSpring;
			configurableJoint.linearLimit = linearLimit;
			SoftJointLimitSpring angularYZLimitSpring = configurableJoint.angularYZLimitSpring;
			SoftJointLimit angularYLimit = configurableJoint.angularYLimit;
			SoftJointLimit angularZLimit = configurableJoint.angularZLimit;
			angularYZLimitSpring.spring = 10000f;
			angularYZLimitSpring.damper = 100f;
			angularYLimit.limit = 3f;
			angularZLimit.limit = 3f;
			configurableJoint.angularYZLimitSpring = angularYZLimitSpring;
			configurableJoint.angularYLimit = angularYLimit;
			configurableJoint.angularZLimit = angularZLimit;
			SoftJointLimitSpring angularXLimitSpring = configurableJoint.angularXLimitSpring;
			SoftJointLimit lowAngularXLimit = configurableJoint.lowAngularXLimit;
			SoftJointLimit highAngularXLimit = configurableJoint.highAngularXLimit;
			angularXLimitSpring.spring = 10000f;
			angularXLimitSpring.damper = 100f;
			lowAngularXLimit.limit = -1f;
			highAngularXLimit.limit = 1f;
			configurableJoint.angularXLimitSpring = angularXLimitSpring;
			configurableJoint.lowAngularXLimit = lowAngularXLimit;
			configurableJoint.highAngularXLimit = highAngularXLimit;
			configurableJoint.angularXMotion = ConfigurableJointMotion.Limited;
			configurableJoint.angularYMotion = ConfigurableJointMotion.Limited;
			configurableJoint.angularZMotion = ConfigurableJointMotion.Locked;
			configurableJoint.xMotion = ConfigurableJointMotion.Locked;
			configurableJoint.yMotion = ConfigurableJointMotion.Locked;
			configurableJoint.zMotion = ConfigurableJointMotion.Limited;
		}
		else
		{
			configurableJoint.angularXMotion = ConfigurableJointMotion.Locked;
			configurableJoint.angularYMotion = ConfigurableJointMotion.Locked;
			configurableJoint.angularZMotion = ConfigurableJointMotion.Locked;
			configurableJoint.xMotion = ConfigurableJointMotion.Locked;
			configurableJoint.yMotion = ConfigurableJointMotion.Locked;
			configurableJoint.zMotion = ConfigurableJointMotion.Locked;
		}
		configurableJoint.connectedBody = target;
		currentJoint = configurableJoint;
		if (StatMaster.UseJointParenting && target != null)
		{
			BasicInfo component = target.GetComponent<BasicInfo>();
			if (!(component == null))
			{
				component.SetGrabbed(true, this);
			}
		}
	}

	public void SetFlexible(bool f)
	{
		flexible = f;
	}
}
