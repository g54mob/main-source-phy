using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class KuwaharaRenderer : PostProcessEffectRenderer<Kuwahara>
	{
		private Shader KuwaharaShader;

		public override void Init()
		{
			KuwaharaShader = Shader.Find("Hidden/SC Post Effects/Kuwahara");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			if (context.camera.orthographic)
			{
				base.settings.mode.value = Kuwahara.KuwaharaMode.Regular;
			}
			PropertySheet propertySheet = context.propertySheets.Get(KuwaharaShader);
			propertySheet.properties.SetFloat("_Radius", (int)base.settings.radius);
			propertySheet.properties.SetFloat("_FadeDistance", base.settings.fadeDistance);
			propertySheet.properties.SetVector("_DistanceParams", new Vector4(base.settings.fadeDistance, base.settings.invertFadeDistance ? 1 : 0, 0f, 0f));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value);
		}

		public override DepthTextureMode GetCameraFlags()
		{
			if (base.settings.mode.value == Kuwahara.KuwaharaMode.DepthFade)
			{
				return DepthTextureMode.Depth;
			}
			return DepthTextureMode.None;
		}
	}
}
