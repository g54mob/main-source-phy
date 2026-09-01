using System;
using System.Collections;
using UnityEngine;

public class BellRing : MonoBehaviour
{
	public Rigidbody OuterBell;

	public float minForce = 50f;

	public AudioSource audioSource;

	public float timeOfRing = 4f;

	public Light[] Lights;

	public GameObject RefractionSphere;

	public GameObject BellRingAudio;

	public ParticleSystem[] particles;

	public ParticleSystem.EmissionModule[] em;

	public float lightMax;

	public float particleRateMax;

	public bool haveRung;

	public float timeSinceLast;

	public Action onTrigger;

	private void Awake()
	{
		em = new ParticleSystem.EmissionModule[Lights.Length];
		for (int i = 0; i < Lights.Length; i++)
		{
			em[i] = particles[i].emission;
		}
	}

	private void OnEnable()
	{
		if (!StatMaster.levelSimulating)
		{
			for (int i = 0; i < Lights.Length; i++)
			{
				Lights[i].enabled = false;
				em[i].enabled = false;
			}
			return;
		}
		for (int j = 0; j < Lights.Length; j++)
		{
			Lights[j].enabled = true;
			em[j].enabled = true;
		}
		if (audioSource == null)
		{
			Debug.LogError("Bell does not have an audioSource");
		}
		LightInit();
	}

	public void Update()
	{
		if (!WinCondition.hasWon && haveRung)
		{
			timeSinceLast += Time.deltaTime;
			if (timeSinceLast >= timeOfRing)
			{
				haveRung = false;
				WinCondition.currentObjsCompleted--;
				timeSinceLast = 0f;
			}
		}
	}

	public void OnCollisionEnter(Collision other)
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		Rigidbody rigidbody = other.rigidbody;
		if (rigidbody != null && rigidbody == OuterBell && other.relativeVelocity.sqrMagnitude > minForce)
		{
			if (!haveRung)
			{
				haveRung = true;
				WinCondition.currentObjsCompleted++;
				StartCoroutine(FadeLights());
			}
			else
			{
				timeSinceLast = 0f;
			}
			if (onTrigger != null)
			{
				onTrigger();
			}
			PlayRippel(other.contacts[0].point);
		}
	}

	private void PlayRippel(Vector3 point)
	{
		UnityEngine.Object.Instantiate(RefractionSphere, point, Quaternion.identity);
		UnityEngine.Object.Instantiate(BellRingAudio, point, Quaternion.identity);
	}

	private void LightInit()
	{
		for (int i = 0; i < Lights.Length; i++)
		{
			Lights[i].intensity = 0f;
			em[i].rate = new ParticleSystem.MinMaxCurve(0f);
		}
	}

	private IEnumerator FadeLights()
	{
		float ratio = 0f;
		while (haveRung)
		{
			float timeRatio = timeSinceLast / timeOfRing;
			ratio = Mathf.Clamp01(1f - timeRatio * timeRatio);
			for (int i = 0; i < Lights.Length; i++)
			{
				if (WinCondition.hasWon)
				{
					Lights[i].intensity = lightMax;
					em[i].rate = new ParticleSystem.MinMaxCurve(particleRateMax);
				}
				else
				{
					Lights[i].intensity = ratio * lightMax;
					em[i].rate = new ParticleSystem.MinMaxCurve(ratio * particleRateMax);
				}
			}
			if (WinCondition.hasWon || !StatMaster.levelSimulating)
			{
				break;
			}
			yield return null;
		}
	}
}
