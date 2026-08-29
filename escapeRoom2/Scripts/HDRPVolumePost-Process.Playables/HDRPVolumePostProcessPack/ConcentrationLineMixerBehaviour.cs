using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class ConcentrationLineMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			float num2 = 0f;
			Vector2 zero = Vector2.zero;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			Color value = new Color(0f, 0f, 0f, 0f);
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				ConcentrationLineBehaviour behaviour = ((ScriptPlayable<ConcentrationLineBehaviour>)playable.GetInput(i)).GetBehaviour();
				float inputWeight = playable.GetInputWeight(i);
				num += behaviour.intensity * inputWeight;
				num2 += (float)behaviour.lineFrequency * inputWeight;
				zero += behaviour.centerViewportInScreen * inputWeight;
				num3 += behaviour.blankRadius * inputWeight;
				num4 += behaviour.colorThreshold * inputWeight;
				num5 += behaviour.blankAspect * inputWeight;
				num6 += behaviour.lineRotation * inputWeight;
				value += behaviour.lineColor * inputWeight;
			}
			(playerData as Volume).sharedProfile.TryGet<ConcentrationLine>(out ConcentrationLine component);
			component.intensity.value = num;
			component.lineFrequency.value = (int)num2;
			component.centerViewportInScreen.value = zero;
			component.blankRadius.value = num3;
			component.colorThreshold.value = num4;
			component.blankAspect.value = num5;
			component.lineRotation.value = num6;
			component.lineColor.value = value;
		}
	}
}
