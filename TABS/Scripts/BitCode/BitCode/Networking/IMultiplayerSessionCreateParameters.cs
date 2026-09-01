using BitCode.Users;

namespace BitCode.Networking
{
	public interface IMultiplayerSessionCreateParameters
	{
		ILocalAccount User { get; }
	}
}
