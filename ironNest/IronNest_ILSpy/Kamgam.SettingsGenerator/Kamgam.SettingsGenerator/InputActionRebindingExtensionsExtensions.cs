using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Kamgam.SettingsGenerator;

public static class InputActionRebindingExtensionsExtensions
{
	public unsafe static bool FindBinding(InputActionAsset inputActionAsset, string bindingId, out InputBinding binding)
	{
		//IL_003e: Expected O, but got Ref
		//IL_0088: Expected O, but got Ref
		//IL_012e: Expected O, but got Ref
		ref InputBinding reference;
		if (!string.IsNullOrEmpty(bindingId))
		{
			Guid guid = Guid.Parse(bindingId);
			bool flag = (object)inputActionAsset == null;
			int num = default(int);
			InputActionAsset inputActionAsset2 = (InputActionAsset)(&num);
			if (!flag)
			{
				IEnumerable<InputBinding> bindings = inputActionAsset.bindings;
				if (bindings != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					IntPtr intPtr = default(IntPtr);
					object obj = (object)(&intPtr);
					int num2 = 0;
					inputActionAsset2 = null;
					object obj2 = default(object);
					InputBinding inputBinding = default(InputBinding);
					object obj3 = default(object);
					while (true)
					{
						if (intPtr != (IntPtr)0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							if (obj2 == null)
							{
								break;
							}
							bool flag2 = intPtr == (IntPtr)0;
							inputActionAsset2 = null;
							if (!flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18018D060");
								num2 = inputBinding.id._a;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180645F80");
								bool flag3 = obj3 == null;
								inputActionAsset2 = (InputActionAsset)(&num);
								if (!flag3)
								{
									reference = ref *(InputBinding*)inputBinding;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v400 @ rax_v22+10]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v400 @ rax_v22+20]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v400 @ rax_v22+30]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v400 @ rax_v22+40]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v400 @ rax_v22+50]");
									_ = 0;
									if (obj != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
									}
									return true;
								}
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					}
					goto IL_021b;
				}
			}
			throw new NullReferenceException();
		}
		goto IL_021b;
		IL_021b:
		reference = ref *(InputBinding*)null;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		return false;
	}

	public unsafe static InputAction GetActionOfBinding(InputActionAsset inputActionAsset, string bindingId)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0040: Expected O, but got Ref
		//IL_0077: Expected O, but got I4
		//IL_0080: Expected O, but got I4
		//IL_0089: Expected O, but got I4
		//IL_009a: Expected O, but got I4
		//IL_040a: Expected O, but got Ref
		//IL_0425: Expected O, but got Ref
		//IL_00b7: Expected O, but got Ref
		//IL_0107: Expected O, but got I4
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Expected O, but got Unknown
		//IL_03ad: Expected O, but got Ref
		//IL_03c8: Expected O, but got Ref
		//IL_0123: Expected O, but got Ref
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_018c: Expected O, but got Ref
		//IL_01a7: Expected O, but got Ref
		//IL_01e5: Expected O, but got I
		//IL_0224: Expected O, but got Ref
		//IL_0321: Expected O, but got I
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_028c: Expected O, but got I
		//IL_02b1: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (!string.IsNullOrEmpty(bindingId))
		{
			Guid guid = Guid.Parse(bindingId);
			if ((object)inputActionAsset != null)
			{
				int a = default(int);
				ReadOnlyArray<InputActionMap> actionMaps = ((InputActionAsset)(&a)).actionMaps;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
				object obj3 = (object)actionMaps >> 32;
				bool flag = (nint)obj3 <= 0;
				InputBinding inputBinding = (InputBinding)0;
				InputBinding inputBinding2 = (InputBinding)0;
				object obj4 = 0;
				object obj5 = actionMaps;
				object obj6 = 0;
				if (flag)
				{
					goto IL_0307;
				}
				object obj15 = default(object);
				while (true)
				{
					InputActionAsset inputActionAsset2 = (InputActionAsset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
					ReadOnlyArray<InputActionMap> actionMaps2 = inputActionAsset2.actionMaps;
					object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 248));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+F8]");
					if ((nint)0 == 0)
					{
						break;
					}
					InputActionMap inputActionMap = (InputActionMap)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
					ReadOnlyArray<InputAction> actions = inputActionMap.actions;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
					object obj8 = (object)actions >> 32;
					bool flag2 = (nint)obj8 <= 0;
					InputBinding inputBinding3 = inputBinding2;
					object obj9 = actions;
					object obj10 = 0;
					if (!flag2)
					{
						while (true)
						{
							InputActionMap inputActionMap2 = (InputActionMap)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 88));
							ReadOnlyArray<InputAction> actions2 = inputActionMap2.actions;
							object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 264));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+108]");
							if ((nint)0 == 0)
							{
								break;
							}
							InputAction inputAction = (InputAction)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 104));
							ReadOnlyArray<InputBinding> bindings = inputAction.bindings;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
							object obj12 = (object)bindings >> 32;
							bool flag3 = (nint)obj12 <= 0;
							obj4 = bindings;
							string text = null;
							obj4 = bindings;
							if (!flag3)
							{
								bool flag4;
								do
								{
									InputAction inputAction2 = (InputAction)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 120));
									ReadOnlyArray<InputBinding> bindings2 = inputAction2.bindings;
									object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-10]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+10]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
									inputBinding3 = (InputBinding)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+20]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
									_ = 0;
									_ = guid._a;
									Guid id = inputBinding.id;
									object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180645F80");
									if (obj15 == null)
									{
										text++;
										flag4 = System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12);
										a = id._a;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
										inputBinding = (InputBinding)0;
										obj4 = bindings2;
										a = id._a;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
										inputBinding = (InputBinding)0;
										obj4 = bindings2;
										continue;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+108]");
									return (InputAction)0;
								}
								while (flag4);
							}
							obj10++;
							bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8);
							obj9 = actions2;
							if (flag5)
							{
								continue;
							}
							goto IL_02f9;
						}
						break;
					}
					goto IL_036d;
					IL_02f9:
					inputBinding2 = inputBinding3;
					goto IL_036d;
					IL_036d:
					obj6++;
					bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3);
					obj5 = actionMaps2;
					if (flag6)
					{
						continue;
					}
					goto IL_0307;
				}
			}
			return (InputAction)(object)new NullReferenceException();
		}
		goto IL_0307;
		IL_0307:
		return null;
	}

	public unsafe static InputActionMap GetActionMapOfBinding(InputActionAsset inputActionAsset, string bindingId)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0040: Expected O, but got Ref
		//IL_0077: Expected O, but got I4
		//IL_0080: Expected O, but got I4
		//IL_0089: Expected O, but got I4
		//IL_009a: Expected O, but got I4
		//IL_040a: Expected O, but got Ref
		//IL_0425: Expected O, but got Ref
		//IL_00b7: Expected O, but got Ref
		//IL_0107: Expected O, but got I4
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Expected O, but got Unknown
		//IL_03ad: Expected O, but got Ref
		//IL_03c8: Expected O, but got Ref
		//IL_0123: Expected O, but got Ref
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_018c: Expected O, but got Ref
		//IL_01a7: Expected O, but got Ref
		//IL_01e5: Expected O, but got I
		//IL_0224: Expected O, but got Ref
		//IL_0321: Expected O, but got I
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_028c: Expected O, but got I
		//IL_02b1: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (!string.IsNullOrEmpty(bindingId))
		{
			Guid guid = Guid.Parse(bindingId);
			if ((object)inputActionAsset != null)
			{
				int a = default(int);
				ReadOnlyArray<InputActionMap> actionMaps = ((InputActionAsset)(&a)).actionMaps;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
				object obj3 = (object)actionMaps >> 32;
				bool flag = (nint)obj3 <= 0;
				InputBinding inputBinding = (InputBinding)0;
				InputBinding inputBinding2 = (InputBinding)0;
				object obj4 = 0;
				object obj5 = actionMaps;
				object obj6 = 0;
				if (flag)
				{
					goto IL_0307;
				}
				object obj15 = default(object);
				while (true)
				{
					InputActionAsset inputActionAsset2 = (InputActionAsset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
					ReadOnlyArray<InputActionMap> actionMaps2 = inputActionAsset2.actionMaps;
					object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 248));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+F8]");
					if ((nint)0 == 0)
					{
						break;
					}
					InputActionMap inputActionMap = (InputActionMap)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
					ReadOnlyArray<InputAction> actions = inputActionMap.actions;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
					object obj8 = (object)actions >> 32;
					bool flag2 = (nint)obj8 <= 0;
					InputBinding inputBinding3 = inputBinding2;
					object obj9 = actions;
					object obj10 = 0;
					if (!flag2)
					{
						while (true)
						{
							InputActionMap inputActionMap2 = (InputActionMap)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 88));
							ReadOnlyArray<InputAction> actions2 = inputActionMap2.actions;
							object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 264));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+108]");
							if ((nint)0 == 0)
							{
								break;
							}
							InputAction inputAction = (InputAction)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 104));
							ReadOnlyArray<InputBinding> bindings = inputAction.bindings;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
							object obj12 = (object)bindings >> 32;
							bool flag3 = (nint)obj12 <= 0;
							obj4 = bindings;
							string text = null;
							obj4 = bindings;
							if (!flag3)
							{
								bool flag4;
								do
								{
									InputAction inputAction2 = (InputAction)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 120));
									ReadOnlyArray<InputBinding> bindings2 = inputAction2.bindings;
									object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-10]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+10]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
									inputBinding3 = (InputBinding)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+20]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
									_ = 0;
									_ = guid._a;
									Guid id = inputBinding.id;
									object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180645F80");
									if (obj15 == null)
									{
										text++;
										flag4 = System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12);
										a = id._a;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
										inputBinding = (InputBinding)0;
										obj4 = bindings2;
										a = id._a;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
										inputBinding = (InputBinding)0;
										obj4 = bindings2;
										continue;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+F8]");
									return (InputActionMap)0;
								}
								while (flag4);
							}
							obj10++;
							bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8);
							obj9 = actions2;
							if (flag5)
							{
								continue;
							}
							goto IL_02f9;
						}
						break;
					}
					goto IL_036d;
					IL_02f9:
					inputBinding2 = inputBinding3;
					goto IL_036d;
					IL_036d:
					obj6++;
					bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3);
					obj5 = actionMaps2;
					if (flag6)
					{
						continue;
					}
					goto IL_0307;
				}
			}
			return (InputActionMap)(object)new NullReferenceException();
		}
		goto IL_0307;
		IL_0307:
		return null;
	}

	public unsafe static int GetBindingIndexWithinActionMap(InputActionAsset inputActionAsset, string bindingId)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0255: Expected I4, but got I8
		//IL_0275: Expected I4, but got O
		//IL_0040: Expected O, but got Ref
		//IL_0079: Expected O, but got I4
		//IL_008b: Expected O, but got I4
		//IL_02b0: Expected O, but got Ref
		//IL_02cb: Expected O, but got Ref
		//IL_00a7: Expected O, but got Ref
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Expected O, but got Unknown
		//IL_010b: Expected O, but got Ref
		//IL_0126: Expected O, but got Ref
		//IL_017b: Expected O, but got Ref
		//IL_01e3: Expected O, but got I
		//IL_0208: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		if (!string.IsNullOrEmpty(bindingId))
		{
			Guid guid = Guid.Parse(bindingId);
			if ((object)inputActionAsset != null)
			{
				int a = default(int);
				ReadOnlyArray<InputActionMap> actionMaps = ((InputActionAsset)(&a)).actionMaps;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
				object obj3 = (object)actionMaps >> 32;
				bool flag = (nint)obj3 <= 0;
				InputBinding inputBinding = (InputBinding)0;
				object obj4 = actionMaps;
				object obj5 = 0;
				if (flag)
				{
					goto IL_0248;
				}
				object obj11 = default(object);
				while (true)
				{
					InputActionAsset inputActionAsset2 = (InputActionAsset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 24));
					ReadOnlyArray<InputActionMap> actionMaps2 = inputActionAsset2.actionMaps;
					object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 168));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+A8]");
					if ((nint)0 == 0)
					{
						break;
					}
					InputActionMap inputActionMap = (InputActionMap)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
					ReadOnlyArray<InputBinding> bindings = inputActionMap.bindings;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
					object obj7 = (object)bindings >> 32;
					bool flag2 = (nint)obj7 <= 0;
					object obj8 = bindings;
					int num = 0;
					if (!flag2)
					{
						bool flag3;
						do
						{
							InputActionMap inputActionMap2 = (InputActionMap)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
							ReadOnlyArray<InputBinding> bindings2 = inputActionMap2.bindings;
							object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-10]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+10]");
							_ = 0;
							_ = guid._a;
							Guid id = inputBinding.id;
							object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180645F80");
							if (obj11 == null)
							{
								num++;
								flag3 = num < (nint)obj7;
								a = id._a;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
								inputBinding = (InputBinding)0;
								obj8 = bindings2;
								a = id._a;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
								inputBinding = (InputBinding)0;
								continue;
							}
							return num;
						}
						while (flag3);
					}
					obj5++;
					bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3);
					obj4 = actionMaps2;
					if (flag4)
					{
						continue;
					}
					goto IL_0248;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		goto IL_0248;
		IL_0248:
		return -1;
	}

	public unsafe static int GetBindingIndexWithinAction(InputActionAsset inputActionAsset, string bindingId)
	{
		//IL_0008: Expected O, but got Ref
		//IL_032c: Expected I4, but got I8
		//IL_034c: Expected I4, but got O
		//IL_0040: Expected O, but got Ref
		//IL_0077: Expected O, but got I4
		//IL_0080: Expected O, but got I4
		//IL_0089: Expected O, but got I4
		//IL_009a: Expected O, but got I4
		//IL_0422: Expected O, but got Ref
		//IL_043d: Expected O, but got Ref
		//IL_00bf: Expected O, but got Ref
		//IL_010f: Expected O, but got I4
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Expected O, but got Unknown
		//IL_03c5: Expected O, but got Ref
		//IL_03e0: Expected O, but got Ref
		//IL_012b: Expected O, but got Ref
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Expected O, but got Unknown
		//IL_019c: Expected O, but got Ref
		//IL_01b7: Expected O, but got Ref
		//IL_01f5: Expected O, but got I
		//IL_0234: Expected O, but got Ref
		//IL_0271: Expected O, but got I4
		//IL_029c: Expected O, but got I
		//IL_02ac: Expected I4, but got O
		//IL_02c9: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (!string.IsNullOrEmpty(bindingId))
		{
			Guid guid = Guid.Parse(bindingId);
			if ((object)inputActionAsset != null)
			{
				int a = default(int);
				ReadOnlyArray<InputActionMap> actionMaps = ((InputActionAsset)(&a)).actionMaps;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
				object obj3 = (object)actionMaps >> 32;
				bool flag = (nint)obj3 <= 0;
				InputBinding inputBinding = (InputBinding)0;
				InputBinding inputBinding2 = (InputBinding)0;
				object obj4 = 0;
				object obj5 = actionMaps;
				object obj6 = 0;
				string text = bindingId;
				if (flag)
				{
					goto IL_031f;
				}
				object obj15 = default(object);
				while (true)
				{
					InputActionAsset inputActionAsset2 = (InputActionAsset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
					ReadOnlyArray<InputActionMap> actionMaps2 = inputActionAsset2.actionMaps;
					object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 248));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+F8]");
					if ((nint)0 == 0)
					{
						break;
					}
					InputActionMap inputActionMap = (InputActionMap)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
					ReadOnlyArray<InputAction> actions = inputActionMap.actions;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
					object obj8 = (object)actions >> 32;
					bool flag2 = (nint)obj8 <= 0;
					InputBinding inputBinding3 = inputBinding2;
					object obj9 = actions;
					object obj10 = 0;
					if (!flag2)
					{
						while (true)
						{
							InputActionMap inputActionMap2 = (InputActionMap)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 88));
							ReadOnlyArray<InputAction> actions2 = inputActionMap2.actions;
							object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 264));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+108]");
							if ((nint)0 == 0)
							{
								break;
							}
							InputAction inputAction = (InputAction)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 104));
							ReadOnlyArray<InputBinding> bindings = inputAction.bindings;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
							object obj12 = (object)bindings >> 32;
							bool flag3 = (nint)obj12 <= 0;
							obj4 = bindings;
							int num = 0;
							obj4 = bindings;
							text = null;
							if (!flag3)
							{
								bool flag4;
								do
								{
									InputAction inputAction2 = (InputAction)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 120));
									ReadOnlyArray<InputBinding> bindings2 = inputAction2.bindings;
									object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-10]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+10]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
									inputBinding3 = (InputBinding)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+20]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
									_ = 0;
									_ = guid._a;
									Guid id = inputBinding.id;
									object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180645F80");
									if (obj15 == null)
									{
										text = (string)(num + 1);
										flag4 = System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12);
										a = id._a;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
										inputBinding = (InputBinding)0;
										obj4 = bindings2;
										num = (int)text;
										a = id._a;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
										inputBinding = (InputBinding)0;
										obj4 = bindings2;
										continue;
									}
									return num;
								}
								while (flag4);
							}
							obj10++;
							bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8);
							obj9 = actions2;
							if (flag5)
							{
								continue;
							}
							goto IL_0311;
						}
						break;
					}
					goto IL_0385;
					IL_0311:
					inputBinding2 = inputBinding3;
					goto IL_0385;
					IL_0385:
					obj6++;
					bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3);
					obj5 = actionMaps2;
					if (flag6)
					{
						continue;
					}
					goto IL_031f;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		goto IL_031f;
		IL_031f:
		return -1;
	}

	public static void ApplyBindingOverride(InputActionAsset inputActionAsset, string bindingId, string overridePath, string overrideInteractions = null, string overrideProcessors = null)
	{
		bool flag = ApplyBindingOverrideWithResult(inputActionAsset, bindingId, overridePath, overrideInteractions, overrideProcessors);
	}

	public unsafe static bool ApplyBindingOverrideWithResult(InputActionAsset inputActionAsset, string bindingId, string overridePath, string overrideInteractions = null, string overrideProcessors = null)
	{
		//IL_0008: Expected O, but got Ref
		//IL_00db: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		int bindingIndexWithinActionMap = GetBindingIndexWithinActionMap(inputActionAsset, bindingId);
		if (bindingIndexWithinActionMap >= 0)
		{
			InputActionMap actionMapOfBinding = GetActionMapOfBinding(inputActionAsset, bindingId);
			if (actionMapOfBinding != null)
			{
				_ = 0;
				_ = 0;
				_ = 0;
				_ = 0;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+77]");
				_ = 0;
				InputBinding bindingOverride = (InputBinding)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-79]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-69]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-49]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-39]");
				_ = 0;
				InputActionRebindingExtensions.ApplyBindingOverride(actionMapOfBinding, bindingIndexWithinActionMap, bindingOverride);
				return true;
			}
		}
		return false;
	}

	public static void ClearOverride(InputActionAsset inputActionAsset, string bindingId)
	{
		int bindingIndexWithinAction = GetBindingIndexWithinAction(inputActionAsset, bindingId);
		if (bindingIndexWithinAction >= 0)
		{
			InputAction actionOfBinding = GetActionOfBinding(inputActionAsset, bindingId);
			if (actionOfBinding != null)
			{
				InputActionRebindingExtensions.RemoveBindingOverride(actionOfBinding, bindingIndexWithinAction);
			}
		}
	}
}
