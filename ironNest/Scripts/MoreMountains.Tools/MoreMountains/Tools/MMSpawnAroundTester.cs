using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMSpawnAroundTester : MonoBehaviour
	{
		public GameObject ObjectToInstantiate;

		public MMSpawnAroundProperties SpawnProperties;

		[Header("Debug")]
		public int DebugQuantity;

		[MMInspectorButton("DebugSpawn")]
		public bool DebugSpawnButton;

		[Header("Gizmos")]
		public bool DrawGizmos;

		public int GizmosQuantity;

		public float GizmosSize;

		protected GameObject _gameObject;

		public virtual void DebugSpawn()
		{
		}

		public virtual void Spawn()
		{
		}

		protected virtual void OnDrawGizmos()
		{
		}
	}
}
