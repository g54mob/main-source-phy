namespace Heathen.SteamworksIntegration
{
	public interface IModularField
	{
		int Priority { get; }

		bool Synchronised { get; }

		string Header { get; }
	}
}
