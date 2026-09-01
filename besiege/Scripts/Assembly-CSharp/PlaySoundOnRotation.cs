using UnityEngine;

public class PlaySoundOnRotation : MonoBehaviour
{
	public float threshold = 0.15f;

	private float thesholdSqr;

	public float maxVolume = 0.4f;

	public AudioSource audioSource;

	public float maxAngularVelocity = 10f;

	public AnimationCurve curve;

	private Rigidbody rB;

	private float volumeLerpSpeed = 3f;

	private float fallOff;

	private void Start()
	{
		rB = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Awake()
	{
		thesholdSqr = threshold * threshold;
	}

	private void Update()
	{
		if (rB.angularVelocity.sqrMagnitude > thesholdSqr)
		{
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
			float volume = maxVolume * curve.Evaluate(Mathf.Clamp((rB.angularVelocity.magnitude - threshold) / maxAngularVelocity, 0f, 1f));
			audioSource.volume = volume;
			fallOff = 0f;
		}
		else if (audioSource.isPlaying)
		{
			fallOff += Time.deltaTime * volumeLerpSpeed;
			audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Mathf.Clamp01(fallOff));
		}
	}
}
