namespace XGamingRuntime.Interop
{
	internal struct XPackageInstallationProgress
	{
		internal readonly ulong totalBytes;

		internal readonly ulong installedBytes;

		internal readonly ulong launchBytes;

		internal readonly NativeBool launchable;

		internal readonly NativeBool completed;

		internal XPackageInstallationProgress(XGamingRuntime.XPackageInstallationProgress publicObject)
		{
			totalBytes = publicObject.TotalBytes;
			installedBytes = publicObject.InstalledBytes;
			launchBytes = publicObject.LaunchBytes;
			launchable = new NativeBool(publicObject.Launchable);
			completed = new NativeBool(publicObject.Completed);
		}
	}
}
