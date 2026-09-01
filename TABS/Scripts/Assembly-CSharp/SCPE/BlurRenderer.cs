using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class BlurRenderer : PostProcessEffectRenderer<Blur>
	{
		private enum Pass
		{
			Blend = 0,
			Gaussian = 1,
			Box = 2
		}

		private Shader shader;

		private int screenCopyID;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Blur");
			screenCopyID = Shader.PropertyToID("_ScreenCopyTexture");
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			CommandBuffer command = context.command;
			command.GetTemporaryRT(screenCopyID, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat);
			command.BlitFullscreenTriangle(context.source, screenCopyID, propertySheet, 0);
			int num = Shader.PropertyToID("_Temp1");
			int num2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(num, context.screenWidth / (int)base.settings.downscaling, context.screenHeight / (int)base.settings.downscaling, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(num2, context.screenWidth / (int)base.settings.downscaling, context.screenHeight / (int)base.settings.downscaling, 0, FilterMode.Bilinear);
			command.Blit(screenCopyID, num);
			command.ReleaseTemporaryRT(screenCopyID);
			int pass = (((Blur.BlurMethod)base.settings.mode == Blur.BlurMethod.Gaussian) ? 1 : 2);
			for (int i = 0; i < (int)base.settings.iterations; i++)
			{
				if ((int)base.settings.iterations > 12)
				{
					return;
				}
				command.SetGlobalVector("_BlurOffsets", new Vector4((float)base.settings.amount / (float)context.screenWidth, 0f, 0f, 0f));
				context.command.BlitFullscreenTriangle(num, num2, propertySheet, pass);
				command.SetGlobalVector("_BlurOffsets", new Vector4(0f, (float)base.settings.amount / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(num2, num, propertySheet, pass);
				if ((bool)base.settings.highQuality)
				{
					command.SetGlobalVector("_BlurOffsets", new Vector4((float)base.settings.amount / (float)context.screenWidth, 0f, 0f, 0f));
					context.command.BlitFullscreenTriangle(num, num2, propertySheet, pass);
					command.SetGlobalVector("_BlurOffsets", new Vector4(0f, (float)base.settings.amount / (float)context.screenHeight, 0f, 0f));
					context.command.BlitFullscreenTriangle(num2, num, propertySheet, pass);
				}
			}
			command.BlitFullscreenTriangle(num, context.destination, propertySheet, 0);
			command.ReleaseTemporaryRT(num);
			command.ReleaseTemporaryRT(num2);
		}
	}
}
