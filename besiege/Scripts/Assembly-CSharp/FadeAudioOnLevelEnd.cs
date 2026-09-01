using UnityEngine;

public class FadeAudioOnLevelEnd : MonoBehaviour
{
	public FadeAudio fadeCode;

	private void Start()
	{
		GameObject.Find("FADE").GetComponent<FadeScreen>().fadeAudioCode.Add(fadeCode);
	}
}
