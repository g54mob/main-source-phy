using System;

namespace Meta.XR.EnvironmentDepth
{
	internal class DepthProviderNotSupported : IDepthProvider
	{
		public bool IsSupported => false;

		public bool RemoveHands
		{
			set
			{
			}
		}

		public void SetDepthEnabled(bool isEnabled, bool removeHands)
		{
		}

		public DepthFrameDesc GetFrameDesc(int eye)
		{
			throw new NotSupportedException();
		}

		public bool GetDepthTextureId(ref uint textureId)
		{
			throw new NotSupportedException();
		}
	}
}
