using UnityEngine;
using UnityEngine.Playables;

public class AudioLoopBehaviour : PlayableBehaviour
{
	public AudioClip m_loopClip;

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		AudioLoopSpawner audioLoopSpawner = playerData as AudioLoopSpawner;
		if (audioLoopSpawner != null)
		{
			audioLoopSpawner.StartLoop(m_loopClip);
		}
	}
}
