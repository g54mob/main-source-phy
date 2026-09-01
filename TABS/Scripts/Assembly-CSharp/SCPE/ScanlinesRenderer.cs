using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class ScanlinesRenderer : PostProcessEffectRenderer<Scanlines>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Scanlines");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.amount, (float)base.settings.intensity / 1000f, (float)base.settings.speed * 8f, 0f));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
		}
	}
}
