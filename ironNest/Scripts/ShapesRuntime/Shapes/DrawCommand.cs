using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Shapes
{
	public class DrawCommand : IDisposable
	{
		private static int bufferID;

		private static int drawCommandWriteNestLevel;

		private static Stack<DrawCommand> cBuffersWriting;

		internal static Dictionary<Camera, List<DrawCommand>> cBuffersRendering;

		private bool hasValidCamera;

		internal bool hasRendered;

		internal int id;

		private bool pushPopState;

		private Camera cam;

		internal readonly List<int> cachedTextIds;

		internal readonly List<UnityEngine.Object> cachedAssets;

		internal readonly List<DisposableMesh> cachedMeshes;

		internal readonly List<ShapeDrawCall> drawCalls;

		public RenderPassEvent camEvt;

		internal static bool IsAddingDrawCommandsToBuffer => false;

		internal static DrawCommand CurrentWritingCommandBuffer => null;

		static DrawCommand()
		{
		}

		public static void ClearAllCommands()
		{
		}

		public static void FlushNullCameras()
		{
		}

		private static void RegisterCommand(DrawCommand cmd)
		{
		}

		internal static void OnCommandRendered(DrawCommand cmd)
		{
		}

		internal DrawCommand Initialize(Camera cam, RenderPassEvent cameraEvent = RenderPassEvent.BeforeRenderingPostProcessing)
		{
			return null;
		}

		internal void AppendToBuffer(RasterCommandBuffer cmd)
		{
		}

		internal void AppendToBuffer(CommandBuffer cmd)
		{
		}

		private void Clear()
		{
		}

		private void CleanupCachedAssetsAndMeshes()
		{
		}

		public void Dispose()
		{
		}
	}
}
