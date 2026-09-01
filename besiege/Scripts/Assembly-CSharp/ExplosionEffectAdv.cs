using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectAdv : MonoBehaviour
{
	[Serializable]
	public class ShockwavePhase
	{
		public float duration;

		public float size = 10f;

		public AnimationCurve scaling;

		public AnimationCurve distortion;

		public AnimationCurve wrinkling;
	}

	[Serializable]
	public class WaterZonePhase
	{
		public float duration;

		public AnimationCurve pct;

		public AnimationCurve ramp;
	}

	[Serializable]
	public class ParticlePhase
	{
		[Serializable]
		public class Particle
		{
			public ParticleSystem particle;

			public float baseEmission;
		}

		public float duration;

		public Particle[] particles;

		public AnimationCurve emissionPct;
	}

	[Serializable]
	public class PyroPhase
	{
		public float duration;

		public float size = 5f;

		public AnimationCurve scaling;

		public AnimationCurve heat;
	}

	[Serializable]
	public class ForcePhase
	{
		public float duration;

		public float force = 5f;

		public float size = 5f;

		public AnimationCurve forceScale;

		public AnimationCurve scaling;
	}

	[Serializable]
	public class AudioPhase
	{
		public float duration;

		public float volume;

		public AudioClip sfx;
	}

	public ShockwavePhase[] shockwavePhases;

	public ParticlePhase[] particlePhases;

	public PyroPhase[] pyroPhases;

	public WaterZonePhase[] waterPhases;

	public ForcePhase[] forcePhases;

	public AudioPhase[] audioPhases;

	public bool lerpAirToWater = true;

	[Header("References")]
	public Transform fireball;

	public MeshRenderer shockwave;

	public WaterZone zone;

	public AudioSource underwaterAudio;

	public AudioSource surfaceAudio;

	public Light light;

	public int colliderAmount = 512;

	public ParticleSystem extraFoam;

	public Transform[] moveToWaterHeight;

	public ExplosionMat[] pyroclasticPufts = new ExplosionMat[0];

	protected MaterialPropertyBlock shockPB;

	private bool shockwaveDone;

	private bool particlesDone;

	private bool pyroDone;

	private bool forcesDone;

	private bool waterAnimDone;

	private float maxPower;

	private float maxRadius;

	private HashSet<Rigidbody> inExplosion = new HashSet<Rigidbody>();

	private HashSet<Rigidbody> hasBeenCalled = new HashSet<Rigidbody>();

	private Dictionary<Rigidbody, BasicInfo> getInfo = new Dictionary<Rigidbody, BasicInfo>();

	protected float DeltaTime
	{
		get
		{
			return Time.deltaTime;
		}
	}

	public void Start()
	{
		shockPB = new MaterialPropertyBlock();
		zone.Pct = 0f;
		shockwave.transform.localScale = Vector3.zero;
		fireball.localScale = Vector3.zero;
		float num = Mathf.Lerp(base.transform.position.y, WaterController.waterTransformHeight + 3.5f, 0.5f);
		if (base.transform.position.y > num - 15f)
		{
			if (base.transform.position.y < num - 5f)
			{
				extraFoam.Play();
			}
			for (int i = 0; i < moveToWaterHeight.Length; i++)
			{
				Vector3 position = moveToWaterHeight[i].position;
				position.y = num;
				moveToWaterHeight[i].position = position;
			}
		}
		for (int j = 0; j < pyroclasticPufts.Length; j++)
		{
			pyroclasticPufts[j].AssignMaterials();
			pyroclasticPufts[j].ren.enabled = true;
		}
		StartCoroutine(LerpWater());
		StartCoroutine(LerpShockwave());
		StartCoroutine(LerpParticles());
		StartCoroutine(LerpPyro());
		StartCoroutine(LerpForces());
		StartCoroutine(UpdateAudio());
		for (int k = 0; k < forcePhases.Length; k++)
		{
			if (maxPower < forcePhases[k].force)
			{
				maxPower = forcePhases[k].force;
			}
		}
		for (int l = 0; l < forcePhases.Length; l++)
		{
			if (maxRadius < forcePhases[l].size)
			{
				maxRadius = forcePhases[l].size;
			}
		}
	}

	private void LateUpdate()
	{
		float num = OptionsMaster.BesiegeConfig.PhysicsVolume * 0.01f * OptionsMaster.BesiegeConfig.SfxVolume * 0.01f;
		underwaterAudio.volume = num * ((!WaterFogController.overWater) ? 1f : 0f);
		surfaceAudio.volume = num * ((!WaterFogController.overWater) ? 0f : 1f);
		underwaterAudio.pitch = Time.timeScale;
		if (shockwaveDone && particlesDone && pyroDone && forcesDone && waterAnimDone && zone.Pct <= float.Epsilon)
		{
			UnityEngine.Object.Destroy(base.gameObject, 4f);
		}
	}

	protected IEnumerator LerpWater()
	{
		float size = Mathf.Max(base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z);
		if (size > 0f)
		{
			zone.baseValue *= size;
			zone.gradientSize *= size;
			zone.depthRange *= size;
		}
		for (int i = 0; i < waterPhases.Length; i++)
		{
			WaterZonePhase phase = waterPhases[i];
			for (float t = 0f; t < phase.duration; t += DeltaTime)
			{
				float pct = t / phase.duration;
				zone.Pct = phase.pct.Evaluate(pct);
				zone.Exponent = phase.ramp.Evaluate(pct);
				yield return null;
			}
		}
		waterAnimDone = true;
		zone.Pct = 0f;
	}

	protected IEnumerator LerpShockwave()
	{
		for (int i = 0; i < shockwavePhases.Length; i++)
		{
			ShockwavePhase phase = shockwavePhases[i];
			for (float t = 0f; t < phase.duration; t += DeltaTime)
			{
				float pct = t / phase.duration;
				shockwave.transform.localScale = phase.scaling.Evaluate(pct) * Vector3.one * phase.size;
				shockPB.SetFloat("_Refraction", phase.distortion.Evaluate(pct));
				shockPB.SetFloat("_Power", phase.wrinkling.Evaluate(pct));
				shockwave.SetPropertyBlock(shockPB);
				yield return null;
			}
		}
		shockwave.enabled = false;
		shockwaveDone = true;
	}

	protected IEnumerator LerpParticles()
	{
		for (int i = 0; i < particlePhases.Length; i++)
		{
			ParticlePhase phase = particlePhases[i];
			for (int j = 0; j < phase.particles.Length; j++)
			{
				phase.particles[j].particle.randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
				phase.particles[j].particle.Play();
			}
			for (float t = 0f; t < phase.duration; t += DeltaTime)
			{
				float pct = t / phase.duration;
				float rate = phase.emissionPct.Evaluate(pct);
				for (int k = 0; k < phase.particles.Length; k++)
				{
					ParticleSystem.EmissionModule e = phase.particles[k].particle.emission;
					e.rate = rate * phase.particles[k].baseEmission;
				}
				yield return null;
			}
			for (int l = 0; l < phase.particles.Length; l++)
			{
				phase.particles[l].particle.Stop();
			}
		}
		particlesDone = true;
	}

	protected IEnumerator LerpPyro()
	{
		for (int i = 0; i < pyroPhases.Length; i++)
		{
			PyroPhase phase = pyroPhases[i];
			Vector3 size = Vector3.one * phase.size;
			for (float t = 0f; t < phase.duration; t += DeltaTime)
			{
				float pct = t / phase.duration;
				float scale = phase.scaling.Evaluate(pct);
				float heat = phase.heat.Evaluate(pct);
				fireball.localScale = scale * size;
				for (int j = 0; j < pyroclasticPufts.Length; j++)
				{
					pyroclasticPufts[j].heat = heat;
				}
				light.intensity = heat * 16f;
				light.range = scale * 13f;
				yield return null;
			}
		}
		fireball.gameObject.SetActive(false);
		pyroDone = true;
	}

	protected IEnumerator LerpForces()
	{
		Vector3 pos = base.transform.position;
		LayerMask m = AddPiece.CreateLayerMask(new int[8] { 0, 12, 14, 15, 24, 25, 26, 28 });
		yield return new WaitForFixedUpdate();
		Collider[] cols = new Collider[colliderAmount];
		for (int i = 0; i < forcePhases.Length; i++)
		{
			ForcePhase phase = forcePhases[i];
			for (float t = 0f; t < phase.duration; t += DeltaTime)
			{
				float pct = t / phase.duration;
				float f = phase.forceScale.Evaluate(pct) * phase.force;
				float fa = ((!(f > 0f)) ? 0f : (phase.force * 0.25f));
				float r = phase.scaling.Evaluate(pct) * phase.size;
				int numColliders = Physics.OverlapSphereNonAlloc(pos, r, cols, m);
				for (int j = 0; j < numColliders; j++)
				{
					Rigidbody b = cols[j].attachedRigidbody;
					if (!object.ReferenceEquals(b, null))
					{
						BasicInfo info;
						if (!getInfo.TryGetValue(b, out info))
						{
							info = b.GetComponent<BasicInfo>();
							getInfo.Add(b, info);
						}
						float submerged = ((!object.ReferenceEquals(info, null)) ? info.submergedPercent : ((!WaterController.Exist || !WaterController.IsUnderwater(b.worldCenterOfMass)) ? 0f : 1f));
						if (lerpAirToWater)
						{
							float radius = Mathf.Lerp(0.5f * maxRadius + 0.5f * r, r, submerged);
							float force = Mathf.Lerp(fa, f, submerged);
							ExplodeBody(b, pos, force, 0f, radius);
						}
						else if (submerged > 0.9f)
						{
							ExplodeBody(b, pos, f, 0f, r);
						}
						else if (submerged > 0.1f)
						{
							ExplodeBody(b, pos, f, 0f, 0.5f * maxRadius + 0.5f * r);
						}
						else
						{
							ExplodeBody(b, pos, fa, 0f, 0.5f * maxRadius + 0.5f * r);
						}
					}
				}
				inExplosion.Clear();
				yield return new WaitForFixedUpdate();
			}
		}
		forcesDone = true;
		hasBeenCalled.Clear();
		getInfo.Clear();
	}

	protected IEnumerator UpdateAudio()
	{
		for (int i = 0; i < audioPhases.Length; i++)
		{
			AudioPhase phase = audioPhases[i];
			if ((bool)phase.sfx)
			{
				underwaterAudio.PlayOneShot(phase.sfx, phase.volume);
			}
			yield return new WaitForSeconds(phase.duration);
		}
	}

	protected void ExplodeBody(Rigidbody b, Vector3 pos, float f, float t, float r)
	{
		if (!inExplosion.Contains(b))
		{
			float num = ((WaterController.Exist && !(b.transform.position.y > WaterController.waterTransformHeight)) ? 0f : 1f);
			bool flag = hasBeenCalled.Contains(b);
			b.WakeUp();
			b.AddExplosionForce(f, pos, r, num, ForceMode.Force);
			inExplosion.Add(b);
			if (!flag)
			{
				Debug.DrawLine(b.worldCenterOfMass, pos, Color.red, 5f);
				ExplosionCallbacks(b.gameObject, pos, maxPower, num, t, maxRadius);
				hasBeenCalled.Add(b);
			}
		}
	}

	protected void ExplosionCallbacks(GameObject go, Vector3 pos, float f, float up, float t, float r)
	{
		int mask = 237;
		IEnumerable<IExplosionEffect> interfaces = ReferenceMaster.GetInterfaces<IExplosionEffect>(go);
		foreach (IExplosionEffect item in interfaces)
		{
			item.OnExplode(f, up, t, pos, r, mask, true);
		}
	}
}
