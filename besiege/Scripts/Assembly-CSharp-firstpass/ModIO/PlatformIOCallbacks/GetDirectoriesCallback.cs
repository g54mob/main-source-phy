using System.Collections.Generic;

namespace ModIO.PlatformIOCallbacks
{
	public delegate void GetDirectoriesCallback(string path, bool success, IList<string> directoryList);
}
