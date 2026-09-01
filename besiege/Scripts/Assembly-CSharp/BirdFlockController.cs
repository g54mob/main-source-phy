using System.Collections;
using UnityEngine;

public class BirdFlockController : MonoBehaviour
{
	public Transform flockMiddle;

	public Transform flockHolder;

	public float flockRotateSpeed = 10f;

	public AudioSource flockSound;

	public float sfxFadeOutDuration = 6f;

	private float flockSfxStartVol;

	private bool flockSoundActive = true;

	private float currentCheckTime;

	private float checkInterval = 1f;

	private void Start()
	{
		flockSfxStartVol = flockSound.volume;
	}

	private void Update()
	{
		flockMiddle.Rotate(Vector3.up * Time.deltaTime * flockRotateSpeed);
		if (!flockSoundActive)
		{
			return;
		}
		currentCheckTime += Time.deltaTime;
		if (currentCheckTime > checkInterval)
		{
			if ((float)base.transform.childCount <= 5f)
			{
				flockSoundActive = false;
				StartCoroutine(LerpSoundOut(flockSfxStartVol));
			}
			currentCheckTime = 0f;
		}
	}

	private IEnumerator LerpSoundOut(float startVol)
	{
		float cTime = 0f;
		float rate = 1f / sfxFadeOutDuration;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			flockSound.volume = Mathf.Lerp(startVol, 0f, cTime);
			yield return null;
		}
	}
}
