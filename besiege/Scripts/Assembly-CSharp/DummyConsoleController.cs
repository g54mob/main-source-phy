public class DummyConsoleController : IConsoleController
{
	public void AppendLogLine(string message)
	{
	}

	public void HandleRconCommand(ushort playerId, string password, string command, string[] args)
	{
	}
}
