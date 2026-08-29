using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class GaussianBlurHoriaontalMixerBehaviour : GaussianBlurMixerBehaviourBase
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			List<GaussianBlurBehaviourBase> list = new List<GaussianBlurBehaviourBase>();
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				GaussianBlurHorizontalBehaviour behaviour = ((ScriptPlayable<GaussianBlurHorizontalBehaviour>)playable.GetInput(i)).GetBehaviour();
				list.Add(behaviour);
			}
			(playerData as Volume).sharedProfile.TryGet<GaussianBlurStep1Horizontal>(out GaussianBlurStep1Horizontal component);
			ProcessFrameForGaussianBlur(playable, info, playerData, component, list);
		}
	}
}
