using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class GaussianBlurMixerBehaviourBase : PlayableBehaviour
	{
		public void ProcessFrameForGaussianBlur(Playable playable, FrameData info, object playerData, GaussianBlurBase gaussianBlur, List<GaussianBlurBehaviourBase> behaviourList)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				GaussianBlurBehaviourBase gaussianBlurBehaviourBase = behaviourList[i];
				float inputWeight = playable.GetInputWeight(i);
				if (!Mathf.Approximately(inputWeight, 0f))
				{
					num += gaussianBlurBehaviourBase.intensity * inputWeight;
					num2 += gaussianBlurBehaviourBase.sampleCount * inputWeight;
					num3 += gaussianBlurBehaviourBase.sampleInterval * inputWeight;
					num4 += gaussianBlurBehaviourBase.standardDeviation * inputWeight;
					num5 += gaussianBlurBehaviourBase.brightness * inputWeight;
				}
			}
			gaussianBlur.intensity.value = num;
			gaussianBlur.sampleCount.value = (int)num2;
			gaussianBlur.sampleInterval.value = num3;
			gaussianBlur.standardDeviation.value = num4;
			gaussianBlur.brightness.value = num5;
		}
	}
}
