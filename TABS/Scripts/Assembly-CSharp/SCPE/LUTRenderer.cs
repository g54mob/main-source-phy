using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class LUTRenderer : PostProcessEffectRenderer<LUT>
	{
		private Shader shader;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/LUT");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			if ((bool)base.settings.lutNear.value)
			{
				propertySheet.properties.SetTexture("_LUT_Near", base.settings.lutNear);
				propertySheet.properties.SetVector("_LUT_Params", new Vector4(1f / (float)base.settings.lutNear.value.width, 1f / (float)base.settings.lutNear.value.height, (float)base.settings.lutNear.value.height - 1f, base.settings.intensity));
			}
			if (base.settings.mode.value == LUT.Mode.DistanceBased)
			{
				propertySheet.properties.SetFloat("_Distance", base.settings.distance);
				if ((bool)base.settings.lutFar.value)
				{
					propertySheet.properties.SetTexture("_LUT_Far", base.settings.lutFar);
				}
			}
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value);
		}
	}
}
