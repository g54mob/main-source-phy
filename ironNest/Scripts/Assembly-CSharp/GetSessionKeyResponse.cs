using System;

public class GetSessionKeyResponse
{
	public Guid SessionId { get; set; }

	public DateTime ExpiresAtUtc { get; set; }
}
