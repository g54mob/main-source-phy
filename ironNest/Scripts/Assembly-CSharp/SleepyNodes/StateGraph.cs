using System;
using System.Collections.Generic;
using UnityEngine;

namespace SleepyNodes
{
	[CreateAssetMenu(fileName = "new StateGraph", menuName = "Graphs/StateGraph")]
	public class StateGraph : NodeGraph
	{
		[NonSerialized]
		private StateNodeEntry _EntryPoint;

		[NonSerialized]
		private List<EventNode> _EventNodes;

		[NonSerialized]
		public StateNode.NodeExecutionState CurrentState;

		[NonSerialized]
		public Dictionary<string, StateNode.NodeExecutionState> SideExecutionPaths;

		public Dictionary<string, object> Variables;

		public override List<Type> NodeRestriction => null;

		public StateNodeEntry EntryPoint => null;

		public List<EventNode> EventNodes => null;

		public bool TryGetVariable<T>(string variableName, out T variable)
		{
			variable = default(T);
			return false;
		}

		public void SetVariable(string variableName, object obj)
		{
		}

		public virtual void Run()
		{
		}

		public virtual void Update()
		{
		}
	}
}
