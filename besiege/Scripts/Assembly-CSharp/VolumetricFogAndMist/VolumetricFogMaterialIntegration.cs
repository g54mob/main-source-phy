using UnityEngine;

namespace VolumetricFogAndMist
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(VolumetricFog))]
	public class VolumetricFogMaterialIntegration : MonoBehaviour
	{
		private enum PropertyType
		{
			Float = 0,
			Float3 = 1,
			Float4 = 2,
			Color = 3,
			Texture2D = 4,
			FloatArray = 5,
			Float4Array = 6,
			ColorArray = 7,
			Matrix4x4 = 8
		}

		private struct Properties
		{
			public string name;

			public PropertyType type;
		}

		private static Properties[] props = new Properties[29]
		{
			new Properties
			{
				name = "_NoiseTex",
				type = PropertyType.Texture2D
			},
			new Properties
			{
				name = "_FogAlpha",
				type = PropertyType.Float
			},
			new Properties
			{
				name = "_Color",
				type = PropertyType.Color
			},
			new Properties
			{
				name = "_FogDistance",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_FogData",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_FogWindDir",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_FogStepping",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_BlurTex",
				type = PropertyType.Texture2D
			},
			new Properties
			{
				name = "_FogVoidPosition",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_FogVoidData",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_FogAreaPosition",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_FogAreaData",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_FogOfWar",
				type = PropertyType.Texture2D
			},
			new Properties
			{
				name = "_FogOfWarCenter",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_FogOfWarSize",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_FogOfWarCenterAdjusted",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_FogPointLightPosition",
				type = PropertyType.Float4Array
			},
			new Properties
			{
				name = "_FogPointLightColor",
				type = PropertyType.ColorArray
			},
			new Properties
			{
				name = "_SunPosition",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_SunDir",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_SunColor",
				type = PropertyType.Float3
			},
			new Properties
			{
				name = "_FogScatteringData",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_FogScatteringData2",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_VolumetricFogSunDepthTexture",
				type = PropertyType.Texture2D
			},
			new Properties
			{
				name = "_VolumetricFogSunDepthTexture_TexelSize",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_VolumetricFogSunProj",
				type = PropertyType.Matrix4x4
			},
			new Properties
			{
				name = "_VolumetricFogSunWorldPos",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_VolumetricFogSunShadowsData",
				type = PropertyType.Float4
			},
			new Properties
			{
				name = "_Jitter",
				type = PropertyType.Float
			}
		};

		private static string[] keywords = new string[10] { "FOG_DISTANCE_ON", "FOG_AREA_SPHERE", "FOG_AREA_BOX", "FOG_VOID_SPHERE", "FOG_VOID_BOX", "FOG_OF_WAR_ON", "FOG_SCATTERING_ON", "FOG_BLUR_ON", "FOG_POINT_LIGHTS", "FOG_SUN_SHADOWS_ON" };

		public VolumetricFog fog;

		public Renderer[] materials;

		private void OnEnable()
		{
			fog = GetComponent<VolumetricFog>();
		}

		private void OnPreRender()
		{
			if (fog == null)
			{
				return;
			}
			Material fogMat = fog.fogMat;
			if (fogMat == null || materials == null || materials.Length == 0)
			{
				return;
			}
			for (int i = 0; i < props.Length; i++)
			{
				if (!fogMat.HasProperty(props[i].name))
				{
					continue;
				}
				switch (props[i].type)
				{
				case PropertyType.Color:
				{
					Color color = fogMat.GetColor(props[i].name);
					for (int n = 0; n < materials.Length; n++)
					{
						if (materials[n] != null && materials[n].sharedMaterial != null)
						{
							materials[n].sharedMaterial.SetColor(props[i].name, color);
						}
					}
					break;
				}
				case PropertyType.ColorArray:
					Debug.Log("Tries to get Color array");
					break;
				case PropertyType.FloatArray:
					Debug.Log("Tries to get Float array");
					break;
				case PropertyType.Float4Array:
					Debug.Log("Tries to get Float4Array");
					break;
				case PropertyType.Float:
				{
					for (int m = 0; m < materials.Length; m++)
					{
						if (materials[m] != null && materials[m].sharedMaterial != null)
						{
							materials[m].sharedMaterial.SetFloat(props[i].name, fogMat.GetFloat(props[i].name));
						}
					}
					break;
				}
				case PropertyType.Float3:
				case PropertyType.Float4:
				{
					for (int l = 0; l < materials.Length; l++)
					{
						if (materials[l] != null && materials[l].sharedMaterial != null)
						{
							materials[l].sharedMaterial.SetVector(props[i].name, fogMat.GetVector(props[i].name));
						}
					}
					break;
				}
				case PropertyType.Matrix4x4:
				{
					for (int k = 0; k < materials.Length; k++)
					{
						if (materials[k] != null && materials[k].sharedMaterial != null)
						{
							materials[k].sharedMaterial.SetMatrix(props[i].name, fogMat.GetMatrix(props[i].name));
						}
					}
					break;
				}
				case PropertyType.Texture2D:
				{
					for (int j = 0; j < materials.Length; j++)
					{
						if (materials[j] != null && materials[j].sharedMaterial != null)
						{
							materials[j].sharedMaterial.SetTexture(props[i].name, fogMat.GetTexture(props[i].name));
						}
					}
					break;
				}
				}
			}
			for (int num = 0; num < keywords.Length; num++)
			{
				if (fogMat.IsKeywordEnabled(keywords[num]))
				{
					for (int num2 = 0; num2 < materials.Length; num2++)
					{
						if (materials[num2] != null && materials[num2].sharedMaterial != null)
						{
							materials[num2].sharedMaterial.EnableKeyword(keywords[num]);
						}
					}
					continue;
				}
				for (int num3 = 0; num3 < materials.Length; num3++)
				{
					if (materials[num3] != null && materials[num3].sharedMaterial != null)
					{
						materials[num3].sharedMaterial.DisableKeyword(keywords[num]);
					}
				}
			}
		}
	}
}
