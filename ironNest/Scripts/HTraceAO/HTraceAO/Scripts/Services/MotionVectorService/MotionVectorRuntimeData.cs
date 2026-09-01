using System.Collections.Generic;
using UnityEngine;

namespace HTraceAO.Scripts.Services.MotionVectorService
{
	public class MotionVectorRuntimeData
	{
		public bool WasMovedThisFrame;

		public readonly MaterialPropertyBlock MaterialPropertyBlock;

		public Matrix4x4 PreviousModalMatrix;

		public readonly List<Renderer> Renderers;

		public MotionVectorRuntimeData(List<Renderer> renderers)
		{
		}
	}
}
