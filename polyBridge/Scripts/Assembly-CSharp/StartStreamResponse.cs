using System;

[Serializable]
public class StartStreamResponse
{
	public string id;

	public Owner streamer;

	public string twitch_channel_id;

	public string twitch_channel_name;

	public bool enabled;

	public int submissions_cooldown;

	public bool subscribers_only;

	public bool moderated;

	public bool chat_bot_enabled;

	public int chat_bot_interval;

	public bool can_use_bits;

	public bool bits_enabled;

	public bool bits_only;
}
