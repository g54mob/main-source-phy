using System;
using System.Collections.Generic;
using UnityEngine;

namespace SleepyNodes
{
	[CreateAssetMenu(fileName = "OperationGraph_", menuName = "Graphs/Operation Graph")]
	public class OperationGraph : StateGraph
	{
		[Header("Identity")]
		public string OperationID;

		[Tooltip("Human-readable name shown in UI (e.g., Operation selection). Keep it short and unique.\nExamples: 'Operation Dawn', 'Test Operation A'.")]
		public string displayName;

		[Tooltip("Optional description for UI/tooling. Kept minimal to avoid localization overhead.\nExample: 'Introductory set of missions to learn the basics.'")]
		[TextArea(2, 4)]
		public string description;

		public override List<Type> NodeRestriction => null;

		public override List<Type> NodeTypeExludes => null;

		public List<MissionNode> Missions => null;
	}
}
