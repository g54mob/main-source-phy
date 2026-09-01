using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DialValueEventWatcher : MonoBehaviour
{
	[Serializable]
	public class ValueTrigger
	{
		public float targetValue;

		public UnityEvent onMatched;

		public bool fireOnceUntilExit = true;

		[NonSerialized]
		public bool isInsideBand;
	}

	private DialInteractable dial;

	private float matchTolerance = 0.01f;

	private bool evaluateOnEnable;

	private List<ValueTrigger> triggers;

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		DialInteractable dialInteractable = default(DialInteractable);
		dial = dialInteractable;
		if (dial == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
			dial = dialInteractable;
		}
		evaluateOnEnable = false;
		matchTolerance = 0.01f;
	}

	private void Awake()
	{
		DialInteractable dialInteractable = default(DialInteractable);
		if (dial == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			dial = dialInteractable;
		}
		if (dial == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
			dial = dialInteractable;
		}
	}

	private void OnEnable()
	{
		//IL_0036: Expected O, but got I4
		//IL_003f: Expected O, but got I4
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		if (dial != null)
		{
			List<ValueTrigger> list = triggers;
			object obj = 0;
			object obj2 = 0;
			while ((nint)obj2 < list._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				obj++;
				_ = 0;
				list = triggers;
				obj2 = obj;
			}
			DialInteractable dialInteractable = dial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.AddListener(call);
			if (evaluateOnEnable)
			{
				DialInteractable dialInteractable2 = dial;
				Evaluate(dialInteractable2.accumulatedValue);
			}
		}
		else
		{
			Debug.LogWarning("[DialValueEventWatcher] No DialInteractable reference found. This watcher will do nothing.", this);
		}
	}

	private void OnDisable()
	{
		if (dial != null)
		{
			DialInteractable dialInteractable = dial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.RemoveListener(call);
		}
	}

	private void HandleDialValueChanged(float value)
	{
		Evaluate(value);
	}

	private void Evaluate(float dialValue)
	{
		//IL_0046: Invalid comparison between I4 and F4
		//IL_0055: Expected F4, but got I4
		//IL_020c: Expected O, but got I4
		//IL_0215: Expected O, but got I4
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected F4, but got Unknown
		//IL_0155: Expected O, but got I
		if (triggers == null)
		{
			return;
		}
		List<ValueTrigger> list = triggers;
		if (list._size == 0)
		{
			return;
		}
		float num = matchTolerance;
		bool flag = 0f > matchTolerance;
		float num2 = 0f;
		if (!flag)
		{
			num2 = matchTolerance;
		}
		object obj = 0;
		object obj3 = default(object);
		for (object obj2 = 0; (nint)obj2 < list._size; list = triggers, obj++, obj2 = obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 == null)
			{
				continue;
			}
			float num3 = dialValue;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ stack_8_v4+10]");
			float num4 = num3 - 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			num = num4 & 0;
			bool flag2 = num2 < num;
			bool flag3 = !flag2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ stack_8_v4+20]");
			if ((nint)0 == 0)
			{
				if (!flag3)
				{
					continue;
				}
			}
			else
			{
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ stack_8_v4+21]");
					if ((nint)0 != 0)
					{
						_ = 0;
					}
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ stack_8_v4+21]");
				if ((nint)0 != 0)
				{
					continue;
				}
				_ = 1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ stack_8_v4+18]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ stack_8_v4+18]");
				((UnityEvent)0).Invoke();
			}
		}
	}

	public DialValueEventWatcher()
	{
		List<ValueTrigger> list = new List<ValueTrigger>();
		triggers = list;
		base._002Ector();
	}
}
