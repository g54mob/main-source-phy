using System;
using Cpp2ILInjected;
using UnityEngine;

public class LightPulse : MonoBehaviour
{
	public float frequency = 1f;

	public float smoothing = 1f;

	private Light _light;

	private float _baseIntensity;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Light light = default(Light);
		_light = light;
		float intensity = _light.intensity;
		_baseIntensity = intensity;
	}

	private void OnEnable()
	{
		float intensity = _light.intensity;
		_baseIntensity = intensity;
	}

	private void OnDisable()
	{
		_light.intensity = _baseIntensity;
	}

	private void Update()
	{
		//IL_0044: Invalid comparison between I4 and F4
		//IL_00c3: Expected F4, but got I4
		//IL_01c0: Invalid comparison between I4 and F4
		//IL_00ff: Expected F4, but got I4
		//IL_0081: Expected F4, but got I4
		//IL_00b5: Expected F4, but got I4
		float time = Time.time;
		float num = time * frequency;
		float num2 = MathF.Floor(num);
		float num3 = num - num2;
		float num4;
		if (!(0f > num3))
		{
			if (num3 > 1f)
			{
				num4 = 0f;
				num3 = 1f;
			}
			else
			{
				if (0.5f > num3)
				{
					goto IL_01d4;
				}
				num4 = 0f;
			}
			goto IL_0152;
		}
		num3 = 0f;
		goto IL_01d4;
		IL_01d4:
		num4 = 1f;
		goto IL_0152;
		IL_0152:
		float num5 = num3 * (float)Math.PI;
		float num6 = num5 + num5;
		float num7 = num6 - (float)Math.PI / 2f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		float num8 = smoothing;
		float num9 = num7 + 1f;
		float num10 = num9 * 0.5f;
		if (!(0f > smoothing))
		{
			if (num8 > 1f)
			{
				num8 = 1f;
			}
		}
		else
		{
			num8 = 0f;
		}
		float num11 = num10 - num4;
		float num12 = num11 * num8;
		float num13 = num12 + num4;
		float intensity = num13 * _baseIntensity;
		_light.intensity = intensity;
	}
}
