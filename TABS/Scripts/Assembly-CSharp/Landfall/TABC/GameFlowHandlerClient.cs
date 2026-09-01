using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABC
{
	public class GameFlowHandlerClient : MonoBehaviour
	{
		public static GameFlowHandlerClient instance;

		public GameFlowHandlerServer server;

		private void Awake()
		{
			instance = this;
		}

		public void ServerToClientStartBattle()
		{
			BattleManager.instance.StartBattle();
		}

		public void ServerToClientForceEndBattle()
		{
		}

		public void ServerToClientRefreshShop()
		{
			ShopHandler.instance.Refresh();
		}

		public void ServerToClientStartSpecialRound(int challangeID)
		{
			ChallangeTeir challangeTeir = RoundHandler.instance.specialRounds[challangeID].challangeTeir;
			ChallangeHandlerUI.instance.NewChallange(challangeTeir);
		}

		public void ServerToClientGiveMoney()
		{
			WalletHandlerClient.instance.AddMoney(2);
		}

		public void ServerToCLientUpdatePlayerList(List<Client> playerList)
		{
			PlayerList.instance.UpdatePlayerList(playerList);
		}

		public void ClientToServerBattleOver()
		{
			server.ClientBattleOver(0);
		}

		public void ClientToServerUpdateHealth(int newHealth)
		{
			server.SetClientHealth(0, newHealth);
		}

		public void ClientToServerUpdateMoney(int newMoney)
		{
			server.SetClientMoney(0, newMoney);
		}
	}
}
