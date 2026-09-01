using System;
using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.GameState;
using UnityEngine;

namespace Landfall.TABC
{
	public class BattleManager : MonoBehaviour
	{
		public static BattleManager instance;

		private bool isBattling;

		private Unit[] spawnedEnemies;

		public List<Unit> battlingUnits = new List<Unit>();

		private List<Unit> myUnits = new List<Unit>();

		public Action<bool> BattleOverAction { get; internal set; }

		private void Awake()
		{
			instance = this;
		}

		private void Update()
		{
			if (isBattling)
			{
				CheckIfBattleOver();
			}
		}

		private void CheckIfBattleOver()
		{
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < spawnedEnemies.Length; i++)
			{
				if (spawnedEnemies[i] == null)
				{
					Debug.LogError("A enemy unit is null. This should not be the case");
				}
				else if (!spawnedEnemies[i].dead)
				{
					flag = true;
				}
			}
			for (int j = 0; j < myUnits.Count; j++)
			{
				if (myUnits[j] == null)
				{
					Debug.LogError("A friendly unit is null. This should not be the case");
				}
				else if (!myUnits[j].dead)
				{
					flag2 = true;
				}
			}
			if (!flag2 || !flag)
			{
				Team winnerTeam = ((!flag2) ? Team.Blue : Team.Red);
				BattleOver(winnerTeam);
			}
		}

		public void SpawnOpponent(BattleLayout battleLayout)
		{
			spawnedEnemies = null;
			Board currentBoard = BoardManager.instance.m_CurrentBoard;
			battlingUnits.Clear();
			if (GameFlowHandlerServer.isDebug)
			{
				List<Unit> list = new List<Unit>();
				for (int i = 0; i < currentBoard.Units.Count; i++)
				{
					for (int j = 0; j < currentBoard.Units[i].unitData.spawnedUnits.Length; j++)
					{
						if (currentBoard.Units[i].pos.y > 0)
						{
							list.Add(currentBoard.Units[i].unitData.spawnedUnits[j]);
							battlingUnits.Add(currentBoard.Units[i].unitData.spawnedUnits[j]);
						}
					}
				}
				spawnedEnemies = list.ToArray();
			}
			else
			{
				spawnedEnemies = new Unit[battleLayout.units.Length];
				for (int k = 0; k < battleLayout.units.Length; k++)
				{
					spawnedEnemies[k] = UnitHandler.instance.SpawnUnit(battleLayout.units[k].unit, BoardData.instance.BoardPosToWorld(battleLayout.units[k].pos));
				}
			}
			myUnits = new List<Unit>();
			for (int l = 0; l < currentBoard.Units.Count; l++)
			{
				for (int m = 0; m < currentBoard.Units[l].unitData.spawnedUnits.Length; m++)
				{
					if (!GameFlowHandlerServer.isDebug || currentBoard.Units[l].pos.y <= 0)
					{
						myUnits.Add(currentBoard.Units[l].unitData.spawnedUnits[m]);
						battlingUnits.Add(currentBoard.Units[l].unitData.spawnedUnits[m]);
					}
				}
			}
		}

		public void StartBattle()
		{
			isBattling = true;
			ServiceLocator.GetService<GameStateManager>().EnterBattleState();
			for (int i = 0; i < spawnedEnemies.Length; i++)
			{
				spawnedEnemies[i].UnfreezeBody();
			}
			for (int j = 0; j < myUnits.Count; j++)
			{
				myUnits[j].UnfreezeBody();
			}
		}

		public void BattleOver(Team winnerTeam)
		{
			BattleOverAction?.Invoke(winnerTeam == Team.Red);
			isBattling = false;
			GameFlowHandlerClient.instance.ClientToServerBattleOver();
			StartCoroutine(DelayBattleEnd());
		}

		private IEnumerator DelayBattleEnd()
		{
			yield return new WaitForSeconds(3f);
			ServiceLocator.GetService<GameStateManager>().EnterPlacementState();
			BoardManager.instance.RespawnUnits();
		}
	}
}
