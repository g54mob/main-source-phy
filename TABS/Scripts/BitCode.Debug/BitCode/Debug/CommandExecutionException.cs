using System;

namespace BitCode.Debug
{
	public class CommandExecutionException : CommandInvocationException
	{
		public readonly string CommandName;

		public CommandExecutionException(string commandName)
		{
			while (true)
			{
				int num = 1569151368;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x2D4C38F3)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						goto IL_0028;
					case 2u:
						return;
					}
					break;
					IL_0028:
					CommandName = commandName;
					num = ((int)num2 * -56613271) ^ 0x2672C8CD;
				}
			}
		}

		public CommandExecutionException(string commandName, string message)
			: base(message)
		{
			CommandName = commandName;
		}

		public CommandExecutionException(string commandName, string message, Exception innerException)
			: base(message, innerException)
		{
			while (true)
			{
				int num = 745781082;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x674DE32E)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_002a;
					case 0u:
						return;
					}
					break;
					IL_002a:
					CommandName = commandName;
					num = ((int)num2 * -738625210) ^ -984310474;
				}
			}
		}
	}
}
