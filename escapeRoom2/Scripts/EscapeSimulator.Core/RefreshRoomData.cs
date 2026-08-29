using System;

public class RefreshRoomData
{
	public string id;

	public ulong steamId;

	public string roomTitle;

	public string imageUrl;

	public DateTime modified;

	public ESUGC eSUGC;

	public bool shouldBeDeleted;

	public RefreshRoomData(string id, string roomTitle, string imageUrl, DateTime modified, ESUGC eSUGC)
	{
		this.id = id;
		this.roomTitle = roomTitle;
		this.imageUrl = imageUrl;
		this.modified = modified;
		this.eSUGC = eSUGC;
	}

	public RefreshRoomData(string id, ulong steamId, string roomTitle, string imageUrl, DateTime modified, ESUGC eSUGC)
		: this(id, roomTitle, imageUrl, modified, eSUGC)
	{
		this.steamId = steamId;
	}
}
