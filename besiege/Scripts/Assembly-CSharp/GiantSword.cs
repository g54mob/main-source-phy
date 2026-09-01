using System.Collections;
using UnityEngine;

public class GiantSword : MonoBehaviour
{
	public Renderer rend;

	public Color emissCol;

	public bool lifted;

	public float liftedHeight = 5f;

	public Transform myTransform;

	public Renderer cursedRenderer;

	public AudioSource sfx;

	public ParticleSystem magicDustParticles;

	public ParticleSystem magicDustParticlesLooping;

	public Transform lightningBolts;

	public Transform spikes;

	public float spikeLerpSpeed = 0.5f;

	public float spikeRaisedPos = -9.17f;

	private Material material;

	private void Start()
	{
		material = rend.material;
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && !lifted && myTransform.position.y >= liftedHeight)
		{
			GiantSwordEffect();
			lifted = true;
		}
	}

	private void GiantSwordEffect()
	{
		sfx.Play();
		magicDustParticles.Play();
		magicDustParticlesLooping.Play();
		material.SetColor("_EmissCol", emissCol);
		StartCoroutine(Lightning());
		StartCoroutine(AnimateSpikes());
	}

	private IEnumerator Lightning()
	{
		for (int i = 0; i < lightningBolts.childCount; i++)
		{
			lightningBolts.GetChild(i).GetComponent<Renderer>().enabled = true;
			yield return new WaitForSeconds(0.08f);
			lightningBolts.GetChild(i).GetComponent<Renderer>().enabled = false;
		}
	}

	private IEnumerator AnimateSpikes()
	{
		spikes.gameObject.SetActive(true);
		for (int i = 0; i < spikes.childCount; i++)
		{
			spikes.GetChild(i).GetComponent<Renderer>().enabled = true;
			yield return StartCoroutine(LerpPositionY(spikes.GetChild(i)));
			yield return new WaitForSeconds(0.03f);
			spikes.GetChild(i).GetChild(0).GetComponent<ParticleSystem>()
				.Play();
		}
	}

	private IEnumerator LerpPositionY(Transform obj)
	{
		float cTime = 0f;
		float rate = 1f / (spikeLerpSpeed * Random.Range(0.6f, 1.2f));
		float startPos = obj.localPosition.y;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			obj.localPosition = new Vector3(obj.localPosition.x, Mathf.Lerp(startPos, spikeRaisedPos, cTime), obj.localPosition.z);
			yield return null;
		}
	}
}
