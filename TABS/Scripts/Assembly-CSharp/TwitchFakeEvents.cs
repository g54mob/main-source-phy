using UnityEngine;

public class TwitchFakeEvents : MonoBehaviour
{
	private string[] RandomWords = new string[6] { "Never", "Gonna", "Give", "You", "Up", "Kappa" };

	private string[] FakeUsers = new string[5] { "Alpha", "Beta", "Gamma", "Delta", "Epsilon" };

	public int MinBitDono = 10;

	public int MaxBitDono = 10000;

	private bool isFakeConnected;

	private string GetUser()
	{
		return FakeUsers[Random.Range(0, FakeUsers.Length)] + "_" + Random.Range(1, 1000);
	}

	private void Update()
	{
		if (!isFakeConnected)
		{
			TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
			if ((bool)service)
			{
				service.FakeConnect();
				isFakeConnected = true;
			}
		}
	}

	public void FakeChatMSG()
	{
		string user = GetUser();
		string text = "@badge-info=;badges=premium;color=;display-name=<Name>;emotes=;flags=;id=GenericID;mod=0;room-id=42;subscriber=0;tmi-sent-ts=14;turbo=0;user-id=12;user-type= :<Name>!<Name>@<Name>.tmi.twitch.tv PRIVMSG #GenericStream :<StreamText>";
		text = text.Replace("<Name>", user);
		string text2 = "";
		bool flag = false;
		for (int i = 0; i < 6; i++)
		{
			if (Random.Range(0, 100) > 50 || !flag)
			{
				flag = true;
				int num = Random.Range(0, RandomWords.Length);
				text2 = text2 + RandomWords[num] + " ";
			}
		}
		text = text.Replace("<StreamText>", text2);
		ServiceLocator.GetService<TwitchHandler>().IRC.FakeMessage(text);
	}

	public void FakeSubscriber()
	{
		string text = "@badge-info=;badges=premium/1;color=#19B321;display-name=<Name>;emotes=;flags=;id=GenericID;login=<Name>;mod=0;msg-id=sub;msg-param-cumulative-months=5;msg-param-months=0;msg-param-should-share-streak=0;msg-param-sub-plan-name=Channel\\sSubscription\\s(BigStreamer);msg-param-sub-plan=Prime;room-id=1;subscriber=1;system-msg=ComradeXades88\\ssubscribed\\swith\\sTwitch\\sPrime.;tmi-sent-ts=123;user-id=42;user-type= :tmi.twitch.tv USERNOTICE #bigstreamer";
		string user = GetUser();
		text = text.Replace("<Name>", user);
		ServiceLocator.GetService<TwitchHandler>().IRC.FakeMessage(text);
	}

	public void FakeBitDonation()
	{
		string user = GetUser();
		int num = Random.Range(MinBitDono, MaxBitDono);
		string text = "@badge-info=;badges=bits/<NoBits>;bits=<NoBits>;color=;display-name=<Name>;emotes=;flags=;id=GenericID;mod=0;room-id=42;subscriber=0;tmi-sent-ts=14;turbo=0;user-id=12;user-type= :<Name>!<Name>@<Name>.tmi.twitch.tv PRIVMSG #GenericStream :Cheer<NoBits> Kappa";
		text = text.Replace("<NoBits>", num.ToString());
		text = text.Replace("<Name>", user);
		ServiceLocator.GetService<TwitchHandler>().IRC.FakeMessage(text);
	}
}
