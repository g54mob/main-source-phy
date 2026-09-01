using System.Collections.Generic;

namespace ModIO.PlatformIOCallbacks
{
	public delegate void GetFilesCallback(string path, bool success, IList<string> fileList);
}
