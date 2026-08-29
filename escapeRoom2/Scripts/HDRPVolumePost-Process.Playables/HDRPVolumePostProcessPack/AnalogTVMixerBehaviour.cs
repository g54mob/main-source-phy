using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class AnalogTVMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			float num2 = 0f;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			Vector2 zero3 = Vector2.zero;
			float num3 = 0f;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				AnalogTVBehaviour behaviour = ((ScriptPlayable<AnalogTVBehaviour>)playable.GetInput(i)).GetBehaviour();
				float inputWeight = playable.GetInputWeight(i);
				num += behaviour.masterVolume * inputWeight;
				num2 += behaviour.lineCount * inputWeight;
				zero += behaviour.chromaticR * inputWeight;
				zero2 += behaviour.chromaticG * inputWeight;
				zero3 += behaviour.chromaticB * inputWeight;
				num3 += behaviour.speed * inputWeight;
			}
			(playerData as Volume).sharedProfile.TryGet<AnalogTVLines>(out AnalogTVLines component);
			component.masterVolume.value = num;
			component.lineCount.value = (int)num2;
			component.chromaticR.value = zero;
			component.chromaticG.value = zero2;
			component.chromaticB.value = zero3;
			component.speed.value = num3;
		}
	}
}
