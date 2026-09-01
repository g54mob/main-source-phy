using System.Collections;
using UnityEngine;

public class ObjectPlaceAnimation : MonoBehaviour
{
	public Renderer glowEffect;

	public float lerpSpeed = 1f;

	public float maxAlpha = 1f;

	public ParticleSystem[] particles;

	public ParticleSystem stoneParticle;

	public ParticleSystem virtualParticle;

	public ParticleSystem cloudParticle;

	public float duration = 5f;

	private float time;

	private Vector3 size;

	private Color startCol;

	private bool flashing;

	public void DisableSound()
	{
		RandomSoundController[] componentsInChildren = base.gameObject.GetComponentsInChildren<RandomSoundController>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
	}

	public void Setup(LevelEntity entity)
	{
		LevelBoundingBox boundingBox = entity.behaviour.boundingBox;
		if (entity.behaviour.hasBoundingBox)
		{
			size = boundingBox.GetSize();
		}
		else
		{
			size = Vector3.one;
		}
		float num = (size.x + size.z) / 10f;
		float num2 = ((!(size.x < size.z)) ? size.x : size.z);
		Transform parent = glowEffect.transform.parent;
		parent.localScale = new Vector3(num2 / 1.5f, 25f, num2 / 1.5f);
		if (entity.behaviour.hasBoundingBox)
		{
			parent.position = boundingBox.transform.position;
		}
		else
		{
			parent.position = entity.transform.position;
		}
		ParticleSystem.ShapeModule shape = virtualParticle.shape;
		shape.box = size * 0.85f;
		shape = stoneParticle.shape;
		shape.radius = num;
		shape = cloudParticle.shape;
		shape.radius = num;
		short minCount = (short)Mathf.Clamp(num * 2f, 3f, 90f);
		short maxCount = (short)Mathf.Clamp(num * 4f, 6f, 100f);
		stoneParticle.emission.SetBursts(new ParticleSystem.Burst[1]
		{
			new ParticleSystem.Burst(0f, minCount, maxCount)
		});
		minCount = (short)Mathf.Clamp(num * 15f, 25f, 150f);
		maxCount = (short)Mathf.Clamp(num * 23f, 35f, 350f);
		virtualParticle.emission.SetBursts(new ParticleSystem.Burst[1]
		{
			new ParticleSystem.Burst(0f, minCount, maxCount)
		});
		ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = virtualParticle.velocityOverLifetime;
		velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(1f * size.y, Mathf.Clamp(5f * size.y, 15f, 30f));
		minCount = (short)Mathf.Clamp(num * 5f, 8f, 100f);
		maxCount = (short)Mathf.Clamp(num * 6f, 8f, 150f);
		cloudParticle.emission.SetBursts(new ParticleSystem.Burst[1]
		{
			new ParticleSystem.Burst(0f, minCount, maxCount)
		});
		cloudParticle.startSize *= num * 0.66f;
		virtualParticle.GetComponent<ParticleSystemRenderer>().material.mainTexture = entity.GetComponentInChildren<Renderer>().material.mainTexture;
		for (int i = 0; i < particles.Length; i++)
		{
			PlayParticles(particles[i]);
		}
		PlayParticles(virtualParticle);
		PlayParticles(stoneParticle);
		PlayParticles(cloudParticle);
		startCol = glowEffect.material.GetColor("_TintColor");
		if (!flashing)
		{
			StartCoroutine(Flash());
		}
	}

	private void PlayParticles(ParticleSystem p)
	{
		p.Stop();
		p.randomSeed = (uint)Random.Range(0, 9999999);
		p.Play();
	}

	private void Update()
	{
		time += Time.deltaTime;
		if (time > duration)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private IEnumerator Flash()
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		flashing = true;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			startCol.a = Mathf.Lerp(maxAlpha, 0f, cTime);
			glowEffect.material.SetColor("_TintColor", startCol);
			yield return null;
		}
		flashing = false;
	}
}
