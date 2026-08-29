using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Others/Letter Box")]
	public sealed class LetterBox : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		public enum AspectRatioType
		{
			OneOne = 0,
			FiveFour = 1,
			FourThree = 2,
			ThreeTwo = 3,
			SixteenNine = 4,
			SixteenTen = 5,
			TwentyOneNine = 6,
			Custom = 7
		}

		[Serializable]
		public sealed class AspectRatioParameter : VolumeParameter<AspectRatioType>
		{
		}

		public BoolParameter enabled = new BoolParameter(value: false);

		public ColorParameter color = new ColorParameter(Color.black);

		public AspectRatioParameter aspectRatioType = new AspectRatioParameter
		{
			value = AspectRatioType.OneOne
		};

		public FloatParameter customAspect = new FloatParameter(1.25f);

		private Material m_Material;

		private float aspect;

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
			m_Material = CoreUtils.CreateEngineMaterial("Hidden/InanEvin/RichFX/LetterBox");
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (m_Material == null)
			{
				return;
			}
			if (aspectRatioType.value == AspectRatioType.Custom)
			{
				aspect = customAspect.value;
			}
			else if (aspectRatioType.value == AspectRatioType.FiveFour)
			{
				aspect = 1.25f;
			}
			if (aspectRatioType.value == AspectRatioType.FourThree)
			{
				aspect = 1.3333f;
			}
			if (aspectRatioType.value == AspectRatioType.OneOne)
			{
				aspect = 1f;
			}
			if (aspectRatioType.value == AspectRatioType.SixteenNine)
			{
				aspect = 1.77777f;
			}
			if (aspectRatioType.value == AspectRatioType.SixteenTen)
			{
				aspect = 1.6f;
			}
			if (aspectRatioType.value == AspectRatioType.ThreeTwo)
			{
				aspect = 1.5f;
			}
			if (aspectRatioType.value == AspectRatioType.TwentyOneNine)
			{
				aspect = 2.33333f;
			}
			float num = source.rtHandleProperties.currentViewportSize.x;
			float num2 = source.rtHandleProperties.currentViewportSize.y;
			float num3 = num / num2;
			float num4 = 0f;
			int shaderPassId = 0;
			m_Material.SetColor("_Color", color.value);
			m_Material.SetTexture("_InputTexture", source);
			if (num3 < aspect - 0.01f)
			{
				num4 = (num2 - num / aspect) * 0.5f / num2;
			}
			else
			{
				if (!(num3 > aspect + 0.01f))
				{
					m_Material.SetFloat("_Offset", 0f);
					m_Material.SetFloat("_OffsetInv", 1f);
					HDUtils.DrawFullScreen(cmd, m_Material, destination);
					return;
				}
				num4 = (num - num2 * aspect) * 0.5f / num;
				shaderPassId = 1;
			}
			m_Material.SetFloat("_Offset", num4);
			m_Material.SetFloat("_OffsetInv", 1f - num4);
			HDUtils.DrawFullScreen(cmd, m_Material, destination, null, shaderPassId);
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(m_Material);
		}
	}
}
