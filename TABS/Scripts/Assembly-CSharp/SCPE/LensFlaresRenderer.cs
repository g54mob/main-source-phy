using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	public sealed class LensFlaresRenderer : PostProcessEffectRenderer<LensFlares>
	{
		private enum Pass
		{
			LuminanceDiff = 0,
			Ghosting = 1,
			Blur = 2,
			Blend = 3,
			Debug = 4
		}

		private Shader shader;

		private int emissionTex;

		private int flaresTex;

		private RenderTexture aoRT;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Lensflares");
			emissionTex = Shader.PropertyToID("_BloomTex");
			flaresTex = Shader.PropertyToID("_FlaresTex");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			CommandBuffer command = context.command;
			propertySheet.properties.SetFloat("_Intensity", base.settings.intensity);
			float value = Mathf.GammaToLinearSpace(base.settings.luminanceThreshold.value);
			propertySheet.properties.SetFloat("_Threshold", value);
			propertySheet.properties.SetFloat("_Distance", base.settings.distance);
			propertySheet.properties.SetFloat("_Falloff", base.settings.falloff);
			propertySheet.properties.SetFloat("_Ghosts", (int)base.settings.iterations);
			propertySheet.properties.SetFloat("_HaloSize", base.settings.haloSize);
			propertySheet.properties.SetFloat("_HaloWidth", base.settings.haloWidth);
			propertySheet.properties.SetFloat("_ChromaticAbberation", base.settings.chromaticAbberation);
			if ((bool)base.settings.colorTex.value)
			{
				propertySheet.properties.SetTexture("_ColorTex", base.settings.colorTex);
			}
			else
			{
				propertySheet.properties.SetTexture("_ColorTex", Texture2D.whiteTexture);
			}
			if ((bool)base.settings.maskTex.value)
			{
				propertySheet.properties.SetTexture("_MaskTex", base.settings.maskTex);
			}
			else
			{
				propertySheet.properties.SetTexture("_MaskTex", Texture2D.whiteTexture);
			}
			context.command.GetTemporaryRT(emissionTex, context.width, context.height, 0, FilterMode.Bilinear, RenderTextureFormat.DefaultHDR);
			context.command.BlitFullscreenTriangle(context.source, emissionTex, propertySheet, 0);
			context.command.SetGlobalTexture("_BloomTex", emissionTex);
			context.command.GetTemporaryRT(flaresTex, context.width, context.height, 0, FilterMode.Bilinear, RenderTextureFormat.DefaultHDR);
			context.command.BlitFullscreenTriangle(emissionTex, flaresTex, propertySheet, 1);
			context.command.SetGlobalTexture("_FlaresTex", flaresTex);
			int num = Shader.PropertyToID("_Temp1");
			int num2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(num, context.width / 2, context.height / 2, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(num2, context.width / 2, context.height / 2, 0, FilterMode.Bilinear);
			command.Blit(flaresTex, num);
			command.ReleaseTemporaryRT(flaresTex);
			for (int i = 0; i < (int)base.settings.passes; i++)
			{
				command.SetGlobalVector("_BlurOffsets", new Vector4((float)base.settings.blur / (float)context.screenWidth, 0f, 0f, 0f));
				context.command.BlitFullscreenTriangle(num, num2, propertySheet, 2);
				command.SetGlobalVector("_BlurOffsets", new Vector4(0f, (float)base.settings.blur / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(num2, num, propertySheet, 2);
			}
			context.command.SetGlobalTexture("_FlaresTex", num);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.debug ? 4 : 3);
			context.command.ReleaseTemporaryRT(emissionTex);
			context.command.ReleaseTemporaryRT(flaresTex);
			context.command.ReleaseTemporaryRT(num);
			context.command.ReleaseTemporaryRT(num2);
		}
	}
}
