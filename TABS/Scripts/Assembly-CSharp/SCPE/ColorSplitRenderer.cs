using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class ColorSplitRenderer : PostProcessEffectRenderer<ColorSplit>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Color Split");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			propertySheet.properties.SetFloat("_Offset", (float)base.settings.offset / 100f);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value);
		}
	}
}
