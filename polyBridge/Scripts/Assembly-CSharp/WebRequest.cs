using UnityEngine;
using UnityEngine.Networking;

public class WebRequest
{
	private static readonly int WEBREQUEST_TIMEOUT_SECONDS = 30;

	public static UnityWebRequest Post(string endpoint, string token)
	{
		WWWForm form = new WWWForm();
		return Post(endpoint, token, form);
	}

	public static UnityWebRequest Post(string endpoint, string token, WWWForm form)
	{
		UnityWebRequest unityWebRequest = UnityWebRequest.Post(endpoint, form);
		unityWebRequest.SetRequestHeader("Authorization", "Bearer " + token);
		unityWebRequest.timeout = WEBREQUEST_TIMEOUT_SECONDS;
		return unityWebRequest;
	}

	public static UnityWebRequest Get(string endpoint, string token)
	{
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(endpoint);
		unityWebRequest.SetRequestHeader("Authorization", "Bearer " + token);
		unityWebRequest.timeout = WEBREQUEST_TIMEOUT_SECONDS;
		return unityWebRequest;
	}

	public static UnityWebRequest GetTexture(string endpoint)
	{
		UnityWebRequest texture = UnityWebRequestTexture.GetTexture(endpoint);
		texture.timeout = WEBREQUEST_TIMEOUT_SECONDS;
		return texture;
	}

	public static byte[] ReadAllBytes(string filepath)
	{
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(filepath);
		unityWebRequest.SendWebRequest();
		while (!unityWebRequest.isDone)
		{
			if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
			{
				return null;
			}
		}
		return unityWebRequest.downloadHandler.data;
	}

	public static string ReadAllText(string filepath)
	{
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(filepath);
		unityWebRequest.SendWebRequest();
		while (!unityWebRequest.isDone)
		{
			if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
			{
				return null;
			}
		}
		return unityWebRequest.downloadHandler.text;
	}

	public static string GetErrorMessage(UnityWebRequest request)
	{
		if (!string.IsNullOrEmpty(request.error))
		{
			return request.error;
		}
		return Localize.Get("WARN_UNKNOWN_ERROR");
	}
}
