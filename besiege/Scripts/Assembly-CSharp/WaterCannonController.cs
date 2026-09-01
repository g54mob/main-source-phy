using System;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/Block Behaviours/WaterCannonController")]
public class WaterCannonController : BlockBehaviour, IFireEffect
{
	public ParticleSystem[] waterParticles;

	public ParticleSystem[] steamParticles;

	public ParticleSystemRenderer[] overParticles;

	public ParticleSystemRenderer[] underParticles;

	public AudioSource sfx;

	public AudioClip waterClip;

	public AudioClip steamClip;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	protected bool mixerIsAbove = true;

	public bool isActive;

	public bool boiling;

	public bool prevActiveState;

	public bool prevBoilingState;

	public float waterNegativeForce = 100f;

	public float steamNegativeForce = 100f;

	public int version = 1;

	private float[] waterSpeeds;

	private float[] waterEmissionRates;

	private float[] steamSpeeds;

	private float[] steamEmissionRates;

	private MKey shootKey;

	private MToggle holdToShootToggle;

	private MSlider strengthSlider;

	private float boilingTimer;

	private bool water;

	private float boilingAmount;

	private bool physicsRunning;

	private bool keyPressed;

	private bool emuPressed;

	private bool keyHeld;

	private bool emuHeld;

	private float particleTime;

	private float volume = 1f;

	public MToggle HoldToShootToggle
	{
		get
		{
			return holdToShootToggle;
		}
	}

	public MSlider StrengthSlider
	{
		get
		{
			return strengthSlider;
		}
	}

	public MKey ShootKey
	{
		get
		{
			return shootKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		shootKey = AddKey(2429, "shoot", ControlScheme.BlockControls.WaterCannon, 0, KeyCode.Y);
		holdToShootToggle = AddToggle(2441, "hold-to-fire", false);
		strengthSlider = AddSlider(2427, "strength", 1f, 0.1f, 3f, string.Empty);
		if (!isSimulating)
		{
			return;
		}
		if (WaterController.Exist)
		{
			if (sfx != null)
			{
				mixer = sfx.outputAudioMixerGroup;
				underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
			}
			water = true;
			WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Combine(WaterFogController.UnderwaterToggled, new Action<bool>(Underwater));
			Underwater(!WaterFogController.overWater);
		}
		if (sfx != null)
		{
			SetSfxParameters();
		}
		SetParticles(waterParticles, true, false);
		SetParticles(steamParticles, false, false);
		waterSpeeds = new float[waterParticles.Length];
		waterEmissionRates = new float[waterParticles.Length];
		steamSpeeds = new float[steamParticles.Length];
		steamEmissionRates = new float[steamParticles.Length];
		if (steamParticles.Length != 0 && steamParticles[0] != null)
		{
			WaterFogController.AddEffectMat(steamParticles[0].GetComponent<ParticleSystemRenderer>().sharedMaterial);
		}
		GetEmitRate(waterParticles);
		GetEmitRate(steamParticles);
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		physicsRunning = true;
	}

	protected override void OnDestroy()
	{
		physicsRunning = false;
		SetParticles(waterParticles, false, false);
		SetParticles(steamParticles, false, false);
		if (isSimulating && water)
		{
			WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Remove(WaterFogController.UnderwaterToggled, new Action<bool>(Underwater));
		}
		base.OnDestroy();
	}

	private void Underwater(bool under)
	{
		for (int i = 0; i < underParticles.Length; i++)
		{
			underParticles[i].enabled = under;
		}
		for (int j = 0; j < overParticles.Length; j++)
		{
			overParticles[j].enabled = !under;
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		keyPressed = shootKey.IsPressed;
		keyHeld = shootKey.IsHeld;
		EvaluateKey(keyPressed, keyHeld, emuHeld);
		if (!SimPhysics)
		{
			float num = NetworkScene.ServerSettings.sendRate * 2f;
			if (VisualController.lastIgniteTime > Time.time - num)
			{
				boilingTimer += Time.deltaTime * 10f;
			}
			boiling = boilingAmount > 0.1f;
			LerpGlow(Time.deltaTime * 3f);
		}
		if (physicsRunning)
		{
			SetParticleUpdate();
		}
		else if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			SetParticleUpdate();
		}
		if (WaterController.Exist)
		{
			if (base.GetSubmergedPctMV > 0.75f)
			{
				if (mixerIsAbove)
				{
					sfx.outputAudioMixerGroup = underwaterMixer;
					mixerIsAbove = false;
				}
			}
			else if (!mixerIsAbove)
			{
				sfx.outputAudioMixerGroup = mixer;
				mixerIsAbove = true;
			}
		}
		float num2 = Time.time - particleTime;
		float num3 = Application.targetFrameRate;
		if (num3 < 0f)
		{
			num3 = 100f;
		}
		if (num2 > Time.timeScale / num3)
		{
			SimulateParticles(waterParticles, num2, false);
			SimulateParticles(steamParticles, num2, false);
			particleTime = Time.time;
		}
	}

	private void SetParticleUpdate()
	{
		if (prevActiveState != isActive || prevBoilingState != boiling)
		{
			prevActiveState = isActive;
			prevBoilingState = boiling;
			SetParticles();
		}
	}

	public override void EmulationUpdateBlock()
	{
		emuPressed = shootKey.EmulationPressed();
		emuHeld = shootKey.EmulationHeld(true);
		EvaluateKey(emuPressed, emuHeld, keyHeld);
	}

	protected bool EvaluateKey(bool keyPressed, bool keyHeld, bool altHeld)
	{
		bool result = false;
		if (!holdToShootToggle.IsActive)
		{
			if (keyPressed)
			{
				isActive = !isActive;
				result = true;
			}
		}
		else
		{
			bool flag = keyHeld || altHeld;
			if (isActive != flag)
			{
				isActive = flag;
				result = true;
			}
		}
		return result;
	}

	public override void FixedUpdateBlock()
	{
		LerpGlow(Time.fixedDeltaTime);
		if (isActive)
		{
			boiling = boilingAmount > 0.1f;
			float num = ((!boiling) ? waterNegativeForce : steamNegativeForce);
			float num2 = ((!(strengthSlider.Value > 1f)) ? strengthSlider.Value : (1f + (strengthSlider.Value - 1f) * 1.4f));
			if (!isParented)
			{
				Rigidbody.AddRelativeForce(Vector3.up * num * num2);
			}
			else
			{
				Rigidbody.AddForceAtPosition(base.transform.TransformDirection(Vector3.up * num * num2), base.transform.TransformPoint(originalCOM));
			}
		}
		float deltaTime = Time.fixedTime - particleTime;
		SimulateParticles(waterParticles, deltaTime, true);
		SimulateParticles(steamParticles, deltaTime, true);
		particleTime = Time.fixedTime;
	}

	private void SetParticles()
	{
		if (isActive)
		{
			SetParticles((!boiling) ? waterParticles : steamParticles, !boiling, true);
			SetParticles((!boiling) ? steamParticles : waterParticles, boiling, false);
			sfx.Stop();
			float value = strengthSlider.Value;
			bool flag = boiling || value < -10f || value > 50f;
			sfx.clip = ((!flag) ? waterClip : steamClip);
			sfx.timeSamples = UnityEngine.Random.Range(0, sfx.clip.samples);
			sfx.volume = volume * ((!flag) ? 1f : 0.75f);
			sfx.Play();
		}
		else
		{
			SetParticles(waterParticles, true, false);
			SetParticles(steamParticles, false, false);
			sfx.Stop();
		}
	}

	private void SetSfxParameters()
	{
		float value = strengthSlider.Value;
		float num = Mathf.Clamp01((Mathf.Abs(value) - 1f) * 0.01f);
		sfx.pitch = 1f + num * 0.5f;
		sfx.volume = (volume = Mathf.Lerp(0.25f, 0.05f, num));
	}

	public void SimulateParticles(ParticleSystem[] particleSystem, float deltaTime, bool fixedTime)
	{
		if (StatMaster.isHeadless)
		{
			return;
		}
		foreach (ParticleSystem particleSystem2 in particleSystem)
		{
			if (particleSystem2.collision.enabled)
			{
				particleSystem2.Simulate(deltaTime, true, false, fixedTime);
			}
		}
	}

	private void SetParticles(ParticleSystem[] particleSystem, bool isWater, bool toggle)
	{
		if (strengthSlider.Value > 2.1474836E+09f)
		{
			return;
		}
		float num = 0f;
		if (toggle)
		{
			num = ((!(strengthSlider.Value > 1f)) ? strengthSlider.Value : (1f + (strengthSlider.Value - 1f) / 3.8f));
			num = ((!isWater || !(num < 0.4f)) ? num : (0.5f * num + 0.2f));
		}
		for (int i = 0; i < particleSystem.Length; i++)
		{
			ParticleSystem particleSystem2 = particleSystem[i];
			if (!StatMaster.isHeadless || particleSystem2.collision.enabled)
			{
				ParticleSystem.EmissionModule emission = particleSystem2.emission;
				if (toggle)
				{
					particleSystem2.startSpeed = ((!isWater) ? steamSpeeds[i] : waterSpeeds[i]) * num;
					emission.rate = ((!isWater) ? steamEmissionRates[i] : waterEmissionRates[i]) * num;
				}
				if (!particleSystem2.isPlaying)
				{
					particleSystem2.randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
				}
				if (!particleSystem2.collision.enabled)
				{
					particleSystem2.Play();
				}
				emission.enabled = toggle;
			}
		}
	}

	private void GetEmitRate(ParticleSystem[] particleSystem)
	{
		for (int i = 0; i < particleSystem.Length; i++)
		{
			ParticleSystem particleSystem2 = particleSystem[i];
			bool flag = particleSystem == waterParticles;
			float[] array = ((!flag) ? steamSpeeds : waterSpeeds);
			float[] array2 = ((!flag) ? steamEmissionRates : waterEmissionRates);
			array[i] = particleSystem2.startSpeed;
			array2[i] = particleSystem2.emission.rate.constant;
			if (!particleSystem2.isPlaying && !particleSystem2.collision.enabled)
			{
				particleSystem2.Play();
				ParticleSystem.EmissionModule emission = particleSystem2.emission;
				emission.enabled = true;
			}
		}
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		boilingTimer += Time.deltaTime * 10f;
		return true;
	}

	protected void LerpGlow(float delta)
	{
		float t = delta * Prefab.heatLerpSpeed;
		if (boilingTimer > 0f || boilingAmount > 0f)
		{
			boilingTimer = Mathf.Clamp01(boilingTimer - delta);
			boilingAmount = Mathf.Lerp(boilingAmount, (!(boilingTimer > 0f)) ? 0f : 1f, t);
			if (boilingAmount <= 0.01f)
			{
				boilingAmount = 0f;
			}
		}
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating)
		{
			return;
		}
		if (!data.HasKey("bmt-version"))
		{
			if (data.WasLoadedFromFile)
			{
				version = 0;
				data.Write("bmt-version", version);
			}
		}
		else
		{
			version = data.ReadInt("bmt-version");
		}
		if (version == 0)
		{
			strengthSlider.SetRange(0.1f, 1.5f);
		}
	}
}
