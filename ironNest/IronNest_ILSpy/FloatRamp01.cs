using UnityEngine;

public class FloatRamp01 : MonoBehaviour, IFloatValueProvider
{
	private float rampDuration = 5f;

	private float currentValue;

	private float elapsedTime;

	public float GetFloatValue()
	{
		return currentValue;
	}

	private void OnEnable()
	{
		currentValue = 0f;
	}

	private void Update()
	{
		//IL_0060: Invalid comparison between I4 and F4
		//IL_00a9: Expected F4, but got I4
		if (!(currentValue < 1f))
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		float num = (elapsedTime = deltaTime + elapsedTime) / rampDuration;
		if (!(0f > num))
		{
			if (num > 1f)
			{
				currentValue = 1f;
				return;
			}
		}
		else
		{
			num = 0f;
		}
		currentValue = num;
	}
}
