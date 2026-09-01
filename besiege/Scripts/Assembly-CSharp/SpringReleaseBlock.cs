using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

[AddComponentMenu("Blocks/Block Behaviours/Spring Release Block")]
public class SpringReleaseBlock : BlockBehaviour
{
	[FormerlySerializedAs("audio")]
	public AudioSource sfx;

	public AudioClip[] impactSfx = new AudioClip[0];

	public float sfxMinImpact = 20f;

	public float sfxMaxImpact = 200f;

	protected ConfigurableJoint myJoint;

	protected MKey activateKey;

	protected MSlider forceSlider;

	protected MSlider angleSlider;

	protected MToggle autoReset;

	protected bool winding;

	protected bool ready;

	protected float windPct;

	protected float timeSinceBite;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	private bool activatePressed;

	private bool emuActivatePressed;

	private bool hasJoint;

	public MSlider ForceSlider
	{
		get
		{
			return forceSlider;
		}
	}

	public MSlider AngleSlider
	{
		get
		{
			return angleSlider;
		}
	}

	public MToggle AutoReset
	{
		get
		{
			return autoReset;
		}
	}

	public MKey ActivateKey
	{
		get
		{
			return activateKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		myJoint = blockJoint as ConfigurableJoint;
		activateKey = AddKey(3911, "RELEASE", ControlScheme.BlockControls.Jaw, 0, KeyCode.X);
		autoReset = AddToggle(4425, "auto-Reset", true);
		forceSlider = AddSlider(2490, "force", 1f, 0.25f, 2f, string.Empty);
		angleSlider = AddSlider(2501, "jaw-angle", 65f, 1f, 90f, string.Empty);
		forceSlider.logScaling = true;
		if (!stripped && SimPhysics)
		{
			forceSlider.ValueChanged += SetForce;
			SetForce(forceSlider.Value);
		}
		if (isSimulating)
		{
			mixer = sfx.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
		}
	}

	protected void SetForce(float f)
	{
		if (float.IsNaN(f))
		{
			f = 0f;
		}
		JointDrive angularXDrive = myJoint.angularXDrive;
		angularXDrive.positionSpring = f * 10000f;
		angularXDrive.maximumForce = Mathf.Pow(f, 2f) * 10000f;
		myJoint.angularXDrive = angularXDrive;
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void StartPhysics(bool isKinematic)
	{
		if (!noRigidbody)
		{
			Rigidbody.centerOfMass = Vector3.Scale(base.transform.localScale, new Vector3(0f, 0.2444f, 1.0318f));
		}
		hasJoint = true;
		if (!SimPhysics || myJoint == null)
		{
			hasJoint = false;
			Unregister();
		}
		else if (myJoint.connectedBody == null)
		{
			UnityEngine.Object.Destroy(myJoint);
			hasJoint = false;
			Unregister();
		}
		else if (autoReset.IsActive)
		{
			StartWind();
		}
	}

	protected void OnJointBreak()
	{
		if (SimPhysics)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			hasJoint = false;
			myJoint = null;
			Unregister();
		}
	}

	private void Unregister()
	{
		_parentMachine.UnregisterUpdate(this, false);
		_parentMachine.UnregisterFixedUpdate(this, false);
		_parentMachine.UnregisterEmulationUpdate(this);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		activatePressed = activateKey.IsPressed;
		CheckKeys(activatePressed);
	}

	public override void EmulationUpdateBlock()
	{
		emuActivatePressed = activateKey.EmulationPressed();
		CheckKeys(emuActivatePressed);
	}

	private void CheckKeys(bool pressed)
	{
		if (pressed)
		{
			if (autoReset.IsActive)
			{
				Release();
				StartWind();
			}
			else if (ready)
			{
				Release();
			}
			else if (!winding)
			{
				StartWind();
			}
		}
	}

	private void StartWind()
	{
		ready = true;
		winding = true;
		windPct = -0.5f + Mathf.Min(timeSinceBite, 0.5f);
	}

	private void Release()
	{
		timeSinceBite = 0f;
		ready = false;
		winding = false;
		if (hasJoint)
		{
			if (!noRigidbody && Rigidbody.IsSleeping())
			{
				Rigidbody.WakeUp();
			}
			myJoint.targetRotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}

	public override void FixedUpdateBlock()
	{
		if (!hasJoint)
		{
			return;
		}
		timeSinceBite += Time.unscaledDeltaTime;
		if (!winding)
		{
			return;
		}
		if (windPct > 0f)
		{
			if (!noRigidbody && Rigidbody.IsSleeping())
			{
				Rigidbody.WakeUp();
			}
			myJoint.targetRotation = Quaternion.Euler(Mathf.Lerp(0f, angleSlider.Value, windPct), 0f, 0f);
		}
		windPct += Time.fixedDeltaTime;
		if (windPct > 1f)
		{
			windPct = 1f;
			winding = false;
			myJoint.targetRotation = Quaternion.Euler(angleSlider.Value, 0f, 0f);
		}
	}

	protected void OnCollisionEnter(Collision contact)
	{
		if (!isSimulating || !SimPhysics)
		{
			return;
		}
		float sqrMagnitude = contact.relativeVelocity.sqrMagnitude;
		if (sqrMagnitude > sfxMinImpact)
		{
			float num = Mathf.Clamp01(0.02f + Mathf.InverseLerp(sfxMinImpact, sfxMaxImpact, sqrMagnitude) * 0.3f);
			PlaySound(num);
			if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly)
			{
				if (NetBlock != null)
				{
					byte eventData = (byte)(num * 255f);
					NetBlock.Event(NetworkEntity.EntityEvent.SoundOnCollide, eventData);
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
		}
		if (sqrMagnitude < 1500f || !contact.collider.gameObject.activeInHierarchy)
		{
			return;
		}
		Rigidbody attachedRigidbody;
		switch (contact.collider.gameObject.layer)
		{
		case 24:
		case 29:
		case 31:
			return;
		case 12:
		case 14:
		case 15:
		case 17:
		case 18:
		case 25:
		case 26:
		{
			attachedRigidbody = contact.collider.attachedRigidbody;
			BlockHealthBar component = attachedRigidbody.GetComponent<BlockHealthBar>();
			if ((bool)component)
			{
				component.DamageBlock(3f);
			}
			return;
		}
		}
		attachedRigidbody = contact.collider.attachedRigidbody;
		if (attachedRigidbody == null)
		{
			return;
		}
		BasicInfo component2 = attachedRigidbody.GetComponent<BasicInfo>();
		if (component2 == null)
		{
			return;
		}
		if (!StatMaster.GodTools.UnbreakableMode)
		{
			BlockBehaviour blockBehaviour = component2 as BlockBehaviour;
			if (!object.ReferenceEquals(blockBehaviour, null))
			{
				if (blockBehaviour.gotChildBlocks)
				{
					BlockBehaviour childBlockFromCollider = blockBehaviour.GetChildBlockFromCollider(contact.collider);
					if (!object.ReferenceEquals(childBlockFromCollider, null))
					{
						blockBehaviour = childBlockFromCollider;
					}
				}
				if (blockBehaviour.Prefab.hasHealthBar)
				{
					blockBehaviour.BlockHealth.DamageBlock(sqrMagnitude * 0.00125f);
				}
				else if (ReduceBreakForceOnImpact.Used && blockBehaviour.Prefab.reduceBreakforce)
				{
					blockBehaviour.BreakOnImpact.ReduceJointBreakForce(3f);
				}
				if (blockBehaviour.isParented && blockBehaviour.jointBreakForce <= 0f)
				{
					blockBehaviour.UnParentChildBlock(blockBehaviour);
				}
				return;
			}
		}
		float damage = 50f * Time.deltaTime;
		if (component2.hasAiScript)
		{
			KillingHandler killingHandler = component2.aiEntity.my.killingHandler;
			killingHandler.TakeDamage(damage, InjuryType.Sharp);
			return;
		}
		EnemyAISimple enemyAISimple = component2 as EnemyAISimple;
		if (!object.ReferenceEquals(enemyAISimple, null))
		{
			enemyAISimple.TakeDamage(damage, InjuryType.Sharp);
			return;
		}
		BreakBase component3 = attachedRigidbody.GetComponent<BreakBase>();
		if (component3 == null)
		{
			return;
		}
		StructuralPhysTile structuralPhysTile = component3 as StructuralPhysTile;
		if (!object.ReferenceEquals(structuralPhysTile, null))
		{
			structuralPhysTile.DestroyTile(Vector3.zero);
			return;
		}
		PhysNodeTile physNodeTile = component3 as PhysNodeTile;
		if (!object.ReferenceEquals(physNodeTile, null))
		{
			physNodeTile.BreakNode(contact);
		}
	}

	public void PlaySound(float volume)
	{
		if (impactSfx.Length != 0 && !sfx.isPlaying)
		{
			if (base.GetSubmergedPctMV > 0.9f)
			{
				sfx.outputAudioMixerGroup = underwaterMixer;
			}
			else
			{
				sfx.outputAudioMixerGroup = mixer;
			}
			AudioClip clip = impactSfx[UnityEngine.Random.Range(0, impactSfx.Length)];
			sfx.volume = volume;
			sfx.PlayOneShot(clip);
		}
	}
}
