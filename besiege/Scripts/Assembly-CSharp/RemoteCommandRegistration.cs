public class RemoteCommandRegistration : AbstractRegistration
{
	public RconCommandHandler Handler { get; private set; }

	public RemoteCommandRegistration(string name, RconCommandHandler handler, string help)
		: base(name, help)
	{
		Handler = handler;
	}
}
