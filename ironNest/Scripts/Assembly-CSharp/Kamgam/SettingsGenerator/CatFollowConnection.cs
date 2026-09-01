namespace Kamgam.SettingsGenerator
{
	public class CatFollowConnection : Connection<bool>
	{
		private CatMovementManager catMovement;

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool value)
		{
		}

		private void ResolveReferenceIfNeeded()
		{
		}
	}
}
