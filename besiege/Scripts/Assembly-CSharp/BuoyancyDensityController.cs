using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class BuoyancyDensityController : BlockBehaviour, IExplosionEffect
{
	public const float NeutralBuoyancy = 0.4377f;

	public float buoyancyMultiplier = 1f;

	private MSlider buoyancySlider;

	private MSlider rateUpSlider;

	private MSlider rateDownSlider;

	private MToggle returnToggle;

	private MKey increaseBuoyancy;

	private MKey decreaseBuoyancy;

	private bool increaseIsHeld;

	private bool decreaseIsHeld;

	private bool emuIncreaseIsHeld;

	private bool emuDecreaseIsHeld;

	public float buoyancyStep = 0.02f;

	public Transform brokenInstance;

	public Transform brokenWaterInstance;

	public bool breakable;

	public float breakThreshold = 1000f;

	[FormerlySerializedAs("audio")]
	public AudioSource sfx;

	public AudioClip[] impactSfx = new AudioClip[0];

	public float audioCutoff = 150f;

	protected float minValue = 0.1f;

	protected float maxValue = 10f;

	protected float maxDensity = 30f;

	protected float minDensity = 0.33334f;

	protected bool broken;

	protected float defaultDensity = 1f;

	private float orgMass = 1f;

	private float neutralDensity = 1f;

	private float verticalDrag = 1f;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	protected float lerpValue;

	protected float lastDensity = 1f;

	public MSlider BuoyancySlider
	{
		get
		{
			return buoyancySlider;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		neutralDensity = 2.2846699f;
		increaseBuoyancy = AddKey(4525, "RISE", ControlScheme.BlockControls.Barrel, 0, KeyCode.U);
		decreaseBuoyancy = AddKey(4526, "SINK", ControlScheme.BlockControls.Barrel, 1, KeyCode.J);
		if (breakable)
		{
			maxValue = 12f;
			verticalDrag = 1f;
		}
		else
		{
			orgMass = 0.25f;
			verticalDrag = 2f;
		}
		buoyancySlider = AddSlider(2466, "water-buoyancy", (!breakable) ? 2f : 1f, minValue, maxValue, string.Empty);
		buoyancySlider.logScaling = true;
		rateUpSlider = AddSlider(4625, "rate-up", 0.5f, 0.01f, 1f, string.Empty);
		rateDownSlider = AddSlider(4626, "rate-down", 0.5f, 0.01f, 1f, string.Empty);
		returnToggle = AddToggle(3655, "autoReturn", true);
		if (!isSimulating)
		{
			buoyancySlider.ValueChanged += BuoyancySliderChanged;
			BuoyancySliderChanged(buoyancySlider.Value);
		}
		else if (sfx == null)
		{
			Debug.LogError("Probably stripped block not having sound");
		}
		else
		{
			mixer = sfx.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
		}
		minDensity = 1f / (maxValue * buoyancyMultiplier * 0.4377f);
		maxDensity = 1f / (minValue * buoyancyMultiplier * 0.4377f);
	}

	public void SetBuoyancy(float m)
	{
		buoyancySlider.SetValue(m);
	}

	public XData SaveBuoyancy()
	{
		return buoyancySlider.Serialize();
	}

	protected override void Start()
	{
		base.Start();
		if (!isSimulating || SimPhysics)
		{
			BuoyancySliderChanged(buoyancySlider.Value);
			buoyancySlider.ValueChanged += BuoyancySliderChanged;
			if (isSimulating && breakable)
			{
				Transform obj = brokenInstance;
				Transform parent = ((!StatMaster.isMP) ? ReferenceMaster.physicsGoalInstance : base.transform.parent);
				brokenWaterInstance.parent = parent;
				obj.parent = parent;
				brokenInstance.rotation = Quaternion.identity;
			}
		}
	}

	public override void FixedUpdateBlock()
	{
		if (!increaseIsHeld && !decreaseIsHeld && !emuIncreaseIsHeld && !emuDecreaseIsHeld && returnToggle.IsActive)
		{
			if (lerpValue < 0f)
			{
				lerpValue += rateDownSlider.Value * Time.fixedDeltaTime;
			}
			else
			{
				lerpValue -= rateUpSlider.Value * Time.fixedDeltaTime;
			}
			if (Mathf.Abs(lerpValue) < 0.001f)
			{
				lerpValue = 0f;
			}
			SetDensity(Mathf.Lerp(defaultDensity, lastDensity, lerpValue));
		}
		if (!noRigidbody && base.InWater)
		{
			Vector3 velocity = Rigidbody.velocity;
			velocity.x = (velocity.z = 0f);
			velocity.y *= submergedPercent * Mathf.Abs(velocity.y) * -0.5f * verticalDrag;
			velocity.y = Mathf.Clamp(velocity.y, -500f, 500f);
			Rigidbody.AddForce(velocity, ForceMode.Acceleration);
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		increaseIsHeld = increaseBuoyancy.IsHeld;
		decreaseIsHeld = decreaseBuoyancy.IsHeld;
		NewControls(increaseIsHeld && !emuIncreaseIsHeld, decreaseIsHeld && !emuDecreaseIsHeld, Time.deltaTime);
	}

	public override void EmulationUpdateBlock()
	{
		emuIncreaseIsHeld = increaseBuoyancy.EmulationHeld(true);
		emuDecreaseIsHeld = decreaseBuoyancy.EmulationHeld(true);
		NewControls(emuIncreaseIsHeld, emuDecreaseIsHeld, Time.fixedDeltaTime * 2f);
	}

	protected void OldControls(bool increaseBuoyancyHeld, bool decreaseBuoyancyHeld)
	{
		if (decreaseBuoyancyHeld && _inWater)
		{
			float num = density + buoyancyStep * Time.deltaTime * rateUpSlider.Value;
			if (num > maxDensity)
			{
				num = maxDensity;
			}
			SetDensity(num);
		}
		if (increaseBuoyancyHeld)
		{
			float num2 = density - buoyancyStep * Time.deltaTime * rateUpSlider.Value;
			if (num2 < minDensity)
			{
				num2 = minDensity;
			}
			SetDensity(num2);
		}
	}

	protected void NewControls(bool increaseBuoyancyHeld, bool decreaseBuoyancyHeld, float delta)
	{
		if (increaseBuoyancyHeld)
		{
			lerpValue -= rateUpSlider.Value * delta;
			lerpValue = Mathf.Max(lerpValue, -1f);
		}
		else if (decreaseBuoyancyHeld && _inWater)
		{
			lerpValue += rateDownSlider.Value * delta;
			lerpValue = Mathf.Min(lerpValue, 1f);
		}
		if (lerpValue < 0f)
		{
			float t = Mathf.Max(0f - lerpValue, 0f) * rateUpSlider.Value;
			lastDensity = Mathf.Lerp(defaultDensity, minDensity, t);
			SetDensity(lastDensity);
		}
		else
		{
			float t2 = Mathf.Max(lerpValue, 0f) * rateDownSlider.Value;
			lastDensity = Mathf.Lerp(defaultDensity, maxDensity, t2);
			SetDensity(lastDensity);
		}
	}

	private void BuoyancySliderChanged(float newBuoyancy)
	{
		if (newBuoyancy == 0f)
		{
			newBuoyancy = 1f;
		}
		float num = buoyancyMultiplier * newBuoyancy * 0.4377f;
		float num2 = Mathf.Max(0.01f, 1f / num);
		SetDensity(num2);
		defaultDensity = num2;
	}

	private void SetDensity(float d)
	{
		if (!noRigidbody && !broken && density != d)
		{
			density = d;
			SetMassFromDensity(d);
		}
	}

	private void SetMassFromDensity(float d)
	{
		if (breakable)
		{
			Rigidbody.mass = orgMass + Mathf.Lerp(0f, 0.25f, Mathf.InverseLerp(neutralDensity, neutralDensity * 2f, d));
			return;
		}
		Rigidbody.mass = orgMass + Mathf.Lerp(0f, 0.25f, Mathf.InverseLerp(neutralDensity * 0.5f, neutralDensity, d));
		Rigidbody.drag = Mathf.Lerp(0.2f, 0.6f, Mathf.InverseLerp(1f, minDensity, d)) + WaterDrag;
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!isSimulating || !SimPhysics)
		{
			return false;
		}
		if ((float)(mask & 8) != 0f)
		{
			Break();
			return true;
		}
		return false;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!SimPhysics || !isSimulating || StatMaster.GodTools.UnbreakableMode)
		{
			return;
		}
		float sqrMagnitude = other.relativeVelocity.sqrMagnitude;
		if (other.collider.gameObject.layer == 2)
		{
			return;
		}
		if (sqrMagnitude > audioCutoff)
		{
			PlaySound();
			if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly)
			{
				if (NetBlock != null)
				{
					NetBlock.Event(NetworkEntity.EntityEvent.SoundOnCollide, 0);
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
		}
		if (sqrMagnitude > breakThreshold)
		{
			Break();
		}
	}

	public void PlaySound()
	{
		if (!sfx.isPlaying)
		{
			PlaySound(impactSfx, 1.1f, 1.4f);
		}
	}

	protected void PlaySound(AudioClip[] sfx, float pitchMin, float pitchMax)
	{
		if (sfx.Length > 0)
		{
			if (base.GetSubmergedPctMV > 0.9f)
			{
				this.sfx.outputAudioMixerGroup = underwaterMixer;
			}
			else
			{
				this.sfx.outputAudioMixerGroup = mixer;
			}
			AudioClip clip = sfx[UnityEngine.Random.Range(0, sfx.Length)];
			this.sfx.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
			this.sfx.volume = UnityEngine.Random.Range(0.05f, 0.2f);
			this.sfx.clip = clip;
			this.sfx.Play();
		}
	}

	public void Break()
	{
		if (breakable)
		{
			breakable = false;
			CreateSimLists();
			foreach (Joint item in jointsToMe)
			{
				if ((bool)item)
				{
					float breakForce = (item.breakTorque = 0f);
					item.breakForce = breakForce;
				}
			}
			foreach (Joint item2 in iJointTo)
			{
				if ((bool)item2)
				{
					float breakForce = (item2.breakTorque = 0f);
					item2.breakForce = breakForce;
				}
			}
			jointsToMe.Clear();
			iJointTo.Clear();
			if (VisualController.selectedSkin.isDefault)
			{
				Vector3 position = base.transform.position + base.transform.forward * 1.5f;
				brokenInstance.position = position;
				brokenInstance.rotation = base.transform.rotation;
				brokenInstance.gameObject.SetActive(true);
				if (base.InWater && base.GetSubmergedPctMV > 0.4f)
				{
					brokenWaterInstance.position = position;
					brokenWaterInstance.gameObject.SetActive(true);
				}
				CopyMaterialProperties();
				RegisterSimUpdates(false, false, false, false);
				base.gameObject.SetActive(false);
			}
			else
			{
				RegisterSimUpdates(false, false, false, false);
				SetDensity(3f);
			}
		}
		else
		{
			BlockHealth.DamageBlock(2f);
			SetDensity(3f);
		}
		broken = true;
	}

	public virtual void CopyMaterialProperties()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		MeshRenderer.GetPropertyBlock(materialPropertyBlock);
		MeshRenderer[] componentsInChildren = brokenInstance.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetPropertyBlock(materialPropertyBlock);
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating || !data.WasLoadedFromFile || data.HasKey("bmt-water-buoyancy"))
		{
			return;
		}
		string key = "bmt-buoyancy";
		if (data.HasKey(key))
		{
			MapperType mapperType = buoyancySlider;
			XData xData = data.Read(key);
			if (xData != null || !StatMaster.isPaste)
			{
				mapperType.DeSerialize((xData == null) ? mapperType.defaultData : xData);
			}
		}
	}

	public void OnJointBreak(float force)
	{
		if (SimPhysics && StatMaster.stressCoded)
		{
			BlockJoint j;
			int jointLikyBroken = GetJointLikyBroken(out j);
			Vector3 pos = ((jointLikyBroken < 0) ? base.transform.position : base.transform.TransformPoint(j.anchor));
			FragmentVisualController.EmitJointBreakMarker(pos);
		}
	}
}
