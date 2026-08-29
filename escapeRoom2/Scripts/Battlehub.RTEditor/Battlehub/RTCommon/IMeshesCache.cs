using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IMeshesCache
	{
		CacheRefreshMode RefreshMode { get; set; }

		bool IsEmpty { get; }

		IList<RenderMeshesBatch> Batches { get; }

		event Action Refreshing;

		void AddBatch(Mesh mesh, Material material, Matrix4x4[] matrices);

		void RemoveBatch(Mesh mesh);

		void Add(Mesh mesh, Transform transform);

		void SetMaterial(Mesh mesh, Material material);

		void Remove(Mesh mesh, Transform transform);

		void Refresh(bool batchesOnly = false, int maxBatchSize = 128);

		void Clear();

		void Destroy();
	}
}
