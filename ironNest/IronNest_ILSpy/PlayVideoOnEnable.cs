using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnEnable : MonoBehaviour
{
	private VideoPlayer _videoPlayer;

	private bool _alwaysPlayFromStart;

	private void OnEnable()
	{
		if (_alwaysPlayFromStart)
		{
			_videoPlayer.Stop();
		}
		_videoPlayer.Play();
	}

	private void OnDisable()
	{
		_videoPlayer.Pause();
	}

	private void Reset()
	{
		if (_videoPlayer == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
			VideoPlayer videoPlayer = default(VideoPlayer);
			_videoPlayer = videoPlayer;
		}
	}
}
