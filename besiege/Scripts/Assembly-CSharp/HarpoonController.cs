using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

[AddComponentMenu("Blocks/Block Behaviours/HarpoonController")]
public class HarpoonController : BlockBehaviour
{
	private MKey shootKey;

	private MSlider powerSlider;

	private MSlider speedSlider;

	private MKey retractKey;

	private MToggle holdToContract;

	[Header("Generic References")]
	public Transform winchRotateVis;

	public Vector3 winchRotationAngle;

	public float winchEmptyRectactionSpeedMultiplier = 3f;

	public Collider harpoonBlockCollider;

	[Header("Motor/Winding")]
	public bool likeRope = true;

	public float springPower = 1000f;

	public float restingSpringPower = 0.02f;

	public bool dampen;

	public float dampenAmount = 100f;

	public SpringJoint springJointToAdd;

	public float velocityDamper = 1.2f;

	public float velocityClamp = 10000f;

	public float winchRate = 4f;

	private float startMgntd;

	private float maxMagnitude;

	[HideInInspector]
	public bool shouldContract;

	private Vector3 animateWinchVec = Vector3.zero;

	[HideInInspector]
	public bool autoWind;

	public float reloadDistance = 10f;

	private float prevCurrentLength = float.MaxValue;

	[Header("Cylinder")]
	public GameObject cylinderGO;

	public MeshRenderer ropeVis;

	public float tiling = 1f;

	public float ropeThreshold;

	public Transform cylinder;

	public bool denestRope = true;

	public Transform startRopePoint;

	public Transform endRopePoint;

	public Transform startPoint;

	public Transform endPoint;

	public float radius;

	public float ropeLength = 110f;

	private float maxRopeLength = 40f;

	private float lastCylSqr = -1f;

	private Vector3 cylScale;

	private Vector2 cylTexScale;

	private Vector3 cylPos;

	private Vector3 cylRot;

	private float sqrRopeThreshold;

	private Vector3 contractStartVec;

	private Vector3 contractEndVec;

	private float currentLength;

	private bool snapped;

	[Header("Harpoon")]
	public float harpoonPower;

	public HarpoonTrigger harpoonScript;

	public Transform harpoonBasePos;

	public bool harpoonLoaded = true;

	[Header("VFX/SFX")]
	public ParticleSystem[] snappedDustParticles;

	[FormerlySerializedAs("audioSource")]
	public AudioSource sfx;

	public AudioSource snapAudio;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	[Header("Optional Settings")]
	public bool forceOnImpact;

	public float forceImpact = 100f;

	public bool negateNaturalForce;

	public float jointStrength = 50000f;

	public float breakPullPower = 0.5f;

	private bool lerpForces;

	public float timeToFullForce = 0.5f;

	private float forceLerpTime;

	public float retreactingReactionForce = 100f;

	private bool useProjManager;

	public HarpoonTrigger originalHarpoon;

	private bool doDampen;

	private float doDampenAmount;

	private Vector3 doDampenDiff;

	public MKey ShootKey
	{
		get
		{
			return shootKey;
		}
	}

	public MSlider PowerSlider
	{
		get
		{
			return powerSlider;
		}
	}

	public MSlider SpeedSlider
	{
		get
		{
			return speedSlider;
		}
	}

	public MKey RetractKey
	{
		get
		{
			return retractKey;
		}
	}

	public MToggle HoldToContract
	{
		get
		{
			return holdToContract;
		}
	}

	protected override void Awake()
	{
		cylScale = new Vector3(radius, radius, radius);
		cylTexScale = Vector2.one;
		cylPos = Vector3.zero;
		cylRot = Vector3.zero;
		base.Awake();
		useProjManager = StatMaster.isHosting && base.HasParentMachine && !base.ParentMachine.LocalSim;
		originalHarpoon = harpoonScript;
		if (isSimulating)
		{
			if (WaterController.Exist && sfx != null)
			{
				mixer = sfx.outputAudioMixerGroup;
				underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
			}
			shouldContract = false;
			if (!SimPhysics)
			{
				return;
			}
			if (denestRope)
			{
				cylinder.SetParent(base.transform.parent, true);
			}
		}
		sqrRopeThreshold = ropeThreshold * ropeThreshold;
		BlockEventVisualController obj = VisualController as BlockEventVisualController;
		obj.onMeshChanged = (Action<BlockSkinLoader.SkinPack.Skin>)Delegate.Combine(obj.onMeshChanged, new Action<BlockSkinLoader.SkinPack.Skin>(SetMesh));
		shootKey = AddKey(2429, "shoot", ControlScheme.BlockControls.Harpoon, 0, KeyCode.C);
		retractKey = AddKey(3871, "detach", ControlScheme.BlockControls.Harpoon, 1, KeyCode.V);
		powerSlider = AddSlider(3912, "shoot-power", 1f, 0f, 2f, string.Empty);
		speedSlider = AddSlider(2428, "reel-speed", 1f, 0f, 1.5f, string.Empty);
		holdToContract = AddToggle(4438, "hold to reel in", true);
	}

	protected void SetMesh(BlockSkinLoader.SkinPack.Skin skin)
	{
		winchRotateVis.gameObject.SetActive(skin == null || skin.isDefault);
	}

	protected override void Start()
	{
		base.Start();
		if (isSimulating && SimPhysics)
		{
			maxRopeLength = ropeLength;
			startMgntd = maxRopeLength;
			maxMagnitude = (startMgntd += 0.1f);
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public void ShootSFX()
	{
		if (WaterController.Exist)
		{
			if (base.GetSubmergedPctMV > 0.75f)
			{
				sfx.outputAudioMixerGroup = underwaterMixer;
			}
			else
			{
				sfx.outputAudioMixerGroup = mixer;
			}
		}
		sfx.pitch = UnityEngine.Random.Range(1f, 1.2f);
		sfx.Play();
	}

	protected void ShootHarpoon()
	{
		if (!harpoonLoaded)
		{
			return;
		}
		float value = powerSlider.Value;
		if (float.IsNaN(value))
		{
			return;
		}
		base.ParentMachine.hasFiredProjectiles = true;
		ShootSFX();
		lerpForces = false;
		forceLerpTime = 0f;
		maxMagnitude = maxRopeLength;
		harpoonLoaded = false;
		if (SimPhysics)
		{
			if (useProjManager)
			{
				byte[] array = new byte[17];
				int num = 0;
				Transform transform = harpoonScript.transform;
				NetworkCompression.CompressPosition(transform.position, array, num);
				num += 6;
				NetworkCompression.CompressRotation(transform.rotation, array, num);
				num += 7;
				NetworkCompression.WriteUInt((uint)BuildIndex, false, array, num);
				NetworkAddPiece instance = NetworkAddPiece.Instance;
				Transform transform2 = ProjectileManager.Instance.Spawn(NetworkProjectileType.Harpoon, instance.frame, _parentMachine.PlayerID, array);
				harpoonScript = transform2.GetComponent<HarpoonTrigger>();
			}
			else
			{
				harpoonScript.rb.transform.parent = ReferenceMaster.physicsGoalInstance;
			}
			harpoonScript.rb.useGravity = true;
			harpoonScript.rb.isKinematic = false;
			harpoonScript.rb.interpolation = RigidbodyInterpolation.Interpolate;
			harpoonScript.harpoonTrigger.enabled = true;
			harpoonScript.harpoonCollider.enabled = true;
			harpoonScript.rb.velocity = Rigidbody.velocity;
			harpoonScript.rb.AddTorque(harpoonScript.transform.forward * value * 2500f);
			harpoonScript.rb.AddForceAtPosition(value * harpoonScript.rb.transform.forward * 2500f, harpoonScript.transform.position + harpoonScript.transform.forward);
			Rigidbody.AddForce(value * -harpoonScript.rb.transform.forward * 2.5f, ForceMode.Impulse);
			harpoonScript.stopSelfPropulsion = false;
			if (value > 4f)
			{
				Snap();
			}
			Physics.IgnoreCollision(harpoonScript.harpoonTrigger, harpoonBlockCollider, true);
			Physics.IgnoreCollision(harpoonScript.harpoonCollider, harpoonBlockCollider, true);
			HarpoonTrigger harpoonTrigger = harpoonScript;
			harpoonTrigger.OnAttach = (Action)Delegate.Combine(harpoonTrigger.OnAttach, new Action(OnAttached));
		}
	}

	protected void OnAttached()
	{
		if (sfx.isPlaying)
		{
			sfx.pitch = 1.2f;
		}
		maxMagnitude = (startMgntd = (startRopePoint.position - endRopePoint.position).magnitude + 0.1f);
		if (forceOnImpact && harpoonScript.attachedTo != null)
		{
			harpoonScript.attachedTo.AddForceAtPosition((endRopePoint.position - startRopePoint.position).normalized * forceImpact * -0.1f, endRopePoint.position);
		}
		lerpForces = true;
	}

	protected void CreateCylinderBetweenPoints(Vector3 start, Vector3 end)
	{
		float num = end.x - start.x;
		float num2 = end.y - start.y;
		float num3 = end.z - start.z;
		float num4 = num * num + num2 * num2 + num3 * num3;
		bool flag = cylinderGO.activeSelf;
		if (!isSimulating || num4 > sqrRopeThreshold)
		{
			if (!flag)
			{
				cylinderGO.SetActive(true);
				flag = true;
			}
			float num5 = 0.02f;
			float num6 = num4 - lastCylSqr;
			if (((!(num6 < 0f)) ? num6 : (0f - num6)) > ((!isSimulating) ? num5 : (-1f)))
			{
				float num7 = ((!(num4 < num5)) ? Mathf.Sqrt(num4) : 0.2f);
				cylScale.y = num7 / base.transform.lossyScale.y * 0.5f;
				cylinder.localScale = cylScale;
				if (ropeVis != null)
				{
					cylTexScale.x = tiling * num7;
					ropeVis.material.mainTextureScale = cylTexScale;
				}
				lastCylSqr = num4;
			}
		}
		else if (flag)
		{
			cylinderGO.SetActive(false);
			flag = false;
		}
		if (flag)
		{
			cylPos.Set(start.x + num * 0.5f, start.y + num2 * 0.5f, start.z + num3 * 0.5f);
			cylRot.Set(num, num2, num3);
			cylinder.position = cylPos;
			cylinder.rotation = Quaternion.FromToRotation(Vector3.up, cylRot);
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (snapped)
		{
			return;
		}
		if (CheckSim())
		{
			EvaluateKeys(shootKey.IsPressed, shootKey.IsHeld, retractKey.IsPressed);
		}
		if (harpoonLoaded)
		{
			return;
		}
		currentLength = (startRopePoint.position - endRopePoint.position).magnitude;
		if (SimPhysics && autoWind && !harpoonScript.attached)
		{
			if (currentLength < reloadDistance && maxMagnitude < 0.75f)
			{
				ReloadHarpoon();
			}
			else if (base.transform.InverseTransformPoint(harpoonScript.rb.position).y > -0.7f)
			{
				ReloadHarpoon();
			}
		}
		if (!harpoonScript.attached && !harpoonScript.detaching)
		{
			AnimateWinch((!autoWind) ? (0f - winchEmptyRectactionSpeedMultiplier) : (winchEmptyRectactionSpeedMultiplier * 2f));
		}
	}

	public void ReloadHarpoon()
	{
		if (!harpoonLoaded)
		{
			harpoonLoaded = true;
			doDampen = false;
			if (useProjManager)
			{
				ProjectileManager.Instance.Despawn(harpoonScript.GetComponent<NetworkProjectile>());
			}
			else
			{
				Collider harpoonCollider = harpoonScript.harpoonCollider;
				bool flag = false;
				harpoonScript.harpoonTrigger.enabled = flag;
				harpoonCollider.enabled = flag;
				harpoonScript.rb.interpolation = RigidbodyInterpolation.None;
				harpoonScript.rb.isKinematic = true;
				harpoonScript.rb.useGravity = true;
				harpoonScript.rb.transform.SetParent(base.transform, true);
				harpoonScript.rb.transform.position = harpoonBasePos.position;
				harpoonScript.rb.transform.rotation = harpoonBasePos.rotation;
				Rigidbody rb = harpoonScript.rb;
				Vector3 zero = Vector3.zero;
				harpoonScript.rb.angularVelocity = zero;
				rb.velocity = zero;
				harpoonScript.ResetVis();
				HarpoonTrigger harpoonTrigger = harpoonScript;
				harpoonTrigger.OnAttach = (Action)Delegate.Remove(harpoonTrigger.OnAttach, new Action(OnAttached));
			}
			prevCurrentLength = float.MaxValue;
		}
		autoWind = false;
	}

	public override void EmulationUpdateBlock()
	{
		if (CheckSim())
		{
			EvaluateKeys(shootKey.EmulationPressed(), shootKey.EmulationHeld(true), retractKey.EmulationPressed());
		}
	}

	private void ReelIn()
	{
		if (SimPhysics && !harpoonScript.attached)
		{
			harpoonScript.rb.velocity = Rigidbody.velocity;
			harpoonScript.rb.useGravity = false;
			Collider harpoonCollider = harpoonScript.harpoonCollider;
			bool flag = false;
			harpoonScript.harpoonTrigger.enabled = flag;
			harpoonCollider.enabled = flag;
		}
		if (harpoonScript.detaching)
		{
			autoWind = true;
		}
		else
		{
			autoWind = !autoWind;
		}
	}

	public void Detach()
	{
		harpoonScript.Detach();
		lerpForces = true;
		ReelIn();
	}

	protected bool CheckSim()
	{
		if (!isSimulating || !SimPhysics)
		{
			return false;
		}
		return true;
	}

	protected void EvaluateKeys(bool shootPressed, bool shootHeld, bool retractPressed)
	{
		if (!snapped)
		{
			shouldContract = autoWind;
		}
		if (harpoonLoaded)
		{
			if (shootPressed)
			{
				ShootHarpoon();
			}
			return;
		}
		if (holdToContract.IsActive && harpoonScript.attached)
		{
			autoWind = shootHeld;
		}
		else if (shootPressed)
		{
			if (harpoonScript.attached)
			{
				ReelIn();
			}
			else
			{
				Detach();
			}
		}
		if (retractPressed)
		{
			Detach();
		}
	}

	public override void LateUpdateBlock()
	{
		if (harpoonLoaded)
		{
			CreateCylinderBetweenPoints(startRopePoint.position, startRopePoint.position);
		}
		else
		{
			CreateCylinderBetweenPoints(startRopePoint.position, endRopePoint.position);
		}
	}

	public override void FixedUpdateBlock()
	{
		if (harpoonLoaded || snapped)
		{
			return;
		}
		if (doDampen)
		{
			if (!snapped)
			{
				ContractRope(doDampenAmount, doDampenDiff);
			}
			doDampen = false;
		}
		Vector3 delta = startRopePoint.position - endRopePoint.position;
		currentLength = delta.magnitude;
		if (shouldContract)
		{
			WinchContract((!harpoonScript.attached) ? (winchRate * 20f) : winchRate);
		}
		if (lerpForces)
		{
			float num = Mathf.Clamp(powerSlider.Value, 0.5f, 2f);
			if (forceLerpTime < timeToFullForce)
			{
				forceLerpTime += Time.fixedDeltaTime / num;
				if (forceLerpTime > timeToFullForce)
				{
					forceLerpTime = timeToFullForce;
					lerpForces = false;
				}
			}
		}
		AutonomousLimiting(delta);
	}

	protected void AutonomousLimiting(Vector3 delta)
	{
		if (SimPhysics && (!(currentLength <= maxMagnitude) || shouldContract))
		{
			lerpForces = forceLerpTime < timeToFullForce;
			if (likeRope)
			{
				float num = (currentLength - maxMagnitude) * 20f;
				ContractRope(num * restingSpringPower, delta);
				doDampenAmount = num * dampenAmount;
				doDampenDiff.Set(delta.x, delta.y, delta.z);
				doDampen = true;
			}
			else
			{
				float num2 = (currentLength - maxMagnitude) / 10f;
				num2 = ((num2 < 0f) ? num2 : ((!(num2 > 1f)) ? num2 : 1f));
				ContractRope(num2 * restingSpringPower, delta);
			}
			if (!harpoonScript.stopSelfPropulsion)
			{
				harpoonScript.stopSelfPropulsion = true;
				harpoonScript.rb.velocity = Rigidbody.velocity;
			}
		}
	}

	private void ContractRope(float scaler, Vector3 delta)
	{
		scaler = ((scaler < 0f - velocityClamp) ? (0f - velocityClamp) : ((!(scaler > velocityClamp)) ? scaler : velocityClamp));
		float num = forceLerpTime / timeToFullForce;
		float num2 = ((!likeRope) ? 1f : (1f / currentLength));
		float num3 = num2 * springPower * scaler * num;
		contractStartVec.Set(delta.x * num3, delta.y * num3, delta.z * num3);
		contractEndVec.Set(0f - contractStartVec.x, 0f - contractStartVec.y, 0f - contractStartVec.z);
		if (!harpoonScript.attached)
		{
			contractStartVec *= 0.1f;
			contractEndVec *= 0.5f;
		}
		Rigidbody.AddForceAtPosition(contractStartVec, startRopePoint.position, ForceMode.Force);
		harpoonScript.rb.AddForceAtPosition(contractEndVec, endRopePoint.position, ForceMode.Force);
		if (harpoonScript.attached)
		{
			if (Time.fixedTime > harpoonScript.attachTime + 1f)
			{
				harpoonScript.CheckForBreak(contractStartVec);
			}
		}
		else
		{
			harpoonScript.rb.AddTorque(-harpoonScript.transform.forward * 10f, ForceMode.Force);
		}
	}

	public void Snap()
	{
		if (!isSimulating || snapped)
		{
			return;
		}
		if (base.HasParentMachine)
		{
			if (snapAudio != null && snapAudio.gameObject.activeInHierarchy)
			{
				if (WaterController.Exist)
				{
					if (WaterController.IsUnderwater(cylinder.position))
					{
						snapAudio.outputAudioMixerGroup = underwaterMixer;
					}
					else
					{
						snapAudio.outputAudioMixerGroup = mixer;
					}
				}
				snapAudio.pitch = UnityEngine.Random.Range(0.8f, 1f);
				snapAudio.Play();
			}
			if (snappedDustParticles.Length > 2)
			{
				snappedDustParticles[0].transform.SetParent(base.transform.parent, true);
				snappedDustParticles[1].transform.SetParent(base.transform.parent, true);
				snappedDustParticles[0].transform.position = startPoint.position;
				snappedDustParticles[1].transform.position = endPoint.position;
				snappedDustParticles[0].Play();
				snappedDustParticles[1].Play();
			}
		}
		snapped = true;
		if (_parentMachine != null)
		{
			if (StatMaster.isMP && SimPhysics)
			{
				NetworkBlock netBlock = NetBlock;
				if (netBlock != null)
				{
					netBlock.Event(NetworkEntity.EntityEvent.Break);
					netBlock.pollTransform = false;
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
			_parentMachine.UnregisterUpdate(this, false);
			_parentMachine.UnregisterFixedUpdate(this, false);
			_parentMachine.UnregisterLateUpdate(this, false);
		}
		IsDestroyed = true;
		cylinderGO.SetActive(false);
	}

	private void WinchContract(float rate)
	{
		if (maxMagnitude - currentLength > 5f)
		{
			maxMagnitude = currentLength;
		}
		float num = forceLerpTime / timeToFullForce;
		float num2 = ((!harpoonScript.attached) ? 1f : Mathf.Clamp(speedSlider.Value, 0f, 10f));
		maxMagnitude -= Time.fixedDeltaTime * rate * num2 * num * Mathf.Clamp01(currentLength * 0.5f);
		maxMagnitude = Mathf.Max(maxMagnitude, 0.01f);
		AnimateWinch(1f);
	}

	private void AnimateWinch(float invert)
	{
		if (!harpoonScript.attached || !(currentLength > prevCurrentLength))
		{
			float num = winchRotationAngle.x * Time.deltaTime * invert;
			if (num != 0f)
			{
				animateWinchVec.x = num;
				winchRotateVis.Rotate(animateWinchVec);
				animateWinchVec.x = 0f - num;
			}
			prevCurrentLength = currentLength;
		}
	}

	public void FireKill()
	{
		if (!snapped)
		{
			Snap();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (SimPhysics && harpoonScript != null)
		{
			if (useProjManager)
			{
				ProjectileManager.Instance.Despawn(harpoonScript.transform);
			}
			else
			{
				UnityEngine.Object.Destroy(harpoonScript.gameObject);
			}
		}
	}
}
