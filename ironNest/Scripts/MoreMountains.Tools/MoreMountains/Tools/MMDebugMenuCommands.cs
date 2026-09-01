using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMDebugMenuCommands : MonoBehaviour
	{
		[MMDebugLogCommand]
		public static void Now()
		{
		}

		[MMDebugLogCommand]
		public static void Clear()
		{
		}

		[MMDebugLogCommand]
		public static void Restart()
		{
		}

		[MMDebugLogCommand]
		public static void Reload()
		{
		}

		[MMDebugLogCommand]
		public static void Sysinfo()
		{
		}

		[MMDebugLogCommand]
		public static void Quit()
		{
		}

		[MMDebugLogCommand]
		public static void Exit()
		{
		}

		[MMDebugLogCommand]
		public static void Help()
		{
		}

		private static void InternalQuit()
		{
		}

		[MMDebugLogCommandArgumentCount(1)]
		[MMDebugLogCommand]
		public static void Vsync(string[] args)
		{
		}

		[MMDebugLogCommandArgumentCount(1)]
		[MMDebugLogCommand]
		public static void Framerate(string[] args)
		{
		}

		[MMDebugLogCommandArgumentCount(1)]
		[MMDebugLogCommand]
		public static void Timescale(string[] args)
		{
		}

		[MMDebugLogCommandArgumentCount(2)]
		[MMDebugLogCommand]
		public static void Biggest(string[] args)
		{
		}
	}
}
