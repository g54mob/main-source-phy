using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IRenderersCache
	{
		bool IsEmpty { get; }

		Material MaterialOverride { get; set; }

		IList<Renderer> Renderers { get; }

		event Action Refreshed;

		void Add(Renderer[] renderers, bool forceRender = true, bool forceMatrixRecalculationPerRender = false);

		void Remove(Renderer[] renderers);

		void Add(Renderer renderer, bool forceRender = true, bool forceMatrixRecalcuationPerRender = false);

		void Remove(Renderer renderer);

		void Refresh();

		void Clear();

		void Destroy();
	}
}
