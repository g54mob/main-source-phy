using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GalleryPreviewRequests
{
	public static Queue<string> m_RequestQ = new Queue<string>();

	private static readonly float PROCESS_INTERVAL_SECONDS = 0.1f;

	private static float m_NextProcessTime;

	public static void UpdateManual()
	{
		if (m_RequestQ.Count != 0)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			if (realtimeSinceStartup > m_NextProcessTime)
			{
				AsyncLoadPreviewTexture(m_RequestQ.Dequeue());
				m_NextProcessTime = realtimeSinceStartup + PROCESS_INTERVAL_SECONDS;
			}
		}
	}

	public static int NumInQ()
	{
		return m_RequestQ.Count;
	}

	public static void Clear()
	{
		m_RequestQ.Clear();
	}

	public static void Add(string url)
	{
		if (!m_RequestQ.Contains(url) && !string.IsNullOrEmpty(url))
		{
			m_RequestQ.Enqueue(url);
		}
	}

	public static void AsyncLoadPreviewTexture(string url)
	{
		WebRequest.GetTexture(url).SendWebRequest().completed += OnLoadPreviewComplete;
	}

	private static void OnLoadPreviewComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("Load gallery slot preview failed: " + errorMessage);
			AsyncLoadPreviewComplete(Path.GetFileName(unityWebRequestAsyncOperation.webRequest.url), null);
		}
		else
		{
			Texture2D content = DownloadHandlerTexture.GetContent(unityWebRequestAsyncOperation.webRequest);
			AsyncLoadPreviewComplete(Path.GetFileName(unityWebRequestAsyncOperation.webRequest.url), content);
		}
	}

	private static void AsyncLoadPreviewComplete(string url, Texture2D texture)
	{
		PreviewCache.Cache(url, texture);
	}
}
