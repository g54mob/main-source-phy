using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal;

internal class UVChannels<TVec>
{
	private static readonly int UVChannelCount;

	private ResizableArray<TVec>[] channels;

	private TVec[][] channelsData;

	public TVec[][] Data
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			//IL_0009: Expected O, but got I4
			//IL_0012: Expected O, but got I4
			//IL_0032: Expected O, but got I
			//IL_004d: Expected O, but got I
			//IL_018e: Expected I, but got O
			//IL_0204: Unknown result type (might be due to invalid IL or missing references)
			//IL_0209: Expected O, but got Unknown
			//IL_0212: Unknown result type (might be due to invalid IL or missing references)
			//IL_0217: Expected O, but got Unknown
			//IL_012d: Expected I, but got O
			object obj = 32;
			object obj2 = 0;
			object obj5 = default(object);
			IntPtr intPtr = default(IntPtr);
			while (true)
			{
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rcx_v4 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1>)+30]");
				object obj3 = 0;
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rax_v8+B8]");
				object obj4 = 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
				{
					ResizableArray<TVec>[] array = channels;
					if (channels != null)
					{
						if ((nint)obj2 >= array.Length)
						{
							break;
						}
						TVec[][] array2 = channelsData;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rax_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<TVec>[])+v69 @ rbp_v2]");
						if ((nint)0 == 0)
						{
							if (channelsData != null)
							{
								if ((nint)obj2 >= array2.Length)
								{
									break;
								}
								nint num3 = unchecked((nint)null);
								goto IL_01f6;
							}
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rax_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<TVec>[])+v69 @ rbp_v2]");
							num2 = 0;
							if (channelsData != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rcx_v16 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1>)+10]");
								if ((nint)0 != 0)
								{
									nint num4 = (nint)array2;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									bool flag = obj5 == null;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rcx_v16 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1>)+10]");
									num2 = 0;
									if (flag)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										throw intPtr;
									}
								}
								bool flag2 = (nint)obj2 >= array2.Length;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rcx_v16 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1>)+10]");
								nint num3 = 0;
								if (flag2)
								{
									break;
								}
								goto IL_01f6;
							}
						}
					}
					throw new NullReferenceException();
				}
				return channelsData;
				IL_01f6:
				obj2++;
				obj += 8;
			}
			throw new IndexOutOfRangeException();
		}
	}

	// C# has no syntax for parameterized property 'Item'.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ResizableArray<TVec> get_Item(int index)
	{
		ResizableArray<TVec>[] array = channels;
		if (index < array.Length)
		{
			return array[index];
		}
		return (ResizableArray<TVec>)(object)new IndexOutOfRangeException();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void set_Item(int index, ResizableArray<TVec> value)
	{
		ResizableArray<TVec>[] array = channels;
		array[index] = value;
	}

	public UVChannels()
	{
		//IL_0026: Expected O, but got I
		//IL_003b: Expected O, but got I
		//IL_0075: Expected O, but got I
		//IL_008a: Expected O, but got I
		base._002Ector();
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rcx_v3 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1>)+30]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v8+B8]");
		object obj2 = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
		ResizableArray<TVec>[] array = default(ResizableArray<TVec>[]);
		channels = array;
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rcx_v9 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1>)+30]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v108 @ rax_v17+B8]");
		object obj4 = 0;
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
		TVec[][] array2 = default(TVec[][]);
		channelsData = array2;
	}

	public void Resize(int capacity, bool trimExess = false)
	{
		//IL_0009: Expected O, but got I4
		//IL_0012: Expected O, but got I4
		//IL_0032: Expected O, but got I
		//IL_0047: Expected O, but got I
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		object obj = 32;
		object obj2 = 0;
		while (true)
		{
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rcx_v4 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1>)+30]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v8+B8]");
			object obj4 = 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
			{
				ResizableArray<TVec>[] array = channels;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rdi_v2+v138 @ rax_v10 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<TVec>[])]");
				if ((nint)0 != 0)
				{
					ResizableArray<TVec>[] array2 = channels;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
				}
				obj2++;
				obj += 8;
				continue;
			}
			break;
		}
	}

	static UVChannels()
	{
		//IL_001b: Expected O, but got I
		//IL_0031: Expected O, but got I
		//IL_0082: Expected O, but got I
		//IL_008b: Expected O, but got I4
		//IL_005e: Expected O, but got I
		//IL_0067: Expected O, but got I4
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v7 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1>)+30]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v4+135]");
		object obj2 = (nint)0 & (nint)1;
		if (obj2 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v4+B8]");
			object obj3 = 0;
			obj3 = MeshUtils.UVChannelCount;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rax_v8+B8]");
			object obj4 = 0;
			obj4 = MeshUtils.UVChannelCount;
		}
	}
}
