using System;
using Cpp2ILInjected;

public class RegisterRequest
{
	private Guid? _003CUserId_003Ek__BackingField;

	private long? _003CSteamId_003Ek__BackingField;

	private long? _003CGogId_003Ek__BackingField;

	private string _003CDeviceId_003Ek__BackingField;

	private string _003CUsername_003Ek__BackingField;

	private string _003CAvatarBase64_003Ek__BackingField;

	public Guid? UserId
	{
		get
		{
			//IL_0010: Expected O, but got I
			//IL_0018: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+10]");
			RegisterRequest registerRequest = (RegisterRequest)0;
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

	public long? SteamId
	{
		get
		{
			//IL_0010: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+28]");
			RegisterRequest registerRequest = (RegisterRequest)0;
			return (long?)this;
		}
		set
		{
			_003CSteamId_003Ek__BackingField = value;
		}
	}

	public long? GogId
	{
		get
		{
			//IL_0010: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
			RegisterRequest registerRequest = (RegisterRequest)0;
			return (long?)this;
		}
		set
		{
			_003CGogId_003Ek__BackingField = value;
		}
	}

	public string DeviceId
	{
		get
		{
			return _003CDeviceId_003Ek__BackingField;
		}
		set
		{
			_003CDeviceId_003Ek__BackingField = value;
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
}
