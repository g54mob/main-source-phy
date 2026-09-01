using UnityEngine;
using UnityEngine.Playables;

public class CaveMaterialAnimatorAsset : PlayableAsset
{
	public CaveMaterialAnimatorBehaviour template;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<CaveMaterialAnimatorBehaviour>.Create(graph, template);
	}
}
