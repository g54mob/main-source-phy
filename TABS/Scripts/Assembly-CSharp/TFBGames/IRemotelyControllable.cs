namespace TFBGames
{
	public interface IRemotelyControllable
	{
		bool IsRemotelyControlled { get; }

		void SetIsRemotelyControlled(bool isRemotelyControlled);
	}
}
