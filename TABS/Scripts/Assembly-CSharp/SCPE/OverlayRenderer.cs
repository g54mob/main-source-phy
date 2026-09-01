using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class OverlayRenderer : PostProcessEffectRenderer<Overlay>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Overlay");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			if ((bool)base.settings.overlayTex.value)
			{
				propertySheet.properties.SetTexture("_OverlayTex", base.settings.overlayTex);
			}
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.intensity, Mathf.Pow((float)base.settings.tiling + 1f, 2f), base.settings.autoAspect ? 1f : 0f, (float)base.settings.blendMode.value));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
		}
	}
}
