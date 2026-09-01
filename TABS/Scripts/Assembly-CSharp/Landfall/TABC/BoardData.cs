using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABC
{
	public class BoardData : MonoBehaviour
	{
		public float boardWidth = 18f;

		public int segments = 9;

		private float segmentRadius;

		public static BoardData instance;

		private void Awake()
		{
			instance = this;
			segmentRadius = boardWidth / (float)segments;
		}

		public int2 WorldToBoardPos(Vector3 pos)
		{
			int x = Mathf.RoundToInt(pos.x / segmentRadius);
			int y = Mathf.RoundToInt(pos.z / segmentRadius);
			return new int2(x, y);
		}

		public Vector3 BoardPosToWorld(int2 pos)
		{
			int num = Mathf.RoundToInt((float)pos.x * segmentRadius);
			int num2 = Mathf.RoundToInt((float)pos.y * segmentRadius);
			return new Vector3(num, 0f, num2);
		}
	}
}
