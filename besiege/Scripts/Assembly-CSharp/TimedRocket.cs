using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/Block Behaviours/TimedRocket")]
public class TimedRocket : BlockBehaviour, IExplosionEffect, IFireEffect
{
	public float radius = 5f;

	public float power = 10f;

	public float upPower = 3f;

	public Transform trailGo;

	public Transform exploGo;

	public Transform waterPlane;

	public ParticleSystem[] trail;

	public ParticleSystemRenderer[] smokeTrail;

	public ParticleSystem[] waterTrail;

	public ParticleSystem[] explosion;

	public ParticleSystem[] explosionInWater;

	public ParticleSystem[] getWaterSeed;

	public ParticleSystem[] particlesToColour;

	public ParticleSystem[] enableCollision;

	public ParticleSystem[] wpToColour;

	public Gradient[] colorGradients;

	public ParticleSystemRenderer[] waterRenderers;

	public RandomSoundController SFX;

	private Material[] waterExpMats;

	private Material[] smokeTrailMats;

	private ParticleSystem[] smokeTrailSystem;

	public Color waterColor = Color.white;

	public Transform dustCraterQuad;

	public float randomDelay = 0.08f;

	public float speed;

	public bool hasFired;

	public bool hasExploded;

	public GameObject thisVis;

	public Collider[] colliders;

	public Renderer slipRenderer;

	public FireController fireTrigger;

	public GameObject[] activateOnStart;

	public AudioSource flightSFX;

	[HideInInspector]
	public List<FragmentVisualController> jointsToMeFVC = new List<FragmentVisualController>();

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	private Vector3 explosionPos;

	private Collider[] hitColliders;

	private Rigidbody colAttachedRigidbody;

	private MKey launchKey;

	private MSlider delaySlider;

	private MSlider powerSlider;

	private MSlider chargeSlider;

	private MColourSlider colourSlider;

	private float timeFlown;

	private float realCharge = 1f;

	private List<Rigidbody> prevRigidbodies = new List<Rigidbody>();

	private IEnumerator explodeCoroutine;

	private Vector3 lastLoadedScale = Vector3.one;

	private bool explodedInWater;

	private bool water;

	private bool smokeFollowsWaterCam;

	protected float fireExposure;

	private bool lastSubmerged;

	public MSlider DelaySlider
	{
		get
		{
			return delaySlider;
		}
	}

	public MSlider PowerSlider
	{
		get
		{
			return powerSlider;
		}
	}

	public MSlider ChargeSlider
	{
		get
		{
			return chargeSlider;
		}
	}

	public MColourSlider ColourSlider
	{
		get
		{
			return ColourSlider;
		}
	}

	public MKey LaunchKey
	{
		get
		{
			return launchKey;
		}
	}

	public override Vector3 GetTarget()
	{
		return GetCenter();
	}

	public override Vector3 GetCenter()
	{
		return base.transform.TransformPoint(LocalCenter());
	}

	public Vector3 LocalCenter()
	{
		return Vector3.forward * 0.5f;
	}

	protected override void Awake()
	{
		base.Awake();
		launchKey = AddKey(2442, "launch", ControlScheme.BlockControls.Rocket, 0, KeyCode.T);
		delaySlider = AddSlider(2443, "duration", 1.5f, 0.5f, 10f, string.Empty);
		powerSlider = AddSlider(2444, "strength", 1f, 0.5f, 1.5f, string.Empty);
		chargeSlider = AddSlider(2445, "charge", 1f, 0f, 1.5f, string.Empty);
		colourSlider = AddColourSlider(2446, "colour", new Color(1f, 0.3f, 0f), true);
		SetSlip(colourSlider.Value);
		colourSlider.ValueChanged += SetSlip;
		if (!isSimulating)
		{
			return;
		}
		mixer = flightSFX.outputAudioMixerGroup;
		underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
		exploGo.parent = ReferenceMaster.physicsGoalInstance;
		if (SimPhysics)
		{
			fireTag.onlyIgniteOncePerFrame = false;
			if (base.transform.localScale != lastLoadedScale && !noRigidbody)
			{
				Rigidbody.centerOfMass = Vector3.Scale(LocalCenter(), base.transform.localScale);
			}
			GameObject[] array = activateOnStart;
			foreach (GameObject gameObject in array)
			{
				gameObject.SetActive(true);
			}
		}
	}

	private void SetSlip(Color value)
	{
		Color color = value + Mathf.Clamp(Mathf.Abs(value.r - value.g * 2f) * value.g * 0.5f + value.b, 0f, 1f) * Color.white * 0.3f;
		color = (color * 3f + Color.white * 2f) / 5f - Color.white * 0.2f;
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		materialPropertyBlock.SetColor("_SlipColor", color);
		VisualController.renderers[0].SetPropertyBlock(materialPropertyBlock);
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!hasFired && launchKey.IsPressed)
		{
			hasFired = true;
			StartCoroutine(IEFire(0f));
		}
		if (hasExploded || !flightSFX.isPlaying)
		{
			return;
		}
		if (base.GetSubmergedPctMV > 0.6f)
		{
			if (flightSFX.outputAudioMixerGroup != underwaterMixer)
			{
				flightSFX.outputAudioMixerGroup = underwaterMixer;
			}
		}
		else if (flightSFX.outputAudioMixerGroup != mixer)
		{
			flightSFX.outputAudioMixerGroup = mixer;
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (!hasFired && launchKey.EmulationPressed())
		{
			hasFired = true;
			StartCoroutine(IEFire(0f));
		}
	}

	private void OnDrawGizmos()
	{
		if (!noRigidbody && !(Rigidbody == null))
		{
			Vector3 worldCenterOfMass = Rigidbody.worldCenterOfMass;
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireSphere(worldCenterOfMass, 0.1f);
		}
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		if (hasExploded)
		{
			return false;
		}
		if (!pyroMode && c != null)
		{
			if (c.gameObject.CompareTag("LaunchIgnition"))
			{
				LaunchMessage();
				return true;
			}
			fireExposure += Time.fixedDeltaTime;
			if (fireExposure > 0.04f)
			{
				ExplodeMessage();
				return true;
			}
			return false;
		}
		LaunchMessage();
		return true;
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if ((mask & 0x20) != 0)
		{
			ExplodeMessage();
			return true;
		}
		return false;
	}

	public void ExplodeMessage()
	{
		if (isSimulating && !hasExploded)
		{
			if (explodeCoroutine != null)
			{
				StopCoroutine(explodeCoroutine);
			}
			float num = delaySlider.Max - timeFlown;
			if (chargeSlider.Value > 0f)
			{
				realCharge = (chargeSlider.Value + num * 0.1f) * 0.9f;
			}
			else if (num > delaySlider.Value - 0.35f)
			{
				realCharge = num * 0.1f * 0.9f;
			}
			else
			{
				realCharge = 0f;
			}
			hasExploded = true;
			hasFired = true;
			explodeCoroutine = Explode(0f);
			StartCoroutine(explodeCoroutine);
		}
	}

	public void LaunchMessage()
	{
		if (isSimulating && !hasFired)
		{
			hasFired = true;
			StartCoroutine(IEFire(0.15f));
		}
	}

	public void Fire(float initWait)
	{
		StartCoroutine(IEFire(initWait));
	}

	private IEnumerator IEFire(float initWait)
	{
		if (SimPhysics)
		{
			fireTag.onlyIgniteOncePerFrame = true;
			activateOnStart[0].SetActive(false);
		}
		if (initWait > 0f)
		{
			yield return new WaitForSeconds(initWait);
		}
		if (hasExploded)
		{
			yield break;
		}
		if (StatMaster.isMP && SimPhysics)
		{
			if (NetBlock != null)
			{
				NetBlock.Event(NetworkEntity.EntityEvent.Ignite, 128);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		realCharge = chargeSlider.Value;
		IgnoredByWater = true;
		density = 100f;
		base.InWater = WaterController.Exist && WaterController.IsUnderwater(SimPhysics ? Rigidbody.worldCenterOfMass : center);
		bool belowWater = base.InWater && base.GetSubmergedPctMV >= 0.5f;
		if (!noRigidbody)
		{
			Rigidbody.useGravity = !belowWater;
		}
		lastSubmerged = belowWater;
		StartSmokeTrail();
		float delay = delaySlider.Value;
		if (delay <= 0f)
		{
			delay = 0.0001f;
		}
		float f = 0f;
		float maxForce = 500f;
		float startSpeedFwd = SpeedAlongAxis(velMag: (SimPhysics ? Rigidbody.velocity : NetBlock.Velocity).magnitude, axis: base.transform.up);
		float pow = powerSlider.Value;
		if (pow == 0f)
		{
			pow = 0.0001f;
		}
		float maxSpeed = 52f * pow;
		flightSFX.pitch = 1f;
		flightSFX.Play();
		if (base.GetSubmergedPctMV > 0.6f)
		{
			flightSFX.outputAudioMixerGroup = underwaterMixer;
		}
		else
		{
			flightSFX.outputAudioMixerGroup = mixer;
		}
		for (timeFlown = 0f; timeFlown < delay; timeFlown += Time.fixedDeltaTime)
		{
			yield return new WaitForFixedUpdate();
			if (hasExploded)
			{
				yield break;
			}
			float speedFwd = SpeedAlongAxis(velMag: (SimPhysics ? Rigidbody.velocity : NetBlock.Velocity).magnitude, axis: base.transform.up);
			speed = Mathf.Clamp(speedFwd, 0f, startSpeedFwd + maxSpeed);
			if (startSpeedFwd > 0f)
			{
				startSpeedFwd -= maxSpeed / (5f * delay);
				if (startSpeedFwd < 0f)
				{
					startSpeedFwd = 0f;
				}
			}
			if (f < maxForce)
			{
				f += maxForce / 10f;
			}
			if (SimPhysics)
			{
				float forceNeeded = f * ((maxSpeed - speed) / maxSpeed);
				Rigidbody.AddForce(base.transform.up * pow * forceNeeded, ForceMode.Acceleration);
				base.InWater = WaterController.Exist && WaterController.IsUnderwater(Rigidbody.worldCenterOfMass);
				belowWater = base.InWater;
				if (belowWater != lastSubmerged)
				{
					lastSubmerged = belowWater;
					Rigidbody.useGravity = !belowWater;
					if (belowWater)
					{
						for (int i = 0; i < waterTrail.Length; i++)
						{
							waterTrail[i].Play();
						}
						for (int j = 0; j < smokeTrailSystem.Length; j++)
						{
							smokeTrailSystem[j].Stop();
						}
					}
					else
					{
						Rigidbody.AddTorque(Vector3.Cross(Vector3.up, base.transform.up) * Physics.gravity.y * -0.005f, ForceMode.Impulse);
						for (int k = 0; k < waterTrail.Length; k++)
						{
							waterTrail[k].Stop();
						}
						for (int l = 0; l < smokeTrailSystem.Length; l++)
						{
							smokeTrailSystem[l].Play();
						}
					}
				}
			}
			SetRateSmoke(speed);
		}
		if (SimPhysics && !hasExploded)
		{
			hasExploded = true;
			hasFired = true;
			if (explodeCoroutine != null)
			{
				StopCoroutine(explodeCoroutine);
			}
			explodeCoroutine = Explode(randomDelay);
			StartCoroutine(explodeCoroutine);
		}
	}

	private float SpeedAlongAxis(Vector3 axis, float velMag)
	{
		return (!(velMag > 0f)) ? 0f : (Vector3.Dot(axis, (SimPhysics ? Rigidbody.velocity : NetBlock.Velocity) / velMag) * velMag);
	}

	private void TrailUnderwater(bool camUnder)
	{
		if (camUnder)
		{
			for (int i = 0; i < smokeTrail.Length; i++)
			{
				smokeTrailMats[i].renderQueue = 2999;
			}
		}
		else
		{
			for (int j = 0; j < smokeTrail.Length; j++)
			{
				smokeTrailMats[j].renderQueue = 3001;
			}
		}
	}

	private void Underwater(bool camUnder)
	{
		if (camUnder)
		{
			for (int i = 0; i < waterRenderers.Length; i++)
			{
				waterRenderers[i].sortingLayerName = "Default";
				waterExpMats[i].renderQueue -= 4;
			}
		}
		else
		{
			for (int j = 0; j < waterRenderers.Length; j++)
			{
				waterRenderers[j].sortingLayerName = "Rocket";
				waterExpMats[j].renderQueue += 4;
			}
		}
	}

	private void SetRender(string layer, int offset)
	{
		waterExpMats = new Material[waterRenderers.Length];
		for (int i = 0; i < waterRenderers.Length; i++)
		{
			waterRenderers[i].sortingLayerName = layer;
			waterExpMats[i] = waterRenderers[i].material;
			waterExpMats[i].renderQueue += offset;
			waterRenderers[i].sharedMaterial = waterExpMats[i];
		}
	}

	public void OnExplode()
	{
		if (WaterController.Exist)
		{
			water = true;
			Vector3 vector = GetCenter();
			base.InWater = WaterController.IsUnderwater(vector);
			DebugExtension.DebugWireSphere(vector, (!base.InWater) ? Color.yellow : Color.cyan, 0.5f, 10f, false);
			explodedInWater = base.InWater;
			bool flag = !WaterFogController.overWater;
			if (!explodedInWater)
			{
				SetRender((!flag) ? "Rocket" : "Default", flag ? (-4) : 0);
				WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Combine(WaterFogController.UnderwaterToggled, new Action<bool>(Underwater));
			}
			else
			{
				SetRender("Rocket", 0);
			}
		}
		else
		{
			SetRender("Default", 0);
		}
		StopSmokeTrail();
		bool flag2 = realCharge <= 0f || StatMaster.Rules.DisableExplosions;
		if (StatMaster.isMP && SimPhysics)
		{
			NetworkBlock netBlock = NetBlock;
			if (netBlock != null)
			{
				netBlock.Event(NetworkEntity.EntityEvent.Explode);
				netBlock.pollTransform = flag2;
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		if (flag2)
		{
			return;
		}
		thisVis.SetActive(false);
		Collider[] array = colliders;
		foreach (Collider collider in array)
		{
			if (collider != null)
			{
				collider.enabled = false;
			}
		}
		hasExploded = true;
		StartCoroutine(ExplosionEffect());
	}

	private IEnumerator Explode(float delay)
	{
		StartCoroutine(ReduceTrailVolume(0.5f));
		for (float t = 0f; t < UnityEngine.Random.Range(0f, delay); t += Time.fixedDeltaTime)
		{
			ReduceSmokeTrail();
			yield return new WaitForFixedUpdate();
		}
		OnExplode();
		if (realCharge <= 0f || StatMaster.Rules.DisableExplosions)
		{
			yield return new WaitForSeconds(6f);
			ClearWaterCam();
		}
	}

	private IEnumerator ExplosionEffect()
	{
		StartCoroutine(DisableComponents());
		yield return null;
		ExplosionForce();
		DisplayExplosion();
		SFX.Play();
		DustCraterQuad();
		if (WaterController.Exist)
		{
			yield return new WaitForSeconds(6f);
			ClearWaterCam();
		}
	}

	protected override void OnDisable()
	{
		ClearWaterCam();
		base.OnDisable();
	}

	protected void ClearWaterCam()
	{
		if (water && !explodedInWater)
		{
			water = false;
			WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Remove(WaterFogController.UnderwaterToggled, new Action<bool>(Underwater));
		}
		if (smokeFollowsWaterCam)
		{
			smokeFollowsWaterCam = false;
			WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Remove(WaterFogController.UnderwaterToggled, new Action<bool>(TrailUnderwater));
		}
	}

	private void StartSmokeTrail()
	{
		for (int i = 0; i < trail.Length; i++)
		{
			trail[i].Stop();
			trail[i].randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
			trail[i].Play();
		}
		if (lastSubmerged)
		{
			for (int j = 0; j < waterTrail.Length; j++)
			{
				waterTrail[j].randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
				waterTrail[j].Play();
			}
		}
		fireTrigger.gameObject.SetActive(true);
		fireTrigger.enabled = true;
		if (!WaterController.Exist)
		{
			return;
		}
		smokeTrailMats = new Material[smokeTrail.Length];
		smokeTrailSystem = new ParticleSystem[smokeTrail.Length];
		for (int k = 0; k < smokeTrail.Length; k++)
		{
			smokeTrailSystem[k] = smokeTrail[k].GetComponent<ParticleSystem>();
			smokeTrailMats[k] = smokeTrail[k].material;
			smokeTrailMats[k].renderQueue = ((!WaterFogController.overWater) ? 2999 : 3001);
			if (base.InWater)
			{
				smokeTrailSystem[k].Stop();
			}
			smokeTrailMats[k].SetFloat("_BelowAlpha", 1f);
			smokeTrail[k].sharedMaterial = smokeTrailMats[k];
		}
		WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Combine(WaterFogController.UnderwaterToggled, new Action<bool>(TrailUnderwater));
		smokeFollowsWaterCam = true;
	}

	private void SetRateSmoke(float speed)
	{
		float num = -0.018f * speed + 1f;
		trail[0].startSpeed = ((!(speed < 6f)) ? (10f + 60f / speed) : 20f);
		trail[0].startSize = 3f + num * 0.5f;
	}

	private void ReduceSmokeTrail()
	{
		trail[0].startLifetime = 1f;
		trail[0].startSize = UnityEngine.Random.Range(1.5f, 2.5f);
		for (int i = 0; i < trail.Length; i++)
		{
			ParticleSystem.EmissionModule emission = trail[i].emission;
			float num = emission.rate.constant * 0.4f;
			num = ((!(num < 50f)) ? num : 50f);
			emission.rate = num;
		}
		fireTrigger.gameObject.SetActive(false);
	}

	private IEnumerator ReduceTrailVolume(float duration)
	{
		while (flightSFX.volume > 0f)
		{
			flightSFX.volume -= Time.deltaTime * (1f / duration);
			yield return null;
		}
		flightSFX.Stop();
	}

	private void StopSmokeTrail()
	{
		for (int i = 0; i < trail.Length; i++)
		{
			trail[i].Stop();
		}
		for (int j = 0; j < waterTrail.Length; j++)
		{
			waterTrail[j].Stop();
		}
		if (WaterController.Exist && smokeTrailSystem != null)
		{
			for (int k = 0; k < smokeTrailSystem.Length; k++)
			{
				smokeTrailSystem[k].Stop();
			}
		}
		fireTrigger.gameObject.SetActive(false);
	}

	private Color ColourCorrectExplosion(Color input)
	{
		float num = Mathf.Max(input.r, input.g, input.b);
		Color color = (-0.5f + num / (input.r + input.g + input.b)) * Color.white * 0.25f * 0.5f;
		Color color2 = Mathf.Clamp(Mathf.Abs(input.r - input.g * 2f) * 0.5f + input.b, 0f, 1f) * Color.white * 0.5f * 0.75f * 0.5f;
		return input + color + color2;
	}

	private void DisplayExplosion()
	{
		Color startColor = ColourCorrectExplosion(colourSlider.Value);
		ParticleSystem[] array = particlesToColour;
		foreach (ParticleSystem particleSystem in array)
		{
			particleSystem.startColor = startColor;
		}
		ParticleSystem[] array2 = enableCollision;
		foreach (ParticleSystem particleSystem2 in array2)
		{
			ParticleSystem.CollisionModule collision = particleSystem2.collision;
			collision.enabled = true;
		}
		explosion[0].transform.forward = Vector3.up;
		for (int k = 0; k < explosion.Length; k++)
		{
			explosion[k].Stop();
			explosion[k].randomSeed = (uint)UnityEngine.Random.Range(0f, 9999999f);
			explosion[k].Play();
		}
		if (!explodedInWater)
		{
			return;
		}
		for (int l = 0; l < wpToColour.Length; l++)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetime = wpToColour[l].colorOverLifetime;
			if (colorOverLifetime.enabled)
			{
				colorOverLifetime.color = colorGradients[l];
			}
			wpToColour[l].startColor = waterColor;
		}
		for (int m = 0; m < explosionInWater.Length; m++)
		{
			explosionInWater[m].Stop();
			explosionInWater[m].randomSeed = ((m >= getWaterSeed.Length) ? ((uint)UnityEngine.Random.Range(0f, 9999999f)) : getWaterSeed[m].randomSeed);
			explosionInWater[m].Play();
		}
		SFX.SetMixer(true);
	}

	private IEnumerator DisableComponents()
	{
		foreach (FragmentVisualController fragment in jointsToMeFVC)
		{
			if (!object.ReferenceEquals(fragment, null))
			{
				fragment.OnJointBreak(1f);
			}
		}
		foreach (Renderer vis in visAddedToMe)
		{
			vis.gameObject.SetActive(false);
		}
		Vector3 center = GetCenter();
		if (SimPhysics)
		{
			exploGo.position = center;
			CreateSimLists();
			foreach (Joint joint in jointsToMe)
			{
				if ((bool)joint)
				{
					float breakForce = (joint.breakTorque = 0f);
					joint.breakForce = breakForce;
				}
			}
			jointsToMe.Clear();
			yield return new WaitForFixedUpdate();
			Rigidbody.isKinematic = true;
			Rigidbody.useGravity = false;
			DestroyRigidbody();
			center = GetCenter();
		}
		exploGo.position = center;
		trailGo.parent = ReferenceMaster.physicsGoalInstance;
		float wait = 3f;
		if (WaterController.Exist && explodedInWater)
		{
			waterPlane.up = Vector3.down;
			float waterHeight = WaterController.waterTransformHeight;
			waterPlane.position = new Vector3(exploGo.position.x, waterHeight + 3f, exploGo.position.z);
			float depth = exploGo.position.y - WaterController.waterTransformHeight;
			depth = Mathf.Clamp((0f - depth) * 0.15f - 0.5f, -1E-45f, wait);
			if (depth > 0f)
			{
				wait -= depth;
				yield return new WaitForSeconds(depth);
			}
			Vector3 point1 = center + new Vector3(0f, 0f, 2f);
			Vector3 point2 = center + new Vector3(1.7321f, 0f, -1f);
			Vector3 point3 = center + new Vector3(-1.7321f, 0f, -1f);
			float t = 0f;
			float frames = 2f;
			while (t < wait)
			{
				point1.y = WaterController.CheckHeightMap(point1.x, point1.z);
				point2.y = WaterController.CheckHeightMap(point2.x, point2.z);
				point3.y = WaterController.CheckHeightMap(point3.x, point3.z);
				Vector3 normal = Vector3.Cross(point3 - point1, point2 - point1);
				float avg = (point1.y + point2.y + point3.y) / 3f;
				avg = (avg + waterHeight) / 2f;
				waterPlane.up = normal * 0.5f + Vector3.down;
				waterPlane.position = new Vector3(exploGo.position.x, avg, exploGo.position.z);
				for (int f = 0; (float)f < frames; f++)
				{
					t += Time.deltaTime;
					yield return null;
				}
				frames += 0.5f;
			}
		}
		else
		{
			yield return new WaitForSeconds(wait);
		}
		Vector3 startPos = base.transform.position;
		for (float f2 = 0f; f2 < 1f; f2 += Time.deltaTime)
		{
			base.transform.position = Vector3.Lerp(startPos, base.ParentMachine.MiddlePosition, f2);
			yield return null;
		}
		if (exploGo != null && exploGo.gameObject != null)
		{
			UnityEngine.Object.Destroy(exploGo.gameObject);
		}
		if (trailGo != null && trailGo.gameObject != null)
		{
			UnityEngine.Object.Destroy(trailGo.gameObject);
		}
		MouseOrbit mouseOrbit = SingleInstanceFindOnly<MouseOrbit>.Instance;
		if (mouseOrbit != null && mouseOrbit.targetType == MouseOrbit.TargetType.Block && mouseOrbit.targetInfo == this)
		{
			mouseOrbit.SoftResetCamTarget();
		}
		base.gameObject.SetActive(false);
		isDestroyed = true;
	}

	private void ExplosionForce()
	{
		if (!SimPhysics)
		{
			base.transform.position = NetBlock.Position;
			return;
		}
		explosionPos = GetCenter();
		hitColliders = Physics.OverlapSphere(explosionPos, radius);
		float num = 0.05f;
		Collider[] array = hitColliders;
		foreach (Collider collider in array)
		{
			if (!collider.attachedRigidbody || !(collider.attachedRigidbody != Rigidbody) || prevRigidbodies.Contains(collider.attachedRigidbody) || collider.attachedRigidbody.gameObject.layer == 20 || collider.attachedRigidbody.gameObject.layer == 22 || !(collider.attachedRigidbody.tag != "KeepConstraintsAlways"))
			{
				continue;
			}
			colAttachedRigidbody = collider.attachedRigidbody;
			num = ((!(colAttachedRigidbody.GetComponent<BlockBehaviour>() != null)) ? ((!colAttachedRigidbody.GetComponent<EnemyAISimple>()) ? num : 2.5f) : ((!colAttachedRigidbody.GetComponent<TimedRocket>()) ? 0.5f : 0.01f));
			float num2 = Mathf.Clamp(5f - base.transform.TransformPoint(Prefab.rayPosition).y, 0f, 5f) * 0.02f * num;
			colAttachedRigidbody.WakeUp();
			colAttachedRigidbody.constraints = RigidbodyConstraints.None;
			if (!float.IsNaN(realCharge))
			{
				colAttachedRigidbody.AddExplosionForce(power * realCharge * num, explosionPos, radius, upPower * num2);
			}
			prevRigidbodies.Add(colAttachedRigidbody);
			if (realCharge < 0.1f)
			{
				break;
			}
			int mask = 234;
			foreach (IExplosionEffect @interface in ReferenceMaster.GetInterfaces<IExplosionEffect>(colAttachedRigidbody.gameObject))
			{
				@interface.OnExplode(power * realCharge * num, upPower, 0f, explosionPos, radius, mask, base.InWater);
			}
		}
	}

	private void DustCraterQuad()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (dustCraterQuad == null)
		{
			Debug.LogWarning("TimedRocket doesn't have a dust crater quad!");
			return;
		}
		Vector3 vector = base.transform.TransformPoint(Prefab.rayPosition);
		float floorHeight = SingleInstanceFindOnly<AddPiece>.Instance.floorHeight;
		if (StatMaster.ShowExplosionDecals && vector.y < floorHeight + 5f)
		{
			dustCraterQuad.GetComponent<Renderer>().enabled = true;
			dustCraterQuad.parent = ReferenceMaster.physicsGoalInstance;
			dustCraterQuad.position = new Vector3(vector.x, floorHeight + 0.025f, vector.z);
			dustCraterQuad.forward = Vector3.up;
			dustCraterQuad.localEulerAngles = new Vector3(dustCraterQuad.localEulerAngles.x, dustCraterQuad.localEulerAngles.y, UnityEngine.Random.Range(0f, 360f));
		}
		else
		{
			dustCraterQuad.GetComponent<Renderer>().enabled = false;
		}
	}

	private void SendExplodeMessages(Rigidbody obj, float POWER)
	{
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (!noRigidbody)
		{
			Rigidbody.centerOfMass = Vector3.Scale(LocalCenter(), base.transform.localScale);
		}
		lastLoadedScale = base.transform.localScale;
	}

	public override void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock, bool playAnim = false)
	{
		if (type != ReloadAmmoType.All && type != ReloadAmmoType.Fuel)
		{
			return;
		}
		float num = units;
		float value = delaySlider.Value;
		float num2 = value - timeFlown;
		if (setAmmo)
		{
			if (eachBlock || num < value)
			{
				num2 = num;
				num = 0f;
			}
			else
			{
				num -= value;
				num2 = value;
			}
		}
		else
		{
			float num3 = 0f;
			if (eachBlock || num <= value - num2)
			{
				num2 += num;
				num3 = num;
				num = 0f;
			}
			else
			{
				num3 = value - num2;
				num -= num3;
				num2 = value;
			}
		}
		units = Mathf.RoundToInt(num);
		timeFlown -= num2;
		if (timeFlown < 0f)
		{
			timeFlown = 0f;
		}
		if (hasFired && hasExploded)
		{
			hasExploded = false;
			hasFired = false;
		}
	}
}
