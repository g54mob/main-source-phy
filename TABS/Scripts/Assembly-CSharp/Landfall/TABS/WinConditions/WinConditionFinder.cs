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
	public class WinConditionFinder
	{
		private Dictionary<string, WinConditionEntry> m_winConditionEntries = new Dictionary<string, WinConditionEntry>();

		private BaseGameMode m_gameMode;

		public WinConditionFinder(BaseGameMode gameMode)
		{
			m_gameMode = gameMode;
			FindWinConditions();
		}

		private void FindWinConditions()
		{
			try
			{
				foreach (Type item in from assembly in AppDomain.CurrentDomain.GetAssemblies()
					from type in assembly.GetTypes()
					where type.IsSubclassOf(typeof(WinCondition))
					select type)
				{
					WinConditionIDAttribute customAttribute = item.GetCustomAttribute<WinConditionIDAttribute>();
					if (customAttribute == null)
					{
						Debug.LogError("Could not prepare WinCondition: " + item.FullName + ". Missing WinConditionIDAttribute!");
						continue;
					}
					m_winConditionEntries.Add(item.Name, new WinConditionEntry
					{
						DisplayName = customAttribute.DisplayName,
						WinConditionType = item
					});
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				throw;
			}
		}

		public List<string> GetIdentifiers()
		{
			return m_winConditionEntries.Keys.ToList();
		}

		public string GetDisplayName(string identifier)
		{
			if (!HasIdentifier(identifier))
			{
				return string.Empty;
			}
			return m_winConditionEntries[identifier].DisplayName;
		}

		public string GetDescription(string identifier, out string[] args)
		{
			if (!HasIdentifier(identifier))
			{
				args = null;
				return string.Empty;
			}
			return ((WinCondition)Activator.CreateInstance(m_winConditionEntries[identifier].WinConditionType)).GetDescription(out args);
		}

		public Type GetConditionType(string identifier)
		{
			if (!HasIdentifier(identifier))
			{
				return null;
			}
			return m_winConditionEntries[identifier].WinConditionType;
		}

		public WinCondition CreateWinCondition(string identifier)
		{
			if (!HasIdentifier(identifier))
			{
				return null;
			}
			WinCondition obj = (WinCondition)Activator.CreateInstance(GetConditionType(identifier));
			obj.GameMode = m_gameMode;
			obj.TeamSystem = World.Active.GetOrCreateManager<TeamSystem>();
			obj.Guid = Guid.NewGuid();
			return obj;
		}

		private bool HasIdentifier(string identifier)
		{
			if (!m_winConditionEntries.ContainsKey(identifier))
			{
				Debug.LogError("WinCondition with identifier: " + identifier + " could not be found.");
				return false;
			}
			return true;
		}
	}
}
