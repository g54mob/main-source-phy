using System.IO;
using UnityEngine;

public class FileBrowserSlotThumbnail : ThumbnailComponent
{
	protected IVirtualObject virtualObject;

	public virtual void Initialize(IVirtualObject virtualObject)
	{
		this.virtualObject = virtualObject;
		Initialize(virtualObject.ThumbnailPath.ToString(), virtualObject.IsFolder);
	}

	protected override void ApplyTexture(Texture texture)
	{
		base.ApplyTexture(texture);
		if (hasThumbnailRenderer)
		{
			virtualObject.Thumbnail = texture;
		}
	}

	protected override void ResolveThumbnail(string filePath)
	{
		if (!ReferenceMaster.IsPlatformReady())
		{
			base.ResolveThumbnail(filePath);
		}
		else if (!UseSteamPreview())
		{
			base.ResolveThumbnail(filePath);
		}
	}

	private bool UseSteamPreview()
	{
		if (virtualObject == null || File.Exists(virtualObject.ThumbnailPath.ToString()))
		{
			return false;
		}
		SteamWorkshopManager steamWorkshopManager = (SteamWorkshopManager)SingleInstance<WorkshopManager>.Instance;
		if (virtualObject is PublishedSteamFile)
		{
			PublishedSteamFile publishedSteamFile = (PublishedSteamFile)virtualObject;
			steamWorkshopManager.GetPreviewThumbnail(publishedSteamFile.PreviewImageHandle, ApplyTexture);
			return true;
		}
		return false;
	}
}
