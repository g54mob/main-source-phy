using ArticleSystem;
using Cpp2ILInjected;

namespace SleepyNodes;

public class State_QueueArticle : StateNode
{
	public StateNode To;

	public ArticlePoolDefinition Article;

	public int Amount;

	public string Note;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0099: Expected I, but got O
		//IL_00a9: Expected O, but got I
		//IL_00b9: Expected O, but got I
		while (true)
		{
			base.OnEnter(state);
			if (Article != null)
			{
				ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
				int count = Amount;
				if (Amount < 1)
				{
					count = 1;
				}
				instance.EnqueuePool(Article, count, Note);
			}
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r9_v2 (Il2CppClass<SleepyNodes.State_QueueArticle>)+218]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r9_v2 (Il2CppClass<SleepyNodes.State_QueueArticle>)+220]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v120 @ rax_v6 (should have been resolved before IL gen)");
		}
	}
}
