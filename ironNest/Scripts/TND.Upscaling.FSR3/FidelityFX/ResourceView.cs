using UnityEngine.Rendering;

namespace FidelityFX
{
	public readonly struct ResourceView
	{
		public static readonly ResourceView Unassigned;

		public static readonly ResourceView None;

		public readonly RenderTargetIdentifier RenderTarget;

		public readonly RenderTextureSubElement SubElement;

		public readonly int MipLevel;

		public bool IsValid => false;

		public ResourceView(in RenderTargetIdentifier renderTarget, RenderTextureSubElement subElement = RenderTextureSubElement.Default, int mipLevel = 0)
		{
			RenderTarget = default(RenderTargetIdentifier);
			SubElement = default(RenderTextureSubElement);
			MipLevel = 0;
		}
	}
}
