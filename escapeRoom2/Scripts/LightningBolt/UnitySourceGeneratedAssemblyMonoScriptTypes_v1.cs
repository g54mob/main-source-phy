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
			FilePathsData = new byte[52]
			{
				0, 0, 0, 1, 0, 0, 0, 44, 92, 65,
				115, 115, 101, 116, 115, 92, 76, 105, 103, 104,
				116, 110, 105, 110, 103, 66, 111, 108, 116, 92,
				76, 105, 103, 104, 116, 110, 105, 110, 103, 66,
				111, 108, 116, 83, 99, 114, 105, 112, 116, 46,
				99, 115
			},
			TypesData = new byte[50]
			{
				0, 0, 0, 0, 45, 68, 105, 103, 105, 116,
				97, 108, 82, 117, 98, 121, 46, 76, 105, 103,
				104, 116, 110, 105, 110, 103, 66, 111, 108, 116,
				124, 76, 105, 103, 104, 116, 110, 105, 110, 103,
				66, 111, 108, 116, 83, 99, 114, 105, 112, 116
			},
			TotalFiles = 1,
			TotalTypes = 1,
			IsEditorOnly = false
		};
	}
}
