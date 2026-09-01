using System.Collections.Generic;
using UnityEngine;

namespace HTraceAO.Scripts.Services.MotionVectorService
{
	public class MotionVectorService : IService
	{
		private static readonly int s_ForceNoMotion;

		private static readonly int s_HasLastPositionData;

		private static readonly int s_MotionVectorDepthBias;

		private static readonly int s_PreviousM;

		private static MotionVectorService s_Instance;

		private readonly Dictionary<GameObject, MotionVectorRuntimeData> _runtimeDatas;

		public static MotionVectorService Instance => null;

		public Dictionary<GameObject, MotionVectorRuntimeData> GetObjects => null;

		public void AddObject(GameObject gameObject, Renderer renderer)
		{
		}

		public void AddObject(GameObject gameObject, List<Renderer> renderers)
		{
		}

		public void RemoveObject(GameObject gameObject)
		{
		}

		public void Update()
		{
		}

		public void Cleanup()
		{
		}

		private static bool MatricesAreEqual(Matrix4x4 a, Matrix4x4 b, float tolerance = 0.0001f)
		{
			return false;
		}
	}
}
