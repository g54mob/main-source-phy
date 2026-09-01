using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public abstract class SteamBaseParser
{
	public enum ParserType
	{
		Video = 0,
		News = 1
	}

	public class BaseSteamEntry
	{
		public string Title { get; private set; }

		public int Comments { get; private set; }

		public int Upvotes { get; private set; }

		public BaseSteamEntry(string title, int upvotes, int comments)
		{
			Title = title;
			Upvotes = upvotes;
			Comments = comments;
		}
	}

	public ParserType Type;

	protected int ItemsPerPage = 10;

	protected int AppID;

	protected Regex upvoteRegex = new Regex("rateUp[^>]>\\s*([0-9,]+)");

	public SteamBaseParser(int appId)
	{
		AppID = appId;
	}

	public List<BaseSteamEntry> ParsePage(string pageData)
	{
		List<BaseSteamEntry> list = new List<BaseSteamEntry>();
		string[] array = pageData.Split(new string[1] { "interactable" }, StringSplitOptions.None);
		for (int i = 1; i < array.Length; i++)
		{
			BaseSteamEntry baseSteamEntry = ParseEntry(array[i]);
			if (baseSteamEntry != null)
			{
				list.Add(baseSteamEntry);
			}
		}
		return list;
	}

	protected abstract BaseSteamEntry ParseEntry(string entryData);

	protected abstract string GetFilter(int filter);

	protected int GetNumber(string number)
	{
		int result = 0;
		if (!int.TryParse(number.Replace(",", string.Empty), out result))
		{
			Debug.LogError("Couldn't parse number: " + number);
		}
		return result;
	}

	public abstract string GetURL(int filter, int page);

	protected string GetBaseURL(int appID)
	{
		return "http://steamcommunity.com/app/" + appID + "/homecontent/";
	}

	protected string GetURLSuffix(int page)
	{
		return "&itemspage=" + page + "&screenshotspage=" + page + "&videospage=" + page + "&artpage=" + page + "&allguidepage=" + page + "&webguidepage=" + page + "&integratedguidepage=" + page + "&discussionspage=" + page + "&l=english&filterLanguage=default&searchText=&forceanon=1";
	}
}
