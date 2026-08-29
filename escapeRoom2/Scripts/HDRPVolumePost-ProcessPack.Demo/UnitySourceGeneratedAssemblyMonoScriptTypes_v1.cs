using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;

[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
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
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[48]
			{
				0, 0, 0, 1, 0, 0, 0, 40, 92, 65,
				115, 115, 101, 116, 115, 92, 86, 80, 80, 80,
				92, 68, 101, 109, 111, 92, 83, 99, 114, 105,
				112, 116, 115, 92, 68, 101, 109, 111, 83, 116,
				97, 114, 116, 117, 112, 46, 99, 115
			},
			TypesData = new byte[42]
			{
				0, 0, 0, 0, 37, 72, 68, 82, 80, 86,
				111, 108, 117, 109, 101, 80, 111, 115, 116, 80,
				114, 111, 99, 101, 115, 115, 80, 97, 99, 107,
				124, 68, 101, 109, 111, 83, 116, 97, 114, 116,
				117, 112
			},
			TotalFiles = 1,
			TotalTypes = 1,
			IsEditorOnly = false
		};
	}
}
