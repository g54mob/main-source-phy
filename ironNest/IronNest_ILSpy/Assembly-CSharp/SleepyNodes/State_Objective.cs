using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_Objective : StateNode
{
	public StateNode To;

	public ObjectiveGraph Objective;

	public StateNode OnSuccess;

	public StateNode OnFailure;

	[NonSerialized]
	public List<ObjectiveGraph> Running;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_000d: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0067: Expected O, but got I
		base.OnEnter(state);
		MissionGraph missionGraph = (MissionGraph)graph;
		if ((object)graph != null)
		{
			nint num = (nint)missionGraph;
			nint num2 = (nint)typeof(MissionGraph);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rdx_v4 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v4 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rdx_v4 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v4 (Il2CppClass<SleepyNodes.MissionGraph>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ rax_v8+FFFFFFF8+v68 @ rax_v7*8]");
				if (0 == (nint)typeof(MissionGraph))
				{
					ObjectiveGraph objectiveGraph = UnityEngine.Object.Instantiate(Objective);
					if ((object)objectiveGraph != null && objectiveGraph.nodes != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						List<Node>.Enumerator enumerator = default(List<Node>.Enumerator);
						ObjectiveGraph objectiveGraph2 = default(ObjectiveGraph);
						while (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if ((object)objectiveGraph2 != null)
							{
								objectiveGraph2.nodes = (List<Node>)(object)objectiveGraph;
								continue;
							}
							throw new NullReferenceException();
						}
						enumerator.Dispose();
						if (Running != null)
						{
							Running.Add(objectiveGraph);
							objectiveGraph.StartObjective((MissionGraph)graph, this);
							goto IL_0163;
						}
					}
					throw new NullReferenceException();
				}
			}
		}
		goto IL_0163;
		IL_0163:
		base.OnExit(state, "To");
	}

	public void SendNotification(string notifID)
	{
		//IL_005f: Expected O, but got I4
		//IL_0068: Expected O, but got I4
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		List<ObjectiveGraph> running = Running;
		object obj = 0;
		object obj2 = 0;
		ObjectiveGraph objectiveGraph = default(ObjectiveGraph);
		while ((nint)obj2 < running._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			objectiveGraph.SendNotification(notifID);
			running = Running;
			obj++;
			obj2 = obj;
		}
	}

	public void CheckEvents(EventNode.EventData data)
	{
		//IL_005f: Expected O, but got I4
		//IL_0068: Expected O, but got I4
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		List<ObjectiveGraph> running = Running;
		object obj = 0;
		object obj2 = 0;
		ObjectiveGraph objectiveGraph = default(ObjectiveGraph);
		while ((nint)obj2 < running._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			objectiveGraph.CheckEvents(data);
			running = Running;
			obj++;
			obj2 = obj;
		}
	}

	public void UpdateObjectives()
	{
		//IL_0088: Expected O, but got I4
		//IL_0091: Expected O, but got I4
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		List<ObjectiveGraph> running = Running;
		object obj = 0;
		object obj2 = 0;
		object obj4 = default(object);
		while ((nint)obj2 < running._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj3 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v155 @ rdx_v4+208] (should have been resolved before IL gen)");
			running = Running;
			obj++;
			obj2 = obj;
		}
	}

	public void OnResult(ObjectiveGraph child, ObjectiveGraph.ObjectiveResults results)
	{
		string fieldName;
		if (results == ObjectiveGraph.ObjectiveResults.Success)
		{
			fieldName = "OnSuccess";
		}
		else
		{
			if (results != ObjectiveGraph.ObjectiveResults.Failure)
			{
				goto IL_0088;
			}
			fieldName = "OnFailure";
		}
		StateNode connectedNode = GetConnectedNode<StateNode>(fieldName);
		if (connectedNode != null)
		{
			NodeExecutionState newState = NodeExecutionState.NewState;
			connectedNode.OnEnter(newState);
		}
		goto IL_0088;
		IL_0088:
		bool flag = Running.Remove(child);
	}

	public State_Objective()
	{
		List<ObjectiveGraph> running = new List<ObjectiveGraph>();
		Running = running;
		base._002Ector();
	}
}
