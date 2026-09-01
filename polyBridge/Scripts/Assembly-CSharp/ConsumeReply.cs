using System;

[Serializable]
public class ConsumeReply
{
	public string id;

	public Owner owner;

	public string payload;

	public string level_hash;

	public bool accepted;

	public int twitch_bits_used;
}
