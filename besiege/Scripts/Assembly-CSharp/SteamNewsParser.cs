using System.Text.RegularExpressions;

public class SteamNewsParser : SteamBaseParser
{
	public class NewsEntry : BaseSteamEntry
	{
		public NewsEntry(string title, int upvotes, int comments)
			: base(title, upvotes, comments)
		{
		}
	}

	private string[] filters = new string[1] { string.Empty };

	private Regex titleRegex = new Regex("CardContentNewsTitle[^>]+>([^<]+)");

	private Regex commentRegex = new Regex("CardCommentCount[^>]+>\\s*([0-9,]+)");

	public SteamNewsParser(int appId)
		: base(appId)
	{
		Type = ParserType.News;
	}

	public override string GetURL(int filter, int page)
	{
		return GetBaseURL(AppID) + "?announcementsoffset=" + (page - 1) * ItemsPerPage + "&userreviewsoffset=0&p=" + page + "&appHubSubSection=14&browsefilter=trend" + GetURLSuffix(page);
	}

	protected override string GetFilter(int filter)
	{
		return filters[filter];
	}

	protected override BaseSteamEntry ParseEntry(string entryData)
	{
		Match match = titleRegex.Match(entryData);
		Match match2 = upvoteRegex.Match(entryData);
		Match match3 = commentRegex.Match(entryData);
		if (!match.Success || !match2.Success || !match3.Success)
		{
			return null;
		}
		string value = match.Groups[1].Value;
		int number = GetNumber(match3.Groups[1].Value);
		int number2 = GetNumber(match2.Groups[1].Value);
		return new NewsEntry(value, number2, number);
	}
}
