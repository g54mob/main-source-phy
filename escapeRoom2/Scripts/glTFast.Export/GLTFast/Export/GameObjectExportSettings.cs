using UnityEngine;

namespace GLTFast.Export
{
	public class GameObjectExportSettings
	{
		public bool OnlyActiveInHierarchy { get; set; } = true;

		public bool DisabledComponents { get; set; }

		public LayerMask LayerMask { get; set; } = -1;
	}
}
