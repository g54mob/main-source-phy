namespace Kamgam.SettingsGenerator
{
	public class CatBlendShapeCustomizationConnection : Connection<float>
	{
		private readonly bool eyes;

		private readonly bool body;

		private readonly bool fur;

		private readonly bool whiskers;

		private readonly int blendShapeIndex;

		private CatCustomizationController catCustomization;

		public CatBlendShapeCustomizationConnection(bool eyes, bool body, bool fur, bool whiskers, int blendShapeIndex)
		{
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float value)
		{
		}

		private void ResolveReferenceIfNeeded()
		{
		}
	}
}
