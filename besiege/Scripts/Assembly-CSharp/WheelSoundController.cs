using UnityEngine;

[AddComponentMenu("Blocks/WheelSoundController")]
public class WheelSoundController : MonoBehaviour
{
	public static int activeWheels;

	private AudioSource audioSource;

	private Machine machine;

	private void Start()
	{
		machine = GetComponentInParent<Machine>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!(machine == null))
		{
			if (machine.isSimulating && !audioSource.isPlaying)
			{
				audioSource.Play();
			}
			else if (!machine.isSimulating && audioSource.isPlaying)
			{
				audioSource.Stop();
				activeWheels = 0;
			}
		}
	}
}
