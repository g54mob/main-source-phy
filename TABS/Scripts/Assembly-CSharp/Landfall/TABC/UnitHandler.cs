using System;
using System.Collections.Generic;
using Landfall.TABS;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABC
{
	public class UnitHandler : MonoBehaviour
	{
		public static UnitHandler instance;

		public GameObject healthBar;

		public List<UnitData> myUnits = new List<UnitData>();

		public Action UpdateUnitsAction;

		internal List<UnitData> myUnitsOnBoard = new List<UnitData>();

		internal List<UnitData> myUnitsOnBench = new List<UnitData>();

		public void UpdateUnits()
		{
			myUnits.Clear();
			myUnitsOnBoard.Clear();
			for (int i = 0; i < BoardManager.instance.m_CurrentBoard.Units.Count; i++)
			{
				if (!BoardManager.instance.m_CurrentBoard.Units[i].unitData.isBeingDestroyed)
				{
					myUnits.Add(BoardManager.instance.m_CurrentBoard.Units[i].unitData);
					myUnitsOnBoard.Add(BoardManager.instance.m_CurrentBoard.Units[i].unitData);
				}
			}
			for (int j = 0; j < BoardManagerUI.instance.buttons.Length; j++)
			{
				UnitData component = BoardManagerUI.instance.buttons[j].GetComponent<UnitData>();
				if (component.dataInstance.unit != null)
				{
					myUnits.Add(component);
					myUnitsOnBench.Add(component);
				}
			}
			UpdateUnitsAction?.Invoke();
			ChoseExcessUnits();
			UnitCombinations.instance.CheckForCombine();
		}

		public void RemoveUnit(UnitData data)
		{
			UnitButton component = data.GetComponent<UnitButton>();
			if (!component)
			{
				BoardManager.instance.RemoveUnit(data.dataInstance.boardPos);
				UnityEngine.Object.Destroy(data.dataInstance.unitObject);
			}
			else
			{
				component.SetUnit(null, isOWned: false);
			}
		}

		public void ChoseExcessUnits()
		{
			if (GameFlowHandlerServer.isDebug)
			{
				return;
			}
			int level = XPHandlerClient.instance.level;
			int num = 0;
			for (int i = 0; i < BoardManager.instance.m_CurrentBoard.Units.Count; i++)
			{
				num++;
				if (num > level)
				{
					BoardManager.instance.m_CurrentBoard.Units[i].unitData.SetExcess(setExcessive: true);
				}
				else
				{
					BoardManager.instance.m_CurrentBoard.Units[i].unitData.SetExcess(setExcessive: false);
				}
			}
		}

		public void CutExcessUnits()
		{
			if (GameFlowHandlerServer.isDebug)
			{
				return;
			}
			for (int num = BoardManager.instance.m_CurrentBoard.Units.Count - 1; num >= 0; num--)
			{
				if (BoardManager.instance.m_CurrentBoard.Units[num].unitData.isExcessive)
				{
					DragHandler.instance.GetOffBoard(BoardManager.instance.m_CurrentBoard.Units[num].unitData);
				}
			}
		}

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			RoundHandler roundHandler = RoundHandler.instance;
			roundHandler.EnterBattleActionLate = (Action)Delegate.Combine(roundHandler.EnterBattleActionLate, new Action(CutExcessUnits));
		}

		public Unit SpawnUnit(UnitBlueprint unitBlueprint, Vector3 pos, bool freezeUnit = true, int level = 1)
		{
			Team team = Team.Red;
			Vector3 vector = Vector3.forward;
			if (pos.z > 0f)
			{
				team = Team.Blue;
				vector = -Vector3.forward;
			}
			GameObject[] array = unitBlueprint.Spawn(pos, quaternion.LookRotation(vector, Vector3.up), team, 1f + ((float)level - 1f) * 0.2f);
			Unit component = array[0].GetComponent<Unit>();
			component.gameObject.AddComponent<SinkOnDeath>().time = 0.5f;
			if (freezeUnit)
			{
				component.FreezeBody();
			}
			component.level = level;
			UnityEngine.Object.Instantiate(healthBar).GetComponent<TABCUnitUI>().Init(component);
			return array[0].GetComponent<Unit>();
		}

		public void RemoveUnit(Unit unit)
		{
			unit.DestroyUnit();
		}
	}
}
