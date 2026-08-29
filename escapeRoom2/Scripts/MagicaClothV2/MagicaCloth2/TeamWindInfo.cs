using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public struct TeamWindInfo : IValid
	{
		public int windId;

		public float time;

		public float main;

		public float3 direction;

		public bool IsValid()
		{
			return main > 1E-06f;
		}

		public override string ToString()
		{
			return $"windId:{windId}, time:{time}, main:{main}, direction:{direction}";
		}

		public void DebugLog()
		{
			Debug.Log(ToString());
		}
	}
}
