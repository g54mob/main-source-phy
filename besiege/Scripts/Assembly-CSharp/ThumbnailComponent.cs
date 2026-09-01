using System.IO;
using Besiege;
using UnityEngine;

public class ThumbnailComponent : MonoBehaviour
{
	protected Material thumbnailMaterial;

	protected Renderer thumbnailRenderer;

	protected bool hasCustomTexture;

	protected bool isFolderThumbnail;

	[SerializeField]
	protected Texture2D noFolderThumbnailTexture;

	[SerializeField]
	protected Texture2D noThumbnailTexture;

	protected Texture currentTexture;

	private bool hasTexture;

	private string thumbnailPath;

	private AssetImporter.LoadingObject loadingObj;

	private byte[] customTextureBytes;

	public static int images;

	protected bool hasThumbnailRenderer;

	public virtual void Initialize(string thumbnailPath, bool isFolderThumbnail)
	{
		this.thumbnailPath = thumbnailPath;
		this.isFolderThumbnail = isFolderThumbnail;
		thumbnailRenderer = GetComponent<Renderer>();
		if (thumbnailRenderer != null)
		{
			hasThumbnailRenderer = true;
			thumbnailMaterial = thumbnailRenderer.material;
		}
		else
		{
			hasThumbnailRenderer = false;
		}
	}

	public virtual void Initialize(byte[] thumbnailBytes, bool isFolderThumbnail)
	{
		this.isFolderThumbnail = isFolderThumbnail;
		thumbnailRenderer = GetComponent<Renderer>();
		if (thumbnailRenderer != null)
		{
			hasThumbnailRenderer = true;
			thumbnailMaterial = thumbnailRenderer.material;
		}
		else
		{
			hasThumbnailRenderer = false;
		}
		customTextureBytes = thumbnailBytes;
	}

	protected virtual void ResolveThumbnail(string filePath)
	{
		DeleteOldTexture();
		Stop();
		if (File.Exists(filePath))
		{
			loadingObj = AssetImporter.StartImport.Texture(filePath, false, DoneLoading);
			images++;
		}
		hasCustomTexture = false;
		if (isFolderThumbnail)
		{
			ApplyTexture(noFolderThumbnailTexture);
		}
		else
		{
			ApplyTexture(noThumbnailTexture);
		}
	}

	public void DoneLoading(AssetImporter.LoadingObject loadingObj)
	{
		ApplyTexture(loadingObj.tex);
		hasCustomTexture = true;
	}

	public void Stop()
	{
		if (loadingObj != null)
		{
			loadingObj.Stop();
			loadingObj = null;
		}
	}

	protected virtual Texture GetCurrentTexture()
	{
		return currentTexture;
	}

	protected virtual void ApplyTexture(Texture texture)
	{
		if (hasThumbnailRenderer)
		{
			currentTexture = texture;
			thumbnailMaterial.mainTexture = texture;
		}
	}

	protected byte[] FetchTextureBytes(string path)
	{
		return File.ReadAllBytes(path);
	}

	protected void DeleteOldTexture()
	{
		if (hasCustomTexture)
		{
			Texture texture = GetCurrentTexture();
			if (texture != noFolderThumbnailTexture && texture != noThumbnailTexture)
			{
				images--;
				Object.DestroyImmediate(texture);
			}
			if (isFolderThumbnail)
			{
				ApplyTexture(noFolderThumbnailTexture);
			}
			else
			{
				ApplyTexture(noThumbnailTexture);
			}
		}
	}

	public virtual void SetVisible()
	{
		if (!hasTexture)
		{
			if (customTextureBytes != null)
			{
				ApplyThumbnailBytes(customTextureBytes);
			}
			else
			{
				ResolveThumbnail(thumbnailPath);
			}
		}
		hasTexture = true;
	}

	public virtual void SetInvisible()
	{
		DeleteOldTexture();
		Stop();
		hasTexture = false;
	}

	private void ApplyThumbnailBytes(byte[] thumbBytes)
	{
		DeleteOldTexture();
		ApplyTexture(AssetImporter.ConvertTextureBytes(thumbBytes));
		hasCustomTexture = true;
	}

	private void OnDisable()
	{
		SetInvisible();
	}

	private void OnDestroy()
	{
		SetInvisible();
	}
}
