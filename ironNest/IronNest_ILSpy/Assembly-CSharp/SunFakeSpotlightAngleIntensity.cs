using Cpp2ILInjected;
using UnityEngine;

public class SunFakeSpotlightAngleIntensity : MonoBehaviour
{
	public enum RotationSpace
	{
		Local,
		World
	}

	public enum RotationAxis
	{
		X,
		Y,
		Z
	}

	public enum CaptureTiming
	{
		Awake,
		Start
	}

	private Light targetLight;

	private Transform observedRotation;

	private RotationSpace rotationSpace;

	private RotationAxis axis = RotationAxis.Y;

	private float minFullBrightnessAngle = -70f;

	private float maxFullBrightnessAngle = 70f;

	private float rampOutDegrees = 20f;

	private AnimationCurve normalizedToMultiplier;

	private float baseIntensity;

	private bool useInitialLightIntensityAsBase;

	private CaptureTiming captureTiming;

	private bool updateContinuously;

	private bool clampMultiplier01;

	private float capturedInitialIntensity;

	private bool hasCaptured;

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Light light = default(Light);
		targetLight = light;
		Transform transform = base.transform;
		observedRotation = transform;
	}

	private void Awake()
	{
		ResolveDefaults();
		if (useInitialLightIntensityAsBase && captureTiming == CaptureTiming.Awake)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 34 Invalid \"Jump target not found in method: 0x180404910\"");
		}
	}

	private void Start()
	{
		ResolveDefaults();
		if (useInitialLightIntensityAsBase && captureTiming == CaptureTiming.Start)
		{
			CaptureInitialIntensityIfNeeded();
		}
	}

	private void LateUpdate()
	{
		if (updateContinuously)
		{
			Apply();
		}
	}

	public void Apply()
	{
		//IL_00d3: Expected O, but got I4
		//IL_0227: Expected F4, but got I4
		//IL_03da: Invalid comparison between I4 and F4
		//IL_0255: Invalid comparison between I4 and F4
		//IL_0322: Invalid comparison between I4 and F4
		//IL_0334: Expected F4, but got I4
		//IL_02a0: Expected F4, but got I4
		if (!targetLight || !observedRotation)
		{
			return;
		}
		Vector3 vector = ((rotationSpace != RotationSpace.Local) ? observedRotation.eulerAngles : observedRotation.localEulerAngles);
		bool flag = axis == RotationAxis.X;
		float num;
		if (!flag)
		{
			object obj = axis - 1;
			float num2 = default(float);
			num = ((!flag && (nint)obj == 1) ? vector.z : num2);
		}
		else
		{
			num = vector.x;
		}
		if (num > 180f)
		{
			num += -360f;
		}
		float num3 = minFullBrightnessAngle;
		bool flag2 = !(minFullBrightnessAngle > maxFullBrightnessAngle);
		float num4 = maxFullBrightnessAngle;
		if (!flag2)
		{
			num4 = minFullBrightnessAngle;
			num3 = maxFullBrightnessAngle;
		}
		float num5;
		if (!(num < num3) && !(num4 < num))
		{
			num5 = 1f;
		}
		else
		{
			if (0.0001f < rampOutDegrees)
			{
				float num6 = ((!(num3 > num)) ? (num - num4) : (num3 - num));
				float num7 = num6 / rampOutDegrees;
				num5 = 1f - num7;
				if (!(0f > num5))
				{
					if (num5 > 1f)
					{
						num5 = 1f;
					}
					goto IL_0391;
				}
			}
			num5 = 0f;
		}
		goto IL_0391;
		IL_030a:
		float num9;
		float num8 = num9 * num5;
		bool flag3 = !(0f < num8);
		float intensity = 0f;
		if (!flag3)
		{
			intensity = num8;
		}
		targetLight.intensity = intensity;
		return;
		IL_02fb:
		num9 = baseIntensity;
		goto IL_030a;
		IL_0391:
		if (normalizedToMultiplier != null)
		{
			float num10 = normalizedToMultiplier.Evaluate(num5);
			num5 = num10;
		}
		if (clampMultiplier01)
		{
			if (!(0f > num5))
			{
				if (num5 > 1f)
				{
					num5 = 1f;
				}
			}
			else
			{
				num5 = 0f;
			}
		}
		if (!useInitialLightIntensityAsBase)
		{
			goto IL_02fb;
		}
		if (!hasCaptured)
		{
			CaptureInitialIntensityIfNeeded();
			if (!hasCaptured)
			{
				goto IL_02fb;
			}
		}
		num9 = capturedInitialIntensity;
		goto IL_030a;
	}

	private void ResolveDefaults()
	{
		if (!targetLight)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Light light = default(Light);
			targetLight = light;
		}
		if (!observedRotation)
		{
			Transform transform = base.transform;
			observedRotation = transform;
		}
	}

	private void CaptureInitialIntensityIfNeeded()
	{
		if (!hasCaptured && (bool)targetLight)
		{
			float intensity = targetLight.intensity;
			capturedInitialIntensity = intensity;
			hasCaptured = true;
		}
	}

	private float GetBaseIntensity()
	{
		if (useInitialLightIntensityAsBase)
		{
			if (!hasCaptured)
			{
				CaptureInitialIntensityIfNeeded();
			}
			if (hasCaptured)
			{
				return capturedInitialIntensity;
			}
		}
		return baseIntensity;
	}

	private static float GetSignedAngleDegrees(Transform t, RotationSpace space, RotationAxis axis)
	{
		//IL_007c: Expected O, but got I4
		Vector3 vector = ((space != RotationSpace.Local) ? t.eulerAngles : t.localEulerAngles);
		float num = vector.x;
		bool flag = axis == RotationAxis.X;
		if (!flag)
		{
			object obj = axis - 1;
			float num2 = default(float);
			num = ((!flag && (nint)obj == 1) ? vector.z : num2);
		}
		if (num > 180f)
		{
			num += -360f;
		}
		return num;
	}

	private static float ComputeNormalizedBrightness(float angle, float minFull, float maxFull, float rampOut)
	{
		//IL_00fc: Expected F4, but got I4
		//IL_0145: Invalid comparison between I4 and F4
		bool flag = !(minFull > maxFull);
		float num = maxFull;
		float num2 = minFull;
		if (!flag)
		{
			num = minFull;
			num2 = maxFull;
		}
		if (!(angle < num2) && !(num < angle))
		{
			return 1f;
		}
		float num5;
		if (0.0001f < rampOut)
		{
			float num3 = ((!(num2 > angle)) ? (angle - num) : (num2 - angle));
			float num4 = num3 / rampOut;
			num5 = 1f - num4;
			if (!(0f > num5))
			{
				if (num5 > 1f)
				{
					return 1f;
				}
				goto IL_0159;
			}
		}
		num5 = 0f;
		goto IL_0159;
		IL_0159:
		return num5;
	}

	public SunFakeSpotlightAngleIntensity()
	{
		Keyframe[] keys = new Keyframe[2];
		Keyframe keyframe = new Keyframe(0f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe2 = new Keyframe(1f, 1f);
		_ = 0;
		_ = 0;
		_ = 0;
		normalizedToMultiplier = new AnimationCurve(keys);
		baseIntensity = 1f;
		useInitialLightIntensityAsBase = true;
		captureTiming = CaptureTiming.Start;
		updateContinuously = true;
		base._002Ector();
	}
}
