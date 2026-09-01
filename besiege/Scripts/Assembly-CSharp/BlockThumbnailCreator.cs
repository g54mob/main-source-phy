using System.Collections;
using System.IO;
using UnityEngine;

public class BlockThumbnailCreator
{
	public static IEnumerator WriteScreenshot(BlockButtonControl buttonCtrl, Camera camera, string path)
	{
		int texSize = 256;
		int halfTexSize = texSize / 2;
		buttonCtrl.gameObject.SendMessage("SetEnabledMsg", false);
		buttonCtrl.GetComponent<Tooltip>().enabled = false;
		Material oldMaterial = buttonCtrl.bg.material;
		buttonCtrl.bg.material = null;
		Vector3 oldPosition = buttonCtrl.transform.position;
		Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
		buttonCtrl.transform.position = camera.ScreenToWorldPoint(screenCenter) + new Vector3(0f, 0f, 12f);
		Vector3 oldLocalScale = buttonCtrl.transform.localScale;
		Vector3 bottomLeftScreen = screenCenter - new Vector3(halfTexSize, halfTexSize);
		Vector3 topRightScreen = screenCenter + new Vector3(halfTexSize, halfTexSize);
		Vector3 bottomLeftWorld = camera.ScreenToWorldPoint(bottomLeftScreen);
		Vector3 topRightWorld = camera.ScreenToWorldPoint(topRightScreen);
		Vector3 scale = (bottomLeftWorld - topRightWorld).Absolute();
		Vector3 parentScale = buttonCtrl.transform.parent.lossyScale;
		buttonCtrl.transform.localScale = new Vector3(scale.x / parentScale.x, scale.x / parentScale.x, scale.x / parentScale.x);
		Texture2D tex = new Texture2D(texSize, texSize, TextureFormat.RGB24, false);
		yield return new WaitForEndOfFrame();
		tex.ReadPixels(new Rect(screenCenter.x - (float)halfTexSize, screenCenter.y - (float)halfTexSize, texSize, texSize), 0, 0);
		tex.Apply();
		buttonCtrl.transform.position = oldPosition;
		buttonCtrl.transform.localScale = oldLocalScale;
		buttonCtrl.bg.material = oldMaterial;
		buttonCtrl.gameObject.SendMessage("SetEnabledMsg", true);
		buttonCtrl.GetComponent<Tooltip>().enabled = true;
		Color32[] colors = tex.GetPixels32();
		for (int i = 0; i < colors.Length; i++)
		{
			if (colors[i].r > 120 && colors[i].g == 0 && colors[i].b > 150)
			{
				colors[i].a = 0;
			}
		}
		Texture2D newTex = new Texture2D(texSize, texSize, TextureFormat.RGBA32, false);
		newTex.SetPixels32(colors);
		byte[] bytes = newTex.EncodeToPNG();
		Object.Destroy(tex);
		Object.Destroy(newTex);
		File.WriteAllBytes(path, bytes);
	}
}
