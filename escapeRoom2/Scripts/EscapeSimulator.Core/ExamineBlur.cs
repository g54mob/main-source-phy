using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

internal class ExamineBlur : CustomPass
{
	private static class ShaderID
	{
		public static readonly int _BlitTexture = Shader.PropertyToID("_BlitTexture");

		public static readonly int _BlitScaleBias = Shader.PropertyToID("_BlitScaleBias");

		public static readonly int _BlitMipLevel = Shader.PropertyToID("_BlitMipLevel");

		public static readonly int _Radius = Shader.PropertyToID("_Radius");

		public static readonly int _Source = Shader.PropertyToID("_Source");

		public static readonly int _ColorBufferCopy = Shader.PropertyToID("_ColorBufferCopy");

		public static readonly int _Mask = Shader.PropertyToID("_Mask");

		public static readonly int _MaskDepth = Shader.PropertyToID("_MaskDepth");

		public static readonly int _InvertMask = Shader.PropertyToID("_InvertMask");

		public static readonly int _ViewPortSize = Shader.PropertyToID("_ViewPortSize");

		public static readonly int _ColorMultiplier = Shader.PropertyToID("_ColorMultiplier");

		public static readonly int _KawasePixelOffset = Shader.PropertyToID("_KawasePixelOffset");

		public static readonly int _BrightnessMultiplier = Shader.PropertyToID("_BrightnessMultiplier");
	}

	public float blurAmount;

	public float blurDirection;

	public float backgroundFadeTarget = 0.25f;

	private Material kawaseMaterial;

	private Material compositeMaterial;

	private Material whiteRenderersMaterial;

	private RTHandle downSampleBuffer;

	private RTHandle[] blurBuffer = new RTHandle[2];

	private RTHandle maskBuffer;

	private RTHandle maskDepthBuffer;

	private RTHandle colorCopy;

	private ShaderTagId[] shaderTags;

	[SerializeField]
	[HideInInspector]
	private Shader kawaseBlurShader;

	private int blurLevel;

	private float screenWidth;

	private float screenHeight;

	private int recaptureFrame;

	private int[] kawaseOffsetValues = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

	protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
	{
		if (kawaseBlurShader == null)
		{
			kawaseBlurShader = Resources.Load<Shader>("KawaseBlur");
		}
		kawaseMaterial = CoreUtils.CreateEngineMaterial(kawaseBlurShader);
		Vector2 scaleFactor = new Vector2(0.5f, 0.5f);
		blurBuffer[0] = RTHandles.Alloc(scaleFactor, TextureXR.slices, DepthBits.None, GraphicsFormat.B10G11R11_UFloatPack32, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, enableRandomWrite: false, useMipMap: false, autoGenerateMips: true, isShadowMap: false, 1, 0f, MSAASamples.None, bindTextureMS: false, useDynamicScale: false, useDynamicScaleExplicit: false, RenderTextureMemoryless.None, VRTextureUsage.None, "BlurBuffer[0]");
		blurBuffer[1] = RTHandles.Alloc(scaleFactor, TextureXR.slices, DepthBits.None, GraphicsFormat.B10G11R11_UFloatPack32, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, enableRandomWrite: false, useMipMap: false, autoGenerateMips: true, isShadowMap: false, 1, 0f, MSAASamples.None, bindTextureMS: false, useDynamicScale: false, useDynamicScaleExplicit: false, RenderTextureMemoryless.None, VRTextureUsage.None, "BlurBuffer[1]");
		shaderTags = new ShaderTagId[4]
		{
			new ShaderTagId("Forward"),
			new ShaderTagId("ForwardOnly"),
			new ShaderTagId("SRPDefaultUnlit"),
			new ShaderTagId("FirstPass")
		};
	}

	public bool hasCapturedBackground()
	{
		return blurLevel > 0;
	}

	protected override void Execute(CustomPassContext ctx)
	{
		if (!ctx.hdCamera.camera.gameObject.CompareTag("MainCamera"))
		{
			return;
		}
		if (blurAmount == 0f)
		{
			screenWidth = Screen.width;
			screenHeight = Screen.height;
			blurLevel = 0;
			return;
		}
		if ((float)Screen.width != screenWidth || screenHeight != (float)Screen.height)
		{
			screenWidth = Screen.width;
			screenHeight = Screen.height;
			recaptureFrame = 2;
		}
		if (recaptureFrame > 0)
		{
			blurLevel = 0;
			recaptureFrame--;
			return;
		}
		if (blurLevel < kawaseOffsetValues.Length)
		{
			if (blurLevel >= 0)
			{
				RTHandle rTHandle = ((blurLevel == 0) ? ctx.cameraColorBuffer : blurBuffer[(blurLevel + 1) % 2]);
				RTHandle colorBuffer = blurBuffer[blurLevel % 2];
				MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
				materialPropertyBlock.SetTexture("_Source", rTHandle);
				materialPropertyBlock.SetVector("_KawasePixelOffset", kawasePixelOffset(kawaseOffsetValues[blurLevel]));
				HDUtils.DrawFullScreen(ctx.cmd, kawaseMaterial, colorBuffer, materialPropertyBlock);
			}
			blurLevel++;
		}
		RTHandle rTHandle2 = blurBuffer[(blurLevel + 1) % 2];
		MaterialPropertyBlock materialPropertyBlock2 = new MaterialPropertyBlock();
		materialPropertyBlock2.SetTexture("_Source", rTHandle2);
		materialPropertyBlock2.SetFloat("_BrightnessMultiplier", Mathf.Lerp(1f, backgroundFadeTarget, blurAmount));
		materialPropertyBlock2.SetFloat("_BlurAmount", (blurDirection < 0f) ? blurAmount : 1f);
		HDUtils.DrawFullScreen(ctx.cmd, kawaseMaterial, ctx.cameraColorBuffer, materialPropertyBlock2, 1);
		Vector4 kawasePixelOffset(int level)
		{
			float num = (float)level + 0.5f;
			Vector2Int currentViewportSize = blurBuffer[0].rtHandleProperties.currentViewportSize;
			return new Vector4(num / (float)currentViewportSize.x, num / (float)currentViewportSize.y);
		}
	}

	protected override void Cleanup()
	{
		blurBuffer[0].Release();
		blurBuffer[1].Release();
	}
}
