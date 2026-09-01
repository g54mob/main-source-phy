using UnityEngine;
using UnityEngine.Tilemaps;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Tilemaps/MMTilemapBoolean")]
	public class MMTilemapBoolean : MonoBehaviour
	{
		public Tilemap TilemapToClean;

		[MMInspectorButton("BooleanClean")]
		public bool BooleanCleanButton;

		protected Tilemap _tilemap;

		public virtual void BooleanClean()
		{
		}
	}
}
