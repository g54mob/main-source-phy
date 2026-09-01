namespace TFBGames
{
	public class VersionUpdaterService : ServicePrefab
	{
		private const int Version = 11;

		private const string VersionKey = "version";

		public override void OnStart()
		{
			ServiceLocator.GetService<WaitForStorage>().FireWhenReady(OnStorageReady);
		}

		private void OnStorageReady()
		{
			IPlayerPrefsPlatform service = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			int num = service.GetInt("version", 0);
			if (num != 11)
			{
				IVersionUpdater[] components = GetComponents<IVersionUpdater>();
				int i = 0;
				for (int num2 = components.Length; i < num2; i++)
				{
					components[i].DoUpdate(num, 11);
				}
				service.SetInt("version", 11);
				service.Save();
			}
		}
	}
}
