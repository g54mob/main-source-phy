using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class RefractionRenderer : PostProcessEffectRenderer<Refraction>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Refraction");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			propertySheet.properties.SetFloat("_Amount", base.settings.amount);
			if ((bool)base.settings.refractionTex.value)
			{
				propertySheet.properties.SetTexture("_RefractionTex", base.settings.refractionTex);
			}
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.convertNormalMap ? 1 : 0);
		}
	}
}
