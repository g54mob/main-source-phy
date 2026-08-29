using System;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class KuwaharaFilterMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				KuwaharaFilterBehaviour behaviour = ((ScriptPlayable<KuwaharaFilterBehaviour>)playable.GetInput(i)).GetBehaviour();
				float inputWeight = playable.GetInputWeight(i);
				num += behaviour.intensity * inputWeight;
			}
			(playerData as Volume).sharedProfile.TryGet<KuwaharaFilter>(out KuwaharaFilter component);
			component.intensity.value = (int)num;
		}
	}
}
