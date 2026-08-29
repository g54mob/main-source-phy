using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Gaussian Blur/Step1 (Horizontal)")]
	public sealed class GaussianBlurStep1Horizontal : GaussianBlurBase
	{
		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (IsActive())
			{
				SetMaterialProperties(m_Material);
				cmd.Blit(source, destination, m_Material, 0);
			}
		}
	}
}
