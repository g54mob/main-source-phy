using System;
using GLTFast.Schema;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace GLTFast.Export
{
	public static class KhrLightsPunctual
	{
		public static LightPunctual ConvertToLight(Light uLight)
		{
			LightPunctual lightPunctual = new LightPunctual
			{
				name = uLight.name
			};
			RenderPipeline renderPipeline = RenderPipelineUtils.RenderPipeline;
			LightType type = uLight.type;
			HDAdditionalLightData hDAdditionalLightData = null;
			if (renderPipeline == RenderPipeline.HighDefinition)
			{
				hDAdditionalLightData = uLight.gameObject.GetComponent<HDAdditionalLightData>();
			}
			switch (type)
			{
			case LightType.Spot:
				lightPunctual.SetLightType(LightPunctual.Type.Spot);
				lightPunctual.spot = new SpotLight
				{
					outerConeAngle = uLight.spotAngle * (MathF.PI / 180f) * 0.5f,
					innerConeAngle = uLight.innerSpotAngle * (MathF.PI / 180f) * 0.5f
				};
				break;
			case LightType.Directional:
				lightPunctual.SetLightType(LightPunctual.Type.Directional);
				break;
			case LightType.Point:
				lightPunctual.SetLightType(LightPunctual.Type.Point);
				break;
			default:
				lightPunctual.SetLightType(LightPunctual.Type.Spot);
				lightPunctual.spot = new SpotLight
				{
					outerConeAngle = MathF.PI / 8f,
					innerConeAngle = 0.30543262f
				};
				break;
			}
			lightPunctual.LightColor = uLight.color.linear;
			lightPunctual.range = GetLightRange(uLight, type);
			switch (renderPipeline)
			{
			case RenderPipeline.BuiltIn:
				lightPunctual.intensity = uLight.intensity * MathF.PI;
				break;
			case RenderPipeline.Universal:
				lightPunctual.intensity = uLight.intensity;
				break;
			case RenderPipeline.HighDefinition:
				if (hDAdditionalLightData == null)
				{
					lightPunctual.intensity = uLight.intensity;
					break;
				}
				switch (type)
				{
				case LightType.Spot:
				case LightType.Point:
					lightPunctual.intensity = LightUnitUtils.ConvertIntensity(uLight, uLight.intensity, uLight.lightUnit, LightUnit.Candela);
					break;
				case LightType.Directional:
					lightPunctual.intensity = LightUnitUtils.ConvertIntensity(uLight, uLight.intensity, uLight.lightUnit, LightUnit.Lux);
					break;
				default:
					lightPunctual.intensity = uLight.intensity;
					break;
				}
				break;
			default:
				lightPunctual.intensity = uLight.intensity;
				break;
			}
			return lightPunctual;
		}

		private static float GetLightRange(Light light, LightType lightType)
		{
			return light.range;
		}
	}
}
