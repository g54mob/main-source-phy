using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class AmbientOcclusion2DRenderer : PostProcessEffectRenderer<AmbientOcclusion2D>
	{
		private enum Pass
		{
			LuminanceDiff = 0,
			Blur = 1,
			Blend = 2,
			Debug = 3
		}

		private Shader shader;

		private int aoTexID;

		private int screenCopyID;

		private RenderTexture aoRT;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Ambient Occlusion 2D");
			aoTexID = Shader.PropertyToID("_AO");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			CommandBuffer command = context.command;
			propertySheet.properties.SetFloat("_SampleDistance", base.settings.distance);
			float value = ((QualitySettings.activeColorSpace == ColorSpace.Gamma) ? Mathf.GammaToLinearSpace(base.settings.luminanceThreshold.value) : base.settings.luminanceThreshold.value);
			propertySheet.properties.SetFloat("_Threshold", value);
			propertySheet.properties.SetFloat("_Blur", base.settings.blurAmount);
			propertySheet.properties.SetFloat("_Intensity", base.settings.intensity);
			context.command.GetTemporaryRT(aoTexID, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat);
			context.command.BlitFullscreenTriangle(context.source, aoTexID, propertySheet, 0);
			int num = Shader.PropertyToID("_Temp1");
			int num2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(num, context.screenWidth / (int)base.settings.downscaling, context.screenHeight / (int)base.settings.downscaling, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(num2, context.screenWidth / (int)base.settings.downscaling, context.screenHeight / (int)base.settings.downscaling, 0, FilterMode.Bilinear);
			command.Blit(aoTexID, num);
			for (int i = 0; i < (int)base.settings.iterations; i++)
			{
				command.SetGlobalVector("_BlurOffsets", new Vector4((float)base.settings.blurAmount / (float)context.screenWidth, 0f, 0f, 0f));
				context.command.BlitFullscreenTriangle(num, num2, propertySheet, 1);
				command.SetGlobalVector("_BlurOffsets", new Vector4(0f, (float)base.settings.blurAmount / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(num2, num, propertySheet, 1);
			}
			context.command.SetGlobalTexture("_AO", num);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.aoOnly ? 3 : 2);
			context.command.ReleaseTemporaryRT(num);
			context.command.ReleaseTemporaryRT(num2);
			context.command.ReleaseTemporaryRT(aoTexID);
		}
	}
}
