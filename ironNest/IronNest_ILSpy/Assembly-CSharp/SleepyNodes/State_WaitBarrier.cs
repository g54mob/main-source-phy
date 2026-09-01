namespace SleepyNodes;

public class State_WaitBarrier : StateNode
{
	public StateNode To;

	public StateNode ResetCounter;

	public int Count = 1;

	public bool AutoReset;

	public bool StopAfter = true;

	public bool AllowUpTo;

	private int current;

	public override void ResetNode()
	{
		current = 0;
	}

	public override void OnEnter(NodeExecutionState state)
	{
		// ILSpy could not decompile this. Please report the exception below,
		// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
		// System.IndexOutOfRangeException: Index was outside the bounds of the array.
		//    at ICSharpCode.Decompiler.IL.ILReader.ReadBlock(ImportedBlock block, CancellationToken cancellationToken) in /_/ICSharpCode.Decompiler/IL/ILReader.cs:line 521
		//    at ICSharpCode.Decompiler.IL.ILReader.ReadInstructions(CancellationToken cancellationToken) in /_/ICSharpCode.Decompiler/IL/ILReader.cs:line 504
		//    at ICSharpCode.Decompiler.IL.ILReader.ReadIL(MethodDefinitionHandle method, MethodBodyBlock body, GenericContext genericContext, ILFunctionKind kind, CancellationToken cancellationToken) in /_/ICSharpCode.Decompiler/IL/ILReader.cs:line 724
		//    at ICSharpCode.Decompiler.CSharp.CSharpDecompiler.DecompileBody(IMethod method, EntityDeclaration entityDecl, DecompileRun decompileRun, ITypeResolveContext decompilationContext, ExtensionInfo extensionInfo) in /_/ICSharpCode.Decompiler/CSharp/CSharpDecompiler.cs:line 2282
	}
}
