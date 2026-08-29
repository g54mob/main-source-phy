using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class WaterSurfaceMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			float num2 = 0f;
			float value = 0f;
			float value2 = 0f;
			float value3 = 0f;
			float value4 = 0f;
			float value5 = 0f;
			Color clear = Color.clear;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				WaterSurfaceBehaviour behaviour = ((ScriptPlayable<WaterSurfaceBehaviour>)playable.GetInput(i)).GetBehaviour();
				float inputWeight = playable.GetInputWeight(i);
				num += behaviour.intensity * inputWeight;
			}
			(playerData as Volume).sharedProfile.TryGet<WaterSurface>(out WaterSurface component);
			component.intensity.value = num;
			component.layerCount.value = (int)num2;
			component.rippleBrightness.value = value;
			component.surfaceBrightness.value = value2;
			component.seed1.value = value3;
			component.seed2.value = value4;
			component.meshDetail.value = value5;
			component.color.value = clear;
		}
	}
}
