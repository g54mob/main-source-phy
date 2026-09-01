using System.Text.RegularExpressions;

public class SteamVideoParser : SteamBaseParser
{
	public class VideoEntry : BaseSteamEntry
	{
		public string VideoURL { get; private set; }

		public string ImageURL { get; private set; }

		public string ContentLink { get; private set; }

		public string OwnerURL { get; private set; }

		public string Owner { get; private set; }

		public VideoEntry(string title, string image, string link, string owner, string ownerlink, int upvotes, int comments)
			: base(title, upvotes, comments)
		{
			ImageURL = image;
			ContentLink = link;
			Owner = owner;
			OwnerURL = ownerlink;
		}
	}

	private string[] filters = new string[7] { "toprated", "trendday", "trendweek", "trendthreemonths", "trendsixmonths", "trendyear", "mostrecent" };

	private Regex titleRegex = new Regex("CardContentTitle[^>]+>([^<]+)");

	private Regex linkRegex = new Regex("\"(http[^?]+\\?id=[0-9]+)");

	private Regex imagePreviewRegex = new Regex("CardContentPreviewImage\"\\s+src=\"([^\"]+)\"");

	private Regex userRegex = new Regex("CardContentAuthorName[^=]+=\"([^\"]+)\"\\s*>([^<]+)");

	private Regex commentRegex = new Regex("CardCommentCount[^>]+>\\s*([0-9,]+)");

	public SteamVideoParser(int appId)
		: base(appId)
	{
		Type = ParserType.Video;
	}

	public override string GetURL(int filter, int page)
	{
		return GetBaseURL(AppID) + "?userreviewsoffset=0&p=" + page + "&appid=" + AppID + "&appHubSubSection=3&appHubSubSection=3&browsefilter=" + GetFilter(filter) + GetURLSuffix(page);
	}

	protected override string GetFilter(int filter)
	{
		return filters[filter];
	}

	protected override BaseSteamEntry ParseEntry(string entryData)
	{
		Match match = imagePreviewRegex.Match(entryData);
		Match match2 = titleRegex.Match(entryData);
		Match match3 = userRegex.Match(entryData);
		Match match4 = linkRegex.Match(entryData);
		Match match5 = upvoteRegex.Match(entryData);
		Match match6 = commentRegex.Match(entryData);
		if (!match.Success || !match2.Success || !match3.Success || !match4.Success)
		{
			return null;
		}
		string value = match.Groups[1].Value;
		string value2 = match2.Groups[1].Value;
		string value3 = match3.Groups[1].Value;
		string value4 = match3.Groups[2].Value;
		string value5 = match4.Groups[1].Value;
		int comments = (match6.Success ? GetNumber(match6.Groups[1].Value) : 0);
		int upvotes = (match5.Success ? GetNumber(match5.Groups[1].Value) : 0);
		return new VideoEntry(value2, value, value5, value4, value3, upvotes, comments);
	}
}
