namespace ModIO.PlatformIOCallbacks
{
	public delegate void GetFileSizeAndHashCallback(string path, bool success, long byteCount, string md5Hash);
}
