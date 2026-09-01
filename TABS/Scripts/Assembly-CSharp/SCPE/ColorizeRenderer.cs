using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class ColorizeRenderer : PostProcessEffectRenderer<Colorize>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Colorize");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			if ((bool)base.settings.colorRamp.value)
			{
				propertySheet.properties.SetTexture("_ColorRamp", base.settings.colorRamp);
			}
			propertySheet.properties.SetFloat("_Intensity", base.settings.intensity);
			propertySheet.properties.SetFloat("_BlendMode", (float)base.settings.mode.value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
		}
	}
}
