namespace TFBGames
{
	public class DeactivateGameObjectPlatformOverride : PlatformSpecificOverride
	{
		protected override void ApplyPlatformOverride()
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
