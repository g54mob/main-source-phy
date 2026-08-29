using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class RuntimeSelectionFilteringArgs : EventArgs
	{
		public IList<RaycastHit> Hits { get; private set; }

		public RuntimeSelectionFilteringArgs(IEnumerable<RaycastHit> hits)
		{
			Hits = hits.ToList();
		}
	}
}
