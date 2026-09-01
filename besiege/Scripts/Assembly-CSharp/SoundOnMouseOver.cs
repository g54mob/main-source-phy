using UnityEngine;

public class SoundOnMouseOver : MonoBehaviour
{
	private void OnMouseEnter()
	{
		GetComponent<AudioSource>().Play();
	}
}
