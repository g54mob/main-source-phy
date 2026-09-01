using UnityEngine;

public class SimAudioRandomPlay : MonoBehaviour
{
	public float m_MinReplaySeconds;

	public float m_MaxReplaySeconds;

	public string m_AudioGroup;

	private float m_CountdownTimer;

	private void Start()
	{
		m_CountdownTimer = GetRandomSecondsUntilNextPlay();
	}

	private void Update()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			m_CountdownTimer -= Time.unscaledDeltaTime;
			if (m_CountdownTimer < 0f)
			{
				SimAudio.Play(m_AudioGroup, base.transform.position, useSimPitch: false);
				m_CountdownTimer = GetRandomSecondsUntilNextPlay();
			}
		}
	}

	private float GetRandomSecondsUntilNextPlay()
	{
		return Random.Range(m_MinReplaySeconds, m_MaxReplaySeconds);
	}
}
