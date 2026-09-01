using UnityEngine;

public class SoundOnRotate : MonoBehaviour
{
	public Rigidbody rigid;

	public float maxVolume = 0.4f;

	public AudioSource soundfx;

	public float volumeScaler;

	public float lerpSpeed = 3f;

	public void Update()
	{
		soundfx.volume = Mathf.Lerp(soundfx.volume, Mathf.Clamp(rigid.angularVelocity.sqrMagnitude * volumeScaler, 0f, maxVolume), Time.deltaTime * lerpSpeed);
	}
}
