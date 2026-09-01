using System.IO;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using ModIO;
using UnityEngine;

namespace LevelCreator
{
	public class CustomMap : ScriptableObject, IDatabaseEntity
	{
		[SerializeField]
		private DatabaseEntity m_entity;

		public DatabaseEntity Entity => m_entity;

		public int ModID => Entity.GUID.m_modID;

		public bool LocalClientOwnedCustomContent { get; private set; }

		public ModProfile ModProfile { get; private set; }

		public string FilePath { get; private set; }

		public string LevelPath { get; private set; }

		public string FolderPath { get; private set; }

		public string IconPath { get; private set; }

		public CustomMap(string levelPath, string iconPath, string filePath, string name, DatabaseID id)
		{
			m_entity = new DatabaseEntity(WorkshopContentType.Map);
			m_entity.GUID = id;
			Entity.Name = name;
			SetLevelPath(levelPath);
			SetIconPath(iconPath);
			SetFilePath(filePath);
		}

		public void SetLevelPath(string levelPath)
		{
			if (!levelPath.Contains(CustomContentFilePaths.FileEndingCustomLevel))
			{
				levelPath += CustomContentFilePaths.FileEndingCustomLevel;
			}
			LevelPath = levelPath;
		}

		public void SetIconPath(string iconPath)
		{
			IconPath = iconPath;
			Entity.SetSpriteIconPath(iconPath);
		}

		public void SetFilePath(string filePath)
		{
			FilePath = filePath;
			FolderPath = Path.GetDirectoryName(filePath);
		}

		public bool IsModLevel()
		{
			return ModID > 0;
		}

		public void SetModID(int id)
		{
			Entity.NewModID(id);
		}

		public void SetCustomData(int id, ModProfile modProfile)
		{
			SetModID(id);
			if (modProfile != null && CustomContentLoaderModIO.LocalModIOUser != null)
			{
				LocalClientOwnedCustomContent = modProfile.submittedBy.id == CustomContentLoaderModIO.LocalModIOUser.id;
			}
			ModProfile = modProfile;
		}

		public static SerializedCustomMap Serialize(CustomMap customMap)
		{
			return new SerializedCustomMap
			{
				id = customMap.Entity.GUID,
				name = customMap.Entity.Name,
				levelPath = customMap.LevelPath,
				iconPath = customMap.IconPath,
				filePath = customMap.FilePath
			};
		}

		public static CustomMap Deserialize(SerializedCustomMap serializedCustomMap)
		{
			return new CustomMap(serializedCustomMap.levelPath, serializedCustomMap.iconPath, serializedCustomMap.filePath, serializedCustomMap.name, serializedCustomMap.id);
		}
	}
}
