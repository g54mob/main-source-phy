using System;
using Cpp2ILInjected;
using UnityEngine;

public sealed class SwingIntensityProvider : MonoBehaviour, IFloatValueProvider
{
	private bool autoPopulateOnEnable;

	private SwingReceiver[] swingReceivers;

	private float fullScaleMotion = 50f;

	private float postGain = 1f;

	private float attackTimeSeconds = 0.1f;

	private float releaseTimeSeconds = 0.7f;

	private int receiverCount;

	private float aggregatedMotionRMS;

	private float rawIntensity;

	private float smoothedIntensity;

	private float _smoothedValue;

	private void OnEnable()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		if (autoPopulateOnEnable)
		{
			SwingReceiver[] array = UnityEngine.Object.FindObjectsByType<SwingReceiver>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			swingReceivers = array;
		}
		SwingReceiver[] array2 = swingReceivers;
		int num3;
		if (swingReceivers != null && array2.Length != 0)
		{
			object obj = swingReceivers + 32;
			int num = 0;
			int num2 = 0;
			num3 = 0;
			while (num < array2.Length)
			{
				num2++;
				obj += 8;
				bool flag = (UnityEngine.Object)obj == null;
				int num4 = num3 + 1;
				if (flag)
				{
					num4 = num3;
				}
				num = num2;
				num3 = num4;
			}
		}
		else
		{
			num3 = 0;
		}
		receiverCount = num3;
	}

	private void OnValidate()
	{
		//IL_005e: Invalid comparison between I4 and F4
		//IL_0070: Expected F4, but got I4
		bool flag = !(0.0001f < fullScaleMotion);
		float num = 0.0001f;
		if (!flag)
		{
			num = fullScaleMotion;
		}
		fullScaleMotion = num;
		bool flag2 = !(0f < postGain);
		float num2 = 0f;
		if (!flag2)
		{
			num2 = postGain;
		}
		postGain = num2;
	}

	private void AutoPopulateReceiversFromScene()
	{
		SwingReceiver[] array = UnityEngine.Object.FindObjectsByType<SwingReceiver>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		swingReceivers = array;
	}

	private static int CountValidReceivers(SwingReceiver[] receivers)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_00e4: Expected I4, but got O
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		if (receivers != null && receivers.Length != 0)
		{
			object obj = receivers + 32;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (true)
			{
				if (num < receivers.Length)
				{
					if (num2 >= receivers.Length)
					{
						break;
					}
					num2++;
					obj += 8;
					bool flag = (UnityEngine.Object)obj == null;
					int num4 = num3 + 1;
					if (flag)
					{
						num4 = num3;
					}
					num = num2;
					num3 = num4;
					continue;
				}
				return num3;
			}
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (int)ex;
		}
		return 0;
	}

	private void Update()
	{
		//IL_02e0: Invalid comparison between I4 and F4
		//IL_0080: Expected O, but got I4
		//IL_0089: Expected F4, but got I4
		//IL_0092: Expected F4, but got I4
		//IL_00a4: Expected F4, but got I4
		//IL_032e: Invalid comparison between F4 and I4
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected I4, but got Unknown
		//IL_00c8: Expected O, but got I
		//IL_00df: Expected O, but got I
		//IL_034f: Expected F4, but got I4
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Expected I4, but got Unknown
		//IL_0378: Invalid comparison between I4 and F4
		//IL_01e8: Expected F4, but got I4
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Expected O, but got Unknown
		//IL_03b2: Expected O, but got I4
		//IL_01f6: Expected O, but got I4
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_0248: Invalid comparison between I4 and F4
		//IL_0293: Expected F4, but got I4
		float deltaTime = Time.deltaTime;
		if (0f < deltaTime && receiverCount > 0 && swingReceivers != null)
		{
			SwingReceiver[] array = swingReceivers;
			if (array.Length != 0)
			{
				float num = deltaTime;
				object obj = 32;
				float num2 = 0f;
				float num3 = 0f;
				int num4 = 0;
				for (float num5 = 0f; num5 < (float)array.Length; num5 = num3)
				{
					SwingReceiver[] array2 = swingReceivers;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ r14_v3+v283 @ rax_v14 (SwingReceiver[])]");
					UnityEngine.Object obj2 = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ r14_v3+v283 @ rax_v14 (SwingReceiver[])]");
					if ((UnityEngine.Object)0 != null)
					{
						num4++;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ rsi_v6 (UnityEngine.Object)+74]");
						float num6 = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ rsi_v6 (UnityEngine.Object)+74]");
						num = num6 * 0f;
						num2 += num;
					}
					array = swingReceivers;
					num3++;
					obj += 8;
				}
				receiverCount = num4;
				if (num4 > 0)
				{
					int num7 = (int)(num2 / num4);
					if (0 <= num7)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm6,xmm6\"");
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
					}
					aggregatedMotionRMS = num7;
					int num8 = (int)(num7 / fullScaleMotion);
					float num9 = (float)num8 * postGain;
					if (!(0f > num9))
					{
						if (num9 > 1f)
						{
							num9 = 1f;
						}
					}
					else
					{
						num9 = 0f;
					}
					rawIntensity = num9;
					bool flag = !(num9 > _smoothedValue);
					object obj3 = 60;
					if (!flag)
					{
						obj3 = 56;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v492 @ rax_v9+this @ rcx (SwingIntensityProvider)]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
						object obj4 = deltaTime ^ 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v492 @ rax_v9+this @ rcx (SwingIntensityProvider)]");
						object obj5 = obj4 / 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
						float num10 = 1f - (float)obj5;
						if (!(0f > num10))
						{
							if (num10 > 1f)
							{
								num10 = 1f;
							}
						}
						else
						{
							num10 = 0f;
						}
						float num11 = num9 - _smoothedValue;
						float num12 = num11 * num10;
						num9 = num12 + _smoothedValue;
					}
					_smoothedValue = num9;
					smoothedIntensity = num9;
				}
				else
				{
					aggregatedMotionRMS = 0f;
					smoothedIntensity = 0f;
				}
				return;
			}
		}
		aggregatedMotionRMS = 0f;
		smoothedIntensity = 0f;
	}

	private void ResetState()
	{
		aggregatedMotionRMS = 0f;
		smoothedIntensity = 0f;
	}

	public float GetFloatValue()
	{
		return smoothedIntensity;
	}
}
