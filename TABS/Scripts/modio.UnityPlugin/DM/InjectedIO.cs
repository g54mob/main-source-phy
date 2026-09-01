using ModIO;
using UnityEngine;

namespace DM
{
	public static class InjectedIO
	{
		public static IUserDataIO userDataIO;

		public static IPlatformIO platformIO;

		static InjectedIO()
		{
		}

		public static void inject(IUserDataIO userDataIO, IPlatformIO platformIO)
		{
			Debug.Log("DM: Injecting custom ModIO.IUserDataIO & ModIO.IPlatformIO");
			InjectedIO.userDataIO = userDataIO;
			InjectedIO.platformIO = platformIO;
		}
	}
}
