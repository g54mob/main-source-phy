using System;
using System.IO;

public class LocalFile : VirtualFile
{
	public LocalFile()
		: base(FileSystemPath.Root, FileSystemPath.Root)
	{
	}

	public LocalFile(FileSystemPath path, FileSystemPath thumbnailPath)
		: base(path, thumbnailPath)
	{
		ObjectPath = path;
		ThumbnailPath = thumbnailPath;
	}

	public static T FromFileInfo<T>(FileInfo fileInfo) where T : LocalFile, new()
	{
		return (T)FromFileInfo(fileInfo, typeof(T));
	}

	public static LocalFile FromFileInfo(FileInfo fileInfo, Type fileType)
	{
		FileSystemPath fileSystemPath = FileSystemPath.Parse(fileInfo.FullName);
		FileSystemPath thumbnailPath = GetThumbnailPath(fileSystemPath);
		object obj = Activator.CreateInstance(fileType, fileSystemPath, thumbnailPath);
		return (LocalFile)obj;
	}

	private static FileSystemPath GetThumbnailPath(FileSystemPath objectPath)
	{
		string directoryName = "Thumbnails";
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(objectPath.EntityName);
		FileSystemPath result = objectPath.ParentPath.AppendDirectory(directoryName).AppendFile(fileNameWithoutExtension + ".png");
		if (!File.Exists(result.Path))
		{
			result = objectPath.ParentPath.AppendFile(fileNameWithoutExtension + ".png");
		}
		return result;
	}

	public override void Delete()
	{
		FileInfo fileInfo = new FileInfo(ObjectPath.Path);
		FileInfo fileInfo2 = new FileInfo(ThumbnailPath.Path);
		DeleteFile(fileInfo);
		DeleteFile(fileInfo2);
		base.Delete();
	}

	protected override byte[] GetBytes()
	{
		byte[] result = null;
		try
		{
			result = File.ReadAllBytes(ObjectPath.Path);
		}
		catch (Exception)
		{
		}
		return result;
	}

	private void DeleteFile(FileInfo fileInfo)
	{
		try
		{
			fileInfo.Delete();
		}
		catch (Exception)
		{
		}
	}
}
