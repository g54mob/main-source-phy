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
			FilePathsData = new byte[47]
			{
				0, 0, 0, 2, 0, 0, 0, 39, 92, 80,
				97, 99, 107, 97, 103, 101, 115, 92, 112, 105,
				110, 101, 46, 100, 114, 97, 119, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 80, 105, 110, 101,
				68, 114, 97, 119, 46, 99, 115
			},
			TypesData = new byte[35]
			{
				0, 0, 0, 0, 16, 124, 80, 105, 110, 101,
				68, 114, 97, 119, 67, 111, 110, 116, 101, 120,
				116, 0, 0, 0, 0, 9, 124, 80, 105, 110,
				101, 68, 114, 97, 119
			},
			TotalFiles = 1,
			TotalTypes = 2,
			IsEditorOnly = false
		};
	}
}
