using System;
using SleepyNodes;

[Serializable]
public class FilterEntitySet
{
	[Serializable]
	public class FilterEntityPair
	{
		public enum Operation
		{
			Base = 0,
			And = 1,
			Or = 2
		}

		public Operation operation;

		public FilterEntity FilterEntity;
	}

	public FilterEntityPair[] FilterEntitys;

	public bool Resolve(MapEntity entity, StateNode.NodeExecutionState state)
	{
		return false;
	}
}
