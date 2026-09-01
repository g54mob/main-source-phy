namespace MoreMountains.Tools
{
	public interface IMMPersistent
	{
		string GetGuid();

		string OnSave();

		void OnLoad(string data);

		bool ShouldBeSaved();
	}
}
