using Cpp2ILInjected;
using UnityEngine;

public class GunMeasuredElevationSpeed : MonoBehaviour
{
	private GunController gun;

	private bool useSmoothing = true;

	private float smoothing = 0.15f;

	private bool useUnscaledTime;

	private bool sampleInLateUpdate;

	private float lastElevationDeg;

	private float rawMeasuredSpeed;

	private float smoothedMeasuredSpeed;

	private bool hasSample;

	public float MeasuredElevationSpeed
	{
		get
		{
			if (useSmoothing)
			{
				return smoothedMeasuredSpeed;
			}
			return rawMeasuredSpeed;
		}
	}

	public float MeasuredElevationSpeedAbs
	{
		get
		{
			//IL_001d: Expected O, but got I4
			//IL_0056: Expected F4, but got I
			//IL_0034: Expected O, but got I4
			bool flag = useSmoothing;
			object obj = 60;
			if (!flag)
			{
				obj = 56;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rax_v2+this @ rcx (GunMeasuredElevationSpeed)]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			return num & 0;
		}
	}

	private void Awake()
	{
		if (gun == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			GunController gunController = default(GunController);
			gun = gunController;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 66 Invalid \"Jump target not found in method: 0x18054D8F0\"");
	}

	private void OnEnable()
	{
		ResetSampling();
	}

	private void Update()
	{
		if (!sampleInLateUpdate)
		{
			Sample();
		}
	}

	private void LateUpdate()
	{
		if (sampleInLateUpdate)
		{
			Sample();
		}
	}

	private void ResetSampling()
	{
		hasSample = false;
		rawMeasuredSpeed = 0f;
		if (!(gun != null))
		{
			lastElevationDeg = 0f;
			return;
		}
		GunController gunController = gun;
		lastElevationDeg = gunController._003CCurrentElevation_003Ek__BackingField;
	}

	private void Sample()
	{
		//IL_006e: Invalid comparison between F4 and I
		//IL_009d: Expected F4, but got I
		//IL_011a: Invalid comparison between F4 and I4
		//IL_013c: Invalid comparison between I4 and F4
		//IL_0193: Expected F4, but got I4
		//IL_0261: Invalid comparison between I4 and F4
		//IL_01cf: Expected F4, but got I4
		if (!(gun != null))
		{
			return;
		}
		float num = ((!useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
		bool flag = !(num < 0f);
		float num2 = num;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
			num2 = 0f;
		}
		GunController gunController = gun;
		if (hasSample)
		{
			bool flag2 = !useSmoothing;
			float num3 = gunController._003CCurrentElevation_003Ek__BackingField - lastElevationDeg;
			float num4 = num3 / num2;
			rawMeasuredSpeed = num4;
			float num11;
			if (!flag2 && smoothing > 0f)
			{
				float num5;
				if (!(0f > smoothing))
				{
					bool flag3 = !(smoothing > 1f);
					num5 = smoothing;
					if (!flag3)
					{
						num5 = 1f;
					}
				}
				else
				{
					num5 = 0f;
				}
				float num6 = num2 * 60f;
				float num7 = 1f - num5;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num8 = 1f - num7;
				if (!(0f > num8))
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
				float num9 = rawMeasuredSpeed - smoothedMeasuredSpeed;
				float num10 = num9 * num8;
				num11 = num10 + smoothedMeasuredSpeed;
			}
			else
			{
				num11 = rawMeasuredSpeed;
			}
			smoothedMeasuredSpeed = num11;
			lastElevationDeg = gunController._003CCurrentElevation_003Ek__BackingField;
		}
		else
		{
			lastElevationDeg = gunController._003CCurrentElevation_003Ek__BackingField;
			rawMeasuredSpeed = 0f;
			hasSample = true;
		}
	}
}
