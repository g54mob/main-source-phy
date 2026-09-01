using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class HighPressureSystemManager : MonoBehaviour, IFloatValueProvider
{
	public enum AggregationMode
	{
		Average,
		WorstValve,
		Product
	}

	private string systemId = "Default";

	private AggregationMode aggregationMode;

	private float healthAlertThreshold = -0.1f;

	private bool displayOutputAsPercent;

	private bool logDisplayProviderChanges;

	public UnityEvent<float> OnSystemHealthChanged01;

	public UnityEvent<float> OnHealthBelowThreshold;

	private float debugSystemHealth01 = 1f;

	private bool logHealthChanges;

	private Action<float> m_SystemHealthChanged01;

	private static readonly Dictionary<string, HighPressureSystemManager> registry;

	private readonly List<ValveController> valves;

	private float currentHealth01;

	private bool thresholdWasBreached;

	private float lastProviderValue;

	public string SystemId => systemId;

	public float Health01 => currentHealth01;

	public int ValveCount
	{
		get
		{
			//IL_001d: Expected I4, but got O
			List<ValveController> list = valves;
			if (valves != null)
			{
				return list._size;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public float CurrentValue
	{
		get
		{
			bool flag = !displayOutputAsPercent;
			float num = currentHealth01;
			if (!flag)
			{
				num *= 100f;
			}
			return num;
		}
	}

	public IReadOnlyList<ValveController> RegisteredValves
	{
		get
		{
			if (valves != null)
			{
				return valves.AsReadOnly();
			}
			return (IReadOnlyList<ValveController>)new NullReferenceException();
		}
	}

	public event Action<float> SystemHealthChanged01
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 80;
			Delegate obj2 = this.m_SystemHealthChanged01;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 80;
			Delegate obj2 = this.m_SystemHealthChanged01;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		if (registry.TryGetValue(systemId, out var value) && value != this)
		{
			string message = "HighPressureSystemManager: Duplicate System ID '" + systemId + "'. Keeping the first instance in registry.";
			Debug.LogWarning(message, this);
		}
		else
		{
			registry.set_Item(systemId, this);
		}
		RecomputeHealthAndNotify(forceNotify: true);
		bool flag = !displayOutputAsPercent;
		float num = currentHealth01;
		if (!flag)
		{
			num *= 100f;
		}
		lastProviderValue = num;
	}

	private void OnDestroy()
	{
		if (registry.TryGetValue(systemId, out var value) && value == this)
		{
			bool flag = registry.Remove(systemId);
		}
	}

	private void OnValidate()
	{
	}

	public void RegisterValve(ValveController valve)
	{
		if (valve != null && !valves.Contains(valve))
		{
			valves.Add(valve);
			Action<float> value = HandleValveDamageChanged;
			valve.DamageChanged01 += value;
			RecomputeHealthAndNotify();
		}
	}

	public void UnregisterValve(ValveController valve)
	{
		if (valve != null && valves.Remove(valve))
		{
			Action<float> value = HandleValveDamageChanged;
			valve.DamageChanged01 -= value;
			RecomputeHealthAndNotify();
		}
	}

	private void HandleValveDamageChanged(float _)
	{
		RecomputeHealthAndNotify();
	}

	private unsafe void RecomputeHealthAndNotify(bool forceNotify = false)
	{
		//IL_0148: Invalid comparison between F4 and I4
		//IL_012e: Expected F4, but got Ref
		//IL_01ec: Expected F4, but got Ref
		float num = ComputeHealth();
		if (!forceNotify)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj != null)
			{
				debugSystemHealth01 = currentHealth01;
				goto IL_023b;
			}
		}
		bool flag = !logHealthChanges;
		currentHealth01 = num;
		debugSystemHealth01 = num;
		float num2 = default(float);
		if (!flag && Application.isPlaying)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[HighPressureSystemManager:{systemId}] Health01 -> {arg:0.###}";
			Debug.Log(message, this);
			num2 = currentHealth01;
		}
		Action<float> systemHealthChanged = this.m_SystemHealthChanged01;
		if (this.m_SystemHealthChanged01 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v152 @ rcx_v6 (System.Action`1<System.Single>)+18] (should have been resolved before IL gen)");
			num2 = currentHealth01;
		}
		if (OnSystemHealthChanged01 != null)
		{
			OnSystemHealthChanged01.Invoke((nint)(&num2));
			num2 = currentHealth01;
		}
		bool flag2;
		if (healthAlertThreshold < 0f)
		{
			flag2 = false;
		}
		else
		{
			bool flag3 = healthAlertThreshold < currentHealth01;
			flag2 = !flag3;
		}
		if (flag2)
		{
			if (!thresholdWasBreached)
			{
				thresholdWasBreached = true;
				if (OnHealthBelowThreshold != null)
				{
					OnHealthBelowThreshold.Invoke((nint)(&num2));
				}
			}
		}
		else if (thresholdWasBreached)
		{
			thresholdWasBreached = false;
		}
		goto IL_023b;
		IL_023b:
		CheckProviderValueChanged();
	}

	private float ComputeHealth()
	{
		//IL_0056: Expected O, but got I4
		//IL_035a: Expected O, but got I4
		//IL_0363: Expected F4, but got I4
		//IL_036c: Expected O, but got I4
		//IL_01e9: Expected O, but got I4
		//IL_01f2: Expected F4, but got I4
		//IL_01fb: Expected O, but got I4
		//IL_0096: Expected O, but got I4
		//IL_009f: Expected O, but got I4
		//IL_0479: Invalid comparison between I4 and F4
		//IL_0488: Expected F4, but got I4
		//IL_02e2: Invalid comparison between I4 and F4
		//IL_032d: Expected F4, but got I4
		//IL_0171: Invalid comparison between I4 and F4
		//IL_01bc: Expected F4, but got I4
		//IL_03c7: Expected F4, but got I
		//IL_0423: Expected F4, but got I4
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Expected O, but got Unknown
		//IL_0256: Expected F4, but got I
		//IL_02b2: Expected F4, but got I4
		//IL_010b: Invalid comparison between I4 and F4
		//IL_0156: Expected F4, but got I4
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Expected O, but got Unknown
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Expected O, but got Unknown
		List<ValveController> list = valves;
		float num;
		float num5;
		if (valves != null)
		{
			if (list._size == 0)
			{
				goto IL_04c4;
			}
			bool flag = aggregationMode == AggregationMode.Average;
			object obj4 = default(object);
			if (!flag)
			{
				object obj = aggregationMode - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						goto IL_04c4;
					}
					num = 1f;
					object obj2 = 0;
					object obj3 = 0;
					while ((nint)obj3 < list._size)
					{
						if (valves != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (obj4 != null)
							{
								float num2 = 1f;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ stack_8_v5+E0]");
								float num3 = num2 - 0f;
								if (!(0f > num3))
								{
									if (num3 > 1f)
									{
										num3 = 1f;
									}
								}
								else
								{
									num3 = 0f;
								}
								list = valves;
								obj2++;
								num *= num3;
								if (valves != null)
								{
									obj3 = obj2;
									continue;
								}
							}
						}
						goto IL_04d2;
					}
					if (!(0f > num))
					{
						if (num > 1f)
						{
							num = 1f;
						}
					}
					else
					{
						num = 0f;
					}
					goto IL_0570;
				}
				if (valves != null)
				{
					object obj5 = 0;
					float num4 = 0f;
					object obj6 = 0;
					while ((nint)obj6 < list._size)
					{
						if (valves != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (obj4 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ stack_8_v5+E0]");
								float num3 = 0f;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ stack_8_v5+E0]");
								if ((nint)0 <= (nint)0)
								{
									if (num3 > 1f)
									{
										num3 = 1f;
									}
								}
								else
								{
									num3 = 0f;
								}
								if (!(num4 > num3))
								{
									num4 = num3;
								}
								list = valves;
								obj5++;
								bool flag2 = valves != null;
								obj6 = obj5;
								if (flag2)
								{
									continue;
								}
							}
						}
						goto IL_04d2;
					}
					num5 = 1f - num4;
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
					goto IL_05bd;
				}
			}
			else if (valves != null)
			{
				object obj7 = 0;
				float num6 = 0f;
				object obj8 = 0;
				List<ValveController> list2;
				while (true)
				{
					list2 = valves;
					if ((nint)obj7 >= list._size)
					{
						break;
					}
					if (valves != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (obj4 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ stack_8_v5+E0]");
							float num3 = 0f;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ stack_8_v5+E0]");
							if ((nint)0 <= (nint)0)
							{
								if (num3 > 1f)
								{
									num3 = 1f;
								}
							}
							else
							{
								num3 = 0f;
							}
							list = valves;
							num6 += num3;
							obj8++;
							bool flag3 = valves != null;
							obj7 = obj8;
							if (flag3)
							{
								continue;
							}
						}
					}
					goto IL_04d2;
				}
				if (valves != null)
				{
					float num7 = num6 / (float)list2._size;
					num5 = 1f - num7;
					bool flag4 = 0f > num5;
					num = 0f;
					if (flag4)
					{
						goto IL_0570;
					}
					if (num5 > 1f)
					{
						num5 = 1f;
					}
					goto IL_05bd;
				}
			}
		}
		goto IL_04d2;
		IL_04d2:
		throw new NullReferenceException();
		IL_04c4:
		num5 = 1f;
		goto IL_05bd;
		IL_05bd:
		return num5;
		IL_0570:
		num5 = num;
		goto IL_05bd;
	}

	public float GetFloatValue()
	{
		bool flag = !displayOutputAsPercent;
		float num = currentHealth01;
		if (!flag)
		{
			num *= 100f;
		}
		return num;
	}

	private void CacheProviderValue()
	{
		bool flag = !displayOutputAsPercent;
		float num = currentHealth01;
		if (!flag)
		{
			num *= 100f;
		}
		lastProviderValue = num;
	}

	private void CheckProviderValueChanged()
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_014c: Invalid comparison between F4 and O
		bool flag = !displayOutputAsPercent;
		float num = currentHealth01;
		if (!flag)
		{
			num *= 100f;
		}
		float num2 = lastProviderValue - num;
		float num3 = lastProviderValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num3 & 0;
		float num4 = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num4 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			obj2 = obj;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj3 = num2 & 0;
		float num5 = Mathf.Epsilon * 8f;
		float num6 = (float)obj2 * 1E-06f;
		if (num6 < num5)
		{
			num6 = num5;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num6) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			return;
		}
		if (logDisplayProviderChanges && Application.isPlaying)
		{
			bool flag2 = !displayOutputAsPercent;
			object arg = "";
			if (!flag2)
			{
				arg = "%";
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			string message = $"[HighPressureSystemManager:{systemId}] CurrentValue provider -> {arg2:0.###}{arg}";
			Debug.Log(message, this);
		}
		lastProviderValue = num;
	}

	public ValveController GetRandomRegisteredValve()
	{
		List<ValveController> list = valves;
		if (valves != null)
		{
			if (list._size != 0)
			{
				int num = UnityEngine.Random.Range(0, list._size);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				ValveController result = default(ValveController);
				return result;
			}
			return null;
		}
		return (ValveController)(object)new NullReferenceException();
	}

	public void DamageRandomValve()
	{
		List<ValveController> list = valves;
		UnityEngine.Object obj;
		if (list._size != 0)
		{
			int num = UnityEngine.Random.Range(0, list._size);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			obj = obj2;
		}
		else
		{
			obj = null;
		}
		if (obj != null)
		{
			((ValveController)obj).DamageValve();
		}
	}

	public static IReadOnlyList<HighPressureSystemManager> GetAllManagers()
	{
		if (registry != null)
		{
			Dictionary<string, HighPressureSystemManager>.ValueCollection values = registry.Values;
			List<HighPressureSystemManager> list = new List<HighPressureSystemManager>(values);
			if (list != null)
			{
				return list.AsReadOnly();
			}
		}
		return (IReadOnlyList<HighPressureSystemManager>)new NullReferenceException();
	}

	public static HighPressureSystemManager GetRandomManager()
	{
		if (registry != null)
		{
			if (registry.Count == 0)
			{
				return null;
			}
			if (registry != null)
			{
				Dictionary<string, HighPressureSystemManager>.ValueCollection values = registry.Values;
				List<HighPressureSystemManager> list = new List<HighPressureSystemManager>(values);
				if (list != null)
				{
					int num = UnityEngine.Random.Range(0, list._size);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					HighPressureSystemManager result = default(HighPressureSystemManager);
					return result;
				}
			}
		}
		return (HighPressureSystemManager)(object)new NullReferenceException();
	}

	public static List<ValveController> GetAllRegisteredValves()
	{
		//IL_0085: Expected O, but got I
		List<ValveController> list = new List<ValveController>();
		Dictionary<string, HighPressureSystemManager>.ValueCollection values = registry.Values;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
		Dictionary<string, HighPressureSystemManager>.ValueCollection.Enumerator enumerator = default(Dictionary<string, HighPressureSystemManager>.ValueCollection.Enumerator);
		Dictionary<string, HighPressureSystemManager> dictionary = default(Dictionary<string, HighPressureSystemManager>);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (dictionary != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ stack_10_v4 (System.Collections.Generic.Dictionary`2<System.String, HighPressureSystemManager>)+58]");
					if ((nint)0 == 0)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ stack_10_v4 (System.Collections.Generic.Dictionary`2<System.String, HighPressureSystemManager>)+58]");
					IEnumerable<ValveController> collection = (IEnumerable<ValveController>)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rax_v22 (System.Collections.Generic.IEnumerable`1<ValveController>)+18]");
					if ((nint)0 > (nint)0)
					{
						if (list == null)
						{
							break;
						}
						list.AddRange(collection);
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return list;
		}
		throw new NullReferenceException();
	}

	public static ValveController GetRandomRegisteredValveAcrossAllSystems()
	{
		List<ValveController> allRegisteredValves = GetAllRegisteredValves();
		if (allRegisteredValves != null && allRegisteredValves._size != 0)
		{
			int num = UnityEngine.Random.Range(0, allRegisteredValves._size);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			ValveController result = default(ValveController);
			return result;
		}
		return null;
	}

	private void RegisterInGlobalRegistry()
	{
		if (registry.TryGetValue(systemId, out var value) && value != this)
		{
			string message = "HighPressureSystemManager: Duplicate System ID '" + systemId + "'. Keeping the first instance in registry.";
			Debug.LogWarning(message, this);
		}
		else
		{
			registry.set_Item(systemId, this);
		}
	}

	private void UnregisterFromGlobalRegistry()
	{
		if (registry.TryGetValue(systemId, out var value) && value == this)
		{
			bool flag = registry.Remove(systemId);
		}
	}

	public static HighPressureSystemManager FindBySystemId(string id)
	{
		if (string.IsNullOrWhiteSpace(id))
		{
			return null;
		}
		if (registry != null)
		{
			bool flag = registry.TryGetValue(id, out var value);
			return value;
		}
		return (HighPressureSystemManager)(object)new NullReferenceException();
	}

	public HighPressureSystemManager()
	{
		List<ValveController> list = new List<ValveController>();
		valves = list;
		currentHealth01 = 1f;
		lastProviderValue = 0f / 0f;
		base._002Ector();
	}

	static HighPressureSystemManager()
	{
		Dictionary<string, HighPressureSystemManager> dictionary = new Dictionary<string, HighPressureSystemManager>();
		registry = dictionary;
	}
}
