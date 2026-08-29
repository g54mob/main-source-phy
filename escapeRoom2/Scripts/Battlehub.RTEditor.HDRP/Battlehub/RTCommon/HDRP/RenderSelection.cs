using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace Battlehub.RTCommon.HDRP
{
	public class RenderSelection : RenderGraphics
	{
		protected override void AddCamera(IRTECamera camera)
		{
			if (camera.Event == CameraEvent.AfterEverything)
			{
				GetList(camera).Add(camera);
			}
		}

		protected override void Execute(CustomPassContext ctx)
		{
			Execute(ctx.cmd, ctx.hdCamera);
		}

		private void Execute(CommandBuffer cmd, HDCamera hdCamera)
		{
			if (m_cameras.TryGetValue(hdCamera.camera, out var value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					IRTECamera iRTECamera = value[i];
					s_commandBufferWrapper.WrappedCommandBuffer = cmd;
					iRTECamera.RTECommandBufferOverride = s_commandBufferWrapper;
					iRTECamera.RefreshCommandBuffer();
				}
			}
		}
	}
}
