using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class DoubleVisionRenderer : PostProcessEffectRenderer<DoubleVision>
	{
		private Shader DoubleVisionShader;

		public override void Init()
		{
			DoubleVisionShader = Shader.Find("Hidden/SC Post Effects/Double Vision");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(DoubleVisionShader);
			propertySheet.properties.SetFloat("_Amount", (float)base.settings.intensity / 10f);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value);
		}
	}
}
