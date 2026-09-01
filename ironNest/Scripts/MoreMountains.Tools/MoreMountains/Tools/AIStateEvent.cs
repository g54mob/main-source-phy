namespace MoreMountains.Tools
{
	public struct AIStateEvent
	{
		public AIBrain Brain;

		public AIState ExitState;

		public AIState EnterState;

		private static AIStateEvent e;

		public AIStateEvent(AIBrain brain, AIState exitState, AIState enterState)
		{
			Brain = null;
			ExitState = null;
			EnterState = null;
		}

		public static void Trigger(AIBrain brain, AIState exitState, AIState enterState)
		{
		}
	}
}
