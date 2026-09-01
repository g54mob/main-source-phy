using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

[Serializable]
public class Requirement
{
	public enum RequirementTypes
	{
		None,
		ShellSlotCount,
		TurretIsMoving
	}

	public enum OperationTypes
	{
		Equals,
		NotEquals,
		LessThan,
		GreaterThan,
		LessThanOrEquals,
		GreaterThanOrEquals
	}

	public enum ShellSlots
	{
		Right,
		Left,
		Any,
		PunchardVaribale
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<CylinderShellSelector, bool> _003C_003E9__9_1;

		public static Func<CylinderShellSelector, bool> _003C_003E9__9_2;

		public static Func<CylinderShellSelector, int> _003C_003E9__9_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CExecute_003Eb__9_1(CylinderShellSelector x)
		{
			//IL_0052: Expected I4, but got O
			//IL_0030: Expected O, but got I4
			if ((object)x != null)
			{
				object obj = x.ShellSlotSide - 1;
				return obj == null;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CExecute_003Eb__9_2(CylinderShellSelector x)
		{
			//IL_0044: Expected I4, but got O
			if ((object)x != null)
			{
				return x.ShellSlotSide == ShellSlotPool.ShellSlotSides.Left;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal int _003CExecute_003Eb__9_0(CylinderShellSelector selector)
		{
			//IL_003d: Expected I4, but got O
			if ((object)selector != null)
			{
				return selector.EmptySlotCount();
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public ShellSlotPool.ShellSlotSides slot;

		internal bool _003CExecute_003Eb__3(CylinderShellSelector x)
		{
			//IL_0053: Expected I4, but got O
			//IL_0031: Expected O, but got I4
			if ((object)x != null)
			{
				object obj = x.ShellSlotSide - slot;
				return obj == null;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public RequirementTypes RequirementType;

	public OperationTypes Operation;

	public ShellSlots ShellSlot;

	public string StringValue;

	public int IntValue;

	public bool BoolValue;

	public unsafe bool Execute(Dictionary<string, object> variables)
	{
		//IL_0359: Expected I, but got O
		//IL_051f: Expected I, but got O
		//IL_0063: Expected O, but got I4
		//IL_08b3: Expected O, but got Ref
		//IL_04bb: Expected I, but got O
		//IL_031b: Expected I, but got O
		//IL_0546: Expected I, but got O
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_04e5: Expected O, but got I4
		//IL_02c1: Expected I, but got O
		//IL_0570: Expected O, but got I4
		//IL_00a9: Expected I, but got O
		//IL_03ce: Expected I, but got O
		//IL_08fa: Expected O, but got Ref
		//IL_040b: Expected O, but got I8
		//IL_0425: Expected O, but got I8
		//IL_00df: Expected I, but got O
		//IL_03f9: Expected I, but got O
		//IL_07ba: Expected I, but got O
		//IL_0658: Expected O, but got I
		//IL_065c: Expected O, but got I4
		//IL_02a3: Expected I, but got O
		//IL_0152: Expected I, but got O
		//IL_01b1: Expected I, but got O
		//IL_05ef: Expected I, but got O
		//IL_020d: Expected I4, but got O
		//IL_0230: Expected I, but got O
		if (RequirementType != RequirementTypes.ShellSlotCount)
		{
			goto IL_042f;
		}
		ShellSlotPool shellSlotPool = UnityEngine.Object.FindFirstObjectByType<ShellSlotPool>();
		if (!(shellSlotPool != null))
		{
			goto IL_058f;
		}
		bool flag = ShellSlot == ShellSlots.Right;
		UnityEngine.Object obj3;
		nint num;
		string key;
		nint num2;
		string key2;
		List<CylinderShellSelector> selectors2;
		Func<CylinderShellSelector, bool> func5;
		Func<CylinderShellSelector, bool> func2;
		List<CylinderShellSelector> selectors;
		nint num4;
		if (!flag)
		{
			object obj = ShellSlot - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj2 != 1;
					obj3 = null;
					num = unchecked((nint)null);
					if (!flag2)
					{
						_003C_003Ec__DisplayClass9_0 CS_0024_003C_003E8__locals3 = new _003C_003Ec__DisplayClass9_0();
						bool flag3 = variables == null;
						key = null;
						num2 = unchecked((nint)null);
						if (flag3)
						{
							goto IL_0732;
						}
						key = StringValue;
						if (variables.TryGetValue(StringValue, out var value) && value != null)
						{
							nint num3 = (nint)typeof(ShellSlotPool.ShellSlotSides);
							bool flag4 = (object)value.GetType() != typeof(ShellSlotPool.ShellSlotSides);
							Dictionary<string, object> dictionary = null;
							if (!flag4)
							{
								dictionary = (Dictionary<string, object>)value;
							}
							if (dictionary != null)
							{
								bool flag5 = CS_0024_003C_003E8__locals3 == null;
								num4 = 0;
								num2 = (nint)typeof(ShellSlotPool.ShellSlotSides);
								if (!flag5)
								{
									key = (string)value;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v554 @ rdx_v1 (System.String)+40]");
									nint num5 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ r8_v30 (Il2CppClass<ShellSlotPool+ShellSlotSides>)+40]");
									if (num5 != 0)
									{
										bool flag6 = ((Dictionary<string, object>)value).TryGetValue((string)(object)typeof(ShellSlotPool.ShellSlotSides), out *(object*)typeof(ShellSlotPool.ShellSlotSides));
										num4 = 0;
										key2 = (string)(object)typeof(ShellSlotPool.ShellSlotSides);
										num = (nint)typeof(ShellSlotPool.ShellSlotSides);
										goto IL_08bc;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
									object obj4 = default(object);
									CS_0024_003C_003E8__locals3.slot = (ShellSlotPool.ShellSlotSides)obj4;
									bool flag7 = (object)shellSlotPool == null;
									num4 = 0;
									num2 = (nint)typeof(ShellSlotPool.ShellSlotSides);
									if (!flag7)
									{
										Func<CylinderShellSelector, bool> func = delegate(CylinderShellSelector x)
										{
											//IL_0053: Expected I4, but got O
											//IL_0031: Expected O, but got I4
											if ((object)x == null)
											{
												NullReferenceException ex5 = new NullReferenceException();
												return (byte)(int)ex5 != 0;
											}
											object obj12 = x.ShellSlotSide - CS_0024_003C_003E8__locals3.slot;
											return obj12 == null;
										};
										func2 = func;
										selectors = shellSlotPool.selectors;
										goto IL_075e;
									}
								}
								goto IL_0732;
							}
						}
						string message = StringValue + " Slot Variable Not Found/Set";
						Debug.LogError(message);
						num4 = 0;
						obj3 = null;
						num = unchecked((nint)null);
					}
				}
				else
				{
					bool flag8 = (object)shellSlotPool == null;
					key = null;
					num2 = unchecked((nint)null);
					if (flag8)
					{
						goto IL_0732;
					}
					Func<CylinderShellSelector, int> func3 = _003C_003Ec._003C_003E9__9_0;
					if (_003C_003Ec._003C_003E9__9_0 == null)
					{
						Func<CylinderShellSelector, int> func4 = (_003C_003Ec._003C_003E9__9_0 = delegate(CylinderShellSelector selector)
						{
							//IL_003d: Expected I4, but got O
							if ((object)selector == null)
							{
								NullReferenceException ex5 = new NullReferenceException();
								return (int)ex5;
							}
							return selector.EmptySlotCount();
						});
						num4 = unchecked((nint)null);
						func3 = func4;
					}
					int num6 = Enumerable.Sum(shellSlotPool.selectors, func3);
					obj3 = (UnityEngine.Object)(object)func3;
					num = 0;
				}
				goto IL_0701;
			}
			bool flag9 = (object)shellSlotPool == null;
			key = null;
			num2 = unchecked((nint)null);
			if (!flag9)
			{
				selectors2 = shellSlotPool.selectors;
				func5 = _003C_003Ec._003C_003E9__9_2;
				if (_003C_003Ec._003C_003E9__9_2 == null)
				{
					func5 = (_003C_003Ec._003C_003E9__9_2 = delegate(CylinderShellSelector x)
					{
						//IL_0044: Expected I4, but got O
						if ((object)x == null)
						{
							NullReferenceException ex5 = new NullReferenceException();
							return (byte)(int)ex5 != 0;
						}
						return x.ShellSlotSide == ShellSlotPool.ShellSlotSides.Left;
					});
				}
				goto IL_037e;
			}
		}
		else
		{
			bool flag10 = (object)shellSlotPool == null;
			key = null;
			num2 = unchecked((nint)null);
			if (!flag10)
			{
				selectors2 = shellSlotPool.selectors;
				func5 = _003C_003Ec._003C_003E9__9_1;
				if (_003C_003Ec._003C_003E9__9_1 == null)
				{
					func5 = (_003C_003Ec._003C_003E9__9_1 = delegate(CylinderShellSelector x)
					{
						//IL_0052: Expected I4, but got O
						//IL_0030: Expected O, but got I4
						if ((object)x == null)
						{
							NullReferenceException ex5 = new NullReferenceException();
							return (byte)(int)ex5 != 0;
						}
						object obj12 = x.ShellSlotSide - 1;
						return obj12 == null;
					});
				}
				goto IL_037e;
			}
		}
		goto IL_0732;
		IL_08bc:
		bool flag11 = ((Dictionary<string, object>)(object)typeof(OperationTypes)).TryGetValue(key2, out *(object*)num);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		OperationTypes operationTypes = default(OperationTypes);
		bool flag12 = ((Dictionary<string, object>)(object)typeof(RequirementTypes)).TryGetValue((string)(&operationTypes), out *(object*)num);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object obj5 = default(object);
		object obj6 = default(object);
		string text = $"Operation Type: {obj5} Not supported for Condition Type: {obj6}";
		bool flag13 = ((Dictionary<string, object>)(object)typeof(Exception)).TryGetValue((string)obj5, out *(object*)obj6);
		Exception ex = new Exception(text);
		string text2 = (string)((Dictionary<string, object>)0).TryGetValue(text, out *(object*)null);
		throw ex;
		IL_037e:
		func2 = func5;
		selectors = selectors2;
		goto IL_075e;
		IL_0876:
		bool flag14 = ((Dictionary<string, object>)(object)typeof(OperationTypes)).TryGetValue(key, out *(object*)num2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		NullReferenceException ex2 = default(NullReferenceException);
		bool flag15 = ((Dictionary<string, object>)(object)typeof(RequirementTypes)).TryGetValue((string)(&ex2), out *(object*)num2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		object arg2 = default(object);
		string message2 = $"Operation Type: {arg} Not supported for Condition Type: {arg2}";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex3 = new Exception(message2);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex3;
		IL_058f:
		return false;
		IL_042f:
		if (RequirementType == RequirementTypes.TurretIsMoving)
		{
			TurretController turretController = UnityEngine.Object.FindFirstObjectByType<TurretController>();
			if (!(turretController != null))
			{
				goto IL_058f;
			}
			if (Operation == OperationTypes.Equals)
			{
				bool flag16 = (object)turretController == null;
				key = null;
				num2 = unchecked((nint)null);
				if (!flag16)
				{
					bool isMoving = turretController.IsMoving;
					object obj7 = (isMoving ? 1 : 0) - (BoolValue ? 1 : 0);
					return obj7 == null;
				}
			}
			else
			{
				bool flag17 = Operation != OperationTypes.NotEquals;
				key = null;
				num2 = unchecked((nint)null);
				if (flag17)
				{
					goto IL_0876;
				}
				bool flag18 = (object)turretController == null;
				key = null;
				num2 = unchecked((nint)null);
				if (!flag18)
				{
					bool isMoving2 = turretController.IsMoving;
					object obj8 = (isMoving2 ? 1 : 0) - (BoolValue ? 1 : 0);
					bool flag19 = obj8 == null;
					return !flag19;
				}
			}
			goto IL_0732;
		}
		return true;
		IL_075e:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
		UnityEngine.Object obj9 = default(UnityEngine.Object);
		if (!(obj9 != null))
		{
			goto IL_058f;
		}
		bool flag20 = (object)obj9 == null;
		num4 = 0;
		key = null;
		num2 = unchecked((nint)null);
		if (!flag20)
		{
			int num7 = ((CylinderShellSelector)obj9).EmptySlotCount();
			num4 = 0;
			obj3 = null;
			num = unchecked((nint)null);
			goto IL_0701;
		}
		goto IL_0732;
		IL_0732:
		NullReferenceException ex4 = new NullReferenceException();
		goto IL_0876;
		IL_0701:
		OperationTypes operation = Operation;
		bool flag21 = Operation > OperationTypes.GreaterThanOrEquals;
		key2 = (string)(object)obj3;
		if (!flag21)
		{
			object obj10 = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rdx_v26+45B918+v229 @ rax_v55 (Requirement+OperationTypes)*4]");
			object obj11 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v232 @ rcx_v47 (should have been resolved before IL gen)");
			goto IL_042f;
		}
		goto IL_08bc;
	}
}
