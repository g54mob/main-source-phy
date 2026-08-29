using UnityEngine;
using UnityEngine.Rendering;

public static class PineDraw
{
	private const int PAINT_PASS_DRAW = 0;

	private const int PAINT_PASS_ERASE = 1;

	private const int PAINT_PASS_GROW_MASK = 2;

	private const int PAINT_PASS_VISIBILITY_MASK = 3;

	public static void init(PineDrawContext context, Shader paintShader, Shader blitCopyShader, Shader growPixelsShader, Camera camera)
	{
		context.paintMaterial = new Material(paintShader);
		context.blitCopyMaterial = new Material(blitCopyShader);
		context.growPixelsMaterial = new Material(growPixelsShader);
		context.camera = camera;
	}

	public static RenderTexture createPaintTextureFromDimensions(PineDrawContext context, Renderer renderer, float texelDensityPerUnit = 10f, int minSize = 32, int maxSize = 2048)
	{
		RenderTextureDescriptor desc = new RenderTextureDescriptor(1024, 1024, RenderTextureFormat.ARGB32, 0);
		desc.msaaSamples = 1;
		desc.autoGenerateMips = false;
		desc.useMipMap = false;
		desc.sRGB = true;
		return new RenderTexture(desc);
	}

	private static void commonBrush(PineDrawContext context, Texture baseLayerTexture, RenderTexture targetTexture, Renderer renderer, Texture2D brushTexture, Color brushColor, Vector2 brushCenterScreen, float brushSizeScreen, int pass)
	{
		bool flag = baseLayerTexture == targetTexture;
		CommandBuffer commandBuffer = new CommandBuffer();
		commandBuffer.name = "Pine Draw - Point";
		commandBuffer.SetGlobalTexture("_BaseLayerTexture", baseLayerTexture);
		commandBuffer.SetGlobalTexture("_BrushTexture", brushTexture);
		commandBuffer.SetGlobalColor("_BrushColor", brushColor);
		commandBuffer.SetGlobalVector("_BrushData", new Vector4(brushCenterScreen.x / (float)Screen.width, brushCenterScreen.y / (float)Screen.height, 1f / (brushSizeScreen / (float)Screen.width), 1f / (brushSizeScreen / (float)Screen.height)));
		commandBuffer.SetViewProjectionMatrices(context.camera.worldToCameraMatrix, context.camera.projectionMatrix);
		RenderTextureDescriptor desc = new RenderTextureDescriptor(Screen.width, Screen.height, RenderTextureFormat.Depth, 24);
		desc.msaaSamples = 1;
		desc.autoGenerateMips = false;
		desc.useMipMap = false;
		desc.sRGB = false;
		commandBuffer.GetTemporaryRT(Shader.PropertyToID("VisibilityMask"), desc, FilterMode.Point);
		commandBuffer.SetRenderTarget(Shader.PropertyToID("VisibilityMask"));
		commandBuffer.ClearRenderTarget(clearDepth: true, clearColor: true, Color.clear);
		for (int i = 0; i < renderer.sharedMaterials.Length; i++)
		{
			commandBuffer.DrawRenderer(renderer, context.paintMaterial, i, 3);
		}
		commandBuffer.SetGlobalTexture("_VisibilityMask", Shader.PropertyToID("VisibilityMask"));
		if (flag)
		{
			commandBuffer.GetTemporaryRT(Shader.PropertyToID("DrawTemp"), targetTexture.width, targetTexture.height, 0, FilterMode.Point);
			commandBuffer.Blit(baseLayerTexture, Shader.PropertyToID("DrawTemp"), context.blitCopyMaterial, 0);
			commandBuffer.SetRenderTarget(Shader.PropertyToID("DrawTemp"));
		}
		else
		{
			commandBuffer.Blit(baseLayerTexture, targetTexture, context.blitCopyMaterial, 0);
			commandBuffer.SetRenderTarget(targetTexture);
		}
		commandBuffer.SetViewProjectionMatrices(context.camera.worldToCameraMatrix, context.camera.projectionMatrix);
		for (int j = 0; j < renderer.sharedMaterials.Length; j++)
		{
			commandBuffer.DrawRenderer(renderer, context.paintMaterial, j, pass);
		}
		if (flag)
		{
			commandBuffer.Blit(Shader.PropertyToID("DrawTemp"), targetTexture, context.blitCopyMaterial, 0);
			commandBuffer.ReleaseTemporaryRT(Shader.PropertyToID("DrawTemp"));
		}
		commandBuffer.ReleaseTemporaryRT(Shader.PropertyToID("VisibilityMask"));
		Graphics.ExecuteCommandBuffer(commandBuffer);
	}

	public static void drawBrush(PineDrawContext context, Texture baseLayerTexture, RenderTexture targetTexture, Renderer renderer, Texture2D brushTexture, Color brushColor, Vector2 brushCenterScreen, float brushSizeScreen)
	{
		commonBrush(context, baseLayerTexture, targetTexture, renderer, brushTexture, brushColor, brushCenterScreen, brushSizeScreen, 0);
	}

	public static void eraseBrush(PineDrawContext context, Texture baseLayerTexture, RenderTexture targetTexture, Renderer renderer, Texture2D brushTexture, Color brushColor, Vector2 brushCenterScreen, float brushSizeScreen)
	{
		commonBrush(context, baseLayerTexture, targetTexture, renderer, brushTexture, brushColor, brushCenterScreen, brushSizeScreen, 1);
	}

	public static void growPixels(PineDrawContext context, Texture baseLayerTexture, RenderTexture targetTexture, Renderer renderer)
	{
		bool flag = baseLayerTexture == targetTexture;
		CommandBuffer commandBuffer = new CommandBuffer();
		commandBuffer.name = "Pine Draw - Grow Pixels";
		commandBuffer.GetTemporaryRT(Shader.PropertyToID("GrowMask"), targetTexture.width, targetTexture.height, 0, FilterMode.Point);
		commandBuffer.SetRenderTarget(Shader.PropertyToID("GrowMask"));
		commandBuffer.ClearRenderTarget(clearDepth: true, clearColor: true, Color.clear);
		for (int i = 0; i < renderer.sharedMaterials.Length; i++)
		{
			commandBuffer.DrawRenderer(renderer, context.paintMaterial, i, 2);
		}
		commandBuffer.SetGlobalTexture("_GrowMask", Shader.PropertyToID("GrowMask"));
		if (flag)
		{
			commandBuffer.GetTemporaryRT(Shader.PropertyToID("DrawTemp"), targetTexture.width, targetTexture.height, 0, FilterMode.Point);
			commandBuffer.Blit(baseLayerTexture, Shader.PropertyToID("DrawTemp"), context.blitCopyMaterial, 0);
			commandBuffer.Blit(Shader.PropertyToID("DrawTemp"), targetTexture, context.growPixelsMaterial, 0);
		}
		else
		{
			commandBuffer.Blit(baseLayerTexture, targetTexture, context.growPixelsMaterial, 0);
		}
		commandBuffer.ReleaseTemporaryRT(Shader.PropertyToID("GrowMask"));
		if (flag)
		{
			commandBuffer.ReleaseTemporaryRT(Shader.PropertyToID("DrawTemp"));
		}
		Graphics.ExecuteCommandBuffer(commandBuffer);
	}
}
