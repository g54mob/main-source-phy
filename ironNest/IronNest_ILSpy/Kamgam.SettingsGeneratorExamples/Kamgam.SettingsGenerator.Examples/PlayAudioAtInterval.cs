using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class PlayAudioAtInterval : MonoBehaviour
{
	public float Interval = 2f;

	public AudioSource Source;

	protected float _timer;

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if ((_timer = deltaTime + _timer) > Interval)
		{
			_timer = 0f;
			Source.Play();
		}
	}
}
