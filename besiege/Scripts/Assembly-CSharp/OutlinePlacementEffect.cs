using UnityEngine;

public class OutlinePlacementEffect : MonoBehaviour
{
	public RandomSoundController[] soundControllers;

	public void PlaySound()
	{
		if (soundControllers != null)
		{
			for (int i = 0; i < soundControllers.Length; i++)
			{
				StartCoroutine(soundControllers[i].PlayAudio(0f));
			}
		}
	}
}
