using UnityEngine;

public class DisableAudioListener : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Audio listeners to disable in startup.")]
	protected AudioListener[] audioListeners;

	private void Awake()
	{
		base.enabled = false;
		if (audioListeners == null || audioListeners.Length == 0)
		{
			return;
		}
		int i = 0;
		for (int num = audioListeners.Length; i < num; i++)
		{
			AudioListener audioListener = audioListeners[i];
			if (audioListener != null)
			{
				audioListener.enabled = false;
			}
		}
	}
}
