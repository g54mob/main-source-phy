using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class RadialBlurRenderer : PostProcessEffectRenderer<RadialBlur>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Radial Blur");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			_ = context.command;
			propertySheet.properties.SetFloat("_Amount", (float)base.settings.amount / 50f);
			propertySheet.properties.SetFloat("_Iterations", (int)base.settings.iterations);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
		}
	}
}
