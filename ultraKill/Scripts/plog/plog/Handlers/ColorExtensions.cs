using System;
using plog.Models;

namespace plog.Handlers
{
	public static class ColorExtensions
	{
		public static ConsoleColor ToConsoleColor(this UniversalColor color)
		{
			return (ConsoleColor)((((color.Red > 128) | (color.Green > 128) | (color.Blue > 128)) ? 8 : 0) | ((color.Red > 64) ? 4 : 0) | ((color.Green > 64) ? 2 : 0) | ((color.Blue > 64) ? 1 : 0));
		}
	}
}
