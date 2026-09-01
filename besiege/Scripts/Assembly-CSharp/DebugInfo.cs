using UnityEngine;

public class DebugInfo : MonoBehaviour
{
	private void Start()
	{
		DynamicText component = GetComponent<DynamicText>();
		if ((bool)component)
		{
			component.size /= 2f;
			component.SetText(string.Concat("SystemInfo.deviceModel: ", SystemInfo.deviceModel, "\nSystemInfo.deviceName: ", SystemInfo.deviceName, "\nSystemInfo.deviceType: ", SystemInfo.deviceType, "\nSystemInfo.graphicsDeviceID: ", SystemInfo.graphicsDeviceID, "\nSystemInfo.graphicsDeviceName: ", SystemInfo.graphicsDeviceName, "\nSystemInfo.graphicsDeviceVendor: ", SystemInfo.graphicsDeviceVendor, "\nSystemInfo.graphicsDeviceVendorID: ", SystemInfo.graphicsDeviceVendorID, "\nSystemInfo.graphicsDeviceVersion: ", SystemInfo.graphicsDeviceVersion, "\nSystemInfo.graphicsMemorySize: ", SystemInfo.graphicsMemorySize, "\nSystemInfo.graphicsShaderLevel: ", SystemInfo.graphicsShaderLevel, "\nSystemInfo.operatingSystem: ", SystemInfo.operatingSystem, "\nSystemInfo.processorCount: ", SystemInfo.processorCount, "\nSystemInfo.processorType: ", SystemInfo.processorType, "\nSystemInfo.supports...:\nRenderTargetCount,3DTextures,Accelerometer,ComputeShaders: ", SystemInfo.supportedRenderTargetCount, ",", SystemInfo.supports3DTextures, ",", SystemInfo.supportsAccelerometer, ",", SystemInfo.supportsComputeShaders, "\nGyroscope,ImageEffects,Instancing,RenderTextures: ", SystemInfo.supportsGyroscope, ",", SystemInfo.supportsImageEffects, ",", SystemInfo.supportsInstancing, ",", SystemInfo.supportsRenderTextures, "\nnpotSupport,Shadows,Stencil,Vibration: ", SystemInfo.npotSupport, ",", SystemInfo.supportsShadows, ",", SystemInfo.supportsStencil, ",", SystemInfo.supportsVibration, "\nSystemInfo.systemMemorySize: ", SystemInfo.systemMemorySize, "\nApplication.isEditor: ", Application.isEditor, "\nApplication.isWebPlayer: ", Application.isWebPlayer, "\nApplication.platform: ", Application.platform.ToString(), "\nApplication.unityVersion: ", Application.unityVersion, "\nApplication.dataPath: ", Application.dataPath, "\nApplication.persistentDataPath:\n", Application.persistentDataPath, "\n-"));
		}
	}
}
