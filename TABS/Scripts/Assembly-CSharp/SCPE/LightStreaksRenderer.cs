using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	public sealed class LightStreaksRenderer : PostProcessEffectRenderer<LightStreaks>
	{
		private enum Pass
		{
			LuminanceDiff = 0,
			BlurFast = 1,
			Blur = 2,
			Blend = 3,
			Debug = 4
		}

		private Shader shader;

		private int emissionTex;

		private RenderTexture aoRT;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Light Streaks");
			emissionTex = Shader.PropertyToID("_BloomTex");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			CommandBuffer command = context.command;
			int pass = ((base.settings.quality.value == LightStreaks.Quality.Performance) ? 1 : 2);
			float x = Mathf.GammaToLinearSpace(base.settings.luminanceThreshold.value);
			propertySheet.properties.SetVector("_Params", new Vector4(x, base.settings.intensity, 0f, 0f));
			context.command.GetTemporaryRT(emissionTex, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat);
			context.command.BlitFullscreenTriangle(context.source, emissionTex, propertySheet, 0);
			int num = (int)base.settings.downscaling + 1;
			int num2 = Shader.PropertyToID("_Temp1");
			int num3 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(num2, context.width / num, context.height / num, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(num3, context.width / num, context.height / num, 0, FilterMode.Bilinear);
			command.Blit(emissionTex, num2);
			float num4 = Mathf.Clamp(base.settings.direction, -1f, 1f);
			float num5 = ((num4 < 0f) ? ((0f - num4) * 16f) : 0f);
			float num6 = ((num4 > 0f) ? (num4 * 8f) : 0f);
			int num7 = ((base.settings.quality.value == LightStreaks.Quality.Performance) ? ((int)base.settings.iterations * 3) : ((int)base.settings.iterations));
			for (int i = 0; i < num7; i++)
			{
				command.SetGlobalVector("_BlurOffsets", new Vector4(num5 * (float)base.settings.blur / (float)context.screenWidth, num6 / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(num2, num3, propertySheet, pass);
				command.SetGlobalVector("_BlurOffsets", new Vector4(num5 * (float)base.settings.blur * 2f / (float)context.screenWidth, num6 * 2f / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(num3, num2, propertySheet, pass);
			}
			context.command.SetGlobalTexture("_BloomTex", num2);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.debug ? 4 : 3);
			context.command.ReleaseTemporaryRT(num2);
			context.command.ReleaseTemporaryRT(num3);
			context.command.ReleaseTemporaryRT(emissionTex);
		}
	}
}
