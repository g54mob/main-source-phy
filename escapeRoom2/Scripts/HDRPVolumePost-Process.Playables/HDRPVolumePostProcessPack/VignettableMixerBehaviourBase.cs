using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class VignettableMixerBehaviourBase : PlayableBehaviour
	{
		public void ProcessFrameForVignettable(Playable playable, FrameData info, object playerData, VignettableBase vignette, List<VignettableBehaviourBase> behaviourList)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			float num2 = 0f;
			Vector2 zero = Vector2.zero;
			float num3 = 0f;
			float num4 = 0f;
			Color color = new Color(0f, 0f, 0f, 0f);
			float num5 = 0f;
			float num6 = 0f;
			Vector2 zero2 = Vector2.zero;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				VignettableBehaviourBase vignettableBehaviourBase = behaviourList[i];
				float inputWeight = playable.GetInputWeight(i);
				if (!Mathf.Approximately(inputWeight, 0f))
				{
					num += vignettableBehaviourBase.masterVolume * inputWeight;
					num2 += vignettableBehaviourBase.vignetteIntensity * inputWeight;
					zero += vignettableBehaviourBase.vignetteCenterUV * inputWeight;
					num3 += vignettableBehaviourBase.vignetteAspectRatio * inputWeight;
					num4 += vignettableBehaviourBase.vignetteGradationPower * inputWeight;
					color += vignettableBehaviourBase.vignetteColor * inputWeight;
					num5 += vignettableBehaviourBase.maskRotation * inputWeight;
					num6 += vignettableBehaviourBase.maskReductionRatio * inputWeight;
					zero2 += vignettableBehaviourBase.maskScale * inputWeight;
				}
			}
			vignette.masterVolume.value = num;
			vignette.vignette.value.intensity = num2;
			vignette.vignette.value.centerUV = zero;
			vignette.vignette.value.aspectRatio = num3;
			vignette.vignette.value.gradationPower = num4;
			vignette.vignette.value.color = color;
			vignette.vignette.value.maskRotation = num5;
			vignette.vignette.value.maskReductionRatio = num6;
			vignette.vignette.value.maskScale = zero2;
		}
	}
}
