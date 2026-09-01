using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public struct FileSystemPath : IEquatable<FileSystemPath>, IComparable<FileSystemPath>
{
	public static readonly char DirectorySeparator;

	private readonly string _path;

	public static FileSystemPath Root { get; private set; }

	public string Path
	{
		get
		{
			return _path ?? "/";
		}
	}

	public bool IsDirectory
	{
		get
		{
			return Path[Path.Length - 1] == DirectorySeparator;
		}
	}

	public bool IsFile
	{
		get
		{
			return !IsDirectory;
		}
	}

	public bool IsRoot
	{
		get
		{
			return Path.Length == 1;
		}
	}

	public string EntityName
	{
		get
		{
			string path = Path;
			if (IsRoot)
			{
				return null;
			}
			int num = path.Length;
			if (IsDirectory)
			{
				num--;
			}
			int num2 = path.LastIndexOf(DirectorySeparator, num - 1, num) + 1;
			return path.Substring(num2, num - num2);
		}
	}

	public FileSystemPath ParentPath
	{
		get
		{
			string path = Path;
			if (IsRoot)
			{
				throw new InvalidOperationException("There is no parent of root.");
			}
			int num = path.Length;
			if (IsDirectory)
			{
				num--;
			}
			int num2 = path.LastIndexOf(DirectorySeparator, num - 1, num);
			path = path.Remove(num2 + 1);
			return new FileSystemPath(path);
		}
	}

	private FileSystemPath(string path)
	{
		_path = path;
	}

	static FileSystemPath()
	{
		DirectorySeparator = '/';
		Root = new FileSystemPath(DirectorySeparator.ToString());
	}

	public static bool IsRooted(string s)
	{
		if (s.Length == 0)
		{
			return false;
		}
		return s[0] == DirectorySeparator || System.IO.Path.GetPathRoot(s) != null;
	}

	public static FileSystemPath Parse(string s)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		if (!IsRooted(s))
		{
			throw new ParseException(s, "Path is not rooted");
		}
		if (s.Contains(string.Concat(DirectorySeparator, DirectorySeparator)))
		{
			throw new ParseException(s, "Path contains double directory-separators.");
		}
		return new FileSystemPath(s.Replace('\\', DirectorySeparator));
	}

	public FileSystemPath AppendPath(string relativePath)
	{
		if (IsRooted(relativePath))
		{
			throw new ArgumentException("The specified path should be relative.", "relativePath");
		}
		if (!IsDirectory)
		{
			throw new InvalidOperationException("This FileSystemPath is not a directory.");
		}
		return new FileSystemPath(Path + relativePath);
	}

	public FileSystemPath AppendPath(FileSystemPath path)
	{
		if (!IsDirectory)
		{
			throw new InvalidOperationException("This FileSystemPath is not a directory.");
		}
		return new FileSystemPath(Path + path.Path.Substring(1));
	}

	public FileSystemPath AppendDirectory(string directoryName)
	{
		if (directoryName.Contains(DirectorySeparator.ToString()))
		{
			throw new ArgumentException("The specified name includes directory-separator(s).", "directoryName");
		}
		return new FileSystemPath(Path + directoryName + DirectorySeparator);
	}

	public FileSystemPath AppendFile(string fileName)
	{
		if (fileName.Contains(DirectorySeparator.ToString()))
		{
			throw new ArgumentException("The specified name includes directory-separator(s).", "fileName");
		}
		if (!IsDirectory)
		{
			throw new InvalidOperationException("The specified FileSystemPath is not a directory.");
		}
		return new FileSystemPath(Path + fileName);
	}

	public bool IsParentOf(FileSystemPath path)
	{
		return IsDirectory && Path.Length != path.Path.Length && path.Path.StartsWith(Path);
	}

	public bool IsChildOf(FileSystemPath path)
	{
		return path.IsParentOf(this);
	}

	public FileSystemPath RemoveParent(FileSystemPath parent)
	{
		if (!parent.IsDirectory)
		{
			throw new ArgumentException("The specified path can not be the parent of this path: it is not a directory.");
		}
		if (!Path.StartsWith(parent.Path))
		{
			throw new ArgumentException("The specified path is not a parent of this path.");
		}
		return new FileSystemPath(Path.Remove(0, parent.Path.Length - 1));
	}

	public FileSystemPath RemoveChild(FileSystemPath child)
	{
		if (!Path.EndsWith(child.Path))
		{
			throw new ArgumentException("The specified path is not a child of this path.");
		}
		return new FileSystemPath(Path.Substring(0, Path.Length - child.Path.Length + 1));
	}

	public string GetExtension()
	{
		if (!IsFile)
		{
			throw new ArgumentException("The specified FileSystemPath is not a file.");
		}
		string entityName = EntityName;
		int num = entityName.LastIndexOf('.');
		if (num < 0)
		{
			return string.Empty;
		}
		return entityName.Substring(num);
	}

	public FileSystemPath ChangeExtension(string extension)
	{
		if (!IsFile)
		{
			throw new ArgumentException("The specified FileSystemPath is not a file.");
		}
		string entityName = EntityName;
		int num = entityName.LastIndexOf('.');
		if (num >= 0)
		{
			return ParentPath.AppendFile(entityName.Substring(0, num) + extension);
		}
		return Parse(Path + extension);
	}

	public string[] GetDirectorySegments()
	{
		FileSystemPath fileSystemPath = this;
		if (IsFile)
		{
			fileSystemPath = fileSystemPath.ParentPath;
		}
		LinkedList<string> linkedList = new LinkedList<string>();
		while (!fileSystemPath.IsRoot)
		{
			linkedList.AddFirst(fileSystemPath.EntityName);
			fileSystemPath = fileSystemPath.ParentPath;
		}
		return linkedList.ToArray();
	}

	public int CompareTo(FileSystemPath other)
	{
		return Path.CompareTo(other.Path);
	}

	public override string ToString()
	{
		return Path;
	}

	public override bool Equals(object obj)
	{
		if (obj is FileSystemPath)
		{
			return Equals((FileSystemPath)obj);
		}
		return false;
	}

	public bool Equals(FileSystemPath other)
	{
		return other.Path.Equals(Path);
	}

	public override int GetHashCode()
	{
		return Path.GetHashCode();
	}

	public static bool operator ==(FileSystemPath pathA, FileSystemPath pathB)
	{
		return pathA.Equals(pathB);
	}

	public static bool operator !=(FileSystemPath pathA, FileSystemPath pathB)
	{
		return !(pathA == pathB);
	}
}
