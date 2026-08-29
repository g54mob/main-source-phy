using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GLTFast
{
	[RequireComponent(typeof(Animator))]
	internal class AnimationPlayableComponent : MonoBehaviour
	{
		internal static readonly string k_NotInitializedMessage = "AnimationPlayableComponent was not initialized.";

		internal static readonly string k_InvalidGraphMessage = "PlayableGraph must still be valid before destroying.";

		[SerializeField]
		private AnimationClip[] m_Clips;

		private PlayableGraph m_PlayableGraph;

		public Playable? Playable { get; private set; }

		private void Start()
		{
		}

		private void OnDestroy()
		{
			m_PlayableGraph.Destroy();
		}

		public void Init(AnimationClip[] clips, bool autoSequence)
		{
			m_Clips = clips;
			Animator component = GetComponent<Animator>();
			m_PlayableGraph = PlayableGraph.Create("GltfPlayableGraph");
			m_PlayableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
			ScriptPlayable<AnimationLoopPlayable> scriptPlayable = ScriptPlayable<AnimationLoopPlayable>.Create(m_PlayableGraph);
			scriptPlayable.GetBehaviour().Init(scriptPlayable, m_PlayableGraph, autoSequence, m_Clips);
			AnimationPlayableOutput.Create(m_PlayableGraph, "GltfAnimationPlayableOutput", component).SetSourcePlayable(scriptPlayable, 0);
			Playable = scriptPlayable;
		}
	}
}
