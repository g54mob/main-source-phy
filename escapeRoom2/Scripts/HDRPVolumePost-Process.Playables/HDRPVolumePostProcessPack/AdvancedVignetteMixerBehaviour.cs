using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class AdvancedVignetteMixerBehaviour : VignettableMixerBehaviourBase
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			List<VignettableBehaviourBase> list = new List<VignettableBehaviourBase>();
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				VignettableBehaviourBase behaviour = ((ScriptPlayable<AdvancedVignetteBehaviour>)playable.GetInput(i)).GetBehaviour();
				list.Add(behaviour);
			}
			Volume volume = playerData as Volume;
			if (!(volume == null) && !(volume.sharedProfile == null))
			{
				volume.sharedProfile.TryGet<AdvancedVignette>(out AdvancedVignette component);
				ProcessFrameForVignettable(playable, info, playerData, component, list);
			}
		}
	}
}
