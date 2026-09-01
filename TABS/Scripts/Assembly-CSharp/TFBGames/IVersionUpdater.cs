namespace TFBGames
{
	public interface IVersionUpdater
	{
		void DoUpdate(int oldVersion, int newVersion);
	}
}
