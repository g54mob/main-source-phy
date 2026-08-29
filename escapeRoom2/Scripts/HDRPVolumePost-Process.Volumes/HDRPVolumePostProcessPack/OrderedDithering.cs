using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Ordered Dithering")]
	public sealed class OrderedDithering : CustomPostProcessVolumeComponent, IPostProcessComponent, ICustomVolume
	{
		[Tooltip("Controls the intensity of the effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedIntParameter matrixSize = new ClampedIntParameter(4, 0, 256);

		public ClampedIntParameter colorStep = new ClampedIntParameter(8, 2, 256);

		public ClampedIntParameter ditherThreshold = new ClampedIntParameter(8, 0, 63);

		private Material m_Material;

		private const string kShaderName = "Hidden/HDRP Volume Post-Process Pack/OrderedDithering";

		private readonly Dictionary<int, float[]> OrderMatrixDictionary = new Dictionary<int, float[]>
		{
			{
				2,
				new float[64]
				{
					0f, 2f, 3f, 1f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f
				}
			},
			{
				4,
				new float[64]
				{
					0f, 8f, 2f, 10f, 12f, 4f, 14f, 6f, 3f, 11f,
					1f, 9f, 15f, 7f, 13f, 5f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f
				}
			},
			{
				8,
				new float[64]
				{
					0f, 48f, 12f, 60f, 3f, 51f, 15f, 63f, 32f, 16f,
					44f, 28f, 35f, 19f, 47f, 31f, 8f, 56f, 4f, 52f,
					11f, 59f, 7f, 55f, 40f, 24f, 36f, 20f, 43f, 27f,
					39f, 23f, 2f, 50f, 14f, 62f, 1f, 49f, 13f, 61f,
					34f, 18f, 46f, 30f, 33f, 17f, 45f, 29f, 10f, 58f,
					6f, 54f, 9f, 57f, 5f, 53f, 42f, 26f, 38f, 22f,
					41f, 25f, 37f, 21f
				}
			}
		};

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public string ShaderName => "Hidden/HDRP Volume Post-Process Pack/OrderedDithering";

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return intensity.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/HDRP Volume Post-Process Pack/OrderedDithering") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/HDRP Volume Post-Process Pack/OrderedDithering"));
			}
			else
			{
				Debug.LogError("Unable to find shader 'Hidden/HDRP Volume Post-Process Pack/OrderedDithering'. Post Process Volume OrderedDithering is unable to load.");
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				m_Material.SetFloat("_Intensity", intensity.value);
				m_Material.SetFloatArray("_OrderedMatrix", OrderMatrixDictionary[matrixSize.value]);
				m_Material.SetInt("_MatrixSize", matrixSize.value);
				m_Material.SetInt("_ColorStep", colorStep.value);
				m_Material.SetInt("_DitherThrethold", ditherThreshold.value);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
