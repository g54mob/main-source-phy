using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class GradientRenderer : PostProcessEffectRenderer<Gradient>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Gradient");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			if ((bool)base.settings.gradientTex.value)
			{
				propertySheet.properties.SetTexture("_Gradient", base.settings.gradientTex);
			}
			propertySheet.properties.SetColor("_Color1", base.settings.color1);
			propertySheet.properties.SetColor("_Color2", base.settings.color2);
			propertySheet.properties.SetFloat("_Rotation", (float)base.settings.rotation * 6f);
			propertySheet.properties.SetFloat("_Intensity", base.settings.intensity);
			propertySheet.properties.SetFloat("_BlendMode", (float)base.settings.mode.value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.input.value);
		}
	}
}
