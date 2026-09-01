using System;
using Cpp2ILInjected;

namespace Shapes;

public static class AngularUnitExtensions
{
	public static string[] angUnitToSuffix = new string[3] { "rad", "°", "tr" };

	public static string[] angUnitNames = new string[3] { "Radians", "Degrees", "Turns" };

	public static string[] angUnitNamesShort = new string[3] { "Rad", "Deg", "Turns" };

	public static string Suffix(AngularUnit unit)
	{
		string[] array = angUnitToSuffix;
		if ((int)unit < array.Length)
		{
			return array[(int)unit];
		}
		return (string)(object)new IndexOutOfRangeException();
	}

	public static string Name(AngularUnit unit)
	{
		string[] array = angUnitNames;
		if ((int)unit < array.Length)
		{
			return array[(int)unit];
		}
		return (string)(object)new IndexOutOfRangeException();
	}

	public static string NameShort(AngularUnit unit)
	{
		string[] array = angUnitNamesShort;
		if ((int)unit < array.Length)
		{
			return array[(int)unit];
		}
		return (string)(object)new IndexOutOfRangeException();
	}

	public static float FromRadians(AngularUnit unit)
	{
		//IL_0030: Expected O, but got I4
		bool flag = unit == AngularUnit.Radians;
		if (!flag)
		{
			object obj = unit - 1;
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					return 1f / ((float)Math.PI * 2f);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				object actualValue = default(object);
				ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("unit", actualValue, null);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			return 1f / ((float)Math.PI / 180f);
		}
		return 1f / 1f;
	}

	public static float ToRadians(AngularUnit unit)
	{
		//IL_002b: Expected O, but got I4
		bool flag = unit == AngularUnit.Radians;
		if (!flag)
		{
			object obj = unit - 1;
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					return (float)Math.PI * 2f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				object actualValue = default(object);
				ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("unit", actualValue, null);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			return (float)Math.PI / 180f;
		}
		return 1f;
	}
}
