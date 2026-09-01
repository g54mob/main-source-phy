using System;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

public sealed class ValidateSimplificationOptionsException : Exception
{
	private readonly string propertyName;

	public string PropertyName => propertyName;

	public override string Message
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E47]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			string message = base.Message;
			string newLine = Environment.NewLine;
			return message + newLine + "Property name: " + propertyName;
		}
	}

	public ValidateSimplificationOptionsException(string propertyName, string message)
		: base(message)
	{
		this.propertyName = propertyName;
	}

	public ValidateSimplificationOptionsException(string propertyName, string message, Exception innerException)
		: base(message, innerException)
	{
		this.propertyName = propertyName;
	}
}
