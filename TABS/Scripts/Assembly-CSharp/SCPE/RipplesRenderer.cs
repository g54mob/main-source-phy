using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class RipplesRenderer : PostProcessEffectRenderer<Ripples>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Ripples");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			propertySheet.properties.SetFloat("_Strength", (float)base.settings.strength * 0.01f);
			propertySheet.properties.SetFloat("_Distance", (float)base.settings.distance * 0.01f);
			propertySheet.properties.SetFloat("_Speed", base.settings.speed);
			propertySheet.properties.SetVector("_Size", new Vector2(base.settings.width, base.settings.height));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value);
		}
	}
}
