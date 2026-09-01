using System.Collections;
using System.Linq;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
	public uint seed;

	public Transform explosionObj;

	public Renderer ripple;

	public AnimationCurve rippleAcceleration = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public AnimationCurve rippleFade = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public ParticleSystem[] particles;

	public WaterZone waterZone;

	public Vector3 maxSize;

	public float explosionMoveUpSpeed = 5f;

	[Tooltip("Duration in seconds. Used twice: once for the growing, once for waiting.")]
	public float explosionGrowthSpeed = 5f;

	[Tooltip("Duration in seconds.")]
	public float explosionShrinkSpeed = 5f;

	public Vector3 scaleDelegate;

	public float smooth = 8f;

	public bool isExploding;

	public Renderer[] rendys;

	public Material[] materials;

	[Tooltip("Spawn heat.")]
	public float preHeat = 3f;

	[Tooltip("Target heat at the end of grow.")]
	public float idleHeat = 0.3f;

	[Tooltip("Target heat at the end of wait.")]
	public float startHeat = 1.2f;

	[Tooltip("Target heat at the end of shrink.")]
	public float finalHeat;

	public Light lighty;

	public Transform parentObj;

	public Transform dustCraterQuad;

	private float lightStartBright;

	public Vector3 startSize = Vector3.one;

	private Machine machine;

	private IEnumerator lerpScaleUpCoroutine;

	private IEnumerator lerpScaleDownCoroutine;

	private bool hasWaterZone;

	private bool hasLight;

	private bool hasDustQuad;

	private void Start()
	{
		if (particles == null || particles.Length <= 0)
		{
			particles = GetComponentsInChildren<ParticleSystem>().ToArray();
		}
		if (startSize == Vector3.zero)
		{
			startSize = Vector3.one;
		}
		hasDustQuad = dustCraterQuad != null;
		hasLight = lighty != null;
		if (hasLight)
		{
			lightStartBright = lighty.intensity;
			lighty.intensity = Mathf.Clamp(lightStartBright, 0f, 8f);
		}
		Vector3 vector = startSize;
		base.transform.localScale = vector;
		Vector3 vector2 = vector;
		float num = Mathf.Max(vector2.x, vector2.y, vector2.z);
		explosionMoveUpSpeed *= Mathf.Sqrt(num);
		explosionGrowthSpeed *= num;
		if (num > 1f)
		{
			AudioSource component = base.gameObject.GetComponent<AudioSource>();
			component.pitch = 1f / num;
		}
		materials = new Material[rendys.Length];
		for (int i = 0; i < rendys.Length; i++)
		{
			materials[i] = rendys[i].material;
		}
		if (WaterController.Exist)
		{
			hasWaterZone = waterZone != null;
		}
		Explode();
	}

	private void Update()
	{
		explosionObj.position = new Vector3(explosionObj.position.x, explosionObj.position.y + Time.deltaTime * explosionMoveUpSpeed, explosionObj.position.z);
		explosionObj.localScale = Vector3.Lerp(explosionObj.localScale, scaleDelegate, Time.deltaTime * smooth);
	}

	public void Explode()
	{
		Particles();
		DustCraterQuad();
		StartCoroutine(Explosion());
		if (ripple != null)
		{
			StartCoroutine(Shockwave());
		}
		if (WaterController.Exist && hasWaterZone)
		{
			float num = WaterController.waterTransformHeight + 1.5f;
			float num2 = num - waterZone.transform.position.y;
			Vector3 position = waterZone.transform.position;
			position.y = Mathf.Max(WaterController.waterTransformHeight, num + num2);
			waterZone.transform.position = position;
		}
	}

	private IEnumerator Shockwave()
	{
		ripple.transform.SetParent(base.transform.parent);
		MaterialPropertyBlock props = new MaterialPropertyBlock();
		ripple.GetPropertyBlock(props);
		float duration = explosionGrowthSpeed * 2f + explosionShrinkSpeed;
		Vector3 size = maxSize * (2f + explosionShrinkSpeed / explosionGrowthSpeed) / 3f;
		duration *= 0.65f;
		size *= 0.42f;
		float rippling = ripple.material.GetFloat("_Refraction");
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float pct = t / duration;
			float eva = rippleAcceleration.Evaluate(pct);
			float fade = rippleFade.Evaluate(pct);
			ripple.transform.localScale = Vector3.Lerp(startSize, size, eva);
			props.SetFloat("_Refraction", Mathf.Lerp(rippling, 0f, fade));
			ripple.SetPropertyBlock(props);
			yield return null;
		}
	}

	private void Particles()
	{
		if (particles == null)
		{
			return;
		}
		for (int i = 0; i < particles.Length; i++)
		{
			if (!(particles[i] == null))
			{
				particles[i].Stop();
				particles[i].randomSeed = ((seed != 0) ? seed : ((uint)Random.Range(0f, 9999999f)));
				particles[i].Play();
			}
		}
	}

	private float EaseOut(float pct)
	{
		return 0f - Mathf.Pow(pct - 1f, 2f) + 1f;
	}

	private IEnumerator Explosion()
	{
		isExploding = true;
		Vector3 dustTarget = Vector3.zero;
		if (hasDustQuad)
		{
			dustTarget = dustCraterQuad.localScale;
		}
		explosionObj.gameObject.SetActive(true);
		explosionObj.localScale = startSize;
		float delegateHeat = 0f;
		float rand = Random.Range(0f, 0.1f);
		explosionGrowthSpeed += rand;
		for (float t = 0f; t < explosionGrowthSpeed; t += Time.deltaTime)
		{
			float pct = t / explosionGrowthSpeed;
			scaleDelegate = Vector3.Lerp(startSize, maxSize, pct);
			if (hasDustQuad)
			{
				dustCraterQuad.localScale = Vector3.Lerp(Vector3.one, dustTarget, EaseOut(pct * 0.5f));
			}
			delegateHeat = Mathf.Lerp(preHeat, idleHeat, pct);
			for (int i = 0; i < materials.Length; i++)
			{
				materials[i].SetFloat("_Heat", delegateHeat);
			}
			if (WaterController.Exist && hasWaterZone)
			{
				waterZone.Pct = pct;
			}
			yield return null;
		}
		for (float t2 = 0f; t2 < explosionGrowthSpeed; t2 += Time.deltaTime)
		{
			float pct2 = t2 / explosionGrowthSpeed;
			if (hasDustQuad)
			{
				dustCraterQuad.localScale = Vector3.Lerp(Vector3.one, dustTarget, EaseOut(0.5f + pct2 * 0.5f));
			}
			delegateHeat = Mathf.Lerp(idleHeat, startHeat, Mathf.Pow(pct2, 3f));
			for (int j = 0; j < materials.Length; j++)
			{
				materials[j].SetFloat("_Heat", delegateHeat);
			}
			if (WaterController.Exist && hasWaterZone)
			{
				waterZone.Pct = 1f;
			}
			yield return null;
		}
		for (float t3 = 0f; t3 < explosionShrinkSpeed; t3 += Time.deltaTime)
		{
			float pct3 = t3 / explosionShrinkSpeed;
			scaleDelegate = Vector3.Lerp(maxSize, new Vector3(0.01f, 0.01f, 0.01f), pct3);
			if (hasLight)
			{
				lighty.intensity = Mathf.Clamp(Mathf.Lerp(lightStartBright, 0f, pct3), 0f, 8f);
			}
			delegateHeat = Mathf.Lerp(startHeat, finalHeat, pct3);
			for (int k = 0; k < materials.Length; k++)
			{
				materials[k].SetFloat("_Heat", delegateHeat);
			}
			if (WaterController.Exist && hasWaterZone)
			{
				waterZone.Pct = 1f - pct3;
			}
			yield return null;
		}
		rand = Random.Range(0.5f, 1.6f);
		yield return new WaitForSeconds(rand);
		isExploding = false;
		if (WaterController.Exist && hasWaterZone)
		{
			Object.Destroy(parentObj.gameObject, 4f);
		}
		else
		{
			Object.Destroy(parentObj.gameObject);
		}
	}

	private void DustCraterQuad()
	{
		if (StatMaster.levelSimulating && hasDustQuad)
		{
			Vector3 position = base.transform.position;
			float floorHeight = SingleInstanceFindOnly<AddPiece>.Instance.floorHeight;
			if (StatMaster.ShowExplosionDecals && position.y < floorHeight + 5f)
			{
				dustCraterQuad.parent = ReferenceMaster.physicsGoalInstance;
				dustCraterQuad.position = new Vector3(position.x, floorHeight + 0.025f, position.z);
				dustCraterQuad.forward = Vector3.up;
				dustCraterQuad.localEulerAngles = new Vector3(dustCraterQuad.localEulerAngles.x, dustCraterQuad.localEulerAngles.y, Random.Range(0f, 360f));
			}
			else
			{
				dustCraterQuad.GetComponent<Renderer>().enabled = false;
			}
		}
	}
}
