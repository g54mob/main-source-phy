using System;
using Newtonsoft.Json;

public class LeaderboardEntryResponse
{
	public Guid? UserId { get; set; }

	public int Position { get; set; }

	public string Username { get; set; }

	public string AvatarBase64 { get; set; }

	public int Score { get; set; }

	public string ImageUrl { get; set; }

	public string GifUrl { get; set; }

	public string ZipUrl { get; set; }

	public DateTime CreatedAtUtc { get; set; }

	[JsonIgnore]
	public bool IsPendingLocal { get; set; }

	[JsonIgnore]
	public string LocalReplayPath { get; set; }
}
