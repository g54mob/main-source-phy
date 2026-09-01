using UnityEngine;

public class TimePause : MonoBehaviour
{
	private float _resumeTimeScale = 1f;

	private void OnEnable()
	{
		Time.timeScale = 0f;
	}

	private void OnDisable()
	{
		Time.timeScale = _resumeTimeScale;
	}
}
