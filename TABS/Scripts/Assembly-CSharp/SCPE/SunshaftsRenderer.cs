using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	internal sealed class SunshaftsRenderer : PostProcessEffectRenderer<Sunshafts>
	{
		private enum Pass
		{
			SkySource = 0,
			RadialBlur = 1,
			Blend = 2
		}

		private Shader shader;

		private int skyboxBufferID;

		public override void Init()
		{
			shader = Shader.Find("Hidden/SC Post Effects/Sun Shafts");
			skyboxBufferID = Shader.PropertyToID("_SkyboxBuffer");
		}

		public override void Release()
		{
			base.Release();
		}

		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(shader);
			CommandBuffer command = context.command;
			float z = (base.settings.useCasterIntensity ? SunshaftCaster.intensity : base.settings.sunShaftIntensity.value);
			Vector3 vector = Vector3.one * 0.5f;
			vector = ((!(Sunshafts.sunPosition != Vector3.zero)) ? new Vector3(0.5f, 0.5f, 0f) : context.camera.WorldToViewportPoint(Sunshafts.sunPosition));
			propertySheet.properties.SetVector("_SunPosition", new Vector4(vector.x, vector.y, z, base.settings.falloff));
			Color color = (base.settings.useCasterColor ? SunshaftCaster.color : base.settings.sunColor.value);
			propertySheet.properties.SetFloat("_BlendMode", (float)base.settings.blendMode.value);
			propertySheet.properties.SetColor("_SunColor", (vector.z >= 0f) ? color : new Color(0f, 0f, 0f, 0f));
			propertySheet.properties.SetColor("_SunThreshold", base.settings.sunThreshold);
			int value = (int)base.settings.resolution.value;
			context.command.GetTemporaryRT(skyboxBufferID, context.width / 2, context.height / 2, 0, FilterMode.Bilinear, context.sourceFormat);
			context.command.BlitFullscreenTriangle(context.source, skyboxBufferID, propertySheet, 0);
			command.SetGlobalTexture("_SunshaftBuffer", skyboxBufferID);
			command.BeginSample("Sunshafts blur");
			int num = Shader.PropertyToID("_Temp1");
			int num2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(num, context.width / value, context.height / value, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(num2, context.width / value, context.height / value, 0, FilterMode.Bilinear);
			command.Blit(skyboxBufferID, num);
			float num3 = (float)base.settings.length * 0.0013020834f;
			int num4 = ((!base.settings.highQuality) ? 1 : 2);
			float num5 = (base.settings.highQuality ? ((float)base.settings.length / 2.5f) : ((float)base.settings.length));
			for (int i = 0; i < num4; i++)
			{
				context.command.BlitFullscreenTriangle(num, num2, propertySheet, 1);
				num3 = num5 * (((float)i * 2f + 1f) * 6f) / (float)context.screenWidth;
				propertySheet.properties.SetFloat("_BlurRadius", num3);
				context.command.BlitFullscreenTriangle(num2, num, propertySheet, 1);
				num3 = num5 * (((float)i * 2f + 2f) * 6f) / (float)context.screenWidth;
				propertySheet.properties.SetFloat("_BlurRadius", num3);
			}
			command.EndSample("Sunshafts blur");
			command.SetGlobalTexture("_SunshaftBuffer", num);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 2);
			command.ReleaseTemporaryRT(num);
			command.ReleaseTemporaryRT(num2);
			command.ReleaseTemporaryRT(skyboxBufferID);
		}
	}
}
