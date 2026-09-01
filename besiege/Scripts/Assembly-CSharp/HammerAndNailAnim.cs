using System.Collections;
using UnityEngine;

public class HammerAndNailAnim : MonoBehaviour
{
	public Transform visObject;

	public float hammerStartSpeed;

	public float hammerEndSpeed;

	public float hammerReturnSpeed;

	public Transform hammerVisObj;

	public Renderer hammerRendy;

	public Transform hammerStartObj;

	public Transform hammerReboundObj;

	public Transform hammerMidObj;

	public Transform hammerEndObj;

	public Transform nailVisObj;

	public Renderer nailRendy;

	public Transform nailEndPos;

	public Transform nailStartPos;

	public float fadeInSpeed = 0.1f;

	public float fadeOutSpeed = 0.1f;

	public RandomSoundController soundController;

	public RandomSoundController woodHitAudioController;

	private Vector3 hitPos;

	public ParticleSystem particly;

	public ParticleSystem particly2;

	public ParticleSystem particly3;

	private IEnumerator lerpHammerCoroutine;

	private IEnumerator fadeInCoroutine;

	private IEnumerator fadeOutCoroutine;

	protected void Awake()
	{
		if (woodHitAudioController == null)
		{
			Transform transform = base.transform.FindChild("SoundEffectWoodCrunch");
			if (transform != null)
			{
				woodHitAudioController = transform.gameObject.GetComponent<RandomSoundController>();
			}
		}
	}

	private void Start()
	{
		if (Object.FindObjectOfType<WaterController>() != null)
		{
			Material material = particly.GetComponent<ParticleSystemRenderer>().material;
			material.renderQueue = 3002;
			material = particly2.GetComponent<ParticleSystemRenderer>().material;
			material.renderQueue = 3002;
			material = particly3.GetComponent<ParticleSystemRenderer>().material;
			material.renderQueue = 3002;
		}
	}

	public void Animate(Vector3 hit, Vector3 pos, Vector3 fwd)
	{
		hitPos = hit;
		base.transform.position = pos;
		base.transform.forward = -fwd;
		StartCoroutine(AnimateHammer());
	}

	private IEnumerator AnimateHammer()
	{
		if ((bool)woodHitAudioController)
		{
			woodHitAudioController.Stop();
			woodHitAudioController.Play(false);
		}
		visObject.gameObject.SetActive(true);
		if (lerpHammerCoroutine != null)
		{
			StopCoroutine(lerpHammerCoroutine);
		}
		if (fadeInCoroutine != null)
		{
			StopCoroutine(fadeInCoroutine);
		}
		if (fadeOutCoroutine != null)
		{
			StopCoroutine(fadeOutCoroutine);
		}
		hammerVisObj.localPosition = hammerStartObj.localPosition;
		hammerVisObj.localRotation = hammerStartObj.localRotation;
		nailVisObj.localPosition = nailStartPos.localPosition;
		particly2.transform.position = hitPos;
		particly2.Play();
		particly3.transform.position = hitPos;
		particly3.Play();
		particly.transform.position = hitPos;
		particly.Play();
		fadeInCoroutine = FadeIn();
		StartCoroutine(fadeInCoroutine);
		lerpHammerCoroutine = LerpHammer(0);
		yield return StartCoroutine(lerpHammerCoroutine);
		lerpHammerCoroutine = LerpHammer(1);
		yield return StartCoroutine(lerpHammerCoroutine);
		lerpHammerCoroutine = LerpHammer(2);
		yield return StartCoroutine(lerpHammerCoroutine);
		fadeOutCoroutine = FadeOut();
		yield return StartCoroutine(fadeOutCoroutine);
		visObject.gameObject.SetActive(false);
	}

	private IEnumerator LerpHammer(int phase)
	{
		float cTime = 0f;
		float rate = 1f;
		switch (phase)
		{
		case 0:
			rate /= hammerStartSpeed;
			break;
		case 1:
			rate /= hammerEndSpeed;
			break;
		case 2:
			rate /= hammerReturnSpeed;
			break;
		}
		bool hasAudioPlayed = false;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			switch (phase)
			{
			case 0:
				hammerVisObj.localPosition = Vector3.Lerp(hammerVisObj.localPosition, hammerMidObj.localPosition, cTime);
				hammerVisObj.localRotation = Quaternion.Lerp(hammerVisObj.localRotation, hammerMidObj.localRotation, cTime);
				break;
			case 1:
				hammerVisObj.localPosition = Vector3.Lerp(hammerVisObj.localPosition, hammerEndObj.localPosition, cTime);
				hammerVisObj.localRotation = Quaternion.Lerp(hammerVisObj.localRotation, hammerEndObj.localRotation, cTime);
				if (cTime > 0.7f)
				{
					if (!hasAudioPlayed)
					{
						soundController.Stop();
						soundController.Play(false);
						hasAudioPlayed = true;
					}
					nailVisObj.localPosition = Vector3.Lerp(nailVisObj.localPosition, nailEndPos.localPosition, (cTime - 0.7f) * 4f);
				}
				break;
			case 2:
				hammerVisObj.localPosition = Vector3.Lerp(hammerVisObj.localPosition, hammerReboundObj.localPosition, cTime);
				hammerVisObj.localRotation = Quaternion.Lerp(hammerVisObj.localRotation, hammerReboundObj.localRotation, cTime);
				break;
			}
			yield return null;
		}
	}

	private IEnumerator FadeIn()
	{
		float cTime = 0f;
		float rate = 1f / fadeInSpeed;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			hammerRendy.material.color = new Color(hammerRendy.material.color.r, hammerRendy.material.color.g, hammerRendy.material.color.b, cTime);
			nailRendy.material.color = new Color(nailRendy.material.color.r, nailRendy.material.color.g, nailRendy.material.color.b, cTime);
			yield return null;
		}
	}

	private IEnumerator FadeOut()
	{
		float cTime = 0f;
		float rate = 1f / fadeOutSpeed;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			float currentAlpha = Mathf.Lerp(1f, 0f, cTime);
			hammerRendy.material.color = new Color(hammerRendy.material.color.r, hammerRendy.material.color.g, hammerRendy.material.color.b, currentAlpha);
			nailRendy.material.color = new Color(nailRendy.material.color.r, nailRendy.material.color.g, nailRendy.material.color.b, currentAlpha);
			yield return null;
		}
	}
}
