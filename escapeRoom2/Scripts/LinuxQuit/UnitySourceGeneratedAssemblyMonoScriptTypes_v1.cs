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
			FilePathsData = new byte[46]
			{
				0, 0, 0, 1, 0, 0, 0, 38, 92, 65,
				115, 115, 101, 116, 115, 92, 76, 105, 110, 117,
				120, 81, 117, 105, 116, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 76, 105, 110, 117, 120, 81,
				117, 105, 116, 46, 99, 115
			},
			TypesData = new byte[15]
			{
				0, 0, 0, 0, 10, 124, 76, 105, 110, 117,
				120, 81, 117, 105, 116
			},
			TotalFiles = 1,
			TotalTypes = 1,
			IsEditorOnly = false
		};
	}
}
