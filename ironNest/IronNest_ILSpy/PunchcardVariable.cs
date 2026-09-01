using System;
using Cpp2ILInjected;
using UnityEngine;

public class PunchcardVariable : MonoBehaviour
{
	public enum VariableTypes
	{
		Int,
		Float,
		Text,
		Coordinate,
		Bool,
		ShellSlot
	}

	public string VariableID;

	public VariableTypes VariableType;

	public int VariableInt;

	public float VariableFloat;

	public string VariableText;

	public GridReference VariableCoordinate;

	public bool VariableBool;

	public ShellSlotPool.ShellSlotSides VariableShellSlot;

	public object Get()
	{
		//IL_0016: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 33 Invalid \"Jump target not found in method: 0x180455A26\"");
		return VariableType;
	}

	public void SetInt(int value)
	{
		VariableInt = value;
	}

	public void SetFloat(float value)
	{
		VariableFloat = value;
	}

	public void SetText(string value)
	{
		VariableText = value;
	}

	public void SetText(bool value)
	{
		VariableBool = value;
	}

	public void SetCoordinate(GridReference value)
	{
		VariableCoordinate = value;
	}

	public void SetCoordinate_GridLocation(GridLocations location)
	{
		if (VariableCoordinate == null)
		{
			GridReference variableCoordinate = new GridReference();
			VariableCoordinate = variableCoordinate;
		}
		GridReference variableCoordinate2 = VariableCoordinate;
		variableCoordinate2.Location = location;
	}

	public void SetCoordinate_GridLocation(string location)
	{
		if (Enum.TryParse<GridLocations>(location, ignoreCase: true, out var result))
		{
			if (VariableCoordinate == null)
			{
				GridReference variableCoordinate = new GridReference();
				VariableCoordinate = variableCoordinate;
			}
			GridReference variableCoordinate2 = VariableCoordinate;
			variableCoordinate2.Location = result;
		}
	}

	public void SetCoordinate_GridLocation_L(string l)
	{
		if (!string.IsNullOrEmpty(l))
		{
			if (VariableCoordinate == null)
			{
				GridReference variableCoordinate = new GridReference();
				VariableCoordinate = variableCoordinate;
			}
			string text = l.ToUpper();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AFA60");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebp\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string value = $"{arg}{arg2}";
			if (Enum.TryParse<GridLocations>(value, out var result))
			{
				GridReference variableCoordinate2 = VariableCoordinate;
				variableCoordinate2.Location = result;
			}
		}
	}

	public void SetCoordinate_GridLocation_L_FromIndex(float l)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected I4, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si esi,xmm0\"");
		GridReference gridReference = default(GridReference);
		if (VariableCoordinate == null)
		{
			gridReference = (VariableCoordinate = new GridReference());
		}
		GridReference variableCoordinate = VariableCoordinate;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
		object obj = (object)gridReference >> 2;
		object obj2 = obj >> 31;
		object obj3 = obj + obj2;
		object obj5 = default(object);
		object obj4 = obj5 - obj3;
		object obj6 = obj4 * 4;
		object obj7 = obj4 + obj6;
		object obj8 = obj7 * 2;
		GridLocations location = (GridLocations)(variableCoordinate.Location + obj8);
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(GridLocations));
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value = default(object);
		if (Enum.IsDefined(typeFromHandle, value))
		{
			GridReference variableCoordinate2 = VariableCoordinate;
			variableCoordinate2.Location = location;
		}
	}

	public void SetCoordinate_GridLocation_L_FromIndex(int l)
	{
		//IL_006d: Expected O, but got I4
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected I4, but got Unknown
		//IL_0026: Expected I4, but got O
		bool flag = VariableCoordinate != null;
		int num = l;
		if (!flag)
		{
			num = (int)(VariableCoordinate = new GridReference());
		}
		GridReference variableCoordinate = VariableCoordinate;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
		int num2 = num >> 2;
		int num3 = num2 >> 31;
		object obj = num2 + num3;
		object obj2 = l - obj;
		object obj3 = obj2 * 4;
		object obj4 = obj2 + obj3;
		object obj5 = obj4 * 2;
		GridLocations location = (GridLocations)(variableCoordinate.Location + obj5);
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(GridLocations));
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value = default(object);
		if (Enum.IsDefined(typeFromHandle, value))
		{
			GridReference variableCoordinate2 = VariableCoordinate;
			variableCoordinate2.Location = location;
		}
	}

	public void SetCoordinate_GridLocation_N(float n)
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si esi,xmm0\"");
		object obj2 = default(object);
		object obj = obj2 - 1;
		if ((nint)obj <= 9)
		{
			if (VariableCoordinate == null)
			{
				GridReference variableCoordinate = new GridReference();
				VariableCoordinate = variableCoordinate;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [rcx+10h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string value = $"{arg}{arg2}";
			if (Enum.TryParse<GridLocations>(value, out var result))
			{
				GridReference variableCoordinate2 = VariableCoordinate;
				variableCoordinate2.Location = result;
			}
		}
	}

	public void SetCoordinate_GridLocation_N(int n)
	{
		//IL_00e0: Expected O, but got I4
		int num = default(int);
		object obj = num - 1;
		if ((nint)obj <= 9)
		{
			if (VariableCoordinate == null)
			{
				GridReference variableCoordinate = new GridReference();
				VariableCoordinate = variableCoordinate;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [rcx+10h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string value = $"{arg}{arg2}";
			if (Enum.TryParse<GridLocations>(value, out var result))
			{
				GridReference variableCoordinate2 = VariableCoordinate;
				variableCoordinate2.Location = result;
			}
		}
	}

	public void SetCoordinate_GridLocation_X(int x)
	{
		if (VariableCoordinate == null)
		{
			GridReference variableCoordinate = new GridReference();
			VariableCoordinate = variableCoordinate;
		}
		GridReference variableCoordinate2 = VariableCoordinate;
		variableCoordinate2.X = x;
	}

	public void SetCoordinate_GridLocation_Y(int y)
	{
		if (VariableCoordinate == null)
		{
			GridReference variableCoordinate = new GridReference();
			VariableCoordinate = variableCoordinate;
		}
		GridReference variableCoordinate2 = VariableCoordinate;
		variableCoordinate2.Y = y;
	}

	public void SetShellSlot(float f)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si ecx,xmm0\"");
		object obj = (object)typeof(MathF) ^ (object)typeof(MathF);
		object obj2 = (object)typeof(MathF) & obj;
		bool flag = (nint)obj2 < 0;
		bool flag2 = (nint)typeof(MathF) < 0;
		bool flag3 = flag2 == flag;
		ShellSlotPool.ShellSlotSides variableShellSlot = (ShellSlotPool.ShellSlotSides)(-1 & (flag3 ? 1 : 0));
		VariableShellSlot = variableShellSlot;
	}

	public void SetShellSlot(int f)
	{
		int num = f ^ f;
		int num2 = f & num;
		bool flag = num2 < 0;
		bool flag2 = f < 0;
		bool flag3 = f == 0;
		bool flag4 = flag2 == flag;
		bool flag5 = !flag3;
		ShellSlotPool.ShellSlotSides variableShellSlot = ((flag5 & flag4) ? ShellSlotPool.ShellSlotSides.Right : ShellSlotPool.ShellSlotSides.Left);
		VariableShellSlot = variableShellSlot;
	}

	public void SetShellSlot(bool right)
	{
		VariableShellSlot = (right ? ShellSlotPool.ShellSlotSides.Right : ShellSlotPool.ShellSlotSides.Left);
	}

	public void SetShellSlot_Right()
	{
		VariableShellSlot = ShellSlotPool.ShellSlotSides.Right;
	}

	public void SetShellSlot_Left()
	{
		VariableShellSlot = ShellSlotPool.ShellSlotSides.Left;
	}
}
