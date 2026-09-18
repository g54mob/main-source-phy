using System.Collections;
using UnityEngine;

public class AudioEnablePlayer : MonoBehaviour
{
	[SerializeField]
	private AudioClipTypes audioClipType;

	private void OnEnable()
	{
		StartCoroutine(PlayAudio());
	}

	private IEnumerator PlayAudio()
	{
		yield return new WaitForEndOfFrame();
		Singleton<AudioManager>.Instance.PlayClip(audioClipType, base.transform.position);
	}
}
