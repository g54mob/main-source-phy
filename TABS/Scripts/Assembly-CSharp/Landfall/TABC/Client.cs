using System;

namespace Landfall.TABC
{
	[Serializable]
	public class Client
	{
		public int playerID;

		public int money;

		public int health = 100;

		public bool isSimulatingBattle;

		public GameFlowHandlerClient gameFlow;

		public Client(GameFlowHandlerClient gameFlow, int playerID)
		{
			this.gameFlow = gameFlow;
			this.playerID = playerID;
		}
	}
}
