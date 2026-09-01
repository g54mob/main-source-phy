using UnityEngine;

public class HarpoonLoop : MonoBehaviour
{
	private AudioSource audio;

	private PullStick pullStick;

	public float m = 0.5f;

	private float c;

	private void Start()
	{
		pullStick = GetComponent<PullStick>();
		audio = GetComponent<AudioSource>();
		audio.volume = 0f;
	}

	private void Update()
	{
		float num = 0f;
		c += Time.deltaTime;
		if (c < 3f)
		{
			num = pullStick.forceAmount;
		}
		audio.volume = Mathf.Lerp(audio.volume, num * m, Time.deltaTime * 5f);
	}
}
