using UnityEngine;
using UnityEngine.Tilemaps;

namespace MoreMountains.Tools
{
	[ExecuteAlways]
	[AddComponentMenu("More Mountains/Tools/Tilemaps/MMTilemapShadow")]
	[RequireComponent(typeof(Tilemap))]
	public class MMTilemapShadow : MonoBehaviour
	{
		public Tilemap ReferenceTilemap;

		[MMInspectorButton("UpdateShadows")]
		public bool UpdateShadowButton;

		protected Tilemap _tilemap;

		public virtual void UpdateShadows()
		{
		}

		public static void Copy(Tilemap source, Tilemap destination)
		{
		}
	}
}
