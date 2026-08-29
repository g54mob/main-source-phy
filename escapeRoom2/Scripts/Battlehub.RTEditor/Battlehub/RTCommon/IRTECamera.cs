using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTCommon
{
	public interface IRTECamera
	{
		Camera Camera { get; }

		IRTECommandBuffer RTECommandBuffer { get; }

		IRTECommandBuffer RTECommandBufferOverride { get; set; }

		CameraEvent Event { get; set; }

		IRenderersCache RenderersCache { get; }

		IMeshesCache MeshesCache { get; }

		event Action<IRTECamera> CommandBufferRefresh;

		void RefreshCommandBuffer();

		void Destroy();
	}
}
