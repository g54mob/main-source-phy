using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThumbnailCreator : MonoBehaviour
{
	public Camera thumbnailCamera;

	public GameObject shadowFloor;

	public Light lightSource;

	private bool useMainCam;

	private Transform camTransform;

	private Camera mainCam;

	private Vector3 oldPos;

	private Vector3 oldRootPos;

	private Quaternion oldRot;

	private float oldFov;

	private int oldMask;

	private float oldNearClip;

	private float oldFarClip;

	private bool oldColoredFog;

	private ColorfulFog colorFog;

	private BesiegeConfig oldBesiegeConfig;

	private bool isInitialized;

	private Light[] allLights;

	private Light[] lights = new Light[0];

	private Scene active;

	private Transform buildZoneTransform;

	private Matrix4x4 M;

	private Vector4 p4 = new Vector4(0f, 0f, 0f, 1f);

	private Vector4 clip;

	private float invClip;

	private void SetScene(int i = -1)
	{
	}

	protected void Start()
	{
		Init();
	}

	private void Init()
	{
		if (isInitialized)
		{
			return;
		}
		if (thumbnailCamera == null)
		{
			GameObject gameObject = GameObject.Find("THUMB CAM");
			if (gameObject != null)
			{
				thumbnailCamera = gameObject.GetComponent<Camera>();
			}
		}
		allLights = UnityEngine.Object.FindObjectsOfType<Light>();
		if (thumbnailCamera == null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		if (shadowFloor == null && thumbnailCamera.transform.childCount > 0)
		{
			shadowFloor = thumbnailCamera.transform.GetChild(0).gameObject;
		}
		camTransform = thumbnailCamera.transform;
		FakeCapture();
		mainCam = Camera.main;
		isInitialized = true;
	}

	public void PrepareCamera(bool renderDebugGizmos, bool selectionOnly)
	{
		SetScene(0);
		oldPos = camTransform.position;
		oldRootPos = camTransform.root.position;
		oldRot = camTransform.rotation;
		oldFov = thumbnailCamera.fieldOfView;
		oldMask = thumbnailCamera.cullingMask;
		oldNearClip = thumbnailCamera.nearClipPlane;
		oldFarClip = thumbnailCamera.farClipPlane;
		colorFog = thumbnailCamera.GetComponent<ColorfulFog>();
		oldColoredFog = (bool)colorFog && colorFog.enabled;
		if (useMainCam)
		{
			camTransform.position = mainCam.transform.position;
			camTransform.rotation = mainCam.transform.rotation;
			thumbnailCamera.fieldOfView = mainCam.fieldOfView;
			thumbnailCamera.cullingMask = mainCam.cullingMask ^ ((1 << LayerMask.NameToLayer("Brace")) | (1 << LayerMask.NameToLayer("MachineVis")) | (1 << LayerMask.NameToLayer("Occluder")) | (1 << LayerMask.NameToLayer("JointTrigger")) | (1 << LayerMask.NameToLayer("JointTrigger2")));
			thumbnailCamera.nearClipPlane = mainCam.nearClipPlane;
			thumbnailCamera.farClipPlane = mainCam.farClipPlane;
			if (colorFog != null)
			{
				colorFog.enabled = mainCam.GetComponent<ColorfulFog>().enabled;
			}
			thumbnailCamera.enabled = true;
			return;
		}
		thumbnailCamera.enabled = true;
		if (lightSource != null)
		{
			List<Light> list = new List<Light>();
			for (int i = 0; i < allLights.Length; i++)
			{
				Light light = allLights[i];
				if ((bool)light && light.enabled && light != lightSource)
				{
					list.Add(light);
					light.enabled = false;
				}
			}
			lightSource.enabled = true;
			lights = list.ToArray();
		}
		float floorHeight;
		float mag;
		Vector3 center;
		GetCameraDistanceAndCenter(Machine.Active(), camTransform.root, camTransform, selectionOnly, renderDebugGizmos, out floorHeight, out mag, out center);
		if ((bool)shadowFloor)
		{
			shadowFloor.SetActive(true);
			shadowFloor.transform.rotation = Quaternion.identity;
			shadowFloor.transform.position = new Vector3(0f, floorHeight, 0f);
		}
	}

	public void GetCameraDistanceAndCenter(Machine machine, Transform mover, Transform distancer, bool selectionOnly, bool renderDebugGizmos, out float floorHeight, out float mag, out Vector3 center)
	{
		Transform transform = thumbnailCamera.transform;
		Bounds bounds = machine.GetBounds(false);
		Vector3 center2 = bounds.center;
		if (StatMaster.isMP)
		{
			buildZoneTransform = PlayerData.localPlayer.buildZone.transform;
			center2 = buildZoneTransform.TransformPoint(bounds.center + Vector3.down * 5f);
		}
		bounds = new Bounds(center2, bounds.size);
		floorHeight = bounds.min.y;
		Vector3 position = bounds.ClosestPoint(bounds.center + transform.right * 1000f);
		Vector3 position2 = bounds.ClosestPoint(bounds.center + transform.up * 1000f);
		position = transform.InverseTransformPoint(position);
		position2 = transform.InverseTransformPoint(position2);
		mag = Mathf.Max(position.x, position2.y) * 2f;
		SetCameraFromFrustrum(mover, distancer, mag, center2);
		Vector3 middlePosition = machine.MiddlePosition;
		transform.root.position = middlePosition;
		center = middlePosition;
		float z = thumbnailCamera.WorldToViewportPoint(center).z;
		List<BlockBehaviour> list = ((!selectionOnly) ? Machine.Active().BuildingBlocks : AdvancedBlockEditor.Instance.selectionController.MachineSelection);
		Vector3 max;
		Vector3 min = (max = thumbnailCamera.WorldToViewportPoint(list[0].transform.position));
		foreach (BlockBehaviour item in list)
		{
			switch ((BlockType)item.BlockID)
			{
			case BlockType.BuildSurface:
				EvaluatePosition(ref min, ref max, item.GetCenter());
				continue;
			case BlockType.BuildNode:
			case BlockType.BuildEdge:
				EvaluatePosition(ref min, ref max, item.transform.position);
				continue;
			case BlockType.Brace:
			case BlockType.Spring:
			case BlockType.RopeWinch:
			case BlockType.Pin:
			case BlockType.CameraBlock:
			case BlockType.RopeMeasure:
				continue;
			}
			Bounds defaultBounds = item.Prefab.blockBehaviour.DefaultBounds;
			Vector3 min2 = defaultBounds.min;
			Vector3 max2 = defaultBounds.max;
			M = item.transform.localToWorldMatrix;
			EvaluatePosition(ref min, ref max, M.MultiplyPoint3x4(min2));
			EvaluatePosition(ref min, ref max, M.MultiplyPoint3x4(max2));
			EvaluatePosition(ref min, ref max, M.MultiplyPoint3x4(new Vector3(min2.x, min2.y, max2.z)));
			EvaluatePosition(ref min, ref max, M.MultiplyPoint3x4(new Vector3(min2.x, max2.y, min2.z)));
			EvaluatePosition(ref min, ref max, M.MultiplyPoint3x4(new Vector3(max2.x, min2.y, min2.z)));
			EvaluatePosition(ref min, ref max, M.MultiplyPoint3x4(new Vector3(min2.x, max2.y, max2.z)));
			EvaluatePosition(ref min, ref max, M.MultiplyPoint3x4(new Vector3(max2.x, min2.y, max2.z)));
			EvaluatePosition(ref min, ref max, M.MultiplyPoint3x4(new Vector3(max2.x, max2.y, min2.z)));
		}
		center = new Vector3((min.x + max.x) * 0.5f, (min.y + max.y) * 0.5f, z);
		center = thumbnailCamera.ViewportToWorldPoint(center);
		min = thumbnailCamera.ViewportToWorldPoint(new Vector3(min.x, min.y, z));
		max = thumbnailCamera.ViewportToWorldPoint(new Vector3(max.x, max.y, z));
		min = transform.InverseTransformPoint(min);
		max = transform.InverseTransformPoint(max);
		float a = max.x - min.x;
		float b = max.y - min.y;
		float num = Mathf.Lerp(0.1f, 1f, Mathf.InverseLerp(15f, 5f, mag));
		mag = Mathf.Max(a, b) * (1f + num);
		SetCameraFromFrustrum(mover, distancer, mag, center);
	}

	private void EvaluatePosition(ref Vector3 min, ref Vector3 max, Vector3 pos)
	{
		pos = thumbnailCamera.WorldToViewportPoint(pos);
		if (pos.x < min.x)
		{
			min.x = pos.x;
		}
		else if (pos.x > max.x)
		{
			max.x = pos.x;
		}
		if (pos.y < min.y)
		{
			min.y = pos.y;
		}
		else if (pos.y > max.y)
		{
			max.y = pos.y;
		}
	}

	private void SetCameraFromFrustrum(Transform mover, Transform distancer, float mag, Vector3 center)
	{
		mag *= 0.5f;
		float num = Mathf.Tan(thumbnailCamera.fieldOfView * 0.5f * ((float)Math.PI / 180f));
		float num2 = mag / num;
		mover.position = center;
		if (mag > num)
		{
			distancer.localPosition = new Vector3(0f, distancer.localPosition.y, 0f - num2);
		}
	}

	private void RestoreCamera()
	{
		SetScene();
		thumbnailCamera.enabled = false;
		if ((bool)shadowFloor)
		{
			shadowFloor.SetActive(false);
		}
		if (!useMainCam && lightSource != null)
		{
			for (int i = 0; i < lights.Length; i++)
			{
				Light light = lights[i];
				if ((bool)light)
				{
					light.enabled = true;
				}
			}
			lightSource.enabled = false;
		}
		if (!useMainCam)
		{
			camTransform.root.position = oldRootPos;
			camTransform.position = oldPos;
			return;
		}
		camTransform.position = oldPos;
		camTransform.rotation = oldRot;
		thumbnailCamera.fieldOfView = oldFov;
		thumbnailCamera.cullingMask = oldMask;
		thumbnailCamera.nearClipPlane = oldNearClip;
		thumbnailCamera.farClipPlane = oldFarClip;
		if (colorFog != null)
		{
			colorFog.enabled = oldColoredFog;
		}
	}

	public void CaptureImage(string thumbnailPath, bool useMainCamera)
	{
		byte[] bytes = CaptureImageBytes(TextureFormat.RGB24, true, useMainCamera, false);
		File.WriteAllBytes(thumbnailPath, bytes);
	}

	public byte[] CaptureImageBytes(TextureFormat textureFormat, bool encodePNG, bool useMainCamera, bool renderDebugGizmos, int jpgQuality = 75)
	{
		Texture2D texture2D = CaptureImageTexture(textureFormat, useMainCamera, renderDebugGizmos);
		byte[] result = ((!encodePNG) ? texture2D.EncodeToJPG(jpgQuality) : texture2D.EncodeToPNG());
		UnityEngine.Object.Destroy(texture2D);
		return result;
	}

	public Texture2D CaptureImageTexture(TextureFormat textureFormat, bool useMainCamera, bool renderDebugGizmos, bool selectionOnly = false)
	{
		ShadowResolution shadowResolution = QualitySettings.shadowResolution;
		QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
		bool flag = true;
		useMainCam = useMainCamera;
		Init();
		ReferenceMaster.PrepareThumbnailQualitySettings(true);
		if (flag)
		{
			PrepareCamera(renderDebugGizmos, selectionOnly);
		}
		int num = 512;
		if (thumbnailCamera.targetTexture.height != num)
		{
			Debug.LogWarning("You're using a thumbnail height that doesn't match your render texture, please remember to update the render texture manually since it can't be done through code");
		}
		Texture2D texture2D = new Texture2D(num, num, textureFormat, false);
		RenderTexture.active = thumbnailCamera.targetTexture;
		GL.Clear(true, true, Color.clear);
		thumbnailCamera.Render();
		texture2D.ReadPixels(new Rect(0f, 0f, num, num), 0, 0, false);
		RenderTexture.active = null;
		ReferenceMaster.RestoreQualitySettings(true);
		if (flag)
		{
			RestoreCamera();
		}
		QualitySettings.shadowResolution = shadowResolution;
		return texture2D;
	}

	public void CaptureMachineSelectionImage(string thumbnailPath)
	{
		List<BlockVisualController> list = new List<BlockVisualController>();
		List<BlockBehaviour> buildingBlocks = Machine.Active().BuildingBlocks;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (!blockBehaviour.IsSelected && blockBehaviour.VisualController.isVisible)
			{
				list.Add(blockBehaviour.VisualController);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			BlockVisualController blockVisualController = list[j];
			blockVisualController.SetInvisible();
		}
		Texture2D texture2D = CaptureImageTexture(TextureFormat.RGB24, false, false, true);
		byte[] bytes = texture2D.EncodeToPNG();
		UnityEngine.Object.Destroy(texture2D);
		for (int k = 0; k < list.Count; k++)
		{
			BlockVisualController blockVisualController2 = list[k];
			blockVisualController2.SetVisible();
		}
		File.WriteAllBytes(thumbnailPath, bytes);
	}

	private void FakeCapture()
	{
		thumbnailCamera.enabled = true;
		int num = 512;
		Texture2D texture2D = new Texture2D(num, num, TextureFormat.RGB24, false);
		thumbnailCamera.Render();
		RenderTexture.active = thumbnailCamera.targetTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, num, num), 0, 0, false);
		texture2D.Apply();
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(texture2D);
		thumbnailCamera.enabled = false;
	}
}
