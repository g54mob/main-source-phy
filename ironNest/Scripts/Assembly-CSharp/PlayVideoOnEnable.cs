using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnEnable : MonoBehaviour
{
	[SerializeField]
	private VideoPlayer _videoPlayer;

	[SerializeField]
	private bool _alwaysPlayFromStart;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Reset()
	{
	}
}
