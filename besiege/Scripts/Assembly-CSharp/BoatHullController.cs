using System.Collections;
using UnityEngine;

public class BoatHullController : MonoBehaviour
{
	[SerializeField]
	private AirshipMultiAI steeringScript;

	[SerializeField]
	private CanonNPCv2[] cannons;

	[SerializeField]
	private float minDensityGainPerHitOvertime = 0.2f;

	[SerializeField]
	private float maxDensityGainPerHitOvertime = 0.5f;

	[SerializeField]
	private int sustainableHits = 3;

	[SerializeField]
	private int maxSinkingDensityThreshold = 30;

	public Joint[] destroyOnBreak = new Joint[0];

	private float midDensityGain;

	private float frontDensityGain;

	private float backDensityGain;

	[HideInInspector]
	public bool isSinking;

	[SerializeField]
	private Rigidbody backBoat;

	[SerializeField]
	private Rigidbody frontBoat;

	[SerializeField]
	private Rigidbody midBoat;

	private BasicInfo backBI;

	private BasicInfo midBI;

	private BasicInfo frontBI;

	private int backTimesHit;

	private int frontTimesHit;

	private int midTimesHit;

	private int globalTimesHit;

	private Coroutine sinkingCor;

	[SerializeField]
	[Header("VFX")]
	private ParticleSystem impactParticles;

	private void Start()
	{
		backBI = backBoat.gameObject.GetComponent<BasicInfo>();
		frontBI = frontBoat.gameObject.GetComponent<BasicInfo>();
		midBI = midBoat.gameObject.GetComponent<BasicInfo>();
	}

	public void BreakHull(Rigidbody brokenRB)
	{
		float num = Random.Range(minDensityGainPerHitOvertime, maxDensityGainPerHitOvertime);
		if (brokenRB == backBoat)
		{
			backDensityGain = num * (float)sustainableHits;
			midDensityGain = num * (float)(sustainableHits * 2);
			frontDensityGain = num * (float)(sustainableHits * 2);
		}
		else if (brokenRB == midBoat)
		{
			backDensityGain = num * (float)(sustainableHits * 2);
			midDensityGain = num * (float)sustainableHits;
			frontDensityGain = num * (float)(sustainableHits * 2);
		}
		else if (brokenRB == frontBoat)
		{
			backDensityGain = num * (float)(sustainableHits * 2);
			midDensityGain = num * (float)(sustainableHits * 2);
			frontDensityGain = num * (float)sustainableHits;
		}
	}

	public void StartSinking()
	{
		if (sinkingCor == null)
		{
			Debug.Log("Sinking");
			sinkingCor = StartCoroutine(Sinking());
			isSinking = true;
			steeringScript.broken = true;
			for (int i = 0; i < cannons.Length; i++)
			{
				cannons[i].Broken = true;
			}
			for (int j = 0; j < destroyOnBreak.Length; j++)
			{
				Object.Destroy(destroyOnBreak[j]);
			}
		}
	}

	public void HullHit(Rigidbody hitRB, Vector3 position)
	{
		Debug.Log("Hit");
		float num = Random.Range(minDensityGainPerHitOvertime, maxDensityGainPerHitOvertime);
		impactParticles.transform.position = position;
		impactParticles.Play();
		if (hitRB == backBoat)
		{
			backDensityGain += num;
			backTimesHit++;
			globalTimesHit++;
		}
		else if (hitRB == midBoat)
		{
			midDensityGain += num;
			midTimesHit++;
			globalTimesHit++;
		}
		else if (hitRB == frontBoat)
		{
			frontDensityGain += num;
			globalTimesHit++;
			frontTimesHit++;
		}
		if (globalTimesHit > sustainableHits)
		{
			StartSinking();
		}
	}

	private IEnumerator Sinking()
	{
		float[] densityValues = new float[3] { backBI.density, frontBI.density, midBI.density };
		float currentMaxDensity = Mathf.Max(densityValues);
		while (currentMaxDensity < (float)maxSinkingDensityThreshold)
		{
			ProcessHit(backBI, backDensityGain * Time.deltaTime, backBoat);
			ProcessHit(midBI, midDensityGain * Time.deltaTime, midBoat);
			ProcessHit(frontBI, frontDensityGain * Time.deltaTime, frontBoat);
			yield return null;
		}
	}

	private void ProcessHit(BasicInfo basicInfo, float densityChange, Rigidbody rb)
	{
		basicInfo.density += densityChange;
		rb.mass += densityChange;
	}
}
