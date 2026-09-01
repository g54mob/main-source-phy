namespace ModIO.UserDataIOCallbacks
{
	public delegate void GetFileSizeAndHashCallback(string relativePath, bool success, long byteCount, string md5Hash);
}
