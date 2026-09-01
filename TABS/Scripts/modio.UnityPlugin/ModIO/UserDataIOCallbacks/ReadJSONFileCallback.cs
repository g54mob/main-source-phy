namespace ModIO.UserDataIOCallbacks
{
	public delegate void ReadJSONFileCallback<T>(string relativePath, bool success, T jsonObject);
}
