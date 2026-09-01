using System.IO;
using System.Linq;
using CloudinaryDotNet.Actions;
using UnityEngine;

public class GalleryItem
{
	public SearchResource m_SearchResource;

	public Texture2D m_PreviewTexture;

	public string m_VideoPreviewFilename;

	public bool m_TagChangeInProgress;

	public GalleryItem(SearchResource searchResource)
	{
		m_SearchResource = searchResource;
		m_VideoPreviewFilename = Path.ChangeExtension(searchResource.PublicId, ".jpg");
		m_PreviewTexture = PreviewCache.Get(m_VideoPreviewFilename);
	}

	public string GetId()
	{
		if (m_SearchResource != null)
		{
			return m_SearchResource.PublicId;
		}
		return string.Empty;
	}

	public string GetResourceType()
	{
		if (m_SearchResource == null)
		{
			return "image";
		}
		if (m_SearchResource.ResourceType != ResourceType.Image)
		{
			return "video";
		}
		return "image";
	}

	public string GetVideoPreviewFilename()
	{
		return m_VideoPreviewFilename;
	}

	public string GetVideoUrl()
	{
		if (m_SearchResource == null)
		{
			return string.Empty;
		}
		if (HasTag(GalleryFilterParameters.CLOUDFLARE_TAG))
		{
			return Game.CLOUDFLARE_GALLERY_URL + m_SearchResource.PublicId + ".webm";
		}
		return m_SearchResource.Url.Replace("upload", "upload/q_auto:eco/vc_vp8");
	}

	public string GetCreatedAt()
	{
		if (m_SearchResource != null)
		{
			return m_SearchResource.CreatedAt;
		}
		return string.Empty;
	}

	public string GetLevelID()
	{
		return GalleryMetaData.GetLevelID(m_SearchResource.Context);
	}

	public string GetLevelNameFormatted()
	{
		return GalleryMetaData.GetLevelNameFormatted(m_SearchResource.Context);
	}

	public string GetLevelNameWithoutColorizationTags()
	{
		return GalleryMetaData.GetLevelNameWithoutColorizationTags(m_SearchResource.Context);
	}

	public string GetLevelNameNoPrefix()
	{
		return GalleryMetaData.GetLevelNameNoPrefix(m_SearchResource.Context);
	}

	public string GetWorldName()
	{
		return GalleryMetaData.GetWorldName(m_SearchResource.Context);
	}

	public string GetBudget()
	{
		return GalleryMetaData.GetBudget(m_SearchResource.Context);
	}

	public string GetMaxStress()
	{
		return GalleryMetaData.GetMaxStress(m_SearchResource.Context);
	}

	public float GetMaxStressNormalized()
	{
		return GalleryMetaData.GetMaxStressNormalized(m_SearchResource.Context);
	}

	public string GetOwnerId()
	{
		return GalleryMetaData.GetSteamId(m_SearchResource.Context);
	}

	public bool HasBreaks()
	{
		return Mathf.Approximately(GetMaxStressNormalized(), 1f);
	}

	public bool IsWin()
	{
		return !HasTag(GalleryFilterParameters.FAIL_TAG);
	}

	public bool IsCheat()
	{
		return HasTag(GalleryFilterParameters.CHEAT_TAG);
	}

	public bool IsCurated()
	{
		return HasTag(GalleryFilterParameters.CURATED_TAG);
	}

	public void SetCuratedTag()
	{
		AddTag(GalleryFilterParameters.CURATED_TAG);
	}

	public void ClearCuratedTag()
	{
		RemoveTag(GalleryFilterParameters.CURATED_TAG);
	}

	private bool HasTag(string tag)
	{
		return Gallery.HasTag(m_SearchResource, tag);
	}

	private void AddTag(string tag)
	{
		m_SearchResource.Tags = m_SearchResource.Tags.Concat(new string[1] { tag }).ToArray();
	}

	private void RemoveTag(string tag)
	{
		m_SearchResource.Tags = m_SearchResource.Tags.Where((string val, int idx) => val != tag).ToArray();
	}
}
