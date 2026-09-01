using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class ToggleProxy : MonoBehaviour
{
	public string key;

	public GameObject target;

	public string autoFindChildName;

	public bool deactivateTargetOnEnable;

	public bool invert;

	public bool oneShotActivate;

	public bool oneShotDeactivate;

	private static readonly Dictionary<string, HashSet<ToggleProxy>> _registry;

	private static Action<ToggleProxy> m_OnProxyRegistered;

	public static event Action<ToggleProxy> OnProxyRegistered
	{
		add
		{
			//IL_000e: Expected O, but got I4
			//IL_0050: Expected I, but got O
			//IL_007c: Expected O, but got I
			Delegate obj = ToggleProxy.m_OnProxyRegistered;
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
				nint num = (nint)typeof(ToggleProxy);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v7 (Il2CppClass<ToggleProxy>)+B8]");
				object obj5 = (nint)0 + (nint)8;
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
			Delegate obj = ToggleProxy.m_OnProxyRegistered;
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
				nint num = (nint)typeof(ToggleProxy);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v7 (Il2CppClass<ToggleProxy>)+B8]");
				object obj5 = (nint)0 + (nint)8;
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

	private void OnEnable()
	{
		if (target == null && !string.IsNullOrEmpty(autoFindChildName))
		{
			Transform transform = base.transform;
			Transform transform2 = transform.Find(autoFindChildName);
			if ((bool)transform2)
			{
				GameObject gameObject = transform2.gameObject;
				target = gameObject;
			}
		}
		if (string.IsNullOrEmpty(key))
		{
			string text = base.name;
			string message = "[ToggleProxy] '" + text + "' has empty key.";
			Debug.LogWarning(message);
			return;
		}
		bool flag = _registry.TryGetValue(key, out var _);
		nint num = 0;
		HashSet<ToggleProxy> hashSet = default(HashSet<ToggleProxy>);
		if (!flag)
		{
			hashSet = new HashSet<ToggleProxy>();
			_registry.set_Item(key, hashSet);
			num = 0;
		}
		hashSet.Add(this);
		if (deactivateTargetOnEnable && (bool)target && target.activeSelf)
		{
			target.SetActive(value: false);
		}
		Action<ToggleProxy> onProxyRegistered = ToggleProxy.m_OnProxyRegistered;
		if (ToggleProxy.m_OnProxyRegistered != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v406 @ rcx_v14 (System.Action`1<ToggleProxy>)+18] (should have been resolved before IL gen)");
		}
	}

	private void OnDisable()
	{
		if (!string.IsNullOrEmpty(key) && _registry.TryGetValue(key, out var value))
		{
			bool flag = value.Remove(this);
			if (value._count == 0)
			{
				bool flag2 = _registry.Remove(key);
			}
		}
	}

	private void ResolveTargetIfNeeded()
	{
		if (target == null && !string.IsNullOrEmpty(autoFindChildName))
		{
			Transform transform = base.transform;
			Transform transform2 = transform.Find(autoFindChildName);
			if ((bool)transform2)
			{
				GameObject gameObject = transform2.gameObject;
				target = gameObject;
			}
		}
	}

	internal static void ApplyToKey(string key, bool state)
	{
		if (string.IsNullOrEmpty(key) || !_registry.TryGetValue(key, out var _))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
		HashSet<ToggleProxy>.Enumerator enumerator = default(HashSet<ToggleProxy>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						break;
					}
					((ToggleProxy)obj).ApplyActive(state);
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void ApplyActive(bool requestedState)
	{
		//IL_010a: Expected O, but got I4
		//IL_004b: Expected O, but got I4
		bool flag = target == null;
		if (flag)
		{
			return;
		}
		bool flag2;
		if (requestedState)
		{
			object obj = (oneShotDeactivate ? 1 : 0) - (flag ? 1 : 0);
			flag2 = obj == null;
		}
		else
		{
			flag2 = !oneShotActivate;
		}
		object obj2 = !flag2;
		if (obj2 == null)
		{
			bool flag3 = (byte)((requestedState ? 1u : 0u) ^ 1u) != 0;
			bool flag4 = invert;
			bool flag5 = flag3;
			if (!flag4)
			{
				flag5 = requestedState;
			}
			bool activeSelf = target.activeSelf;
			if (activeSelf != flag5)
			{
				target.SetActive(flag5);
			}
		}
	}

	static ToggleProxy()
	{
		Dictionary<string, HashSet<ToggleProxy>> registry = new Dictionary<string, HashSet<ToggleProxy>>();
		_registry = registry;
	}
}
