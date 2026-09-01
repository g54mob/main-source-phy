using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace InternalModding.Blocks
{
	public class ModBlockThumbnailCreator : MonoBehaviour
	{
		public Camera Cam;

		public IEnumerator TakeThumbnail(ModdedBlock block, string path)
		{
			GameObject blockButton = block.BlockButton;
			if (blockButton == null)
			{
				Debug.LogError("Tried to take thumbnail of block without button!");
				yield break;
			}
			Transform iconPivot = blockButton.transform.FindChild("IconPivot");
			iconPivot.SetParent(base.transform, false);
			Vector3 oldScale = iconPivot.localScale;
			iconPivot.localScale = Vector3.one;
			ReferenceMaster.PrepareThumbnailQualitySettings();
			RenderTexture originalRenderTexture = RenderTexture.active;
			RenderTexture.active = Cam.targetTexture;
			Texture2D captured = new Texture2D(Cam.pixelHeight, Cam.pixelHeight, TextureFormat.ARGB32, false);
			Cam.Render();
			captured.ReadPixels(new Rect(0f, 0f, Cam.pixelHeight, Cam.pixelHeight), 0, 0, false);
			captured.Apply();
			if (Cam.pixelHeight > 256)
			{
				TextureScale.Bilinear(captured, 256, 256);
			}
			byte[] bytes = captured.EncodeToPNG();
			UnityEngine.Object.Destroy(captured);
			RenderTexture.active = originalRenderTexture;
			ReferenceMaster.RestoreQualitySettings();
			iconPivot.localScale = oldScale;
			iconPivot.SetParent(blockButton.transform, false);
			try
			{
				File.WriteAllBytes(path, bytes);
			}
			catch (Exception ex)
			{
				Debug.LogError("Could not write mod block thumbnail: " + ex);
			}
		}
	}
}
