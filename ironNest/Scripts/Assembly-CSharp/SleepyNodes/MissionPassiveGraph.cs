using System;
using System.Collections.Generic;
using UnityEngine;

namespace SleepyNodes
{
	[CreateAssetMenu(fileName = "PassiveGraph_", menuName = "Graphs/Passive Graph")]
	public class MissionPassiveGraph : StateGraph
	{
		[NonSerialized]
		private State_Start _EntryPoint;

		[NonSerialized]
		public MissionGraph ParentGraph;

		public override List<Type> NodeRestriction => null;

		public override List<Type> NodeTypeExludes => null;

		public new State_Start EntryPoint => null;

		public void SendNotification(string notifID)
		{
		}

		public void CheckEvents(EventNode.EventData evt)
		{
		}

		public void OnMissionStart(MissionGraph missionGraph)
		{
		}

		public virtual void ResetNodes()
		{
		}

		public override void Run()
		{
		}

		public override void Update()
		{
		}
	}
}
