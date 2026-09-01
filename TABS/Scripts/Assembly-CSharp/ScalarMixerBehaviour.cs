using UnityEngine;
using UnityEngine.Playables;

public class ScalarMixerBehaviour : PlayableBehaviour
{
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		Transform transform = playerData as Transform;
		Vector3 zero = Vector3.zero;
		if ((bool)transform)
		{
			int inputCount = playable.GetInputCount();
			for (int i = 0; i < inputCount; i++)
			{
				float inputWeight = playable.GetInputWeight(i);
				ScalarBehaviour behaviour = ((ScriptPlayable<ScalarBehaviour>)playable.GetInput(i)).GetBehaviour();
				zero += behaviour.scale * inputWeight;
			}
			transform.localScale = zero;
		}
	}
}
