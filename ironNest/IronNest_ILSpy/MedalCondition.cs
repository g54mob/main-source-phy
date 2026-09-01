using System;
using Cpp2ILInjected;

[Serializable]
public class MedalCondition
{
	public MedalNumberExpression Left;

	public MedalCompareOperator Operator;

	public MedalNumberExpression Right;

	public bool Resolve(MedalTrackedValues values)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 18 Invalid \"Jump target not found in method: 0x1804229BC\"");
		float num = Left.Resolve(values);
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 33 Invalid \"Jump target not found in method: 0x1804229BC\"");
		float num2 = Right.Resolve(values);
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 51 Invalid \"Jump target not found in method: 0x1804229AA\"");
		return (byte)Operator != 0;
	}

	public MedalCondition()
	{
		MedalNumberExpression left = new MedalNumberExpression();
		Left = left;
		MedalNumberExpression right = new MedalNumberExpression();
		Right = right;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
