using UnityEngine;

public class LoopHandler : MonoBehaviour
{
	private AudioSource source;

	private float startPitch = 1f;

	private float defaltDoppler = 1f;

	private void Start()
	{
		source = GetComponent<AudioSource>();
		if ((bool)source)
		{
			startPitch = source.pitch;
			defaltDoppler = source.dopplerLevel;
		}
	}

	private void Update()
	{
		if ((bool)source)
		{
			source.pitch = startPitch * Time.timeScale;
			source.dopplerLevel = defaltDoppler * Time.timeScale;
		}
	}
}
