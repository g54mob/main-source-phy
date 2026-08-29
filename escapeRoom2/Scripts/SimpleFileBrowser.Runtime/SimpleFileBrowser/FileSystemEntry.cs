using System.IO;

namespace SimpleFileBrowser
{
	public readonly struct FileSystemEntry
	{
		public readonly string Path;

		public readonly string Name;

		public readonly string Extension;

		public readonly FileAttributes Attributes;

		public bool IsDirectory => (Attributes & FileAttributes.Directory) == FileAttributes.Directory;

		public FileSystemEntry(string path, string name, string extension, bool isDirectory)
		{
			Path = path;
			Name = name;
			Extension = extension;
			Attributes = (isDirectory ? FileAttributes.Directory : FileAttributes.Normal);
		}

		public FileSystemEntry(FileSystemInfo fileInfo, string extension)
		{
			Path = fileInfo.FullName;
			Name = fileInfo.Name;
			Extension = extension;
			try
			{
				Attributes = fileInfo.Attributes;
			}
			catch
			{
				Attributes = ((fileInfo is DirectoryInfo) ? FileAttributes.Directory : ((FileAttributes)0));
			}
		}
	}
}
