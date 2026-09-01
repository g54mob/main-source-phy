using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class RotationAngleClickEventTrigger : MonoBehaviour
{
	public enum Axis
	{
		X,
		Y,
		Z
	}

	public enum TriggerMode
	{
		DegreesPerClick,
		SpecificAnglesList
	}

	public enum TriggerStyle
	{
		OnCrossBoundaries,
		OnReachBoundary
	}

	public Transform target;

	public Axis axis;

	public TriggerMode triggerMode;

	public float degreesPerClick;

	public List<float> specificAnglesDeg;

	public TriggerStyle triggerStyle;

	public float deadbandDeg;

	public float cooldownSeconds;

	public bool capClicksPerSecond;

	public int maxClicksPerSecond;

	public UnityEvent OnClick;

	public float currentLocalSignedAngleDeg;

	public float previousLocalSignedAngleDeg;

	public int totalInvokesFired;

	public float lastInvokeTime;

	public int invokesInWindow;

	public List<float> activeBoundariesPreview;

	private Transform _effectiveTarget;

	private float _windowStartTime;

	private List<float> _specificNormalized;

	private int _specificHash;

	private void Awake()
	{
		Transform effectiveTarget = ((!(target != null)) ? base.transform : target);
		_effectiveTarget = effectiveTarget;
	}

	private void OnEnable()
	{
		Transform effectiveTarget = ((!(target != null)) ? base.transform : target);
		_effectiveTarget = effectiveTarget;
		float num = (previousLocalSignedAngleDeg = ReadLocalSignedAngleDeg());
		totalInvokesFired = 0;
		currentLocalSignedAngleDeg = num;
		lastInvokeTime = -999f;
		float time = Time.time;
		_windowStartTime = time;
		invokesInWindow = 0;
		RebuildSpecificListIfNeeded(force: true);
		UpdateActiveBoundariesPreview();
	}

	private void Update()
	{
		//IL_05e8: Invalid comparison between F4 and I4
		//IL_00ff: Invalid comparison between I4 and F4
		//IL_011f: Expected F4, but got I4
		//IL_0fd2: Invalid comparison between I4 and F4
		//IL_0164: Expected O, but got I4
		//IL_016d: Expected O, but got I4
		//IL_0d5f: Invalid comparison between I4 and F4
		//IL_0ff9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffe: Expected O, but got Unknown
		//IL_1006: Invalid comparison between F4 and O
		//IL_0185: Invalid comparison between I4 and F4
		//IL_0737: Invalid comparison between O and F4
		//IL_09e2: Invalid comparison between F8 and I4
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1032: Expected O, but got Unknown
		//IL_103a: Invalid comparison between F4 and O
		//IL_0c5f: Invalid comparison between I4 and F4
		//IL_0e6f: Invalid comparison between F4 and I4
		//IL_0756: Invalid comparison between F4 and O
		//IL_0f72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f77: Expected O, but got Unknown
		//IL_0f7f: Invalid comparison between F4 and O
		//IL_10ce: Invalid comparison between F4 and I4
		//IL_07e9: Invalid comparison between F4 and I4
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Expected O, but got Unknown
		//IL_0906: Expected O, but got I4
		//IL_0fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fab: Expected O, but got Unknown
		//IL_0fb3: Invalid comparison between F4 and O
		//IL_0a3e: Invalid comparison between F4 and I4
		//IL_0e56: Expected O, but got I4
		//IL_0ef9: Expected O, but got I4
		//IL_1081: Unknown result type (might be due to invalid IL or missing references)
		//IL_1086: Expected O, but got Unknown
		//IL_0f19: Expected F8, but got I4
		//IL_0890: Expected O, but got I4
		//IL_0bb9: Invalid comparison between F4 and I4
		//IL_10ff: Invalid comparison between F8 and I4
		//IL_110f: Invalid comparison between I4 and F8
		//IL_0aed: Expected F8, but got I4
		if (_effectiveTarget == null)
		{
			Transform effectiveTarget = ((!(target != null)) ? base.transform : target);
			_effectiveTarget = effectiveTarget;
			if (!(_effectiveTarget != null))
			{
				return;
			}
		}
		RebuildSpecificListIfNeeded(force: false);
		float num = ReadLocalSignedAngleDeg();
		bool flag = triggerStyle == TriggerStyle.OnCrossBoundaries;
		float num2 = deadbandDeg;
		currentLocalSignedAngleDeg = num;
		object obj4 = default(object);
		float num19;
		if (!flag)
		{
			if (0f > deadbandDeg)
			{
				num2 = 0f;
			}
			if (triggerMode != TriggerMode.DegreesPerClick)
			{
				List<float> specificNormalized = _specificNormalized;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v409 @ rax_v58 (System.Collections.Generic.List`1<System.Single>)+18]");
				if ((nint)0 > (nint)0)
				{
					object obj = 0;
					object obj2 = 0;
					bool flag4 = default(bool);
					bool flag6 = default(bool);
					while (true)
					{
						object obj3 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v409 @ rax_v58 (System.Collections.Generic.List`1<System.Single>)+18]");
						if ((nint)obj3 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						bool flag3;
						if (0f < num2)
						{
							float x = currentLocalSignedAngleDeg - (float)obj4;
							float num3 = MathF.FMod(x, 360f);
							if (num3 > 180f)
							{
								num3 += -360f;
							}
							if (!(-180f < num3))
							{
								num3 += 360f;
							}
							float num4 = num3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
							object obj5 = num4 & 0;
							bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5);
							flag3 = !flag2;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
							flag3 = flag4;
						}
						if (0f < num2)
						{
							float x2 = previousLocalSignedAngleDeg - (float)obj4;
							float num5 = MathF.FMod(x2, 360f);
							if (num5 > 180f)
							{
								num5 += -360f;
							}
							if (!(-180f < num5))
							{
								num5 += 360f;
							}
							float num6 = num5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
							object obj6 = num6 & 0;
							bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6);
							flag6 = !flag5;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
						}
						if (flag3 && !flag6 && CanFireNow())
						{
							int num7 = totalInvokesFired + 1;
							totalInvokesFired = num7;
							if (OnClick != null)
							{
								OnClick.Invoke();
							}
							RegisterInvokeForCap();
						}
						specificNormalized = _specificNormalized;
						obj++;
						obj2 = obj;
					}
				}
			}
			else
			{
				bool flag7 = 0.0001f > degreesPerClick;
				float num8 = 0.0001f;
				if (!flag7)
				{
					num8 = degreesPerClick;
				}
				float num9 = num / num8;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				float x3 = num9 * num8;
				float num10 = MathF.FMod(x3, 360f);
				bool flag8 = !(num10 > 180f);
				float num11 = num10;
				if (!flag8)
				{
					num11 = num10 + -360f;
				}
				if (!(-180f < num11))
				{
					num11 += 360f;
				}
				bool flag10;
				if (0f < num2)
				{
					float x4 = currentLocalSignedAngleDeg - num11;
					float num12 = MathF.FMod(x4, 360f);
					if (num12 > 180f)
					{
						num12 += -360f;
					}
					if (!(-180f < num12))
					{
						num12 += 360f;
					}
					float num13 = num12;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj7 = num13 & 0;
					bool flag9 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7);
					flag10 = !flag9;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
					bool flag11 = default(bool);
					flag10 = flag11;
				}
				bool flag13 = default(bool);
				if (0f < num2)
				{
					float x5 = previousLocalSignedAngleDeg - num11;
					float num14 = MathF.FMod(x5, 360f);
					if (num14 > 180f)
					{
						num14 += -360f;
					}
					if (!(-180f < num14))
					{
						num14 += 360f;
					}
					float num15 = num14;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj8 = num15 & 0;
					bool flag12 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8);
					flag13 = !flag12;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				}
				if (flag10 && !flag13 && CanFireNow())
				{
					int num16 = totalInvokesFired + 1;
					totalInvokesFired = num16;
					if (OnClick != null)
					{
						OnClick.Invoke();
					}
					float time = Time.time;
					bool flag14 = !capClicksPerSecond;
					lastInvokeTime = time;
					if (!flag14)
					{
						float time2 = Time.time;
						float num17 = time2 - _windowStartTime;
						if (!(num17 < 1f))
						{
							_windowStartTime = time2;
							invokesInWindow = 0;
						}
						int num18 = invokesInWindow + 1;
						invokesInWindow = num18;
					}
				}
			}
		}
		else
		{
			bool flag15 = !(deadbandDeg > 0f);
			num19 = num;
			if (flag15)
			{
				goto IL_0659;
			}
			if (!IsNearAnyBoundary(previousLocalSignedAngleDeg, deadbandDeg))
			{
				num19 = currentLocalSignedAngleDeg;
				if (!IsNearAnyBoundary(currentLocalSignedAngleDeg, deadbandDeg))
				{
					goto IL_0659;
				}
			}
		}
		goto IL_0bf6;
		IL_0659:
		if (triggerMode != TriggerMode.DegreesPerClick)
		{
			List<float> specificNormalized2 = _specificNormalized;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rax_v32 (System.Collections.Generic.List`1<System.Single>)+18]");
			if ((nint)0 > (nint)0)
			{
				float num20 = previousLocalSignedAngleDeg;
				float num21 = ((!(currentLocalSignedAngleDeg > previousLocalSignedAngleDeg)) ? currentLocalSignedAngleDeg : previousLocalSignedAngleDeg);
				List<float> specificNormalized3 = _specificNormalized;
				bool flag16 = num20 > currentLocalSignedAngleDeg;
				int num22 = 0;
				int num23 = 0;
				int num24 = 0;
				if (!flag16)
				{
					num22 = 0;
					num23 = 0;
					num20 = currentLocalSignedAngleDeg;
					num24 = 0;
				}
				while (true)
				{
					int num25 = num24;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v536 @ rax_v36 (System.Collections.Generic.List`1<System.Single>)+18]");
					if ((nint)num25 >= (nint)0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num21) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num20) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
					{
						num22++;
					}
					specificNormalized3 = _specificNormalized;
					num23++;
					bool flag17 = _specificNormalized != null;
					num24 = num23;
					if (!flag17)
					{
						throw new NullReferenceException();
					}
				}
				if (num22 > 0)
				{
					List<float> list = default(List<float>);
					object obj10;
					do
					{
						bool flag18;
						float num28;
						if (cooldownSeconds > 0f)
						{
							float time3 = Time.time;
							float num26 = time3 - lastInvokeTime;
							num19 = cooldownSeconds;
							float num27 = cooldownSeconds - num26;
							flag18 = num27 == 0f;
							bool flag19 = cooldownSeconds > num26;
							num28 = cooldownSeconds;
							if (flag19)
							{
								goto IL_08fa;
							}
						}
						if (capClicksPerSecond)
						{
							float time4 = Time.time;
							num19 = time4 - _windowStartTime;
							if (!(num19 < 1f))
							{
								_windowStartTime = time4;
								invokesInWindow = 0;
							}
							bool flag20 = maxClicksPerSecond >= 1;
							specificNormalized3 = (List<float>)maxClicksPerSecond;
							if (!flag20)
							{
								specificNormalized3 = (List<float>)1;
							}
							object obj9 = invokesInWindow - specificNormalized3;
							flag18 = obj9 == null;
							bool flag21 = invokesInWindow >= (nint)specificNormalized3;
							num28 = num19;
							if (flag21)
							{
								goto IL_08fa;
							}
						}
						int num29 = totalInvokesFired + 1;
						totalInvokesFired = num29;
						flag18 = OnClick == null;
						if (!flag18)
						{
							OnClick.Invoke();
						}
						RegisterInvokeForCap();
						num28 = num19;
						specificNormalized3 = list;
						goto IL_08fa;
						IL_08fa:
						obj10 = !flag18;
						num19 = num28;
					}
					while (obj10 != null);
				}
			}
		}
		else
		{
			bool flag22 = 0.0001f > degreesPerClick;
			float num30 = 0.0001f;
			if (!flag22)
			{
				num30 = degreesPerClick;
			}
			float num31 = currentLocalSignedAngleDeg / num30;
			float num32 = previousLocalSignedAngleDeg / num30;
			float num33 = ((!(num31 > num32)) ? num31 : num32);
			if (!(num32 > num31))
			{
				num32 = num31;
			}
			double num34 = Math.Ceiling(num33);
			double num35 = Math.Floor(num32);
			if (!(num35 < num34))
			{
				double num36 = num35 - num34;
				double num37 = num36 + 1.0;
				if (num37 > 0.0)
				{
					double num44 = default(double);
					object obj11;
					do
					{
						bool flag23;
						float num40;
						if (cooldownSeconds > 0f)
						{
							float time5 = Time.time;
							float num38 = time5 - lastInvokeTime;
							num30 = cooldownSeconds;
							float num39 = cooldownSeconds - num38;
							flag23 = num39 == 0f;
							bool flag24 = cooldownSeconds > num38;
							num40 = cooldownSeconds;
							if (flag24)
							{
								goto IL_0eed;
							}
						}
						bool flag25 = !capClicksPerSecond;
						double num41 = num37;
						if (!flag25)
						{
							float time6 = Time.time;
							num30 = time6 - _windowStartTime;
							if (!(num30 < 1f))
							{
								_windowStartTime = time6;
								invokesInWindow = 0;
							}
							num41 = maxClicksPerSecond;
							if (maxClicksPerSecond < 1)
							{
								num41 = 1.0;
							}
							double num42 = (double)invokesInWindow - num41;
							flag23 = num42 == 0.0;
							bool flag26 = !((double)invokesInWindow < num41);
							num40 = num30;
							num37 = num41;
							if (flag26)
							{
								goto IL_0eed;
							}
						}
						int num43 = totalInvokesFired + 1;
						totalInvokesFired = num43;
						if (OnClick != null)
						{
							OnClick.Invoke();
							num41 = num44;
						}
						float time7 = Time.time;
						lastInvokeTime = time7;
						flag23 = !capClicksPerSecond;
						num40 = num30;
						num37 = num41;
						if (!flag23)
						{
							float time8 = Time.time;
							num40 = time8 - _windowStartTime;
							float num45 = num40 - 1f;
							flag23 = num45 == 0f;
							if (!(num40 < 1f))
							{
								_windowStartTime = time8;
								invokesInWindow = 0;
							}
							int num46 = invokesInWindow + 1;
							invokesInWindow = num46;
							num37 = num41;
						}
						goto IL_0eed;
						IL_0eed:
						obj11 = !flag23;
						num30 = num40;
					}
					while (obj11 != null);
				}
			}
		}
		goto IL_0bf6;
		IL_0bf6:
		previousLocalSignedAngleDeg = currentLocalSignedAngleDeg;
		UpdateActiveBoundariesPreview();
	}

	private float ReadLocalSignedAngleDeg()
	{
		//IL_0113: Expected I, but got O
		//IL_012c: Expected F4, but got O
		//IL_013c: Expected F4, but got I
		//IL_0072: Expected O, but got I4
		float x;
		float num;
		if (_effectiveTarget != null)
		{
			Vector3 localEulerAngles = _effectiveTarget.localEulerAngles;
			x = localEulerAngles.x;
			num = localEulerAngles.z;
		}
		else
		{
			nint num2 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num3 = 0;
			x = (float)Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rcx_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			num = 0f;
		}
		bool flag = axis == Axis.X;
		if (!flag)
		{
			object obj = axis - 1;
			float num4 = default(float);
			x = ((!flag && (nint)obj == 1) ? num : num4);
		}
		float num5 = MathF.FMod(x, 360f);
		if (num5 > 180f)
		{
			num5 += -360f;
		}
		if (!(-180f < num5))
		{
			num5 += 360f;
		}
		return num5;
	}

	private static float ToSigned180(float degrees0To360)
	{
		float num = MathF.FMod(degrees0To360, 360f);
		if (num > 180f)
		{
			num += -360f;
		}
		if (!(-180f < num))
		{
			num += 360f;
		}
		return num;
	}

	private static float NormalizeSigned(float angleDeg)
	{
		float num = MathF.FMod(angleDeg, 360f);
		if (num > 180f)
		{
			num += -360f;
		}
		if (!(-180f < num))
		{
			num += 360f;
		}
		return num;
	}

	private bool IsNearAnyBoundary(float angleDeg, float deadband)
	{
		//IL_01ab: Invalid comparison between I4 and F4
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_01a2: Expected I4, but got O
		//IL_0056: Expected O, but got I4
		//IL_005f: Expected O, but got I4
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_02a7: Invalid comparison between F4 and O
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		if (0f < deadband)
		{
			if (triggerMode == TriggerMode.DegreesPerClick)
			{
				bool flag = 0.0001f > degreesPerClick;
				float num = 0.0001f;
				if (!flag)
				{
					num = degreesPerClick;
				}
				float num2 = angleDeg / num;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				float num3 = num2 - num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj = num3 & 0;
				float num4 = (float)obj * num;
				bool flag2 = deadband < num4;
				return !flag2;
			}
			List<float> specificNormalized = _specificNormalized;
			if (_specificNormalized == null)
			{
				goto IL_0194;
			}
			object obj2 = 0;
			object obj3 = 0;
			object obj5 = default(object);
			while (true)
			{
				object obj4 = obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v10 (System.Collections.Generic.List`1<System.Single>)+18]");
				if ((nint)obj4 >= 0)
				{
					break;
				}
				if (_specificNormalized != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					float x = angleDeg - (float)obj5;
					float num5 = MathF.FMod(x, 360f);
					if (num5 > 180f)
					{
						num5 += -360f;
					}
					if (!(-180f < num5))
					{
						num5 += 360f;
					}
					float num6 = num5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj6 = num6 & 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj7 = obj6 & 0;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)deadband) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
					{
						return true;
					}
					specificNormalized = _specificNormalized;
					obj2++;
					if (_specificNormalized != null)
					{
						obj3 = obj2;
						continue;
					}
				}
				goto IL_0194;
			}
		}
		return false;
		IL_0194:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private static bool IsInsideZone(float angleDeg, float boundaryDeg, float toleranceDeg)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00d4: Invalid comparison between F4 and O
		if (0f < toleranceDeg)
		{
			float x = angleDeg - boundaryDeg;
			float num = MathF.FMod(x, 360f);
			if (num > 180f)
			{
				num += -360f;
			}
			if (!(-180f < num))
			{
				num += 360f;
			}
			float num2 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num2 & 0;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)toleranceDeg) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
			return !flag;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		bool result = default(bool);
		return result;
	}

	private bool CanFireNow()
	{
		//IL_000b: Invalid comparison between F4 and I4
		if (cooldownSeconds > 0f)
		{
			float time = Time.time;
			float num = time - lastInvokeTime;
			if (cooldownSeconds > num)
			{
				goto IL_00d8;
			}
		}
		if (capClicksPerSecond)
		{
			float time2 = Time.time;
			float num2 = time2 - _windowStartTime;
			if (!(num2 < 1f))
			{
				_windowStartTime = time2;
				invokesInWindow = 0;
			}
			int num3 = maxClicksPerSecond;
			if (maxClicksPerSecond < 1)
			{
				num3 = 1;
			}
			if (invokesInWindow >= num3)
			{
				goto IL_00d8;
			}
		}
		return true;
		IL_00d8:
		return false;
	}

	private void RegisterInvokeForCap()
	{
		float time = Time.time;
		bool flag = !capClicksPerSecond;
		lastInvokeTime = time;
		if (!flag)
		{
			float time2 = Time.time;
			float num = time2 - _windowStartTime;
			if (!(num < 1f))
			{
				_windowStartTime = time2;
				invokesInWindow = 0;
			}
			int num2 = invokesInWindow + 1;
			invokesInWindow = num2;
		}
	}

	private void FireInvoke()
	{
		int num = totalInvokesFired + 1;
		totalInvokesFired = num;
		if (OnClick != null)
		{
			OnClick.Invoke();
		}
	}

	private unsafe void RebuildSpecificListIfNeeded(bool force)
	{
		//IL_000e: Expected O, but got I4
		//IL_0017: Expected F8, but got I4
		//IL_0028: Expected O, but got I4
		//IL_02f9: Expected I4, but got F8
		//IL_0326: Expected O, but got I
		//IL_0333: Invalid comparison between F4 and I4
		//IL_02db: Invalid comparison between F8 and I4
		//IL_0694: Expected I, but got O
		//IL_03a5: Expected O, but got I
		//IL_03d1: Expected O, but got I4
		//IL_03da: Expected O, but got I4
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_06eb: Expected I, but got O
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Expected O, but got Unknown
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Expected O, but got Unknown
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Expected O, but got Unknown
		//IL_062f: Expected F4, but got Ref
		List<float> list = specificAnglesDeg;
		object obj = 0;
		double num = 17.0;
		bool index = force;
		object obj2 = 0;
		float num3 = default(float);
		double num6 = default(double);
		while (true)
		{
			object obj3 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rsi_v1 (System.Collections.Generic.List`1<System.Single>)+18]");
			if ((nint)obj3 >= 0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			float num2 = num3 * 1000f;
			nint num4 = (nint)typeof(Math);
			float num5 = ((List<float>)(object)typeof(Math)).get_Item((int)(&num6));
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rcx_v27 (Il2CppClass<System.Math>)+E4]");
			double num9;
			if ((nint)0 >= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm8\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018055130Dh\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rcx_v27 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm8\"");
					double num7 = Math.Floor(num2);
					double num8 = num * 31.0;
					num = num8 + num7;
					obj++;
					index = (byte)(&num6) != 0;
					obj2 = obj;
					continue;
				}
				object obj4 = num6 & 1;
				bool flag = obj4 == null;
				num9 = num6;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm12\"");
					double num10 = num * 31.0;
					num = num10 + num6;
					obj++;
					index = (byte)(&num6) != 0;
					obj2 = obj;
					continue;
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm7\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018055135Dh\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rcx_v27 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 == 0)
				{
					object obj5 = num6 & 1;
					bool flag2 = obj5 == null;
					num9 = num6;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm12\"");
						double num11 = num * 31.0;
						num = num11 + num6;
						obj++;
						index = (byte)(&num6) != 0;
						obj2 = obj;
						continue;
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm8\"");
					num9 = Math.Ceiling(num2);
				}
			}
			double num12 = num * 31.0;
			num = num12 + num9;
			obj++;
			index = (byte)(&num6) != 0;
			obj2 = obj;
		}
		if (!force && num == (double)_specificHash)
		{
			return;
		}
		_specificHash = (int)num;
		List<float> specificNormalized = _specificNormalized;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rbx_v6 (System.Collections.Generic.List`1<System.Single>)+1C]");
		_ = (nint)0 + (nint)1;
		float num13 = ((List<float>)0).get_Item(index ? 1 : 0);
		if (num13 == 0f)
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rbx_v6 (System.Collections.Generic.List`1<System.Single>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rbx_v6 (System.Collections.Generic.List`1<System.Single>)+10]");
				nint num14 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rbx_v6 (System.Collections.Generic.List`1<System.Single>)+18]");
				Array.Clear((Array)num14, 0, 0);
			}
		}
		HashSet<int> hashSet = new HashSet<int>();
		List<float> list2 = specificAnglesDeg;
		object obj6 = 0;
		object obj7 = 0;
		double num20 = default(double);
		while (true)
		{
			object obj8 = obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v569 @ rcx_v13 (System.Collections.Generic.List`1<System.Single>)+18]");
			if ((nint)obj8 >= 0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			float num15 = MathF.FMod(num3, 360f);
			bool flag3 = !(num15 > 180f);
			float num16 = num15;
			if (!flag3)
			{
				num16 = num15 + -360f;
			}
			if (!(-180f < num16))
			{
				num16 += 360f;
			}
			float num17 = num16 * 1000f;
			nint num18 = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm6,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v17 (Il2CppClass<System.Math>)+E4]");
			double num19;
			double num21;
			if ((nint)0 >= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm8\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180551508h\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v17 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm8\"");
					num19 = Math.Floor(num17);
					goto IL_05d9;
				}
				object obj9 = num20 & 1;
				bool flag4 = obj9 == null;
				num21 = num20;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm12\"");
					num21 = num20;
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [182206D70h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180551539h\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v17 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm8\"");
					num19 = Math.Ceiling(num17);
					goto IL_05d9;
				}
				object obj10 = num20 & 1;
				bool flag5 = obj10 == null;
				num21 = num20;
				if (!flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm12\"");
					num21 = num20;
				}
			}
			goto IL_05e6;
			IL_05d9:
			num21 = num19;
			goto IL_05e6;
			IL_05e6:
			bool flag6 = hashSet.Contains((int)(&num6));
			num6 = num21;
			if (!flag6)
			{
				hashSet.Add((int)(&num6));
				_specificNormalized.Add((nint)(&num6));
				num6 = num16;
			}
			list2 = specificAnglesDeg;
			obj6++;
			obj7 = obj6;
		}
		_specificNormalized.Sort();
	}

	private int ComputeListHash(List<float> list)
	{
		//IL_02ab: Expected I4, but got O
		//IL_000e: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_02d6: Expected I, but got O
		//IL_0266: Expected O, but got I4
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Expected O, but got Unknown
		//IL_028b: Expected I4, but got F8
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_0151: Expected O, but got I4
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_0176: Expected I4, but got F8
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_020a: Expected O, but got I4
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_022f: Expected I4, but got F8
		//IL_0104: Expected O, but got I4
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected I4, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		if (list != null)
		{
			object obj = 0;
			int num = 17;
			object obj2 = 0;
			object obj4 = default(object);
			double num6 = default(double);
			while (true)
			{
				object obj3 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [list @ rdx (System.Collections.Generic.List`1<System.Single>)+18]");
				if ((nint)obj3 >= 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				float num2 = (float)obj4 * 1000f;
				nint num3 = (nint)typeof(Math);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm9\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rcx_v5 (Il2CppClass<System.Math>)+E4]");
				double num7;
				if ((nint)0 >= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm7\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180550BBCh\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rcx_v5 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm7\"");
						double num4 = Math.Floor(num2);
						object obj5 = num * 31;
						double num5 = (double)obj5 + num4;
						obj++;
						num = (int)num5;
						obj2 = obj;
						continue;
					}
					object obj6 = num6 & 1;
					bool flag = obj6 == null;
					num7 = num6;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm8\"");
						object obj7 = num * 31;
						num = (int)(obj7 + num6);
						obj++;
						obj2 = obj;
						continue;
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm10\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180550C0Ch\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rcx_v5 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 == 0)
					{
						object obj8 = num6 & 1;
						bool flag2 = obj8 == null;
						num7 = num6;
						if (!flag2)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm8\"");
							object obj9 = num * 31;
							double num8 = (double)obj9 + num6;
							obj++;
							num = (int)num8;
							obj2 = obj;
							continue;
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm7\"");
						num7 = Math.Ceiling(num2);
					}
				}
				object obj10 = num * 31;
				double num9 = (double)obj10 + num7;
				obj++;
				num = (int)num9;
				obj2 = obj;
			}
			return num;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	private unsafe void UpdateActiveBoundariesPreview()
	{
		//IL_0099: Expected O, but got I
		//IL_0056: Expected O, but got I
		//IL_00b7: Expected O, but got I
		//IL_00d9: Expected O, but got I
		//IL_01fd: Expected O, but got I8
		//IL_018d: Expected F4, but got Ref
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		List<float> list = activeBoundariesPreview;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rbx_v1 (System.Collections.Generic.List`1<System.Single>)+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		IntPtr intPtr = default(IntPtr);
		if (obj == null)
		{
			_ = 0;
			int num = (int)(nint)intPtr;
			Array array = (Array)0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rbx_v1 (System.Collections.Generic.List`1<System.Single>)+18]");
			int num2 = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rbx_v1 (System.Collections.Generic.List`1<System.Single>)+18]");
			bool flag = (nint)0 <= (nint)0;
			int num = (int)(nint)intPtr;
			Array array = (Array)0;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rbx_v1 (System.Collections.Generic.List`1<System.Single>)+10]");
				array = (Array)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rbx_v1 (System.Collections.Generic.List`1<System.Single>)+10]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rbx_v1 (System.Collections.Generic.List`1<System.Single>)+18]");
				Array.Clear((Array)num3, 0, 0);
				num = 0;
			}
		}
		if (triggerMode != TriggerMode.DegreesPerClick)
		{
			activeBoundariesPreview.AddRange(_specificNormalized);
			return;
		}
		bool flag2 = 0.0001f > degreesPerClick;
		float num4 = 0.0001f;
		if (!flag2)
		{
			num4 = degreesPerClick;
		}
		float num5 = currentLocalSignedAngleDeg / num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		object obj2 = 4294967291L;
		float num8 = default(float);
		bool flag3;
		do
		{
			float num6 = (float)obj2 + num5;
			float x = num6 * num4;
			float num7 = MathF.FMod(x, 360f);
			if (num7 > 180f)
			{
				num7 += -360f;
			}
			if (!(-180f < num7))
			{
				num7 += 360f;
			}
			activeBoundariesPreview.Add((nint)(&num8));
			obj2++;
			flag3 = (nint)obj2 <= 5;
			int num2 = 0;
		}
		while (flag3);
	}

	public unsafe RotationAngleClickEventTrigger()
	{
		//IL_0012: Expected F4, but got Ref
		//IL_001f: Expected F4, but got Ref
		//IL_0031: Expected F4, but got Ref
		//IL_003e: Expected F4, but got Ref
		axis = Axis.Y;
		degreesPerClick = 2f;
		object obj = default(object);
		specificAnglesDeg = new List<float>
		{
			(nint)(&obj),
			(nint)(&obj),
			(nint)(&obj),
			(nint)(&obj)
		};
		deadbandDeg = 0.2f;
		cooldownSeconds = 0.05f;
		maxClicksPerSecond = 20;
		lastInvokeTime = -999f;
		activeBoundariesPreview = new List<float>();
		_specificNormalized = new List<float>();
		base._002Ector();
	}
}
