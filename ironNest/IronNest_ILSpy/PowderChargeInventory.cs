using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class PowderChargeInventory : MonoBehaviour
{
	private static PowderChargeInventory _003CInstance_003Ek__BackingField;

	private int startingCharges = 12;

	private int maxCapacity = 24;

	private int currentChargesForInspector;

	private UnityEvent onInventoryEmpty;

	private UnityEvent onSixOrLessRemaining;

	private UnityEvent onMoreThanSixRemaining;

	private bool invokeThresholdEventsOnStart = true;

	private Action<int> m_OnChargesChanged;

	private int _currentCharges;

	public static PowderChargeInventory Instance
	{
		get
		{
			return _003CInstance_003Ek__BackingField;
		}
		private set
		{
			_003CInstance_003Ek__BackingField = value;
		}
	}

	public int CurrentCharges
	{
		get
		{
			return _currentCharges;
		}
		set
		{
			if (_currentCharges != value)
			{
				_currentCharges = value;
				currentChargesForInspector = value;
				Action<int> onChargesChanged = this.m_OnChargesChanged;
				if (this.m_OnChargesChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v15 @ rcx_v2 (System.Action`1<System.Int32>)+18] (should have been resolved before IL gen)");
				}
				((_currentCharges <= 0) ? onInventoryEmpty : ((_currentCharges <= 6) ? onSixOrLessRemaining : onMoreThanSixRemaining))?.Invoke();
			}
		}
	}

	public float CurrentChargesAsFloat
	{
		get
		{
			//IL_0007: Expected F4, but got I4
			return _currentCharges;
		}
	}

	public float CurrentChargesAsPercent
	{
		get
		{
			//IL_0040: Expected F4, but got I4
			if (maxCapacity > 0)
			{
				return (float)_currentCharges / (float)maxCapacity;
			}
			return 0f;
		}
	}

	public event Action<int> OnChargesChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 80;
			Delegate obj2 = this.m_OnChargesChanged;
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
			Delegate obj2 = this.m_OnChargesChanged;
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
		if (_003CInstance_003Ek__BackingField != null && _003CInstance_003Ek__BackingField != this)
		{
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
		}
		else
		{
			_003CInstance_003Ek__BackingField = this;
			_currentCharges = startingCharges;
			currentChargesForInspector = startingCharges;
		}
	}

	private void Start()
	{
		Action<int> onChargesChanged = this.m_OnChargesChanged;
		if (this.m_OnChargesChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v4 @ rcx_v1 (System.Action`1<System.Int32>)+18] (should have been resolved before IL gen)");
		}
		if (invokeThresholdEventsOnStart)
		{
			((_currentCharges <= 0) ? onInventoryEmpty : ((_currentCharges <= 6) ? onSixOrLessRemaining : onMoreThanSixRemaining))?.Invoke();
		}
	}

	public bool TryUseCharge()
	{
		if (_currentCharges <= 0)
		{
			Debug.LogWarning("Attempted to use a powder charge, but none are left!");
			return false;
		}
		int num = _currentCharges - 1;
		if (_currentCharges != num)
		{
			Action<int> onChargesChanged = this.m_OnChargesChanged;
			_currentCharges = num;
			currentChargesForInspector = num;
			if (this.m_OnChargesChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v72 @ rcx_v7 (System.Action`1<System.Int32>)+18] (should have been resolved before IL gen)");
			}
			((_currentCharges <= 0) ? onInventoryEmpty : ((_currentCharges <= 6) ? onSixOrLessRemaining : onMoreThanSixRemaining))?.Invoke();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string message = $"Powder charge used. Remaining: {arg}";
		Debug.Log(message);
		return true;
	}

	public void AddCharges(int amount)
	{
		if (amount > 0)
		{
			int num = _currentCharges + amount;
			if (num >= maxCapacity)
			{
				num = maxCapacity;
			}
			if (num != _currentCharges)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				string message = $"Added {arg} charges. New total: {arg2}";
				Debug.Log(message);
				CurrentCharges = num;
			}
		}
	}

	private void InvokeStateEventsForCount(int count)
	{
		((count <= 0) ? onInventoryEmpty : ((count <= 6) ? onSixOrLessRemaining : onMoreThanSixRemaining))?.Invoke();
	}
}
