using System.Runtime.CompilerServices;

internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static MonoScriptData Get()
	{
		//IL_0067: Expected native int or pointer, but got O
		//IL_0075: Expected native int or pointer, but got O
		//IL_009e: Expected native int or pointer, but got O
		//IL_002e: Expected native int or pointer, but got O
		//IL_003c: Expected native int or pointer, but got O
		//IL_004a: Expected native int or pointer, but got O
		//IL_0058: Expected native int or pointer, but got O
		MonoScriptData monoScriptData = default(MonoScriptData);
		System.Runtime.CompilerServices.Unsafe.Write(&((MonoScriptData*)(nint)monoScriptData)->FilePathsData, null);
		((MonoScriptData*)(nint)monoScriptData)->TotalTypes = 0;
		System.Runtime.CompilerServices.Unsafe.Write(&((MonoScriptData*)(nint)monoScriptData)->FilePathsData, new byte[360]
		{
			0, 0, 0, 1, 0, 0, 0, 115, 92, 65,
			115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
			105, 110, 115, 92, 75, 97, 109, 103, 97, 109,
			92, 83, 101, 116, 116, 105, 110, 103, 115, 71,
			101, 110, 101, 114, 97, 116, 111, 114, 92, 76,
			105, 98, 115, 92, 85, 73, 84, 111, 111, 108,
			107, 105, 116, 67, 111, 109, 112, 111, 110, 101,
			110, 116, 115, 70, 111, 114, 83, 101, 116, 116,
			105, 110, 103, 115, 92, 82, 117, 110, 116, 105,
			109, 101, 92, 83, 99, 114, 105, 112, 116, 115,
			92, 85, 73, 69, 108, 101, 109, 101, 110, 116,
			67, 108, 105, 99, 107, 69, 118, 101, 110, 116,
			46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
			111, 92, 65, 115, 115, 101, 116, 115, 92, 80,
			108, 117, 103, 105, 110, 115, 92, 75, 97, 109,
			103, 97, 109, 92, 83, 101, 116, 116, 105, 110,
			103, 115, 71, 101, 110, 101, 114, 97, 116, 111,
			114, 92, 76, 105, 98, 115, 92, 85, 73, 84,
			111, 111, 108, 107, 105, 116, 67, 111, 109, 112,
			111, 110, 101, 110, 116, 115, 70, 111, 114, 83,
			101, 116, 116, 105, 110, 103, 115, 92, 82, 117,
			110, 116, 105, 109, 101, 92, 83, 99, 114, 105,
			112, 116, 115, 92, 85, 73, 69, 108, 101, 109,
			101, 110, 116, 69, 118, 101, 110, 116, 115, 46,
			99, 115, 0, 0, 0, 1, 0, 0, 0, 110,
			92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
			117, 103, 105, 110, 115, 92, 75, 97, 109, 103,
			97, 109, 92, 83, 101, 116, 116, 105, 110, 103,
			115, 71, 101, 110, 101, 114, 97, 116, 111, 114,
			92, 76, 105, 98, 115, 92, 85, 73, 84, 111,
			111, 108, 107, 105, 116, 67, 111, 109, 112, 111,
			110, 101, 110, 116, 115, 70, 111, 114, 83, 101,
			116, 116, 105, 110, 103, 115, 92, 82, 117, 110,
			116, 105, 109, 101, 92, 83, 99, 114, 105, 112,
			116, 115, 92, 85, 73, 69, 108, 101, 109, 101,
			110, 116, 84, 121, 112, 101, 115, 46, 99, 115
		});
		System.Runtime.CompilerServices.Unsafe.Write(&((MonoScriptData*)(nint)monoScriptData)->TypesData, new byte[177]
		{
			0, 0, 0, 0, 57, 75, 97, 109, 103, 97,
			109, 46, 85, 73, 84, 111, 111, 108, 107, 105,
			116, 67, 111, 109, 112, 111, 110, 101, 110, 116,
			115, 70, 111, 114, 83, 101, 116, 116, 105, 110,
			103, 115, 124, 85, 73, 69, 108, 101, 109, 101,
			110, 116, 67, 108, 105, 99, 107, 69, 118, 101,
			110, 116, 0, 0, 0, 0, 53, 75, 97, 109,
			103, 97, 109, 46, 85, 73, 84, 111, 111, 108,
			107, 105, 116, 67, 111, 109, 112, 111, 110, 101,
			110, 116, 115, 70, 111, 114, 83, 101, 116, 116,
			105, 110, 103, 115, 124, 85, 73, 69, 108, 101,
			109, 101, 110, 116, 69, 118, 101, 110, 116, 115,
			0, 0, 0, 0, 52, 75, 97, 109, 103, 97,
			109, 46, 85, 73, 84, 111, 111, 108, 107, 105,
			116, 67, 111, 109, 112, 111, 110, 101, 110, 116,
			115, 70, 111, 114, 83, 101, 116, 116, 105, 110,
			103, 115, 124, 85, 73, 69, 108, 101, 109, 101,
			110, 116, 84, 121, 112, 101, 115
		});
		((MonoScriptData*)(nint)monoScriptData)->TotalFiles = 3;
		((MonoScriptData*)(nint)monoScriptData)->TotalTypes = 3;
		((MonoScriptData*)(nint)monoScriptData)->IsEditorOnly = false;
		return monoScriptData;
	}
}
