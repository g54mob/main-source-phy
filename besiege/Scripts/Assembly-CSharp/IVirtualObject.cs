using System;
using UnityEngine;

public interface IVirtualObject
{
	Action<IVirtualObject> ObjectDeleted { get; set; }

	bool IsUploadable { get; set; }

	bool IsDeletable { get; set; }

	FileSystemPath ObjectPath { get; set; }

	FileSystemPath ThumbnailPath { get; set; }

	VirtualFolder Parent { get; set; }

	Texture Thumbnail { get; set; }

	bool IsFolder { get; }

	bool HasSuffix { get; }

	string Name { get; }

	double Date { get; }

	IVirtualObject Create(FileSystemPath path);

	void Delete();

	void Open();
}
