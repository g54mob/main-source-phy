using InternalModding.Mods;

namespace InternalModding.Loading
{
	public interface IComponentProvider
	{
		bool ActiveInSingleplayer { get; }

		bool LoadMod(ModContainer mod);

		bool ActivateMod(ModContainer mod);

		void RegisterPrefabs(ModContainer mod);

		void UnregisterPrefabs(ModContainer mod);

		void PostRegisterPrefabs();
	}
}
