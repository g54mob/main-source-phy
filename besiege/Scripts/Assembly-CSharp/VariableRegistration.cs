public class VariableRegistration : AbstractRegistration
{
	public MutableWrapper Variable { get; private set; }

	public VariableRegistration(string name, MutableWrapper wrapper, string help)
		: base(name, help)
	{
		Variable = wrapper;
	}
}
