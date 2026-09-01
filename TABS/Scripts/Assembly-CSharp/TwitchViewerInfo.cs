using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TwitchViewerInfo : MonoBehaviour
{
	[HideInInspector]
	public string channelName;

	[HideInInspector]
	public TwitchViewers Viewers = new TwitchViewers();

	public bool HasPop;

	public bool IsCurrentlyGettingPop;

	public void PopulateViewers()
	{
		HasPop = false;
		string uri = "https://tmi.twitch.tv/group/user/" + channelName + "/chatters";
		IsCurrentlyGettingPop = true;
		StartCoroutine(GetRequest(uri));
	}

	public string GetRandomViewer()
	{
		return "";
	}

	private IEnumerator GetRequest(string uri)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
			yield return webRequest.Send();
			string[] array = uri.Split('/');
			int num = array.Length - 1;
			if (webRequest.isNetworkError)
			{
				Debug.Log(array[num] + ": Error: " + webRequest.error);
			}
			else
			{
				string json = webRequest.downloadHandler.text.Replace("\n", "");
				Viewers = JsonUtility.FromJson<TwitchViewers>(json);
			}
		}
		HasPop = true;
		IsCurrentlyGettingPop = false;
	}
}
