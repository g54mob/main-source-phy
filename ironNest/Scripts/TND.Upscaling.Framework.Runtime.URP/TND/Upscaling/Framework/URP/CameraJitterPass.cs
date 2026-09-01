using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	public class CameraJitterPass : ScriptableRenderPass
	{
		protected UpscalerController_URP _currentController;

		protected Vector2 _currentJitterOffset;

		protected Matrix4x4 _inverseJitterMatrix;

		public virtual bool Setup(UpscalerController_URP controller)
		{
			return false;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		public Matrix4x4 SetupJitter(UpscalerController_URP controller, int displayWidth)
		{
			return default(Matrix4x4);
		}

		private Matrix4x4 SetupJitter(int displayWidth)
		{
			return default(Matrix4x4);
		}

		private static int GetJitterPhaseCount(int renderWidth, int displayWidth)
		{
			return 0;
		}

		private static void GetJitterOffset(out float outX, out float outY, int index, int phaseCount)
		{
			outX = default(float);
			outY = default(float);
		}

		private static Vector3 CreateJitterVector(in Vector2 jitterOffset, in Vector2Int renderSize)
		{
			return default(Vector3);
		}
	}
}
