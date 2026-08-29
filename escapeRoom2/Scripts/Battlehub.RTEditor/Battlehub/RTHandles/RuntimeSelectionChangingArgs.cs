using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class RuntimeSelectionChangingArgs : EventArgs
	{
		public bool Cancel { get; set; }

		public IList<UnityEngine.Object> Selected { get; private set; }

		public RuntimeSelectionChangingArgs(IEnumerable<UnityEngine.Object> selected)
		{
			Selected = selected.ToList();
		}
	}
}
