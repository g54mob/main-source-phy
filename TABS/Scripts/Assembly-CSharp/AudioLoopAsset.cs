using UnityEngine;
using UnityEngine.Playables;

public class AudioLoopAsset : PlayableAsset
{
	public ExposedReference<AudioClip> m_loopClip;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		ScriptPlayable<AudioLoopBehaviour> scriptPlayable = ScriptPlayable<AudioLoopBehaviour>.Create(graph);
		scriptPlayable.GetBehaviour().m_loopClip = m_loopClip.Resolve(graph.GetResolver());
		return scriptPlayable;
	}
}
