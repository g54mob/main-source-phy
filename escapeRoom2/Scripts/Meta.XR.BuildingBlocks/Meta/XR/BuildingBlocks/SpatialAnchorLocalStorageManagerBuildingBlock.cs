using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.XR.BuildingBlocks
{
	public class SpatialAnchorLocalStorageManagerBuildingBlock : MonoBehaviour
	{
		private SpatialAnchorCoreBuildingBlock _spatialAnchorCore;

		private List<Guid> _uuids = OVRObjectPool.Get<List<Guid>>();

		private const string NumUuidsPlayerPref = "numUuids";

		private void Start()
		{
			_spatialAnchorCore = SpatialAnchorCoreBuildingBlock.GetBaseInstances()[0];
			_spatialAnchorCore.OnAnchorCreateCompleted.AddListener(SaveAnchorUuidToLocalStorage);
			_spatialAnchorCore.OnAnchorEraseCompleted.AddListener(RemoveAnchorFromLocalStorage);
		}

		internal void SaveAnchorUuidToLocalStorage(OVRSpatialAnchor anchor, OVRSpatialAnchor.OperationResult result)
		{
			if (result == OVRSpatialAnchor.OperationResult.Success)
			{
				if (!PlayerPrefs.HasKey("numUuids"))
				{
					PlayerPrefs.SetInt("numUuids", 0);
				}
				int num = PlayerPrefs.GetInt("numUuids");
				PlayerPrefs.SetString("uuid" + num, anchor.Uuid.ToString());
				PlayerPrefs.SetInt("numUuids", ++num);
			}
		}

		internal void RemoveAnchorFromLocalStorage(OVRSpatialAnchor anchor, OVRSpatialAnchor.OperationResult result)
		{
			Guid uuid = anchor.Uuid;
			if (result == OVRSpatialAnchor.OperationResult.Failure)
			{
				return;
			}
			int num = PlayerPrefs.GetInt("numUuids", 0);
			for (int i = 0; i < num; i++)
			{
				string key = "uuid" + i;
				if (PlayerPrefs.GetString(key, "").Equals(uuid.ToString()))
				{
					string key2 = "uuid" + (num - 1);
					string value = PlayerPrefs.GetString(key2);
					PlayerPrefs.SetString(key, value);
					PlayerPrefs.DeleteKey(key2);
					num--;
					if (num < 0)
					{
						num = 0;
					}
					PlayerPrefs.SetInt("numUuids", num);
					break;
				}
			}
		}

		internal List<Guid> GetAnchorAnchorUuidFromLocalStorage()
		{
			if (!PlayerPrefs.HasKey("numUuids"))
			{
				Reset();
				Debug.Log("[SpatialAnchorLocalStorageManagerBuildingBlock] Anchor not found.");
				return null;
			}
			_uuids.Clear();
			int num = PlayerPrefs.GetInt("numUuids");
			for (int i = 0; i < num; i++)
			{
				string key = "uuid" + i;
				if (PlayerPrefs.HasKey(key))
				{
					string g = PlayerPrefs.GetString(key);
					_uuids.Add(new Guid(g));
				}
			}
			return _uuids;
		}

		public void Reset()
		{
			PlayerPrefs.SetInt("numUuids", 0);
		}

		private void OnDestroy()
		{
			_spatialAnchorCore.OnAnchorCreateCompleted.RemoveAllListeners();
			OVRObjectPool.Return(_uuids);
		}
	}
}
