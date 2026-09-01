public class CommandRegistration : AbstractRegistration
{
	public CommandHandler Handler { get; private set; }

	public CommandRegistration(string name, CommandHandler handler, string help)
		: base(name, help)
	{
		Handler = handler;
	}
}
