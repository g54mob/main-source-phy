using System.Collections.Generic;
using System.IO;
using CloudinaryDotNet.Actions;

public class GallerySearchResult
{
	public List<SearchResource> m_Resources;

	public bool m_PreviewsRequested;

	public string m_NextCursor;

	public GallerySearchResult(List<SearchResource> resources, string nextCursor)
	{
		m_Resources = resources;
		m_NextCursor = nextCursor;
		m_PreviewsRequested = false;
	}

	public void RequestPreviewImages()
	{
		if (m_PreviewsRequested)
		{
			return;
		}
		foreach (SearchResource resource in m_Resources)
		{
			string previewUrl = Gallery.GetPreviewUrl(resource);
			if (PreviewCache.Get(Path.GetFileName(previewUrl)) == null)
			{
				GalleryPreviewRequests.Add(previewUrl);
			}
		}
		m_PreviewsRequested = true;
	}
}
