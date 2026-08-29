using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class GaussianBlurVerticalMixerBehaviour : GaussianBlurMixerBehaviourBase
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			List<GaussianBlurBehaviourBase> list = new List<GaussianBlurBehaviourBase>();
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				GaussianBlurVerticalBehaviour behaviour = ((ScriptPlayable<GaussianBlurVerticalBehaviour>)playable.GetInput(i)).GetBehaviour();
				list.Add(behaviour);
			}
			(playerData as Volume).sharedProfile.TryGet<GaussianBlurStep2Vertical>(out GaussianBlurStep2Vertical component);
			ProcessFrameForGaussianBlur(playable, info, playerData, component, list);
		}
	}
}
