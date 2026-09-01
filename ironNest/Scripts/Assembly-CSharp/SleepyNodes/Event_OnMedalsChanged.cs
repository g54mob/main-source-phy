namespace SleepyNodes
{
	[NodeWidth(400)]
	[NodeName("[Event] Medals Changed (Deprecated)")]
	public class Event_OnMedalsChanged : EventNode
	{
		public bool FilterByMedalID;

		public string MedalID;

		public bool FilterByChange;

		public EventData_MedalsChanged.Changes Change;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}
	}
}
