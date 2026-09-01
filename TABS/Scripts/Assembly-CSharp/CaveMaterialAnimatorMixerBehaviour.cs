using UnityEngine;
using UnityEngine.Playables;

public class CaveMaterialAnimatorMixerBehaviour : PlayableBehaviour
{
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		Material material = playerData as Material;
		float num = 0f;
		if (!material)
		{
			return;
		}
		int inputCount = playable.GetInputCount();
		string name = "";
		for (int i = 0; i < inputCount; i++)
		{
			float inputWeight = playable.GetInputWeight(i);
			CaveMaterialAnimatorBehaviour behaviour = ((ScriptPlayable<CaveMaterialAnimatorBehaviour>)playable.GetInput(i)).GetBehaviour();
			if (i == 0)
			{
				name = behaviour.parameterName;
			}
			num += behaviour.value * inputWeight;
		}
		material.SetFloat(name, num);
	}
}
