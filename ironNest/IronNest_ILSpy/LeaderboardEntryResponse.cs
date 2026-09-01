using System;
using Cpp2ILInjected;

public class LeaderboardEntryResponse
{
	private Guid? _003CUserId_003Ek__BackingField;

	private int _003CPosition_003Ek__BackingField;

	private string _003CUsername_003Ek__BackingField;

	private string _003CAvatarBase64_003Ek__BackingField;

	private int _003CScore_003Ek__BackingField;

	private string _003CImageUrl_003Ek__BackingField;

	private string _003CGifUrl_003Ek__BackingField;

	private string _003CZipUrl_003Ek__BackingField;

	private DateTime _003CCreatedAtUtc_003Ek__BackingField;

	private bool _003CIsPendingLocal_003Ek__BackingField;

	private string _003CLocalReplayPath_003Ek__BackingField;

	public Guid? UserId
	{
		get
		{
			//IL_0010: Expected O, but got I
			//IL_0018: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+10]");
			LeaderboardEntryResponse leaderboardEntryResponse = (LeaderboardEntryResponse)0;
			_003CUserId_003Ek__BackingField = (Guid?)(object)0;
			return (Guid?)this;
		}
		set
		{
			_003CUserId_003Ek__BackingField = value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (System.Nullable`1<System.Guid>)+10]");
			_ = 0;
		}
	}

	public int Position
	{
		get
		{
			return _003CPosition_003Ek__BackingField;
		}
		set
		{
			_003CPosition_003Ek__BackingField = value;
		}
	}

	public string Username
	{
		get
		{
			return _003CUsername_003Ek__BackingField;
		}
		set
		{
			_003CUsername_003Ek__BackingField = value;
		}
	}

	public string AvatarBase64
	{
		get
		{
			return _003CAvatarBase64_003Ek__BackingField;
		}
		set
		{
			_003CAvatarBase64_003Ek__BackingField = value;
		}
	}

	public int Score
	{
		get
		{
			return _003CScore_003Ek__BackingField;
		}
		set
		{
			_003CScore_003Ek__BackingField = value;
		}
	}

	public string ImageUrl
	{
		get
		{
			return _003CImageUrl_003Ek__BackingField;
		}
		set
		{
			_003CImageUrl_003Ek__BackingField = value;
		}
	}

	public string GifUrl
	{
		get
		{
			return _003CGifUrl_003Ek__BackingField;
		}
		set
		{
			_003CGifUrl_003Ek__BackingField = value;
		}
	}

	public string ZipUrl
	{
		get
		{
			return _003CZipUrl_003Ek__BackingField;
		}
		set
		{
			_003CZipUrl_003Ek__BackingField = value;
		}
	}

	public DateTime CreatedAtUtc
	{
		get
		{
			return _003CCreatedAtUtc_003Ek__BackingField;
		}
		set
		{
			_003CCreatedAtUtc_003Ek__BackingField = value;
		}
	}

	public bool IsPendingLocal
	{
		get
		{
			return _003CIsPendingLocal_003Ek__BackingField;
		}
		set
		{
			_003CIsPendingLocal_003Ek__BackingField = value;
		}
	}

	public string LocalReplayPath
	{
		get
		{
			return _003CLocalReplayPath_003Ek__BackingField;
		}
		set
		{
			_003CLocalReplayPath_003Ek__BackingField = value;
		}
	}
}
