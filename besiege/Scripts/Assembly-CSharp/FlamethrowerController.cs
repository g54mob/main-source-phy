using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

[AddComponentMenu("Blocks/Block Behaviours/FlamethrowerController")]
public class FlamethrowerController : BlockBehaviour
{
	public bool isFlaming;

	private float timey;

	public bool timeOut;

	[FormerlySerializedAs("particles")]
	public ParticleSystem fireParticles;

	public ParticleSystem bubbleParticles;

	public FireController fireController;

	public float fuelConsumptionUnderwater = 0.25f;

	private bool submerged;

	[NonSerialized]
	[Obsolete("Use fireParticles instead.")]
	public ParticleSystem particles;

	public ReloadAnimation anim;

	public bool keyWasHeld;

	private float baseAmmo = 10f;

	private MKey igniteKey;

	private MToggle holdToFire;

	private MSlider rangeSlider;

	public AudioSource sfx;

	public AudioClip fireIgnite;

	public AudioClip fireLoop;

	public AudioClip bubbleLoop;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	private ParticleSystem.VelocityOverLifetimeModule fireVel;

	private ParticleSystem.LimitVelocityOverLifetimeModule limit;

	private bool keyPressed;

	private bool emuPressed;

	private bool keyHeld;

	private bool emuHeld;

	private float targetVolume = 1f;

	private float targetPitch = 1f;

	private float fadeOut;

	public MToggle HoldToFireToggle
	{
		get
		{
			return holdToFire;
		}
	}

	public MSlider RangeSlider
	{
		get
		{
			return rangeSlider;
		}
	}

	public MKey IgniteKey
	{
		get
		{
			return igniteKey;
		}
	}

	protected override void Awake()
	{
		particles = fireParticles;
		base.Awake();
		anim.Awake(this);
		igniteKey = AddKey(2463, "ignite", ControlScheme.BlockControls.Flamethrower, 0, KeyCode.Y);
		holdToFire = AddToggle(2464, "hold-to-fire", false);
		if (isSimulating)
		{
			mixer = sfx.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
			fireVel = fireParticles.velocityOverLifetime;
			limit = fireParticles.limitVelocityOverLifetime;
			if (bubbleParticles != null)
			{
				bubbleParticles.Stop();
				bubbleParticles.randomSeed = (uint)UnityEngine.Random.Range(0f, 9999999f);
			}
			fireParticles.Stop();
			fireParticles.randomSeed = (uint)UnityEngine.Random.Range(0f, 9999999f);
			timey += baseAmmo;
		}
		else
		{
			WaterFogController.AddEffectMat(fireParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial);
		}
		rangeSlider = AddSlider(2465, "range", 1f, 0.2f, 1.25f, string.Empty);
		rangeSlider.ValueChanged += SetFlameRange;
	}

	private void SetFlameRange(float value)
	{
		fireParticles.startSpeed = 12.3f * value + 0.5f;
		if (value > 1f)
		{
			ParticleSystem.EmissionModule emission = fireParticles.emission;
			emission.rate = emission.rate.constant * 1.125f;
		}
		if (!stripped)
		{
			fireController.overlapCenter.z = 2.4443357f * value + 1.636254f;
			fireController.overlapSize.z = 4.8886714f * value + 0.3571632f;
		}
	}

	public override void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock, bool playAnim = true)
	{
		if (type != ReloadAmmoType.All && type != ReloadAmmoType.Fire)
		{
			return;
		}
		if (setAmmo)
		{
			if (eachBlock || (float)units < baseAmmo)
			{
				timey = (float)units * 0.25f;
				units = 0;
			}
			else
			{
				units -= (int)baseAmmo;
				timey = baseAmmo;
			}
			if (playAnim)
			{
				anim.AnimateReload((timey > 0f) ? 3 : 0);
			}
		}
		else
		{
			float num = 0f;
			if (eachBlock || (float)units <= baseAmmo - timey)
			{
				timey += (float)units * 0.25f;
				num = timey;
				units = 0;
			}
			else
			{
				num = (baseAmmo - timey) * 4f;
				units -= (int)num;
				timey = baseAmmo;
			}
			if (playAnim)
			{
				anim.AnimateReload((num > 0f) ? 3 : 0);
			}
		}
		timeOut = timey <= 0f;
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		keyPressed = igniteKey.IsPressed;
		keyHeld = igniteKey.IsHeld;
		UpdateFlameState();
		EvaluateKey(keyPressed, keyHeld, emuHeld, ref keyWasHeld);
		if (!timeOut)
		{
			if (isFlaming)
			{
				timey -= Time.deltaTime * ((!submerged) ? 1f : fuelConsumptionUnderwater);
			}
			if (timey <= 0f)
			{
				TimeOut();
			}
		}
		if (sfx.isPlaying)
		{
			sfx.pitch = targetPitch * ((!submerged) ? 1f : 0.65f);
		}
	}

	public override void EmulationUpdateBlock()
	{
		emuPressed = igniteKey.EmulationPressed();
		emuHeld = igniteKey.EmulationHeld(true);
		EvaluateKey(emuPressed, emuHeld, keyHeld, ref keyWasHeld);
	}

	protected void EvaluateKey(bool keyPressed, bool keyHeld, bool altHeld, ref bool keyWasHeld)
	{
		if (!holdToFire.IsActive)
		{
			if (keyPressed)
			{
				Flame();
			}
		}
		else if (keyHeld || altHeld)
		{
			if (!keyWasHeld)
			{
				keyWasHeld = true;
				FlameOn();
			}
		}
		else if (keyWasHeld)
		{
			keyWasHeld = false;
			FlameOff();
		}
	}

	private void Flame()
	{
		if (!isFlaming)
		{
			FlameOn();
		}
		else
		{
			FlameOff();
		}
	}

	private void UpdateSubmerged()
	{
		if (!base.InWater && !StatMaster.GodTools.GravityDisabled)
		{
			submerged = false;
			return;
		}
		submerged = base.GetSubmergedPctMV >= 1f && !StatMaster.GodTools.GravityDisabled;
		if (!submerged)
		{
			submerged = WaterController.Exist && WaterController.IsUnderwater(sfx.transform.position);
		}
	}

	private void UpdateFlameState()
	{
		bool flag = submerged;
		UpdateSubmerged();
		if (!isFlaming)
		{
			if (fadeOut > 0f)
			{
				sfx.volume = targetVolume * fadeOut;
				fadeOut -= Time.deltaTime;
			}
			else if (sfx.isPlaying)
			{
				sfx.Stop();
			}
			return;
		}
		Vector3 vector = base.transform.InverseTransformDirection((!SimPhysics) ? NetBlock.Velocity : Rigidbody.velocity);
		float magnitude = vector.magnitude;
		fireVel.z = Mathf.Max(-1f, vector.z);
		limit.limit = Mathf.Max(0f, 20f + magnitude * 0.8f);
		if (submerged != flag)
		{
			if (submerged)
			{
				fireParticles.Stop();
				bubbleParticles.Play();
				fireController.gameObject.SetActive(false);
				sfx.Stop();
				sfx.timeSamples = 0;
				sfx.outputAudioMixerGroup = underwaterMixer;
				sfx.clip = bubbleLoop;
			}
			else
			{
				fireParticles.Play();
				bubbleParticles.Stop();
				fireController.gameObject.SetActive(true);
				sfx.Stop();
				sfx.timeSamples = 0;
				sfx.outputAudioMixerGroup = mixer;
				sfx.clip = fireLoop;
			}
			sfx.Play();
			sfx.timeSamples = UnityEngine.Random.Range(0, sfx.clip.samples);
		}
	}

	private void FlameOn()
	{
		if (StatMaster.Rules.DisableFire)
		{
			return;
		}
		if (base.HasParentMachine && _parentMachine.InfiniteAmmoMode)
		{
			StatMaster.GodTools.HasBeenUsed = true;
		}
		else if (timeOut)
		{
			return;
		}
		if (!sfx.isPlaying || fadeOut > 0f)
		{
			float num = Mathf.InverseLerp(0.25f, 1.25f, rangeSlider.Value);
			targetPitch = 0.6f + num * 0.5f;
			targetVolume = 0.1f + num * 0.1f;
			sfx.pitch = targetPitch * ((!submerged) ? 1f : 0.65f);
			sfx.volume = targetVolume;
			if (fadeOut <= 0f)
			{
				sfx.timeSamples = 0;
				sfx.clip = ((!submerged) ? fireLoop : bubbleLoop);
				sfx.outputAudioMixerGroup = mixer;
				sfx.Play();
				sfx.timeSamples = UnityEngine.Random.Range(0, sfx.clip.samples);
				if (!submerged)
				{
					sfx.PlayOneShot(fireIgnite, 0.75f);
				}
			}
		}
		isFlaming = true;
		fadeOut = 0f;
		if (SimPhysics && !submerged)
		{
			fireController.gameObject.SetActive(true);
		}
		if (!StatMaster.isHeadless)
		{
			if (submerged && bubbleParticles != null)
			{
				bubbleParticles.Play();
			}
			else
			{
				fireParticles.Play();
			}
		}
	}

	private void FlameOff()
	{
		isFlaming = false;
		anim.StopReloadAnim();
		fadeOut = 1f;
		if (SimPhysics)
		{
			fireController.gameObject.SetActive(false);
		}
		if (!StatMaster.isHeadless)
		{
			fireParticles.Stop();
			if (bubbleParticles != null)
			{
				bubbleParticles.Stop();
			}
		}
	}

	private void TimeOut()
	{
		if (!timeOut && base.HasParentMachine)
		{
			timey = 0f;
			timeOut = true;
			if (!base.ParentMachine.InfiniteAmmoMode)
			{
				FlameOff();
			}
		}
	}
}
