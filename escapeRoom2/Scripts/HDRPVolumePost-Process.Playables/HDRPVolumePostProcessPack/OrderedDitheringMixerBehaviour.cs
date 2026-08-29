using System;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class OrderedDitheringMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				OrderedDitheringBehaviour behaviour = ((ScriptPlayable<OrderedDitheringBehaviour>)playable.GetInput(i)).GetBehaviour();
				float inputWeight = playable.GetInputWeight(i);
				num += behaviour.intensity * inputWeight;
				num2 += (float)behaviour.colorStep * inputWeight;
				num3 += (float)behaviour.ditherThreshold * inputWeight;
			}
			(playerData as Volume).sharedProfile.TryGet<OrderedDithering>(out OrderedDithering component);
			component.intensity.value = num;
			component.colorStep.value = (int)num2;
			component.ditherThreshold.value = (int)num3;
		}
	}
}
