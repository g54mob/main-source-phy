using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.XR.BuildingBlocks
{
	[RequireComponent(typeof(SpatialAnchorSpawnerBuildingBlock))]
	public class SpatialAnchorLoaderBuildingBlock : MonoBehaviour
	{
		private SpatialAnchorCoreBuildingBlock _spatialAnchorCore;

		private SpatialAnchorSpawnerBuildingBlock _spatialAnchorSpawner;

		private void Awake()
		{
			_spatialAnchorSpawner = GetComponent<SpatialAnchorSpawnerBuildingBlock>();
			_spatialAnchorCore = SpatialAnchorCoreBuildingBlock.GetBaseInstances()[0];
		}

		public virtual void LoadAndInstantiateAnchors(List<Guid> uuids)
		{
			_spatialAnchorCore.LoadAndInstantiateAnchors(_spatialAnchorSpawner.AnchorPrefab, uuids);
		}

		public virtual void LoadAnchorsFromDefaultLocalStorage()
		{
			SpatialAnchorLocalStorageManagerBuildingBlock spatialAnchorLocalStorageManagerBuildingBlock = UnityEngine.Object.FindAnyObjectByType<SpatialAnchorLocalStorageManagerBuildingBlock>();
			if (!spatialAnchorLocalStorageManagerBuildingBlock)
			{
				Debug.Log("[SpatialAnchorLocalStorageManagerBuildingBlock] component is missing.");
				return;
			}
			List<Guid> anchorAnchorUuidFromLocalStorage = spatialAnchorLocalStorageManagerBuildingBlock.GetAnchorAnchorUuidFromLocalStorage();
			if (anchorAnchorUuidFromLocalStorage != null)
			{
				_spatialAnchorCore.LoadAndInstantiateAnchors(_spatialAnchorSpawner.AnchorPrefab, anchorAnchorUuidFromLocalStorage);
			}
		}
	}
}
