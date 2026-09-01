using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorScreenshotHandler
	{
		public static void CaptureScreenshot(string filename, bool campaign)
		{
			Debug.Log("Trying to capture screenshot to path: " + filename);
			GameObject cameraObject = null;
			Camera cameraComponent = null;
			RenderTexture renderTexture = null;
			byte[] array = null;
			if (!campaign)
			{
				BattleCreatorScreenshotCameraTAG battleCreatorScreenshotCameraTAG = Object.FindObjectOfType<BattleCreatorScreenshotCameraTAG>();
				if (battleCreatorScreenshotCameraTAG != null)
				{
					cameraObject = battleCreatorScreenshotCameraTAG.gameObject;
				}
				else
				{
					Debug.LogError("Cannot find screenshot camera!");
				}
			}
			else
			{
				BattleCreatorCampaignScreenshotMaker battleCreatorCampaignScreenshotMaker = Object.FindObjectOfType<BattleCreatorCampaignScreenshotMaker>();
				if (battleCreatorCampaignScreenshotMaker != null)
				{
					cameraObject = battleCreatorCampaignScreenshotMaker.gameObject;
				}
			}
			Texture2D texture2D;
			if (cameraObject != null)
			{
				renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
				texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, mipChain: true);
				cameraObject.SetActive(value: true);
				cameraComponent = cameraObject.GetComponentInChildren<Camera>();
				if (cameraComponent != null)
				{
					cameraComponent.targetTexture = renderTexture;
					cameraComponent.Render();
				}
				RenderTexture.active = renderTexture;
				texture2D.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
			}
			else
			{
				texture2D = Texture2D.blackTexture;
			}
			texture2D.Apply();
			array = ScaleTexture(texture2D, 640, 360).EncodeToPNG();
			ServiceLocator.GetService<FileIOWrapper>().WriteAllBytes(filename, array, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
			{
				if (cameraComponent != null)
				{
					cameraComponent.targetTexture = null;
				}
				RenderTexture.active = null;
				if (renderTexture != null)
				{
					Object.Destroy(renderTexture);
				}
				if (campaign && cameraObject != null)
				{
					cameraObject.SetActive(value: false);
				}
			});
		}

		private static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
		{
			Texture2D texture2D = new Texture2D(targetWidth, targetHeight, source.format, mipChain: true);
			Color[] pixels = texture2D.GetPixels(0);
			float num = 1f / (float)source.width * ((float)source.width / (float)targetWidth);
			float num2 = 1f / (float)source.height * ((float)source.height / (float)targetHeight);
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = source.GetPixelBilinear(num * ((float)i % (float)targetWidth), num2 * Mathf.Floor(i / targetWidth));
			}
			texture2D.SetPixels(pixels, 0);
			texture2D.Apply();
			return texture2D;
		}
	}
}
