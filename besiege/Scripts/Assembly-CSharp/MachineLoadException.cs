using System;

internal class MachineLoadException : Exception
{
	public MachineLoadException()
	{
	}

	public MachineLoadException(string message)
		: base(message)
	{
	}

	public MachineLoadException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
