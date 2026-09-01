using System;
using System.Collections.Generic;
using System.Reflection;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ShellSlotPool : MonoBehaviour
{
	public enum ShellInsertionMode
	{
		FirstAvailable,
		RoundRobin,
		Random,
		FillOneThenNext,
		RightOnly,
		LeftOnly
	}

	public enum ShellSlotSides
	{
		Left,
		Right
	}

	public enum ShellSource
	{
		Mission,
		Punchcard
	}

	private enum RemainingBucket
	{
		Other = 3,
		Two = 2,
		One = 1,
		Zero = 0
	}

	public List<CylinderShellSelector> selectors;

	public int nextRoundRobinIndex;

	public bool autoDetectCapacity;

	public int capacityOverride;

	public bool cacheDetectedCapacity;

	public float pollInterval;

	public bool invokeOnStart;

	public bool reFireOnReEntry;

	public UnityEvent onTwoRemaining;

	public UnityEvent onOneRemaining;

	public UnityEvent onEmpty;

	private int _cachedDetectedCapacity;

	private bool _warnedCapacityUnknown;

	private RemainingBucket _lastBucket;

	private float _nextPollAt;

	private HashSet<RemainingBucket> _firedEver;

	private static readonly string[] CapacityPropertyNames = new string[6] { "TotalSlots", "TotalSlotCount", "SlotCount", "Capacity", "SlotCapacity", "ChamberCount" };

	private static readonly string[] CapacityMethodNames = new string[7] { "TotalSlots", "TotalSlotCount", "GetTotalSlots", "GetSlotCount", "Capacity", "GetCapacity", "GetChamberCount" };

	public int TotalEmptySlots()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		int num = 0;
		List<CylinderShellSelector>.Enumerator enumerator = default(List<CylinderShellSelector>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((bool)obj)
				{
					if ((object)obj == null)
					{
						break;
					}
					int num2 = ((CylinderShellSelector)obj).EmptySlotCount();
					num += num2;
				}
				continue;
			}
			enumerator.Dispose();
			return num;
		}
		throw new NullReferenceException();
	}

	public bool HasEmptySlot()
	{
		int num = TotalEmptySlots();
		int num2 = num ^ num;
		int num3 = num & num2;
		bool flag = num3 < 0;
		bool flag2 = num < 0;
		bool flag3 = num == 0;
		bool flag4 = flag2 == flag;
		bool flag5 = !flag3;
		return flag5 & flag4;
	}

	public bool InsertShell(ShellDefinition shell, ShellInsertionMode mode, ShellSource source, out CylinderShellSelector usedSelector, out int slotIndex)
	{
		//IL_00f7: Expected O, but got I4
		//IL_0104: Expected O, but got I8
		//IL_00b4: Expected O, but got I8
		//IL_00ce: Expected O, but got I8
		object obj = 0;
		object obj2 = 4294967295L;
		if (shell != null)
		{
			if ((object)shell == null)
			{
				throw new NullReferenceException();
			}
			if (shell.ImpactEffectPrefab != null && !(shell.BlueprintPrefab == null) && mode <= ShellInsertionMode.LeftOnly)
			{
				object obj3 = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ rdx_v6+565B0C+mode @ r8 (ShellSlotPool+ShellInsertionMode)*4]");
				object obj4 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v205 @ rcx_v11 (should have been resolved before IL gen)");
				throw new NullReferenceException();
			}
		}
		return false;
	}

	public int GetTotalCapacity()
	{
		int num;
		if (autoDetectCapacity)
		{
			num = GetDetectedCapacity();
			if (num > 0)
			{
				goto IL_007d;
			}
		}
		bool flag = capacityOverride < 0;
		num = 0;
		if (!flag)
		{
			num = capacityOverride;
		}
		goto IL_007d;
		IL_007d:
		return num;
	}

	public int GetTotalRemainingShells()
	{
		//IL_00f5: Expected I4, but got I8
		bool flag;
		int num;
		if (autoDetectCapacity)
		{
			int detectedCapacity = GetDetectedCapacity();
			flag = detectedCapacity < 0;
			bool flag2 = detectedCapacity > 0;
			num = detectedCapacity;
			if (flag2)
			{
				goto IL_0094;
			}
		}
		bool flag3 = capacityOverride < 0;
		num = 0;
		if (!flag3)
		{
			num = capacityOverride;
		}
		flag = num < 0;
		if (num <= 0)
		{
			return -1;
		}
		goto IL_0094;
		IL_0094:
		int num2 = TotalEmptySlots();
		int num3 = num - num2;
		if (!flag)
		{
			if (num3 > num)
			{
				return num;
			}
		}
		else
		{
			num3 = 0;
		}
		return num3;
	}

	public void RecalculateAndDispatch()
	{
		//IL_0031: Expected I4, but got I8
		if (!cacheDetectedCapacity)
		{
			_cachedDetectedCapacity = -1;
		}
		CheckAndDispatchThresholds();
	}

	private void Awake()
	{
		_lastBucket = RemainingBucket.Other;
		_firedEver.Clear();
	}

	private void OnEnable()
	{
		//IL_0031: Expected I4, but got I8
		if (!cacheDetectedCapacity)
		{
			_cachedDetectedCapacity = -1;
		}
		if (invokeOnStart)
		{
			CheckAndDispatchThresholds(forceTransition: true);
		}
		float time = Time.time;
		float nextPollAt = time + pollInterval;
		_nextPollAt = nextPollAt;
	}

	private void Update()
	{
		//IL_000b: Invalid comparison between I4 and F4
		if (0f < pollInterval)
		{
			float time = Time.time;
			if (time < _nextPollAt)
			{
				return;
			}
			float time2 = Time.time;
			float nextPollAt = time2 + pollInterval;
			_nextPollAt = nextPollAt;
		}
		CheckAndDispatchThresholds();
	}

	private void OnValidate()
	{
		//IL_000b: Invalid comparison between I4 and F4
		if (0f > pollInterval)
		{
			pollInterval = 0f;
		}
		if (capacityOverride < 0)
		{
			capacityOverride = 0;
		}
	}

	private unsafe void CheckAndDispatchThresholds(bool forceTransition = false)
	{
		//IL_0215: Expected O, but got I4
		bool flag;
		int num;
		if (autoDetectCapacity)
		{
			int detectedCapacity = GetDetectedCapacity();
			flag = detectedCapacity < 0;
			bool flag2 = detectedCapacity > 0;
			num = detectedCapacity;
			if (flag2)
			{
				goto IL_007a;
			}
		}
		bool flag3 = capacityOverride < 0;
		num = 0;
		if (!flag3)
		{
			num = capacityOverride;
		}
		flag = num < 0;
		if (num > 0)
		{
			goto IL_007a;
		}
		goto IL_029d;
		IL_01ea:
		int num2;
		bool flag4 = num2 == 0;
		UnityEvent unityEvent;
		if (!flag4)
		{
			object obj = num2 - 1;
			if (!flag4)
			{
				if ((nint)obj != 1)
				{
					goto IL_027e;
				}
				unityEvent = onTwoRemaining;
			}
			else
			{
				unityEvent = onOneRemaining;
			}
		}
		else
		{
			unityEvent = onEmpty;
		}
		unityEvent?.Invoke();
		goto IL_027e;
		IL_007a:
		int num3 = TotalEmptySlots();
		int num4 = num - num3;
		if (!flag)
		{
			if (num4 > num)
			{
				num4 = num;
			}
		}
		else
		{
			num4 = 0;
		}
		int num6 = default(int);
		if (num4 >= 0)
		{
			switch (num4)
			{
			default:
			{
				int num5 = -num4;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb edi,edi\"");
				num2 = num5 & 3;
				break;
			}
			case 1:
				num2 = 1;
				break;
			case 2:
				num2 = 2;
				break;
			}
			bool flag5 = num2 == (int)_lastBucket;
			bool flag6 = forceTransition;
			if (!flag5)
			{
				flag6 = true;
			}
			if (!flag6)
			{
				if (reFireOnReEntry == flag6)
				{
					bool flag7 = _firedEver.Contains((RemainingBucket)(int)(&num6));
					bool flag8 = !flag7;
					num6 = num2;
					if (flag8)
					{
						goto IL_01ea;
					}
				}
				if (num2 != (int)_lastBucket)
				{
					_lastBucket = (RemainingBucket)num2;
				}
				return;
			}
			goto IL_01ea;
		}
		goto IL_029d;
		IL_027e:
		_firedEver.Add((RemainingBucket)(int)(&num6));
		_lastBucket = (RemainingBucket)num2;
		return;
		IL_029d:
		if (!_warnedCapacityUnknown)
		{
			_warnedCapacityUnknown = true;
			Debug.LogWarning("[ShellSlotPool] Total capacity is unknown. Threshold events are disabled. Enable auto detection or set 'capacityOverride' to the total slots across all selectors.", this);
		}
	}

	private static RemainingBucket BucketFromRemaining(int remaining)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected I4, but got Unknown
		switch (remaining)
		{
		default:
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb eax,eax\"");
			object obj = default(object);
			return (RemainingBucket)(obj & 3);
		}
		case 1:
			return RemainingBucket.One;
		case 2:
			return RemainingBucket.Two;
		}
	}

	private int GetDetectedCapacity()
	{
		if (cacheDetectedCapacity && _cachedDetectedCapacity >= 0)
		{
			return _cachedDetectedCapacity;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		int num = 0;
		List<CylinderShellSelector>.Enumerator enumerator = default(List<CylinderShellSelector>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if ((bool)obj)
			{
				int num2 = TryGetCapacityFromSelector(obj);
				num += num2;
			}
		}
		enumerator.Dispose();
		if (cacheDetectedCapacity)
		{
			bool flag = num >= 0;
			int cachedDetectedCapacity = num;
			if (!flag)
			{
				cachedDetectedCapacity = 0;
			}
			_cachedDetectedCapacity = cachedDetectedCapacity;
		}
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	private static int TryGetCapacityFromSelector(object selector)
	{
		//IL_0018: Expected O, but got I4
		//IL_0529: Expected I4, but got O
		//IL_027a: Expected O, but got I4
		//IL_00a1: Expected O, but got I
		//IL_00cc: Expected O, but got I4
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Expected O, but got Unknown
		//IL_02f4: Expected O, but got I
		//IL_0124: Expected O, but got I
		//IL_031f: Expected O, but got I4
		//IL_015b: Expected O, but got I4
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Expected O, but got Unknown
		//IL_018f: Expected O, but got I4
		//IL_01aa: Expected I, but got O
		//IL_01ba: Expected O, but got I
		//IL_01dc: Expected O, but got I
		//IL_0377: Expected O, but got I
		//IL_03ae: Expected O, but got I4
		//IL_0209: Expected I, but got O
		//IL_03e3: Expected O, but got I
		//IL_024a: Expected I4, but got O
		//IL_03f0: Expected I, but got O
		//IL_043e: Expected I4, but got O
		if (selector != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			object obj = 0;
			Type type = default(Type);
			Type[] types = default(Type[]);
			ParameterModifier[] modifiers = default(ParameterModifier[]);
			object obj4 = default(object);
			object obj8 = default(object);
			object obj10 = default(object);
			object obj11 = default(object);
			while (true)
			{
				string[] capacityPropertyNames = CapacityPropertyNames;
				int result;
				PropertyInfo propertyInfo;
				object obj7;
				if (CapacityPropertyNames != null)
				{
					if ((nint)obj >= capacityPropertyNames.Length)
					{
						object obj2 = 0;
						while (true)
						{
							string[] capacityMethodNames = CapacityMethodNames;
							if (CapacityMethodNames == null)
							{
								break;
							}
							if ((nint)obj2 >= capacityMethodNames.Length)
							{
								goto end_IL_04ba;
							}
							propertyInfo = (PropertyInfo)(object)CapacityMethodNames;
							if (CapacityMethodNames == null)
							{
								break;
							}
							object obj3 = obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v493 @ rcx_v9 (System.Reflection.PropertyInfo)+18]");
							if ((nint)obj3 < 0)
							{
								if ((object)type == null)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v493 @ rcx_v9 (System.Reflection.PropertyInfo)+20+v76 @ rdi_v12*8]");
								MethodInfo method = type.GetMethod((string)0, (BindingFlags)20, null, types, modifiers);
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
								bool flag = obj4 == null;
								object obj5 = 0;
								Binder binder = null;
								if (flag)
								{
									goto IL_05a7;
								}
								if ((object)method == null)
								{
									break;
								}
								Type returnType = method.ReturnType;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
								RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
								Type typeFromHandle = Type.GetTypeFromHandle(handle);
								bool flag2 = ((object)returnType).Equals((object)typeFromHandle);
								bool flag3 = !flag2;
								obj5 = 0;
								binder = null;
								if (flag3)
								{
									goto IL_05a7;
								}
								object obj6 = method.Invoke(selector, null);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
								obj5 = 0;
								nint num = (nint)obj6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v548 @ rdx_v21 (Il2CppClass<System.Object>)+40]");
								nint num2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ r8_v4+40]");
								bool flag4 = num2 != 0;
								binder = null;
								obj7 = obj6;
								if (!flag4)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
									result = (int)obj8;
									if ((nint)obj8 <= 0)
									{
										binder = null;
										goto IL_05a7;
									}
									goto IL_0465;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
								throw new NullReferenceException();
							}
							goto IL_0508;
							IL_05a7:
							obj2++;
						}
					}
					else
					{
						propertyInfo = (PropertyInfo)(object)CapacityPropertyNames;
						if (CapacityPropertyNames != null)
						{
							object obj9 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v493 @ rcx_v9 (System.Reflection.PropertyInfo)+18]");
							if ((nint)obj9 >= 0)
							{
								goto IL_0508;
							}
							if ((object)type != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v493 @ rcx_v9 (System.Reflection.PropertyInfo)+20+v212 @ rdi_v4*8]");
								PropertyInfo property = type.GetProperty((string)0, (BindingFlags)20);
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
								bool flag5 = obj10 == null;
								object obj5 = 0;
								Binder binder = null;
								if (flag5)
								{
									goto IL_0529;
								}
								if ((object)property != null)
								{
									Type propertyType = property.PropertyType;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
									RuntimeTypeHandle handle2 = (RuntimeTypeHandle)((nint)0 + (nint)32);
									Type typeFromHandle2 = Type.GetTypeFromHandle(handle2);
									bool flag6 = ((object)propertyType).Equals((object)typeFromHandle2);
									bool flag7 = !flag6;
									obj5 = 0;
									binder = null;
									if (!flag7)
									{
										bool canRead = property.CanRead;
										bool flag8 = !canRead;
										obj5 = 0;
										binder = null;
										if (!flag8)
										{
											nint num3 = (nint)property;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v741 @ r9_v14 (Il2CppClass<System.Reflection.PropertyInfo>)+2D0]");
											binder = (Binder)0;
											object value = property.GetValue(selector, null);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
											obj5 = 0;
											bool flag9 = value == null;
											propertyInfo = property;
											if (!flag9)
											{
												nint num4 = (nint)value;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v618 @ rdx_v33 (Il2CppClass<System.Object>)+40]");
												nint num5 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ r8_v4+40]");
												if (num5 == 0)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
													result = (int)obj11;
													if ((nint)obj11 <= 0)
													{
														goto IL_0529;
													}
													goto IL_0465;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
												propertyInfo = (PropertyInfo)value;
											}
											throw new NullReferenceException();
										}
									}
									goto IL_0529;
								}
							}
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
				IL_0529:
				obj++;
				continue;
				IL_0465:
				return result;
				IL_0508:
				obj7 = propertyInfo;
				throw new IndexOutOfRangeException();
				continue;
				end_IL_04ba:
				break;
			}
		}
		return 0;
	}

	public ShellSlotPool()
	{
		//IL_0040: Expected I4, but got I8
		List<CylinderShellSelector> list = new List<CylinderShellSelector>();
		selectors = list;
		autoDetectCapacity = true;
		cacheDetectedCapacity = true;
		pollInterval = 0.1f;
		invokeOnStart = true;
		_cachedDetectedCapacity = -1;
		_lastBucket = RemainingBucket.Other;
		_firedEver = new HashSet<RemainingBucket>();
		base._002Ector();
	}
}
