using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_WaitEntityDestroyed : StateNode
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<MapEntity, bool> _003C_003E9__3_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003COnExecute_003Eb__3_0(MapEntity x)
		{
			//IL_004b: Expected I4, but got O
			if (x != null)
			{
				bool isAlive = x.IsAlive;
				return (byte)((isAlive ? 1u : 0u) ^ 1u) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public StateNode To;

	public TargetSelection Entites;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0080: Expected I, but got O
		//IL_0090: Expected O, but got I
		//IL_00a0: Expected O, but got I
		while (true)
		{
			base.OnEnter(state);
			if (FireMission._003CInstance_003Ek__BackingField != null && !(FireMission._003CInstance_003Ek__BackingField == null))
			{
				break;
			}
			Debug.LogWarning("Fire mission not found, skipping wait");
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ r9_v1 (Il2CppClass<SleepyNodes.State_WaitEntityDestroyed>)+218]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ r9_v1 (Il2CppClass<SleepyNodes.State_WaitEntityDestroyed>)+220]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v157 @ rax_v10 (should have been resolved before IL gen)");
		}
		List<MapEntity> list = Entites.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763370");
	}

	public override void OnExecute(NodeExecutionState state)
	{
		//IL_00c4: Expected I, but got O
		//IL_00d4: Expected O, but got I
		//IL_00e4: Expected O, but got I
		while (!(FireMission._003CInstance_003Ek__BackingField != null))
		{
			Debug.LogWarning("Fire mission not found, skipping wait");
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ r9_v1 (Il2CppClass<SleepyNodes.State_WaitEntityDestroyed>)+218]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ r9_v1 (Il2CppClass<SleepyNodes.State_WaitEntityDestroyed>)+220]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v131 @ rax_v8 (should have been resolved before IL gen)");
		}
		List<MapEntity> list = new List<MapEntity>();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763230");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ stack_20_v1 (System.Collections.Generic.IEnumerable`1<MapEntity>)+18]");
		if ((nint)0 > (nint)0)
		{
			Func<MapEntity, bool> predicate = _003C_003Ec._003C_003E9__3_0;
			if (_003C_003Ec._003C_003E9__3_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__3_0 = delegate(MapEntity x)
				{
					//IL_004b: Expected I4, but got O
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					bool isAlive = x.IsAlive;
					return (byte)((isAlive ? 1u : 0u) ^ 1u) != 0;
				});
			}
			IEnumerable<MapEntity> source = default(IEnumerable<MapEntity>);
			if (!Enumerable.All(source, predicate))
			{
				return;
			}
		}
		base.OnExit(state, "To");
	}
}
