public class AbstractRegistration
{
	public string Name { get; private set; }

	public string Help { get; private set; }

	public AbstractRegistration(string name, string help)
	{
		Name = name;
		Help = help;
	}
}
