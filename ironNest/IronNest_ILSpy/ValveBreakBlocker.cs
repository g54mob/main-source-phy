using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class ValveBreakBlocker : MonoBehaviour
{
	private bool blockAllSystems;

	private bool blockSpecificSystems;

	private List<string> blockedSystemIds;

	private bool logDebug;

	private static int s_globalBlockerCount = 0;

	private static readonly HashSet<string> s_blockedSystems;

	private static Action<bool> m_OnBlockStateChanged;

	private static Action<string[]> m_OnPerSystemBlocksChanged;

	public static bool IsBlocked
	{
		get
		{
			int num = s_globalBlockerCount ^ s_globalBlockerCount;
			int num2 = s_globalBlockerCount & num;
			bool flag = num2 < 0;
			bool flag2 = s_globalBlockerCount < 0;
			bool flag3 = s_globalBlockerCount == 0;
			bool flag4 = flag2 == flag;
			bool flag5 = !flag3;
			return flag5 & flag4;
		}
	}

	public static IReadOnlyCollection<string> BlockedSystems => s_blockedSystems;

	public static event Action<bool> OnBlockStateChanged
	{
		add
		{
			//IL_000e: Expected O, but got I4
			//IL_0050: Expected I, but got O
			//IL_007c: Expected O, but got I
			Delegate obj = ValveBreakBlocker.m_OnBlockStateChanged;
			object obj4 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Combine(obj, value);
				if ((object)obj2 == null)
				{
					object obj3 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj4 == null;
					object obj3 = obj4;
					if (flag)
					{
						break;
					}
				}
				nint num = (nint)typeof(ValveBreakBlocker);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v7 (Il2CppClass<ValveBreakBlocker>)+B8]");
				object obj5 = (nint)0 + (nint)16;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_000e: Expected O, but got I4
			//IL_0050: Expected I, but got O
			//IL_007c: Expected O, but got I
			Delegate obj = ValveBreakBlocker.m_OnBlockStateChanged;
			object obj4 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Remove(obj, value);
				if ((object)obj2 == null)
				{
					object obj3 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj4 == null;
					object obj3 = obj4;
					if (flag)
					{
						break;
					}
				}
				nint num = (nint)typeof(ValveBreakBlocker);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v7 (Il2CppClass<ValveBreakBlocker>)+B8]");
				object obj5 = (nint)0 + (nint)16;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public static event Action<string[]> OnPerSystemBlocksChanged
	{
		add
		{
			//IL_000e: Expected O, but got I4
			//IL_0050: Expected I, but got O
			//IL_007c: Expected O, but got I
			Delegate obj = ValveBreakBlocker.m_OnPerSystemBlocksChanged;
			object obj4 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Combine(obj, value);
				if ((object)obj2 == null)
				{
					object obj3 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj4 == null;
					object obj3 = obj4;
					if (flag)
					{
						break;
					}
				}
				nint num = (nint)typeof(ValveBreakBlocker);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v7 (Il2CppClass<ValveBreakBlocker>)+B8]");
				object obj5 = (nint)0 + (nint)24;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_000e: Expected O, but got I4
			//IL_0050: Expected I, but got O
			//IL_007c: Expected O, but got I
			Delegate obj = ValveBreakBlocker.m_OnPerSystemBlocksChanged;
			object obj4 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Remove(obj, value);
				if ((object)obj2 == null)
				{
					object obj3 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj4 == null;
					object obj3 = obj4;
					if (flag)
					{
						break;
					}
				}
				nint num = (nint)typeof(ValveBreakBlocker);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v7 (Il2CppClass<ValveBreakBlocker>)+B8]");
				object obj5 = (nint)0 + (nint)24;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public static bool IsSystemBlocked(string systemId)
	{
		//IL_00bb: Expected I4, but got O
		if (!string.IsNullOrWhiteSpace(systemId))
		{
			if (s_globalBlockerCount > 0)
			{
				return true;
			}
			if (s_blockedSystems != null)
			{
				return s_blockedSystems.Contains(systemId);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		int num = s_globalBlockerCount ^ s_globalBlockerCount;
		int num2 = s_globalBlockerCount & num;
		bool flag = num2 < 0;
		bool flag2 = s_globalBlockerCount < 0;
		bool flag3 = s_globalBlockerCount == 0;
		bool flag4 = flag2 == flag;
		bool flag5 = !flag3;
		return flag5 & flag4;
	}

	public static void AddSystemBlock(string systemId)
	{
		if (!string.IsNullOrWhiteSpace(systemId))
		{
			s_blockedSystems.Add(systemId);
			object obj = default(object);
			if (obj != null)
			{
				NotifyPerSystemBlocksChanged();
			}
		}
	}

	public static void RemoveSystemBlock(string systemId)
	{
		if (!string.IsNullOrWhiteSpace(systemId) && s_blockedSystems.Remove(systemId))
		{
			NotifyPerSystemBlocksChanged();
		}
	}

	public static void ClearSystemBlocks()
	{
		HashSet<string> hashSet = s_blockedSystems;
		if (hashSet._count != 0)
		{
			s_blockedSystems.Clear();
			NotifyPerSystemBlocksChanged();
		}
	}

	private static void NotifyGlobalChanged()
	{
		Action<bool> onBlockStateChanged = ValveBreakBlocker.m_OnBlockStateChanged;
		if (ValveBreakBlocker.m_OnBlockStateChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v43 @ rbx_v1 (System.Action`1<System.Boolean>)+18] (should have been resolved before IL gen)");
		}
	}

	private static void NotifyPerSystemBlocksChanged()
	{
		Action<string[]> onPerSystemBlocksChanged = ValveBreakBlocker.m_OnPerSystemBlocksChanged;
		if (ValveBreakBlocker.m_OnPerSystemBlocksChanged != null)
		{
			List<string> list = new List<string>(s_blockedSystems);
			string[] array = list.ToArray();
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v47 @ rdi_v1 (System.Action`1<System.String[]>)+18] (should have been resolved before IL gen)");
		}
	}

	private unsafe void OnEnable()
	{
		//IL_008f: Expected O, but got I4
		//IL_0098: Expected O, but got I4
		//IL_00a1: Expected O, but got I4
		//IL_0302: Expected O, but got I4
		//IL_0031: Expected I, but got O
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_012e: Expected O, but got I
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		string text = default(string);
		if (blockAllSystems)
		{
			int num = s_globalBlockerCount + 1;
			s_globalBlockerCount = num;
			NotifyGlobalChanged();
			if (logDebug)
			{
				string arg = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				int num2 = s_globalBlockerCount ^ s_globalBlockerCount;
				int num3 = s_globalBlockerCount & num2;
				bool flag = num3 < 0;
				bool flag2 = s_globalBlockerCount < 0;
				bool flag3 = s_globalBlockerCount == 0;
				bool flag4 = flag2 == flag;
				bool flag5 = !flag3;
				text = (string)(flag5 & flag4);
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				object obj = default(object);
				string message = $"[ValveBreakBlocker] Global block ON by '{arg}'. Count={arg2}. IsBlocked={obj}";
				Debug.Log(message, this);
				int num4 = s_globalBlockerCount;
				nint num5 = unchecked((nint)null);
				object obj2 = obj;
			}
		}
		if (!blockSpecificSystems || blockedSystemIds == null)
		{
			return;
		}
		List<string> list = blockedSystemIds;
		object obj3 = 0;
		object obj4 = 0;
		object obj5 = 0;
		object obj6 = default(object);
		while ((nint)obj5 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag6 = string.IsNullOrWhiteSpace(text);
			nint num5 = (nint)(&text);
			if (!flag6)
			{
				s_blockedSystems.Add(text);
				bool flag7 = obj6 == null;
				num5 = 0;
				if (!flag7)
				{
					obj4++;
					num5 = 0;
				}
			}
			list = blockedSystemIds;
			obj3++;
			object obj2 = 0;
			obj5 = obj3;
		}
		if ((nint)obj4 > 0)
		{
			NotifyPerSystemBlocksChanged();
			if (logDebug)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				string arg3 = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg4 = default(object);
				object arg5 = default(object);
				string message2 = $"[ValveBreakBlocker] Added {arg4} per-system blocks by '{arg3}'. TotalBlockedSystems={arg5}";
				Debug.Log(message2, this);
			}
		}
	}

	private unsafe void OnDisable()
	{
		//IL_00e7: Expected O, but got I4
		//IL_00f0: Expected O, but got I4
		//IL_00f9: Expected O, but got I4
		//IL_0054: Expected I, but got O
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Expected O, but got Unknown
		//IL_0322: Expected O, but got I4
		//IL_0185: Expected O, but got I
		//IL_0091: Expected I, but got O
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		string text = default(string);
		if (blockAllSystems)
		{
			int val = s_globalBlockerCount - 1;
			int num = Math.Max(0, val);
			s_globalBlockerCount = num;
			NotifyGlobalChanged();
			bool flag = !logDebug;
			nint num2 = unchecked((nint)null);
			if (!flag)
			{
				string arg = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				int num3 = s_globalBlockerCount ^ s_globalBlockerCount;
				int num4 = s_globalBlockerCount & num3;
				bool flag2 = num4 < 0;
				bool flag3 = s_globalBlockerCount < 0;
				bool flag4 = s_globalBlockerCount == 0;
				bool flag5 = flag3 == flag2;
				bool flag6 = !flag4;
				text = (string)(flag6 & flag5);
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				object obj = default(object);
				string message = $"[ValveBreakBlocker] Global block OFF by '{arg}'. Count={arg2}. IsBlocked={obj}";
				Debug.Log(message, this);
				int num5 = s_globalBlockerCount;
				object obj2 = obj;
				num2 = unchecked((nint)null);
			}
		}
		if (!blockSpecificSystems || blockedSystemIds == null)
		{
			return;
		}
		List<string> list = blockedSystemIds;
		object obj3 = 0;
		object obj4 = 0;
		object obj5 = 0;
		while ((nint)obj5 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag7 = string.IsNullOrWhiteSpace(text);
			nint num2 = (nint)(&text);
			if (!flag7)
			{
				bool flag8 = s_blockedSystems.Remove(text);
				bool flag9 = !flag8;
				num2 = 0;
				if (!flag9)
				{
					obj4++;
					num2 = 0;
				}
			}
			list = blockedSystemIds;
			obj3++;
			object obj2 = 0;
			obj5 = obj3;
		}
		if ((nint)obj4 > 0)
		{
			NotifyPerSystemBlocksChanged();
			if (logDebug)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				string arg3 = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg4 = default(object);
				object arg5 = default(object);
				string message2 = $"[ValveBreakBlocker] Removed {arg4} per-system blocks by '{arg3}'. TotalBlockedSystems={arg5}";
				Debug.Log(message2, this);
			}
		}
	}

	private void OnValidate()
	{
	}

	public ValveBreakBlocker()
	{
		List<string> list = new List<string>();
		blockedSystemIds = list;
		base._002Ector();
	}

	static ValveBreakBlocker()
	{
		HashSet<string> hashSet = new HashSet<string>();
		s_blockedSystems = hashSet;
	}
}
