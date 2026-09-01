using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_WaitSeconds : StateNode
{
	public StateNode To;

	public StateNode Cancel;

	public StateNode OnCancelled;

	public StateNode InstantProgress;

	public StateNode ResetTime;

	public float Seconds;

	[NonSerialized]
	private bool cancelCalled;

	private bool instantProgress;

	private bool resetTime;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		cancelCalled = false;
		resetTime = false;
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0049: Expected O, but got I4
		//IL_0092: Expected O, but got I4
		//IL_0141: Expected I, but got O
		//IL_0151: Expected O, but got I
		//IL_015a: Expected O, but got I4
		//IL_00cc: Expected I, but got O
		//IL_00e7: Expected O, but got I
		//IL_00f0: Expected O, but got I4
		//IL_00f9: Expected O, but got I4
		base.OnEnter(state);
		object obj2;
		object obj3;
		if (state.lastFieldPort == "Cancel")
		{
			cancelCalled = true;
			object obj = 0;
		}
		else
		{
			object obj;
			if (!(state.lastFieldPort == "InstantProgress"))
			{
				if (state.lastFieldPort == "ResetTime")
				{
					nint num = (nint)this;
					resetTime = true;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ rax_v14 (Il2CppClass<SleepyNodes.State_WaitSeconds>)+208]");
					obj2 = 0;
					obj3 = 0;
					obj = 0;
					goto IL_015f;
				}
				cancelCalled = false;
				resetTime = false;
				float time = Time.time;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763370");
				return;
			}
			instantProgress = true;
			obj = 0;
		}
		nint num2 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v8 (Il2CppClass<SleepyNodes.State_WaitSeconds>)+208]");
		obj2 = 0;
		obj3 = 0;
		goto IL_015f;
		IL_015f:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v83 @ r10_v1 (should have been resolved before IL gen)");
	}

	public override void OnExecute(NodeExecutionState state)
	{
		//IL_0069: Invalid comparison between F4 and O
		string outFieldName;
		if (!cancelCalled)
		{
			if (resetTime)
			{
				resetTime = false;
				float time = Time.time;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763370");
				return;
			}
			if (!instantProgress)
			{
				float time2 = Time.time;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763230");
				object obj = default(object);
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)time2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					return;
				}
			}
			instantProgress = false;
			outFieldName = "To";
		}
		else
		{
			cancelCalled = false;
			outFieldName = "OnCancelled";
		}
		base.OnExit(state, outFieldName);
	}
}
