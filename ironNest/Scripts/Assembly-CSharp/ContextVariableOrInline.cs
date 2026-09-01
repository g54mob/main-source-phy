using System;
using SleepyNodes;

[Serializable]
public class ContextVariableOrInline<T>
{
	public enum SelectionTypes
	{
		Inline = 0,
		Context = 1
	}

	public SelectionTypes SelectionType;

	public T Value;

	public string ContextKey;

	public T Get(StateNode.NodeExecutionState state)
	{
		return default(T);
	}
}
