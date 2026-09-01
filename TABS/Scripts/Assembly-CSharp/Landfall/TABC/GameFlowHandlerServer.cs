using System.Collections;
using System.Collections.Generic;
using Landfall.TABS.GameState;
using UnityEngine;

namespace Landfall.TABC
{
	public class GameFlowHandlerServer : MonoBehaviour
	{
		public int round;

		public List<Client> clients = new List<Client>();

		public static bool isDebug = true;

		private void Awake()
		{
			Client item = new Client(Object.FindObjectOfType<GameFlowHandlerClient>(), 0);
			clients.Add(item);
		}

		private void Start()
		{
			ServiceLocator.GetService<GameStateManager>().EnterPlacementState();
			if (isDebug)
			{
				StartCoroutine(DebugGameLoop());
			}
			else
			{
				StartCoroutine(GameLoop());
			}
		}

		private IEnumerator DebugGameLoop()
		{
			while (true)
			{
				StartRound();
				while (!Input.GetKeyDown(KeyCode.P))
				{
					yield return null;
				}
				SetRoundState(RoundHandler.RoundState.Battle);
				SpawnSpecialRoundBattles();
				StartBattles();
				while (!AllClientsReady())
				{
					yield return null;
				}
				BattlesOver();
				GiveRoundMoney();
				RoundHandler.instance.IncrementRound();
			}
		}

		private IEnumerator GameLoop()
		{
			while (true)
			{
				StartRound();
				yield return TimeCounter.Wait(500f);
				int num = RoundHandler.instance.IsItChallangeTime();
				if (num != -1)
				{
					StartSpecialRoundSelection(num);
					yield return TimeCounter.Wait(1f);
					SetRoundState(RoundHandler.RoundState.Battle);
					SpawnSpecialRoundBattles();
				}
				else
				{
					SetRoundState(RoundHandler.RoundState.Battle);
					SpawnBattles();
				}
				StartBattles();
				while (!AllClientsReady())
				{
					yield return null;
				}
				BattlesOver();
				yield return TimeCounter.Wait(3f);
				GiveRoundMoney();
				RoundHandler.instance.IncrementRound();
			}
		}

		private void StartSpecialRoundSelection(int challangeID)
		{
			SetRoundState(RoundHandler.RoundState.PickingChallange);
			GameFlowHandlerClient.instance.ServerToClientStartSpecialRound(challangeID);
		}

		private void SpawnSpecialRoundBattles()
		{
			ChallangeHandlerUI.instance.ForceStopPicking();
			if (isDebug)
			{
				BattleManager.instance.SpawnOpponent(null);
			}
			else
			{
				BattleManager.instance.SpawnOpponent(ChallangeHandlerUI.instance.GetPickedChallange().battle);
			}
		}

		private void SpawnBattles()
		{
		}

		private void StopSpecialRoundSelection()
		{
		}

		public void StartRound()
		{
			SetRoundState(RoundHandler.RoundState.Planning);
			RefreshAllShops();
		}

		public void StartBattles()
		{
			for (int i = 0; i < clients.Count; i++)
			{
				clients[i].isSimulatingBattle = true;
				clients[i].gameFlow.ServerToClientStartBattle();
			}
		}

		public void BattlesOver()
		{
			SetRoundState(RoundHandler.RoundState.PostRound);
		}

		public bool AllClientsReady()
		{
			bool result = true;
			for (int i = 0; i < clients.Count; i++)
			{
				if (clients[i].isSimulatingBattle)
				{
					result = false;
				}
			}
			return result;
		}

		public void RefreshAllShops()
		{
			for (int i = 0; i < clients.Count; i++)
			{
				clients[i].gameFlow.ServerToClientRefreshShop();
			}
		}

		public void GiveRoundMoney()
		{
			for (int i = 0; i < clients.Count; i++)
			{
				clients[i].gameFlow.ServerToClientGiveMoney();
			}
		}

		public void SetRoundState(RoundHandler.RoundState roundState)
		{
			RoundHandler.instance.SetRoundState(roundState);
		}

		public void ClientBattleOver(int playerID)
		{
			clients[playerID].isSimulatingBattle = false;
			PlayerInfoWasUpdated();
		}

		public void SetClientHealth(int playerID, int newHealth)
		{
			clients[playerID].health = newHealth;
			PlayerInfoWasUpdated();
		}

		public void SetClientMoney(int playerID, int newMoney)
		{
			clients[playerID].money = newMoney;
			PlayerInfoWasUpdated();
		}

		public void PlayerInfoWasUpdated()
		{
			for (int i = 0; i < clients.Count; i++)
			{
				clients[0].gameFlow.ServerToCLientUpdatePlayerList(clients);
			}
		}
	}
}
