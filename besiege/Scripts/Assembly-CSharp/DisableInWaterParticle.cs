using UnityEngine;

public class DisableInWaterParticle : MonoBehaviour
{
	public BasicInfo bInfo;

	public ParticleSystem[] toDisable;

	public ParticleSystem[] toEnable;

	public bool reverseWhenExitWater;

	private bool wasInWater;

	private void Update()
	{
		if (!wasInWater && bInfo.InWater)
		{
			wasInWater = true;
			for (int i = 0; i < toDisable.Length; i++)
			{
				toDisable[i].Stop(true);
			}
			for (int j = 0; j < toEnable.Length; j++)
			{
				toEnable[j].Play(true);
			}
		}
		else if (reverseWhenExitWater && wasInWater && !bInfo.InWater)
		{
			wasInWater = false;
			for (int k = 0; k < toDisable.Length; k++)
			{
				toDisable[k].Play(true);
			}
			for (int l = 0; l < toEnable.Length; l++)
			{
				toEnable[l].Stop(true);
			}
		}
	}
}
