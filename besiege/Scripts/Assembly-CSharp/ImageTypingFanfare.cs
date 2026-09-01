using System.Collections;
using UnityEngine;

public class ImageTypingFanfare : MonoBehaviour
{
	public Texture2D[] images;

	public RandomSoundController randomSoundCode;

	public Renderer rend;

	public float timeBetweenLetters = 0.1f;

	private void Start()
	{
		Reset();
	}

	public void Reset()
	{
		rend.enabled = false;
	}

	public IEnumerator Type()
	{
		int i = 0;
		rend.enabled = true;
		while (i < images.Length)
		{
			randomSoundCode.Play();
			rend.material.mainTexture = images[i];
			i++;
			yield return new WaitForSeconds(timeBetweenLetters);
		}
	}
}
