using System.Collections;
using System.Collections.Generic;
using Landfall.TABS.AI.Systems;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS
{
	public class CastleFightGamemode : InstancedHandler<CastleFightGamemode>
	{
		public float deathGoldBackRate = 0.05f;

		public float houseIncomeBaseFactor = 0.05f;

		public float houseIncomeExponenet = 1.2f;

		public int maxHouses = 10;

		public float redMoney;

		public float blueMoney;

		public float redGpm = 160f;

		public float blueGpm = 160f;

		public int blueHouses;

		public int redHouses;

		public int BluePoints;

		public int RedPoints;

		public int PointsToWin = 3;

		private TeamSystem m_teamSystem;

		private void Start()
		{
			m_teamSystem = World.Active.GetExistingManager<TeamSystem>();
			StartCoroutine(BorderLoop());
			StartCoroutine(MoneyLoop());
			CastleFightGPMText.SetGPM(redGpm, Team.Red);
			CastleFightGPMText.SetGPM(blueGpm, Team.Blue);
			CastleFightGoldText.SetGold(InstancedHandler<CastleFightGamemode>.Instance.redMoney, Team.Red);
			CastleFightGoldText.SetGold(InstancedHandler<CastleFightGamemode>.Instance.blueMoney, Team.Blue);
		}

		private void OnUnitDeath(Unit unit)
		{
			Team team = Team.Red;
			if (unit.Team == Team.Red)
			{
				team = Team.Blue;
			}
			AddMoney(deathGoldBackRate * (float)unit.unitBlueprint.GetUnitCost(), team);
		}

		private IEnumerator MoneyLoop()
		{
			Debug.Log("Money loop start!");
			while (true)
			{
				Debug.Log("Money loop!");
				AddMoney(redGpm / 60f, Team.Red);
				AddMoney(blueGpm / 60f, Team.Blue);
				yield return new WaitForSeconds(1f);
			}
		}

		public static bool BuyHouse(float cost, Team team)
		{
			return InstancedHandler<CastleFightGamemode>.Instance.InternalBoughtHouse(cost, team);
		}

		public static void AddIncome(float cost, Team team, bool negative = false)
		{
			InstancedHandler<CastleFightGamemode>.Instance.AddIncomeInternal(cost, team, negative);
		}

		private bool InternalBoughtHouse(float cost, Team team)
		{
			if (team == Team.Red && redHouses < maxHouses)
			{
				redHouses++;
			}
			else
			{
				if (team != Team.Blue || blueHouses >= maxHouses)
				{
					return false;
				}
				blueHouses++;
			}
			AddIncomeInternal(cost, team, negative: false);
			return true;
		}

		private bool AddIncomeInternal(float cost, Team team, bool negative)
		{
			switch (team)
			{
			case Team.Red:
				if (negative)
				{
					redGpm -= Mathf.Pow(cost * houseIncomeBaseFactor, houseIncomeExponenet);
				}
				else
				{
					redGpm += Mathf.Pow(cost * houseIncomeBaseFactor, houseIncomeExponenet);
				}
				break;
			case Team.Blue:
				if (negative)
				{
					blueGpm -= Mathf.Pow(cost * houseIncomeBaseFactor, houseIncomeExponenet);
				}
				else
				{
					blueGpm += Mathf.Pow(cost * houseIncomeBaseFactor, houseIncomeExponenet);
				}
				break;
			default:
				return false;
			}
			CastleFightGPMText.SetGPM(InstancedHandler<CastleFightGamemode>.Instance.redGpm, Team.Red);
			CastleFightGPMText.SetGPM(InstancedHandler<CastleFightGamemode>.Instance.blueGpm, Team.Blue);
			return true;
		}

		public static void SellHouse(UnitBlueprint unit, Team team, float totalSpentOnBuilding)
		{
			if (team == Team.Red)
			{
				InstancedHandler<CastleFightGamemode>.Instance.redHouses--;
			}
			else
			{
				InstancedHandler<CastleFightGamemode>.Instance.blueHouses--;
			}
			AddMoney(totalSpentOnBuilding / 2f, team);
		}

		public static void AddMoney(float money, Team team)
		{
			if (team == Team.Red)
			{
				InstancedHandler<CastleFightGamemode>.Instance.redMoney += money;
				CastleFightGoldText.SetGold(InstancedHandler<CastleFightGamemode>.Instance.redMoney, team);
			}
			else
			{
				InstancedHandler<CastleFightGamemode>.Instance.blueMoney += money;
				CastleFightGoldText.SetGold(InstancedHandler<CastleFightGamemode>.Instance.blueMoney, team);
			}
		}

		public static void RemoveMoney(float money, Team team)
		{
			if (team == Team.Red)
			{
				InstancedHandler<CastleFightGamemode>.Instance.redMoney -= money;
				CastleFightGoldText.SetGold(InstancedHandler<CastleFightGamemode>.Instance.redMoney, team);
			}
			else
			{
				InstancedHandler<CastleFightGamemode>.Instance.blueMoney -= money;
				CastleFightGoldText.SetGold(InstancedHandler<CastleFightGamemode>.Instance.blueMoney, team);
			}
		}

		public static float GetMoney(Team team)
		{
			if (team == Team.Red)
			{
				return InstancedHandler<CastleFightGamemode>.Instance.redMoney;
			}
			return InstancedHandler<CastleFightGamemode>.Instance.blueMoney;
		}

		private IEnumerator BorderLoop()
		{
			float timestep = 0.25f;
			while (true)
			{
				yield return new WaitForSeconds(timestep);
				List<Unit> teamUnits = m_teamSystem.GetTeamUnits(Team.Red);
				List<Unit> teamUnits2 = m_teamSystem.GetTeamUnits(Team.Blue);
				CheckUnits(teamUnits, Team.Red);
				CheckUnits(teamUnits2, Team.Blue);
			}
		}

		private void CheckUnits(List<Unit> units, Team team)
		{
			for (int i = 0; i < units.Count; i++)
			{
				Transform transform = units[i].Hip.transform;
				Transform transform2 = null;
				transform2 = ((team != Team.Red) ? CastleFightLine.RedLine : CastleFightLine.BlueLine);
				if (Vector3.Dot(transform2.position - transform.position, transform2.forward) > 0f)
				{
					Score((team != Team.Blue) ? Team.Blue : Team.Red);
				}
			}
		}

		private void Score(Team team)
		{
			if (team == Team.Blue)
			{
				BluePoints++;
			}
			else
			{
				RedPoints++;
			}
			CastleFightUI.instance.Score(BluePoints, RedPoints);
			ClearAllUnits();
			if (BluePoints >= PointsToWin)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			if (RedPoints >= PointsToWin)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		private void ClearAllUnits()
		{
			List<Unit> teamUnits = m_teamSystem.GetTeamUnits(Team.Red);
			for (int i = 0; i < teamUnits.Count; i++)
			{
				teamUnits[i].holdingHandler.LetGoOfAll();
				Object.Destroy(teamUnits[i].gameObject);
			}
			teamUnits = m_teamSystem.GetTeamUnits(Team.Blue);
			for (int j = 0; j < teamUnits.Count; j++)
			{
				teamUnits[j].holdingHandler.LetGoOfAll();
				Object.Destroy(teamUnits[j].gameObject);
			}
		}
	}
}
