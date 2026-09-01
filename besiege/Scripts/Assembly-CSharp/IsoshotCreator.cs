using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IsoshotCreator : MonoBehaviour
{
	[SerializeField]
	private Camera thumbnailCamera;

	public Light dirLight;

	public GameObject meshArrow;

	public bool useShadows = true;

	private bool useMainCam;

	private Transform camTransform;

	private Camera mainCam;

	protected Vector3 oldPos;

	protected Quaternion oldRot;

	protected float oldFov;

	protected int oldMask;

	protected float oldNearClip;

	protected float oldFarClip;

	protected bool oldColoredFog;

	protected BesiegeConfig oldBesiegeConfig;

	private ColorfulFog colorFog;

	private string screenDir = string.Empty;

	private Vector3 oldEuler;

	private float oldIntensity = 1f;

	private List<GameObject> arrows = new List<GameObject>();

	protected virtual void Start()
	{
		if (thumbnailCamera == null)
		{
			thumbnailCamera = GetComponent<Camera>();
		}
		if (thumbnailCamera == null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		if (dirLight == null)
		{
			dirLight = UnityEngine.Object.FindObjectOfType<Light>();
		}
		camTransform = thumbnailCamera.transform;
		FakeCapture();
		mainCam = Camera.main;
		screenDir = Path.Combine(StaticSettings.DataPath, "Screenshots");
	}

	protected virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.F11))
		{
			DateTime now = DateTime.Now;
			string text = "Besiege_" + now.ToString("yyyy_MM_dd") + "_" + now.ToString("hh_mm_ss");
			string thumbnailPath = Path.Combine(screenDir, text + ".png");
			CaptureImage(thumbnailPath, StatMaster.levelSimulating);
		}
	}

	private void PrepareMachine()
	{
		Machine machine = Machine.Active();
		foreach (BlockBehaviour buildingBlock in machine.BuildingBlocks)
		{
			MeshRenderer arrow = buildingBlock.VisualController.arrow;
			if (arrow != null)
			{
				Color color = arrow.material.color;
				arrow.material.color = new Color(color.r, color.g, color.b, 0f);
				GameObject gameObject = UnityEngine.Object.Instantiate(meshArrow, arrow.transform) as GameObject;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				arrows.Add(gameObject);
			}
		}
	}

	private void PrepareCamera()
	{
		oldEuler = dirLight.transform.eulerAngles;
		oldIntensity = dirLight.intensity;
		Vector3 eulerAngles = new Vector3(110.37f, -319.478f, -269.793f);
		dirLight.transform.eulerAngles = eulerAngles;
		dirLight.intensity = 0.8f;
		oldPos = camTransform.position;
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
		}
		thumbnailCamera.enabled = true;
	}

	private void RestoreCamera()
	{
		thumbnailCamera.enabled = false;
		dirLight.transform.eulerAngles = oldEuler;
		dirLight.intensity = oldIntensity;
	}

	private void RestoreMachine()
	{
		for (int i = 0; i < arrows.Count; i++)
		{
			if (!(arrows[i] == null))
			{
				MeshRenderer component = arrows[i].transform.parent.GetComponent<MeshRenderer>();
				Color color = component.material.color;
				component.material.color = new Color(color.r, color.g, color.b, 0.591f);
				UnityEngine.Object.Destroy(arrows[i]);
			}
		}
		arrows.Clear();
	}

	public byte[] CaptureImageBytes(TextureFormat textureFormat, bool encodePNG, int jpgQuality = 75)
	{
		PrepareMachine();
		BesiegeConfig besiegeConfig = new BesiegeConfig();
		besiegeConfig.ShadowsEnabled = useShadows;
		ReferenceMaster.PrepareThumbnailQualitySettings(besiegeConfig);
		PrepareCamera();
		int height = thumbnailCamera.targetTexture.height;
		Texture2D texture2D = new Texture2D(height, height, textureFormat, false);
		thumbnailCamera.Render();
		RenderTexture.active = thumbnailCamera.targetTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, height, height), 0, 0, false);
		texture2D.Apply();
		RenderTexture.active = null;
		ReferenceMaster.RestoreQualitySettings();
		RestoreCamera();
		RestoreMachine();
		byte[] result = ((!encodePNG) ? texture2D.EncodeToJPG(jpgQuality) : texture2D.EncodeToPNG());
		UnityEngine.Object.Destroy(texture2D);
		return result;
	}

	public void CaptureImage(string thumbnailPath, bool useMainCamera)
	{
		useMainCam = useMainCamera;
		byte[] bytes = CaptureImageBytes(TextureFormat.ARGB32, true);
		File.WriteAllBytes(thumbnailPath, bytes);
	}

	private void FakeCapture()
	{
		thumbnailCamera.enabled = true;
		int num = 512;
		Texture2D texture2D = new Texture2D(num, num, TextureFormat.ARGB32, false);
		thumbnailCamera.Render();
		RenderTexture.active = thumbnailCamera.targetTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, num, num), 0, 0, false);
		texture2D.Apply();
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(texture2D);
		thumbnailCamera.enabled = false;
	}
}
