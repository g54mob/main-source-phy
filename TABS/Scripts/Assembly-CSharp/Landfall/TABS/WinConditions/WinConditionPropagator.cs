using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	public class WinConditionPropagator
	{
		public delegate void OnInjectedWinConditionsDelegate();

		private Dictionary<string, WinConditionEntry> m_winConditionEntries = new Dictionary<string, WinConditionEntry>();

		private Dictionary<Team, WinConditionEvaluator> m_evaluators = new Dictionary<Team, WinConditionEvaluator>();

		private List<Team> m_allTeams = new List<Team>();

		private WinConditionFinder m_winConditionFinder;

		private BaseGameMode m_currentGameMode;

		public WinConditionFinder WinConditionFinder => m_winConditionFinder;

		public OnInjectedWinConditionsDelegate OnInjectedWinConditionsCallback { get; set; }

		public WinConditionPropagator(BaseGameMode gameMode)
		{
			m_currentGameMode = gameMode;
			m_winConditionFinder = new WinConditionFinder(gameMode);
			foreach (Team item in Enum.GetValues(typeof(Team)).Cast<Team>())
			{
				WinConditionEvaluator value = new WinConditionEvaluator(item);
				m_evaluators.Add(item, value);
				m_allTeams.Add(item);
			}
		}

		public void InjectWinConditionEvaluators(SerializedEvaluator[] serializedEvaluators)
		{
			m_evaluators.Clear();
			for (int i = 0; i < serializedEvaluators.Length; i++)
			{
				WinConditionEvaluator winConditionEvaluator = serializedEvaluators[i].ToEvaluator();
				m_evaluators.Add(winConditionEvaluator.OwningTeam, winConditionEvaluator);
			}
			TeamSystem orCreateManager = World.Active.GetOrCreateManager<TeamSystem>();
			foreach (KeyValuePair<Team, WinConditionEvaluator> evaluator in m_evaluators)
			{
				WinCondition[] winConditions = evaluator.Value.GetWinConditions();
				foreach (WinCondition obj in winConditions)
				{
					obj.GameMode = m_currentGameMode;
					obj.TeamSystem = orCreateManager;
				}
			}
			OnInjectedWinConditionsCallback?.Invoke();
		}

		public void InjectDefaultWinConditionsForAllTeams()
		{
			foreach (Team item in Enum.GetValues(typeof(Team)).Cast<Team>())
			{
				WinConditionEvaluator winConditionEvaluator = m_evaluators[item];
				WinCondition condition = m_winConditionFinder.CreateWinCondition(typeof(LastTeamStandingWinCondition).Name);
				winConditionEvaluator.AddWinCondition(condition);
			}
			OnInjectedWinConditionsCallback?.Invoke();
		}

		public List<FieldInfo> GetFields(WinCondition winCondition)
		{
			Type type = winCondition.GetType();
			List<FieldInfo> list = new List<FieldInfo>();
			foreach (FieldInfo item in from f in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where f.IsPublic || f.GetCustomAttribute<SerializeField>() != null
				select f)
			{
				list.Add(item);
			}
			return list;
		}

		public void AddWinCondition(Team team, WinCondition winCondition)
		{
			m_evaluators[team].AddWinCondition(winCondition);
			winCondition.OwningTeam = team;
			winCondition.GameMode = m_currentGameMode;
			winCondition.TeamSystem = World.Active.GetOrCreateManager<TeamSystem>();
		}

		public void AddWinConditionForAllTeams(WinCondition winCondition)
		{
			foreach (Team allTeam in m_allTeams)
			{
				AddWinCondition(allTeam, winCondition);
			}
		}

		public void RemoveWinCondition(Team team, WinCondition winCondition)
		{
			m_evaluators[team].RemoveWinCondition(winCondition);
			if (m_evaluators[team].GetWinConditions().Length == 0)
			{
				WinCondition condition = m_winConditionFinder.CreateWinCondition(typeof(LastTeamStandingWinCondition).Name);
				m_evaluators[team].AddWinCondition(condition);
			}
		}

		public void RemoveWinCondition(Team team, Guid winConditionGuid)
		{
			m_evaluators[team].RemoveWinCondition(winConditionGuid);
		}

		public void RemoveWinConditionForAllTeams(WinCondition winCondition)
		{
			foreach (Team allTeam in m_allTeams)
			{
				m_evaluators[allTeam].RemoveWinCondition(winCondition);
			}
		}

		public void ClearWinConditionsForTeam(Team team)
		{
			m_evaluators[team].ClearWinConditions();
		}

		public void ClearAllWinConditions()
		{
			foreach (Team allTeam in m_allTeams)
			{
				m_evaluators[allTeam].ClearWinConditions();
			}
			if (m_currentGameMode is SandboxGameMode)
			{
				InjectDefaultWinConditionsForAllTeams();
			}
		}

		public WinCondition GetWinConditionFromTeam(Team team, Guid guid)
		{
			return m_evaluators[team].GetWinCondition(guid);
		}

		public WinCondition[] GetWinConditionsForTeam(Team team)
		{
			return m_evaluators[team].GetWinConditions();
		}

		public SerializedWinCondition[] GetSerializedWinConditionsForTeam(Team team)
		{
			return m_evaluators[team].GetSerializedWinConditions();
		}

		public List<SerializedEvaluator> GetSerializedWinEvaluators()
		{
			List<SerializedEvaluator> list = new List<SerializedEvaluator>();
			foreach (Team allTeam in m_allTeams)
			{
				list.Add(new SerializedEvaluator(m_evaluators[allTeam]));
			}
			return list;
		}

		public void OnUnitDied(Unit unit)
		{
			foreach (Team allTeam in m_allTeams)
			{
				m_evaluators[allTeam].OnUnitDied(unit);
			}
		}

		public bool IsUnitMustKill(Unit unit, out WinCondition mustKillUnitWinCondition)
		{
			if (unit.RuntimeReference == null)
			{
				mustKillUnitWinCondition = null;
				return false;
			}
			foreach (Team allTeam in m_allTeams)
			{
				if (m_evaluators[allTeam].IsUnitMustKill(unit, out var mustKillUnitWinCondition2))
				{
					mustKillUnitWinCondition = mustKillUnitWinCondition2;
					return true;
				}
			}
			mustKillUnitWinCondition = null;
			return false;
		}

		public void OnUpdate()
		{
			foreach (Team allTeam in m_allTeams)
			{
				m_evaluators[allTeam].OnUpdate();
			}
		}

		public void OnReset()
		{
			foreach (Team allTeam in m_allTeams)
			{
				m_evaluators[allTeam].OnResetConditions();
			}
		}

		public void OnMatchOVer()
		{
			foreach (Team allTeam in m_allTeams)
			{
				m_evaluators[allTeam].OnMatchOver();
			}
		}
	}
}
