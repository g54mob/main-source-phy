using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class OdometerDisplayWatcher : MonoBehaviour
{
	private OdometerDisplay odometer;

	private float currentValue;

	private float valueChangeSpeed;

	private float smoothedValueChangeSpeed;

	private float rollingCounterSpeedPercent;

	private bool useUnscaledTime;

	private bool absoluteSpeed;

	private bool enableSpeedSmoothing = true;

	private float speedSmoothingTimeConstant = 0.25f;

	private float maxPerFrameSpeedDelta;

	private float Rolling_Counter_Speed_Cap;

	private bool initialized;

	private float lastValue;

	private bool speedSmoothingInitialized;

	private float speedSmoothed;

	public float Rolling_Counter_Speed => rollingCounterSpeedPercent;

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		OdometerDisplay odometerDisplay = default(OdometerDisplay);
		odometer = odometerDisplay;
	}

	private void Update()
	{
		//IL_0067: Invalid comparison between I4 and F4
		//IL_0370: Invalid comparison between F4 and I4
		//IL_0382: Expected O, but got I4
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected F4, but got Unknown
		//IL_045b: Invalid comparison between F4 and I4
		//IL_046d: Expected F4, but got I4
		//IL_0127: Expected O, but got I4
		//IL_0187: Invalid comparison between F4 and I4
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Expected O, but got Unknown
		//IL_03c6: Invalid comparison between O and F4
		//IL_03d8: Expected O, but got I4
		//IL_02b4: Invalid comparison between I4 and F4
		//IL_02c3: Expected F4, but got I4
		//IL_013e: Expected O, but got I4
		//IL_014c: Invalid comparison between F4 and I4
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_0417: Expected O, but got I4
		//IL_04ad: Invalid comparison between I4 and F4
		//IL_027d: Expected F4, but got I4
		if (!(odometer != null))
		{
			return;
		}
		float num = ((!useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
		if (!(0f < num))
		{
			return;
		}
		OdometerDisplay odometerDisplay = odometer;
		float num3;
		if (initialized)
		{
			bool flag = !absoluteSpeed;
			float num2 = odometerDisplay.currentNumber - lastValue;
			num3 = num2 / num;
			if (!flag)
			{
				float num4 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				num3 = num4 & 0;
			}
			bool flag2 = !(maxPerFrameSpeedDelta > 0f);
			object obj = 0;
			if (!flag2)
			{
				bool flag3 = speedSmoothingInitialized;
				object obj2 = 84;
				if (!flag3)
				{
					obj2 = 48;
				}
				float num5 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rax_v15+this @ rcx (OdometerDisplayWatcher)]");
				float num6 = num5 - 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj3 = num6 & 0;
				bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxPerFrameSpeedDelta);
				obj = 48;
				if (!flag4)
				{
					float num7 = ((num6 < 0f) ? (-1f) : 1f);
					float num8 = num7 * maxPerFrameSpeedDelta;
					float num9 = num8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rax_v15+this @ rcx (OdometerDisplayWatcher)]");
					num3 = num9 + 0f;
					obj = 48;
				}
			}
			bool flag5 = !enableSpeedSmoothing;
			valueChangeSpeed = num3;
			float num10 = num;
			if (!flag5)
			{
				bool flag6 = !(speedSmoothingTimeConstant > 0f);
				num10 = num;
				if (!flag6)
				{
					if (speedSmoothingInitialized)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
						object obj4 = num ^ 0;
						bool flag7 = !(0.0001f < speedSmoothingTimeConstant);
						float num11 = 0.0001f;
						if (!flag7)
						{
							num11 = speedSmoothingTimeConstant;
						}
						float num12 = (float)obj4 / num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
						num10 = 1f - num12;
						if (!(0f > num10))
						{
							if (num10 > 1f)
							{
								float num13 = num3 - speedSmoothed;
								float num14 = num13 * 1f;
								num3 = num14 + speedSmoothed;
								num10 = 1f;
								goto IL_04f7;
							}
						}
						else
						{
							num10 = 0f;
						}
						float num15 = num3 - speedSmoothed;
						float num16 = num15 * num10;
						num3 = num16 + speedSmoothed;
					}
					else
					{
						speedSmoothingInitialized = true;
						num10 = num;
					}
					goto IL_04f7;
				}
			}
			goto IL_041c;
		}
		lastValue = odometerDisplay.currentNumber;
		currentValue = odometerDisplay.currentNumber;
		valueChangeSpeed = 0f;
		speedSmoothingInitialized = false;
		speedSmoothed = 0f;
		rollingCounterSpeedPercent = 0f;
		initialized = true;
		return;
		IL_04f7:
		speedSmoothed = num3;
		goto IL_041c;
		IL_041c:
		float num17 = num3 * 100f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num18 = (smoothedValueChangeSpeed = num17 / 100f);
		bool flag8 = !(Rolling_Counter_Speed_Cap > 0f);
		float num19 = 0f;
		if (!flag8)
		{
			float num20 = num18 / Rolling_Counter_Speed_Cap;
			bool flag9 = 0f > num20;
			num19 = 0f;
			if (!flag9)
			{
				num19 = ((num20 > 1f) ? 1f : num20);
			}
		}
		rollingCounterSpeedPercent = num19;
		currentValue = odometerDisplay.currentNumber;
		lastValue = odometerDisplay.currentNumber;
	}

	private static float RoundTo2Decimals(float v)
	{
		float num = v * 100f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		return num / 100f;
	}
}
