using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Color Effects/CGA Filter")]
	public sealed class CGAFilter : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public enum ColorPalette
		{
			Palette1 = 0,
			Palette2 = 1,
			Palette3 = 2,
			Palette4 = 3,
			Palette5 = 4,
			Palette6 = 5,
			Palette7 = 6,
			Palette8 = 7
		}

		[Serializable]
		public sealed class ColorPaletteTypeParam : VolumeParameter<ColorPalette>
		{
		}

		public BoolParameter enabled = new BoolParameter(value: false);

		public ClampedFloatParameter gamma = new ClampedFloatParameter(0.5f, 0f, 2.5f);

		public ColorPaletteTypeParam colorPalette = new ColorPaletteTypeParam
		{
			value = ColorPalette.Palette1
		};

		private Material m_Material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (m_Material != null)
			{
				return enabled.value;
			}
			return false;
		}

		public override void Setup()
		{
			if (Shader.Find("Hidden/InanEvin/RichFX/CGAFilter") != null)
			{
				m_Material = new Material(Shader.Find("Hidden/InanEvin/RichFX/CGAFilter"));
			}
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (!(m_Material == null))
			{
				(new Vector4[1])[0] = Vector4.zero;
				if (colorPalette.value == ColorPalette.Palette1)
				{
					m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes[0]);
				}
				else if (colorPalette.value == ColorPalette.Palette2)
				{
					m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes[1]);
				}
				if (colorPalette.value == ColorPalette.Palette3)
				{
					m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes[2]);
				}
				if (colorPalette.value == ColorPalette.Palette4)
				{
					m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes[3]);
				}
				if (colorPalette.value == ColorPalette.Palette5)
				{
					m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes[4]);
				}
				if (colorPalette.value == ColorPalette.Palette6)
				{
					m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes[5]);
				}
				if (colorPalette.value == ColorPalette.Palette7)
				{
					m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes[6]);
				}
				if (colorPalette.value == ColorPalette.Palette8)
				{
					m_Material.SetVectorArray("_Colors", ColorPalettes.colorPalettes[7]);
				}
				m_Material.SetFloat("_Intensity", 1f);
				m_Material.SetFloat("_Gamma", gamma.value);
				m_Material.SetTexture("_InputTexture", source);
				HDUtils.DrawFullScreen(cmd, m_Material, destination);
			}
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
