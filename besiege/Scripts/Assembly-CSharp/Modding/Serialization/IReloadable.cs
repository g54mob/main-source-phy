namespace Modding.Serialization
{
	public interface IReloadable
	{
		void OnReload(IReloadable newObject);

		void PreprocessForReloading();
	}
}
