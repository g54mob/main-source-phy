using UnityEngine;
using UnityEngine.Playables;

public class ScalerAsset : PlayableAsset
{
	public ScalarBehaviour template;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<ScalarBehaviour>.Create(graph, template);
	}
}
