using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class FlexibleNoiseEdgeMixerBehaviour : PlayableBehaviour
	{
		private readonly List<FlexibleNoiseEdge> playableTargetList = new List<FlexibleNoiseEdge>();

		private bool runningPlyable;

		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			base.OnBehaviourPause(playable, info);
			runningPlyable = false;
			StopPlayableStatus();
		}

		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			base.OnBehaviourPlay(playable, info);
			runningPlyable = true;
		}

		public override void OnGraphStart(Playable playable)
		{
			base.OnGraphStart(playable);
			runningPlyable = true;
		}

		public override void OnGraphStop(Playable playable)
		{
			base.OnGraphStop(playable);
			runningPlyable = false;
			StopPlayableStatus();
		}

		private void StopPlayableStatus()
		{
			foreach (FlexibleNoiseEdge playableTarget in playableTargetList)
			{
				playableTarget.StopPlayable();
			}
		}

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			(playerData as Volume).profile.TryGet<FlexibleNoiseEdge>(out FlexibleNoiseEdge component);
			component.StartPlayable();
			if (!playableTargetList.Contains(component))
			{
				playableTargetList.Add(component);
			}
			base.ProcessFrame(playable, info, playerData);
			if (runningPlyable)
			{
				float num = 0f;
				float num2 = 0f;
				Vector2 zero = Vector2.zero;
				float num3 = 0f;
				float num4 = 0f;
				Color color = new Color(0f, 0f, 0f, 0f);
				float maskRotation = 0f;
				float maskReductionRatio = 0f;
				Vector2 zero2 = Vector2.zero;
				Vector2 zero3 = Vector2.zero;
				int inputCount = playable.GetInputCount();
				for (int i = 0; i < inputCount; i++)
				{
					FlexibleNoiseEdgeBehaviour behaviour = ((ScriptPlayable<FlexibleNoiseEdgeBehaviour>)playable.GetInput(i)).GetBehaviour();
					float inputWeight = playable.GetInputWeight(i);
					num += behaviour.masterVolume * inputWeight;
					num2 += behaviour.vignetteIntensity * inputWeight;
					zero += behaviour.vignetteCenterUV * inputWeight;
					num3 += behaviour.vignetteAspectRatio * inputWeight;
					num4 += behaviour.vignetteGradationPower * inputWeight;
					zero3 += behaviour.surfaceTextureScrollVector * inputWeight;
				}
				component.masterVolume.value = num;
				component.setting.value.vignette.intensity = num2;
				component.setting.value.vignette.centerUV = zero;
				component.setting.value.vignette.aspectRatio = num3;
				component.setting.value.vignette.gradationPower = num4;
				component.setting.value.vignette.color = color;
				component.setting.value.vignette.maskRotation = maskRotation;
				component.setting.value.vignette.maskReductionRatio = maskReductionRatio;
				component.setting.value.vignette.maskScale = zero2;
				component.setting.value.surface.vector = zero3;
				if (!runningPlyable)
				{
					StopPlayableStatus();
				}
			}
		}
	}
}
