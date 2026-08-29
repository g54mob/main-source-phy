using System;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IRenderPipelineCameraUtility
	{
		event Action<Camera, bool> PostProcessingEnabled;

		void Stack(Camera baseCamera, Camera overlayCamera);

		void RequiresDepthTexture(Camera camera, bool value);

		bool IsPostProcessingEnabled(Camera camera);

		void EnablePostProcessing(Camera camera, bool value);

		void SetBackgroundColor(Camera camera, Color color);

		void ResetCullingMask(Camera camera);
	}
}
