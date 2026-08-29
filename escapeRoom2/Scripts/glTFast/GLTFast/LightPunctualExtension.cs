using System;
using GLTFast.Schema;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace GLTFast
{
	public static class LightPunctualExtension
	{
		public static void ToUnityLight(this LightPunctual lightSource, Light lightDestination, float lightIntensityFactor)
		{
			switch (lightSource.GetLightType())
			{
			case LightPunctual.Type.Spot:
				lightDestination.type = LightType.Spot;
				break;
			case LightPunctual.Type.Directional:
				lightDestination.type = LightType.Directional;
				break;
			case LightPunctual.Type.Point:
				lightDestination.type = LightType.Point;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			case LightPunctual.Type.Unknown:
				break;
			}
			lightDestination.useColorTemperature = false;
			lightDestination.color = lightSource.LightColor.gamma;
			LightAssignIntensity(lightDestination, lightSource, lightIntensityFactor);
			lightDestination.range = ((lightSource.range > 0f) ? lightSource.range : 100000f);
			if (lightSource.GetLightType() == LightPunctual.Type.Spot)
			{
				lightDestination.spotAngle = lightSource.spot.outerConeAngle * 57.29578f * 2f;
				lightDestination.innerSpotAngle = lightSource.spot.innerConeAngle * 57.29578f * 2f;
			}
		}

		public static void ToLightPunctual(this Light lightSource, LightPunctual lightDestination, float lightIntensityFactor)
		{
			switch (lightSource.type)
			{
			case LightType.Spot:
				lightDestination.SetLightType(LightPunctual.Type.Spot);
				break;
			case LightType.Directional:
				lightDestination.SetLightType(LightPunctual.Type.Directional);
				break;
			case LightType.Point:
				lightDestination.SetLightType(LightPunctual.Type.Point);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			lightDestination.LightColor = lightSource.color;
			LightAssignIntensity(lightDestination, lightSource, lightIntensityFactor);
			lightDestination.range = ((lightSource.range > 0f) ? lightSource.range : 100000f);
			if (lightSource.type == LightType.Spot)
			{
				lightDestination.spot = lightDestination.spot ?? new SpotLight();
				lightDestination.spot.outerConeAngle = lightSource.spotAngle / 57.29578f * 0.5f;
				lightDestination.spot.innerConeAngle = lightSource.innerSpotAngle / 57.29578f * 0.5f;
			}
		}

		private static void LightAssignIntensity(Light lightDestination, LightPunctual lightSource, float lightIntensityFactor)
		{
			float num = lightSource.intensity * lightIntensityFactor;
			switch (RenderPipelineUtils.RenderPipeline)
			{
			case RenderPipeline.BuiltIn:
				lightDestination.intensity = num / MathF.PI;
				break;
			case RenderPipeline.Universal:
				lightDestination.intensity = num;
				break;
			case RenderPipeline.HighDefinition:
			{
				LightUnit lightUnit = ((lightSource.GetLightType() != LightPunctual.Type.Directional) ? LightUnit.Candela : LightUnit.Lux);
				lightDestination.gameObject.AddComponent<HDAdditionalLightData>();
				lightDestination.lightUnit = lightUnit;
				lightDestination.intensity = lightSource.intensity;
				break;
			}
			default:
				lightDestination.intensity = num;
				break;
			}
		}

		private static void LightAssignIntensity(LightPunctual lightDestination, Light lightSource, float lightIntensityFactor)
		{
			float num = lightSource.intensity / lightIntensityFactor;
			switch (RenderPipelineUtils.RenderPipeline)
			{
			case RenderPipeline.BuiltIn:
				lightDestination.intensity = num * MathF.PI;
				break;
			case RenderPipeline.Universal:
				lightDestination.intensity = num;
				break;
			case RenderPipeline.HighDefinition:
				lightDestination.intensity = lightSource.intensity;
				break;
			default:
				lightDestination.intensity = num;
				break;
			}
		}
	}
}
