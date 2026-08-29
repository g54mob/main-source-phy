using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	[VolumeComponentMenu("HDRP Volume Post-Process Pack/Gaussian Blur/Step2 (Vertical)")]
	public sealed class GaussianBlurStep2Vertical : GaussianBlurBase
	{
		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			if (IsActive())
			{
				SetMaterialProperties(m_Material);
				cmd.Blit(source, destination, m_Material, 1);
			}
		}
	}
}
