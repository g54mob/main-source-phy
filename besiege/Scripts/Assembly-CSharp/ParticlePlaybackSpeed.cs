using UnityEngine;

public class ParticlePlaybackSpeed : MonoBehaviour
{
	public float speed = 1f;

	private void Start()
	{
		GetComponent<ParticleSystem>().playbackSpeed = speed;
	}
}
