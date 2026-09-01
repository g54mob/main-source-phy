using System;
using System.Collections.Generic;
using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Tags;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.AI.Systems
{
	[UpdateBefore(typeof(BeforeUpdate))]
	[AlwaysUpdateSystem]
	public class TeamSystem : ComponentSystem
	{
		private struct GOEntityWrapper
		{
			public GameObject GameObject;

			public Transform Transform;

			public Rigidbody MainRig;

			public DataHandler UnitData;
		}

		private struct Filter
		{
			private EntityArray Entities;

			public ComponentDataArray<UnitTag> _Tags;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private Dictionary<Entity, GOEntityWrapper> Units;

		[Inject]
		private BeforeUpdate m_barrier;

		[Inject]
		private Filter m_filter;

		private Dictionary<Team, List<Unit>> m_teamUnits;

		private Dictionary<Team, List<Unit>> m_winConditionUnits;

		private List<Unit> m_allUnits;

		private Action<Unit> m_onUnitDeadAction;

		public bool OnlyOneTeamAlive { get; private set; }

		protected override void OnCreateManager()
		{
			m_teamUnits = new Dictionary<Team, List<Unit>>();
			m_winConditionUnits = new Dictionary<Team, List<Unit>>();
			m_allUnits = new List<Unit>();
			Units = new Dictionary<Entity, GOEntityWrapper>();
			base.OnCreateManager();
		}

		protected override void OnStartRunning()
		{
			base.OnStartRunning();
			OnlyOneTeamAlive = false;
		}

		protected override void OnStopRunning()
		{
			RemoveAll();
			base.OnStopRunning();
		}

		public void AddOnUnitDeadListener(Action<Unit> listener)
		{
			m_onUnitDeadAction = (Action<Unit>)Delegate.Combine(m_onUnitDeadAction, listener);
		}

		public void RemoveOnUnitDeadListener(Action<Unit> listener)
		{
			m_onUnitDeadAction = (Action<Unit>)Delegate.Remove(m_onUnitDeadAction, listener);
		}

		public void TriggerUnitDeathAction(Unit unit)
		{
			GameObjectEntity component = unit.GetComponent<GameObjectEntity>();
			if (component == null)
			{
				Debug.LogError(string.Concat(unit, " Has no GameObjectEntity"));
			}
			else if (component.EntityManager == null)
			{
				Debug.LogError(string.Concat(unit, " is missing an EntityManager. If this is the VampireBat ignore!"));
			}
			else if (component.EntityManager.HasComponent<UnitTag>(component.Entity))
			{
				component.EntityManager.RemoveComponent<UnitTag>(component.Entity);
				RemoveEntity(component.Entity, unit.Team, unit);
				bool flag = false;
				bool flag2 = false;
				if (m_winConditionUnits.ContainsKey(Team.Red))
				{
					flag = ((m_winConditionUnits[Team.Red].Count != 0) ? true : false);
				}
				if (m_winConditionUnits.ContainsKey(Team.Blue))
				{
					flag2 = ((m_winConditionUnits[Team.Blue].Count != 0) ? true : false);
				}
				if (flag != flag2)
				{
					OnlyOneTeamAlive = true;
				}
				else
				{
					OnlyOneTeamAlive = false;
				}
				m_onUnitDeadAction?.Invoke(unit);
			}
		}

		public void AddUnit(Entity entity, GameObject go, Transform transform, Rigidbody mainRig, DataHandler unitData, Team team, Unit unit, bool excludeFromWinCondition)
		{
			Units.Add(entity, new GOEntityWrapper
			{
				GameObject = go,
				Transform = transform,
				MainRig = mainRig,
				UnitData = unitData
			});
			AddUnitToTeam(unit, team, excludeFromWinCondition);
		}

		private void AddUnitToTeam(Unit unit, Team team, bool excludeFromWinCondition)
		{
			if (!m_teamUnits.ContainsKey(team))
			{
				m_teamUnits.Add(team, new List<Unit>());
			}
			m_teamUnits[team].Add(unit);
			m_allUnits.Add(unit);
			if (!excludeFromWinCondition)
			{
				if (!m_winConditionUnits.ContainsKey(team))
				{
					m_winConditionUnits.Add(team, new List<Unit>());
				}
				m_winConditionUnits[team].Add(unit);
			}
		}

		private void RemoveUnitFromTeam(Unit unit, Team team)
		{
			m_allUnits.Remove(unit);
			if (m_teamUnits.ContainsKey(team))
			{
				m_teamUnits[team].Remove(unit);
			}
			if (m_winConditionUnits.ContainsKey(team))
			{
				m_winConditionUnits[team].Remove(unit);
			}
		}

		public void RemoveEntity(Entity entity, Team team, Unit unit)
		{
			Units.Remove(entity);
			if (unit != null)
			{
				RemoveUnitFromTeam(unit, team);
			}
		}

		public Rigidbody GetMainRig(Entity entity)
		{
			if (!Units.ContainsKey(entity))
			{
				return null;
			}
			return Units[entity].MainRig;
		}

		public DataHandler GetDataHandler(Entity entity)
		{
			if (!Units.ContainsKey(entity))
			{
				return null;
			}
			return Units[entity].UnitData;
		}

		public void RemoveAll()
		{
			Units.Clear();
			m_teamUnits.Clear();
			m_winConditionUnits.Clear();
			m_allUnits.Clear();
		}

		public List<Unit> GetTeamUnits(Team team)
		{
			if (!m_teamUnits.ContainsKey(team))
			{
				return new List<Unit>();
			}
			return m_teamUnits[team];
		}

		public List<Unit> GetWinConditionTeamUnits(Team team)
		{
			if (!m_winConditionUnits.ContainsKey(team))
			{
				return null;
			}
			return m_winConditionUnits[team];
		}

		public List<Unit> GetAllUnits()
		{
			return m_allUnits;
		}

		protected override void OnUpdate()
		{
		}
	}
}
