using System.Collections.Generic;

namespace Mono.CSharp
{
	internal sealed class ErrorMessage : AbstractMessage
	{
		public override bool IsWarning
		{
			get
			{
				return false;
			}
		}

		public override string MessageType
		{
			get
			{
				return "error";
			}
		}

		public ErrorMessage(int code, Location loc, string message, List<string> extraInfo)
			: base(code, loc, message, extraInfo)
		{
		}

		public ErrorMessage(AbstractMessage aMsg)
			: base(aMsg)
		{
		}
	}
}
