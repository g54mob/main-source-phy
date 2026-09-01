using System;
using Cpp2ILInjected;
using UnityEngine;

public class TimeToImpactSecondsBridge : MonoBehaviour
{
	public enum PredictionMode
	{
		UseGunControllerPredictedImpactTime,
		ComputeFromGunAtFireTime
	}

	[Serializable]
	private class ShotCountdown
	{
		private double startTime = -1.0;

		private float durationSeconds;

		public void Begin(double now, float duration)
		{
			//IL_0013: Invalid comparison between I4 and F4
			//IL_0025: Expected F4, but got I4
			startTime = now;
			bool flag = !(0f < duration);
			float num = 0f;
			if (!flag)
			{
				num = duration;
			}
			durationSeconds = num;
		}

		public float GetRemaining(double now)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			//IL_0037: Expected F4, but got I4
			//IL_0062: Invalid comparison between I4 and F8
			//IL_0074: Expected F8, but got I4
			//IL_00bb: Invalid comparison between I4 and F4
			//IL_00cd: Expected F4, but got I4
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [rcx+10h]\"");
			object obj = default(object);
			object obj2 = default(object);
			bool flag = obj == obj2;
			object obj4 = default(object);
			object obj3 = ~obj4;
			object obj5 = flag & obj3;
			float result = 0f;
			if (obj5 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rcx+10h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm1\"");
				bool flag2 = !(0.0 < now);
				double num = 0.0;
				if (!flag2)
				{
					num = now;
				}
				float num2 = durationSeconds - (float)num;
				bool flag3 = !(0f < num2);
				result = 0f;
				if (!flag3)
				{
					result = num2;
				}
			}
			return result;
		}

		public bool IsActive(double now)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			//IL_0037: Expected F4, but got I4
			//IL_00c6: Invalid comparison between F4 and I4
			//IL_0075: Expected O, but got I4
			//IL_010e: Invalid comparison between I4 and F4
			//IL_0120: Expected F4, but got I4
			//IL_008c: Expected O, but got I4
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [rcx+10h]\"");
			object obj = default(object);
			object obj2 = default(object);
			bool flag = obj == obj2;
			object obj4 = default(object);
			object obj3 = ~obj4;
			object obj5 = flag & obj3;
			float num = 0f;
			if (obj5 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rcx+10h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm1\"");
				bool flag2 = 0 >= 0;
				object obj6 = 0;
				if (!flag2)
				{
					obj6 = 0;
				}
				float num2 = durationSeconds - (float)obj6;
				bool flag3 = !(0f < num2);
				num = 0f;
				if (!flag3)
				{
					num = num2;
				}
			}
			bool flag4 = num < 0.0001f;
			float num3 = num - 0.0001f;
			bool flag5 = num3 == 0f;
			bool flag6 = !flag4;
			bool flag7 = !flag5;
			return flag7 & flag6;
		}

		public void Clear()
		{
			durationSeconds = 0f;
			startTime = -1.0;
		}
	}

	private GunController gunA;

	private GunController gunB;

	private PredictionMode predictionMode;

	private float noInFlightFallbackSeconds = 999f;

	private bool clampOutput;

	private float outputMinSeconds;

	private float outputMaxSeconds = 999f;

	private int roundToDecimals = 2;

	private float inspectorCurrentOutputSeconds;

	private bool inspectorAActive;

	private bool inspectorBActive;

	private float inspectorARemainingSeconds;

	private float inspectorBRemainingSeconds;

	private ShotCountdown shotA = new ShotCountdown
	{
		startTime = -1.0
	};

	private ShotCountdown shotB = new ShotCountdown
	{
		startTime = -1.0
	};

	public float CurrentTimeToImpactSeconds => inspectorCurrentOutputSeconds;

	private void OnEnable()
	{
		Action handler = HandleGunAFired;
		Subscribe(gunA, handler);
		Action handler2 = HandleGunBFired;
		Subscribe(gunB, handler2);
	}

	private void OnDisable()
	{
		Action handler = HandleGunAFired;
		Unsubscribe(gunA, handler);
		Action handler2 = HandleGunBFired;
		Unsubscribe(gunB, handler2);
	}

	private void Update()
	{
		//IL_008c: Expected O, but got I4
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_00a2: Expected F4, but got I4
		//IL_012d: Expected F4, but got I4
		//IL_00e0: Expected F4, but got I4
		//IL_03b7: Invalid comparison between I4 and F4
		//IL_03c9: Expected F4, but got I4
		//IL_01b8: Expected F4, but got I4
		//IL_016b: Expected F4, but got I4
		//IL_00f7: Expected F4, but got I4
		//IL_0455: Invalid comparison between F4 and I4
		//IL_040d: Invalid comparison between I4 and F4
		//IL_041f: Expected F4, but got I4
		//IL_0243: Expected F4, but got I4
		//IL_01f6: Expected F4, but got I4
		//IL_0182: Expected F4, but got I4
		//IL_04fc: Invalid comparison between F4 and I4
		//IL_04b4: Invalid comparison between I4 and F4
		//IL_04c6: Expected F4, but got I4
		//IL_0281: Expected F4, but got I4
		//IL_020d: Expected F4, but got I4
		//IL_0589: Invalid comparison between I4 and F4
		//IL_059b: Expected F4, but got I4
		//IL_0298: Expected F4, but got I4
		double timeAsDouble = Time.timeAsDouble;
		ShotCountdown shotCountdown = shotA;
		object obj = (object)shotA ^ (object)shotA;
		object obj2 = (object)shotA & obj;
		bool flag = (nint)obj2 < 0;
		bool flag2 = (nint)shotA < 0;
		bool flag3 = shotA == null;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm4,qword ptr [rax+10h]\"");
		bool flag4 = flag2 == flag;
		object obj3 = !flag3;
		object obj4 = flag4 & obj3;
		float num = 0f;
		if (obj4 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rax+10h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
			bool flag5 = 0 >= 0;
			float num2 = 0f;
			if (!flag5)
			{
				num2 = 0f;
			}
			float num3 = shotCountdown.durationSeconds - num2;
			bool flag6 = !(0f < num3);
			num = 0f;
			if (!flag6)
			{
				num = num3;
			}
		}
		ShotCountdown shotCountdown2 = shotB;
		inspectorARemainingSeconds = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm4,qword ptr [rax+10h]\"");
		bool flag7 = (nint)shotB > 0;
		float num4 = 0f;
		if (!flag7)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rax+10h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
			bool flag8 = 0 >= 0;
			float num2 = 0f;
			if (!flag8)
			{
				num2 = 0f;
			}
			float num5 = shotCountdown2.durationSeconds - num2;
			bool flag9 = !(0f < num5);
			num4 = 0f;
			if (!flag9)
			{
				num4 = num5;
			}
		}
		ShotCountdown shotCountdown3 = shotA;
		inspectorBRemainingSeconds = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm4,qword ptr [rax+10h]\"");
		bool flag10 = (nint)shotA > 0;
		float num6 = 0f;
		if (!flag10)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rax+10h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
			bool flag11 = 0 >= 0;
			float num2 = 0f;
			if (!flag11)
			{
				num2 = 0f;
			}
			float num7 = shotCountdown3.durationSeconds - num2;
			bool flag12 = !(0f < num7);
			num6 = 0f;
			if (!flag12)
			{
				num6 = num7;
			}
		}
		bool flag13 = num6 < 0.0001f;
		float num8 = num6 - 0.0001f;
		bool flag14 = num8 == 0f;
		bool flag15 = !flag13;
		bool flag16 = !flag14;
		bool flag17 = flag16 & flag15;
		inspectorAActive = flag17;
		ShotCountdown shotCountdown4 = shotB;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm4,qword ptr [rax+10h]\"");
		bool flag18 = (nint)shotB > 0;
		float num9 = 0f;
		if (!flag18)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rax+10h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
			bool flag19 = 0 >= 0;
			float num2 = 0f;
			if (!flag19)
			{
				num2 = 0f;
			}
			float num10 = shotCountdown4.durationSeconds - num2;
			bool flag20 = !(0f < num10);
			num9 = 0f;
			if (!flag20)
			{
				num9 = num10;
			}
		}
		bool flag21 = num9 < 0.0001f;
		float num11 = num9 - 0.0001f;
		bool flag22 = num11 == 0f;
		bool flag23 = !flag21;
		bool flag24 = !flag22;
		bool flag25 = flag24 & flag23;
		inspectorBActive = flag25;
		float num12 = ComputeLowestRemainingOrFallback(timeAsDouble);
		bool flag26 = !clampOutput;
		float num13 = num12;
		double num14 = timeAsDouble;
		if (!flag26)
		{
			if (!(outputMinSeconds > num12))
			{
				num14 = outputMaxSeconds;
				bool flag27 = !(num12 > outputMaxSeconds);
				num13 = num12;
				if (!flag27)
				{
					num13 = outputMaxSeconds;
				}
			}
			else
			{
				num13 = outputMinSeconds;
				num14 = timeAsDouble;
			}
		}
		float num16;
		if (roundToDecimals > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			float num15 = 10f * num13;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			num16 = num15 / 10f;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			num16 = num13;
		}
		inspectorCurrentOutputSeconds = num16;
	}

	public void ClearShots()
	{
		ShotCountdown shotCountdown = shotA;
		shotCountdown.startTime = -1.0;
		shotCountdown.durationSeconds = 0f;
		ShotCountdown shotCountdown2 = shotB;
		shotCountdown2.startTime = -1.0;
		shotCountdown2.durationSeconds = 0f;
	}

	private void HandleGunAFired()
	{
		StartCountdownForGun(gunA, shotA);
	}

	private void HandleGunBFired()
	{
		StartCountdownForGun(gunB, shotB);
	}

	private void StartCountdownForGun(GunController gun, ShotCountdown shot)
	{
		//IL_00ea: Invalid comparison between I4 and F4
		//IL_00fc: Expected F4, but got I4
		//IL_0125: Expected F4, but got I4
		//IL_021c: Expected F4, but got I4
		//IL_00a5: Invalid comparison between I4 and F4
		//IL_0140: Invalid comparison between I4 and F4
		//IL_0152: Expected F4, but got I4
		//IL_01b3: Invalid comparison between I4 and F4
		//IL_01c5: Expected F4, but got I4
		//IL_01dc: Expected F4, but got I4
		if (!(gun != null))
		{
			return;
		}
		float num;
		float num5;
		if (gun != null)
		{
			if (predictionMode == PredictionMode.UseGunControllerPredictedImpactTime)
			{
				bool flag = !(0f < gun._003CPredictedImpactTime_003Ek__BackingField);
				num = 0f;
				if (!flag)
				{
					num = gun._003CPredictedImpactTime_003Ek__BackingField;
				}
				goto IL_0213;
			}
			ShellBlueprint chamberedShellBlueprint = gun.ChamberedShellBlueprint;
			if (chamberedShellBlueprint != null)
			{
				float adjustedShellSpeed = chamberedShellBlueprint.GetAdjustedShellSpeed();
				bool flag2 = 0f < adjustedShellSpeed;
				float num2 = adjustedShellSpeed;
				if (!flag2)
				{
					num2 = 1f;
				}
				float num3 = gun.MapElevationToRange(gun._003CCurrentElevation_003Ek__BackingField);
				float num4 = num3 / num2;
				bool flag3 = !(0f < num4);
				num = 0f;
				if (!flag3)
				{
					num = num4;
				}
				num5 = 0f;
				goto IL_0205;
			}
		}
		num = 0f;
		goto IL_0213;
		IL_0205:
		double timeAsDouble = Time.timeAsDouble;
		shot.startTime = timeAsDouble;
		bool flag4 = !(0f < num);
		float num6 = 0f;
		if (!flag4)
		{
			num6 = num;
		}
		if (num5 < num6)
		{
			num5 = num6;
		}
		shot.durationSeconds = num5;
		return;
		IL_0213:
		num5 = 0f;
		goto IL_0205;
	}

	private float GetTravelTimeAtFireTimeSeconds(GunController gun)
	{
		//IL_00c9: Invalid comparison between I4 and F4
		//IL_00db: Expected F4, but got I4
		//IL_0101: Expected F4, but got I4
		//IL_0084: Invalid comparison between I4 and F4
		//IL_0134: Invalid comparison between I4 and F4
		//IL_0146: Expected F4, but got I4
		if (gun != null)
		{
			if (predictionMode == PredictionMode.UseGunControllerPredictedImpactTime)
			{
				bool flag = !(0f < gun._003CPredictedImpactTime_003Ek__BackingField);
				float result = 0f;
				if (!flag)
				{
					result = gun._003CPredictedImpactTime_003Ek__BackingField;
				}
				return result;
			}
			ShellBlueprint chamberedShellBlueprint = gun.ChamberedShellBlueprint;
			if (chamberedShellBlueprint != null)
			{
				float adjustedShellSpeed = chamberedShellBlueprint.GetAdjustedShellSpeed();
				bool flag2 = 0f < adjustedShellSpeed;
				float num = adjustedShellSpeed;
				if (!flag2)
				{
					num = 1f;
				}
				float num2 = gun.MapElevationToRange(gun._003CCurrentElevation_003Ek__BackingField);
				float num3 = num2 / num;
				bool flag3 = !(0f < num3);
				float result2 = 0f;
				if (!flag3)
				{
					result2 = num3;
				}
				return result2;
			}
		}
		return 0f;
	}

	private float ComputeLowestRemainingOrFallback(double now)
	{
		//IL_008c: Expected O, but got I4
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_00a2: Expected F4, but got I4
		//IL_03a0: Invalid comparison between F4 and I4
		//IL_03c9: Expected O, but got I4
		//IL_00e0: Expected F4, but got I4
		//IL_012d: Expected F4, but got I4
		//IL_0405: Invalid comparison between I4 and F4
		//IL_0417: Expected F4, but got I4
		//IL_044d: Invalid comparison between F4 and I4
		//IL_0476: Expected O, but got I4
		//IL_00f7: Expected F4, but got I4
		//IL_022a: Expected F4, but got I4
		//IL_016b: Expected F4, but got I4
		//IL_04b3: Invalid comparison between I4 and F4
		//IL_04c5: Expected F4, but got I4
		//IL_0268: Expected O, but got I4
		//IL_0182: Expected F4, but got I4
		//IL_02be: Expected O, but got I4
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Expected O, but got Unknown
		//IL_02d4: Expected F4, but got I4
		//IL_0545: Invalid comparison between I4 and F4
		//IL_0557: Expected F4, but got I4
		//IL_027f: Expected O, but got I4
		//IL_0312: Expected O, but got I4
		//IL_05a1: Invalid comparison between I4 and F4
		//IL_05b3: Expected F4, but got I4
		//IL_0329: Expected O, but got I4
		ShotCountdown shotCountdown = shotA;
		object obj = (object)shotA ^ (object)shotA;
		object obj2 = (object)shotA & obj;
		bool flag = (nint)obj2 < 0;
		bool flag2 = (nint)shotA < 0;
		bool flag3 = shotA == null;
		float num9;
		float num11;
		if (!flag3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm5,qword ptr [rax+10h]\"");
			bool flag4 = flag2 == flag;
			object obj3 = !flag3;
			object obj4 = flag4 & obj3;
			float num = 0f;
			if (obj4 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rax+10h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
				bool flag5 = 0 >= 0;
				float num2 = 0f;
				if (!flag5)
				{
					num2 = 0f;
				}
				float num3 = shotCountdown.durationSeconds - num2;
				bool flag6 = !(0f < num3);
				num = 0f;
				if (!flag6)
				{
					num = num3;
				}
			}
			ShotCountdown shotCountdown2 = shotB;
			bool flag7 = num < 0.0001f;
			float num4 = num - 0.0001f;
			bool flag8 = num4 == 0f;
			bool flag9 = !flag7;
			bool flag10 = !flag8;
			object obj5 = flag10 & flag9;
			if (shotB != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm5,qword ptr [rax+10h]\"");
				bool flag11 = (nint)shotB > 0;
				float num5 = 0f;
				if (!flag11)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rax+10h]\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
					bool flag12 = 0 >= 0;
					float num6 = 0f;
					if (!flag12)
					{
						num6 = 0f;
					}
					float num7 = shotCountdown2.durationSeconds - num6;
					bool flag13 = !(0f < num7);
					num5 = 0f;
					if (!flag13)
					{
						num5 = num7;
					}
				}
				bool flag14 = num5 < 0.0001f;
				float num8 = num5 - 0.0001f;
				bool flag15 = num8 == 0f;
				bool flag16 = !flag14;
				bool flag17 = !flag15;
				object obj6 = flag17 & flag16;
				bool flag18;
				bool flag19;
				bool flag20;
				if (obj5 == null)
				{
					object obj7 = obj6 ^ obj6;
					object obj8 = obj6 & obj7;
					flag18 = (nint)obj8 < 0;
					flag19 = (nint)obj6 < 0;
					flag20 = obj6 == null;
					if (flag20)
					{
						return noInFlightFallbackSeconds;
					}
					num9 = 1f / 0f;
				}
				else
				{
					ShotCountdown shotCountdown3 = shotA;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm5,qword ptr [rax+10h]\"");
					bool flag21 = (nint)obj5 > 0;
					num9 = 0f;
					if (!flag21)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rax+10h]\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
						bool flag22 = 0 >= 0;
						object obj9 = 0;
						if (!flag22)
						{
							obj9 = 0;
						}
						float num10 = shotCountdown3.durationSeconds - (float)obj9;
						bool flag23 = !(0f < num10);
						num9 = 0f;
						if (!flag23)
						{
							num9 = num10;
						}
					}
					object obj10 = obj6 ^ obj6;
					object obj11 = obj6 & obj10;
					flag18 = (nint)obj11 < 0;
					flag19 = (nint)obj6 < 0;
					flag20 = obj6 == null;
					if (flag20)
					{
						num11 = 1f / 0f;
						goto IL_0565;
					}
				}
				ShotCountdown shotCountdown4 = shotB;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm5,qword ptr [rax+10h]\"");
				bool flag24 = flag19 == flag18;
				object obj12 = !flag20;
				object obj13 = flag24 & obj12;
				num11 = 0f;
				if (obj13 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,qword ptr [rax+10h]\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm6\"");
					bool flag25 = 0 >= 0;
					object obj14 = 0;
					if (!flag25)
					{
						obj14 = 0;
					}
					float num12 = shotCountdown4.durationSeconds - (float)obj14;
					bool flag26 = !(0f < num12);
					float num13 = 0f;
					if (!flag26)
					{
						num13 = num12;
					}
					if (num9 > num13)
					{
						num9 = num13;
					}
					return num9;
				}
				goto IL_0565;
			}
		}
		throw new NullReferenceException();
		IL_0565:
		if (num9 > num11)
		{
			num9 = num11;
		}
		return num9;
	}

	private static float Round(float value, int decimals)
	{
		if (decimals > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			float num = 10f * value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			return num / 10f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		return value;
	}

	private static void Subscribe(GunController gun, Action handler)
	{
		//IL_00e0: Expected O, but got I4
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_012b: Expected O, but got I4
		if (!(gun != null) || handler == null)
		{
			return;
		}
		if ((object)gun == null)
		{
			NullReferenceException ex = new NullReferenceException();
			object obj = 0;
			UnityEngine.Object obj2 = gun;
		}
		else
		{
			object obj3 = gun + 168;
			Delegate obj4 = gun.OnShellLaunched;
			Delegate obj7 = default(Delegate);
			while (true)
			{
				Delegate obj5 = Delegate.Combine(obj4, handler);
				bool flag = (object)obj5 == null;
				Delegate obj6 = null;
				if (!flag)
				{
					bool flag2 = (object)obj5.GetType() != typeof(Action);
					obj6 = null;
					if (!flag2)
					{
						obj6 = obj5;
					}
					bool flag3 = (object)obj6 == null;
					object obj = 0;
					NullReferenceException ex = (NullReferenceException)(object)obj5;
					UnityEngine.Object obj2 = (UnityEngine.Object)(object)typeof(Action);
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj7 != obj4;
				obj4 = obj7;
				if (!flag4)
				{
					return;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private static void Unsubscribe(GunController gun, Action handler)
	{
		//IL_00e0: Expected O, but got I4
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_012b: Expected O, but got I4
		if (!(gun != null) || handler == null)
		{
			return;
		}
		if ((object)gun == null)
		{
			NullReferenceException ex = new NullReferenceException();
			object obj = 0;
			UnityEngine.Object obj2 = gun;
		}
		else
		{
			object obj3 = gun + 168;
			Delegate obj4 = gun.OnShellLaunched;
			Delegate obj7 = default(Delegate);
			while (true)
			{
				Delegate obj5 = Delegate.Remove(obj4, handler);
				bool flag = (object)obj5 == null;
				Delegate obj6 = null;
				if (!flag)
				{
					bool flag2 = (object)obj5.GetType() != typeof(Action);
					obj6 = null;
					if (!flag2)
					{
						obj6 = obj5;
					}
					bool flag3 = (object)obj6 == null;
					object obj = 0;
					NullReferenceException ex = (NullReferenceException)(object)obj5;
					UnityEngine.Object obj2 = (UnityEngine.Object)(object)typeof(Action);
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj7 != obj4;
				obj4 = obj7;
				if (!flag4)
				{
					return;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}
}
