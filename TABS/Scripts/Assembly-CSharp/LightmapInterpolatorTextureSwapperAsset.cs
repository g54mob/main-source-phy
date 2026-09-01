using UnityEngine;
using UnityEngine.Playables;

public class LightmapInterpolatorTextureSwapperAsset : PlayableAsset
{
	public ExposedReference<Texture2D> m_texture01;

	public ExposedReference<Texture2D> m_texture02;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		ScriptPlayable<LightmapInterpolatorTextureSwapperBehaviour> scriptPlayable = ScriptPlayable<LightmapInterpolatorTextureSwapperBehaviour>.Create(graph);
		LightmapInterpolatorTextureSwapperBehaviour behaviour = scriptPlayable.GetBehaviour();
		behaviour.m_texture01 = m_texture01.Resolve(graph.GetResolver());
		behaviour.m_texture02 = m_texture02.Resolve(graph.GetResolver());
		return scriptPlayable;
	}
}
