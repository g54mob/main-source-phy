using System;
using System.Collections.Generic;
using UnityEngine;

namespace SleepyNodes
{
	[CreateAssetMenu(fileName = "ObjectiveGraph_", menuName = "Graphs/Objective Graph")]
	public class ObjectiveGraph : StateGraph
	{
		public enum ObjectiveResults
		{
			Success = 0,
			Failure = 1
		}

		[NonSerialized]
		private ObjectiveEntry _EntryPoint;

		[NonSerialized]
		public bool IsActivated;

		[NonSerialized]
		public MissionGraph ParentGraph;

		[NonSerialized]
		public State_Objective ParentNode;

		public override List<Type> NodeRestriction => null;

		public override List<Type> NodeTypeExludes => null;

		public new ObjectiveEntry EntryPoint => null;

		public void SendNotification(string notifID)
		{
		}

		public void CheckEvents(EventNode.EventData evt)
		{
		}

		public void StartObjective(MissionGraph missionGraph, State_Objective parentNode)
		{
		}

		public virtual void ResetNodes()
		{
		}

		public override void Run()
		{
		}

		public void Finish(ObjectiveResults result)
		{
		}

		public override void Update()
		{
		}
	}
}
