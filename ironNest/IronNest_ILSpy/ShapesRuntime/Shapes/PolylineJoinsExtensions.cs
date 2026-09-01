using System;
using Cpp2ILInjected;

namespace Shapes;

internal static class PolylineJoinsExtensions
{
	public static bool HasJoinMesh(PolylineJoins join)
	{
		//IL_002b: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		bool flag = join == PolylineJoins.Simple;
		if (!flag)
		{
			object obj = join - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (flag || (nint)obj2 == 1)
				{
					return true;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				object actualValue = default(object);
				ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("join", actualValue, null);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
		}
		return false;
	}

	public static bool HasSimpleJoin(PolylineJoins join)
	{
		//IL_002b: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected I4, but got Unknown
		bool flag = join == PolylineJoins.Simple;
		if (!flag)
		{
			object obj = join - 1;
			if (!flag)
			{
				bool flag2 = (byte)(obj - 1) != 0;
				if (!flag)
				{
					if (flag2)
					{
						return flag2;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					object actualValue = default(object);
					ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("join", actualValue, null);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
			}
		}
		return false;
	}
}
