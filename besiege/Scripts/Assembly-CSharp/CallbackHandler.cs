using UnityEngine;

public class CallbackHandler : MonoBehaviour
{
	private IPlatformCallbackHandler platformCallbackHandler = new SteamCallbackHandler();

	private bool isInitialized;

	private void Start()
	{
		if (!platformCallbackHandler.Initialize())
		{
			Object.Destroy(this);
			return;
		}
		isInitialized = true;
		Object.DontDestroyOnLoad(this);
	}

	private void OnDisable()
	{
		if (isInitialized)
		{
			platformCallbackHandler.Dispose();
		}
	}
}
