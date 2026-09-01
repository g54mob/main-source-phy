using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using Localisation;
using UnityEngine;

namespace SleepyNodes;

public class State_TeleprinterText : StateNode
{
	[Serializable]
	public class StringReplacement
	{
		public string Text;

		public EntityContextKeys EntityContextKey;

		public StringReplacement()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7B3]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			Text = "{ID}";
			EntityContextKey = EntityContextKeys.EntityEffected;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<string, object>, bool> _003C_003E9__8_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003COnEnter_003Eb__8_0(KeyValuePair<string, object> x)
		{
			//IL_00d7: Expected O, but got I4
			//IL_0013: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_00a3: Expected O, but got I4
			//IL_011c: Expected O, but got I4
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
			object obj = default(object);
			bool flag = obj == null;
			object obj2 = 0;
			if (flag)
			{
				goto IL_00e5;
			}
			nint num = (nint)typeof(GridReference);
			object obj3 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r9_v2 (Il2CppClass<GridReference>)+130]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r10_v2+130]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r9_v2 (Il2CppClass<GridReference>)+130]");
			object obj6;
			if (num2 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r10_v2+C8]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rax_v9+FFFFFFF8+v48 @ rax_v5*8]");
				bool flag2 = 0 == (nint)typeof(GridReference);
				obj6 = 1;
				if (flag2)
				{
					goto IL_0104;
				}
			}
			obj6 = 0;
			goto IL_0104;
			IL_0104:
			bool flag3 = obj6 == null;
			obj2 = 0;
			if (!flag3)
			{
				obj2 = obj;
			}
			goto IL_00e5;
			IL_00e5:
			bool flag4 = obj2 == null;
			return !flag4;
		}
	}

	public StateNode To;

	public Teleprinter.Teleprinters Printer;

	public bool OnlyQueue;

	public bool WaitUntilComplete;

	public TextIdentifier Text;

	public Teleprinter.TeleprinterAlarmState AlarmState;

	public List<StringReplacement> EntityIDToReplace;

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_004c: Expected I, but got O
		//IL_0676: Expected I4, but got O
		//IL_00a1: Expected I, but got O
		//IL_00a9: Expected O, but got Ref
		//IL_00f6: Expected O, but got Ref
		//IL_0114: Expected O, but got I
		//IL_0125: Expected O, but got I
		//IL_0619: Expected I, but got O
		//IL_0772: Expected I, but got O
		//IL_0235: Expected O, but got I
		//IL_023e: Expected I, but got O
		//IL_02fa: Expected O, but got I
		//IL_0381: Expected O, but got I4
		//IL_0332: Expected O, but got I
		//IL_05ee: Expected I, but got O
		//IL_0499: Expected O, but got I
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Expected O, but got Unknown
		//IL_03b3: Expected O, but got I
		//IL_03eb: Expected O, but got I
		//IL_044c: Expected I, but got O
		//IL_04dc: Expected O, but got I
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Expected O, but got Unknown
		//IL_0514: Expected O, but got I
		//IL_0548: Expected I, but got O
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Expected O, but got Unknown
		//IL_0870: Expected O, but got I
		//IL_0566: Expected O, but got I
		//IL_059b: Expected I, but got O
		//IL_05ae: Expected I, but got O
		//IL_0890: Expected O, but got I
		//IL_08a6: Expected I, but got O
		base.OnEnter(state);
		Teleprinter teleprinter = Teleprinter.GetTeleprinter(Printer);
		if (!(teleprinter != null))
		{
			return;
		}
		bool flag = Text == null;
		nint num = unchecked((nint)null);
		UnityEngine.Object obj = null;
		EntityContextKeys entityContextKeys;
		if (!flag)
		{
			if (!Text.TryGet(out var text))
			{
				return;
			}
			bool flag2 = EntityIDToReplace == null;
			num = unchecked((nint)null);
			obj = (UnityEngine.Object)(&text);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<StringReplacement>.Enumerator enumerator = default(List<StringReplacement>.Enumerator);
				IntPtr intPtr = default(IntPtr);
				string text3 = default(string);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag3 = intPtr == (IntPtr)0;
					num = 0;
					entityContextKeys = (EntityContextKeys)(int)(&intPtr);
					string text2 = (string)(&enumerator);
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v676 @ stack_-B0_v14 (Il2CppMethodInfo)+10]");
						text2 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v676 @ stack_-B0_v14 (Il2CppMethodInfo)+10]");
						if (string.IsNullOrWhiteSpace((string)0))
						{
							continue;
						}
						bool flag4 = state == null;
						num = 0;
						entityContextKeys = EntityContextKeys.EntityTarget;
						if (!flag4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v676 @ stack_-B0_v14 (Il2CppMethodInfo)+18]");
							entityContextKeys = EntityContextKeys.EntityTarget;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v676 @ stack_-B0_v14 (Il2CppMethodInfo)+18]");
							bool flag5 = state.TryGet<MapEntity>(EntityContextKeys.EntityTarget, out var value);
							bool flag6 = !flag5;
							nint num2 = 0;
							if (!flag6)
							{
								bool flag7 = value == null;
								num2 = 0;
								num = (nint)(&value);
								text2 = text3;
								if (flag7)
								{
									throw new NullReferenceException();
								}
								bool flag8 = text3 == null;
								num2 = 0;
								num = (nint)(&value);
								text2 = text3;
								if (flag8)
								{
									throw new NullReferenceException();
								}
								string text4 = text;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v676 @ stack_-B0_v14 (Il2CppMethodInfo)+10]");
								text3 = text4.Replace((string)0, value.ID);
								num2 = unchecked((nint)null);
							}
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				Dictionary<string, GridReference> dictionary = new Dictionary<string, GridReference>(StringComparer.s_ordinalIgnoreCase);
				if (state != null)
				{
					Func<KeyValuePair<string, object>, bool> func = _003C_003Ec._003C_003E9__8_0;
					nint num2;
					if (_003C_003Ec._003C_003E9__8_0 == null)
					{
						Func<KeyValuePair<string, object>, bool> func2 = (_003C_003Ec._003C_003E9__8_0 = delegate
						{
							//IL_00d7: Expected O, but got I4
							//IL_0013: Expected I, but got O
							//IL_002b: Expected O, but got I
							//IL_00a3: Expected O, but got I4
							//IL_011c: Expected O, but got I4
							//IL_0067: Expected O, but got I
							//IL_008c: Expected O, but got I4
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
							object obj22 = default(object);
							bool flag16 = obj22 == null;
							object obj23 = 0;
							if (flag16)
							{
								goto IL_00e5;
							}
							nint num7 = (nint)typeof(GridReference);
							object obj24 = obj22;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r9_v2 (Il2CppClass<GridReference>)+130]");
							object obj25 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r10_v2+130]");
							nint num8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r9_v2 (Il2CppClass<GridReference>)+130]");
							object obj27;
							if (num8 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r10_v2+C8]");
								object obj26 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rax_v9+FFFFFFF8+v48 @ rax_v5*8]");
								bool flag17 = 0 == (nint)typeof(GridReference);
								obj27 = 1;
								if (flag17)
								{
									goto IL_0104;
								}
							}
							obj27 = 0;
							goto IL_0104;
							IL_0104:
							bool flag18 = obj27 == null;
							obj23 = 0;
							if (!flag18)
							{
								obj23 = obj22;
							}
							goto IL_00e5;
							IL_00e5:
							bool flag19 = obj23 == null;
							return !flag19;
						});
						num2 = unchecked((nint)null);
						func = func2;
					}
					IEnumerable<KeyValuePair<string, object>> enumerable = Enumerable.Where(state.State, func);
					bool flag9 = enumerable == null;
					num = 0;
					obj = (UnityEngine.Object)(object)func;
					if (flag9)
					{
						goto IL_066e;
					}
					IEnumerator<KeyValuePair<string, object>> enumerator2 = enumerable.GetEnumerator();
					List<StringReplacement>.Enumerator enumerator4 = default(List<StringReplacement>.Enumerator);
					List<StringReplacement>.Enumerator enumerator3 = enumerator4;
					IntPtr intPtr2 = default(IntPtr);
					object obj9 = default(object);
					object obj17 = default(object);
					object obj18 = default(object);
					List<StringReplacement>.Enumerator enumerator5 = default(List<StringReplacement>.Enumerator);
					string key = default(string);
					object obj21 = default(object);
					while (true)
					{
						object obj3;
						object obj8;
						if (intPtr2 != (IntPtr)0)
						{
							object obj2 = (nint)intPtr2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v814 @ r10_v14+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_036e;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v814 @ r10_v14+B0]");
							obj3 = 0;
							GridReference gridReference = null;
							while (true)
							{
								object obj4 = (object)gridReference + (object)gridReference;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v834 @ r8_v27+v1236 @ rax_v77*8]");
								if (0 == (nint)typeof(IEnumerator))
								{
									break;
								}
								gridReference = (GridReference)(gridReference + 1);
								GridReference gridReference2 = gridReference;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v814 @ r10_v14+12E]");
								if ((nint)gridReference2 < 0)
								{
									continue;
								}
								goto IL_036e;
							}
							object obj5 = (object)gridReference + (object)gridReference;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v834 @ r8_v27+8+v1292 @ rcx_v67*8]");
							object obj6 = (nint)0 << 4;
							object obj7 = obj6 + 312;
							obj8 = obj7 + obj2;
							goto IL_07ed;
						}
						throw new NullReferenceException();
						IL_07ed:
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1297 @ rdx_v32] (should have been resolved before IL gen)");
						if (obj9 == null)
						{
							break;
						}
						object obj16;
						if (intPtr2 != (IntPtr)0)
						{
							object obj10 = (nint)intPtr2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v667 @ r10_v15+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0427;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v667 @ r10_v15+B0]");
							object obj11 = 0;
							GridReference gridReference3 = null;
							while (true)
							{
								object obj12 = (object)gridReference3 + (object)gridReference3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1324 @ r8_v41+v1329 @ rcx_v60*8]");
								if (0 == (nint)typeof(IEnumerator<KeyValuePair<string, object>>))
								{
									break;
								}
								gridReference3 = (GridReference)(gridReference3 + 1);
								GridReference gridReference4 = gridReference3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v667 @ r10_v15+12E]");
								if ((nint)gridReference4 < 0)
								{
									continue;
								}
								goto IL_0427;
							}
							object obj13 = (object)gridReference3 + (object)gridReference3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1324 @ r8_v41+8+v1383 @ rcx_v62*8]");
							object obj14 = (nint)0 << 4;
							object obj15 = obj14 + 312;
							obj16 = obj15 + obj10;
							goto IL_0814;
						}
						throw new NullReferenceException();
						IL_0427:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						obj16 = obj17;
						goto IL_0814;
						IL_0814:
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1388 @ r8_v28] (should have been resolved before IL gen)");
						enumerator3 = (List<StringReplacement>.Enumerator)obj18;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
						bool flag10 = dictionary == null;
						nint num3 = (nint)(&enumerator5);
						GridReference value2;
						if (!flag10)
						{
							nint num4 = (nint)typeof(GridReference);
							if (intPtr == (IntPtr)0)
							{
								value2 = null;
								goto IL_05c0;
							}
							num = intPtr;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v692 @ rdx_v39 (Il2CppClass<GridReference>)+130]");
							object obj19 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ r8_v4 (Il2CppMethodInfo)+130]");
							nint num5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v692 @ rdx_v39 (Il2CppClass<GridReference>)+130]");
							bool flag11 = num5 < 0;
							num2 = intPtr;
							nint num6 = (nint)typeof(GridReference);
							if (!flag11)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ r8_v4 (Il2CppMethodInfo)+C8]");
								object obj20 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v697 @ rax_v68+FFFFFFF8+v696 @ rax_v67*8]");
								bool flag12 = 0 != (nint)typeof(GridReference);
								num2 = intPtr;
								num6 = (nint)typeof(GridReference);
								if (!flag12)
								{
									num = unchecked((nint)null);
									num = intPtr;
									bool flag13 = num == 0;
									value2 = (GridReference)num;
									num2 = intPtr;
									num6 = (nint)typeof(GridReference);
									if (!flag13)
									{
										goto IL_05c0;
									}
								}
							}
							bool flag14 = ((NodeExecutionState)num2).TryGet<MapEntity>((EntityContextKeys)num6, out *(MapEntity*)num);
						}
						throw new NullReferenceException();
						IL_05c0:
						dictionary.set_Item(key, value2);
						continue;
						IL_036e:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						obj3 = 0;
						obj8 = obj21;
						goto IL_07ed;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1804ADBC0");
					num2 = (nint)typeof(IEnumerator);
				}
				List<string> lines = FireMissionTokenProcessor.ProcessBlock(text3, dictionary);
				bool flag15 = (object)teleprinter == null;
				num = unchecked((nint)null);
				obj = (UnityEngine.Object)(object)dictionary;
				if (!flag15)
				{
					bool waitForTrigger = default(bool);
					PrintJob printJob = teleprinter.SubmitLines("Mission Node", lines, null, waitForTrigger);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763370");
					teleprinter.SignalAlarm(AlarmState);
					return;
				}
			}
		}
		goto IL_066e;
		IL_066e:
		entityContextKeys = (EntityContextKeys)obj;
		throw new NullReferenceException();
	}

	public override void OnExecute(NodeExecutionState state)
	{
		if (WaitUntilComplete)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763480");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ stack_8_v4+38]");
				if ((nint)0 == 0)
				{
					return;
				}
			}
		}
		base.OnExit(state, "To");
	}
}
