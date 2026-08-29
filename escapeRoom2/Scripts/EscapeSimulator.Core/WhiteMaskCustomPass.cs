using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class WhiteMaskCustomPass : CustomPass
{
	public LayerMask maskLayer;

	public Material overrideMaterial;

	public RenderTexture targetRenderTexture;

	protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
	{
	}

	protected override void Execute(CustomPassContext ctx)
	{
		RTHandle cameraColorBuffer = ctx.cameraColorBuffer;
		CoreUtils.SetRenderTarget(ctx.cmd, targetRenderTexture, ClearFlag.All, Color.black);
		CustomPassUtils.DrawRenderers(in ctx, maskLayer, RenderQueueType.All, overrideMaterial);
		CoreUtils.SetRenderTarget(ctx.cmd, cameraColorBuffer);
	}

	protected override void Cleanup()
	{
	}
}
