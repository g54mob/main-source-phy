using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class NoisyDistortionMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			float num2 = 0f;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				NoisyDistortionBehaviour behaviour = ((ScriptPlayable<NoisyDistortionBehaviour>)playable.GetInput(i)).GetBehaviour();
				float inputWeight = playable.GetInputWeight(i);
				num += behaviour.intensity * inputWeight;
				zero += behaviour.runtimeNoiseScale * inputWeight;
				zero2 += behaviour.runtimeNoiseVector * inputWeight;
				num2 += behaviour.trancateNoiseValue * inputWeight;
			}
			(playerData as Volume).sharedProfile.TryGet<NoisyDistortion>(out NoisyDistortion component);
			component.intensity.value = num;
			component.runtimeNoiseScale.value = zero;
			component.runtimeNoiseVector.value = zero2;
			component.trancateNoiseValue.value = num2;
		}
	}
}
