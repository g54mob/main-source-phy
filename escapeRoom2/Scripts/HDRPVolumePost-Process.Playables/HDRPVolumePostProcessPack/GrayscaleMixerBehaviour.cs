using System;
using UnityEngine.Playables;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class GrayscaleMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			float num = 0f;
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				GrayscaleBehaviour behaviour = ((ScriptPlayable<GrayscaleBehaviour>)playable.GetInput(i)).GetBehaviour();
				float inputWeight = playable.GetInputWeight(i);
				num += behaviour.intensity * inputWeight;
			}
			(playerData as Volume).sharedProfile.TryGet<Grayscale>(out Grayscale component);
			component.intensity.value = num;
		}
	}
}
