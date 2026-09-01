public interface IEntryPointHandler
{
	void ConnectToLobby(ulong lobbySteamId, string password);

	void ConnectToServer(string ipAddress, int port, string password);

	void ConnectToServer(ulong serverId, string password);

	void StartServer(DedicatedServerMode mode);

	void TestMachine(string machinePath, float testDuration, bool isHeadless, int numTestMachines);
}
