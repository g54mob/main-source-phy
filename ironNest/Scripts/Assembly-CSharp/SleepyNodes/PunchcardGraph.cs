using System;
using System.Collections.Generic;
using UnityEngine;

namespace SleepyNodes
{
	[CreateAssetMenu(fileName = "PunchcardGraph_", menuName = "Graphs/Punchcard Graph")]
	public class PunchcardGraph : StateGraph
	{
		[NonSerialized]
		private State_CardActionStart _EntryPoint;

		public bool IsActivated;

		public override List<Type> NodeRestriction => null;

		public override List<Type> NodeTypeExludes => null;

		public new State_CardActionStart EntryPoint => null;

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
