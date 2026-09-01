using System;

namespace Lofelt.NiceVibrations;

[Serializable]
public struct GamepadRumble
{
	public int[] durationsMs;

	public int totalDurationMs;

	public float[] lowFrequencyMotorSpeeds;

	public float[] highFrequencyMotorSpeeds;

	public bool IsValid()
	{
		if (durationsMs != null && lowFrequencyMotorSpeeds != null && highFrequencyMotorSpeeds != null)
		{
			float[] array = lowFrequencyMotorSpeeds;
			int[] array2 = durationsMs;
			if (array2.Length == array.Length)
			{
				float[] array3 = highFrequencyMotorSpeeds;
				if (array2.Length == array3.Length)
				{
					bool flag = array2.Length < 0;
					bool flag2 = array2.Length == 0;
					bool flag3 = !flag;
					bool flag4 = !flag2;
					return flag4 & flag3;
				}
			}
		}
		return false;
	}
}
