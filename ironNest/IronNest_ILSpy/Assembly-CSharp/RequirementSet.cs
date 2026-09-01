using System;
using System.Collections.Generic;
using Cpp2ILInjected;

[Serializable]
public class RequirementSet
{
	[Serializable]
	public class RequirementPair
	{
		public enum Operation
		{
			Base,
			And,
			Or
		}

		public Operation operation;

		public Requirement requirement;
	}

	public RequirementPair[] requirements;

	public bool Resolve(Dictionary<string, object> variables)
	{
		//IL_002c: Expected O, but got I4
		//IL_0043: Expected O, but got I4
		//IL_004c: Expected O, but got I4
		//IL_005e: Expected O, but got I4
		//IL_0357: Expected I4, but got O
		//IL_0094: Expected O, but got I
		//IL_00bb: Expected I, but got O
		//IL_00f0: Expected I, but got O
		//IL_0113: Expected O, but got I
		//IL_013f: Expected I, but got O
		//IL_017e: Expected O, but got I
		//IL_01a2: Expected I, but got O
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Expected O, but got Unknown
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Expected O, but got Unknown
		//IL_01eb: Expected O, but got I
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_0218: Expected I, but got O
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Expected O, but got Unknown
		RequirementPair[] array = requirements;
		bool flag = requirements == null;
		Dictionary<string, object> dictionary2 = default(Dictionary<string, object>);
		Dictionary<string, object> dictionary = dictionary2;
		object obj = 0;
		if (!flag)
		{
			object obj2 = 32;
			obj = 0;
			bool flag2 = true;
			object obj3 = 0;
			while (true)
			{
				if ((nint)obj < array.Length)
				{
					if ((nint)obj3 < array.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rdi_v5+v169 @ rax_v12 (RequirementPair[])]");
						object obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rdi_v5+v169 @ rax_v12 (RequirementPair[])]");
						bool flag3 = (nint)0 == 0;
						dictionary = dictionary2;
						nint num = (nint)array;
						if (flag3)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rax_v15+18]");
						bool flag4 = (nint)0 == 0;
						dictionary = dictionary2;
						num = (nint)array;
						if (flag4)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rax_v15+18]");
						bool flag5 = ((Requirement)0).Execute(dictionary2);
						RequirementPair[] array2 = requirements;
						bool flag6 = requirements == null;
						dictionary = dictionary2;
						num = unchecked((nint)null);
						if (flag6)
						{
							break;
						}
						if ((nint)obj3 < array2.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rdi_v5+v39 @ rcx_v12 (RequirementPair[])]");
							object obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rdi_v5+v39 @ rcx_v12 (RequirementPair[])]");
							bool flag7 = (nint)0 == 0;
							dictionary = dictionary2;
							num = unchecked((nint)null);
							if (flag7)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rcx_v13+10]");
							bool flag8 = (nint)0 == 0;
							if (!flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rcx_v13+10]");
								object obj6 = -1;
								if (!flag8)
								{
									bool flag9 = (nint)obj6 != 1;
									dictionary = dictionary2;
									num = unchecked((nint)null);
									if (flag9)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										NotImplementedException ex = new NotImplementedException();
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										throw ex;
									}
									flag2 |= flag5;
									array = requirements;
									obj3++;
									obj2 += 8;
									obj = obj3;
								}
								else
								{
									if (!flag2 && !flag5)
									{
										return false;
									}
									flag2 &= flag5;
									array = requirements;
									obj3++;
									obj2 += 8;
									obj = obj3;
								}
							}
							else
							{
								obj3++;
								array = requirements;
								obj2 += 8;
								obj = obj3;
								flag2 = flag5;
							}
							continue;
						}
					}
					IndexOutOfRangeException ex2 = new IndexOutOfRangeException();
					return (byte)(int)ex2 != 0;
				}
				return flag2;
			}
		}
		throw new NullReferenceException();
	}

	public RequirementSet()
	{
		RequirementPair[] array = new RequirementPair[0];
		requirements = array;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
