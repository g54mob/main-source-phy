using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamListParser))]
public class SteamVideoList : MonoBehaviour
{
	public GameObject bottomBar;

	public GameObject template;

	public SteamListParser.SteamVideoFilter filter = SteamListParser.SteamVideoFilter.MostRecent;

	public int videoAmount = 20;

	private int pageIndex = 1;

	private SteamListParser parser;

	private int videoCount;

	public void Awake()
	{
		parser = GetComponent<SteamListParser>();
		SteamListParser steamListParser = parser;
		steamListParser.OnGetContent = (Action<SteamBaseParser.ParserType, List<SteamBaseParser.BaseSteamEntry>>)Delegate.Combine(steamListParser.OnGetContent, new Action<SteamBaseParser.ParserType, List<SteamBaseParser.BaseSteamEntry>>(OnGetVideoList));
		template.SetActive(false);
		RequestNewPage();
	}

	public void RequestNewPage()
	{
		parser.GetVideoContent(SteamListParser.SteamVideoFilter.MostRecent, pageIndex++);
	}

	private void OnGetVideoList(SteamBaseParser.ParserType parserType, List<SteamBaseParser.BaseSteamEntry> entries)
	{
		if (parserType != SteamBaseParser.ParserType.Video)
		{
			return;
		}
		foreach (SteamVideoParser.VideoEntry entry in entries)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(template);
			gameObject.SetActive(true);
			SteamVideoItem componentInChildren = gameObject.GetComponentInChildren<SteamVideoItem>();
			componentInChildren.SetEntry(entry);
			gameObject.transform.SetParent(base.transform, false);
			videoCount++;
		}
		if (entries.Count > 0 && videoCount < videoAmount)
		{
			RequestNewPage();
		}
	}
}
