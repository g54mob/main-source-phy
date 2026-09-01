using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GeneralParticleSystem : MonoBehaviour
{
	public GameObject particleObject;

	public bool useTimeScale;

	private ObjectPool particlePool;

	public float randomXPos;

	public float randomYPos;

	public float rate = 10f;

	public bool playOnAwake = true;

	public bool loop;

	public float duration = 2f;

	public AnimationCurve sizeMultiplierOverTime = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	public AnimationCurve emissionMultiplierOverTime = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	public ObjectParticle particleSettings;

	public float simulationSpeed = 1f;

	[HideInInspector]
	public float simulationSpeedMultiplier = 1f;

	public float saturationMultiplier = 1f;

	public UnityEvent startEvent;

	public float startEventDelay;

	public UnityEvent endEvent;

	public float endEventDelay;

	private float sizeOverTimeAnimationCurveLength;

	private float sizeMultiplierOverTimeAnimationCurveLength;

	private float alphaOverTimeAnimationCurveLength;

	private float emissionOverTimeAnimationCurveLength;

	private bool inited;

	private Coroutine emissionLoop;

	private float lastEmissionTime;

	private bool isPlaying;

	private void Start()
	{
		if (playOnAwake)
		{
			Play();
		}
	}

	private void Init()
	{
		if (!inited)
		{
			inited = true;
			if (particleSettings.sizeOverTime.keys.Length > 1)
			{
				sizeOverTimeAnimationCurveLength = particleSettings.sizeOverTime[particleSettings.sizeOverTime.length - 1].time - particleSettings.sizeOverTime[0].time;
			}
			if (particleSettings.alphaOverTime.keys.Length > 1)
			{
				alphaOverTimeAnimationCurveLength = particleSettings.alphaOverTime[particleSettings.alphaOverTime.length - 1].time - particleSettings.alphaOverTime[0].time;
			}
			if (sizeMultiplierOverTime.keys.Length > 1)
			{
				sizeMultiplierOverTimeAnimationCurveLength = sizeMultiplierOverTime[sizeMultiplierOverTime.length - 1].time - sizeMultiplierOverTime[0].time;
			}
			if (emissionMultiplierOverTime.keys.Length > 1)
			{
				emissionOverTimeAnimationCurveLength = emissionMultiplierOverTime[emissionMultiplierOverTime.length - 1].time - emissionMultiplierOverTime[0].time;
			}
			particleObject.SetActive(value: false);
			particlePool = new ObjectPool(particleObject, 100, base.transform);
		}
	}

	public void Play()
	{
		Init();
		if (!isPlaying)
		{
			emissionLoop = StartCoroutine(DoPlay());
		}
	}

	private void OnDisable()
	{
		Stop();
	}

	public void Stop()
	{
		DisableAllParticles();
		if (emissionLoop != null)
		{
			StopCoroutine(emissionLoop);
		}
	}

	private void DisableAllParticles()
	{
		if (base.transform.childCount > 0)
		{
			for (int i = 0; i < base.transform.GetChild(0).childCount; i++)
			{
				base.transform.GetChild(0).GetChild(i).gameObject.SetActive(value: false);
			}
		}
	}

	private IEnumerator DoPlay()
	{
		isPlaying = true;
		if (startEvent != null)
		{
			if (startEventDelay != 0f)
			{
				StartCoroutine(DelayEvent(startEvent, startEventDelay));
			}
			else
			{
				startEvent.Invoke();
			}
		}
		float counter = 0f;
		while (counter < duration)
		{
			CheckIfShouldEmit(counter / duration);
			counter += (useTimeScale ? Time.deltaTime : Time.unscaledDeltaTime) * (simulationSpeed * simulationSpeedMultiplier);
			yield return null;
		}
		isPlaying = false;
		if (loop)
		{
			Play();
		}
		else if (endEvent != null)
		{
			if (endEventDelay != 0f)
			{
				StartCoroutine(DelayEvent(endEvent, endEventDelay));
			}
			else
			{
				endEvent.Invoke();
			}
		}
	}

	private void CheckIfShouldEmit(float currentAnimationTime)
	{
		if ((useTimeScale ? Time.time : Time.unscaledTime) > lastEmissionTime + 1f / rate / (simulationSpeed * simulationSpeedMultiplier) / emissionMultiplierOverTime.Evaluate(currentAnimationTime * emissionOverTimeAnimationCurveLength) * Time.timeScale)
		{
			StartCoroutine(DoPlarticleLife(currentAnimationTime));
			lastEmissionTime = Time.time;
		}
	}

	private IEnumerator DoPlarticleLife(float currentAnimationTime)
	{
		GameObject spawned = particlePool.GetObject();
		float counter = 0f;
		float t = particleSettings.lifetime;
		Vector3 startSize = spawned.transform.localScale;
		Vector3 modifiedStartSize = particleSettings.size * sizeMultiplierOverTime.Evaluate(currentAnimationTime * sizeMultiplierOverTimeAnimationCurveLength) * spawned.transform.localScale;
		Image img = spawned.GetComponent<Image>();
		Color startColor = Color.magenta;
		if ((bool)img)
		{
			startColor = img.color;
		}
		if ((bool)img)
		{
			float value = Random.value;
			if (particleSettings.color != Color.magenta)
			{
				img.color = particleSettings.color;
			}
			if (particleSettings.randomColor != Color.magenta)
			{
				img.color = Color.Lerp(img.color, particleSettings.randomColor, value);
			}
			if (!particleSettings.singleRandomValueColor)
			{
				value = Random.value;
			}
			if (particleSettings.randomAddedColor != Color.black)
			{
				img.color += Color.Lerp(Color.black, particleSettings.randomAddedColor, value);
			}
			if (!particleSettings.singleRandomValueColor)
			{
				value = Random.value;
			}
			if (particleSettings.randomAddedSaturation != 0f || saturationMultiplier != 1f)
			{
				Color.RGBToHSV(img.color, out var H, out var S, out var V);
				S += value * particleSettings.randomAddedSaturation;
				S *= saturationMultiplier;
				img.color = Color.HSVToRGB(H, S, V);
			}
		}
		spawned.transform.Rotate(base.transform.forward * particleSettings.rotation, Space.World);
		spawned.transform.Rotate(base.transform.forward * Random.Range(0f - particleSettings.randomRotation, particleSettings.randomRotation), Space.World);
		spawned.transform.localPosition = Vector3.zero;
		spawned.transform.position += base.transform.up * Random.Range(0f - randomYPos, randomYPos);
		spawned.transform.position += base.transform.right * Random.Range(0f - randomXPos, randomXPos);
		spawned.transform.position += base.transform.forward * Random.Range(-0.1f, 0.1f);
		while (counter < t)
		{
			if (particleSettings.sizeOverTime.keys.Length > 1)
			{
				spawned.transform.localScale = modifiedStartSize * particleSettings.sizeOverTime.Evaluate(counter / t * sizeOverTimeAnimationCurveLength);
			}
			float num = particleSettings.alphaOverTime.Evaluate(counter / t * alphaOverTimeAnimationCurveLength);
			if ((bool)img && img.color.a != num)
			{
				img.color = new Color(img.color.r, img.color.g, img.color.b, num);
			}
			counter += (useTimeScale ? Time.deltaTime : Time.unscaledDeltaTime) * (simulationSpeed * simulationSpeedMultiplier);
			yield return null;
		}
		if ((bool)img)
		{
			img.color = startColor;
		}
		spawned.transform.localScale = startSize;
		particlePool.ReleaseObject(spawned);
	}

	private IEnumerator DelayEvent(UnityEvent e, float t)
	{
		yield return new WaitForSeconds(t);
		e?.Invoke();
	}
}
