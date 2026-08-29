namespace Meta.XR.EnvironmentDepth
{
	internal interface IDepthProvider
	{
		bool IsSupported { get; }

		bool RemoveHands { set; }

		void SetDepthEnabled(bool isEnabled, bool removeHands);

		DepthFrameDesc GetFrameDesc(int eye);

		bool GetDepthTextureId(ref uint textureId);
	}
}
