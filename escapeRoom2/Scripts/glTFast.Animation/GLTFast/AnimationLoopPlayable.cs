using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GLTFast
{
	internal class AnimationLoopPlayable : PlayableBehaviour
	{
		private int m_Index;

		private bool m_AutoSequence;

		internal AnimationMixerPlayable Mixer { get; private set; }

		internal float Time { get; set; }

		public void Init(Playable owner, PlayableGraph graph, bool autoSequence, params AnimationClip[] clips)
		{
			if (clips == null)
			{
				throw new ArgumentNullException("clips");
			}
			if (clips.Length == 0)
			{
				throw new ArgumentOutOfRangeException("clips");
			}
			m_AutoSequence = autoSequence;
			owner.SetInputCount(1);
			owner.SetInputWeight(0, 1f);
			Mixer = AnimationMixerPlayable.Create(graph, clips.Length);
			graph.Connect(Mixer, 0, owner, 0);
			for (int i = 0; i < clips.Length; i++)
			{
				graph.Connect(AnimationClipPlayable.Create(graph, clips[i]), 0, Mixer, i);
				Mixer.SetInputWeight(i, (i == 0) ? 1f : 0f);
			}
		}

		public override void PrepareFrame(Playable playable, FrameData info)
		{
			Time -= info.deltaTime;
			if (!(Time > 0f))
			{
				if (m_AutoSequence)
				{
					Mixer.SetInputWeight(m_Index, 0f);
					m_Index = ++m_Index % Mixer.GetInputCount();
					Mixer.SetInputWeight(m_Index, 1f);
				}
				AnimationClipPlayable playable2 = (AnimationClipPlayable)Mixer.GetInput(m_Index);
				playable2.SetTime(0.0);
				Time = playable2.GetAnimationClip().length;
			}
		}
	}
}
