using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class TiltShiftRenderer : PostProcessEffectRenderer<TiltShift>
	{
		private Shader shader;

		private int screenCopyID;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Tilt Shift");
			screenCopyID = Shader.PropertyToID("_ScreenCopyTexture");
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			CommandBuffer command = context.command;
			propertySheet.properties.SetFloat("_Size", base.settings.areaSize);
			propertySheet.properties.SetFloat("_Amount", base.settings.amount);
			context.command.GetTemporaryRT(screenCopyID, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat);
			command.BlitFullscreenTriangle(context.source, screenCopyID, propertySheet, (int)base.settings.mode.value);
			command.SetGlobalTexture("_BlurredTex", screenCopyID);
			command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 2);
			command.ReleaseTemporaryRT(screenCopyID);
		}
	}
}
