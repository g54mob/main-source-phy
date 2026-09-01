using System.Collections;
using UnityEngine;

public class SkinUploadDialog : UploadDialog
{
	public TextMesh title;

	public Transform ThumbnailMesh;

	public Texture2D noThumbTex;

	public override void Initialize(UploadDialogMode uploadMode, UploadData uploadData, Texture thumbnailTexture = null)
	{
		base.Initialize(uploadMode, uploadData, thumbnailTexture);
		title.text = uploadData.Name;
		StartCoroutine(LoadImage());
	}

	private bool isErrorImage(Texture tex)
	{
		return tex != null && tex.name == string.Empty && tex.height == 8 && tex.width == 8 && tex.filterMode == FilterMode.Bilinear && tex.anisoLevel == 1 && tex.wrapMode == TextureWrapMode.Repeat && tex.mipMapBias == 0f;
	}

	private IEnumerator LoadImage()
	{
		Material thumbnailMaterial = ThumbnailMesh.GetComponent<Renderer>().material;
		thumbnailMaterial.mainTexture = new Texture2D(512, 512, TextureFormat.RGB24, false);
		if (ReferenceMaster.UIActive != ReferenceMaster.WorkshopItemType.Machine && ReferenceMaster.UIActive != ReferenceMaster.WorkshopItemType.Skins)
		{
			yield break;
		}
		WWW www = new WWW("file://" + uploadData.ThumbnailPath.Replace("\\", "/"));
		yield return www;
		try
		{
			thumbnailMaterial.mainTexture = www.texture;
			if (isErrorImage(thumbnailMaterial.mainTexture))
			{
				thumbnailMaterial.mainTexture = noThumbTex;
			}
		}
		catch
		{
			thumbnailMaterial.mainTexture = noThumbTex;
		}
	}

	private void OnApplicationFocus(bool focusStatus)
	{
		if (focusStatus)
		{
			StartCoroutine(LoadImage());
		}
	}
}
