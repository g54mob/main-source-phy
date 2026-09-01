using UnityEngine;
using UnityEngine.Tilemaps;

namespace MoreMountains.Tools
{
	public class MMTilemapCleaner : MonoBehaviour
	{
		[MMInspectorButton("Clean")]
		public bool CleanButton;

		[MMInspectorButton("CleanAllChildren")]
		public bool CleanAllButton;

		protected Tilemap _tilemap;

		protected Tilemap[] _tilemaps;

		public virtual void Clean()
		{
		}

		public virtual void CleanAllChildren()
		{
		}
	}
}
