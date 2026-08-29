using System;
using System.Collections;
using UnityEngine;

public class OVRSpectatorModeDomeTest : MonoBehaviour
{
	private bool inited;

	public Camera defaultExternalCamera;

	private OVRPlugin.Fovf defaultFov;

	public Transform SpectatorAnchor;

	public Transform Head;

	private void Awake()
	{
	}

	private void Start()
	{
		if (!defaultExternalCamera)
		{
			Debug.LogWarning("defaultExternalCamera undefined");
		}
		if (!OVRManager.instance.enableMixedReality)
		{
			OVRManager.instance.enableMixedReality = true;
		}
	}

	private void Initialize()
	{
		if (!inited && OVRPlugin.IsMixedRealityInitialized())
		{
			OVRPlugin.ResetDefaultExternalCamera();
			Debug.LogFormat("GetExternalCameraCount before adding manual external camera {0}", OVRPlugin.GetExternalCameraCount());
			UpdateDefaultExternalCamera();
			Debug.LogFormat("GetExternalCameraCount after adding manual external camera {0}", OVRPlugin.GetExternalCameraCount());
			OVRPlugin.GetMixedRealityCameraInfo(0, out var _, out var cameraIntrinsics);
			defaultFov = cameraIntrinsics.FOVPort;
			inited = true;
		}
	}

	private void UpdateDefaultExternalCamera()
	{
		OVRPlugin.CameraIntrinsics cameraIntrinsics = default(OVRPlugin.CameraIntrinsics);
		OVRPlugin.CameraExtrinsics cameraExtrinsics = default(OVRPlugin.CameraExtrinsics);
		cameraIntrinsics.IsValid = OVRPlugin.Bool.True;
		cameraIntrinsics.LastChangedTimeSeconds = Time.time;
		float num = defaultExternalCamera.fieldOfView * (MathF.PI / 180f);
		float num2 = Mathf.Atan(Mathf.Tan(num * 0.5f) * 1.7777778f) * 2f;
		OVRPlugin.Fovf fOVPort = default(OVRPlugin.Fovf);
		fOVPort.UpTan = (fOVPort.DownTan = Mathf.Tan(num * 0.5f));
		fOVPort.LeftTan = (fOVPort.RightTan = Mathf.Tan(num2 * 0.5f));
		cameraIntrinsics.FOVPort = fOVPort;
		cameraIntrinsics.VirtualNearPlaneDistanceMeters = defaultExternalCamera.nearClipPlane;
		cameraIntrinsics.VirtualFarPlaneDistanceMeters = defaultExternalCamera.farClipPlane;
		cameraIntrinsics.ImageSensorPixelResolution.w = 1920;
		cameraIntrinsics.ImageSensorPixelResolution.h = 1080;
		cameraExtrinsics.IsValid = OVRPlugin.Bool.True;
		cameraExtrinsics.LastChangedTimeSeconds = Time.time;
		cameraExtrinsics.CameraStatusData = OVRPlugin.CameraStatus.CameraStatus_Calibrated;
		cameraExtrinsics.AttachedToNode = OVRPlugin.Node.None;
		OVRCameraRig componentInParent = Camera.main.GetComponentInParent<OVRCameraRig>();
		if ((bool)componentInParent)
		{
			OVRPose oVRPose = componentInParent.trackingSpace.ToOVRPose();
			OVRPose oVRPose2 = defaultExternalCamera.transform.ToOVRPose();
			cameraExtrinsics.RelativePose = (oVRPose.Inverse() * oVRPose2).ToPosef();
		}
		else
		{
			cameraExtrinsics.RelativePose = OVRPlugin.Posef.identity;
		}
		if (!OVRPlugin.SetDefaultExternalCamera("UnityExternalCamera", ref cameraIntrinsics, ref cameraExtrinsics))
		{
			Debug.LogError("SetDefaultExternalCamera() failed");
		}
	}

	private void UpdateSpectatorCameraStatus()
	{
	}

	private Vector3 SpectatorCameraDomePosition(Vector3 spectatorAnchorPosition, float d, float e, float p)
	{
		float num = d * Mathf.Cos(MathF.PI / 180f * e) * Mathf.Cos(MathF.PI / 180f * p);
		float num2 = d * Mathf.Sin(MathF.PI / 180f * e);
		return new Vector3(z: d * Mathf.Cos(MathF.PI / 180f * e) * Mathf.Sin(MathF.PI / 180f * p) + spectatorAnchorPosition.z, x: num + spectatorAnchorPosition.x, y: num2 + spectatorAnchorPosition.y);
	}

	private IEnumerator TimerCoroutine()
	{
		yield return new WaitForSeconds(2f);
	}

	private void Update()
	{
		if (!inited)
		{
			Initialize();
		}
		else if ((bool)defaultExternalCamera && OVRPlugin.IsMixedRealityInitialized())
		{
			UpdateSpectatorCameraStatus();
			UpdateDefaultExternalCamera();
			OVRPlugin.OverrideExternalCameraFov(0, useOverriddenFov: false, default(OVRPlugin.Fovf));
			OVRPlugin.OverrideExternalCameraStaticPose(0, useOverriddenPose: false, OVRPlugin.Posef.identity);
		}
	}

	private void OnApplicationPause()
	{
	}

	private void OnApplicationQuit()
	{
	}
}
