using System;
using System.Collections.Generic;
using Landfall.TABS;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Data Asset", menuName = "TABS/Upgrade Data Asset")]
public class UpgradeDataAsset : SerializedScriptableObject
{
	[SerializeField]
	private Dictionary<Guid, DatabaseID> m_guidToDatabaseID;

	public DatabaseID GetDatabaseID(Guid guid)
	{
		if (m_guidToDatabaseID == null)
		{
			return default(DatabaseID);
		}
		if (!m_guidToDatabaseID.ContainsKey(guid))
		{
			Debug.LogWarning("Trying to get databaseID from missing GUID");
			return default(DatabaseID);
		}
		return m_guidToDatabaseID[guid];
	}
}
