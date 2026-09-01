using System.Collections.Generic;

public class TwitchChatters
{
	public HashSet<string> hashes = new HashSet<string>();

	public Dictionary<string, ActiveChatter> broadcaster = new Dictionary<string, ActiveChatter>();

	public Dictionary<string, ActiveChatter> vips = new Dictionary<string, ActiveChatter>();

	public Dictionary<string, ActiveChatter> moderators = new Dictionary<string, ActiveChatter>();

	public Dictionary<string, ActiveChatter> staff = new Dictionary<string, ActiveChatter>();

	public Dictionary<string, ActiveChatter> admins = new Dictionary<string, ActiveChatter>();

	public Dictionary<string, ActiveChatter> global_mods = new Dictionary<string, ActiveChatter>();

	public Dictionary<string, ActiveChatter> viewers = new Dictionary<string, ActiveChatter>();

	public Dictionary<string, ActiveChatter> subscribers = new Dictionary<string, ActiveChatter>();
}
