using System;
using UnityEngine;

namespace Landfall.TABS
{
	[Serializable]
	public struct PlacementSquare
	{
		[SerializeField]
		private bool DebugDraw;

		[SerializeField]
		private Vector3 m_offset;

		[SerializeField]
		private Vector3 m_size;

		public Vector3 Offset => m_offset;

		public Vector3 Size => m_size;

		public void OnDrawGizmos(Vector3 unitPos, Vector3 unitScale, Quaternion unitRotation)
		{
			if (DebugDraw)
			{
				Vector3 offset = Offset;
				Color color = Gizmos.color;
				Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.2f);
				Gizmos.matrix = Matrix4x4.TRS(unitPos, unitRotation, unitScale);
				Gizmos.DrawCube(offset, Size);
				Gizmos.color = Color.green;
				Gizmos.DrawWireCube(offset, Size);
				Gizmos.color = color;
			}
		}
	}
}
