using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamListParser : MonoBehaviour
{
	public enum SteamVideoFilter
	{
		MostPopularAllTime = 0,
		MostPopularToday = 1,
		MostPopularWeek = 2,
		MostPopularThreeMonths = 3,
		MostPopularSixMonths = 4,
		MostPopularYear = 5,
		MostRecent = 6
	}

	public enum SteamNewsFilter
	{
		AllNews = 0,
		Announcements = 1
	}

	public int AppID = 346010;

	public Action<SteamBaseParser.ParserType, List<SteamBaseParser.BaseSteamEntry>> OnGetContent;

	public void GetVideoContent(SteamVideoFilter filter, int page)
	{
		GetContent(new SteamVideoParser(AppID), (int)filter, page);
	}

	public void GetNewsContent(SteamNewsFilter filter, int page)
	{
		GetContent(new SteamNewsParser(AppID), (int)filter, page);
	}

	private void GetContent(SteamBaseParser parser, int filter, int page)
	{
		StartCoroutine(RequestPage(parser, filter, page));
	}

	private IEnumerator RequestPage(SteamBaseParser parser, int filter, int page)
	{
		WWW www = new WWW(parser.GetURL(filter, page));
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			Debug.LogError("Couldn't fetch list! Error message: " + www.error);
			yield break;
		}
		List<SteamBaseParser.BaseSteamEntry> entries = parser.ParsePage(www.text);
		if (OnGetContent != null)
		{
			OnGetContent(parser.Type, entries);
		}
	}
}
