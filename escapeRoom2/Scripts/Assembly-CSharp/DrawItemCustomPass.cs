using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DrawItemCustomPass : CustomPass
{
	public Camera camera;

	public RenderTexture renderTexture;

	protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
	{
		renderTexture = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32);
	}

	protected override void Execute(CustomPassContext ctx)
	{
		if (ctx.hdCamera.camera.cameraType == CameraType.SceneView)
		{
			return;
		}
		CoreUtils.SetRenderTarget(ctx.cmd, renderTexture.colorBuffer, renderTexture.depthBuffer, ClearFlag.All);
		using (new CustomPassUtils.OverrideCameraRendering(ctx, camera))
		{
			ctx.cmd.ClearRenderTarget(clearDepth: true, clearColor: true, Color.green);
			CustomPassUtils.RenderFromCamera(in ctx, camera, -1);
		}
	}

	protected override void Cleanup()
	{
	}
}
