namespace ModIO.PlatformIOCallbacks
{
	public delegate void ReadJSONFileCallback<T>(string path, bool success, T jsonObject);
}
