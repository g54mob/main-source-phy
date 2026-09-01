using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DM;
using Landfall.TABS.Workshop;
using ModIO;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS
{
	[CreateAssetMenu(fileName = "Faction", menuName = "TABS/Faction", order = 1)]
	public class Faction : ScriptableObject, IDatabaseEntity
	{
		public enum FactionCatagory
		{
			Landfall = 0,
			Custom = 1,
			Dev = 2
		}

		[SerializeField]
		private DatabaseEntity m_entity;

		public bool m_displayFaction = true;

		public int index = 999;

		[SerializeField]
		private bool m_isSecret;

		[SerializeField]
		private bool m_lockSecrets;

		[SerializeField]
		private UnitBlueprint[] m_units;

		[Tooltip("Enabling this sorts the faction units by their cost in the placement UI and Battle Radial Menu.")]
		[SerializeField]
		private bool m_sortByCost;

		public Color m_FactionColor;

		public FactionCatagory m_FactionCatagory;

		public int modID;

		[Tooltip("Should this Faction skip updating its assigned UnitBluePrints according to the Landfall Unit Database validation logic. In other words, if skip is true, it will use the UnitBlueprints manually assigned to the asset object.")]
		[SerializeField]
		private bool skipValidatingAssetAgainstUnitBluePrintList;

		private CustomFactionColorDatabase.CustomFactionColor customFactionColor;

		private FactionIcon customFactionIcon;

		public DatabaseEntity Entity => m_entity;

		public bool IsSecret => m_isSecret;

		public bool LockSecrets => m_lockSecrets;

		public UnitBlueprint[] Units
		{
			get
			{
				return m_units;
			}
			set
			{
				m_units = value;
			}
		}

		public bool IsModFaction => modID != 0;

		public bool SkipValidatingAssetAgainstUnitBluePrintList => skipValidatingAssetAgainstUnitBluePrintList;

		public FactionIcon FactionIcon
		{
			get
			{
				return customFactionIcon;
			}
			set
			{
				customFactionIcon = value;
				if (customFactionIcon == null || customFactionIcon.Entity == null)
				{
					return;
				}
				customFactionIcon.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
				{
					if (sprite != null && Entity != null)
					{
						Entity.SetSpriteIcon(sprite);
					}
				});
			}
		}

		public CustomFactionColorDatabase.CustomFactionColor CustomFactionColor
		{
			get
			{
				if (customFactionColor == null)
				{
					customFactionColor = ContentDatabase.Instance().GetCustomFactionColorDatabase().CustomFacionColors[0];
				}
				return customFactionColor;
			}
			set
			{
				customFactionColor = value;
				if (value == null)
				{
					customFactionColor = ContentDatabase.Instance().GetCustomFactionColorDatabase().CustomFacionColors[0];
				}
				m_FactionColor = customFactionColor.m_Color;
			}
		}

		public bool IsCustom { get; set; }

		public void Init()
		{
			m_entity = new DatabaseEntity(WorkshopContentType.Faction);
		}

		public void Init(DatabaseID id)
		{
			m_entity = new DatabaseEntity(WorkshopContentType.Faction);
			m_entity.GUID = id;
		}

		public bool DoesIncludeUnit(DatabaseID unitID)
		{
			int num = m_units.Length;
			for (int i = 0; i < num; i++)
			{
				if (m_units[i].Entity.GUID == unitID)
				{
					return true;
				}
			}
			return false;
		}

		[Obsolete("This method is obsolete, please use the async version.", false)]
		public static string GetCustomFactionPath(Faction faction)
		{
			string text = faction.Entity.GUID.ToString();
			string filePathFaction = CustomContentFilePaths.FilePathFaction;
			if (faction.IsModFaction)
			{
				string[] files = Directory.GetFiles(PluginSettings.INSTALLATION_DIRECTORY, text + CustomContentFilePaths.FileEndingFaction, SearchOption.AllDirectories);
				if (files.Length != 0)
				{
					return files[0];
				}
				return null;
			}
			string text2 = Path.Combine(filePathFaction, text);
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			service.CreateDirectory(filePathFaction, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			service.CreateDirectory(text2, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			return string.Format(Path.Combine(text2 + "/", "{0}{1}"), text, CustomContentFilePaths.FileEndingFaction);
		}

		public static void GetCustomFactionPathAsync(Faction faction, Action<string> doneCallBack)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			string fileName = faction.Entity.GUID.ToString();
			string filePathFaction = CustomContentFilePaths.FilePathFaction;
			try
			{
				if (faction.IsModFaction)
				{
					fileIO.GetFiles(PluginSettings.INSTALLATION_DIRECTORY, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] entries, Exception exception)
					{
						if (exception != null || entries == null || entries.Length == 0)
						{
							doneCallBack?.Invoke(null);
						}
						else
						{
							doneCallBack?.Invoke(entries[0]);
						}
					});
				}
				string fileDirectory = Path.Combine(filePathFaction, fileName);
				fileIO.CreateDirectory(filePathFaction, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
				{
					if (exception != null)
					{
						doneCallBack?.Invoke(null);
					}
					else
					{
						fileIO.CreateDirectory(fileDirectory, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception fileDirectoryException)
						{
							if (fileDirectoryException != null)
							{
								doneCallBack?.Invoke(null);
							}
							else
							{
								doneCallBack?.Invoke(string.Format(Path.Combine(fileDirectory, "{0}{1}"), fileName, CustomContentFilePaths.FileEndingFaction));
							}
						});
					}
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to create custom Faction directories with exception " + ex.Message);
				doneCallBack?.Invoke(null);
			}
		}

		public void RemoveUnit(UnitBlueprint unitToRemove)
		{
			Debug.Log($"Removing {unitToRemove.Entity.Name} from {Entity.Name}, faction length: {Units.Length}");
			List<UnitBlueprint> list = Units.ToList();
			UnitBlueprint unitBlueprint = list.Find((UnitBlueprint u) => u.Entity.GUID == unitToRemove.Entity.GUID);
			if ((bool)unitBlueprint)
			{
				list.Remove(unitBlueprint);
			}
			Units = list.ToArray();
			Debug.Log($"Successfully removed {unitToRemove.Entity.Name} from {Entity.Name}, new faction length: {Units.Length}");
		}

		public SerializedFaction Serialize()
		{
			SerializedFaction serializedFaction = new SerializedFaction();
			serializedFaction.ID = Entity.GUID;
			serializedFaction.units = new DatabaseID[Units.Length];
			for (int i = 0; i < Units.Length; i++)
			{
				serializedFaction.units[i] = Units[i].Entity.GUID;
			}
			serializedFaction.name = Entity.Name;
			serializedFaction.icon = customFactionIcon.Entity.GUID;
			if (customFactionColor != null)
			{
				serializedFaction.color = customFactionColor.m_DatabaseID;
			}
			return serializedFaction;
		}

		public static Faction Deserialize(SerializedFaction serializedFaction)
		{
			Faction faction = new Faction();
			faction.Init(serializedFaction.ID);
			faction.Entity.Name = serializedFaction.name;
			faction.IsCustom = true;
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			List<UnitBlueprint> list = new List<UnitBlueprint>();
			for (int i = 0; i < serializedFaction.units.Length; i++)
			{
				UnitBlueprint unitBlueprint = contentDatabase.GetUnitBlueprint(serializedFaction.units[i]);
				if (unitBlueprint != null)
				{
					list.Add(unitBlueprint);
					continue;
				}
				Debug.Log(string.Concat("Unit: ", serializedFaction.units[i], " does not longer exsist. Removing from faction ", serializedFaction.name));
			}
			faction.Units = list.ToArray();
			faction.FactionIcon = contentDatabase.GetFactionIcon(serializedFaction.icon);
			faction.CustomFactionColor = contentDatabase.GetCustomFactionColorDatabase().GetFactionColor(serializedFaction.color);
			return faction;
		}

		public bool ValidateFactionUnitPropsAndWeapons()
		{
			UnitBlueprint[] units = Units;
			for (int i = 0; i < units.Length; i++)
			{
				if (units[i].HasMissingComponents)
				{
					return false;
				}
			}
			return true;
		}

		public UnitBlueprint[] GetRearrangedUnits()
		{
			if (!m_sortByCost)
			{
				return Units;
			}
			UnitBlueprint[] array = new UnitBlueprint[Units.Length];
			Array.Copy(Units, array, Units.Length);
			Array.Sort(array, (UnitBlueprint unit1, UnitBlueprint unit2) => unit1.GetUnitCost().CompareTo(unit2.GetUnitCost()));
			return array;
		}
	}
}
