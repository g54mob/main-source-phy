using System;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS;
using UnityEngine;

namespace DM
{
	public class AssetLoader
	{
		private Dictionary<DatabaseID, UnityEngine.Object> m_nonStreamableAssets;

		private Dictionary<DatabaseID, string> m_resourceAssetPaths;

		private Dictionary<DatabaseID, string> m_adderssableAssetPaths;

		public AssetLoader(string expectedBuildDateTimeString, List<UnityEngine.Object> nonStreamableGameObjects)
		{
			AssetDatabaseFile assetDatabaseFile = DatabaseSerializer.Deserialize<AssetDatabaseFile>(Resources.Load<TextAsset>("DMAssetDatabase").bytes);
			if (assetDatabaseFile.buildDateTime != expectedBuildDateTimeString)
			{
				throw new Exception($"ResourceDatabase and Assets has a different build times: {assetDatabaseFile.buildDateTime} != {expectedBuildDateTimeString}");
			}
			m_nonStreamableAssets = assetDatabaseFile.nonStreamableAssets.ToDictionary((AssetDatabaseFile.NonStreamableAsset entry) => new DatabaseID
			{
				m_modID = entry.modId,
				m_ID = entry.id
			}, (AssetDatabaseFile.NonStreamableAsset entry) => nonStreamableGameObjects[entry.index]);
			m_resourceAssetPaths = new Dictionary<DatabaseID, string>();
			m_adderssableAssetPaths = assetDatabaseFile.streamableAssets.ToDictionary((AssetDatabaseFile.StreamableAsset entry) => new DatabaseID
			{
				m_modID = entry.modId,
				m_ID = entry.id
			}, (AssetDatabaseFile.StreamableAsset entry) => entry.path);
		}

		private string ToStreamablePath(DatabaseID databaseId)
		{
			if (m_adderssableAssetPaths.TryGetValue(databaseId, out var value))
			{
				return value;
			}
			return null;
		}

		private static string BuildDatabaseIdURI(DatabaseID databaseId)
		{
			return databaseId.ToString();
		}

		public bool Exists(DatabaseID databaseId)
		{
			return m_nonStreamableAssets.ContainsKey(databaseId) | m_adderssableAssetPaths.ContainsKey(databaseId);
		}

		public T GetNonStreamableAsset<T>(DatabaseID databaseId) where T : UnityEngine.Object
		{
			if (m_nonStreamableAssets.TryGetValue(databaseId, out var value))
			{
				return value as T;
			}
			return null;
		}

		public T GetResourceAsset<T>(DatabaseID databaseId) where T : UnityEngine.Object
		{
			if (m_resourceAssetPaths.TryGetValue(databaseId, out var value))
			{
				Debug.LogFormat("Loaded resource {}", value);
				return Resources.Load<T>(value);
			}
			return null;
		}

		public T GetPersistentAddressableAsset<T>(DatabaseID databaseId) where T : UnityEngine.Object
		{
			return null;
		}

		public T GetAsset<T>(DatabaseID databaseId) where T : UnityEngine.Object
		{
			if (databaseId == default(DatabaseID))
			{
				return null;
			}
			T val = GetNonStreamableAsset<T>(databaseId) ?? GetResourceAsset<T>(databaseId) ?? GetPersistentAddressableAsset<T>(databaseId);
			if (val == null)
			{
				Debug.LogWarningFormat("Failed to load {0} ({1})", databaseId, ToStreamablePath(databaseId));
			}
			return val;
		}
	}
}
