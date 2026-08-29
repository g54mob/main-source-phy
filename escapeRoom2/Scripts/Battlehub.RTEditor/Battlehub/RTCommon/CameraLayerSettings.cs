using System;

namespace Battlehub.RTCommon
{
	[Serializable]
	public struct CameraLayerSettings
	{
		public static readonly CameraLayerSettings Default = new CameraLayerSettings(20, 21, 4, 17, 18, 19, 16);

		public int ResourcePreviewLayer;

		public int RuntimeGraphicsLayer;

		public int MaxGraphicsLayers;

		public int AllScenesLayer;

		public int ExtraLayer2;

		public int ExtraLayer;

		public int UIBackgroundLayer;

		public int RaycastMask => ~(((1 << MaxGraphicsLayers) - 1 << RuntimeGraphicsLayer) | (1 << AllScenesLayer) | (1 << ExtraLayer) | (1 << ExtraLayer2) | (1 << ResourcePreviewLayer));

		public CameraLayerSettings(int resourcePreviewLayer, int runtimeGraphicsLayer, int maxLayers, int allSceneLayer, int extraLayer, int hiddenLayer, int uiBackgroundLayer)
		{
			ResourcePreviewLayer = resourcePreviewLayer;
			RuntimeGraphicsLayer = runtimeGraphicsLayer;
			MaxGraphicsLayers = maxLayers;
			AllScenesLayer = allSceneLayer;
			ExtraLayer = extraLayer;
			ExtraLayer2 = hiddenLayer;
			UIBackgroundLayer = uiBackgroundLayer;
		}
	}
}
