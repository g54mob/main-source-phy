using System.Collections.Generic;

public class UploadData
{
	public string Name;

	public string Path;

	public bool UploadContent;

	public bool UploadThumbnail;

	public string ThumbnailPath;

	public List<string> Tags;

	public bool IsFolder;

	public string Description;

	public WorkshopManager.ItemTypes ItemType;

	public WorkshopManager.UploadVisibility Visibility;

	public uint DlcDependencyMask;

	public UploadData()
	{
		Description = "N/A";
		Visibility = WorkshopManager.UploadVisibility.Public;
	}

	public UploadData(UploadData other)
	{
		Name = other.Name;
		Path = other.Path;
		UploadContent = other.UploadContent;
		UploadThumbnail = other.UploadThumbnail;
		ThumbnailPath = other.ThumbnailPath;
		Tags = other.Tags;
		IsFolder = other.IsFolder;
		Description = other.Description;
		ItemType = other.ItemType;
		Visibility = other.Visibility;
		DlcDependencyMask = other.DlcDependencyMask;
	}

	public override string ToString()
	{
		return string.Format("Name = {0}, ItemType = {1}, Path = {2}, ThumbnailPath = {3}, IsFolder = {4}, UploadContent = {5}, UploadThumbnail = {6}, Tags = ({7}), Visibility = {8}, DlcDependencyMask = {9}, DlcDpendencies = {10}", Name, ItemType, Path, ThumbnailPath, IsFolder, UploadContent, UploadThumbnail, string.Join(", ", Tags.ToArray()), Visibility, DlcDependencyMask);
	}
}
