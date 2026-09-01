using UnityEngine;

public class PlayLoopWhenObjectActive : MonoBehaviour
{
	public float lerpSpeed = 2f;

	private float startVolume = 1f;

	public GameObject target;

	private AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
		startVolume = source.volume;
		source.volume = 0f;
	}

	private void Update()
	{
		if ((bool)target)
		{
			if (target.activeSelf)
			{
				source.volume = Mathf.Lerp(source.volume, startVolume, lerpSpeed * Time.deltaTime);
			}
			else
			{
				source.volume = Mathf.Lerp(source.volume, 0f, lerpSpeed * Time.deltaTime);
			}
		}
	}
}
