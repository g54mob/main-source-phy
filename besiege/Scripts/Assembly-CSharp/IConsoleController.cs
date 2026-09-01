public interface IConsoleController
{
	void AppendLogLine(string message);

	void HandleRconCommand(ushort playerId, string password, string command, string[] args);
}
