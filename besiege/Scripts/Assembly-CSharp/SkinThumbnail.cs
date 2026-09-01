using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SkinThumbnail : MonoBehaviour
{
	public static bool Finished;

	public Camera cam;

	public MeshRenderer previewerRenderer;

	public MeshFilter previewerFilter;

	public void SetSkin(BlockSkinLoader.SkinPack.Skin skin)
	{
		BlockButtonControl buttonIcon = skin.prefab.GetButtonIcon();
		Vector3 localPosition = buttonIcon.myRenderer.transform.localPosition;
		previewerRenderer.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);
		previewerRenderer.transform.localRotation = buttonIcon.myRenderer.transform.localRotation;
		previewerRenderer.transform.localScale = buttonIcon.myRenderer.transform.localScale;
		previewerFilter.mesh = skin.mesh;
		previewerRenderer.material.mainTexture = skin.texture;
	}

	public void CaptureImage(string path)
	{
		Finished = false;
		StartCoroutine(IECaptureImage(path));
	}

	public IEnumerator IECaptureImage(string path)
	{
		yield return new WaitForEndOfFrame();
		if (cam == null)
		{
			yield break;
		}
		ReferenceMaster.PrepareThumbnailQualitySettings();
		RenderTexture originalRenderTex = RenderTexture.active;
		RenderTexture.active = cam.targetTexture;
		Texture2D captured = new Texture2D(cam.pixelHeight, cam.pixelHeight, TextureFormat.RGB24, false);
		cam.Render();
		captured.ReadPixels(new Rect(cam.pixelWidth / 2 - cam.pixelHeight / 2, 0f, cam.pixelHeight, cam.pixelHeight), 0, 0);
		captured.Apply();
		if (cam.pixelHeight > 512)
		{
			TextureScale.Bilinear(captured, 512, 512);
		}
		byte[] bytes = captured.EncodeToPNG();
		UnityEngine.Object.Destroy(captured);
		RenderTexture.active = originalRenderTex;
		ReferenceMaster.RestoreQualitySettings();
		try
		{
			File.WriteAllBytes(path, bytes);
			Finished = true;
		}
		catch (Exception ex)
		{
			Debug.Log("Could not write thumbnail: " + ex);
		}
	}
}
