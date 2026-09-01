using System.Collections;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
	public string soundObjectName = "MUSIC";

	public float volumeToBe = 1f;

	public float lerpDuration = 1f;

	private AudioSource soundObj;

	private void Awake()
	{
		GameObject gameObject = GameObject.Find(soundObjectName);
		if (gameObject != null)
		{
			soundObj = gameObject.GetComponent<AudioSource>();
			StartCoroutine(LerpVolume());
		}
	}

	private IEnumerator LerpVolume()
	{
		float cTime = 0f;
		float rate = 1f / lerpDuration;
		float startVol = soundObj.volume;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			soundObj.volume = Mathf.Lerp(startVol, volumeToBe, cTime);
			yield return null;
		}
	}
}
