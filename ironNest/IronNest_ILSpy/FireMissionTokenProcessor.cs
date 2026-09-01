using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Cpp2ILInjected;
using Localisation;
using UnityEngine;

public static class FireMissionTokenProcessor
{
	public class LineEvalContext
	{
		public string Raw;

		public int LineIndex;

		public Dictionary<string, MapEntity> SelectedIds;

		public Dictionary<string, GridReference> LocationContextKeys;

		public bool noActiveMatchFound;

		public HashSet<EntityRoles> implicatedRoles;

		public Vector2? FromPos;

		public LineEvalContext()
		{
			Dictionary<string, MapEntity> selectedIds = new Dictionary<string, MapEntity>(StringComparer.s_ordinalIgnoreCase);
			SelectedIds = selectedIds;
			Dictionary<string, GridReference> locationContextKeys = new Dictionary<string, GridReference>(StringComparer.s_ordinalIgnoreCase);
			LocationContextKeys = locationContextKeys;
			HashSet<EntityRoles> hashSet = new HashSet<EntityRoles>();
			implicatedRoles = hashSet;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public class Command
	{
		public class Parameter
		{
			public List<string> Ids;

			public int Index;

			public List<Option> Options;

			public bool RandomFlagSeen;

			public Parameter()
			{
				List<string> ids = new List<string>();
				Ids = ids;
				List<Option> options = new List<Option>();
				Options = options;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			}
		}

		public class Option
		{
			public string Symbol;

			public string Value;
		}

		public static class OptionSynbols
		{
			public static string Index = ":";

			public static string Add = "+";

			public static string Subtract = "-";
		}

		public string Raw;

		public string Cmd;

		public List<string> Modifiers;

		public List<Parameter> Parameters;

		public Command()
		{
			//IL_0048: Expected O, but got I
			//IL_0058: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rax_v2+B8]");
			object cmd = 0;
			Cmd = (string)cmd;
			List<string> modifiers = new List<string>();
			Modifiers = modifiers;
			List<Parameter> parameters = new List<Parameter>();
			Parameters = parameters;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Match, string> _003C_003E9__5_0;

		public static Func<string, string> _003C_003E9__5_1;

		public static Func<Command.Option, bool> _003C_003E9__15_0;

		public static Func<Command.Option, bool> _003C_003E9__15_1;

		public static Func<MapEntity, bool> _003C_003E9__28_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CParseCommand_003Eb__5_0(Match m)
		{
			if (m != null)
			{
				GroupCollection groups = m.Groups;
				if (groups != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
					Capture capture = default(Capture);
					if (capture != null)
					{
						return capture.Value;
					}
				}
			}
			return (string)(object)new NullReferenceException();
		}

		internal string _003CParseCommand_003Eb__5_1(string m)
		{
			if (m != null)
			{
				string text = m.Trim();
				if (text != null)
				{
					return text.ToLowerInvariant();
				}
			}
			return (string)(object)new NullReferenceException();
		}

		internal bool _003CFormatTimer_003Eb__15_0(Command.Option x)
		{
			//IL_0018: Expected I4, but got O
			if (x == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return x.Symbol == Command.OptionSynbols.Add;
		}

		internal bool _003CFormatTimer_003Eb__15_1(Command.Option x)
		{
			//IL_0018: Expected I4, but got O
			if (x == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return x.Symbol == Command.OptionSynbols.Subtract;
		}

		internal unsafe bool _003CTryResolveRoleToRandomEntity_003Eb__28_1(MapEntity x)
		{
			//IL_0030: Expected I4, but got O
			if (x == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			object obj = default(object);
			object obj2 = default(object);
			bool flag = FlagExtensions.Has((MapEntityStates)(int)(&obj), (MapEntityStates)(int)(&obj2));
			return (byte)((flag ? 1u : 0u) ^ 1u) != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass14_0
	{
		public EntityRoles role;

		internal unsafe bool _003CFormatRemaining_003Eb__0(MapEntity e)
		{
			//IL_006d: Expected I4, but got O
			//IL_003c: Expected O, but got I4
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			if (e == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			object obj = default(object);
			object obj2 = default(object);
			if (FlagExtensions.Has((MapEntityStates)(int)(&obj), (MapEntityStates)(int)(&obj2)))
			{
				return false;
			}
			object obj3 = e.Role & role;
			object obj4 = obj3 - role;
			return obj4 == null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass27_0
	{
		public MapEntity entity;

		internal bool _003CGetDisplayName_003Eb__0(KeyValuePair<string, MapEntity> x)
		{
			//IL_0089: Expected I4, but got O
			//IL_0072: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
			object obj = default(object);
			if (obj != null)
			{
				MapEntity mapEntity = entity;
				if (entity != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
						return ((string)0).Equals(mapEntity.RawID, StringComparison.OrdinalIgnoreCase);
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass27_1
	{
		public MapEntity to;

		internal bool _003CGetDisplayName_003Eb__1(KeyValuePair<string, MapEntity> x)
		{
			//IL_0089: Expected I4, but got O
			//IL_0072: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
			object obj = default(object);
			if (obj != null)
			{
				MapEntity mapEntity = to;
				if (to != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
						return ((string)0).Equals(mapEntity.RawID, StringComparison.OrdinalIgnoreCase);
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public EntityRoles role;

		public LineEvalContext context;

		public string roleString;

		internal bool _003CTryResolveRoleToRandomEntity_003Eb__0(MapEntity e)
		{
			//IL_0062: Expected I4, but got O
			//IL_0031: Expected O, but got I4
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Expected O, but got Unknown
			if (e != null)
			{
				object obj = e.Role & role;
				object obj2 = obj - role;
				return obj2 == null;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CTryResolveRoleToRandomEntity_003Eb__2(MapEntity candidate)
		{
			//IL_009a: Expected I4, but got O
			_003C_003Ec__DisplayClass28_1 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass28_1();
			if (CS_0024_003C_003E8__locals6 != null)
			{
				CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals1 = this;
				CS_0024_003C_003E8__locals6.candidate = candidate;
				LineEvalContext lineEvalContext = context;
				if (context != null)
				{
					Func<KeyValuePair<string, MapEntity>, bool> predicate = delegate
					{
						//IL_0098: Expected I4, but got O
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
						_003C_003Ec__DisplayClass28_0 obj = CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals1;
						if (CS_0024_003C_003E8__locals6.CS_0024_003C_003E8__locals1 != null)
						{
							string value = obj.roleString + ":";
							string text = default(string);
							if (text != null)
							{
								bool flag2 = text.StartsWith(value);
								if (!flag2)
								{
									return flag2;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
								object obj2 = (object)text - (object)CS_0024_003C_003E8__locals6.candidate;
								return obj2 == null;
							}
						}
						NullReferenceException ex2 = new NullReferenceException();
						return (byte)(int)ex2 != 0;
					};
					bool flag = Enumerable.Any(lineEvalContext.SelectedIds, predicate);
					return (byte)((flag ? 1u : 0u) ^ 1u) != 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass28_1
	{
		public MapEntity candidate;

		public _003C_003Ec__DisplayClass28_0 CS_0024_003C_003E8__locals1;

		internal bool _003CTryResolveRoleToRandomEntity_003Eb__3(KeyValuePair<string, MapEntity> existing)
		{
			//IL_0098: Expected I4, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
			_003C_003Ec__DisplayClass28_0 obj = CS_0024_003C_003E8__locals1;
			if (CS_0024_003C_003E8__locals1 != null)
			{
				string value = obj.roleString + ":";
				string text = default(string);
				if (text != null)
				{
					bool flag = text.StartsWith(value);
					if (!flag)
					{
						return flag;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
					object obj2 = (object)text - (object)candidate;
					return obj2 == null;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public LineEvalContext eval;

		internal string _003CProcessLine_003Eg__Replacer_007C0(Match m)
		{
			if (m != null)
			{
				GroupCollection groups = m.Groups;
				if (groups != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
					Capture capture = default(Capture);
					if (capture != null)
					{
						string value = capture.Value;
						if (value != null)
						{
							string text = value.Trim();
							Command command = ParseCommand(text);
							if (command != null)
							{
								return ProcessToken(command, eval);
							}
							return text;
						}
					}
				}
			}
			return (string)(object)new NullReferenceException();
		}
	}

	private static readonly Regex tokenRegex;

	private static readonly Regex parametereRegex;

	private static readonly Regex parameterePartRegex;

	public static Command ParseCommand(string text)
	{
		Command command;
		if (!string.IsNullOrWhiteSpace(text))
		{
			Match match = Regex.Match(text, "^\\S+");
			if (match != null)
			{
				if (!match.Success)
				{
					goto IL_0220;
				}
				string value = match.Value;
				if (text != null)
				{
					string input = text.Substring(((Capture)match)._003CLength_003Ek__BackingField);
					if (parametereRegex != null)
					{
						MatchCollection source = parametereRegex.Matches(text);
						Func<Match, string> selector = _003C_003Ec._003C_003E9__5_0;
						if (_003C_003Ec._003C_003E9__5_0 == null)
						{
							selector = (_003C_003Ec._003C_003E9__5_0 = delegate(Match m)
							{
								if (m != null)
								{
									GroupCollection groups = m.Groups;
									if (groups != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
										Capture capture = default(Capture);
										if (capture != null)
										{
											return capture.Value;
										}
									}
								}
								return (string)(object)new NullReferenceException();
							});
						}
						IEnumerable<string> source2 = Enumerable.Select(source, selector);
						if (parametereRegex != null)
						{
							string text2 = parametereRegex.Replace(input, " ");
							if (text2 != null)
							{
								string[] source3 = text2.Split(' ', StringSplitOptions.RemoveEmptyEntries);
								command = new Command();
								if (command != null)
								{
									command.Raw = text;
									if (value != null)
									{
										string cmd = value.ToLowerInvariant();
										command.Cmd = cmd;
										Func<string, string> selector2 = _003C_003Ec._003C_003E9__5_1;
										if (_003C_003Ec._003C_003E9__5_1 == null)
										{
											selector2 = (_003C_003Ec._003C_003E9__5_1 = delegate(string m)
											{
												if (m != null)
												{
													string text3 = m.Trim();
													if (text3 != null)
													{
														return text3.ToLowerInvariant();
													}
												}
												return (string)(object)new NullReferenceException();
											});
										}
										IEnumerable<string> source4 = Enumerable.Select(source3, selector2);
										List<string> modifiers = Enumerable.ToList(source4);
										command.Modifiers = modifiers;
										Func<string, Command.Parameter> selector3 = ParseParameter;
										IEnumerable<Command.Parameter> source5 = Enumerable.Select(source2, selector3);
										List<Command.Parameter> parameters = Enumerable.ToList(source5);
										command.Parameters = parameters;
										goto IL_0339;
									}
								}
							}
						}
					}
				}
			}
			return (Command)(object)new NullReferenceException();
		}
		goto IL_0220;
		IL_0220:
		command = null;
		goto IL_0339;
		IL_0339:
		return command;
	}

	private unsafe static Command.Parameter ParseParameter(string text)
	{
		//IL_098a: Expected I, but got O
		//IL_002b: Expected I, but got O
		//IL_0058: Expected O, but got I4
		//IL_0937: Expected O, but got Ref
		//IL_0949: Expected O, but got Ref
		//IL_0afd: Expected I, but got O
		//IL_0b1a: Expected I, but got O
		//IL_0b28: Expected I, but got O
		//IL_00ef: Expected O, but got I4
		//IL_016e: Expected O, but got I
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_01a4: Expected I, but got O
		//IL_01b4: Expected O, but got I
		//IL_01e0: Expected I, but got O
		//IL_01ee: Expected I, but got O
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0214: Expected O, but got I
		//IL_0241: Expected I, but got O
		//IL_024f: Expected I, but got O
		//IL_029f: Expected I, but got O
		//IL_02e4: Expected I, but got O
		//IL_02e9: Expected I, but got O
		//IL_02f2: Expected I, but got O
		//IL_031d: Expected I, but got O
		//IL_0357: Expected I, but got O
		//IL_035c: Expected I, but got O
		//IL_03a1: Expected I, but got O
		//IL_03a6: Expected I, but got O
		//IL_03af: Expected I, but got O
		//IL_03ef: Expected I, but got O
		//IL_03f4: Expected I, but got O
		//IL_03f9: Expected I, but got O
		//IL_0439: Expected I, but got O
		//IL_043e: Expected I, but got O
		//IL_0443: Expected I, but got O
		//IL_0486: Expected I, but got O
		//IL_05e4: Expected I, but got O
		//IL_05e9: Expected I, but got O
		//IL_05ee: Expected I, but got O
		//IL_04d1: Expected I, but got O
		//IL_04d6: Expected I, but got O
		//IL_04db: Expected I, but got O
		//IL_0626: Expected I, but got O
		//IL_062b: Expected I, but got O
		//IL_0630: Expected I, but got O
		//IL_0666: Expected I, but got O
		//IL_051d: Expected I, but got O
		//IL_0522: Expected I, but got O
		//IL_0527: Expected I, but got O
		//IL_0690: Expected I, but got O
		//IL_054a: Expected I4, but got O
		//IL_0576: Expected I, but got O
		//IL_057b: Expected I, but got O
		//IL_0583: Expected I, but got O
		//IL_0921: Expected I, but got O
		//IL_092a: Expected O, but got I4
		//IL_05ba: Expected I, but got O
		//IL_07f4: Expected I, but got O
		//IL_07fd: Expected O, but got I4
		//IL_085b: Expected I, but got O
		//IL_0860: Expected I, but got O
		//IL_0865: Expected I, but got O
		//IL_0701: Expected I, but got O
		//IL_0889: Expected I4, but got O
		//IL_08b5: Expected I, but got O
		//IL_08ba: Expected I, but got O
		//IL_08c2: Expected I, but got O
		//IL_073c: Expected I, but got O
		//IL_0741: Expected I, but got O
		//IL_0746: Expected I, but got O
		//IL_08f8: Expected I, but got O
		//IL_0901: Expected O, but got I4
		//IL_0769: Expected I4, but got O
		//IL_0795: Expected I, but got O
		//IL_079a: Expected I, but got O
		//IL_07a2: Expected I, but got O
		//IL_07d8: Expected I, but got O
		//IL_07e1: Expected O, but got I4
		Command.Parameter parameter = new Command.Parameter();
		bool flag = parameterePartRegex == null;
		string text2 = null;
		nint num4;
		string text4;
		if (!flag)
		{
			MatchCollection matchCollection = parameterePartRegex.Matches(text);
			bool flag2 = matchCollection == null;
			nint num = unchecked((nint)null);
			text2 = text;
			if (!flag2)
			{
				IEnumerator enumerator = matchCollection.GetEnumerator();
				object obj = 1;
				object obj2 = default(object);
				object obj3 = default(object);
				object obj15 = default(object);
				object obj16 = default(object);
				object obj17 = default(object);
				string text3 = default(string);
				while (true)
				{
					object obj12;
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj3 != null)
						{
							bool flag3 = obj2 == null;
							MatchCollection matchCollection2 = null;
							if (flag3)
							{
								goto IL_0a51;
							}
							object obj4 = obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ r10_v22+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0126;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ r10_v22+B0]");
							num = 0;
							object obj5 = 0;
							while (true)
							{
								object obj6 = obj5 + obj5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1089 @ r8_v6 (Il2CppClass<System.String>)+v767 @ rax_v101*8]");
								if (0 == (nint)typeof(IEnumerator))
								{
									break;
								}
								obj5++;
								object obj7 = obj5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ r10_v22+12E]");
								if ((nint)obj7 < 0)
								{
									continue;
								}
								goto IL_0126;
							}
							object obj8 = obj5 + obj5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1089 @ r8_v6 (Il2CppClass<System.String>)+8+v909 @ rcx_v73*8]");
							object obj9 = (nint)0 + (nint)1;
							object obj10 = obj9 << 4;
							object obj11 = obj10 + 312;
							obj12 = obj11 + obj4;
							goto IL_0ae5;
						}
						object obj13 = (object)(&obj2);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj14 = (object)(&obj15);
						obj14 = obj16;
						if (obj16 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						break;
					}
					throw new NullReferenceException();
					IL_0a51:
					throw new NullReferenceException();
					IL_0126:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					num = 1;
					obj12 = obj17;
					goto IL_0ae5;
					IL_0ae5:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v916 @ rdx_v31] (should have been resolved before IL gen)");
					nint num2 = (nint)typeof(Match);
					bool flag4 = text3 == null;
					nint num3 = (nint)typeof(IEnumerator);
					num4 = (nint)typeof(Match);
					text4 = text3;
					if (!flag4)
					{
						num = (nint)text3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rdx_v33 (Il2CppClass<System.Text.RegularExpressions.Match>)+130]");
						object obj18 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1089 @ r8_v6 (Il2CppClass<System.String>)+130]");
						nint num5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rdx_v33 (Il2CppClass<System.Text.RegularExpressions.Match>)+130]");
						bool flag5 = num5 < 0;
						num3 = (nint)typeof(IEnumerator);
						num4 = (nint)typeof(Match);
						text4 = text3;
						if (!flag5)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1089 @ r8_v6 (Il2CppClass<System.String>)+C8]");
							object obj19 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1067 @ rax_v62+FFFFFFF8+v1012 @ rax_v61*8]");
							bool flag6 = 0 != (nint)typeof(Match);
							num3 = (nint)typeof(IEnumerator);
							num4 = (nint)typeof(Match);
							text4 = text3;
							if (!flag6)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1089 @ r8_v6 (Il2CppClass<System.String>)+190]");
								num4 = 0;
								GroupCollection groupCollection = (GroupCollection)((IEnumerable)text3).GetEnumerator();
								bool flag7 = groupCollection == null;
								num3 = (nint)typeof(IEnumerator);
								text4 = text3;
								if (!flag7)
								{
									Group obj20 = groupCollection.get_Item("symbol");
									bool flag8 = obj20 == null;
									num3 = (nint)typeof(IEnumerator);
									num = unchecked((nint)null);
									num4 = unchecked((nint)"symbol");
									text4 = text3;
									if (!flag8)
									{
										string value = obj20.Value;
										nint num6 = (nint)text3;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1249 @ rdx_v37 (Il2CppClass<System.String>)+190]");
										num4 = 0;
										GroupCollection groupCollection2 = (GroupCollection)((IEnumerable)text3).GetEnumerator();
										bool flag9 = groupCollection2 == null;
										num3 = (nint)typeof(IEnumerator);
										num = unchecked((nint)null);
										text4 = text3;
										if (!flag9)
										{
											Group obj21 = groupCollection2.get_Item("value");
											bool flag10 = obj21 == null;
											num3 = (nint)typeof(IEnumerator);
											num = unchecked((nint)null);
											num4 = unchecked((nint)"value");
											text4 = text3;
											if (!flag10)
											{
												string value2 = obj21.Value;
												bool flag11 = value2 == null;
												num3 = (nint)typeof(IEnumerator);
												num = unchecked((nint)null);
												num4 = unchecked((nint)null);
												text4 = text3;
												if (!flag11)
												{
													string text5 = value2.Trim();
													bool flag12 = text5 == null;
													num3 = (nint)typeof(IEnumerator);
													num = unchecked((nint)null);
													num4 = unchecked((nint)null);
													text4 = text3;
													if (!flag12)
													{
														string text6 = text5.ToLowerInvariant();
														bool flag13 = string.IsNullOrWhiteSpace(text6);
														num3 = (nint)typeof(IEnumerator);
														if (flag13)
														{
															continue;
														}
														nint num7;
														if (obj == null)
														{
															bool flag14 = parameter == null;
															num3 = (nint)typeof(IEnumerator);
															num = unchecked((nint)null);
															num4 = unchecked((nint)null);
															text4 = text3;
															if (flag14)
															{
																throw new NullReferenceException();
															}
															Command.Option option = new Command.Option();
															bool flag15 = option == null;
															num3 = (nint)typeof(IEnumerator);
															num = unchecked((nint)null);
															num4 = unchecked((nint)null);
															text4 = (string)(object)option;
															if (flag15)
															{
																throw new NullReferenceException();
															}
															((string)(object)option)._stringLength = (int)value;
															bool flag16 = parameter.Options == null;
															num3 = (nint)typeof(IEnumerator);
															num = unchecked((nint)null);
															num4 = (nint)text6;
															text4 = (string)(object)option;
															if (flag16)
															{
																throw new NullReferenceException();
															}
															parameter.Options.Add(option);
															num7 = 0;
															num4 = (nint)option;
															text4 = (string)(object)option;
														}
														else
														{
															bool flag17 = parameter == null;
															num3 = (nint)typeof(IEnumerator);
															num = unchecked((nint)null);
															num4 = unchecked((nint)null);
															text4 = text3;
															if (flag17)
															{
																throw new NullReferenceException();
															}
															bool flag18 = parameter.Ids == null;
															num3 = (nint)typeof(IEnumerator);
															num = unchecked((nint)null);
															num4 = unchecked((nint)null);
															text4 = text3;
															if (flag18)
															{
																throw new NullReferenceException();
															}
															parameter.Ids.Add(text6);
															num7 = 0;
															num4 = (nint)text6;
															text4 = text3;
														}
														bool flag19 = value == null;
														num3 = (nint)typeof(IEnumerator);
														num = num7;
														if (!flag19)
														{
															if (!value.Contains(":"))
															{
																if (!value.Contains(","))
																{
																	bool flag20 = string.IsNullOrEmpty(value);
																	num3 = (nint)typeof(IEnumerator);
																	if (!flag20)
																	{
																		Command.Option option2 = new Command.Option();
																		bool flag21 = option2 == null;
																		num3 = (nint)typeof(IEnumerator);
																		num = unchecked((nint)null);
																		num4 = unchecked((nint)null);
																		text4 = (string)(object)option2;
																		if (flag21)
																		{
																			throw new NullReferenceException();
																		}
																		((string)(object)option2)._stringLength = (int)value;
																		bool flag22 = parameter.Options == null;
																		num3 = (nint)typeof(IEnumerator);
																		num = unchecked((nint)null);
																		num4 = (nint)text6;
																		text4 = (string)(object)option2;
																		if (flag22)
																		{
																			throw new NullReferenceException();
																		}
																		parameter.Options.Add(option2);
																		num3 = (nint)typeof(IEnumerator);
																		obj = 0;
																	}
																}
																else
																{
																	num3 = (nint)typeof(IEnumerator);
																	obj = 1;
																}
																continue;
															}
															parameter.RandomFlagSeen = true;
															if (!int.TryParse(text6, out var result))
															{
																Command.Option option3 = new Command.Option();
																bool flag23 = option3 == null;
																num3 = (nint)typeof(IEnumerator);
																num = unchecked((nint)null);
																num4 = unchecked((nint)null);
																text4 = (string)(object)option3;
																if (flag23)
																{
																	throw new NullReferenceException();
																}
																((string)(object)option3)._stringLength = (int)":";
																bool flag24 = parameter.Options == null;
																num3 = (nint)typeof(IEnumerator);
																num = unchecked((nint)null);
																num4 = (nint)text6;
																text4 = (string)(object)option3;
																if (flag24)
																{
																	throw new NullReferenceException();
																}
																parameter.Options.Add(option3);
																num3 = (nint)typeof(IEnumerator);
																obj = 0;
															}
															else
															{
																parameter.Index = result;
																num3 = (nint)typeof(IEnumerator);
																obj = 0;
															}
															continue;
														}
														throw new NullReferenceException();
													}
													throw new NullReferenceException();
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						goto IL_0a51;
					}
					throw new NullReferenceException();
				}
				return parameter;
			}
		}
		num4 = (nint)text2;
		text4 = text;
		throw new NullReferenceException();
	}

	public static List<string> ProcessBlock(string text, Dictionary<string, GridReference> locationContextKeys = null)
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		LineEvalContext lineEvalContext = new LineEvalContext();
		lineEvalContext.LocationContextKeys = locationContextKeys;
		string[] array = new string[2];
		if (array.Length > 0)
		{
			array[0] = "\r\n";
			if (array.Length > 1)
			{
				array[1] = "\n";
				string[] array2 = text.Split(array, StringSplitOptions.None);
				List<string> list = new List<string>(array2.Length);
				object obj = array2 + 32;
				int num = 0;
				while (true)
				{
					if (num < array2.Length)
					{
						if (num >= array2.Length)
						{
							break;
						}
						string item = ProcessLine((string)obj, num, lineEvalContext);
						list.Add(item);
						num++;
						obj += 8;
						continue;
					}
					return list;
				}
			}
		}
		return (List<string>)(object)new IndexOutOfRangeException();
	}

	private unsafe static string ProcessLine(string line, int lineIndex, LineEvalContext eval = null)
	{
		//IL_0347: Expected O, but got I
		//IL_0332: Expected O, but got I
		//IL_031d: Expected O, but got I
		//IL_0308: Expected O, but got I
		_003C_003Ec__DisplayClass8_0 CS_0024_003C_003E8__locals13 = new _003C_003Ec__DisplayClass8_0();
		string result;
		if (CS_0024_003C_003E8__locals13 != null)
		{
			CS_0024_003C_003E8__locals13.eval = eval;
			bool flag = string.IsNullOrEmpty(line);
			result = line;
			if (flag)
			{
				goto IL_034c;
			}
			if (CS_0024_003C_003E8__locals13.eval == null)
			{
				LineEvalContext eval2 = new LineEvalContext();
				CS_0024_003C_003E8__locals13.eval = eval2;
			}
			LineEvalContext eval3 = CS_0024_003C_003E8__locals13.eval;
			if (CS_0024_003C_003E8__locals13.eval != null)
			{
				eval3.Raw = line;
				LineEvalContext eval4 = CS_0024_003C_003E8__locals13.eval;
				if (CS_0024_003C_003E8__locals13.eval != null)
				{
					eval4.LineIndex = lineIndex;
					MatchEvaluator evaluator = delegate(Match m)
					{
						if (m != null)
						{
							GroupCollection groups = m.Groups;
							if (groups != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
								Capture capture = default(Capture);
								if (capture != null)
								{
									string value = capture.Value;
									if (value != null)
									{
										string text2 = value.Trim();
										Command command = ParseCommand(text2);
										if (command != null)
										{
											return ProcessToken(command, CS_0024_003C_003E8__locals13.eval);
										}
										return text2;
									}
								}
							}
						}
						return (string)(object)new NullReferenceException();
					};
					if (tokenRegex != null)
					{
						string text = tokenRegex.Replace(line, evaluator);
						bool flag2 = FireMission._003CInstance_003Ek__BackingField != null;
						bool flag3 = !flag2;
						result = text;
						if (flag3)
						{
							goto IL_034c;
						}
						FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
						if ((object)FireMission._003CInstance_003Ek__BackingField != null)
						{
							bool flag4 = !fireMission.useAlternateTextWhenNoActive;
							result = text;
							if (flag4)
							{
								goto IL_034c;
							}
							LineEvalContext eval5 = CS_0024_003C_003E8__locals13.eval;
							if (CS_0024_003C_003E8__locals13.eval != null)
							{
								bool flag5 = !eval5.noActiveMatchFound;
								result = text;
								if (flag5)
								{
									goto IL_034c;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
								LineEvalContext eval6 = CS_0024_003C_003E8__locals13.eval;
								if (CS_0024_003C_003E8__locals13.eval != null)
								{
									HashSet<EntityRoles> implicatedRoles = eval6.implicatedRoles;
									object obj = default(object);
									if (obj != null)
									{
										if (eval6.implicatedRoles != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rdi_v6 (System.Collections.Generic.HashSet`1<EntityRoles>)+20]");
											object obj2 = default(object);
											if ((nint)0 != 0 && !eval6.implicatedRoles.Contains((EntityRoles)(int)(&obj2)))
											{
												if (eval6.implicatedRoles.Contains((EntityRoles)(int)(&obj2)))
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v27+58]");
													return (string)0;
												}
												if (eval6.implicatedRoles.Contains((EntityRoles)(int)(&obj2)))
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v27+60]");
													return (string)0;
												}
												if (eval6.implicatedRoles.Contains((EntityRoles)(int)(&obj2)))
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v27+68]");
													return (string)0;
												}
											}
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v27+50]");
										return (string)0;
									}
								}
							}
						}
					}
				}
			}
		}
		return (string)(object)new NullReferenceException();
		IL_034c:
		return result;
	}

	public static string ProcessToken(Command command, LineEvalContext context)
	{
		//IL_04cf: Expected O, but got I
		//IL_04df: Expected O, but got I
		string text;
		string text3;
		string text8;
		if (command != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1804FA610");
			object obj = default(object);
			if ((nint)obj > 1281379241)
			{
				if ((long)obj > 2753264687L)
				{
					if ((long)obj != 2944866961L)
					{
						if ((long)obj == 3748513642L)
						{
							text = "direction";
						}
						else
						{
							if ((long)obj != 3915559316L)
							{
								goto IL_04bf;
							}
							text = "dir";
						}
						goto IL_0563;
					}
					if (command.Cmd == "grid")
					{
						string text2 = FormatGrid(command, context);
						text3 = text2;
						goto IL_053d;
					}
				}
				else if ((nint)obj == 1550380322)
				{
					if (command.Cmd == "timer")
					{
						string text4 = FormatTimer(command, context);
						text3 = text4;
						goto IL_053d;
					}
				}
				else if ((nint)obj == 1813141509)
				{
					if (command.Cmd == "bearing")
					{
						string text5 = FormatBearing(command, context);
						text3 = text5;
						goto IL_053d;
					}
				}
				else if ((long)obj == 2753264687L)
				{
					text = "compass";
					goto IL_0563;
				}
			}
			else
			{
				if ((nint)obj <= 414084241)
				{
					if ((nint)obj == 52100733)
					{
						if (!(command.Cmd == "region"))
						{
							goto IL_04bf;
						}
						string text6 = FormatRegion(command, context);
						text3 = text6;
					}
					else if ((nint)obj == 407766659)
					{
						if (!(command.Cmd == "remaining"))
						{
							goto IL_04bf;
						}
						string text7 = FormatRemaining(command, context);
						text3 = text7;
					}
					else
					{
						if ((nint)obj != 414084241 || !(command.Cmd == "point"))
						{
							goto IL_04bf;
						}
						string displayName = GetDisplayName(command, context);
						text3 = displayName;
					}
					goto IL_053d;
				}
				if ((nint)obj == 783488098)
				{
					text8 = "distance";
					goto IL_0592;
				}
				if ((nint)obj == 845187144)
				{
					if (command.Cmd == "target")
					{
						text3 = "the target";
						goto IL_053d;
					}
				}
				else if ((nint)obj == 1281379241)
				{
					text8 = "dist";
					goto IL_0592;
				}
			}
			goto IL_04bf;
		}
		return (string)(object)new NullReferenceException();
		IL_0563:
		if (!(command.Cmd == text))
		{
			goto IL_04bf;
		}
		string text9 = FormatDirection(command, context);
		text3 = text9;
		goto IL_053d;
		IL_0592:
		if (!(command.Cmd == text8))
		{
			goto IL_04bf;
		}
		string text10 = FormatDistance(command, context);
		text3 = text10;
		goto IL_053d;
		IL_053d:
		if (string.IsNullOrWhiteSpace(text3))
		{
			return "[" + command.Raw + "]";
		}
		return text3;
		IL_04bf:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ rax_v9+B8]");
		object obj3 = 0;
		text3 = (string)obj3;
		goto IL_053d;
	}

	private unsafe static string FormatBearing(Command command, LineEvalContext context)
	{
		//IL_0083: Expected O, but got Ref
		//IL_00ae: Expected O, but got I4
		//IL_0150: Invalid comparison between I4 and F4
		List<Command.Parameter> parameters = command.Parameters;
		if (parameters._size >= 2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Command.Parameter parameter = default(Command.Parameter);
			if (TryResolvePosition(parameter, context, out var pos))
			{
				Vector2? vector = (Vector2)(&parameter);
				if (context != null)
				{
					context.FromPos = (Vector2?)(object)0;
					_ = 0;
					if (command.Parameters != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (TryResolvePosition(parameter, context, out var pos2))
						{
							object obj = pos2 - pos;
							object obj3 = default(object);
							object obj4 = default(object);
							object obj2 = obj3 - obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
							float num = (float)obj * 57.29578f;
							if (0f > num)
							{
								num += 360f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
							int num2 = default(int);
							return num2.ToString("000");
						}
						goto IL_017c;
					}
				}
				return (string)(object)new NullReferenceException();
			}
		}
		goto IL_017c;
		IL_017c:
		return "???";
	}

	private unsafe static string FormatDistance(Command command, LineEvalContext context)
	{
		//IL_0083: Expected O, but got Ref
		//IL_00ae: Expected O, but got I4
		//IL_01f0: Expected I, but got O
		List<Command.Parameter> parameters = command.Parameters;
		if (parameters._size >= 2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Command.Parameter parameter = default(Command.Parameter);
			if (TryResolvePosition(parameter, context, out var pos))
			{
				Vector2? vector = (Vector2)(&parameter);
				if (context != null)
				{
					context.FromPos = (Vector2?)(object)0;
					_ = 0;
					if (command.Parameters != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (TryResolvePosition(parameter, context, out var pos2))
						{
							nint num = (nint)typeof(Math);
							object obj = pos - pos2;
							object obj3 = default(object);
							object obj4 = default(object);
							object obj2 = obj3 - obj4;
							object obj5 = obj2 * obj2;
							object obj6 = obj * obj;
							double d = (double)obj5 + (double)obj6;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ rcx_v13 (Il2CppClass<System.Math>)+E4]");
							if ((nint)0 <= (nint)0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
							}
							else
							{
								double num2 = Math.Sqrt(d);
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm6,xmm0\"");
							float num3 = default(float);
							string text = num3.ToString("F2");
							return text + "km";
						}
						goto IL_01ce;
					}
				}
				return (string)(object)new NullReferenceException();
			}
		}
		goto IL_01ce;
		IL_01ce:
		return "???";
	}

	private unsafe static string FormatGrid(Command command, LineEvalContext context)
	{
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected Ref, but got Unknown
		//IL_023e: Expected O, but got I
		//IL_02d0: Expected O, but got I4
		//IL_026b: Expected O, but got I
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Expected O, but got Unknown
		//IL_0356: Expected O, but got I
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected Ref, but got Unknown
		//IL_00c4: Expected O, but got I
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Expected O, but got Unknown
		//IL_02fd: Expected O, but got I
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_011e: Expected native int or pointer, but got O
		//IL_0155: Expected O, but got I
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected Ref, but got Unknown
		//IL_01c8: Expected O, but got I
		//IL_01f5: Expected O, but got I4
		_ = 0;
		Vector2 localPos = Vector2.zeroVector;
		_ = Vector2.zeroVector;
		List<Command.Parameter> parameters = command.Parameters;
		bool flag = parameters._size == 1;
		object obj2 = default(object);
		if (parameters._size >= 1)
		{
			List<Command.Parameter> parameters2;
			object obj4;
			if (!flag)
			{
				if (parameters._size == 2)
				{
					object obj = obj2 - 64;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					ref Vector2 pos = ref *(Vector2*)(obj2 + 40);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
					if (TryResolvePosition((Command.Parameter)0, context, out pos))
					{
						Vector2 value = (Vector2)(obj2 + 40);
						Vector2? vector = (Vector2?)(object)(obj2 - 64);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
						_ = 0;
						_ = 0;
						_ = 0;
						*(Vector2?*)(nint)vector = value;
						if (context != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
							context.FromPos = (Vector2?)(object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-38]");
							_ = 0;
							if (command.Parameters != null)
							{
								object obj3 = obj2 + 40;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								ref Vector2 pos2 = ref *(Vector2*)(obj2 + 16);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
								if (TryResolvePointPositionOnly((Command.Parameter)0, context, out pos2))
								{
									goto IL_025b;
								}
								parameters2 = command.Parameters;
								obj4 = 1;
								goto IL_032e;
							}
						}
						return (string)(object)new NullReferenceException();
					}
				}
				goto IL_038b;
			}
			object obj5 = obj2 + 40;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			ref Vector2 pos3 = ref *(Vector2*)(obj2 + 16);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
			if (TryResolvePointPositionOnly((Command.Parameter)0, context, out pos3))
			{
				goto IL_025b;
			}
			parameters2 = command.Parameters;
			obj4 = 0;
			goto IL_032e;
		}
		return "???";
		IL_025b:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+10]");
		localPos = (Vector2)0;
		goto IL_038b;
		IL_032e:
		object obj6 = obj2 + 16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+10]");
		object obj7 = 0;
		object obj8 = obj2 + 40;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
		return (string)0;
		IL_038b:
		FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
		FireMission fireMission2 = FireMission._003CInstance_003Ek__BackingField;
		FireMission fireMission3 = FireMission._003CInstance_003Ek__BackingField;
		int unusedRowDecimals = default(int);
		return GridCodeConverter.LocalToCode(localPos, fireMission.cellWidth, fireMission2.cellHeight, fireMission3.yIncreasesUp, unusedRowDecimals);
	}

	private unsafe static string FormatRegion(Command command, LineEvalContext context)
	{
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected Ref, but got Unknown
		//IL_023e: Expected O, but got I
		//IL_02d0: Expected O, but got I4
		//IL_026b: Expected O, but got I
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Expected O, but got Unknown
		//IL_0356: Expected O, but got I
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected Ref, but got Unknown
		//IL_00c4: Expected O, but got I
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Expected O, but got Unknown
		//IL_02fd: Expected O, but got I
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_011e: Expected native int or pointer, but got O
		//IL_0155: Expected O, but got I
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected Ref, but got Unknown
		//IL_01c8: Expected O, but got I
		//IL_01f5: Expected O, but got I4
		_ = 0;
		Vector2 localPos = Vector2.zeroVector;
		_ = Vector2.zeroVector;
		List<Command.Parameter> parameters = command.Parameters;
		bool flag = parameters._size == 1;
		object obj2 = default(object);
		if (parameters._size >= 1)
		{
			List<Command.Parameter> parameters2;
			object obj4;
			if (!flag)
			{
				if (parameters._size == 2)
				{
					object obj = obj2 - 64;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					ref Vector2 pos = ref *(Vector2*)(obj2 + 40);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
					if (TryResolvePosition((Command.Parameter)0, context, out pos))
					{
						Vector2 value = (Vector2)(obj2 + 40);
						Vector2? vector = (Vector2?)(object)(obj2 - 64);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
						_ = 0;
						_ = 0;
						_ = 0;
						*(Vector2?*)(nint)vector = value;
						if (context != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
							context.FromPos = (Vector2?)(object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-38]");
							_ = 0;
							if (command.Parameters != null)
							{
								object obj3 = obj2 + 40;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								ref Vector2 pos2 = ref *(Vector2*)(obj2 + 16);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
								if (TryResolvePointPositionOnly((Command.Parameter)0, context, out pos2))
								{
									goto IL_025b;
								}
								parameters2 = command.Parameters;
								obj4 = 1;
								goto IL_032e;
							}
						}
						return (string)(object)new NullReferenceException();
					}
				}
				goto IL_038b;
			}
			object obj5 = obj2 + 40;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			ref Vector2 pos3 = ref *(Vector2*)(obj2 + 16);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
			if (TryResolvePointPositionOnly((Command.Parameter)0, context, out pos3))
			{
				goto IL_025b;
			}
			parameters2 = command.Parameters;
			obj4 = 0;
			goto IL_032e;
		}
		return "???";
		IL_025b:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+10]");
		localPos = (Vector2)0;
		goto IL_038b;
		IL_032e:
		object obj6 = obj2 + 16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+10]");
		object obj7 = 0;
		object obj8 = obj2 + 40;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
		return (string)0;
		IL_038b:
		FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
		FireMission fireMission2 = FireMission._003CInstance_003Ek__BackingField;
		FireMission fireMission3 = FireMission._003CInstance_003Ek__BackingField;
		int unusedRowDecimals = default(int);
		return GridCodeConverter.LocalToCodeRegion(localPos, fireMission.cellWidth, fireMission2.cellHeight, fireMission3.yIncreasesUp, unusedRowDecimals);
	}

	private unsafe static string FormatRemaining(Command command, LineEvalContext context)
	{
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected Ref, but got Unknown
		//IL_01dc: Expected O, but got I
		Command command2 = default(Command);
		if (command2 != null)
		{
			if (command2.Parameters != null)
			{
				List<Command.Parameter> parameters = command2.Parameters;
				if (parameters._size != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Command command3 = default(Command);
					if (command3 != null && command3.Raw != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						int num = 0;
						List<string>.Enumerator enumerator = default(List<string>.Enumerator);
						string text = default(string);
						while (true)
						{
							if (enumerator.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								_003C_003Ec__DisplayClass14_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass14_0();
								string value;
								if (text != null)
								{
									string text2 = text.Trim();
									value = text2;
								}
								else
								{
									value = null;
								}
								if (string.IsNullOrWhiteSpace(value))
								{
									continue;
								}
								if (CS_0024_003C_003E8__locals4 != null)
								{
									ref EntityRoles result = ref *(EntityRoles*)(CS_0024_003C_003E8__locals4 + 16);
									if (!Enum.TryParse<EntityRoles>(value, ignoreCase: true, out result))
									{
										continue;
									}
									bool flag = Enum.TryParse<EntityRoles>(null, ignoreCase: true, out result);
									bool flag2 = !flag;
									string text3 = null;
									if (!flag2)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v29 (System.Boolean)+78]");
										if ((nint)0 == 0)
										{
											break;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v29 (System.Boolean)+78]");
										Dictionary<string, MapEntity>.ValueCollection values = ((Dictionary<string, MapEntity>)0).Values;
										Func<MapEntity, bool> predicate = delegate(MapEntity e)
										{
											//IL_006d: Expected I4, but got O
											//IL_003c: Expected O, but got I4
											//IL_0046: Unknown result type (might be due to invalid IL or missing references)
											//IL_004b: Expected O, but got Unknown
											if (e == null)
											{
												NullReferenceException ex = new NullReferenceException();
												return (byte)(int)ex != 0;
											}
											object obj = default(object);
											object obj2 = default(object);
											if (FlagExtensions.Has((MapEntityStates)(int)(&obj), (MapEntityStates)(int)(&obj2)))
											{
												return false;
											}
											object obj3 = e.Role & CS_0024_003C_003E8__locals4.role;
											object obj4 = obj3 - CS_0024_003C_003E8__locals4.role;
											return obj4 == null;
										};
										int num2 = Enumerable.Count(values, predicate);
										int num3 = num2 + num;
										num = num3;
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							enumerator.Dispose();
							return num.ToString();
						}
						throw new NullReferenceException();
					}
					goto IL_025e;
				}
			}
			return "0";
		}
		goto IL_025e;
		IL_025e:
		throw new NullReferenceException();
	}

	private unsafe static string FormatTimer(Command command, LineEvalContext context)
	{
		//IL_0008: Expected O, but got Ref
		//IL_006c: Expected O, but got Ref
		//IL_0505: Expected O, but got I
		//IL_016f: Expected F8, but got I4
		//IL_01dc: Expected O, but got I
		//IL_00f5: Expected O, but got Ref
		//IL_010d: Expected O, but got Ref
		//IL_0209: Expected O, but got I
		//IL_012c: Expected O, but got I
		//IL_02e2: Expected O, but got I
		//IL_02f9: Expected O, but got I4
		//IL_0251: Expected O, but got I
		//IL_0161: Expected F8, but got I
		//IL_0317: Expected O, but got I
		//IL_0481: Expected O, but got Ref
		//IL_03e2: Expected O, but got I
		//IL_035f: Expected O, but got I
		//IL_0376: Expected O, but got I4
		//IL_0284: Expected F8, but got I
		//IL_0296: Expected O, but got Ref
		//IL_0457: Expected O, but got Ref
		//IL_039a: Expected F8, but got I
		//IL_03ac: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		double value;
		if (command != null)
		{
			if (command.Parameters != null)
			{
				List<Command.Parameter> parameters = command.Parameters;
				if (parameters._size != 0)
				{
					object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+17]");
					object obj4 = 0;
					FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
					if ((object)FireMission._003CInstance_003Ek__BackingField == null || fireMission.RunningTimers == null)
					{
						goto IL_0166;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+17]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdi_v4+10]");
						if ((nint)0 != 0)
						{
							object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 31));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806910A0");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+27]");
							object obj7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+27]");
							if ((nint)0 == 0)
							{
								goto IL_0166;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v356 @ rax_v66+14]");
							value = 0.0;
							goto IL_0174;
						}
					}
					goto IL_04a6;
				}
			}
			return "??:??:??";
		}
		goto IL_04a6;
		IL_04a6:
		return (string)(object)new NullReferenceException();
		IL_0166:
		value = 0.0;
		goto IL_0174;
		IL_0174:
		TimeSpan timeSpan = TimeSpan.FromSeconds(value);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+17]");
		if ((nint)0 != 0)
		{
			Func<Command.Option, bool> func = _003C_003Ec._003C_003E9__15_0;
			if (_003C_003Ec._003C_003E9__15_0 == null)
			{
				func = (_003C_003Ec._003C_003E9__15_0 = delegate(Command.Option x)
				{
					//IL_0018: Expected I4, but got O
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					return x.Symbol == Command.OptionSynbols.Add;
				});
			}
			ref Command.Option item = ref System.Runtime.CompilerServices.Unsafe.As<object, Command.Option>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdi_v4+20]");
			if (EnumerableExtensions.TryFindValue((IEnumerable<Command.Option>)0, func, out item))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+7]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+7]");
				if ((nint)0 == 0)
				{
					goto IL_04a6;
				}
				ref int result = ref System.Runtime.CompilerServices.Unsafe.As<object, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rcx_v38+18]");
				if (int.TryParse((string)0, out result))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
					TimeSpan ts = TimeSpan.FromSeconds(0.0);
					TimeSpan timeSpan2 = (TimeSpan)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 1));
					TimeSpan timeSpan3 = ((TimeSpan*)timeSpan2)->Add(ts);
				}
			}
			Func<Command.Option, bool> func2 = _003C_003Ec._003C_003E9__15_1;
			if (_003C_003Ec._003C_003E9__15_1 == null)
			{
				func2 = (_003C_003Ec._003C_003E9__15_1 = delegate(Command.Option x)
				{
					//IL_0018: Expected I4, but got O
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					return x.Symbol == Command.OptionSynbols.Subtract;
				});
			}
			ref Command.Option item2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Command.Option>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 15));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdi_v4+20]");
			bool flag = EnumerableExtensions.TryFindValue((IEnumerable<Command.Option>)0, func2, out item2);
			bool flag2 = !flag;
			TimeSpan timeSpan4 = (TimeSpan)flag;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+F]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+F]");
				if ((nint)0 == 0)
				{
					goto IL_04a6;
				}
				ref int result2 = ref System.Runtime.CompilerServices.Unsafe.As<object, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 127));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rcx_v27+18]");
				bool flag3 = int.TryParse((string)0, out result2);
				bool flag4 = !flag3;
				timeSpan4 = (TimeSpan)flag3;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+7F]");
					TimeSpan ts2 = TimeSpan.FromSeconds(0.0);
					TimeSpan timeSpan5 = (TimeSpan)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 1));
					timeSpan4 = ((TimeSpan*)timeSpan5)->Subtract(ts2);
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdi_v4+18]");
			bool flag5 = (nint)0 == 0;
			if (!flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdi_v4+18]");
				object obj10 = -1;
				if (flag5)
				{
					TimeSpan timeSpan6 = (TimeSpan)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 1));
					return ((TimeSpan*)timeSpan6)->ToString("hh\\:mm");
				}
				if ((nint)obj10 == 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,xmm6\"");
					int num = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
					return ((int*)num)->ToString();
				}
			}
			TimeSpan timeSpan7 = (TimeSpan)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 1));
			return ((TimeSpan*)timeSpan7)->ToString("hh\\:mm\\:ss");
		}
		goto IL_04a6;
	}

	private unsafe static string FormatDirection(Command command, LineEvalContext context)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected Ref, but got Unknown
		//IL_007d: Expected O, but got I
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00d7: Expected native int or pointer, but got O
		//IL_010e: Expected O, but got I
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected Ref, but got Unknown
		//IL_0181: Expected O, but got I
		//IL_01d1: Expected I, but got O
		//IL_01e1: Expected O, but got I
		//IL_03b9: Expected O, but got I
		//IL_03d6: Expected O, but got I
		//IL_03f9: Invalid comparison between I4 and F4
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0220: Expected O, but got I
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Expected Ref, but got Unknown
		//IL_0243: Expected O, but got I
		_ = 0;
		_ = 0;
		_ = 0;
		List<Command.Parameter> parameters = command.Parameters;
		if (parameters._size >= 2)
		{
			object obj2 = default(object);
			object obj = obj2 - 40;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			ref Vector2 pos = ref *(Vector2*)(obj2 + 40);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-28]");
			if (TryResolvePosition((Command.Parameter)0, context, out pos))
			{
				Vector2 value = (Vector2)(obj2 - 40);
				Vector2? vector = (Vector2?)(object)(obj2 - 32);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
				_ = 0;
				_ = 0;
				_ = 0;
				*(Vector2?*)(nint)vector = value;
				if (context != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-20]");
					context.FromPos = (Vector2?)(object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-18]");
					_ = 0;
					if (command.Parameters != null)
					{
						object obj3 = obj2 - 40;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						ref Vector2 pos2 = ref *(Vector2*)(obj2 - 48);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-28]");
						if (TryResolvePosition((Command.Parameter)0, context, out pos2))
						{
							List<string> modifiers = command.Modifiers;
							bool flag = modifiers._size < 1;
							ref int reference = ref *(int*)context;
							nint num = unchecked((nint)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-28]");
							string text = (string)0;
							int num2 = 2;
							if (!flag)
							{
								object obj4 = obj2 - 40;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-28]");
								text = (string)0;
								reference = ref *(int*)(obj2 + 16);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-28]");
								bool flag2 = int.TryParse((string)0, out reference);
								bool flag3 = !flag2;
								pos2 = ref *(Vector2*)null;
								num = 0;
								num2 = 2;
								if (!flag3)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+10]");
									num2 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+10]");
									if ((nint)0 >= (nint)1)
									{
										bool flag4 = num2 <= 3;
										pos2 = ref *(Vector2*)null;
										num = 0;
										if (!flag4)
										{
											pos2 = ref *(Vector2*)null;
											num = 0;
											num2 = 3;
										}
									}
									else
									{
										pos2 = ref *(Vector2*)null;
										num = 0;
										num2 = 1;
									}
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-30]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
							object obj5 = num3 - 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-2C]");
							nint num4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+2C]");
							object obj6 = num4 - 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
							float num5 = (float)obj5 * 57.29578f;
							if (0f > num5)
							{
								num5 += 360f;
							}
							string text2 = BearingToCompass(num5, num2);
							string text3 = text2.ToUpperInvariant();
							string key = "STR_BEARING_" + text3;
							return LocalisationManager.Instance.Get(key);
						}
						goto IL_0371;
					}
				}
				return (string)(object)new NullReferenceException();
			}
		}
		goto IL_0371;
		IL_0371:
		return "???";
	}

	private static string BearingToCompass(float deg, int level)
	{
		//IL_0340: Expected O, but got I4
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		float deg2 = default(float);
		while (true)
		{
			object obj = 2 - 1;
			bool flag = 2 == 1;
			string[] names;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						continue;
					}
					return MapDirectionExpanded16(deg2);
				}
				string[] array = new string[8];
				if (array.Length <= 0)
				{
					break;
				}
				array[0] = "North";
				if (array.Length <= 1)
				{
					break;
				}
				array[1] = "Northeast";
				if (array.Length <= 2)
				{
					break;
				}
				array[2] = "East";
				if (array.Length <= 3)
				{
					break;
				}
				array[3] = "Southeast";
				if (array.Length <= 4)
				{
					break;
				}
				array[4] = "South";
				if (array.Length <= 5)
				{
					break;
				}
				array[5] = "Southwest";
				if (array.Length <= 6)
				{
					break;
				}
				array[6] = "West";
				if (array.Length <= 7)
				{
					break;
				}
				array[7] = "Northwest";
				names = array;
			}
			else
			{
				string[] array2 = new string[4];
				if (array2.Length <= 0)
				{
					break;
				}
				array2[0] = "North";
				if (array2.Length <= 1)
				{
					break;
				}
				array2[1] = "East";
				if (array2.Length <= 2)
				{
					break;
				}
				array2[2] = "South";
				if (array2.Length <= 3)
				{
					break;
				}
				array2[3] = "West";
				names = array2;
			}
			return MapDirection(deg2, names);
		}
		return (string)(object)new IndexOutOfRangeException();
	}

	private static string MapDirection(float deg, string[] names)
	{
		//IL_006d: Invalid comparison between F8 and I4
		float num = 360f / (float)names.Length;
		float num2 = num * 0.5f;
		float num3 = num2 + deg;
		float num4 = num3 / num;
		double num5 = Math.Floor(num4);
		double num6 = num5 % (double)names.Length;
		if (num6 < (double)names.Length)
		{
			return names[num6];
		}
		return (string)(object)new IndexOutOfRangeException();
	}

	private static string MapDirectionExpanded16(float deg)
	{
		//IL_03c2: Invalid comparison between F8 and I4
		string[] array = new string[16];
		if (array.Length > 0)
		{
			array[0] = "N";
			if (array.Length > 1)
			{
				array[1] = "NNE";
				if (array.Length > 2)
				{
					array[2] = "NE";
					if (array.Length > 3)
					{
						array[3] = "ENE";
						if (array.Length > 4)
						{
							array[4] = "E";
							if (array.Length > 5)
							{
								array[5] = "ESE";
								if (array.Length > 6)
								{
									array[6] = "SE";
									if (array.Length > 7)
									{
										array[7] = "SSE";
										if (array.Length > 8)
										{
											array[8] = "S";
											if (array.Length > 9)
											{
												array[9] = "SSW";
												if (array.Length > 10)
												{
													array[10] = "SW";
													if (array.Length > 11)
													{
														array[11] = "WSW";
														if (array.Length > 12)
														{
															array[12] = "W";
															if (array.Length > 13)
															{
																array[13] = "WNW";
																if (array.Length > 14)
																{
																	array[14] = "NW";
																	if (array.Length > 15)
																	{
																		array[15] = "NNW";
																		float num = 360f / (float)array.Length;
																		float num2 = num * 0.5f;
																		float num3 = num2 + deg;
																		float num4 = num3 / num;
																		double num5 = Math.Floor(num4);
																		double num6 = num5 % (double)array.Length;
																		if (num6 < (double)array.Length)
																		{
																			return Expand16(array[num6]);
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return (string)(object)new IndexOutOfRangeException();
	}

	private static string Expand16(string abbr)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0FD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1804FA610");
		object obj = default(object);
		string result;
		if ((nint)obj > 1696684493)
		{
			if ((long)obj > 3222007936L)
			{
				if ((long)obj > 3524005078L)
				{
					if ((long)obj == 3591115554L)
					{
						bool flag = abbr == "S";
						bool flag2 = !flag;
						result = abbr;
						if (!flag2)
						{
							return "South";
						}
					}
					else
					{
						bool flag3 = (long)obj != 3964714500L;
						result = abbr;
						if (!flag3)
						{
							bool flag4 = abbr == "ESE";
							bool flag5 = !flag4;
							result = abbr;
							if (!flag5)
							{
								return "ESE";
							}
						}
					}
				}
				else if ((long)obj == 3406561745L)
				{
					bool flag6 = abbr == "N";
					bool flag7 = !flag6;
					result = abbr;
					if (!flag7)
					{
						return "North";
					}
				}
				else
				{
					bool flag8 = (long)obj != 3524005078L;
					result = abbr;
					if (!flag8)
					{
						bool flag9 = abbr == "W";
						bool flag10 = !flag9;
						result = abbr;
						if (!flag10)
						{
							return "West";
						}
					}
				}
			}
			else if ((nint)obj > 1794121710)
			{
				if ((long)obj == 2957218717L)
				{
					bool flag11 = abbr == "ENE";
					bool flag12 = !flag11;
					result = abbr;
					if (!flag12)
					{
						return "ENE";
					}
				}
				else
				{
					bool flag13 = (long)obj != 3222007936L;
					result = abbr;
					if (!flag13)
					{
						bool flag14 = abbr == "E";
						bool flag15 = !flag14;
						result = abbr;
						if (!flag15)
						{
							return "East";
						}
					}
				}
			}
			else if ((nint)obj == 1778557231)
			{
				bool flag16 = abbr == "SW";
				bool flag17 = !flag16;
				result = abbr;
				if (!flag17)
				{
					return "Southwest";
				}
			}
			else
			{
				bool flag18 = (nint)obj != 1794121710;
				result = abbr;
				if (!flag18)
				{
					bool flag19 = abbr == "NNW";
					bool flag20 = !flag19;
					result = abbr;
					if (!flag20)
					{
						return "NNW";
					}
				}
			}
		}
		else if ((nint)obj > 1243187548)
		{
			if ((nint)obj > 1543670565)
			{
				if ((nint)obj == 1693580184)
				{
					bool flag21 = abbr == "WSW";
					bool flag22 = !flag21;
					result = abbr;
					if (!flag22)
					{
						return "WSW";
					}
				}
				else
				{
					bool flag23 = (nint)obj != 1696684493;
					result = abbr;
					if (!flag23)
					{
						bool flag24 = abbr == "WNW";
						bool flag25 = !flag24;
						result = abbr;
						if (!flag25)
						{
							return "WNW";
						}
					}
				}
			}
			else if ((nint)obj == 1492124568)
			{
				bool flag26 = abbr == "NNE";
				bool flag27 = !flag26;
				result = abbr;
				if (!flag27)
				{
					return "NNE";
				}
			}
			else
			{
				bool flag28 = (nint)obj != 1543670565;
				result = abbr;
				if (!flag28)
				{
					bool flag29 = abbr == "SE";
					bool flag30 = !flag29;
					result = abbr;
					if (!flag30)
					{
						return "Southeast";
					}
				}
			}
		}
		else if ((nint)obj > 937851900)
		{
			if ((nint)obj == 1008300882)
			{
				bool flag31 = abbr == "SSE";
				bool flag32 = !flag31;
				result = abbr;
				if (!flag32)
				{
					return "SSE";
				}
			}
			else
			{
				bool flag33 = (nint)obj != 1243187548;
				result = abbr;
				if (!flag33)
				{
					bool flag34 = abbr == "SSW";
					bool flag35 = !flag34;
					result = abbr;
					if (!flag35)
					{
						return "SSW";
					}
				}
			}
		}
		else if ((nint)obj == 702965234)
		{
			bool flag36 = abbr == "NW";
			bool flag37 = !flag36;
			result = abbr;
			if (!flag37)
			{
				return "Northwest";
			}
		}
		else
		{
			bool flag38 = (nint)obj != 937851900;
			result = abbr;
			if (!flag38)
			{
				bool flag39 = abbr == "NE";
				bool flag40 = !flag39;
				result = abbr;
				if (!flag40)
				{
					result = "Northeast";
				}
			}
		}
		return result;
	}

	private unsafe static bool TryResolveRelativePosition(string Id, Vector2? reference, out MapEntity entity)
	{
		//IL_0075: Expected O, but got I4
		//IL_0114: Expected O, but got I4
		//IL_015c: Expected O, but got I4
		//IL_017d: Expected I4, but got O
		ref MapEntity reference2 = ref *(MapEntity*)null;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj = default(object);
		if (obj != null)
		{
			Vector2 fromPosition;
			(EntityLocation, float, float) nearest;
			object obj2 = default(object);
			bool isAlive = default(bool);
			UnityEngine.Object obj3;
			NullReferenceException ex;
			switch (Id)
			{
			case "nearesttarget":
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
				fromPosition = (Vector2)32;
				goto IL_0190;
			case "nearestenemy":
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
				fromPosition = (Vector2)1;
				goto IL_0190;
			case "nearestally":
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
					fromPosition = (Vector2)2;
					goto IL_0190;
				}
				IL_0190:
				nearest = ImpactTracker.GetNearest((EntityRoles)(int)(&obj2), fromPosition, isAlive);
				obj3 = (UnityEngine.Object)nearest;
				if (!((UnityEngine.Object)nearest != null))
				{
					break;
				}
				if ((object)nearest != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rdi_v5 (UnityEngine.Object)+B0]");
					reference2 = ref *(MapEntity*)null;
					return true;
				}
				ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
	}

	private unsafe static bool TryResolveSpecialPosition(string Id, out Vector2 pos)
	{
		//IL_0219: Expected I, but got O
		//IL_01ee: Expected I4, but got O
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v3 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v4 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		ref Vector2 reference = ref *(Vector2*)Vector2.zeroVector;
		NullReferenceException ex;
		switch (Id)
		{
		case "turret":
		{
			TurretController instance = TurretController.Instance;
			if ((object)TurretController.Instance != null && (object)instance.turretBase != null)
			{
				Vector3 localPosition = instance.turretBase.localPosition;
				object obj2 = default(object);
				reference = ref *(Vector2*)obj2;
				return true;
			}
			goto IL_01e0;
		}
		case "turretmovementstartloc":
			if ((object)TurretController.Instance != null)
			{
				object obj3 = TurretController.Instance + 228;
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj4 = default(object);
				if (obj4 == null)
				{
					goto default;
				}
				if ((object)TurretController.Instance != null)
				{
					object obj = TurretController.Instance + 228;
					break;
				}
			}
			goto IL_01e0;
		case "turretmovementtargetloc":
			if ((object)TurretController.Instance != null)
			{
				Vector2? vector = (Vector2?)(object)(TurretController.Instance + 240);
				if (!((Vector2?*)vector)->HasValue)
				{
					goto default;
				}
				if ((object)TurretController.Instance != null)
				{
					object obj = TurretController.Instance + 240;
					break;
				}
			}
			goto IL_01e0;
		default:
			{
				return false;
			}
			IL_01e0:
			ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
		object obj5 = default(object);
		reference = ref *(Vector2*)obj5;
		return true;
	}

	private unsafe static bool TryResolveLocationContextKey(string Id, LineEvalContext context, out Vector2 pos)
	{
		//IL_01de: Expected I, but got O
		//IL_015f: Expected I4, but got O
		//IL_0132: Expected O, but got Ref
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v3 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		ref Vector2 reference = ref *(Vector2*)Vector2.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		bool flag;
		GridReference value;
		Vector3[] gridBounds;
		if (context != null && context.LocationContextKeys != null)
		{
			flag = context.LocationContextKeys.TryGetValue(Id, out value);
			if (!flag)
			{
				goto IL_004f;
			}
			if ((object)FireMission._003CInstance_003Ek__BackingField != null)
			{
				GameObject gameObject = GameObject.FindWithTag("MissionParent");
				if (!(gameObject != null))
				{
					gridBounds = System.EmptyArray<Vector3>.Value;
					goto IL_0195;
				}
				Vector3[] array = new Vector3[4];
				if ((object)gameObject != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					RectTransform rectTransform = default(RectTransform);
					if ((object)rectTransform != null)
					{
						rectTransform.GetWorldCorners(array);
						gridBounds = array;
						goto IL_0195;
					}
				}
			}
		}
		goto IL_0151;
		IL_0151:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0195:
		if (value != null)
		{
			Vector3 location = value.GetLocation(gridBounds);
			if ((object)FireMission._003CInstance_003Ek__BackingField != null)
			{
				float num3 = default(float);
				Vector2 vector = FireMission._003CInstance_003Ek__BackingField.ToLocalSpace((Vector3)(&num3));
				reference = ref *(Vector2*)vector;
				flag = true;
				goto IL_004f;
			}
		}
		goto IL_0151;
		IL_004f:
		return flag;
	}

	private unsafe static bool TryResolvePointPositionOnly(Command.Parameter parameter, LineEvalContext context, out Vector2 pos)
	{
		//IL_0185: Expected O, but got Ref
		//IL_0094: Expected O, but got Ref
		//IL_01b1: Expected O, but got Ref
		//IL_03b3: Expected I, but got O
		Command.Parameter parameter2 = default(Command.Parameter);
		if (parameter2 != null && parameter2.Ids != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			MapEntity value = null;
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			object obj = default(object);
			object arg = default(object);
			object obj3 = default(object);
			Vector2? vector = default(Vector2?);
			FireMission fireMission = default(FireMission);
			FireMission fireMission2 = default(FireMission);
			while (true)
			{
				ref Vector2 reference;
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					object obj2;
					if (parameter2.RandomFlagSeen)
					{
						bool flag = context == null;
						string text = (string)(&enumerator);
						if (flag)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string key = $"{obj}:{arg}";
						bool flag2 = context.SelectedIds == null;
						text = "{0}:{1}";
						if (flag2)
						{
							throw new NullReferenceException();
						}
						bool flag3 = context.SelectedIds.TryGetValue(key, out value);
						bool flag4 = !flag3;
						obj2 = obj;
						int index = parameter2.Index;
						if (!flag4)
						{
							if (0 == 0)
							{
								break;
							}
							reference = ref *(Vector2*)obj3;
							enumerator.Dispose();
							goto IL_047a;
						}
					}
					else
					{
						bool flag5 = context == null;
						string text = (string)(&enumerator);
						if (flag5)
						{
							throw new NullReferenceException();
						}
						obj2 = obj;
					}
					if (!TryResolveRelativePosition((string)obj2, (Vector2?)(object)(&vector), out value))
					{
						if (!TryResolveSpecialPosition((string)obj2, out pos))
						{
							bool flag6 = TryResolveLocationContextKey((string)obj2, context, out pos);
							if (!flag6)
							{
								if (parameter2.RandomFlagSeen == flag6)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
									bool flag7 = (object)fireMission == null;
									string text = null;
									if (flag7)
									{
										throw new NullReferenceException();
									}
									if (!fireMission.TryGetMapEntity((string)obj2, out value))
									{
										bool flag8 = TryResolveRoleToRandomEntity((string)obj2, parameter2.Index, context, out value);
										bool flag9 = !flag8;
										nint num = (nint)(&value);
										if (flag9)
										{
											continue;
										}
										if (0 == 0)
										{
											throw new NullReferenceException();
										}
										reference = ref *(Vector2*)obj3;
										enumerator.Dispose();
									}
									else
									{
										if (0 == 0)
										{
											throw new NullReferenceException();
										}
										reference = ref *(Vector2*)obj3;
										enumerator.Dispose();
									}
								}
								else if (!TryResolveRoleToRandomEntity((string)obj2, parameter2.Index, context, out value))
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
									bool flag10 = (object)fireMission2 == null;
									string text = null;
									if (flag10)
									{
										throw new NullReferenceException();
									}
									bool flag11 = fireMission2.TryGetMapEntity((string)obj2, out value);
									bool flag12 = !flag11;
									nint num = unchecked((nint)null);
									if (flag12)
									{
										continue;
									}
									if (0 == 0)
									{
										throw new NullReferenceException();
									}
									reference = ref *(Vector2*)obj3;
									enumerator.Dispose();
								}
								else
								{
									if (0 == 0)
									{
										throw new NullReferenceException();
									}
									reference = ref *(Vector2*)obj3;
									enumerator.Dispose();
								}
							}
							else
							{
								enumerator.Dispose();
							}
						}
						else
						{
							enumerator.Dispose();
						}
					}
					else
					{
						if (0 == 0)
						{
							throw new NullReferenceException();
						}
						reference = ref *(Vector2*)obj3;
						enumerator.Dispose();
					}
					goto IL_047a;
				}
				enumerator.Dispose();
				reference = ref *(Vector2*)null;
				return false;
				IL_047a:
				return true;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private unsafe static bool TryResolveParameterToEntity(Command.Parameter parameter, LineEvalContext context, out MapEntity entity)
	{
		//IL_0060: Expected O, but got Ref
		//IL_031c: Expected O, but got Ref
		//IL_0253: Expected I, but got O
		Command.Parameter parameter2 = default(Command.Parameter);
		if (parameter2 != null && parameter2.Ids != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
			List<string>.Enumerator enumerator = enumerator2;
			List<string>.Enumerator enumerator3 = default(List<string>.Enumerator);
			object obj = default(object);
			object arg = default(object);
			List<string>.Enumerator enumerator4 = default(List<string>.Enumerator);
			FireMission fireMission = default(FireMission);
			FireMission fireMission2 = default(FireMission);
			while (true)
			{
				if (enumerator3.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = context == null;
					Command.Parameter parameter3 = (Command.Parameter)(&enumerator3);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string key = $"{obj}:{arg}";
						bool flag2 = context.SelectedIds == null;
						parameter3 = (Command.Parameter)(object)"{0}:{1}";
						if (!flag2)
						{
							if (context.SelectedIds.TryGetValue(key, out entity))
							{
								enumerator3.Dispose();
							}
							else
							{
								bool flag3 = TryResolveRelativePosition((string)obj, (Vector2?)(object)(&enumerator4), out entity);
								if (!flag3)
								{
									if (parameter2.RandomFlagSeen == flag3)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
										bool flag4 = (object)fireMission == null;
										parameter3 = null;
										if (flag4)
										{
											throw new NullReferenceException();
										}
										if (!fireMission.TryGetMapEntity((string)obj, out entity))
										{
											bool flag5 = TryResolveRoleToRandomEntity((string)obj, parameter2.Index, context, out entity);
											bool flag6 = !flag5;
											nint num = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref entity);
											enumerator = (List<string>.Enumerator)context.FromPos;
											if (flag6)
											{
												continue;
											}
											enumerator3.Dispose();
										}
										else
										{
											enumerator3.Dispose();
										}
									}
									else if (!TryResolveRoleToRandomEntity((string)obj, parameter2.Index, context, out entity))
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
										bool flag7 = (object)fireMission2 == null;
										parameter3 = null;
										if (flag7)
										{
											break;
										}
										bool flag8 = fireMission2.TryGetMapEntity((string)obj, out entity);
										bool flag9 = !flag8;
										nint num = unchecked((nint)null);
										enumerator = (List<string>.Enumerator)context.FromPos;
										if (flag9)
										{
											continue;
										}
										enumerator3.Dispose();
									}
									else
									{
										enumerator3.Dispose();
									}
								}
								else
								{
									enumerator3.Dispose();
								}
							}
							return true;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator3.Dispose();
				ref MapEntity reference = ref *(MapEntity*)null;
				return false;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private unsafe static bool TryResolvePosition(Command.Parameter parameter, LineEvalContext context, out Vector2 pos)
	{
		//IL_00b2: Expected I4, but got O
		if (!TryResolvePointPositionOnly(parameter, context, out pos))
		{
			if (parameter != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AFA60");
				FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
				if ((object)FireMission._003CInstance_003Ek__BackingField != null)
				{
					FireMission fireMission2 = FireMission._003CInstance_003Ek__BackingField;
					if ((object)FireMission._003CInstance_003Ek__BackingField != null)
					{
						FireMission fireMission3 = FireMission._003CInstance_003Ek__BackingField;
						if ((object)FireMission._003CInstance_003Ek__BackingField != null)
						{
							string code = default(string);
							Vector2 vector = GridCodeConverter.CodeToLocal(code, fireMission.cellWidth, fireMission2.cellHeight, fireMission3.yIncreasesUp);
							ref Vector2 reference = ref *(Vector2*)vector;
							return true;
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
	}

	private unsafe static string GetDisplayName(Command command, LineEvalContext context)
	{
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Expected O, but got Unknown
		//IL_0346: Expected O, but got I
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Expected Ref, but got Unknown
		//IL_030c: Expected O, but got I
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Expected O, but got Unknown
		//IL_037e: Expected O, but got I
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected Ref, but got Unknown
		//IL_00e4: Expected O, but got I
		//IL_042a: Expected O, but got I
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Expected O, but got Unknown
		//IL_03bd: Expected O, but got I
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Expected O, but got Unknown
		//IL_03e7: Expected O, but got I
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_014d: Expected native int or pointer, but got O
		//IL_016c: Expected O, but got I
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected Ref, but got Unknown
		//IL_01c7: Expected O, but got I
		//IL_04e1: Expected O, but got I4
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Expected O, but got Unknown
		//IL_0221: Expected O, but got I
		//IL_02a7: Expected O, but got I4
		_ = 0;
		object obj2 = default(object);
		MapEntity mapEntity;
		MapEntity mapEntity2;
		string arg;
		if (command.Parameters != null)
		{
			List<Command.Parameter> parameters = command.Parameters;
			if (parameters._size > 0)
			{
				if (parameters._size != 1)
				{
					if (parameters._size == 2)
					{
						object obj = obj2 + 56;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						ref Vector2 pos = ref *(Vector2*)(obj2 + 32);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+38]");
						if (TryResolvePosition((Command.Parameter)0, context, out pos))
						{
							_003C_003Ec__DisplayClass27_1 CS_0024_003C_003E8__locals10 = new _003C_003Ec__DisplayClass27_1();
							Vector2 value = (Vector2)(obj2 + 32);
							Vector2? vector = (Vector2?)(object)(obj2 - 16);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+20]");
							_ = 0;
							_ = 0;
							_ = 0;
							*(Vector2?*)(nint)vector = value;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-10]");
							context.FromPos = (Vector2?)(object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-8]");
							_ = 0;
							object obj3 = obj2 + 32;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							ref MapEntity entity = ref *(MapEntity*)(CS_0024_003C_003E8__locals10 + 16);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+20]");
							if (TryResolveParameterToEntity((Command.Parameter)0, context, out entity))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
								Func<KeyValuePair<string, MapEntity>, bool> predicate = delegate
								{
									//IL_0089: Expected I4, but got O
									//IL_0072: Expected O, but got I
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
									object obj13 = default(object);
									if (obj13 != null)
									{
										MapEntity to = CS_0024_003C_003E8__locals10.to;
										if (CS_0024_003C_003E8__locals10.to != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
												return ((string)0).Equals(to.RawID, StringComparison.OrdinalIgnoreCase);
											}
										}
									}
									NullReferenceException ex = new NullReferenceException();
									return (byte)(int)ex != 0;
								};
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rax_v53+78]");
								int num = Enumerable.Count((IEnumerable<KeyValuePair<string, MapEntity>>)0, predicate);
								mapEntity = CS_0024_003C_003E8__locals10.to;
								if (num != 1 && mapEntity.IDIndex > 0)
								{
									string text = mapEntity.Name.Get();
									mapEntity2 = CS_0024_003C_003E8__locals10.to;
									nint num2 = 0;
									object obj4 = 0;
									arg = text;
									goto IL_04f7;
								}
								goto IL_0539;
							}
						}
					}
				}
				else
				{
					_003C_003Ec__DisplayClass27_0 CS_0024_003C_003E8__locals12 = new _003C_003Ec__DisplayClass27_0();
					object obj5 = obj2 + 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					ref MapEntity entity2 = ref *(MapEntity*)(CS_0024_003C_003E8__locals12 + 16);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+20]");
					if (TryResolveParameterToEntity((Command.Parameter)0, context, out entity2))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
						Func<KeyValuePair<string, MapEntity>, bool> predicate2 = delegate
						{
							//IL_0089: Expected I4, but got O
							//IL_0072: Expected O, but got I
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
							object obj13 = default(object);
							if (obj13 != null)
							{
								MapEntity entity3 = CS_0024_003C_003E8__locals12.entity;
								if (CS_0024_003C_003E8__locals12.entity != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_20_v1+18]");
										return ((string)0).Equals(entity3.RawID, StringComparison.OrdinalIgnoreCase);
									}
								}
							}
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						};
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v36+78]");
						int num3 = Enumerable.Count((IEnumerable<KeyValuePair<string, MapEntity>>)0, predicate2);
						mapEntity = CS_0024_003C_003E8__locals12.entity;
						if (num3 != 1 && mapEntity.IDIndex > 0)
						{
							if (mapEntity.Name != null)
							{
								string text2 = mapEntity.Name.Get();
								mapEntity2 = CS_0024_003C_003E8__locals12.entity;
								bool flag = CS_0024_003C_003E8__locals12.entity == null;
								nint num2 = 0;
								object obj4 = 0;
								arg = text2;
								if (!flag)
								{
									goto IL_04f7;
								}
							}
							return (string)(object)new NullReferenceException();
						}
						goto IL_0539;
					}
				}
				object obj6 = obj2 + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+20]");
				object obj7 = 0;
				object obj8 = obj2 + 56;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AFA60");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+38]");
				if (!string.Equals((string)0, "target", StringComparison.OrdinalIgnoreCase))
				{
					object obj9 = obj2 + 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+20]");
					object obj10 = 0;
					object obj11 = obj2 + 56;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AFA60");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+38]");
					return (string)0;
				}
				return "the target";
			}
		}
		string message = "GetDisplayName Parameter Null (Point). Line: '" + context.Raw + "'";
		Debug.LogError(message);
		return null;
		IL_0539:
		return mapEntity.Name.Get();
		IL_04f7:
		object obj12 = obj2 + 32;
		_ = mapEntity2.IDIndex;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg2 = default(object);
		return $"{arg}#{arg2}";
	}

	private unsafe static bool TryResolveRoleToRandomEntity(string roleString, int index, LineEvalContext context, out MapEntity entity)
	{
		//IL_04de: Expected I4, but got O
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected Ref, but got Unknown
		//IL_0196: Expected O, but got I
		//IL_0386: Expected O, but got I8
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Expected I4, but got Unknown
		//IL_03b7: Expected O, but got I4
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Expected O, but got Unknown
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Expected I4, but got Unknown
		_003C_003Ec__DisplayClass28_0 CS_0024_003C_003E8__locals22 = new _003C_003Ec__DisplayClass28_0();
		CS_0024_003C_003E8__locals22.context = context;
		CS_0024_003C_003E8__locals22.roleString = roleString;
		ref MapEntity reference = ref *(MapEntity*)null;
		if (!string.IsNullOrWhiteSpace(CS_0024_003C_003E8__locals22.roleString))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text = $"{CS_0024_003C_003E8__locals22.roleString}:{arg}";
			LineEvalContext context2 = CS_0024_003C_003E8__locals22.context;
			if (CS_0024_003C_003E8__locals22.context != null && context2.SelectedIds != null)
			{
				if (!context2.SelectedIds.TryGetValue(text, out entity))
				{
					List<MapEntity> list = new List<MapEntity>();
					object obj = default(object);
					if (list._size <= 0)
					{
						ref EntityRoles result = ref *(EntityRoles*)(CS_0024_003C_003E8__locals22 + 16);
						if (Enum.TryParse<EntityRoles>(CS_0024_003C_003E8__locals22.roleString, ignoreCase: true, out result))
						{
							bool flag = Enum.TryParse<EntityRoles>(null, ignoreCase: true, out result);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v77 (System.Boolean)+78]");
							Dictionary<string, MapEntity>.ValueCollection values = ((Dictionary<string, MapEntity>)0).Values;
							Func<MapEntity, bool> predicate = delegate(MapEntity e)
							{
								//IL_0062: Expected I4, but got O
								//IL_0031: Expected O, but got I4
								//IL_003b: Unknown result type (might be due to invalid IL or missing references)
								//IL_0040: Expected O, but got Unknown
								if (e == null)
								{
									NullReferenceException ex2 = new NullReferenceException();
									return (byte)(int)ex2 != 0;
								}
								object obj4 = e.Role & CS_0024_003C_003E8__locals22.role;
								object obj5 = obj4 - CS_0024_003C_003E8__locals22.role;
								return obj5 == null;
							};
							IEnumerable<MapEntity> collection = Enumerable.Where(values, predicate);
							list.AddRange(collection);
							LineEvalContext context3 = CS_0024_003C_003E8__locals22.context;
							context3.implicatedRoles.Add((EntityRoles)(int)(&obj));
						}
						if (list._size == 0)
						{
							string message = "No Candidates For: " + CS_0024_003C_003E8__locals22.roleString;
							Debug.LogError(message);
							goto IL_04c2;
						}
					}
					FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
					bool flag2 = !fireMission.selectOnlyActivePoints;
					List<MapEntity> list2 = list;
					if (!flag2)
					{
						Func<MapEntity, bool> predicate2 = _003C_003Ec._003C_003E9__28_1;
						if (_003C_003Ec._003C_003E9__28_1 == null)
						{
							predicate2 = (_003C_003Ec._003C_003E9__28_1 = delegate(MapEntity x)
							{
								//IL_0030: Expected I4, but got O
								if (x == null)
								{
									NullReferenceException ex2 = new NullReferenceException();
									return (byte)(int)ex2 != 0;
								}
								object obj4 = default(object);
								object obj5 = default(object);
								bool flag5 = FlagExtensions.Has((MapEntityStates)(int)(&obj4), (MapEntityStates)(int)(&obj5));
								return (byte)((flag5 ? 1u : 0u) ^ 1u) != 0;
							});
						}
						IEnumerable<MapEntity> source = Enumerable.Where(list, predicate2);
						List<MapEntity> list3 = Enumerable.ToList(source);
						bool flag3 = list3._size > 0;
						list2 = list3;
						if (!flag3)
						{
							FireMission fireMission2 = FireMission._003CInstance_003Ek__BackingField;
							if (fireMission2.useAlternateTextWhenNoActive)
							{
								LineEvalContext context4 = CS_0024_003C_003E8__locals22.context;
								context4.noActiveMatchFound = true;
							}
							goto IL_04c2;
						}
					}
					Func<MapEntity, bool> predicate3 = delegate(MapEntity candidate)
					{
						//IL_009a: Expected I4, but got O
						_003C_003Ec__DisplayClass28_1 CS_0024_003C_003E8__locals25 = new _003C_003Ec__DisplayClass28_1();
						if (CS_0024_003C_003E8__locals25 != null)
						{
							CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals1 = CS_0024_003C_003E8__locals22;
							CS_0024_003C_003E8__locals25.candidate = candidate;
							LineEvalContext context7 = CS_0024_003C_003E8__locals22.context;
							if (CS_0024_003C_003E8__locals22.context != null)
							{
								Func<KeyValuePair<string, MapEntity>, bool> predicate4 = delegate
								{
									//IL_0098: Expected I4, but got O
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
									_003C_003Ec__DisplayClass28_0 obj4 = CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals1;
									if (CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals1 != null)
									{
										string value = obj4.roleString + ":";
										string text2 = default(string);
										if (text2 != null)
										{
											bool flag6 = text2.StartsWith(value);
											if (!flag6)
											{
												return flag6;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
											object obj5 = (object)text2 - (object)CS_0024_003C_003E8__locals25.candidate;
											return obj5 == null;
										}
									}
									NullReferenceException ex3 = new NullReferenceException();
									return (byte)(int)ex3 != 0;
								};
								bool flag5 = Enumerable.Any(context7.SelectedIds, predicate4);
								return (byte)((flag5 ? 1u : 0u) ^ 1u) != 0;
							}
						}
						NullReferenceException ex2 = new NullReferenceException();
						return (byte)(int)ex2 != 0;
					};
					IEnumerable<MapEntity> source2 = Enumerable.Where(list2, predicate3);
					List<MapEntity> list4 = Enumerable.ToList(source2);
					bool flag4 = list4._size == 0;
					List<MapEntity> list5 = list2;
					if (!flag4)
					{
						list5 = list4;
					}
					int num = 0;
					int num2 = 0;
					object obj2 = 2166136261L;
					while (num2 < text._stringLength)
					{
						char c = text.get_Chars(num);
						int num3 = c ^ obj2;
						obj2 = num3 * 16777619;
						num++;
						num2 = num;
					}
					FireMission fireMission3 = FireMission._003CInstance_003Ek__BackingField;
					LineEvalContext context5 = CS_0024_003C_003E8__locals22.context;
					int seed = default(int);
					System.Random random = new System.Random(seed);
					object obj3 = context5.LineIndex + obj2;
					seed = obj3 + fireMission3.seed;
					int num4 = random.Next(list5._size);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					reference = ref *(MapEntity*)obj;
					LineEvalContext context6 = CS_0024_003C_003E8__locals22.context;
					context6.SelectedIds.set_Item(text, entity);
				}
				return true;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_04c2;
		IL_04c2:
		return false;
	}

	private static int StableHash(string s)
	{
		//IL_0025: Expected I4, but got I8
		//IL_00ba: Expected I4, but got O
		bool flag = s == null;
		int num = 0;
		int num2 = -2128831035;
		int num3 = 0;
		if (!flag)
		{
			while (num < s._stringLength)
			{
				char c = s.get_Chars(num3);
				int num4 = c ^ num2;
				num2 = num4 * 16777619;
				num3++;
				num = num3;
			}
			return num2;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	static FireMissionTokenProcessor()
	{
		Regex regex = new Regex("\\[(.*?)\\]", (RegexOptions)521);
		tokenRegex = regex;
		Regex regex2 = new Regex("<([^>]+)>", (RegexOptions)521);
		parametereRegex = regex2;
		Regex regex3 = new Regex("(?<symbol>[^a-zA-Z0-9#]+)?(?<value>[a-zA-Z0-9#]+)", (RegexOptions)521);
		parameterePartRegex = regex3;
	}
}
