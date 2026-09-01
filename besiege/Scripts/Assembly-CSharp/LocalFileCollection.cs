using System;
using System.IO;

public abstract class LocalFileCollection : AbstractObjectCollection
{
	protected virtual string FolderName
	{
		get
		{
			return string.Empty;
		}
	}

	protected virtual Type FolderType
	{
		get
		{
			return typeof(LocalFolder);
		}
	}

	public override VirtualFolder GetRoot()
	{
		string path = RootPath + FolderName + FileSystemPath.DirectorySeparator;
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		base.CurrentFolder = LocalFolder.FromDirectoryInfo(directoryInfo, FolderType);
		return base.CurrentFolder;
	}

	public override CreateFolderResult CreateFolder(string folderName)
	{
		FileSystemPath folderPath = base.CurrentFolder.ObjectPath.AppendDirectory(folderName);
		return CreateFolder(folderPath);
	}

	public override CreateFileResult CreateFile(string fileName, out VirtualFile virtualObject)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			virtualObject = null;
			return CreateFileResult.CreateFailed;
		}
		FileSystemPath filePath = base.CurrentFolder.ObjectPath.AppendFile(fileName);
		return CreateFile(filePath, out virtualObject);
	}

	public override void Dispose()
	{
	}

	private CreateFileResult CreateFile(FileSystemPath filePath, out VirtualFile virtualObject)
	{
		string extension = filePath.GetExtension();
		if (string.IsNullOrEmpty(extension))
		{
			filePath = filePath.ChangeExtension(FilterExtension);
		}
		FileInfo fileInfo = new FileInfo(filePath.Path);
		virtualObject = null;
		if (fileInfo.Exists)
		{
			return CreateFileResult.FileExists;
		}
		try
		{
			fileInfo.Create().Dispose();
			virtualObject = LocalFile.FromFileInfo<LocalFile>(fileInfo);
			base.CurrentFolder.AddObject(virtualObject);
		}
		catch (IOException)
		{
			return CreateFileResult.CreateFailed;
		}
		return CreateFileResult.Success;
	}

	private CreateFolderResult CreateFolder(FileSystemPath folderPath)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(folderPath.Path);
		if (directoryInfo.Exists)
		{
			return CreateFolderResult.FolderExists;
		}
		try
		{
			directoryInfo.Create();
			LocalFolder virtualObject = new LocalFolder(directoryInfo);
			base.CurrentFolder.AddObject(virtualObject);
			InvokeCollectionChanged();
		}
		catch (IOException)
		{
			return CreateFolderResult.CreateFailed;
		}
		return CreateFolderResult.Success;
	}
}
