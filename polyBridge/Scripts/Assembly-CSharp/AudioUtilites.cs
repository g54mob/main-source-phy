using UnityEngine;
using UnityEngine.Audio;

public static class AudioUtilites
{
	public static float DecibelToLinear(float val)
	{
		return Mathf.Pow(10f, val / 20f);
	}

	public static float LinearToDecibel(float val)
	{
		if (!(val <= 0f))
		{
			return 20f * Mathf.Log10(val);
		}
		return -80f;
	}

	public static float GetFloat(this AudioMixer mixer, string paramName)
	{
		if (!mixer.GetFloat(paramName, out var value))
		{
			Debug.LogError("Mixer param '" + paramName + "' didn't exist in mixer", mixer);
		}
		return value;
	}

	public static bool CheckDouble(double value)
	{
		if (double.IsNaN(value) || double.IsPositiveInfinity(value) || double.IsNegativeInfinity(value))
		{
			return true;
		}
		return false;
	}
}
