using BitCode.Users;

namespace BitCode.Networking
{
	public interface IMultiplayerSessionJoinParameters
	{
		ILocalAccount User { get; }
	}
}
