using System;
using System.Collections.Generic;
using UnityEngine;

public class PineAR
{
	public static Texture2D textureY;

	public static Texture2D textureCbCr;

	public static Texture2D textureStencil;

	public static Texture2D textureDepth;

	public static ARTrackingState trackingState = ARTrackingState.ARTrackingStateNotAvailable;

	public static ARTrackingStateReason trackingStateReason = ARTrackingStateReason.ARTrackingStateReasonNone;

	public static float ambientIntensity = 1f;

	public static float ambientColorTemperature = 0f;

	public static Vector3 cameraPosition = new Vector3(0f, 1.3f, 0f);

	public static Quaternion cameraRotation = Quaternion.identity;

	public static Matrix4x4 projectionMatrix = Matrix4x4.identity;

	public static Matrix4x4 displayTransform = Matrix4x4.identity;

	public static List<ARPlane> planes = new List<ARPlane>();

	public static List<AREnvironmentProbe> probes = new List<AREnvironmentProbe>();

	public static List<ARAnchor> anchors = new List<ARAnchor>();

	public static Vector3[] pointCloudVertices = new Vector3[0];

	public static bool inited = false;

	public static bool running = false;

	private static float nearZ = 0.01f;

	private static float farZ = 30f;

	private static Vector3 lastMousePosition;

	private static float currentRotationX = 0f;

	private static float currentRotationY = 0f;

	private static float arInitTimer;

	private static int ptrCounter = 1;

	public static void init()
	{
		Debug.Log("Simulated AR session");
		inited = true;
		trackingState = ARTrackingState.ARTrackingStateLimited;
		trackingStateReason = ARTrackingStateReason.ARTrackingStateReasonInitializing;
		arInitTimer = 1f;
	}

	public static void runSession(bool planeDetect)
	{
		running = true;
	}

	public static void update()
	{
		if (arInitTimer > 0f)
		{
			arInitTimer -= Time.deltaTime;
			if (arInitTimer <= 0f)
			{
				trackingState = ARTrackingState.ARTrackingStateNormal;
				trackingStateReason = ARTrackingStateReason.ARTrackingStateReasonNone;
				ARPlane aRPlane = new ARPlane();
				aRPlane.uuid = "fake";
				aRPlane.position = Vector3.zero;
				aRPlane.rotation = Quaternion.identity;
				aRPlane.center = Vector3.zero;
				aRPlane.extent = new Vector3(10f, 0f, 10f);
				aRPlane.classification = ARPlaneClassification.ARPlaneClassificationFloor;
				planes.Add(aRPlane);
			}
		}
		pointCloudVertices = new Vector3[5];
		pointCloudVertices[0] = Vector3.zero + Vector3.up * Mathf.Cos(Time.time) * 0.1f;
		pointCloudVertices[1] = Vector3.forward;
		pointCloudVertices[2] = Vector3.right;
		pointCloudVertices[3] = -Vector3.forward;
		pointCloudVertices[4] = -Vector3.right;
		projectionMatrix = Matrix4x4.Perspective(60f, (float)Screen.width / (float)Screen.height, nearZ, farZ);
		if (Input.GetMouseButton(1))
		{
			if (Input.GetMouseButtonDown(1))
			{
				lastMousePosition = Input.mousePosition;
			}
			Vector3 vector = Input.mousePosition - lastMousePosition;
			lastMousePosition = Input.mousePosition;
			currentRotationX += (0f - vector.x) * 0.1f;
			currentRotationY += vector.y * 0.1f;
			float num = 2f;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				num = 0.1f;
			}
			Quaternion quaternion = Quaternion.Euler(0f, currentRotationX, 0f);
			if (Input.GetKey(KeyCode.W))
			{
				cameraPosition += quaternion * Vector3.forward * Time.deltaTime * num;
			}
			if (Input.GetKey(KeyCode.S))
			{
				cameraPosition += quaternion * -Vector3.forward * Time.deltaTime * num;
			}
			if (Input.GetKey(KeyCode.A))
			{
				cameraPosition += quaternion * -Vector3.right * Time.deltaTime * num;
			}
			if (Input.GetKey(KeyCode.D))
			{
				cameraPosition += quaternion * Vector3.right * Time.deltaTime * num;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				cameraPosition += -Vector3.up * Time.deltaTime * num;
			}
			if (Input.GetKey(KeyCode.E))
			{
				cameraPosition += Vector3.up * Time.deltaTime * num;
			}
			cameraRotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);
		}
	}

	public static void setNearAndFarZPlanes(float nearZ, float farZ)
	{
		PineAR.nearZ = nearZ;
		PineAR.farZ = farZ;
	}

	public static bool raycast(float screenX, float screenY, out Pose pose)
	{
		pose = new Pose(Vector3.zero, Quaternion.identity);
		if (trackingState == ARTrackingState.ARTrackingStateNotAvailable)
		{
			return false;
		}
		return true;
	}

	public static void addEnvironmentProbeAchor(Vector3 position)
	{
		AREnvironmentProbe aREnvironmentProbe = new AREnvironmentProbe();
		aREnvironmentProbe.position = position;
		probes.Add(aREnvironmentProbe);
	}

	public static IntPtr addAnchor(string name, Vector3 position, Quaternion rotation)
	{
		ARAnchor aRAnchor = new ARAnchor();
		aRAnchor.name = name;
		aRAnchor.position = position;
		aRAnchor.rotation = rotation;
		aRAnchor.ptr = (IntPtr)(++ptrCounter);
		anchors.Add(aRAnchor);
		return aRAnchor.ptr;
	}

	public static void removeAnchor(IntPtr anchorPtr)
	{
		foreach (ARAnchor anchor in anchors)
		{
			if (anchor.ptr == anchorPtr)
			{
				anchors.Remove(anchor);
				break;
			}
		}
	}

	public static void stopSession()
	{
		running = false;
		trackingState = ARTrackingState.ARTrackingStateNotAvailable;
		trackingStateReason = ARTrackingStateReason.ARTrackingStateReasonNone;
	}

	public static ARSupportLevel supportedFeatures()
	{
		return ARSupportLevel.Tier3_MeshReconstruction;
	}
}
