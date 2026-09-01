using System;

public class RegisterRequest
{
	public Guid? UserId { get; set; }

	public long? SteamId { get; set; }

	public long? GogId { get; set; }

	public string DeviceId { get; set; }

	public string Username { get; set; }

	public string AvatarBase64 { get; set; }
}
