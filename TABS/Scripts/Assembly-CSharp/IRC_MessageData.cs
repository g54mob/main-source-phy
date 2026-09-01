using System.Collections.Generic;

public struct IRC_MessageData
{
	public Dictionary<string, string> tags;

	public string command;

	public string channel;

	public string text;
}
