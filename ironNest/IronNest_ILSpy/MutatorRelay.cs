using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class MutatorRelay : MonoBehaviour
{
	public enum Condition
	{
		AnyOfActive,
		AllOfActive,
		NoneActive
	}

	public Condition condition;

	public List<MutatorDefinition> requiredMutators;

	public bool activateTargetsWhenConditionTrue;

	public List<GameObject> targets;

	public UnityEvent<bool> onApplied;

	public bool verbose;

	private MutatorRuntime _runtime;

	private void OnEnable()
	{
		//IL_0195: Expected O, but got I
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		MutatorRuntime runtime;
		GameObject gameObject2;
		if (MutatorRuntime._003CInstance_003Ek__BackingField == null)
		{
			GameObject gameObject = new GameObject("MutatorRuntime (Auto)");
			bool flag = (object)gameObject == null;
			gameObject2 = gameObject;
			if (flag)
			{
				goto IL_014b;
			}
			runtime = gameObject.AddComponent<MutatorRuntime>();
		}
		else
		{
			runtime = MutatorRuntime._003CInstance_003Ek__BackingField;
		}
		_runtime = runtime;
		ApplyNow();
		if (!(_runtime != null))
		{
			return;
		}
		gameObject2 = (GameObject)(object)_runtime;
		Action<IReadOnlyList<MutatorDefinition>> b = OnMutatorsChanged;
		if ((object)_runtime != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ rdi_v2 (UnityEngine.GameObject)+20]");
			Delegate obj = (Delegate)0;
			object obj2 = _runtime + 32;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj, b);
				bool flag2 = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag2)
				{
					((MutatorRelay)(object)obj3).OnMutatorsChanged((IReadOnlyList<MutatorDefinition>)typeof(Action<IReadOnlyList<MutatorDefinition>>));
					bool flag3 = (object)obj4 == null;
					gameObject2 = (GameObject)(object)obj3;
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj5 != obj;
				obj = obj5;
				if (!flag4)
				{
					return;
				}
			}
			((MutatorRelay)(object)gameObject2).OnMutatorsChanged((IReadOnlyList<MutatorDefinition>)typeof(Action<IReadOnlyList<MutatorDefinition>>));
			return;
		}
		goto IL_014b;
		IL_014b:
		throw new NullReferenceException();
	}

	private void OnDisable()
	{
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00eb: Expected O, but got I
		if (!(_runtime != null))
		{
			return;
		}
		Delegate runtime = (Delegate)(object)_runtime;
		Action<IReadOnlyList<MutatorDefinition>> value = OnMutatorsChanged;
		object obj = _runtime + 32;
		Delegate obj2 = (Delegate)runtime.m_target;
		Delegate obj4 = default(Delegate);
		while (true)
		{
			Delegate obj3 = Delegate.Remove(obj2, value);
			if ((object)obj3 != null)
			{
				((MutatorRelay)(object)obj3).OnMutatorsChanged((IReadOnlyList<MutatorDefinition>)typeof(Action<IReadOnlyList<MutatorDefinition>>));
				if ((object)obj3 == null)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag = (object)obj4 != obj2;
			obj2 = obj4;
			if (!flag)
			{
				return;
			}
		}
		IntPtr intPtr = default(IntPtr);
		((MutatorRelay)(object)_runtime).OnMutatorsChanged((IReadOnlyList<MutatorDefinition>)(nint)intPtr);
	}

	private void EnsureRuntime()
	{
		if (MutatorRuntime._003CInstance_003Ek__BackingField == null)
		{
			GameObject gameObject = new GameObject("MutatorRuntime (Auto)");
			MutatorRuntime runtime = gameObject.AddComponent<MutatorRuntime>();
			_runtime = runtime;
		}
		else
		{
			_runtime = MutatorRuntime._003CInstance_003Ek__BackingField;
		}
	}

	private void OnMutatorsChanged(IReadOnlyList<MutatorDefinition> _)
	{
		ApplyNow();
	}

	public unsafe void ApplyNow()
	{
		//IL_0230: Expected I4, but got O
		//IL_00a0: Expected O, but got I4
		//IL_00de: Expected O, but got I4
		//IL_00f9: Expected O, but got I4
		//IL_021a: Expected I4, but got O
		//IL_0205: Expected I4, but got O
		//IL_01a3: Expected O, but got I4
		//IL_01ab: Expected I4, but got O
		//IL_028f: Expected O, but got I4
		//IL_0298: Expected O, but got I4
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_01f0: Expected I4, but got O
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected O, but got Unknown
		if (_runtime != null && requiredMutators != null)
		{
			List<MutatorDefinition> list = requiredMutators;
			if (list._size != 0)
			{
				goto IL_00cb;
			}
		}
		bool flag = condition == Condition.AnyOfActive;
		if (flag)
		{
			goto IL_0228;
		}
		object obj = condition - 1;
		if (!flag && (nint)obj != 1)
		{
			goto IL_00cb;
		}
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		bool flag2 = (byte)(int)obj2 != 0;
		bool flag3 = true;
		goto IL_0447;
		IL_0447:
		bool flag4 = activateTargetsWhenConditionTrue;
		bool flag5 = flag3;
		if (!flag4)
		{
			bool flag6 = !flag3;
			flag5 = flag6;
		}
		bool flag7 = !flag5;
		bool flag8 = !flag7;
		if (targets != null)
		{
			List<GameObject> list2 = targets;
			if (list2._size != 0)
			{
				object obj3 = 0;
				object obj4 = 0;
				UnityEngine.Object obj5 = default(UnityEngine.Object);
				object arg2 = default(object);
				while ((nint)obj4 < list2._size)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj5 != null)
					{
						((GameObject)obj5).SetActive(flag8);
						if (verbose)
						{
							string arg = obj5.name;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							string message = $"[MutatorRelay] Target '{arg}' active = {arg2}";
							Debug.Log(message, this);
							flag2 = flag8;
						}
					}
					list2 = targets;
					obj3++;
					obj4 = obj3;
				}
				goto IL_03c5;
			}
		}
		if (verbose)
		{
			string text = base.name;
			string message2 = "[MutatorRelay] No targets assigned on '" + text + "'. No action taken.";
			Debug.Log(message2, this);
		}
		goto IL_03c5;
		IL_00cb:
		List<MutatorDefinition> list3 = requiredMutators;
		object obj6 = 0;
		flag3 = true;
		bool flag9 = false;
		object obj7 = 0;
		while ((nint)obj7 < list3._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj2 != null)
			{
				bool flag10 = (object)_runtime == null;
				bool flag11 = _runtime.IsActive((MutatorDefinition)obj2);
				flag9 = !flag10;
				flag3 &= flag11;
			}
			list3 = requiredMutators;
			obj6++;
			bool flag12 = requiredMutators != null;
			obj7 = obj6;
			if (!flag12)
			{
				throw new NullReferenceException();
			}
		}
		bool flag13 = condition == Condition.AnyOfActive;
		if (!flag13)
		{
			object obj8 = condition - 1;
			flag2 = (byte)(int)obj2 != 0;
			if (!flag13)
			{
				if ((nint)obj8 != 1)
				{
					goto IL_0228;
				}
				bool flag14 = !flag9;
				flag2 = (byte)(int)obj2 != 0;
				flag3 = flag14;
			}
		}
		else
		{
			flag2 = (byte)(int)obj2 != 0;
			flag3 = flag9;
		}
		goto IL_0447;
		IL_03c5:
		if (onApplied != null)
		{
			onApplied.Invoke((byte)(&flag2) != 0);
		}
		return;
		IL_0228:
		flag2 = (byte)(int)obj2 != 0;
		flag3 = false;
		goto IL_0447;
	}

	private bool EvaluateCondition()
	{
		//IL_00a0: Expected O, but got I4
		//IL_0101: Expected O, but got I4
		//IL_010a: Expected O, but got I4
		//IL_027b: Expected I4, but got O
		//IL_01ed: Expected O, but got I4
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		if (_runtime != null && requiredMutators != null)
		{
			List<MutatorDefinition> list = requiredMutators;
			if (list._size != 0)
			{
				goto IL_00cb;
			}
		}
		bool flag = condition == Condition.AnyOfActive;
		if (!flag)
		{
			object obj = condition - 1;
			if (!flag && (nint)obj != 1)
			{
				goto IL_00cb;
			}
			return true;
		}
		goto IL_025f;
		IL_026d:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_025f:
		return false;
		IL_00cb:
		List<MutatorDefinition> list2 = requiredMutators;
		bool flag2 = requiredMutators == null;
		bool flag3 = true;
		bool flag4 = false;
		object obj2 = 0;
		object obj3 = 0;
		if (!flag2)
		{
			UnityEngine.Object obj4 = default(UnityEngine.Object);
			while ((nint)obj3 < list2._size)
			{
				if (requiredMutators != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj4 != null)
					{
						bool flag5 = (object)_runtime == null;
						if (flag5)
						{
							goto IL_026d;
						}
						bool flag6 = _runtime.IsActive((MutatorDefinition)obj4);
						flag4 = !flag5;
						flag3 &= flag6;
					}
					list2 = requiredMutators;
					obj2++;
					if (requiredMutators != null)
					{
						obj3 = obj2;
						continue;
					}
				}
				goto IL_026d;
			}
			bool flag7 = condition == Condition.AnyOfActive;
			if (!flag7)
			{
				object obj5 = condition - 1;
				if (!flag7)
				{
					if ((nint)obj5 == 1)
					{
						return !flag4;
					}
					goto IL_025f;
				}
				return flag3;
			}
			return flag4;
		}
		goto IL_026d;
	}

	public MutatorRelay()
	{
		List<MutatorDefinition> list = new List<MutatorDefinition>();
		requiredMutators = list;
		activateTargetsWhenConditionTrue = true;
		targets = new List<GameObject>();
		base._002Ector();
	}
}
