using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

internal class ScreenSpaceCameraUIBlur : CustomPass
{
	public float blurRadius = 10f;

	public LayerMask uiLayer = 32;

	private RTHandle downSampleBuffer;

	protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
	{
		if (base.injectionPoint != CustomPassInjectionPoint.AfterPostProcess)
		{
			Debug.LogWarning("Custom Pass UI Blur isn't using the after post process injection point. Your post processes will be applied to the UI");
		}
		downSampleBuffer = RTHandles.Alloc(Vector2.one * 0.5f, TextureXR.slices, DepthBits.None, GraphicsFormat.B10G11R11_UFloatPack32, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, enableRandomWrite: false, useMipMap: false, autoGenerateMips: true, isShadowMap: false, 1, 0f, MSAASamples.None, bindTextureMS: false, useDynamicScale: true, useDynamicScaleExplicit: false, RenderTextureMemoryless.None, VRTextureUsage.None, "DownSampleBuffer");
	}

	protected override void AggregateCullingParameters(ref ScriptableCullingParameters cullingParameters, HDCamera hdCamera)
	{
		cullingParameters.cullingMask |= (uint)uiLayer.value;
	}

	protected override void Execute(CustomPassContext ctx)
	{
		if (ctx.hdCamera.camera.cameraType != CameraType.SceneView)
		{
			CustomPassUtils.GaussianBlur(in ctx, ctx.cameraColorBuffer, ctx.cameraColorBuffer, downSampleBuffer, 9, blurRadius);
			CoreUtils.SetRenderTarget(ctx.cmd, ctx.cameraColorBuffer, ctx.customDepthBuffer.Value, ClearFlag.DepthStencil, Color.clear);
			CustomPassUtils.DrawRenderers(in ctx, uiLayer, RenderQueueType.Transparent, null, 0, default(RenderStateBlock), SortingCriteria.CommonTransparent);
		}
	}

	protected override void Cleanup()
	{
		downSampleBuffer.Release();
	}
}
