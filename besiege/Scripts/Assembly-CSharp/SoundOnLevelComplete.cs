using UnityEngine;

public class SoundOnLevelComplete : MonoBehaviour
{
	public AudioSource sfx;

	public bool hasPlayed;

	private void Update()
	{
		if (!hasPlayed && WinCondition.hasWon)
		{
			hasPlayed = true;
			sfx.Play();
		}
	}
}
