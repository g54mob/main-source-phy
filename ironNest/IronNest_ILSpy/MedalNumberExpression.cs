using System;
using Cpp2ILInjected;

[Serializable]
public class MedalNumberExpression
{
	public MedalExpressionMode Mode;

	public MedalValueSource Source;

	public float InlineValue;

	public MedalTrackedValue Variable;

	public string CustomVariableKey;

	public MedalNumberOperand A;

	public MedalMathOperator MathOperator;

	public MedalNumberOperand B;

	public float Resolve(MedalTrackedValues values)
	{
		//IL_0397: Expected O, but got I4
		//IL_007d: Expected O, but got I4
		//IL_03cb: Expected F4, but got I4
		//IL_0155: Expected O, but got I4
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_00b1: Expected F4, but got I4
		//IL_027a: Expected O, but got I4
		//IL_0253: Expected O, but got I4
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_01b9: Expected F4, but got I4
		//IL_02bf: Expected F4, but got I4
		//IL_02f2: Expected F4, but got I4
		if (Mode != MedalExpressionMode.Value)
		{
			MedalNumberOperand a = A;
			if (A != null)
			{
				bool flag = a.Source == MedalValueSource.Inline;
				float num;
				if (!flag)
				{
					object obj = a.Source - 1;
					if (!flag)
					{
						if ((nint)obj != 1)
						{
							num = 0f;
						}
						else
						{
							if (values == null)
							{
								goto IL_0449;
							}
							float customValue = values.GetCustomValue(a.CustomVariableKey);
							num = customValue;
						}
					}
					else
					{
						if (values == null)
						{
							goto IL_0449;
						}
						float customValue = values.GetValue(a.Variable);
						num = customValue;
					}
				}
				else
				{
					num = a.InlineValue;
				}
				MedalNumberOperand b = B;
				if (B != null)
				{
					string text = (string)b.Source;
					bool flag2 = b.Source == MedalValueSource.Inline;
					float num2;
					if (!flag2)
					{
						text = (string)(text - 1);
						if (!flag2)
						{
							if ((nint)text != 1)
							{
								num2 = 0f;
							}
							else
							{
								if (values == null)
								{
									goto IL_0449;
								}
								text = b.CustomVariableKey;
								float customValue2 = values.GetCustomValue(b.CustomVariableKey);
								num2 = customValue2;
							}
						}
						else
						{
							if (values == null)
							{
								goto IL_0449;
							}
							float value = values.GetValue(b.Variable);
							num2 = value;
							text = (string)b.Variable;
						}
					}
					else
					{
						num2 = b.InlineValue;
					}
					bool flag3 = MathOperator == MedalMathOperator.Add;
					float result;
					if (!flag3)
					{
						object obj2 = MathOperator - 1;
						if (flag3)
						{
							return num - num2;
						}
						object obj3 = obj2 - 1;
						if (flag3)
						{
							return num2 * num;
						}
						bool flag4 = (nint)obj3 != 1;
						result = 0f;
						if (!flag4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
							object obj4 = default(object);
							bool flag5 = obj4 != null;
							result = 0f;
							if (!flag5)
							{
								return num / num2;
							}
						}
					}
					else
					{
						result = num2 + num;
					}
					return result;
				}
			}
		}
		else
		{
			bool flag6 = Source == MedalValueSource.Inline;
			if (flag6)
			{
				return InlineValue;
			}
			object obj5 = Source - 1;
			if (!flag6)
			{
				if ((nint)obj5 != 1)
				{
					return 0f;
				}
				if (values != null)
				{
					return values.GetCustomValue(CustomVariableKey);
				}
			}
			else if (values != null)
			{
				return values.GetValue(Variable);
			}
		}
		goto IL_0449;
		IL_0449:
		throw new NullReferenceException();
	}

	private float ResolveValue(MedalTrackedValues values)
	{
		//IL_002f: Expected O, but got I4
		//IL_0060: Expected F4, but got I4
		bool flag = Source == MedalValueSource.Inline;
		if (!flag)
		{
			object obj = Source - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					return 0f;
				}
				return values.GetCustomValue(CustomVariableKey);
			}
			return values.GetValue(Variable);
		}
		return InlineValue;
	}

	public MedalNumberExpression()
	{
		MedalNumberOperand a = new MedalNumberOperand();
		A = a;
		MedalNumberOperand b = new MedalNumberOperand();
		B = b;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
