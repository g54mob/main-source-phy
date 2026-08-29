using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class SimpleRippleMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			Vector2 zero = Vector2.zero;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				SimpleRippleBehaviour behaviour = ((ScriptPlayable<SimpleRippleBehaviour>)playable.GetInput(i)).GetBehaviour();
				float inputWeight = playable.GetInputWeight(i);
				num += behaviour.masterVolume * inputWeight;
				num2 += behaviour.intensity * inputWeight;
				num3 += behaviour.frequency * inputWeight;
				num4 += behaviour.propagation * inputWeight;
				zero += behaviour.rippleCenterUV * inputWeight;
			}
			(playerData as Volume).sharedProfile.TryGet<SimpleRipple>(out SimpleRipple component);
			component.masterVolume.value = num;
			component.intensity.value = num2;
			component.frequency.value = num3;
			component.propagation.value = num4;
			component.rippleCenterUV.value = zero;
		}
	}
}
