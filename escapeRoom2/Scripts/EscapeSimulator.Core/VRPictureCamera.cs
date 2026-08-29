using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VRPictureCamera : MonoBehaviour
{
	private class PictureData
	{
		public string savePath;

		public byte[] bytes;

		public GraphicsFormat format;

		public int width;

		public int height;
	}

	public enum ImageFormat
	{
		Jpg = 0,
		Png = 1
	}

	private const int DISPLAY_PICTURE_WIDTH = 360;

	private const int DISPLAY_PICTURE_HEIGHT = 240;

	private const int SCREENSHOT_PICTURE_WIDTH = 1024;

	private const int SCREENSHOT_PICTURE_HEIGHT = 1024;

	private const float PICTURE_TWEEN_DURATION = 1f;

	public VRButton takePictureButton;

	public LayerMask layersToRender = -1;

	[FormerlySerializedAs("camera")]
	public Camera pictureCamera;

	public Renderer displayRenderer;

	public Text picturesLeftText;

	public float pictureDropTorqueStrength = 0.2f;

	public List<GameObject> pictures;

	private int lastPictureIndex = -1;

	private float lastPictureTakenTime;

	private RenderTexture displayTexture;

	private Game game;

	private readonly List<GameObject> ignoredDuringRendering = new List<GameObject>();

	public void init(Game game)
	{
		this.game = game;
		displayTexture = new RenderTexture(360, 240, 24);
		pictureCamera.targetTexture = displayTexture;
		pictureCamera.cullingMask = layersToRender;
		pictureCamera.aspect = 1f;
		picturesLeftText.text = pictures.Count.ToString();
		setTextureOnDisplays(displayTexture);
	}

	public void enable()
	{
		if (int.Parse(picturesLeftText.text) != 0)
		{
			pictureCamera.enabled = true;
			setTextureOnDisplays(displayTexture);
		}
	}

	public void disable()
	{
		if (pictureCamera.enabled)
		{
			pictureCamera.enabled = false;
			setTextureOnDisplays(Texture2D.blackTexture);
		}
	}

	public bool takePicture(bool saveScreenshotToDisk)
	{
		if (Time.time - lastPictureTakenTime < 1f)
		{
			return false;
		}
		if (!pictureCamera.enabled)
		{
			return false;
		}
		int num = int.Parse(picturesLeftText.text);
		if (num == 0)
		{
			return false;
		}
		picturesLeftText.text = (num - 1).ToString();
		lastPictureTakenTime = Time.time;
		RenderTexture renderTexture = (RenderTexture.active = new RenderTexture(1024, 1024, 24));
		pictureCamera.targetTexture = renderTexture;
		performCameraRender();
		Texture2D texture2D = new Texture2D(1024, 1024, TextureFormat.RGB24, mipChain: false);
		texture2D.ReadPixels(new Rect(0f, 0f, 1024f, 1024f), 0, 0);
		texture2D.Apply();
		displayInGamePicture(texture2D);
		if (saveScreenshotToDisk)
		{
			saveScreenshot(texture2D);
		}
		pictureCamera.targetTexture = displayTexture;
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(renderTexture);
		float value = Vector3.Distance(pictureCamera.transform.position, game.vr.rig.headCamera.transform.position);
		game.vrPictureCameraFlashAlpha = Mathf.Clamp01(Mathf.InverseLerp(5f, 0.5f, value));
		if (num - 1 == 0)
		{
			disable();
		}
		PineFmod.playOneShotSoundAttached("event:/SFX/VRSFX/polaroid_camera_flash", pictureCamera.gameObject);
		PineFmod.playOneShotSoundAttached("event:/SFX/VRSFX/polaroid_camera_button", takePictureButton.gameObject);
		PineFmod.playOneShotSoundAttached("event:/SFX/VRSFX/polaroid_camera_paper_out", base.gameObject);
		return true;
	}

	private void performCameraRender()
	{
		int layer = LayerMask.NameToLayer("UI");
		int[] array = ((ignoredDuringRendering.Count == 0) ? Array.Empty<int>() : new int[ignoredDuringRendering.Count]);
		for (int i = 0; i < ignoredDuringRendering.Count; i++)
		{
			array[i] = ignoredDuringRendering[i].layer;
			ignoredDuringRendering[i].layer = layer;
		}
		pictureCamera.Render();
		for (int j = 0; j < ignoredDuringRendering.Count; j++)
		{
			ignoredDuringRendering[j].layer = array[j];
		}
	}

	private void displayInGamePicture(Texture2D pictureTexture)
	{
		if (lastPictureIndex != -1)
		{
			_ = pictures[lastPictureIndex];
		}
		lastPictureIndex = (lastPictureIndex + 1) % pictures.Count;
		GameObject takenPicture = pictures[lastPictureIndex];
		takenPicture.GetComponent<Renderer>().materials[0].mainTexture = pictureTexture;
		PineTweenSystemEnableNoHandles gameplayTween = game.getGameplayTween();
		GameObject obj = takenPicture;
		Vector3? localPositionTo = new Vector3(takenPicture.transform.localPosition.x, 0.08f, takenPicture.transform.localPosition.z);
		gameplayTween.tween(obj, 1f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, localPositionTo, null, null, null, null, null, null, null, null, null, null, null, delegate
		{
			takenPicture.GetComponent<Collider>().enabled = true;
		});
	}

	private void saveScreenshot(Texture2D pictureTexture)
	{
		string text = Path.Combine(Application.persistentDataPath, "Pictures");
		Directory.CreateDirectory(text);
		string arg = ((PlayerSave.getSettings().pictureImageFormat == ImageFormat.Jpg) ? "jpg" : "png");
		ThreadPool.QueueUserWorkItem(encodePictureAndSaveItToDisk, new PictureData
		{
			savePath = Path.Combine(text, $"{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.{arg}"),
			bytes = pictureTexture.GetRawTextureData(),
			format = pictureTexture.graphicsFormat,
			width = pictureTexture.width,
			height = pictureTexture.height
		});
	}

	private void setTextureOnDisplays(Texture texture)
	{
		displayRenderer.material.mainTexture = texture;
		GetComponent<Renderer>().materials[4].mainTexture = texture;
	}

	private static void encodePictureAndSaveItToDisk(object pictureDataAsObject)
	{
		PictureData pictureData = (PictureData)pictureDataAsObject;
		Stopwatch stopwatch = Stopwatch.StartNew();
		File.WriteAllBytes(pictureData.savePath, (PlayerSave.getSettings().pictureImageFormat == ImageFormat.Jpg) ? ImageConversion.EncodeArrayToJPG(pictureData.bytes, pictureData.format, (uint)pictureData.width, (uint)pictureData.height) : ImageConversion.EncodeArrayToPNG(pictureData.bytes, pictureData.format, (uint)pictureData.width, (uint)pictureData.height));
		UnityEngine.Debug.Log($"[THREAD POOL] Picture '{pictureData.savePath}' encoded and saved to disk in {stopwatch.ElapsedMilliseconds} ms.");
	}
}
