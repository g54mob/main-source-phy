using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XPackageInstallationProgress
	{
		public ulong TotalBytes { get; private set; }

		public ulong InstalledBytes { get; private set; }

		public ulong LaunchBytes { get; private set; }

		public bool Launchable { get; private set; }

		public bool Completed { get; private set; }

		internal XPackageInstallationProgress(XGamingRuntime.Interop.XPackageInstallationProgress interopStruct)
		{
			TotalBytes = interopStruct.totalBytes;
			InstalledBytes = interopStruct.installedBytes;
			LaunchBytes = interopStruct.launchBytes;
			Launchable = interopStruct.launchable.Value;
			Completed = interopStruct.completed.Value;
		}
	}
}
