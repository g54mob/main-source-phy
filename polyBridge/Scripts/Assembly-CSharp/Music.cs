using DarkTonic.MasterAudio;
using UnityEngine;

public class Music
{
	private static PlaylistController m_PlaylistController;

	private static float m_PlayListVolumeAtPauseTime;

	private static bool m_PlayListFadeInComplete = true;

	public static void Init()
	{
		GameObject gameObject = GameObject.FindWithTag("PlaylistController");
		if (!gameObject)
		{
			Debug.LogWarningFormat("Could not find Object with PlaylistController tag, music will not play");
		}
		m_PlaylistController = gameObject.GetComponent<PlaylistController>();
	}

	public static void Start()
	{
		if ((bool)m_PlaylistController)
		{
			m_PlaylistController.StartPlaylist("soundtrack");
			m_PlaylistController.isShuffle = true;
		}
	}

	public static void Pause(float fadeTime)
	{
		if (m_PlayListFadeInComplete)
		{
			m_PlayListVolumeAtPauseTime = m_PlaylistController.PlaylistVolume;
			m_PlayListFadeInComplete = false;
		}
		m_PlaylistController.FadeToVolume(0f, fadeTime, OnFadeOutComplete);
	}

	public static void UnPause()
	{
		m_PlaylistController.UnpausePlaylist();
		m_PlaylistController.FadeToVolume(m_PlayListVolumeAtPauseTime, 4f, OnFadeInComplete);
	}

	private static void OnFadeOutComplete()
	{
		m_PlaylistController.PausePlaylist();
	}

	private static void OnFadeInComplete()
	{
		m_PlayListFadeInComplete = true;
	}
}
